//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;

namespace Wolfram.NETLink.Internal {

/// <summary>
/// A utility class used internally for creating delegate types that call back to Mathematica.
/// </summary>
/// <remarks>
/// This class is publc only because its CallMathematica method must be called from another assembly
/// (a dynamically-generated one). This class is not part of the public .NET/Link API.
/// </remarks>
///
public class DelegateHelper {

    // All types that hold dynamically created delegates are part of this module.
    private static ModuleBuilder delegateModuleBuilder;
    private static string delegateNamespace = "Wolfram.NETLink.DynamicDelegateNamespace";
    private static string delegateAssemblyName = "DynamicDelegateAssembly";
    private static string delegateModuleName = "DynamicDelegateModule";
    // Used only to ensure that each dynamic type has a unique name.
    private static int index = 1;

    // This hashtable is used to commnunicate the link object from createDynamicMethod() into
    // the CallMathematica() method. I could not find a better way to communicate that object than
    // to store it in a table and later look it up in CallMathematica. I could not figure out
    // how to get the link object into the code for the dynamically-generated delegateThunk method.
    // Some things I tried (like creating a static field to hold the value in the DummyType class)
    // gave "not supported in dynamic assembly" exceptions.
    private static Hashtable linkTable = new Hashtable();


    static DelegateHelper() {

        AssemblyName assemblyName = new AssemblyName();
        assemblyName.Name = delegateAssemblyName;
        AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        delegateModuleBuilder = assemblyBuilder.DefineDynamicModule(delegateModuleName);
    }

    internal static MethodInfo callMathematicaMethod = typeof(DelegateHelper).GetMethod("CallMathematica");


    /// <summary>
    /// This is the method that is called from dynamically-created delegates. It performs the actual call to Mathematica.
    /// </summary>
    /// <remarks>
    /// It is easiest to define this code in C# and then simply call it from dynamically-created delegates. We pull all the
    /// functionality shared by all callbacks up into this method. The dynamically-created delegates are just stubs that
    /// prepare the arguments for this method, call it, and pass on the return value.
    /// </remarks>
    /// 
    public static object CallMathematica(int linkHash, Type returnType, string func, int argsToSend, int callsUnshare, int wrapInNETBlock, params object[] args) {

        object result = null;

        // argsToSend is used as a bit field, where each bit position indicates whether or not to send to M the
        // arg at that position in the args[] array.
        int numArgsToSend = 0;
        bool[] sendThisArg = new bool[args.Length];
        for (int i = 0; i < args.Length; i++) {
            if ((argsToSend & (1 << i)) != 0) {
                numArgsToSend++;
                sendThisArg[i] = true;
            }
        }

        // Lookup the link object, which was stored in this table during createDynamicMethod(). If linkHash is 0,
        // use StdLink.Link.
        IKernelLink ml;
        if (linkHash == 0)
            ml = StdLink.Link;
        else
            ml = (IKernelLink) linkTable[linkHash];
        if (ml == null)
            return result;

        // StdLink.RequestTransaction() will throw if the request fails. If we just allow that throw to propagate
        // we lose any other event handling for this event. For example, if sharing is turned off manually while a
        // modeless window is up, then when the window's close box is clicked it will call back to M to execute
        // the Closed event handler (which turns off sharing). But StdLink.RequestTransaction() will of course throw
        // here (because sharing is already off), and that exception interferes with further processing of the event,
        // and the window never actually closes. To avoid scenarios like that, we simply bail out of the callback
        // if it throws, but don't let it propagate further. We can only do this if the handler returns void (like
        // virtually all do), because otherwise we would need a real result to return.
        try {
            StdLink.RequestTransaction();
        } catch (Exception e) {
            // TODO: Do something here for Unix.
            if (Utils.IsWindows)
                MessageBeep(0);
            if (returnType == typeof(void))
                return null;
            else
                throw e;
        }
        lock (ml) {
            // callsUnshare is an int (0 or 1) that is really a bool, but it's easier to manually push ints on the stack.
            // We need to use EnterExpressionPacket (or EnterTextPacket) for a call that might turn off kernel sharing.
            // That restriction is documented further in J/Link's Sharing.m file.
            ml.PutFunction(callsUnshare == 0 ? "EvaluatePacket" : "EnterExpressionPacket", 1);
            ml.PutFunction(Install.MMA_CALLBACKWRAPPER, 4);
            ml.Put(func);
            ml.PutFunction("List", numArgsToSend);
            for (int i = 0; i < args.Length; i++) {
                if (sendThisArg[i])
                    ml.Put(args[i]);
            }
            ml.Put(callsUnshare == 1);
            ml.Put(wrapInNETBlock == 1);
            ml.EndPacket();
            PacketType answerPkt;
            answerPkt = ml.WaitForAnswer();
            try {
                // Cannot get a return value if callsUnshare is set.
                if (callsUnshare == 0 && returnType != typeof(void)) {
                    // Result will come back as {argType, result}.
                    ml.CheckFunctionWithArgCount("List", 2);
                    int argType = ml.GetInteger();
                    result = Utils.readArgAs(ml, argType, returnType);
                }
            } catch (Exception) {
                throw new InvalidCastException("The return value from Mathematica was not of the expected type.");
            } finally {
                ml.ClearError();
                ml.NewPacket();
                if (answerPkt == PacketType.ReturnExpression) {
                    // Throw away the InputNamePacket that follows.
                    // This should never be hit, because the Install.MMA_CALLBACKWRAPPER function returns Null
                    // if we are using EnterExpressionPacket. But we'll leave the code here because if something
                    // goes wrong it might become necessary. For example, if this code is somehow used directly
                    // from a .NET language instead of being called from M, a possible error would be if the NETLink`
                    // package had not been read in and thus MMA_CALLBACKWRAPPER was not defined.
                    ml.NextPacket();
                    ml.NewPacket();
                }
            }
        }
        return result;
    }


