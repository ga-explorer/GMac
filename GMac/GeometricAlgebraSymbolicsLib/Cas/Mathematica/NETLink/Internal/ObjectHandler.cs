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
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;
using Wolfram.NETLink.Internal.COM;


namespace Wolfram.NETLink.Internal {

/// <summary>
/// The class that knows about object references. It stores them, knows how to send them to Mathematica and read them
/// from Mathematica, how to invoke methods and properties, etc. 
/// </summary>
/// <remarks>
/// An IKernelLink implementation will typically hold one of these and delegate all object-related responsibilities to it.
/// </remarks>
/// 
internal class ObjectHandler {

    private InstanceCollection instanceCollection = new InstanceCollection();
    private Hashtable typesTable = new Hashtable();
    private COMDispatchHandler comDispatchHandler = new COMDispatchHandler();

    private object canOnlyMatchOutParam = new object();

    private const int OK                  = 0;
    private const int NAME_NOT_FOUND      = 1;
    private const int ARG_COUNT_INCORRECT = 2;
    private const int ARGS_DONT_MATCH     = 3;

    private const int NOT            = 0;
    private const int ASSIGNABLE    = 1;
    private const int EXACTLY        = 2;


    internal void loadType(IKernelLink ml, Type t) {

        string fullAssemblyQualifiedName = t.AssemblyQualifiedName;
        if (typesTable.ContainsKey(fullAssemblyQualifiedName)) {
            ml.PutFunction("List", 2);
            ml.Put(fullTypeNameForMathematica(t));
            ml.Put(fullAssemblyQualifiedName);
        } else {
            TypeRecord typeRec = new TypeRecord(t);
            ml.PutFunction("List", 9);
            ml.Put(fullTypeNameForMathematica(t));
            ml.Put(fullAssemblyQualifiedName);
            ml.Put(t.Namespace == null ? "" : t.Namespace);
            ml.Put(typeRec.staticFieldNames);
            ml.PutFunction("Thread", 1);
              ml.PutFunction("List", 2);
                ml.Put(typeRec.staticPropertyNames);
                ml.Put(typeRec.isStaticPropertyParameterized);
            ml.Put(typeRec.staticMethodNames);
            ml.Put(typeRec.staticEventNames);
            ml.Put(typeRec.nonPrimitiveFieldOrSimplePropNames);
            ml.Put(typeRec.indexers.Length > 0);
            typesTable.Add(fullAssemblyQualifiedName, typeRec);
        }
    }


    /// <summary>
    /// All calls to ctors, methods, props, etc. go through here. Static or instance-based.
    /// </summary>
    /// 
    internal object call(IKernelLink ml, string assemblyQualifiedName, string objSymbol, int callType, string mmaMemberName, int[] argTypes, out OutParamRecord[] outParams) {

        // This method and everything it calls must throw only CallNETException. Furthermore, it must make every
        // effort to categorize such CallNETExceptions using descriptive constants. The only cases where it is allowed
        // throw a CallNETException that simply wraps another exception is where the exception occurs during invocation
        // of the target member.
        // Link is empty on entry except for the args themselves.

        TypeRecord typeRec = (TypeRecord) typesTable[assemblyQualifiedName];
        if (typeRec == null)
            // Should never happen. Probably a bug in .NET/Link if it does.
            throw new CallNETException(Install.BAD_TYPE, assemblyQualifiedName, null);

        object obj = lookupObject(objSymbol);
        if (obj == null && objSymbol != "Null")
            // An object was specified as the target of the call, but it does not exist in instanceCollection.
            throw new CallNETException(Install.BAD_OBJECT_REFERENCE, null, null);

        int contextMarkPos = mmaMemberName.LastIndexOf('`');
        string memberName = contextMarkPos != -1 ? mmaMemberName.Substring(contextMarkPos + 1) : mmaMemberName;

        // Note that ctors and parameterized properties are routed through method handling because they need the same
        // overload resolution that methods do.
        if (callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_GET || callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_SET) {
            outParams = null;
            return callFieldOrSimpleProperty(ml, callType, typeRec, obj, memberName, argTypes);
        } else {
            return callMethod(ml, callType, typeRec, obj, memberName, argTypes, out outParams);
        }
    }


    internal void releaseInstance(string[] objectSyms) {

        // None of objectSyms can be "Null".
        foreach (string s in objectSyms) {
            ulong key = keyFromMmaSymbol(s);
            instanceCollection.remove(key);
        }
    }


    internal void putReference(IKernelLink ml, object obj, Type upCastCls) {

        bool isCastObject = upCastCls != null;
        Type t = isCastObject ? upCastCls : obj.GetType();

        if (isCastObject && !upCastCls.IsInstanceOfType(obj))
            throw new InvalidCastException();

        // Here is how raw COM objects (objects whose run-time type is System.__ComObject) are
        // treated in putReference():
        // 
        // Raw COM objects only arrive from programmers calling Put() or PutReference().
        // "Installable" calls that return COM objects always wrap them in COMObjectWrapper,
        // which provides type information along with the object.
        //
        // If there is no type information (it is a raw object or the COMObjectWrapper provides
        // no info), we try to determine the default COM interface type for the object. If we find it,
        // we use that for the type in M (NETObject[COMInterface["ICOMName"]]).
        // If no type info at all can be obtained, the object is sent back as a normal object,
        // with type System.__ComObject.
        //
        // The type information in COMObjectWrapper will be either null or a .NET interface type.
        // 
        // If a COM object is being added to instanceCollection (it is being sent to M for the first time),
        // we stuff any type info we have into the object via Marshal.SetComObjectData(). This info is
        // used only when a raw RCW comes into putReference() and we find it is already in
        // instanceCollection. We can then use the cached type info. This means that any time a non-novel
        // untyped COM object is returned to M, it will appear there typed as the type it had the first
        // time it was sent to M.
        //
        // If a COM object is typed as a .NET interface, it will appear in M as NETObject["IntfName"].
        // If it has no COM type info it will appear as NETObject["System.__ComObject"]. If it has COM
        // type info, it will appear as NETObject[COMInterface["IntfName"]]. The object will have the
        // same hash code no matter what guise it is in or what it is cast to. Note, however, that if
        // you cast a COM object to a class type (not an interface) you get a new object with a different
        // hash value. Casting to a class type happens via Marshal.CreateWrapperOfType().
        //
        // The first time a COM object (no matter what guise it is in) is sent to M, it is handled
        // normally there, and added to the current NETBlock. Subsequent times, we send it in such a way
        // that M checks whether the object has been seen yet in this current type, and if not, sets up
        // defs for the new object symbol. It is not added to the NETBlock, however.
        // Instead, it is marked as being an "alias" of an existing object, and its defs will be
        // cleared when the original object is released. Nothing on the .NET side is done differently--there
        // is just one entry in instanceCollection for the object no matter how many times it has been
        // sent to M under different type aliases.

        // IsCOMObject() test below is done first because it is very quick compared to Type.Name, so it
        // short-circuits that expensive call for the more common ordinary objects.
        bool isRawCOMObject = !Utils.IsMono && t.IsCOMObject && t.Name == "__ComObject";
        bool isWrappedCOMObject = obj is COMObjectWrapper;

        // comTypeAlias is only for a COM interface type name, not a managed interface name.
        string comTypeAlias = null;
        // determine COM type info. Use type info in the following priority order: 1) type from COMObjectWrapper,
        // 2) .NET intf type cached in "NETLinkInterface", 3) COM intf type cached in "NetLinkCOMInterface",
        // 4) Default interface from COM ITypeInfo.
        if (isWrappedCOMObject) {
            COMObjectWrapper cow = (COMObjectWrapper) obj;
            // Note that obj and t are reset here to refer to the wrapped object.
            obj = cow.wrappedObject;
            if (cow.type != null) {
                // The cached type is a .NET interface.
                Marshal.SetComObjectData(obj, "NETLinkInterface", cow.type);
                t = cow.type;
                isCastObject = true;
            } else {
                t = obj.GetType(); // Will always be System.__ComObject.
            }
        }
        if (isRawCOMObject || isWrappedCOMObject && t.Name == "__ComObject") {
            Type cachedInterfaceType = (Type) Marshal.GetComObjectData(obj, "NETLinkInterface");
            if (cachedInterfaceType != null) {
                t = cachedInterfaceType;
                isCastObject = true;
            } else {
                comTypeAlias = COMUtilities.GetDefaultCOMInterfaceName(obj);
            }
        }

        // If the incoming object is the Pointer type, create an IntPtr for it. The Pointer class is pretty useless--it
        // appears to exist only as a necessary object representation for a pointer.
        if (obj is Pointer) {
            unsafe { obj = new IntPtr(Pointer.Unbox(obj)); }
            t = typeof(IntPtr);
        }

        // typeAliasName will be "" for unaliased object, or a FullName of a .NET type (for a cast object,
        // including COM objects cast to a .NET interface type), or a COM interface name.
        string typeAliasName = isCastObject ? fullTypeNameForMathematica(t) : (comTypeAlias != null ? comTypeAlias : "");

        ulong key = instanceCollection.keyOf(obj);
        bool isNewObject = key == 0;
        bool isNewAlias;
        if (isNewObject) {
            key = instanceCollection.put(obj, typeAliasName);
            isNewAlias = false; // Value will not be used anyway.
        } else {            
            isNewAlias = instanceCollection.addAlias(key, typeAliasName);
        }
        string objectSymbol = mmaSymbolFromKey(key, typeAliasName);

        if (isNewObject || isNewAlias) {
            // Object wasn't there; put it in, and set key to be the actual key, returned by put.
            // Object is already in instanceCollection, but this is a new type alias for the object.
            string aqTypeName = t.AssemblyQualifiedName;
            string typeName = fullTypeNameForMathematica(t);
            bool typeMustBeLoaded = typesTable[aqTypeName] == null;
            int numArgs = isRawCOMObject || isWrappedCOMObject ? 7 : 5;
            ml.PutFunction(Install.MMA_CREATEINSTANCEDEFS, numArgs);
            ml.Put(typeName);
            ml.Put(aqTypeName);
            ml.PutSymbol(objectSymbol);
            ml.Put(typeMustBeLoaded);
            ml.Put(!isNewObject);  // This arg tells M "this object is an alias to an existing one."
            if (isRawCOMObject || isWrappedCOMObject) {
                ml.Put(true);
                ml.Put(comTypeAlias == null ? "" : comTypeAlias);
            }
        } else {
            ml.PutSymbol(objectSymbol);
        }
    }


    internal object lookupObject(string objSymbol) {

        object obj = null;
        if (objSymbol != "Null") {
            ulong key = keyFromMmaSymbol(objSymbol);
            obj = instanceCollection.get(key);
        }
        return obj;
    }


    internal void peekObjects(IKernelLink ml) {

        ml.PutFunction("List", instanceCollection.size());
        foreach (string objectSymbol in instanceCollection) {
            ml.PutSymbol(objectSymbol);
        }
    }


    internal void peekTypes(IKernelLink ml) {

        ml.PutFunction("List", typesTable.Count);
        foreach (DictionaryEntry de in typesTable) {
            ml.PutFunction("List", 2);
            ml.Put(((TypeRecord) de.Value).fullName);
            ml.Put(((TypeRecord) de.Value).type.AssemblyQualifiedName);
        }
    }

    internal void peekAssemblies(IKernelLink ml) {

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        ml.PutFunction("List", assemblies.Length);
        foreach (Assembly asm in assemblies) {
            ml.PutFunction("List", 2);
            ml.Put(asm.GetName().Name);
            ml.Put(asm.FullName);
        }
    }


    // Use in place of Type.FullName to get a string suitable for use in Mathematica. It only exists for
    // the needs of generic types. It handles "unrealized" generic paramters (like "TOutput") that have a 
    // FullName of null, and also realized generic params, which in the FullName of a type come in as
    // assembly-qualified names, which we don't want. Thus we manually build the type string in this case.
    //
    internal static string fullTypeNameForMathematica(Type t) {
        String fullName = t.FullName;
        // For types that are parameters of generic types, t.FullName is null, so we have to resort to using ToString().
        if (fullName == null)
            fullName = t.ToString();
        // Generic types have args that are given as assembly-qualified names, and we want to strip that out.
        // For example:
        //       System.Collections.Generic.List`1[[System.Int32, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //    should become
        //       System.Collections.Generic.List`1[System.Int32]
        // Unrealized generic types (like those returned from NETTypeInfo on an assembly), have a FullName
        // without brackets: System.Collections.Generic.List`1.
        bool isGeneric = t.IsGenericType;
        if (isGeneric) {
            Type[] genTypes = t.GetGenericArguments();
            int bracketPos = fullName.IndexOf("[");
            if (bracketPos >= 0)
                fullName = fullName.Substring(0, bracketPos + 1);
            else
                fullName += "[";
            for (int i = 0; i < genTypes.Length; i++) {
                fullName += fullTypeNameForMathematica(genTypes[i]);
                if (i < genTypes.Length - 1)
                    fullName += ",";
            }
            fullName += "]";
        }
        return fullName;
    }


    // The following methods are used by KernelLinkImpl to return type information for the NETTypeInfo[] function.
    // The typeRec == null case should never occur in these. Reflection.m code should ensure that LoadNETType has been called.

    internal Type getType(string aqTypeName) {

        TypeRecord typeRec = (TypeRecord) typesTable[aqTypeName];
        if (typeRec == null)
            throw new CallNETException(Install.BAD_TYPE, aqTypeName, null);
        return typeRec.type;
    }

    internal ConstructorInfo[] getConstructors(string aqTypeName) {

        TypeRecord typeRec = (TypeRecord) typesTable[aqTypeName];
        if (typeRec == null)
            throw new CallNETException(Install.BAD_TYPE, aqTypeName, null);
        return typeRec.constructors;
    }

    internal FieldInfo[] getFields(string aqTypeName) {

        TypeRecord typeRec = (TypeRecord) typesTable[aqTypeName];
        if (typeRec == null)
            throw new CallNETException(Install.BAD_TYPE, aqTypeName, null);
        return typeRec.fields;
    }

    internal PropertyInfo[] getProperties(string aqTypeName) {

        TypeRecord typeRec = (TypeRecord) typesTable[aqTypeName];
        if (typeRec == null)
            throw new CallNETException(Install.BAD_TYPE, aqTypeName, null);
        return typeRec.properties;
    }

    internal MethodInfo[] getMethods(string aqTypeName) {

        TypeRecord typeRec = (TypeRecord) typesTable[aqTypeName];
        if (typeRec == null)
            throw new CallNETException(Install.BAD_TYPE, aqTypeName, null);
        return typeRec.methods;
    }

    internal EventInfo[] getEvents(string aqTypeName) {

        TypeRecord typeRec = (TypeRecord) typesTable[aqTypeName];
        if (typeRec == null)
            throw new CallNETException(Install.BAD_TYPE, aqTypeName, null);
        return typeRec.events;
    }


    /**************************************  Private  ******************************************/

    private ArrayList matchingMethods = new ArrayList(32);
    private ArrayList methodRecs = new ArrayList(32);

    private object callMethod(IKernelLink ml, int callType, TypeRecord t, object obj, string memberName, int[] argTypes, out OutParamRecord[] outParams) {

        bool isCtor = callType == 0;
        // The passed-in name will be "" if this was a call made using indexer syntax: obj[params].
        bool isIndexer = memberName == "" && !isCtor;

        // Get this one case out of the way first. If we are constructing a value type (r.g., primitive or struct)
        // and calling the no-arg ctor, this ctor does not show up in the array returned by GetConstructors().
        // You cannot invoke it via reflection in that way. Instead you need to use Activator.CreateInstance().
        if (isCtor && argTypes.Length == 0 && t.type.IsValueType) {
            outParams = null;
            return Activator.CreateInstance(t.type);
        }