    /***********************************************  createDynamicMethod  ***********************************************/

    /// <summary>
    /// This is the method that is called by the Mathematica function NETNewDelegate.
    /// </summary>
    /// <remarks>
    /// It dynamically creates a new method with a given signature that calls back to Mathematica in its implementation
    /// (the dynamic method does this by calling the CallMathematica method defined above). This method just creates a
    /// method; it is elsewhere passed in to Delegate.CreateDelegate to create the actual Delegate object that is returned
    /// as the result of the NETNewDelegate function.
    /// </remarks>
    /// <returns>The created method.</returns>
    /// 
    internal static MethodInfo createDynamicMethod(IKernelLink ml, Type delegateType, string mFunc, int argsToSend, bool callsUnshare, bool wrapInNETBlock) {

        // Store the link object in a table, keyed by its hashcode. We send the hashcode value in to
        // CallMathematica method. The link is then looked up via this table. This is just a means to
        // communicate the link through the delegateThunk() method and into CallMathematica().
        // If link is null, then send 0 for the hash, which means "use StdLink.Link obtained at call time".
        int linkKey;
        if (ml == null) {
            linkKey = 0;
        } else {
            linkKey = ml.GetHashCode();
            if (!linkTable.ContainsKey(linkKey))
                linkTable.Add(linkKey, ml);
        }

        Type returnType = delegateType.GetMethod("Invoke").ReturnType;
        ParameterInfo[] pis = delegateType.GetMethod("Invoke").GetParameters();
        Type[] paramTypes = new Type[pis.Length];
        for (int i = 0; i < paramTypes.Length; i++)
            paramTypes[i] = pis[i].ParameterType;

        TypeBuilder typeBuilder = delegateModuleBuilder.DefineType("DummyType" + index++, TypeAttributes.Public);
        //FieldBuilder linkField = typeBuilder.DefineField("link", typeof(IKernelLink), FieldAttributes.Public | FieldAttributes.Static);
        //linkField.SetValue(null, ml);
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("delegateThunk", MethodAttributes.Static | MethodAttributes.Public, returnType, paramTypes);
        ILGenerator ilGenerator = methodBuilder.GetILGenerator();
        ilGenerator.DeclareLocal(typeof(object[]));
        // Now dynamically generate the code for the body of the delegateThunk method. This code builds the correct arg sequence
        // on the stack and then calls CallMathematica() (defined above). Note that to call a method with a params arg, the
        // caller needs to build an array for the args, not just pass them as a sequence. To see where this code comes from, write
        // a method that does the same thing and diassemble it with ildasm.exe.
        ilGenerator.Emit(OpCodes.Ldc_I4, linkKey);
        ilGenerator.Emit(OpCodes.Ldtoken, returnType);
        ilGenerator.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
        ilGenerator.Emit(OpCodes.Ldstr, mFunc);
        ilGenerator.Emit(OpCodes.Ldc_I4, argsToSend);
        // I didn't see in 5 seconds how to load bools, so I will just convert to int.
        ilGenerator.Emit(OpCodes.Ldc_I4, callsUnshare ? 1 : 0);
        ilGenerator.Emit(OpCodes.Ldc_I4, wrapInNETBlock ? 1 : 0);
        ilGenerator.Emit(OpCodes.Ldc_I4, paramTypes.Length);
        ilGenerator.Emit(OpCodes.Newarr, typeof(object));
        ilGenerator.Emit(OpCodes.Stloc_0);
        for (int i = 0; i < paramTypes.Length; i++) {
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ldc_I4, i);
            ilGenerator.Emit(OpCodes.Ldarg, i);
            // Value types need to be boxed, and ByRef types need to be dereferenced.
            if (paramTypes[i].IsValueType) {
                ilGenerator.Emit(OpCodes.Box, paramTypes[i]);
            } else if (paramTypes[i].IsByRef) {
                Type derefType = paramTypes[i].GetElementType();
                ilGenerator.Emit(OpCodes.Ldobj, derefType);
                if (derefType.IsValueType)
                    ilGenerator.Emit(OpCodes.Box, derefType);
            }
            ilGenerator.Emit(OpCodes.Stelem_Ref);
        }
        ilGenerator.Emit(OpCodes.Ldloc_0);
        ilGenerator.Emit(OpCodes.Call, callMathematicaMethod);
        if (returnType == typeof(void)) {
            ilGenerator.Emit(OpCodes.Pop);
        } else if (Utils.IsTrulyPrimitive(returnType) || returnType.IsEnum) {
            // IntPtr is primitive, but needs to be handled via the IsValueType branch below.
            ilGenerator.Emit(OpCodes.Unbox, returnType);
            // The unbox leaves an address on the stack, so we need to "load indirect" from that address.
            ilGenerator.Emit(getLoadInstructionForType(returnType));
        } else if (returnType.IsValueType) {
            ilGenerator.Emit(OpCodes.Unbox, returnType);
            ilGenerator.Emit(OpCodes.Ldobj, returnType);
        }
        ilGenerator.Emit(OpCodes.Ret);