        if (isIndexer) {
            if (t.indexerName != null)
                memberName = t.indexerName;
            else if (obj != null && !Utils.IsMono && Marshal.IsComObject(obj) && (t.name == "__ComObject" || t.type.IsInterface))
                // Use the standard name for indexing COM collections.
                memberName = "Item";
            else
                throw new CallNETException(Install.NO_INDEXER, t.fullName, null);
        }

        string targetMethodName = memberName;
        // Note that parameterized properties are routed through method handling via their "set_" and "get_"
        // method accessors. This creates a requirement that the accessors conform to the standard .NET naming convention.
        // Simple properties are called directly, not via get_ and set_ methods, so they do not need to have
        // accessor methods with the standard names.
        if (callType == Install.CALLTYPE_PARAM_PROP_GET) {
            // Static parameterized prop get or instance default param prop (indexer) get.
            targetMethodName = "get_" + memberName;
        } else if (callType == Install.CALLTYPE_PARAM_PROP_SET) {
            // Parameterized prop sets including indexers.
            targetMethodName = "set_" + memberName;
        }
        MethodBase[] methods = isCtor ? (MethodBase[]) t.constructors : (MethodBase[]) t.methods;

        bool isParameterizedProperty = callType == Install.CALLTYPE_PARAM_PROP_GET || callType == Install.CALLTYPE_PARAM_PROP_SET;
        matchingMethods.Clear();
        // This fills the matchingMethods arrayList.
        findMethodMatches(targetMethodName, isCtor, argTypes, methods, t.methodNames, matchingMethods, ml.ComplexType, out var err);
        if (callType == Install.CALLTYPE_METHOD && err == NAME_NOT_FOUND) {
            // If we did not find the name among the methods, this may be a call to a non-static parameterized property. These
            // masquerade as CALLTYPE_METHOD. Call it via its "get_" accessor method.
            findMethodMatches("get_" + memberName, isCtor, argTypes, methods, t.methodNames, matchingMethods, ml.ComplexType, out err);
            isParameterizedProperty = true;
        }
        if (err == NAME_NOT_FOUND && obj != null && !Utils.IsMono && Marshal.IsComObject(obj) && (t.name == "__ComObject" || t.type.IsInterface)) {
            // If the name was not found, but we have a COM object, we need to do things differently. The test above tells
            // us that we have a COM object that is either (1) typed as a .NET interface (not a class) or (2) is a raw __ComObject.
            // In either case, we might be calling a method that is valid but does not show up in the list of members 
            // for the type that are known to .NET. If it is a raw __ComObject, none of the COM methods called via IDispatch
            // will show up, and if it is typed as an interface, the base Object methods (e.g., ToString()) will not show
            // up (in addition to COM methods valid for the object but not part of the specified managed interface). We will
            // pass the work off to COMObjectHandler. 
            return comDispatchHandler.callDispatch(ml, obj, memberName, callType, argTypes, out outParams);
        } else if (err != OK) {
            string errTag = ""; // Just for the compiler.
            if (err == ARG_COUNT_INCORRECT) {
                if (isCtor)
                    errTag = Install.CTOR_ARG_COUNT;
                else if (isIndexer)
                    errTag = Install.INDEXER_ARG_COUNT;
                else if (isParameterizedProperty)
                    errTag = Install.PARAM_PROP_ARG_COUNT;
                else
                    errTag = Install.METHOD_ARG_COUNT;
            } else if (err == ARGS_DONT_MATCH) {
                if (isCtor)
                    errTag = Install.CTOR_BAD_ARGS;
                else if (isIndexer)
                    errTag = Install.INDEXER_BAD_ARGS;
                else if (isParameterizedProperty)
                    errTag = Install.PARAM_PROP_BAD_ARGS;
                else
                    errTag = Install.METHOD_BAD_ARGS;
            } else {
                // err is NAME_NOT_FOUND
                if (isCtor)
                    errTag = Install.NO_CTOR;
                else if (isIndexer)
                    errTag = callType == Install.CALLTYPE_PARAM_PROP_GET ? Install.INDEXER_CANNOT_BE_READ : Install.INDEXER_CANNOT_BE_SET;
                else
                    errTag = Install.METHOD_NOT_FOUND;
            }
            throw new CallNETException(errTag, t.fullName, memberName);
        }

        MethodBase mi = null;
        object[] args;
        try {
            if (matchingMethods.Count == 1) {
                mi = (MethodBase) matchingMethods[0];
                ParameterInfo[] parms = mi.GetParameters();
                args = new object[argTypes.Length];
                int i;
                for (i = 0; i < args.Length; i++) {
                    if (Utils.IsOutOnlyParam(parms[i])) {
                        Utils.discardNext(ml);
                        args[i] = null;
                    } else {
                        args[i] = Utils.readArgAs(ml, argTypes[i], parms[i].ParameterType);
                    }
                }
            } else {
                mi = pickBestMethod(ml, matchingMethods, argTypes, out args);
            }
        } catch (Exception) {
            // Typically ArgumentException thrown by Utils.readArgAs() when the expr on the link cannot be read as the
            // necessary type, but could also be MathLinkException.
            // Here we pick the best member name to report in the exception. If is is a prop, go back and use the passed-in name
            // instead of get_XXX or set_XXX. The passed-in memberName might be mangled with U for
            // _, so if we know the actual member name we are trying to call, use it instead.
            string nameToReport = isParameterizedProperty ? memberName : (mi != null ? mi.Name : memberName);
            throw new CallNETException(isCtor ? Install.CTOR_BAD_ARGS : Install.METHOD_BAD_ARGS, t.fullName, mi != null ? mi.Name : memberName);
        }

        // Have now decided on which method to call (held by the variable mi).
        try {
            ParameterInfo[] parms = mi.GetParameters();

            // May need to pad out the args array with Missing's for trailing optional args.
            if (args.Length < parms.Length) {
                object[] newArgs = new object[parms.Length];
                args.CopyTo(newArgs, 0);
                for (int i = args.Length; i < parms.Length; i++) {
                    newArgs[i] = Missing.Value;
                }
                args = newArgs;
            }

            // Two final tweaks: First, because we ignore the incoming values for out params, we may have read a primitive
            // param as a narrower type than the widest out param at that slot. This will cause an error, even though the
            // value passed in to an out param is ignored. The easiest thing to do is just put in null for all out params.
            // Second, we need to accommodate vectors and single values paassed to pointer param slots. We use a single
            // instance of PointerArgumentManager to handle all pointer params for the call.
            PointerArgumentManager pointerArgsManager = null; 
            for (int i = 0; i < args.Length; i++) {
                ParameterInfo pi = parms[i];
                if (Utils.IsOutOnlyParam(pi)) {
                    args[i] = null;
                } else if (pi.ParameterType.IsPointer && Utils.IsTrulyPrimitive(pi.ParameterType.GetElementType()) &&
                             (Utils.IsTrulyPrimitive(args[i].GetType()) || args[i].GetType().IsArray)) {
                    if (pointerArgsManager == null)
                        pointerArgsManager = new PointerArgumentManager(args.Length);
                    args[i] = pointerArgsManager.add(i, args[i], pi);
                }
            }

            // Make the call!
            object result;
            if (pointerArgsManager == null) {
                result = isCtor ? ((ConstructorInfo) mi).Invoke(args) : mi.Invoke(obj, args);
            } else {
                // Don't pay the cost for the try/finally block unless we are using a PointerArgumentManager.
                try {
                    result = isCtor ? ((ConstructorInfo) mi).Invoke(args) : mi.Invoke(obj, args);
                } finally {
                    pointerArgsManager.release();
                }
            }

            // This assert is very temporary. This is to support my understanding that it is impossible to get a __ComObject
            // from a method typed to return a class type (must be an interface type). Note also that to be precise I should also
            // check against MarshalByRefObject like I check for Object, as these are the classes that __ComObject derives from.
            Debug.Assert(isCtor || result == null || (!Utils.IsMono && !Marshal.IsComObject(result)) ||
                            result.GetType().Name != "__ComObject" || ((MethodInfo) mi).ReturnType == typeof(object) ||
                                ((MethodInfo) mi).ReturnType.IsInterface);

            if (!isCtor && result != null && !Utils.IsMono && Marshal.IsComObject(result) &&
                    result.GetType().Name == "__ComObject" && ((MethodInfo) mi).ReturnType.IsInterface) {
                result = new COMObjectWrapper(result, ((MethodInfo) mi).ReturnType);
            }

            // Now handle out/ref params if there were any.
            outParams = null;
            for (int i = 0; i < parms.Length; i++) {
                // IsByRef is true for ref and out params.
                bool wasByRef = parms[i].ParameterType.IsByRef;
                bool wasPointer = pointerArgsManager != null && pointerArgsManager.wasPointerArg(i);
                if (wasByRef || wasPointer) {
                    if (outParams == null)
                        outParams = new OutParamRecord[parms.Length];
                    object arg = wasPointer ? pointerArgsManager.getValue(i) : args[i];
                    if (arg != null && !Utils.IsMono && Marshal.IsComObject(arg) && arg.GetType().Name == "__ComObject") {
                        Type paramType = GetParamType(mi.GetParameters()[i]);
                        if (paramType.IsInterface)
                            arg = new COMObjectWrapper(arg, paramType);
                    }
                    outParams[i] = new OutParamRecord(i, arg);
                }
            }
            return result;
         } catch (Exception invokeException) {
            // Wrap up any TargetInvocationException or other thrown during Invoke.
            throw new CallNETException(invokeException, mi.Name);
        }
   }


    private object callFieldOrSimpleProperty(IKernelLink ml, int callType, TypeRecord t, object obj, string memberName, int[] argTypes) {

        bool isGet = callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_GET;

        // Parameterized properties are handled via their set_ and get_ accessor methods, not routed through here.
        // Check that no param args are supplied.
        Debug.Assert(isGet ? argTypes.Length == 0 : argTypes.Length == 1);

        object result;

        // Try properties first.
        PropertyInfo pi = null;
        for (int propIndex = 0; propIndex < t.propertyNames.Length; propIndex++) {
            if (Utils.memberNamesMatch(t.propertyNames[propIndex], memberName)) {
                pi = t.properties[propIndex];
                break;
            }
        }
        if (pi != null) {
            // Is a property call.
            if (isGet) {
                if (pi.CanRead)
                    try {
                        result = pi.GetValue(obj, null);
                        if (result != null && !Utils.IsMono && Marshal.IsComObject(result) &&
                                result.GetType().Name == "__ComObject" && pi.PropertyType.IsInterface) {
                            // The only way IsInterface would be false if the type was __ComObject is if the property type
                            // was Object or MarshalByRefObject--the classes above __ComObject in its inheritance tree.
                            // It's just quicker to check IsInterface.
                            result = new COMObjectWrapper(result, pi.PropertyType);
                        }
                    } catch (Exception invokeException) {
                        throw new CallNETException(invokeException, pi.Name);
                    }
                else
                    throw new CallNETException(Install.PROP_CANNOT_BE_READ, t.fullName, pi.Name);
            } else {
                // Is a set call.
                if (pi.CanWrite) {
                    object propValue;
                    try {
                        propValue = Utils.readArgAs(ml, argTypes[0], pi.PropertyType);
                    } catch (Exception) {
                        throw new CallNETException(Install.PROP_BAD_ARGS, t.fullName, pi.Name);
                    }
                    try {
                        pi.SetValue(obj, propValue, null);
                    } catch (Exception invokeException) {
                        throw new CallNETException(invokeException, pi.Name);
                    }
                } else {
                    throw new CallNETException(Install.PROP_CANNOT_BE_SET, t.fullName, pi.Name);
                }
                result = null;
            }
        } else {
            // Name is not a valid property name; try fields.
            FieldInfo fi = null;
            for (int fieldIndex = 0; fieldIndex < t.fieldNames.Length; fieldIndex++) {
                if (Utils.memberNamesMatch(t.fieldNames[fieldIndex], memberName)) {
                    fi = t.fields[fieldIndex];
                    break;
                }
            }
            if (fi == null) {
                // Member name not found among fields or properties. Check if it is an event and throw an exception
                // it it is, to warn the user that it is not legal to access events like they were fields.
                for (int eventIndex = 0; eventIndex < t.eventNames.Length; eventIndex++) {
                    if (Utils.memberNamesMatch(t.eventNames[eventIndex], memberName))
                        throw new CallNETException(Install.IS_EVENT, t.fullName, memberName);
                }
                // If we get here, name was not a prop, field, or event. Pass the call to IDispatch if it is a COM object.
                if (obj != null && !Utils.IsMono && Marshal.IsComObject(obj) && (t.name == "__ComObject" || t.type.IsInterface)) {
                    // We have a COM object that is either (1) typed as a .NET interface (not a class) or (2) is a raw __ComObject.
                    return comDispatchHandler.callDispatch(ml, obj, memberName, callType, argTypes, out var unusedOutParams);
                } else {
                    throw new CallNETException(Install.FIELD_NOT_FOUND, t.fullName, memberName);
                }
            }
            if (isGet) {
                try {
                    result = fi.GetValue(obj);
                    if (result != null && !Utils.IsMono && Marshal.IsComObject(result) &&
                            result.GetType().Name == "__ComObject" && fi.FieldType.IsInterface) {
                        result = new COMObjectWrapper(result, fi.FieldType);
                    }
                } catch (Exception invokeException) {
                    throw new CallNETException(invokeException, fi.Name);
                }
            } else if (fi.IsInitOnly || fi.IsLiteral) {
                throw new CallNETException(Install.FIELD_CANNOT_BE_SET, t.fullName, fi.Name);
            } else {
                // Setting field.
                object fieldValue;
                try {
                    fieldValue = Utils.readArgAs(ml, argTypes[0], fi.FieldType);
                } catch (Exception) {
                    throw new CallNETException(Install.FIELD_BAD_ARGS, t.fullName, fi.Name);
                }
                try {
                    fi.SetValue(obj, fieldValue);
                } catch (Exception invokeException) {
                    throw new CallNETException(invokeException, fi.Name);
                }
                result = null;
            }
        }
        return result;
    }


    private unsafe MethodBase pickBestMethod(IKernelLink ml, IList matchingMethods, int[] argTypes, out object[] args) {

        args = new object[argTypes.Length];
        for (int i = 0; i < args.Length; i++)
            args[i] = canOnlyMatchOutParam;

        methodRecs.Clear();
        foreach (MethodBase m in matchingMethods)
            methodRecs.Add(new MethodRec(m, m.GetParameters()));

        // Logic: If arg on link is:
        //    INTEGER    read as widest int or real (if no ints) among methods
        //    REAL       read as widest real among methods
        //    STRING     read as string
        //    BOOLEAN    read as bool
        //    OBJECTREF  read as object. Rule out methods based on type if possible.
        //    SYMBOL     read as Expr. Reject any method that does not have an Expr at that slot.

        for (int i = 0; i < argTypes.Length; i++) {

            int argType = argTypes[i];

            if (methodRecs.Count == 1) {
                ParameterInfo pi = ((MethodRec) methodRecs[0]).pia[i];
                if (Utils.IsOutOnlyParam(pi)) {
                    Utils.discardNext(ml);
                    args[i] = null;
                } else {
                    args[i] = Utils.readArgAs(ml, argType, pi.ParameterType);
                }
                continue;
            }

            bool atLeastOneExprAtThisSlot = false;
            bool atLeastOneExprArrayAtThisSlot = false;
            bool allMethodsAgreeAtThisSlot = true;
            bool allOutParamsAtThisSlot = true;
            Type firstMethodNonOutType = null;
            foreach (MethodRec mr in methodRecs) {
                ParameterInfo pi = mr.pia[i];
                Type t = pi.ParameterType;
                if (!Utils.IsOutOnlyParam(pi)) {
                    allOutParamsAtThisSlot = false;
                    if (firstMethodNonOutType == null)
                       firstMethodNonOutType = t;
                    else if (firstMethodNonOutType != t)
                        allMethodsAgreeAtThisSlot = false;
                }
                if (t == typeof(Expr))
                    atLeastOneExprAtThisSlot = true;
                else if (t == typeof(Expr[]))
                    atLeastOneExprArrayAtThisSlot = true;
            }
            if (allOutParamsAtThisSlot) {
                Utils.discardNext(ml);
                args[i] = null;
                continue;
            }
            if (allMethodsAgreeAtThisSlot) {
                args[i] = Utils.readArgAs(ml, argType, firstMethodNonOutType);
                continue;
            }
            // Note that an Expr at this slot in any matching signature causes the value to be read as
            // an Expr. This is because in some sense an Expr is the "widest" possible type. It could be
            // converted down to any native type later. Currently this is not done, however, and we
            // actually cull out all methods that didn't have an Expr at this slot. We could in the future
            // remove this culling and add code that allows "narrowing" Exprs afterwards.
            if (atLeastOneExprAtThisSlot) {
                args[i] = Utils.readArgAs(ml, argType, typeof(Expr));
                for (int j = methodRecs.Count - 1; j >= 0; j--) {
                    MethodRec mr = (MethodRec) methodRecs[j];
                    ParameterInfo pi = mr.pia[i];
                    if (pi.ParameterType != typeof(Expr) && !Utils.IsOutOnlyParam(pi))
                        methodRecs.RemoveAt(j); 
                }
                continue;
            }
            if (atLeastOneExprArrayAtThisSlot && (argType == Install.ARGTYPE_VECTOR || argType == Install.ARGTYPE_MATRIX ||
                    argType == Install.ARGTYPE_TENSOR3 || argType == Install.ARGTYPE_LIST)) {
                args[i] = Utils.readArgAs(ml, argType, typeof(Expr[]));
                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(Expr));
                continue;
            }

            switch (argType) {
                case Install.ARGTYPE_INTEGER: {
                    // From tests in possibleMatch(), we know all params at this slot must be numeric or object.
                    // Could also be an enum.
                    bool needsDecimal = false;
                    foreach (MethodRec mr in methodRecs) {
                        ParameterInfo pi = mr.pia[i];
                        Type paramType = GetParamType(pi);
                        if (!Utils.IsOutOnlyParam(pi) && (paramType == typeof(long) || paramType == typeof(ulong) || paramType == typeof(decimal))) {
                            needsDecimal = true;
                            break;
                        }
                    }
                    if (needsDecimal) {
                        args[i] = Utils.readArgAs(ml, argType, typeof(decimal));
                    } else {
                        args[i] = Utils.readArgAs(ml, argType, typeof(int));
                    }
                    break;
                }
                case Install.ARGTYPE_REAL: {
                    // From tests in possibleMatch(), we know all params at this slot must be real or object.
                    bool needsDecimal = false;
                    foreach (MethodRec mr in methodRecs) {
                        ParameterInfo pi = mr.pia[i];
                        Type paramType = GetParamType(pi);
                        if (!Utils.IsOutOnlyParam(pi) && paramType == typeof(decimal)) {
                            needsDecimal = true;
                            break;
                        }
                    }
                    if (needsDecimal) {
                        args[i] = Utils.readArgAs(ml, argType, typeof(decimal));
                    } else {
                        args[i] = Utils.readArgAs(ml, argType, typeof(double));
                    }
                    break;
                }
                case Install.ARGTYPE_STRING: {
                    args[i] = Utils.readArgAs(ml, argType, typeof(string));
                    break;
                }
                case Install.ARGTYPE_BOOLEAN: {
                    args[i] = Utils.readArgAs(ml, argType, typeof(bool));
                    break;
                }
                case Install.ARGTYPE_OBJECTREF: {
                    object obj = Utils.readArgAs(ml, argType, typeof(object));
                    Type objType = obj.GetType();
                    // Cull out all methods that do not have a compatible object param at this slot.
                    for (int j = methodRecs.Count - 1; j >= 0; j--) {
                        MethodRec mr = (MethodRec) methodRecs[j];
                        ParameterInfo pi = mr.pia[i];
                        if (Utils.IsOutOnlyParam(pi))
                            continue;
                        // isWideningConversion tells us if objType is assignment-compatible with parameter type. It is equivalent
                        // to Type.IsAssignableFrom for objects, and for primitives it tells whether parameter type is wider
                        // than objType. Lets pointers through, because the Pointer class has no accessible type information.
                        if (!isWideningConversion(objType, pi))
                            methodRecs.RemoveAt(j);
                    }
                    args[i] = obj;
                    break;
                }
                case Install.ARGTYPE_NULL: {
                    ml.GetSymbol();
                    args[i] = null;
                    break;
                }
                case Install.ARGTYPE_MISSING: {
                    ml.GetSymbol();
                    args[i] = System.Reflection.Missing.Value;
                    // Cull out all methods that do not have an optional param at this slot.
                    for (int j = methodRecs.Count - 1; j >= 0; j--) {
                        MethodRec mr = (MethodRec) methodRecs[j];
                        if (!mr.pia[i].IsOptional)
                            methodRecs.RemoveAt(j);
                    }
                    break;
                }
                case Install.ARGTYPE_COMPLEX: {
                    // We won't have passed findMethodMatches() if complex class was null.
                    Debug.Assert(ml.ComplexType != null);
                    args[i] = Utils.readArgAs(ml, argType, ml.ComplexType);
                    break;
                }
                case Install.ARGTYPE_VECTOR: {
                    // We know by earlier tests that all params at this slot must be 1-D arrays or just Array.
                    // Look at first leaf value and read the array as that type. But first check if the param is an
                    // Expr[].
                    bool wasReadAsExprArray = false;
                    Type paramType = null;
                    foreach (MethodRec mr in methodRecs) {
                        ParameterInfo pi = mr.pia[i];
                        paramType = GetParamType(pi);
                        if (!Utils.IsOutOnlyParam(pi) && paramType == typeof(Expr[])) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Expr[]));
                            wasReadAsExprArray = true;
                            break;
                        }
                    }
                    // Cull out all methods that did not have an Expr[] at this slot.
                    if (wasReadAsExprArray) {
                        cullOutIncompatibleArrayMethods(methodRecs, i, typeof(Expr));
                        break;  // On to next arg.
                    }
                    // Now check the leaf type.
                    ExpressionType leafExprType;
                    leafExprType = getLeafExprType(ml, out var leafObjectType);
                    if (leafExprType == ExpressionType.String) {
                        args[i] = Utils.readArgAs(ml, argType, typeof(string[]));
                        cullOutIncompatibleArrayMethods(methodRecs, i, typeof(string));
                    } else if (leafExprType == ExpressionType.Object) {
                        // leafObjectType could be null here, if all the elements of the incoming array are Null.
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            paramType = GetParamType(pi);
                            if (Utils.IsOutOnlyParam(pi))
                                continue;
                            // The culling that occurrred in findMethodMatches() is what supports this Assert.
                            Debug.Assert(paramType.IsArray || paramType == typeof(Array),
                                    "Unexpected type in pickBestMethod(): " + paramType + ". Should be an array or Array"); 
                            if (paramType.IsArray) {
                                Type elementType = paramType.GetElementType();
                                if (leafObjectType == null || elementType.IsAssignableFrom(leafObjectType)) {
                                    args[i] = Utils.readArgAs(ml, argType, paramType);
                                    cullOutIncompatibleArrayMethods(methodRecs, i, elementType);
                                    break;
                                }
                            } else {
                                // paramType == typeof(Array)
                                args[i] = Utils.readArgAs(ml, argType, paramType);
                                break;
                            }
                        }
                        // OK to leave here without assiging to args[i], which we can detect by noting that it still has the value it
                        // was initialized to. This will happen if the incoming objects did not match the types of any methods.
                        // The method finally chosen might have an out parameter at this slot. But we need to make sure we drain the link.
                        if (args[i] == canOnlyMatchOutParam)
                            Utils.discardNext(ml);
                    } else if (leafExprType == ExpressionType.Boolean) {
                        args[i] = Utils.readArgAs(ml, argType, typeof(bool[]));
                        cullOutIncompatibleArrayMethods(methodRecs, i, typeof(bool));
                    } else if (leafExprType == ExpressionType.Real) {
                        bool needsDouble = false, needsDecimal = false;
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            paramType = GetParamType(pi);
                            if (Utils.IsOutOnlyParam(pi))
                                continue;
                            if (paramType == typeof(double[]) || paramType == typeof(double*))
                                needsDouble = true;
                            else if (paramType == typeof(decimal[]) || paramType == typeof(decimal*))
                                needsDecimal = true;
                        }
                        if (needsDecimal) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(decimal[]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(decimal));
                        } else if (needsDouble) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(double[]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(double));
                        } else {
                            // Note that for an Array slot, an array of reals will be read as float.
                            args[i] = Utils.readArgAs(ml, argType, typeof(float[]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(float));
                        }
                    } else if (leafExprType == ExpressionType.Integer) {
                        bool hasByte = false, hasSByte = false, hasChar = false, hasShort = false, hasUShort = false,
                                hasInt = false, hasUInt = false, hasLong = false, hasULong = false, hasDecimal = false,
                                    hasFloat = false, hasDouble = false;
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            paramType = GetParamType(pi);
                            // Of course we don't count out params, and also don't allow int list to be passed for an Enum[] slot.
                            if (Utils.IsOutOnlyParam(pi) || paramType.IsEnum)
                                continue;
                            Type elementType = paramType.IsArray /*|| paramType.IsPointer*/ ? paramType.GetElementType() : null;
                            switch (Type.GetTypeCode(elementType)) {
                                case TypeCode.Int32: hasInt = true; break;
                                case TypeCode.Double: hasDouble = true; break;
                                case TypeCode.Byte: hasByte = true; break;
                                case TypeCode.SByte: hasSByte = true; break;
                                case TypeCode.Char: hasChar = true; break;
                                case TypeCode.Int16: hasShort = true; break;
                                case TypeCode.UInt16: hasUShort = true; break;
                                case TypeCode.UInt32: hasUInt = true; break;
                                case TypeCode.Int64: hasLong = true; break;
                                case TypeCode.UInt64: hasULong = true; break;
                                case TypeCode.Decimal: hasDecimal = true; break;
                                case TypeCode.Single: hasFloat = true; break;
                                default: break; // Including when elementype == null because paramType == Array.
                            }
                        }
                        Type widestType = null;
                        if (hasDecimal)
                            widestType = typeof(decimal[]);
                        else if (hasLong)
                            widestType = typeof(long[]);
                        else if (hasULong)
                            widestType = typeof(ulong[]);
                        else if (hasInt)
                            widestType = typeof(int[]);
                        else if (hasUInt)
                            widestType = typeof(uint[]);
                        else if (hasShort)
                            widestType = typeof(short[]);
                        else if (hasUShort)
                            widestType = typeof(ushort[]);
                        else if (hasChar)
                            widestType = typeof(char[]);
                        else if (hasByte)
                            widestType = typeof(byte[]);
                        else if (hasSByte)
                            widestType = typeof(sbyte[]);
                        else if (hasDouble)
                            widestType = typeof(double[]);
                        else if (hasFloat)
                            widestType = typeof(float[]);
                        else
                            widestType = typeof(int[]);  // Array and enum params fall through to here. Read as int[].
                        args[i] = Utils.readArgAs(ml, argType, widestType);
                        cullOutIncompatibleArrayMethods(methodRecs, i, widestType.GetElementType());
                    } else if (leafExprType == ExpressionType.Complex) {
                        if (ml.ComplexType == null)
                            throw new MathLinkException(MathLinkException.MLE_NO_COMPLEX);
                        args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(ml.ComplexType, 0).GetType());
                        cullOutIncompatibleArrayMethods(methodRecs, i, ml.ComplexType);
                    } else {
                        // Leaf is Symbol or Function.
                        throw new ArgumentException();
                    }
                    break;
                }
                case Install.ARGTYPE_MATRIX: {
                    // We know by earlier tests that all params at this slot must be 2-D arrays or just Array.
                    // Look at first leaf value and read the array as that type. But first check if the param is an
                    // Expr[,] or Expr[][]. For all types, favor [,] over [][].
                    bool wasReadAsExprArray = false;
                    bool wasReadAsJaggedExprArray = false;
                    Type paramType = null;
                    foreach (MethodRec mr in methodRecs) {
                        ParameterInfo pi = mr.pia[i];
                        if (Utils.IsOutOnlyParam(pi))
                            continue;
                        paramType = GetParamType(pi);
                        if (paramType == typeof(Expr[,])) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Expr[,]));
                            wasReadAsExprArray = true;
                            break;
                        } else if (paramType == typeof(Expr[][])) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Expr[][]));
                            wasReadAsJaggedExprArray = true;
                            break;
                        }
                    }
                    // Cull out all methods that did not have an Expr[,] at this slot.
                    if (wasReadAsExprArray) {
                        cullOutIncompatibleArrayMethods(methodRecs, i, typeof(Expr));
                        break;  // On to next arg.
                    } else if (wasReadAsJaggedExprArray) {
                        cullOutIncompatibleArrayMethods(methodRecs, i, typeof(Expr[]));
                        break;  // On to next arg.
                    }
                    // Now check the leaf type.
                    ExpressionType leafExprType;
                    leafExprType = getLeafExprType(ml, out var leafObjectType);

                    bool hasMultiDimensionalArray = false;
                    bool hasJaggedArray = false;
                    Type elementType;

                    foreach (MethodRec mr in methodRecs) {
                        ParameterInfo pi = mr.pia[i];
                        paramType = GetParamType(mr.pia[i]);
                        if (Utils.IsOutOnlyParam(pi))
                            continue;
                        elementType = paramType.GetElementType();
                        int arrayRank = paramType.GetArrayRank();
                        if (arrayRank == 2)
                            hasMultiDimensionalArray = true;
                        else if (arrayRank == 1 && elementType.IsArray)
                            hasJaggedArray = true;
                        else
                            // If it's not one of the above two, must be Array. If this assert fires, bug is in
                            // findMethodMatches() or possibleMatch().
                            Debug.Assert(paramType == typeof(Array));
                    }

                    if (leafExprType == ExpressionType.String) {
                        if (hasMultiDimensionalArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(string[,]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(string));
                        } else if (hasJaggedArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(string[][]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(string[]));
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else if (leafExprType == ExpressionType.Object) {
                        // leafObjectType could be null here, if all the elements of the incoming array are Null.
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            paramType = GetParamType(pi);
                            if (Utils.IsOutOnlyParam(pi))
                                continue;
                            // The culling that occurrred in findMethodMatches() is what supports this Assert.
                            Debug.Assert(paramType.IsArray || paramType == typeof(Array),
                                    "Unexpected type in pickBestMethod(): " + paramType + ". Should be an array or Array"); 
                            if (paramType.IsArray) {
                                int arrayRank = paramType.GetArrayRank();
                                Debug.Assert(arrayRank <= 2);
                                elementType = paramType.GetElementType();
                                if (arrayRank == 1)
                                    elementType = elementType.GetElementType();
                                if (leafObjectType == null || elementType.IsAssignableFrom(leafObjectType)) {
                                    args[i] = Utils.readArgAs(ml, argType, paramType);
                                    cullOutIncompatibleArrayMethods(methodRecs, i, paramType.GetElementType());
                                    break; 
                                }
                            } else {
                                // paramType == typeof(Array)
                                args[i] = Utils.readArgAs(ml, argType, paramType);
                                break;
                            }
                        }
                        // OK to leave here without assiging to args[i], which we can detect by noting that it still has the value it
                        // was initialized to. This will happen if the incoming objects did not match the types of any methods.
                        // The method finally chosen might have an out parameter at this slot. But we need to make sure we drain the link.
                        if (args[i] == canOnlyMatchOutParam)
                            Utils.discardNext(ml);
                    } else if (leafExprType == ExpressionType.Boolean) {
                        if (hasMultiDimensionalArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(bool[,]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(bool));
                        } else if (hasJaggedArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(bool[][]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(bool[]));
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else if (leafExprType == ExpressionType.Real) {
                        bool needsDouble = false, needsDecimal = false;
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            if (Utils.IsOutOnlyParam(pi))
                                continue;
                            paramType = GetParamType(pi);
                            if (paramType == typeof(double[,]) || paramType == typeof(double[][]))
                                needsDouble = true;
                            else if (paramType == typeof(decimal[,]) || paramType == typeof(decimal[][]))
                                needsDecimal = true;
                        }
                        if (hasMultiDimensionalArray) {
                            if (needsDecimal) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(decimal[,]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(decimal));
                            } else if (needsDouble) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(double[,]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(double));
                            } else {
                                // Note that for an Array slot, an array of reals will be read as float.
                                args[i] = Utils.readArgAs(ml, argType, typeof(float[,]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(float));
                            }
                        } else if (hasJaggedArray) {
                            if (needsDecimal) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(decimal[][]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(decimal[]));
                            } else if (needsDouble) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(double[][]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(double[]));
                            } else {
                                // Note that for an Array slot, an array of reals will be read as float.
                                args[i] = Utils.readArgAs(ml, argType, typeof(float[][]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(float[]));
                            }
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else if (leafExprType == ExpressionType.Integer) {
                        bool hasByte = false, hasSByte = false, hasChar = false, hasShort = false, hasUShort = false,
                                hasInt = false, hasUInt = false, hasLong = false, hasULong = false, hasDecimal = false,
                                    hasFloat = false, hasDouble = false;
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            paramType = GetParamType(pi);
                            // Of course we don't count out params, and also don't allow int list to be passed for an Enum[] slot.
                            if (Utils.IsOutOnlyParam(pi) || paramType.IsEnum)
                                continue;
                            if (paramType != typeof(Array)) {
                                elementType = paramType.GetElementType();
                                if (elementType.IsArray)
                                    elementType = elementType.GetElementType();
                                switch (Type.GetTypeCode(elementType)) {
                                    case TypeCode.Int32: hasInt = true; break;
                                    case TypeCode.Double: hasDouble = true; break;
                                    case TypeCode.Byte: hasByte = true; break;
                                    case TypeCode.SByte: hasSByte = true; break;
                                    case TypeCode.Char: hasChar = true; break;
                                    case TypeCode.Int16: hasShort = true; break;
                                    case TypeCode.UInt16: hasUShort = true; break;
                                    case TypeCode.UInt32: hasUInt = true; break;
                                    case TypeCode.Int64: hasLong = true; break;
                                    case TypeCode.UInt64: hasULong = true; break;
                                    case TypeCode.Decimal: hasDecimal = true; break;
                                    case TypeCode.Single: hasFloat = true; break;
                                    default: break;
                                }
                            }
                        }
                        Type widestType = null;
                        // We won't get into any of these 'if' clauses if the type is Array, so we are deciding here only between
                        // multidimensional and ragged arrays.
                        if (hasDecimal)
                            widestType = hasMultiDimensionalArray ? typeof(decimal[,]) : typeof(decimal[]);
                        else if (hasLong)
                            widestType = hasMultiDimensionalArray ? typeof(long[,]) : typeof(long[]);
                        else if (hasULong)
                            widestType = hasMultiDimensionalArray ? typeof(ulong[,]) : typeof(ulong[]);
                        else if (hasInt)
                            widestType = hasMultiDimensionalArray ? typeof(int[,]) : typeof(int[]);
                        else if (hasUInt)
                            widestType = hasMultiDimensionalArray ? typeof(uint[,]) : typeof(uint[]);
                        else if (hasShort)
                            widestType = hasMultiDimensionalArray ? typeof(short[,]) : typeof(short[]);
                        else if (hasUShort)
                            widestType = hasMultiDimensionalArray ? typeof(ushort[,]) : typeof(ushort[]);
                        else if (hasChar)
                            widestType = hasMultiDimensionalArray ? typeof(char[,]) : typeof(char[]);
                        else if (hasByte)
                            widestType = hasMultiDimensionalArray ? typeof(byte[,]) : typeof(byte[]);
                        else if (hasSByte)
                            widestType = hasMultiDimensionalArray ? typeof(sbyte[,]) : typeof(sbyte[]);
                        else if (hasDouble)
                            widestType = hasMultiDimensionalArray ? typeof(double[,]) : typeof(double[]);
                        else if (hasFloat)
                            widestType = hasMultiDimensionalArray ? typeof(float[,]) : typeof(float[]);
                        else
                            // Array and enum params fall through to here, although 'widestType' is ignored for Array params.
                            widestType = hasMultiDimensionalArray ? typeof(int[,]) : typeof(int[]);
                        if (hasMultiDimensionalArray) {
                            args[i] = Utils.readArgAs(ml, argType, widestType);
                            cullOutIncompatibleArrayMethods(methodRecs, i, widestType.GetElementType());
                        } else if (hasJaggedArray) {
                            Type subArrayType = Array.CreateInstance(widestType, 0).GetType();
                            args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(subArrayType, 1).GetType());
                            cullOutIncompatibleArrayMethods(methodRecs, i, subArrayType);
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else if (leafExprType == ExpressionType.Complex) {
                        if (ml.ComplexType == null)
                            throw new MathLinkException(MathLinkException.MLE_NO_COMPLEX);
                        if (hasMultiDimensionalArray) {
                            args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(ml.ComplexType, 1, 0).GetType());
                            cullOutIncompatibleArrayMethods(methodRecs, i, ml.ComplexType);
                        } else if (hasJaggedArray) {
                            Type subArrayType = Array.CreateInstance(ml.ComplexType, 0).GetType();
                            args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(subArrayType, 1).GetType());
                            cullOutIncompatibleArrayMethods(methodRecs, i, subArrayType);
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else {
                        // Leaf is Function or Symbol.
                        throw new ArgumentException();
                    }
                    break;
                }
                case Install.ARGTYPE_TENSOR3: {
                    // We know by earlier tests that all params at this slot must be 3-D arrays ([,,] or [][][]) or just Array.
                    // Look at first leaf value and read the array as that type. But first check if the param is an
                    // Expr[,,] or Expr[][][]. For all types, favor [,,] over [][][].
                    bool wasReadAsExprArray = false;
                    bool wasReadAsJaggedExprArray = false;
                    Type paramType = null;
                    foreach (MethodRec mr in methodRecs) {
                        ParameterInfo pi = mr.pia[i];
                        if (Utils.IsOutOnlyParam(pi))
                            continue;
                        paramType = GetParamType(pi);
                        if (paramType == typeof(Expr[,,])) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Expr[,,]));
                            wasReadAsExprArray = true;
                            break;
                        } else if (paramType == typeof(Expr[][][])) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Expr[][][]));
                            wasReadAsJaggedExprArray = true;
                            break;
                        }
                    }
                    // Cull out all methods that did not have an Expr[,] at this slot.
                    if (wasReadAsExprArray) {
                        cullOutIncompatibleArrayMethods(methodRecs, i, typeof(Expr));
                        break;  // On to next arg.
                    } else if (wasReadAsJaggedExprArray) {
                        cullOutIncompatibleArrayMethods(methodRecs, i, typeof(Expr[][]));
                        break;  // On to next arg.
                    }
                    // Now check the leaf type.
                    ExpressionType leafExprType;
                    leafExprType = getLeafExprType(ml, out var leafObjectType);

                    bool hasMultiDimensionalArray = false;
                    bool hasJaggedArray = false;
                    Type elementType;

                    foreach (MethodRec mr in methodRecs) {
                        ParameterInfo pi = mr.pia[i];
                        if (Utils.IsOutOnlyParam(pi))
                            continue;
                        paramType = GetParamType(pi);
                        elementType = paramType.GetElementType();
                        int arrayRank = paramType.GetArrayRank();
                        if (arrayRank == 3)
                            hasMultiDimensionalArray = true;
                        else if (arrayRank == 1 && elementType.IsArray && elementType.GetArrayRank() == 1
                                && elementType.GetElementType().IsArray)
                            hasJaggedArray = true;
                        else
                            // If it's not one of the above two, must be Array. If this assert fires, bug is in
                            // findMethodMatches() or possibleMatch().
                            Debug.Assert(paramType == typeof(Array));
                    }

                    if (leafExprType == ExpressionType.String) {
                        if (hasMultiDimensionalArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(string[,,]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(string));
                        } else if (hasJaggedArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(string[][][]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(string[][]));
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else if (leafExprType == ExpressionType.Object) {
                        // leafObjectType could be null here, if all the elements of the incoming array are Null.
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            if (Utils.IsOutOnlyParam(pi))
                                continue;
                            paramType = GetParamType(pi);
                            // The culling that occurrred in findMethodMatches() is what supports this Assert.
                            Debug.Assert(paramType.IsArray || paramType == typeof(Array),
                                    "Unexpected type in pickBestMethod(): " + paramType + ". Should be an array or Array"); 
                            if (paramType.IsArray) {
                                int arrayRank = paramType.GetArrayRank();
                                Debug.Assert(arrayRank == 3 || arrayRank == 1);
                                elementType = null;
                                if (arrayRank == 3)
                                    elementType = paramType.GetElementType();
                                else if (arrayRank == 1)
                                    elementType = paramType.GetElementType().GetElementType().GetElementType();
                                if (leafObjectType == null || elementType.IsAssignableFrom(leafObjectType)) {
                                    args[i] = Utils.readArgAs(ml, argType, paramType);
                                    cullOutIncompatibleArrayMethods(methodRecs, i, paramType.GetElementType());
                                    break; 
                                }
                            } else {
                                // paramType == typeof(Array)
                                args[i] = Utils.readArgAs(ml, argType, paramType);
                                break;
                            }
                        }
                        // OK to leave here without assiging to args[i], which we can detect by noting that it still has the value it
                        // was initialized to. This will happen if the incoming objects did not match the types of any methods.
                        // The method finally chosen might have an out parameter at this slot. But we need to make sure we drain the link.
                        if (args[i] == canOnlyMatchOutParam)
                            Utils.discardNext(ml);
                    } else if (leafExprType == ExpressionType.Boolean) {
                        if (hasMultiDimensionalArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(bool[,,]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(bool));
                        } else if (hasJaggedArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(bool[][][]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(bool[][]));
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else if (leafExprType == ExpressionType.Real) {
                        bool needsDouble = false, needsDecimal = false;
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            if (Utils.IsOutOnlyParam(pi))
                                continue;
                            paramType = GetParamType(pi);
                            if (paramType == typeof(double[,,]) || paramType == typeof(double[][][]))
                                needsDouble = true;
                            else if (paramType == typeof(decimal[,,]) || paramType == typeof(decimal[][][]))
                                needsDecimal = true;
                        }
                        if (hasMultiDimensionalArray) {
                            if (needsDecimal) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(decimal[,,]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(decimal));
                            } else if (needsDouble) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(double[,,]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(double));
                            } else {
                                // Note that for an Array slot, an array of reals will be read as float.
                                args[i] = Utils.readArgAs(ml, argType, typeof(float[,,]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(float));
                            }
                        } else if (hasJaggedArray) {
                            if (needsDecimal) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(decimal[][][]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(decimal[]));
                            } else if (needsDouble) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(double[][][]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(double[]));
                            } else {
                                // Note that for an Array slot, an array of reals will be read as float.
                                args[i] = Utils.readArgAs(ml, argType, typeof(float[][][]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(float[]));
                            }
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else if (leafExprType == ExpressionType.Integer) {
                        bool hasByte = false, hasSByte = false, hasChar = false, hasShort = false, hasUShort = false,
                                hasInt = false, hasUInt = false, hasLong = false, hasULong = false, hasDecimal = false,
                                    hasFloat = false, hasDouble = false;
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            paramType = GetParamType(pi);
                            // Of course we don't count out params, and also don't allow int list to be passed for an Enum[] slot.
                            if (Utils.IsOutOnlyParam(pi) || paramType.IsEnum)
                                continue;
                            if (paramType != typeof(Array)) {
                                elementType = paramType.GetElementType();
                                if (elementType.IsArray)
                                    elementType = elementType.GetElementType().GetElementType();
                                switch (Type.GetTypeCode(elementType)) {
                                    case TypeCode.Int32: hasInt = true; break;
                                    case TypeCode.Double: hasDouble = true; break;
                                    case TypeCode.Byte: hasByte = true; break;
                                    case TypeCode.SByte: hasSByte = true; break;
                                    case TypeCode.Char: hasChar = true; break;
                                    case TypeCode.Int16: hasShort = true; break;
                                    case TypeCode.UInt16: hasUShort = true; break;
                                    case TypeCode.UInt32: hasUInt = true; break;
                                    case TypeCode.Int64: hasLong = true; break;
                                    case TypeCode.UInt64: hasULong = true; break;
                                    case TypeCode.Decimal: hasDecimal = true; break;
                                    case TypeCode.Single: hasFloat = true; break;
                                    default: break;
                                }
                            }
                        }
                        Type widestType = null;
                        // We won't get into any of these 'if' clauses if the type is Array, so we are deciding here only between
                        // multidimensional and ragged arrays.
                        if (hasDecimal)
                            widestType = hasMultiDimensionalArray ? typeof(decimal[,,]) : typeof(decimal[][]);
                        else if (hasLong)
                            widestType = hasMultiDimensionalArray ? typeof(long[,,]) : typeof(long[][]);
                        else if (hasULong)
                            widestType = hasMultiDimensionalArray ? typeof(ulong[,,]) : typeof(ulong[][]);
                        else if (hasInt)
                            widestType = hasMultiDimensionalArray ? typeof(int[,,]) : typeof(int[][]);
                        else if (hasUInt)
                            widestType = hasMultiDimensionalArray ? typeof(uint[,,]) : typeof(uint[][]);
                        else if (hasShort)
                            widestType = hasMultiDimensionalArray ? typeof(short[,,]) : typeof(short[][]);
                        else if (hasUShort)
                            widestType = hasMultiDimensionalArray ? typeof(ushort[,,]) : typeof(ushort[][]);
                        else if (hasChar)
                            widestType = hasMultiDimensionalArray ? typeof(char[,,]) : typeof(char[][]);
                        else if (hasByte)
                            widestType = hasMultiDimensionalArray ? typeof(byte[,,]) : typeof(byte[][]);
                        else if (hasSByte)
                            widestType = hasMultiDimensionalArray ? typeof(sbyte[,,]) : typeof(sbyte[][]);
                        else if (hasDouble)
                            widestType = hasMultiDimensionalArray ? typeof(double[,,]) : typeof(double[][]);
                        else if (hasFloat)
                            widestType = hasMultiDimensionalArray ? typeof(float[,,]) : typeof(float[][]);
                        else
                            // Array and enum params fall through to here, although 'widestType' is ignored for Array params.
                            widestType = hasMultiDimensionalArray ? typeof(int[,,]) : typeof(int[][]);
                        if (hasMultiDimensionalArray) {
                            args[i] = Utils.readArgAs(ml, argType, widestType);
                            cullOutIncompatibleArrayMethods(methodRecs, i, widestType.GetElementType());
                        } else if (hasJaggedArray) {
                            Type subArrayType = Array.CreateInstance(widestType, 0).GetType();
                            args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(subArrayType, 1).GetType());
                            cullOutIncompatibleArrayMethods(methodRecs, i, subArrayType);
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else if (leafExprType == ExpressionType.Complex) {
                        if (ml.ComplexType == null)
                            throw new MathLinkException(MathLinkException.MLE_NO_COMPLEX);
                        if (hasMultiDimensionalArray) {
                            args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(ml.ComplexType, 1, 1, 0).GetType());
                            cullOutIncompatibleArrayMethods(methodRecs, i, ml.ComplexType);
                        } else if (hasJaggedArray) {
                            Type subArrayType2 = Array.CreateInstance(ml.ComplexType, 0).GetType();
                            Type subArrayType1 = Array.CreateInstance(subArrayType2, 1).GetType();
                            args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(subArrayType1, 1).GetType());
                            cullOutIncompatibleArrayMethods(methodRecs, i, subArrayType1);
                        } else {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                        }
                    } else {
                        // Leaf is Function or Symbol.
                        throw new ArgumentException();
                    }
                    break;
                }
                case Install.ARGTYPE_LIST: {
                    // We know from earlier tests that every arg slot is either Array or a >3-deep rectangular or any jagged.
                    // For 3-deep and greater, or jagged arrays from Mathematica, we get a little lax in overload resolution.
                    // Look for the first method with an arg slot that matches the data roughly and read as that type.
                    int depth = Utils.determineIncomingArrayDepth(ml);
                    // Now check the leaf type.
                    ExpressionType leafExprType;
                    leafExprType = getLeafExprType(ml, out var leafObjectType);

                    if (leafExprType == ExpressionType.Function)
                        // If the array is empty but not rectangular, it cannot fit any .NET array type.
                        throw new ArgumentException();
                    if (depth == 1) {
                        // Incoming array is non-vector: {1, {2}, 3}.
                        throw new ArgumentException();
                    } else if (depth == 2) {
                        // Incoming array is jagged.
                        bool hasJaggedExprArray = false;
                        bool hasJaggedArray = false;
                        Type paramType = null;
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            if (Utils.IsOutOnlyParam(pi))
                                continue;
                            paramType = GetParamType(pi);
                            if (paramType == typeof(Expr[][])) {
                                hasJaggedExprArray = true;
                                break;
                            } else if (paramType.IsArray) {
                                Type elementType = paramType.GetElementType();
                                int arrayRank = paramType.GetArrayRank();
                                if (arrayRank == 1 && elementType.IsArray) {
                                    hasJaggedArray = true;
                                    break;
                                }
                            }
                        }
                        if (hasJaggedExprArray) {
                            args[i] = Utils.readArgAs(ml, argType, typeof(Expr[][]));
                            cullOutIncompatibleArrayMethods(methodRecs, i, typeof(Expr[]));
                            break;  // On to next arg.
                        }

                        if (leafExprType == ExpressionType.String) {
                            if (hasJaggedArray) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(string[][]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(string[]));
                            } else {
                                args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                            }
                        } else if (leafExprType == ExpressionType.Object) {
                            // leafObjectType could be null here, if all the elements of the incoming array are Null.
                            foreach (MethodRec mr in methodRecs) {
                                ParameterInfo pi = mr.pia[i];
                                if (Utils.IsOutOnlyParam(pi))
                                    continue;
                                paramType = GetParamType(pi);
                                // The culling that occurrred in findMethodMatches() is what supports this Assert.
                                Debug.Assert(paramType.IsArray || paramType == typeof(Array),
                                        "Unexpected type in pickBestMethod(): " + paramType + ". Should be an array or Array"); 
                                if (paramType.IsArray) {
                                    int arrayRank = paramType.GetArrayRank();
                                    Type elementType = paramType.GetElementType();
                                    Debug.Assert(arrayRank > 3 || arrayRank == 1 && elementType.IsArray);
                                    if (arrayRank == 1) {
                                        // We are guaranteed to be able to go this deep:
                                        elementType = elementType.GetElementType();
                                        // But we want only 2-deep raggeds, so bail if it's deeper.
                                        if (!elementType.IsArray && (leafObjectType == null || elementType.IsAssignableFrom(leafObjectType))) {
                                            args[i] = Utils.readArgAs(ml, argType, paramType);
                                            cullOutIncompatibleArrayMethods(methodRecs, i, paramType.GetElementType());
                                            break; 
                                        }
                                    }
                                } else {
                                    // paramType == typeof(Array)
                                    args[i] = Utils.readArgAs(ml, argType, paramType);
                                    break;
                                }
                            }
                            // OK to leave here without assiging to args[i], which we can detect by noting that it still has the value it
                            // was initialized to. This will happen if the incoming objects did not match the types of any methods.
                            // The method finally chosen might have an out parameter at this slot. But we need to make sure we drain the link.
                            if (args[i] == canOnlyMatchOutParam)
                                Utils.discardNext(ml);
                        } else if (leafExprType == ExpressionType.Boolean) {
                            if (hasJaggedArray) {
                                args[i] = Utils.readArgAs(ml, argType, typeof(bool[][]));
                                cullOutIncompatibleArrayMethods(methodRecs, i, typeof(bool[]));
                            } else {
                                args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                            }
                        } else if (leafExprType == ExpressionType.Real) {
                            bool needsDouble = false, needsDecimal = false;
                            foreach (MethodRec mr in methodRecs) {
                                ParameterInfo pi = mr.pia[i];
                                if (Utils.IsOutOnlyParam(pi))
                                    continue;
                                paramType = GetParamType(pi);
                                if (paramType == typeof(double[][]))
                                    needsDouble = true;
                                else if (paramType == typeof(decimal[][]))
                                    needsDecimal = true;
                            }
                            if (hasJaggedArray) {
                                if (needsDecimal) {
                                    args[i] = Utils.readArgAs(ml, argType, typeof(decimal[][]));
                                    cullOutIncompatibleArrayMethods(methodRecs, i, typeof(decimal[]));
                                } else if (needsDouble) {
                                    args[i] = Utils.readArgAs(ml, argType, typeof(double[][]));
                                    cullOutIncompatibleArrayMethods(methodRecs, i, typeof(double[]));
                                } else {
                                    // Note that for an Array slot, an array of reals will be read as float.
                                    args[i] = Utils.readArgAs(ml, argType, typeof(float[][]));
                                    cullOutIncompatibleArrayMethods(methodRecs, i, typeof(float[]));
                                }
                            } else {
                                args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                            }
                        } else if (leafExprType == ExpressionType.Integer) {
                            bool hasByte = false, hasSByte = false, hasChar = false, hasShort = false, hasUShort = false,
                                    hasInt = false, hasUInt = false, hasLong = false, hasULong = false, hasDecimal = false,
                                        hasFloat = false, hasDouble = false;
                            foreach (MethodRec mr in methodRecs) {
                                ParameterInfo pi = mr.pia[i];
                                paramType = GetParamType(pi);
                                if (Utils.IsOutOnlyParam(pi) || paramType.IsEnum)
                                    continue;
                                Type elementType = paramType.IsArray ? paramType.GetElementType() : null;
                                while (elementType != null && elementType.IsArray)
                                    elementType = elementType.GetElementType();
                                switch (Type.GetTypeCode(elementType)) {
                                    case TypeCode.Int32: hasInt = true; break;
                                    case TypeCode.Double: hasDouble = true; break;
                                    case TypeCode.Byte: hasByte = true; break;
                                    case TypeCode.SByte: hasSByte = true; break;
                                    case TypeCode.Char: hasChar = true; break;
                                    case TypeCode.Int16: hasShort = true; break;
                                    case TypeCode.UInt16: hasUShort = true; break;
                                    case TypeCode.UInt32: hasUInt = true; break;
                                    case TypeCode.Int64: hasLong = true; break;
                                    case TypeCode.UInt64: hasULong = true; break;
                                    case TypeCode.Decimal: hasDecimal = true; break;
                                    case TypeCode.Single: hasFloat = true; break;
                                    default: break; // Including when elementype == null because paramType == Array or enum.
                                }
                            }
                            if (hasJaggedArray) {
                                Type widestType = null;
                                // We won't get into any of these 'if' clauses if the type is Array.
                                if (hasDecimal)
                                    widestType = typeof(decimal[]);
                                else if (hasLong)
                                    widestType = typeof(long[]);
                                else if (hasULong)
                                    widestType = typeof(ulong[]);
                                else if (hasInt)
                                    widestType = typeof(int[]);
                                else if (hasUInt)
                                    widestType = typeof(uint[]);
                                else if (hasShort)
                                    widestType = typeof(short[]);
                                else if (hasUShort)
                                    widestType = typeof(ushort[]);
                                else if (hasChar)
                                    widestType = typeof(char[]);
                                else if (hasByte)
                                    widestType = typeof(byte[]);
                                else if (hasSByte)
                                    widestType = typeof(sbyte[]);
                                else if (hasDouble)
                                    widestType = typeof(double[]);
                                else if (hasFloat)
                                    widestType = typeof(float[]);
                                else
                                    // Enum params fall through to here. Read as int[][].
                                    widestType = typeof(int[]);
                                args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(widestType, 1).GetType());
                                cullOutIncompatibleArrayMethods(methodRecs, i, widestType);
                            } else {
                                args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                            }
                        } else if (leafExprType == ExpressionType.Complex) {
                            if (ml.ComplexType == null)
                                throw new MathLinkException(MathLinkException.MLE_NO_COMPLEX);
                            if (hasJaggedArray) {
                                Type subArrayType = Array.CreateInstance(ml.ComplexType, 0).GetType();
                                args[i] = Utils.readArgAs(ml, argType, Array.CreateInstance(subArrayType, 1).GetType());
                                cullOutIncompatibleArrayMethods(methodRecs, i, subArrayType);
                            } else {
                                args[i] = Utils.readArgAs(ml, argType, typeof(Array));
                            }
                        } else {
                            // Leaf is Function or Symbol.
                            throw new ArgumentException();
                        }
                    } else {
                        // Incoming array is 3-deep jagged, or >=4-deep, jagged or not. All methods will have a >=4-deep multi array
                        // or any jagged.
                        // Look first for an ideal match to the incoming leaf type, then if not found settle for some method
                        // that is a reasonable match.
                        MethodRec exactMatchMethodRec = null, okMatchMethodRec = null;
                        Type complexType = ml.ComplexType;
                        foreach (MethodRec mr in methodRecs) {
                            ParameterInfo pi = mr.pia[i];
                            if (Utils.IsOutOnlyParam(pi))
                                continue;
                            Type paramType = GetParamType(pi);
                            if (paramType.IsArray) {
                                Type elementType = paramType.GetElementType();
                                int arrayRank = paramType.GetArrayRank();
                                Debug.Assert(arrayRank >= 4 || arrayRank == 1 && elementType.IsArray);
                                if (elementType.IsArray) {
                                    if (arrayRank > 1)
                                        // Don't allow multi at start, then jagged: [,][]
                                        continue;
                                    // Ensure that the param array is 1-deep all the way down: [][][][]...
                                    int thisParamJaggedArrayDepth = 1;
                                    while (elementType.IsArray && elementType.GetArrayRank() == 1) {
                                        elementType = elementType.GetElementType();
                                        thisParamJaggedArrayDepth++;
                                    }
                                    if (thisParamJaggedArrayDepth == depth) {
                                        if (leafExprType == ExpressionType.Integer && elementType == typeof(int) ||
                                                leafExprType == ExpressionType.Real && elementType == typeof(double) ||
                                                    leafExprType == ExpressionType.String && elementType == typeof(string) ||
                                                        leafExprType == ExpressionType.Boolean && elementType == typeof(bool) ||
                                                            leafExprType == ExpressionType.Object && (leafObjectType == null || elementType.IsAssignableFrom(leafObjectType)) ||
                                                                leafExprType == ExpressionType.Complex && elementType == ml.ComplexType) {
                                            exactMatchMethodRec = mr;
                                            break;
                                        } else if (leafExprType == ExpressionType.Integer && possibleMatch(Install.ARGTYPE_INTEGER, elementType, false, false, complexType) ||
                                                leafExprType == ExpressionType.Real && possibleMatch(Install.ARGTYPE_REAL, elementType, false, false, complexType) ||
                                                    leafExprType == ExpressionType.String && possibleMatch(Install.ARGTYPE_STRING, elementType, false, false, complexType) ||
                                                        leafExprType == ExpressionType.Boolean && possibleMatch(Install.ARGTYPE_BOOLEAN, elementType, false, false, complexType) ||
                                                            leafExprType == ExpressionType.Object && (leafObjectType == null || elementType.IsAssignableFrom(leafObjectType)) ||
                                                                leafExprType == ExpressionType.Complex && elementType == ml.ComplexType) {
                                            okMatchMethodRec = mr;
                                        }
                                    }
                                } else if (depth > 3) {
                                    // Multidim arrays. Depth must be >3 because if the incoming array is only 3-deep it is jagged.
                                    if (arrayRank != depth)
                                        continue;
                                    if (leafExprType == ExpressionType.Integer && elementType == typeof(int) ||
                                            leafExprType == ExpressionType.Real && elementType == typeof(double) ||
                                                leafExprType == ExpressionType.String && elementType == typeof(string) ||
                                                    leafExprType == ExpressionType.Boolean && elementType == typeof(bool) ||
                                                        leafExprType == ExpressionType.Object && (leafObjectType == null || elementType.IsAssignableFrom(leafObjectType)) ||
                                                            leafExprType == ExpressionType.Complex && elementType == ml.ComplexType) {
                                        exactMatchMethodRec = mr;
                                        break;
                                    } else if (leafExprType == ExpressionType.Integer && possibleMatch(Install.ARGTYPE_INTEGER, elementType, false, false, complexType) ||
                                            leafExprType == ExpressionType.Real && possibleMatch(Install.ARGTYPE_REAL, elementType, false, false, complexType) ||
                                                leafExprType == ExpressionType.String && possibleMatch(Install.ARGTYPE_STRING, elementType, false, false, complexType) ||
                                                    leafExprType == ExpressionType.Boolean && possibleMatch(Install.ARGTYPE_BOOLEAN, elementType, false, false, complexType) ||
                                                        leafExprType == ExpressionType.Object && (leafObjectType == null || elementType.IsAssignableFrom(leafObjectType)) ||
                                                            leafExprType == ExpressionType.Complex && elementType == ml.ComplexType) {
                                        okMatchMethodRec = mr;
                                    }
                                }
                            } // No else; ignore Array type for this pass.
                        }
                        if (exactMatchMethodRec != null) {
                            args[i] = Utils.readArgAs(ml, argType, GetParamType(exactMatchMethodRec.pia[i]));
                        } else if (okMatchMethodRec != null) {
                            args[i] = Utils.readArgAs(ml, argType, GetParamType(okMatchMethodRec.pia[i]));
                        }
                        // OK to leave here without assiging to args[i], which we can detect by noting that it still has the value it
                        // was initialized to. This will happen if the incoming objects did not match the types of any methods.
                        // The method finally chosen might have an out parameter at this slot. But we need to make sure we drain the link.
                        if (args[i] == canOnlyMatchOutParam)
                            Utils.discardNext(ml);
                    }
                    break;
                }
                case Install.ARGTYPE_OTHER: {
                    // Cull out any methods that don't have an out param at this slot (only an out param can take
                    // a junk arg like the one that is on the link).
                    for (int methodIndex = methodRecs.Count - 1; methodIndex >= 0; methodIndex--) {
                        MethodRec mr = (MethodRec) methodRecs[methodIndex];
                        if (!Utils.IsOutOnlyParam(mr.pia[i]))
                            methodRecs.RemoveAt(i); 
                    }
                    // If none survived the purging, then throw.
                    if (methodRecs.Count == 0)
                        throw new ArgumentException();
                    Utils.discardNext(ml);
                    args[i] = null;
                    break;
                }
                default:
                    Debug.Fail("Bad ArgType enum value in pickBestMethod()");
                    break;
            }
        }

        // We have now read all the args and done some culling of the possible methods.

        int numMatches = methodRecs.Count;
        if (numMatches == 0)
            throw new ArgumentException();

        bool[] objsAssignable = new bool[numMatches];
        bool[] primsMatchExactly = new bool[numMatches];

        bool atLeastOneMethodMatchesObjects = false;
        for (int i = 0; i < numMatches; i++) {
            ParameterInfo[] paramInfos = ((MethodRec) methodRecs[i]).pia;
            int objsMatch = 0, primsMatch = 0;
            checkTypeMatches(args, paramInfos, out objsMatch, out primsMatch);
            if (objsMatch == EXACTLY && primsMatch == EXACTLY)
                // Leave all this mess behind as soon as we find an exact match.
                return ((MethodRec) methodRecs[i]).m;
            if (objsMatch == EXACTLY || objsMatch == ASSIGNABLE) {
                objsAssignable[i] = true;
                atLeastOneMethodMatchesObjects = true;
            }
            // Because primitives are read as the widest type of any method at that slot, prims will either be an exact
            // match for a slot or they will require narrowing. 
            if (primsMatch == EXACTLY)
                primsMatchExactly[i] = true;
        }
        
        // Getting here means that no method/ctor had an _exact_ match for objects and primitives.
        
        // If there were none whose objects matched in a callable way, bail.
        if (!atLeastOneMethodMatchesObjects)
            throw new ArgumentException();

        // Now settle for the first match in an IsAssignableFrom way.
        for (int i = 0; i < numMatches; i++) {
            if (objsAssignable[i] && primsMatchExactly[i])
                return ((MethodRec) methodRecs[i]).m;
        }
                
        // Getting here means that there were ctors/methods whose objects matched in the "IsAssignableFrom" sense, but
        // whose primitives need to be massaged. This can result from several scenarios, for example:
        //      Class has ctor sigs  (int, Object1)  and  (byte, Object2)   where Object1 and Object2 are not related.
        // If the ctor is called from M with Object2, the args will be read as (INT, OBJECT). The int arg will be too
        // wide for the Object2 form. It is presumably rare to get this far, except in the case where an integer was sent
        // for an Enum slot.
        object[] narrowedArgs = new object[argTypes.Length];
        for (int i = 0; i < numMatches && objsAssignable[i]; i++) {
            args.CopyTo(narrowedArgs, 0);
            ParameterInfo[] paramInfos = ((MethodRec) methodRecs[i]).pia;
            if (narrowPrimitives(args, paramInfos, narrowedArgs)) {
                narrowedArgs.CopyTo(args, 0);
                return ((MethodRec) methodRecs[i]).m;
            }
        }                
            
        // Fall-through to here means that there was no appropriate ctor/method for the args.
        // This should only happen on a user error (passing bad args), not a failing of .NET/Link to disambiguate among
        // potentials.
        throw new ArgumentException();
    }


    // Called for ctors, methods, and non-static parameterized props.
    private static void findMethodMatches(string methName, bool isCtor, int[] argTypes, MethodBase[] methods, string[] methodNames,
                                IList matchingMethods, Type complexClass, out int err) {

        bool nameExists = false;
        bool argCountCorrect = false;
        bool argsOK = false;
        for (int i = 0; i < methods.Length; i++) {
            bool nameMatches = isCtor || Utils.memberNamesMatch(methodNames[i], methName);
            if (!nameMatches)
                continue;
            nameExists = true;
            MethodBase mi = methods[i];
            ParameterInfo[] pis = mi.GetParameters();
            // To see if the number of supplied arguments is appropriate, we must consider trailing optional params.
            int numTrailingOptionals = 0;
            for (int j = 0; j < pis.Length; j++) {
                if (pis[j].IsOptional)
                    numTrailingOptionals++;
                else
                    numTrailingOptionals = 0;
            }
            bool argCountMatches =
                argTypes.Length <= pis.Length &&                      // Caller hasn't supplied too many args.
                argTypes.Length + numTrailingOptionals >= pis.Length; // If too few, they can be made up by trailing optionals.
            if (!argCountMatches)
                continue;
            argCountCorrect = true;
            bool theseArgsMatch = true;
            for (int j = 0; j < argTypes.Length; j++) {
                // Ignore args passed from M if this is an out param.
                ParameterInfo pi = pis[j];
                if (!Utils.IsOutOnlyParam(pi) && !possibleMatch(argTypes[j], GetParamType(pi), pi.IsOptional, pi.ParameterType.IsPointer, complexClass)) {
                    theseArgsMatch = false;
                    break;
                }
            }
            if (!theseArgsMatch)
                continue;
            argsOK = true;
            matchingMethods.Add(mi);
        }
        if (!nameExists)
            err = NAME_NOT_FOUND;
        else if (!argCountCorrect)
            err = ARG_COUNT_INCORRECT;
        else if (!argsOK)
            err = ARGS_DONT_MATCH;
        else
            err = OK;
    }


    private static bool possibleMatch(int argType, Type paramType, bool isOptional, bool wasPointer, Type complexClass) {

        // paramType has already been "dereferenced" if it is byref or a pointer. The wasPointer arg tells us
        // whether paramType was originally a pointer before dereferencing.

        TypeCode paramTypeCode = Type.GetTypeCode(paramType);

        // Any incoming argument type can be passed as an Expr.
        if (paramType == typeof(Expr))
            return true;

        switch (argType) {
            case Install.ARGTYPE_INTEGER:
                return (Utils.IsTrulyPrimitive(paramType) && paramTypeCode != TypeCode.Boolean) || paramType.IsEnum ||
                            paramTypeCode == TypeCode.Decimal || paramType == typeof(object) || paramType == complexClass;
            case Install.ARGTYPE_REAL:
                return paramTypeCode == TypeCode.Double || paramTypeCode == TypeCode.Single ||
                            paramTypeCode == TypeCode.Decimal || paramType == typeof(object) || paramType == complexClass;
            case Install.ARGTYPE_STRING:
                return paramTypeCode == TypeCode.String || paramType == typeof(object);
            case Install.ARGTYPE_BOOLEAN:
                return paramTypeCode == TypeCode.Boolean || paramType == typeof(object);
            case Install.ARGTYPE_OBJECTREF:
                return true;  // Allow passing an object ref for any param type.
            case Install.ARGTYPE_NULL:
                return paramTypeCode == TypeCode.Object || paramTypeCode == TypeCode.String;
            case Install.ARGTYPE_MISSING:
                return isOptional;
            case Install.ARGTYPE_VECTOR:
                return (paramType.IsArray && paramType.GetArrayRank() == 1) || paramType == typeof(Array) ||
                            paramType == typeof(object) || wasPointer;
            case Install.ARGTYPE_MATRIX: {
                if (paramType == typeof(Array) || paramType == typeof(object) || paramType == typeof(object[]))
                    return true;
                if (!paramType.IsArray)
                    return false;
                int arrayRank = paramType.GetArrayRank();
                Type elementType = paramType.GetElementType();
                // Either an array like [,] or [][].
                return arrayRank == 2 || arrayRank == 1 && elementType.IsArray && elementType.GetArrayRank() == 1;
            }
            case Install.ARGTYPE_TENSOR3: {
                if (paramType == typeof(Array) ||
                        paramType == typeof(object) ||
                            paramType == typeof(object[]) || 
                                paramType == typeof(object[,]) || 
                                    paramType == typeof(object[][]))
                    return true;
                if (!paramType.IsArray)
                    return false;
                int arrayRank = paramType.GetArrayRank();
                Type elementType = paramType.GetElementType();
                // Either an array like [,,] or [][][].
                return arrayRank == 3 || arrayRank == 1 && elementType.IsArray &&
                            elementType.GetArrayRank() == 1 && elementType.GetElementType().IsArray &&
                                elementType.GetElementType().GetArrayRank() == 1;
            }
            case Install.ARGTYPE_LIST: {
                if (paramType == typeof(Array) || paramType == typeof(object) || paramType == typeof(object[]))
                    return true;
                if (!paramType.IsArray)
                    return false;
                int arrayRank = paramType.GetArrayRank();
                Type elementType = paramType.GetElementType();
                // We know the incoming arg value is not a vector or matrix or tensor3.
                // Test is that param array is: Either > 3 deep, or is jagged.
                return arrayRank > 3 || arrayRank == 1 && elementType.IsArray;
            }
            case Install.ARGTYPE_COMPLEX:
                return paramType == complexClass || paramType == typeof(object);
            case Install.ARGTYPE_OTHER:
                return false;
            default:
                Debug.Fail("Bad type in possibleMatch()");
                return false;
        }
    }


    private static void checkTypeMatches(object[] args, ParameterInfo[] paramInfos, out int objsMatch, out int primsMatch) {
        
        objsMatch = EXACTLY;
        primsMatch = EXACTLY;
        for (int i = 0; i < args.Length; i++) {
            if (args[i] == null) {
                // TODO: was I thinking about out params here...?
            } else {
                Type argType = args[i] != null ? args[i].GetType() : null;
                ParameterInfo pi = paramInfos[i];
                Type paramType = GetParamType(pi);  // This will "dereference" byref and pointer types.
                if (Utils.IsOutOnlyParam(pi)) {
                    // TODO: This do nothing is temporary until I understand better...
                    // Do nothing
                } else if (paramType == argType) {
                    // Do nothing
                } else if (pi.IsOptional && args[i] == Missing.Value) {
                    // Do nothing
                } else if (pi.ParameterType.IsPointer && (argType == typeof(Pointer) || argType == typeof(IntPtr) ||
                                argType.IsArray || argType == typeof(Array))) {
                    // Do nothing. Note the weak overload resolution here--no check of the array element type vs. the pointer type,
                    // or checking the depth of array (we only support passing a vector to a pointer arg).
                } else if (Utils.IsTrulyPrimitive(paramType)) {
                    primsMatch = NOT;
                } else if (paramType.IsEnum && Utils.IsTrulyPrimitive(argType)) {
                    // User passed an integer for an Enum slot (it can be upcast in narrowPrimitives()).
                    primsMatch = NOT;
                } else if (paramType.IsAssignableFrom(argType)) {
                    if (objsMatch != NOT)
                        objsMatch = ASSIGNABLE;
                } else {
                    objsMatch = NOT;
                }
            }
        }
    }

    
    // Checks whether primitives can be narrowed without losing information, and does the narrowing. This also handles
    // the case where an integer was read as an int but needs to be converted up to an enum.
    // The narrowedArgs array has already been filled with the original args, so only prims that need to be narrowed
    // have to be modified.
    private static bool narrowPrimitives(object[] args, ParameterInfo[] paramInfos, object[] narrowedArgs) {
            
        try {
            for (int i = 0; i < args.Length; i++) {
                if (args[i] == null)
                    continue;
                Type paramType = GetParamType(paramInfos[i]);
                if (paramType.IsEnum) {
                    narrowedArgs[i] = Enum.ToObject(paramType, narrowedArgs[i]);
                    continue;
                }
                if (!Utils.IsTrulyPrimitive(paramType) || Utils.IsOutOnlyParam(paramInfos[i]) || args[i] == Missing.Value)
                    continue;
                TypeCode paramTypeCode = Type.GetTypeCode(paramType);
                switch (paramTypeCode) {
                    case TypeCode.Byte:
                        narrowedArgs[i] = Convert.ToByte(narrowedArgs[i]);
                        break;
                    case TypeCode.SByte:
                        narrowedArgs[i] = Convert.ToSByte(narrowedArgs[i]);
                        break;
                    case TypeCode.Char:
                        narrowedArgs[i] = Convert.ToChar(narrowedArgs[i]);
                        break;
                    case TypeCode.UInt16:
                        narrowedArgs[i] = Convert.ToUInt16(narrowedArgs[i]);
                        break;
                    case TypeCode.Int16:
                        narrowedArgs[i] = Convert.ToInt16(narrowedArgs[i]);
                        break;
                    case TypeCode.Int32:
                        narrowedArgs[i] = Convert.ToInt32(narrowedArgs[i]);
                        break;
                    case TypeCode.UInt32:
                        narrowedArgs[i] = Convert.ToUInt32(narrowedArgs[i]);
                        break;
                    case TypeCode.Int64:
                        narrowedArgs[i] = Convert.ToInt64(narrowedArgs[i]);
                        break;
                    case TypeCode.UInt64:
                        narrowedArgs[i] = Convert.ToUInt64(narrowedArgs[i]);
                        break;
                    case TypeCode.Single:
                        narrowedArgs[i] = Convert.ToSingle(narrowedArgs[i]);
                        break;
                    case TypeCode.Double:
                        narrowedArgs[i] = Convert.ToDouble(narrowedArgs[i]);
                        break;
                    default:
                        break; // Do nothing.
                }
            }
        } catch (Exception) {
            // Various conversion exceptions all mean that the narrowing is not valid, either because the
            // data types are completely out of whack (I think this is actually prevented elsewhere) or
            // the value to convert is out of the range of the type to convert to.
            return false;
        }
        return true;
    }


    private bool isWideningConversion(Type from, ParameterInfo pi) {

        Type to = GetParamType(pi);

        if (from == to)
            return true;

        if (Utils.IsTrulyPrimitive(from) && to == typeof(object))
            return true;

        if (to.IsByRef && to.GetElementType() == from)
            return true;
        
        // Cannot use 'to' in next test because GetParamType() dereferences pointers.
        if ((from == typeof(Pointer) || from == typeof(IntPtr)) && pi.ParameterType.IsPointer)
            return true;

        if (!Utils.IsTrulyPrimitive(from))
            return to.IsAssignableFrom(from);

        switch (Type.GetTypeCode(from)) {
            case TypeCode.Byte:
            case TypeCode.SByte:
                return to == typeof(short) || to == typeof(ushort) || to == typeof(int) || to == typeof(uint) || to == typeof(long) ||
                            to == typeof(ulong) || to == typeof(float) || to == typeof(double) || to == typeof(decimal);
            case TypeCode.Int16:
            case TypeCode.Char:
            case TypeCode.UInt16:
                return to == typeof(int) || to == typeof(uint) || to == typeof(long) || to == typeof(ulong) || to == typeof(float) ||
                            to == typeof(double) || to == typeof(decimal);
            case TypeCode.Int32:
            case TypeCode.UInt32:
                return to == typeof(long) || to == typeof(ulong) || to == typeof(float) || to == typeof(double) || to == typeof(decimal);
            case TypeCode.Int64:
            case TypeCode.UInt64:
                return to == typeof(float) || to == typeof(double) || to == typeof(decimal);
            case TypeCode.Single:
                return to == typeof(double) || to == typeof(decimal);
            case TypeCode.Double:
                return to == typeof(decimal);
            default:
                // Nothing should fall through to here.
                return to.IsAssignableFrom(from);
        }
    }


    // The reason that depth is not passed as an arg is that we are almost undoubtedly only deciding among
    // arrays of the same depth (by the tests in findMatchingMethods()). By not also checking depth we are
    // merely being more lenient--not culling a method with an array of the right type but the wrong depth.
    private static void cullOutIncompatibleArrayMethods(ArrayList methodRecs, int argPos, Type leafTypeToKeep) {

        for (int i = methodRecs.Count - 1; i >= 0; i--) {
            MethodRec mr = (MethodRec) methodRecs[i];
            ParameterInfo pi = mr.pia[argPos];
            // GetParamType will "deref" pointer types, but we don't want that here.
            Type paramType = pi.ParameterType.IsPointer ? pi.ParameterType : GetParamType(pi);
            Debug.Assert(paramType.IsArray || paramType == typeof(Array) || pi.ParameterType.IsPointer);
            if (paramType != typeof(Array) && !Utils.IsOutOnlyParam(pi)) {
                Type elementType = paramType.GetElementType();
                bool removeThisMethod = false;
                if (Utils.IsTrulyPrimitive(elementType))
                    removeThisMethod = elementType != leafTypeToKeep;
                else
// TOOK THIS OUT: decision not to allow int list to be passed to an enum[] arg.
//                else if (!(elementType.IsEnum && leafTypeToKeep == typeof(int)))
//                    // Don't cull if element is an enum and the leafTypeToKeep is an int. For enums, leafTypeToKeep
//                    // always comes in as int, not the underying type of the enum.
                    removeThisMethod = !elementType.IsAssignableFrom(leafTypeToKeep);
                if (removeThisMethod)
                    methodRecs.RemoveAt(i); 
            }
        }
    }


    // Reads through the expression on the link, looking for the first leaf element it finds and returns the ExpressionType.
    // Will return ExpressionType.Function if the array is empty at the last dimension. Also fills the out param with the type
    // of object if it is an array of object refs. This leafObjectType value will be null if there are only Nulls.
    // Will throw MathLinkException if called when the watiting expression is not a function. If it succeeds without
    // throwing an exception, the link is left in the same state as when it started.
    // As an example, it will give ExpressionType.String for an array like this: {{}, {"abc"}}.
    private ExpressionType getLeafExprType(IKernelLink ml, out Type leafObjectType) {

        ILinkMark mark = ml.CreateMark();
        try {
            ExpressionType result = getLeafExprType0(ml, out leafObjectType);
            ml.SeekMark(mark);
            return result;
        } finally {
            ml.DestroyMark(mark);
        }
    }


    // This is the recursive worker function (doesn't set marks).
    private ExpressionType getLeafExprType0(IKernelLink ml, out Type leafObjectType) {

        leafObjectType = null;
        bool hadNull = false;
        ExpressionType result = ExpressionType.Function;
        string f = ml.GetFunction(out var len);
        if (f == "List") {
            for (int i = 0; i < len; i++) {
                result = ml.GetNextExpressionType();
                if (result == ExpressionType.Function) {
                    ExpressionType thisBranchLeafExprType = getLeafExprType0(ml, out leafObjectType);
                    // If we have an object array, don't return until we have found a non-Null object entry.
                    if (thisBranchLeafExprType != ExpressionType.Function &&
                            (thisBranchLeafExprType != ExpressionType.Object || leafObjectType != null))
                        return thisBranchLeafExprType;
                } else if (result == ExpressionType.Object) {
                    object obj = ml.GetObject();
                    if (obj == null) {
                        // If we find a Null leaf we want to keep looking so as to get object type info. But we
                        // record that a Null was found so that if no true objects are found elsewhere we can
                        // return ExpressionType.Object instead of Function.
                        hadNull = true;
                    } else {
                        leafObjectType = obj.GetType();
                        return result;
                    }
                } else {
                    return result;
                }
            }
        }
        // Walking off the end of the loop means that the leaves at every level of this branch were {} or Null.
        return result == ExpressionType.Function && hadNull ? ExpressionType.Object : result;
    }


    // For ref params, the type will be something like Int32&; for pointers, Int32*. We want this method
    // to test based on the "element type" for ref types and pointer types.
    private static Type GetParamType(ParameterInfo pi) {
        Type paramType = pi.ParameterType;
        return paramType.IsByRef || paramType.IsPointer ? paramType.GetElementType() : paramType;
    }


    // These are the functions that translate back and forth from the Mathematica representation of a .NET
    // object to the key used for lookup on the .NET side.
    // The two possible forms for the Mathematica symbol are NETLink`Objects`NETObject$XXXXX and
    // NETLink`Objects`NETObject$TypeAliasString$XXXXX. In both, XXXXX is a positive integer that gives
    // the key used for looking up objects in the InstanceCollection. TypeAliasString is used whenever
    // the same object needs multiple repesentations in Mathematica. This occurs for cast objects
    // created with either CastNETObject[] or CastCOMObject[].

    private static int objectSymbolPrefixLength = Install.MMA_OBJECTSYMBOLPREFIX.Length;

    private static ulong keyFromMmaSymbol(string sym) {

        // It is very possible to get here with a bogus object symbol name. In fact, this is the only place
        // where we even decide if the name has the right form. Return 0 if the symbol does not have the right
        // form.
        if (!sym.StartsWith(Install.MMA_OBJECTSYMBOLPREFIX))
            return 0;
        // This strips off everything up to, and including, the first $: "NETLink`Objects`NETObject$".
        string keyString = sym.Substring(objectSymbolPrefixLength);
        // Might still be type alias information in keyString, so skip to after next $ if one exists.
        int endTypeInfo = keyString.IndexOf('$');
        if (endTypeInfo > 0)
            keyString = keyString.Substring(endTypeInfo + 1);
        // Will always be > 0 (all instance index values sent to M are > 0).
        return UInt64.Parse(keyString);
    }

    private static String mmaSymbolFromKey(ulong key, string typeAlias) {
        
        if (typeAlias == "") {
            return Install.MMA_OBJECTSYMBOLPREFIX + key;
        } else {
            // This can run into minor trouble if the names of two different types that an object
            // was cast to hashed to the same value. I'll assume that is too unlikely to be worth considering.
            return Install.MMA_OBJECTSYMBOLPREFIX + ((uint) typeAlias.GetHashCode()) + "$" + key;
        }
    }


    /***************************************  TypeRecord Class  *******************************************/
    
    private class TypeRecord {

        private Type t;
        private string typeName;
        private string fullNameForMma;
        private ConstructorInfo[] cis;
        private FieldInfo[] fis;
        private PropertyInfo[] pis;
        private MethodInfo[] mis;
        private EventInfo[] eis;
        private PropertyInfo[] indxrs;  // "Default parameterized property" in VB lingo.
        private string[] fNames;
        private string[] pNames;
        private string[] mNames;
        private string[] eNames;
        private string iName;

        internal TypeRecord(Type t) {

            this.t = t;
            typeName = t.Name;
            fullNameForMma = fullTypeNameForMathematica(t);
            cis = t.GetConstructors();
            fis = t.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            pis = t.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            mis = t.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            eis = t.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            // Members that are re-declared as 'new' in a class show up twice in the list of methods--once for the parent
            // method and once for the re-declared member. This does not happen with virtuals overriden in a child class
            // (only the overriden child member shows up). We want to remove such hidden-by-signature parent class members.
            fis = (FieldInfo[]) cullOutHiddenBySigMembers(fis).ToArray(typeof(FieldInfo));
            pis = (PropertyInfo[]) cullOutHiddenBySigMembers(pis).ToArray(typeof(PropertyInfo));
            mis = (MethodInfo[]) cullOutHiddenBySigMembers(mis).ToArray(typeof(MethodInfo));
            eis = (EventInfo[]) cullOutHiddenBySigMembers(eis).ToArray(typeof(EventInfo));

            if (t.IsInterface) {
                // If an object is typed as an interface, the only methods available to it will be those found by the
                // GetMethods() call on the interface type. This means that even the Object methods (ToString,
                // GetHashValue, etc.) will not be available on that object. There are two cases in .NET/Link where
                // an object could be typed as an interface instead of its true runtime class type. The first case
                // is where CastNETObject was called on it to deliberately upcast to an interface type. On such
                // an object, it is no big deal that Object methods cannot be called. In fact, it could be
                // considered a feature--that is what the user requested by upcasting. The other case, however,
                // is where .NET/Link automatically types a COM object as a managed interface based on type
                // information from the method call that returned it. For such objects, the user never requested
                // the slicing to the interface type, and they are probably unaware that the object's type is
                // an interface instead of a class. They would be very confused if obj@ToString[] didn't work
                // on these objects. To fix this, we add the Object methods to the set of callable methods on
                // all objects that are typed by an interface instead of a class.
                MethodInfo[] objectMis = typeof(object).GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
                MethodInfo[] newMis = new MethodInfo[mis.Length + objectMis.Length];
                mis.CopyTo(newMis, 0);
                objectMis.CopyTo(newMis, mis.Length);
                mis = newMis;
            }

            // It is expensive to call the Name property, so to avoid doing it at every method call we cache the names.
            fNames = new string[fis.Length];
            for (int i = 0; i < fis.Length; i++)
                fNames[i] = fis[i].Name;
            pNames = new string[pis.Length];
            for (int i = 0; i < pis.Length; i++)
                pNames[i] = pis[i].Name;
            mNames = new string[mis.Length];
            for (int i = 0; i < mis.Length; i++)
                mNames[i] = mis[i].Name;
            eNames = new string[eis.Length];
            for (int i = 0; i < eis.Length; i++)
                eNames[i] = eis[i].Name;
            ArrayList indxs = new ArrayList();
            foreach (MemberInfo mi in t.GetDefaultMembers()) {
                if (mi.MemberType == MemberTypes.Property) {
                    indxs.Add(mi);
                    iName = mi.Name;
                }
            }
            indxrs = (PropertyInfo[]) indxs.ToArray(typeof(PropertyInfo));
        }

        internal Type type {
            get { return t; }
        }

        internal string name {
            get { return typeName; }
        }

        internal string fullName {
            get { return fullNameForMma; }
        }

        internal Array staticFieldNames {
            get {
                ArrayList names = new ArrayList();
                foreach (FieldInfo fi in fis) {
                    if (fi.IsStatic)
                        names.Add(fi.Name);
                }
                return names.ToArray();
            }
        }

        internal Array staticPropertyNames {
            get {
                ArrayList names = new ArrayList();
                foreach (PropertyInfo pi in pis) {
                    // Don't know why there isn't an IsStatic property for properties, but there isn't,
                    // so we look at the accessor methods.
                    MethodInfo[] accessors = pi.GetAccessors();
                    foreach (MethodInfo mi in accessors) {
                        if (mi.IsStatic) {
                            names.Add(pi.Name);
                            break;
                        }
                    }
                }
                return names.ToArray();
            }
        }

        internal Array staticMethodNames {
            get {
                ArrayList names = new ArrayList();
                foreach (MethodInfo mi in mis) {
                    if (mi.IsStatic)
                        names.Add(mi.Name);
                }
                return names.ToArray();
            }
        }

        internal Array staticEventNames {
            get {
                ArrayList names = new ArrayList();
                foreach (EventInfo ei in eis) {
                    // Don't know why there isn't an IsStatic property for properties, but there isn't,
                    // so we look at the Add method.
                    MethodInfo addMethod = ei.GetAddMethod();
                    if (addMethod.IsStatic)
                        names.Add(ei.Name);
                }
                return names.ToArray();
            }
        }

        internal Array nonPrimitiveFieldOrSimplePropNames {
            // Name is a bit of a misnomer. We want the names of all field/prop members that are of types
            // that will be sent to M as references. Only non-static members.
            get {
                ArrayList names = new ArrayList();
                foreach (PropertyInfo pi in pis) {
                    // Only object types. Note that we don't bother to check against ml.GetComplexType(). We don't have
                    // the link available here, and although it would be simple to pass in the complex class, it is
                    // probably not worth the clutter. It's not a problem if we overestimate the fields that hold byref types.
                    // This just means the Mathematica will have to do a tiny amount more work deciding if a chained
                    // field/prop get needs regrouping: (obj@prop)@prop. It is only a small optimization that we even
                    // bother to cull out the fields/props that cannot hold object types.
                    Type t = pi.PropertyType;
                    if (Utils.IsTrulyPrimitive(t) || t == typeof(string) || t == typeof(Expr) ||
                            t == typeof(Array) || t.IsArray)
                        continue;
                    // Must have a Get accessor. Chaining is only for gets, not sets.
                    if (!pi.CanRead)
                        continue;
                    // Don't want parameterized props.
                    if (pi.GetIndexParameters().Length > 0)
                        continue;
                    // Don't want statics.
                    MethodInfo[] accessors = pi.GetAccessors();
                    foreach (MethodInfo mi in accessors) {
                        if (!mi.IsStatic) {
                            names.Add(pi.Name);
                            break;
                        }
                    }
                }
                foreach (FieldInfo fi in fis) {
                    Type t = fi.FieldType;
                    if (Utils.IsTrulyPrimitive(t) || t == typeof(string) || t == typeof(Expr) ||
                            t == typeof(Array) || t.IsArray)
                        continue;
                    if (!fi.IsStatic)
                        names.Add(fi.Name);
                }
                return names.ToArray();
            }
        }

        internal Array isStaticPropertyParameterized {
            get {                
                ArrayList isParameterized = new ArrayList();
                foreach (PropertyInfo pi in pis) {
                    bool isStatic = false;
                    foreach (MethodInfo mi in pi.GetAccessors()) {
                        if (mi.IsStatic) {
                            isStatic = true;
                            break;
                        }
                    }
                    if (isStatic)
                        isParameterized.Add(pi.GetIndexParameters().Length > 0);
                }
                return isParameterized.ToArray();
            }
        }

        internal string[] fieldNames {
            get { return fNames; }
        }
        internal string[] propertyNames {
            get { return pNames; }
        }
        internal string[] methodNames {
            get { return mNames; }
        }
        internal string[] eventNames {
            get { return eNames; }
        }
        internal string indexerName {
            get { return iName; }
        }

        internal ConstructorInfo[] constructors {
            get { return cis; }
        }
        internal FieldInfo[] fields {
            get { return fis; }
        }
        internal PropertyInfo[] properties {
            get { return pis; }
        }
        internal MethodInfo[] methods {
            get { return mis; }
        }
        internal EventInfo[] events {
            get { return eis; }
        }
        internal MemberInfo[] indexers {
            get { return indxrs; }
        }

        // Members that are re-declared as 'new' in a class show up twice in the list of methods--once for the parent
        // method and once for the re-declared member. This does not happen with virtuals overriden in a child class
        // (only the overriden child member shows up). We want to remove such hidden-by-signature parent class members.
        private ArrayList cullOutHiddenBySigMembers(MemberInfo[] mis) {

            bool isProperty = false, isMethod = false;
            // Convenient to reuse a single one of these:
            ParameterInfo[] emptyParams = new ParameterInfo[0];

            ArrayList result = new ArrayList();
            if (mis.Length == 0)
                return result;

            switch (mis[0].MemberType) {
                case MemberTypes.Property:
                    isProperty = true;
                    break;
                case MemberTypes.Method:
                    isMethod = true;
                    break;
                default:
                    break;
            }
            // March through the list of members and null out any that are hidden parent members.
            for (int i = 0; i < mis.Length; i++) {
                MemberInfo mi = mis[i];
                // mi could be null, as this process is nulling out elements in the mis array.
                if (mi == null)
                    continue;
                string memberName = mi.Name;
                ParameterInfo[] parms;
                if (isProperty)
                    parms = ((PropertyInfo) mi).GetIndexParameters();
                else if (isMethod)
                    parms = ((MethodInfo) mi).GetParameters();
                else
                    parms = emptyParams;
                for (int j = 0; j < mis.Length; j++) {
                    MemberInfo otherMi = mis[j];
                    if (otherMi == null)
                        continue;
                    bool isIdenticalSig = true;
                    if (otherMi.Name == memberName) {
                        ParameterInfo[] otherParms;
                        if (isProperty)
                            otherParms = ((PropertyInfo) otherMi).GetIndexParameters();
                        else if (isMethod)
                            otherParms = ((MethodInfo) otherMi).GetParameters();
                        else
                            otherParms = emptyParams;
                        if (otherParms.Length != parms.Length)
                            break;
                        for (int k = 0; k < otherParms.Length; k++) {
                            if (parms[k] != otherParms[k]) {
                                isIdenticalSig = false;
                                break;
                            }
                        }
                        // If we have found a member (otherMi) that is identical in sig to another (mi), we only want to
                        // cull out otherMi if it was declared higher up in the inheritance hierarchy than mi. 
                        if (isIdenticalSig && mi.DeclaringType.IsSubclassOf(otherMi.DeclaringType))
                            mis[j] = null;
                    }
                }
            }
            foreach (MemberInfo mi in mis)
                if (mi != null)
                    result.Add(mi);
            return result;
        }

    }   // End of TypeRecord


    private class MethodRec {
        internal MethodBase m;
        internal ParameterInfo[] pia;
        internal MethodRec(MethodBase m, ParameterInfo[] pia) {
            this.m = m;
            this.pia = pia;
        }
    }


    /***********************************  PointerArgumentManager Class  *****************************************/

    // PointerArgumentManager is used to manage pointer params to a method. .NET/Link allows you to pass a value, say an integer,
    // to a pointer param slot, like int*. It also allows you to pass a vector to a pointer param. The idea is that for, say,
    // an int*, we create an array that holds this lone value, GCHandle it to pin it, then use UnsafeAddrOfPinnedArrayElement()
    // to return the address of the value. We replace the arg to be passed in to the method with this new pointer. Then after
    // the method is called we can Free the GCHandle and extract the value, which may have been modified during the call.
    // PointerArgumentManager is the class that manages these tasks for us.
    private class PointerArgumentManager {

        private bool[] wasAlreadyArray;
        private Array[] valueHolderArrays;
        private GCHandle[] gcHandles;

        internal PointerArgumentManager(int argc) {

            gcHandles = new GCHandle[argc];
            valueHolderArrays = new Array[argc];
            wasAlreadyArray = new bool[argc];
        }

        internal IntPtr add(int indexInArgArray, object arg, ParameterInfo pi) {

            Type argType = arg.GetType();
            Type paramType = pi.ParameterType;
            Type pointerType = paramType.GetElementType();
            Array valueHolder;
            if (argType.IsArray) {
                // If the argument was already an array (e.g., we passed a list of integers from M to an int* parameter),
                // we don't need to allocate a new array like in the case of a single value. But we do need to allocate a
                // new array if the array being passed in arrives here as an array of the wrong type (e.g., int[] but the param
                // is a short*).
                wasAlreadyArray[indexInArgArray] = true;
                Type argArrayElementType = argType.GetElementType();
                Array a = (Array) arg;
                if (argArrayElementType != pointerType) {
                    // Create a new array of the corect type and copy the values in (with conversion).
                    int len = a.GetLength(0);
                    valueHolder = Array.CreateInstance(pointerType, len);
                    for (int i = 0; i < len; i++)
                        valueHolder.SetValue(Convert.ChangeType(a.GetValue(i), pointerType), i);
                } else {
                    valueHolder = (Array) arg;
                }
            } else {
                valueHolder = Array.CreateInstance(pointerType, 1);
                valueHolder.SetValue(Convert.ChangeType(arg, pointerType), 0);
            }
            valueHolderArrays[indexInArgArray] = valueHolder;
            gcHandles[indexInArgArray] = GCHandle.Alloc(valueHolder, GCHandleType.Pinned);
            return Marshal.UnsafeAddrOfPinnedArrayElement(valueHolder, 0);
        }

        internal bool wasPointerArg(int index) {
            return valueHolderArrays[index] != null;
        }

        internal object getValue(int index) {
            Array valueHolder = valueHolderArrays[index];
            return wasAlreadyArray[index] ? valueHolder : valueHolder.GetValue(0);
        }

        internal void release() {
            for (int i = 0; i < gcHandles.Length; i++)
                if (wasPointerArg(i))
                    gcHandles[i].Free();
        }
    }  // End of PointerArgumentManager
 

    /***********************************  InstanceCollection Class  *****************************************/

    // This is the class that holds the set of objects that are referenced in Mathematica. When objects are
    // sent to Mathematica by reference, they get put in here; when ReleaseObject is called in Mathematica,
    // they get removed from here.

    private class InstanceCollection {

        private Hashtable table;
        
        internal InstanceCollection() {
            table = new Hashtable(541);  // Increase the default size. 541 == 100th prime number.
        }
        
        // Return the long code that is object's key.
        // Returns 0 for "not there" and non-zero to give the key. Needless to say, we must
        // never let a key be 0 (this is handled elsewhere).
        internal ulong keyOf(object obj) {

            uint hash = (uint) getHashCode(obj);
            Bucket b = (Bucket) table[hash];
            if (b == null) {
                return 0;
            } else {
                uint withinBucketKey = b.keyOf(obj);
                return withinBucketKey == 0 ? 0 : (((ulong) hash) << 24) | withinBucketKey;
            }
        }

        internal object get(ulong key) {

            if (key == 0)
                return null;
            uint keyInt = (uint) (key >> 24); // The object's hashcode (made positive)
            Bucket b = (Bucket) table[keyInt];
            uint withinBucketKey = (uint) (key & 0x00FFFFFF);
            return b.get(withinBucketKey);
        }

        // Returns the key. Only call this if you know object is not already in there. Otherwise you'll
        // end up with the same object stored more than once. This is not a serious problem, however.
        internal ulong put(object obj, string alias) {

            uint hash = (uint) getHashCode(obj);
            Bucket b = (Bucket) table[hash];
            if (b == null) {
                // No one has yet used this hashcode.
                b = new Bucket();
                table.Add(hash, b);
            }
            // withinBucketKey can never be 0, so no overall key can be 0.
            uint withinBucketKey = (uint) b.put(obj, alias);
            return (((ulong) hash) << 24) | withinBucketKey;
        }

        // Adds a new type alias to the set of aliases recorded for the given object. Returns true if the alias
        // is in fact new; false otherwise (in which case the function does nothing).
        // put() adds a new object to the collection; addAlias() is for an existing object getting a new alias.
        internal bool addAlias(ulong key, string alias) {

            uint keyInt = (uint) (key >> 24); // The object's hashcode (made positive)
            Bucket b = (Bucket) table[keyInt];
            // b is never null because the caller has already determined that the object for this key is in the collection.
            System.Diagnostics.Debug.Assert(b != null);
            uint withinBucketKey = (uint) (key & 0x00FFFFFF);
            return b.addAlias(withinBucketKey, alias);
        }

        internal void remove(ulong key) {
            
            // Safe to call (no exception thrown) if key is not in hashtable.
            uint outerKey = (uint) (key >> 24); // The object's hashcode (made positive)
            Bucket b = (Bucket) table[outerKey];
            if (b != null) {
                if (b.size() == 1) {
                    // Remove the whole bucket.
                    table.Remove(outerKey);
                } else {
                    // Just remove the obj from its bucket.
                    uint withinBucketKey = (uint) (key & 0x00FFFFFF);
                    b.remove(withinBucketKey);
                }
            }
        }

        internal int size() {

            ICollection buckets = table.Values;
            int count = 0;
            foreach (Bucket b in buckets)
                count += b.size();
            return count;
        }
        

        // Provide an enumerator so that we can iterate through with foreach. This facility is only used for
        // peekObjects().

        public InstanceCollectionEnumerator GetEnumerator() {
            return new InstanceCollectionEnumerator(table);
        }

        // This enumerates the M symbols given to the objects in the collection.
        internal class InstanceCollectionEnumerator {

            Hashtable bucketTable;
            IEnumerator topLevelEnumerator, withinBucketEnumerator;

            internal InstanceCollectionEnumerator(Hashtable bucketTable) {
                this.bucketTable = bucketTable;
            }

            public bool MoveNext() {

                if (topLevelEnumerator == null) {
                    topLevelEnumerator = bucketTable.GetEnumerator();
                    if (!topLevelEnumerator.MoveNext())
                        return false;
                }
                if (withinBucketEnumerator == null) {
                    withinBucketEnumerator = ((Bucket) ((DictionaryEntry) topLevelEnumerator.Current).Value).GetEnumerator();
                }
                if (withinBucketEnumerator.MoveNext()) {
                    return true;
                } else if (topLevelEnumerator.MoveNext()) {
                    withinBucketEnumerator = ((Bucket) ((DictionaryEntry) topLevelEnumerator.Current).Value).GetEnumerator();
                    return withinBucketEnumerator.MoveNext();
                } else {
                    return false;
                }
            }

            public object Current {
                get {
                    uint outerKey = (uint) ((DictionaryEntry) topLevelEnumerator.Current).Key;
                    uint withinBucketKey = (uint) ((DictionaryEntry) withinBucketEnumerator.Current).Key;
                    ulong key = (((ulong) outerKey) << 24) | withinBucketKey;
                    Bucket.BucketRec br = (Bucket.BucketRec) ((DictionaryEntry) withinBucketEnumerator.Current).Value;
                    object obj = br.obj;
                    // The object will appear only once no matter how many aliases it has, so we pick the first
                    // alias it was entered under.
                    return mmaSymbolFromKey(key, br.aliases[0]);
                }
            }
        }

        
        // Bit of a hack. We want to avoid calling the Expr class hashCode() method, as it can be expensive.
        // Instead we call a method for that class that amounts to super.hashCode(). This is perfectly legit--
        // we don't have any reason to prefer hashCode() over any other method that provides a reasonably
        // consistent int value from an object. In fact, for Expr, since hashCode() comes from the Expr's value,
        // it is possible to generate many Exprs that have the same hash, hurting the performance of keyOf()
        // if these objects are returned to Mathematica. This is another reason it is better to use
        // super.hashCode() for the Expr class.
        private static int getHashCode(object obj) {
            return obj is Expr ? ((Expr) obj).inheritedHashCode() : obj.GetHashCode();
        }
        
    }


    /****************************************  Bucket class  *******************************************/

    // Buckets are hashtables that use a key that is just an index count, ever growing. We have already used the
    // object's hashCode() value as the key in the InstanceCollection hashtable, so there is no other way to get
    // information out of the objects themselves to use as a key. The Integer within-bucket keys are stored as
    // 24-bit numbers, so if a single bucket ever gets more than 2^24 elements, then repeat
    // indices will occur and problems. Not a big concern, though, as 2^24 object references in Mathematica will
    // consume about 50 Gb. Also, object lookup is slow (meaning the keyOf operation, which hunts based on the object
    // itself, not its key), being a linear search, so if a bucket gets a very large number of elements things will
    // be very slow for that reason as well. This type of search is used when objects are sent to M, not by method calls.

    private class Bucket {

        private Hashtable table;
        private uint nextKey;
        private const uint largestKey = (1 << 24) - 1;  // Largest number that can be represented in 24 bits.

        internal Bucket() {
            // Collisions are unlikely, so use a small initial size.
            table = new Hashtable(17, 1.0F);
            // Start at 1. This is just to ensure that no overall key can ever be 0, even if a hashcode is 0.
            nextKey = 1;
        }

        // Returns int code that is key within this bucket.
        internal uint put(object obj, string alias) {

            uint withinBucketKey = nextKey++;
            if (nextKey > largestKey)
                nextKey = 1;  // Start back at 1.
            table.Add(withinBucketKey, new BucketRec(obj, withinBucketKey, alias));
            return withinBucketKey;
        }

        // Adds a new type alias to the set of aliases recorded for the given object. Returns true if the alias
        // is in fact new; false otherwise (in which case the function does nothing).
        // put() adds a new object to the bucket; addAlias() is for an existing object getting a new alias.
        internal bool addAlias(uint withinBucketKey, string alias) {

            BucketRec r = (BucketRec) table[withinBucketKey];
            return r.addAlias(alias);
        }

        internal object get(uint withinBucketKey) {

            BucketRec r = (BucketRec) table[withinBucketKey];
            return r != null ? r.obj : null;
        }
        
        internal void remove(uint withinBucketKey) {
            table.Remove(withinBucketKey);
        }
        
        // Returns Integer code that is key within this bucket.
        // Returns 0 for "not there". These numbers will not exceed the 24-bit
        // limit expected of them by the calling code. This method here is the bottleneck
        // in the case where buckets get a very large number of objects (say thousands).
        internal uint keyOf(object obj) {

            ICollection values = table.Values;
            foreach (BucketRec br in values) {
                object storedObj = br.obj;
                if (Object.ReferenceEquals(storedObj, obj) || br.compareValuesOnly && obj.Equals(storedObj))
                    return br.key;
            }
            return 0;
        }

        internal int size() {
            return table.Count;
        }

        internal IEnumerator GetEnumerator() {
            return table.GetEnumerator();
        }

        // This class is the element within the Bucket hashtable. We associate the object and its key so that
        // we can improve performance of reverse lookups (get key from object, done in keyOf()).
        internal class BucketRec {
            
            // When a boxed value type is passed to a byref value type param slot in Invoke() (e.g., a boxed int is sent
            // to an out int slot), what the called method gets is the address of the primitive data itself, not the
            // address of the boxed object. When it assigns to the byref param, it copies over the actual raw data.
            // This means that a boxed primitive is not immutable like, say, an instance of the Java Integer class is.
            // Here is an example of this behavior:
            //     object o1 = 0;
            //     object o2 = o1;
            //     meth.Invoke(new object[]{o1});  /* meth takes an out or ref int and assigns 42 to it. */
            // After this, not only is o1 set to 42, but so is o2! There is no way to get behavior like this in a normal
            // (non-reflective) call, because you cannot pass an object to an out int slot--you must create an int variable
            // and pass it instead, and the int variable is a new copy of the internal int value of the boxed object.
            // The problem with this is that we want to share object references sent to Mathematica whenever possible, because
            // creating a new NET object in M is comparatively expensive. This is particularly important with enums, as they
            // are always sent as references (primitives are usually sent by value, so it isn't such a big deal for them).
            // Every time a particular enum value is sent to M, we want to reuse the same reference in the InstanceCollection,
            // but we cannot do that unless the objects are immutable, and byref passing via Invoke() opens a loophole for
            // mutability. The fix is to simply deep-copy boxed primitives and enums both when they are stored in, and extracted from,
            // the InstanceCollection. This deep copying is encapsulated entirely within this class.

            object o;

            internal uint key;
            internal bool compareValuesOnly;
            internal string[] aliases;
            
            internal BucketRec(object obj, uint key, string alias) {

                this.key = key;
                this.aliases = new string[]{alias};
                Type t = obj.GetType();
                // These conversions are just tricks to create a new object of the same type but that boxes a new copy
                // of the internal value.
                if (Utils.IsTrulyPrimitive(t)) {
                    compareValuesOnly = true;
                    o = Convert.ChangeType(obj, t);
                } else if (t.IsEnum) {
                    compareValuesOnly = true;
                    o = Enum.ToObject(t, obj);
                } else {
                    o = obj;
                }
            }

            internal object obj {
                get {
                    if (compareValuesOnly) {
                        Type t = o.GetType();
                        if (Utils.IsTrulyPrimitive(t))
                            return Convert.ChangeType(o, t);
                        else
                            // Will be an Enum.
                            return Enum.ToObject(t, o);
                    } else
                        return o;
                }
            }

            // Returns true if the alias is in fact new; false otherwise (in which case the function does nothing).
            internal bool addAlias(string alias) {

                foreach (string a in aliases)
                    if (a == alias)
                        return false;

                string[] newAliases = new string[aliases.Length + 1];
                aliases.CopyTo(newAliases, 0);
                newAliases[newAliases.Length - 1] = alias;
                aliases = newAliases;
                return true;
            }
        }

    }  // End of InstanceCollection

}  // End of ObjectHandler


internal class OutParamRecord {
    internal int argPosition;
    internal object val;
    internal OutParamRecord(int argPosition, object val) { this.argPosition = argPosition; this.val = val; }
}


}