        Type newType = typeBuilder.CreateType();
        return newType.GetMethod("delegateThunk");
    }


    // Works for primitives (except IntPtr) and Enums.
    private static OpCode getLoadInstructionForType(Type t) {

        switch (Type.GetTypeCode(t)) {
            case TypeCode.Boolean:  return OpCodes.Ldind_I1;
            case TypeCode.Byte:     return OpCodes.Ldind_U1;
            case TypeCode.SByte:    return OpCodes.Ldind_I1;
            case TypeCode.Char:     return OpCodes.Ldind_U2;
            case TypeCode.Int16:    return OpCodes.Ldind_I2;
            case TypeCode.UInt16:   return OpCodes.Ldind_U2;
            case TypeCode.Int32:    return OpCodes.Ldind_I4;
            case TypeCode.UInt32:   return OpCodes.Ldind_U4;
            case TypeCode.Int64:    return OpCodes.Ldind_I8;
            case TypeCode.UInt64:   return OpCodes.Ldind_I8;  // Odd, there isn't a U8 opcode....
            case TypeCode.Single:   return OpCodes.Ldind_R4;
            case TypeCode.Double:   return OpCodes.Ldind_R8;
            default: throw new ArgumentException();  // Just for the compiler.
        }
    }


    /***********************************************  defineDelegate  ***********************************************/

    private static int nextIndex = 1; // Used for differentiating delegate names within the assembly if necessary.

    /// <summary>
    /// This method is called by the DefineNETDelegate Mathematica function.
    /// </summary>
    /// <remarks>
    /// Thie method dynamically creates and returns a new delegate Type object. It does not actually create an
    /// instance of the type--that would be done by a later call to the Mathematica function NETNewDelegate, which
    /// calls the createDynamicMethod method above.
    /// <para>
    /// This is a rarely-used method. It is only needed when there is not an existing delegate type for a method signature
    /// you want to use as a callback to Mathematica (refer to the DefineNETDelegate Mathematica function for more information).
    /// </para>
    /// </remarks>
    /// 
    internal static string defineDelegate(string name, string retTypeName, string[] paramTypeNames) {

        Type retType = retTypeName == null ? typeof(void) : TypeLoader.GetType(Utils.addSystemNamespace(retTypeName), true);
        Type[] paramTypes = new Type[paramTypeNames == null ? 0 : paramTypeNames.Length];
        if (paramTypeNames != null) {
            for (int i = 0; i < paramTypes.Length; i++)
                paramTypes[i] = TypeLoader.GetType(Utils.addSystemNamespace(paramTypeNames[i]), true);
        }

        // Check to see if incoming name conflicts with an existing type name. This is easy to get if user reevals a
        // DefineNETDelegate call without changing the type name. We make the change for them.
        Type[] existingTypes = delegateModuleBuilder.GetTypes();
        // Use an ArrayList instead of a plain array so that we can simply the name-checking logic by using the Contains() method.
        System.Collections.ArrayList existingNames = new System.Collections.ArrayList(existingTypes.Length);
        foreach (Type t in existingTypes)
            existingNames.Add(t.Name);
        string uniqueName = name;
        while (existingNames.Contains(uniqueName))
            uniqueName = name + "$" + (nextIndex++);

        TypeBuilder typeBuilder = delegateModuleBuilder.DefineType(delegateNamespace + "." + uniqueName,
                TypeAttributes.Public | TypeAttributes.AutoLayout | TypeAttributes.AnsiClass | TypeAttributes.Sealed, typeof(MulticastDelegate));
        ConstructorBuilder ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig |
            MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, new Type[]{typeof(object), typeof(int)});
        ctorBuilder.SetImplementationFlags(MethodImplAttributes.Managed | MethodImplAttributes.Runtime);
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("Invoke", MethodAttributes.Public | MethodAttributes.Virtual, retType, paramTypes);
        methodBuilder.SetImplementationFlags(MethodImplAttributes.Managed | MethodImplAttributes.Runtime);
        Type newType = typeBuilder.CreateType();
        return newType.FullName;
    }


    [System.Runtime.InteropServices.DllImport("user32.dll")] 
    private static extern int MessageBeep(uint n);  
}

}
