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
using Wolfram.NETLink.Internal.COM;

namespace Wolfram.NETLink.Internal {

/// <summary>
/// This class knows how to deal with CallPackets, which are requests to invoke .NET/Link specific functionality,
/// including all calls from Mathematica into .NET.
/// </summary>
/// <remarks>
/// An IKernelLink implementation will typically hold an instance of this class and call its handleCallPacket
/// method when a CallPacket arrives. An instance of this class is shared between the main link and UI link,
/// so no state can go into here that is link-specific.
/// </remarks>
///
internal class CallPacketHandler {

    private IMathLink feServerLink;
    private ObjectHandler objectHandler;


    internal CallPacketHandler(ObjectHandler objectHandler) {
        this.objectHandler = objectHandler;
    }


    // This is used here and up in KernelLinkImpl, but it needs to be stored here because it is part of the state
    // that needs to be shared between the main link and ui link.
    public IMathLink FEServerLink {
        get { return feServerLink; }
        set { feServerLink = value; }
    }


    /***************************************  handleCallPacket  *****************************************/

    internal void handleCallPacket(KernelLinkImpl ml) {

        int index = 0;

        /* Strategy for exception handling here:
            1) catch ALL here, throw nothing.
            2) each function handles its own exceptions before return link is dirtied.
            3) each function minimizes the possibility of exceptions being thrown once
               process of putting result begins. If an exception occurs after this point,
               they throw to let it be caught by handler in this function. The result of
               such exceptions will either be invisible in M (a complete expression was
               already sent), or user will see $Aborted (if nothing or partial expr was sent).
               These are not very desirable, hence commandment to minimize this possibility.

            These rules may not apply to callNET, which is a special case.
        */

        // At this point, a CallPacket has been opened.
        try {
            index = ml.GetInteger();
            ml.CheckFunction("List");
        } catch (MathLinkException e) {
            handleCleanException(ml, e);
            return;
        }

        // Reset lastExceptionDuringCallPacketHandling unless this callpacket is a request to get its value.
        if (index != Install.GETEXCEPTION)
            ml.LastExceptionDuringCallPacketHandling = null;

        try {
            StdLink.setup(ml);
            ml.WasInterrupted = false;
            switch (index) {
                // The indices here are from the Install class. They mimic the standard function indices
                // in installable C programs that are established during the Install call.
                // Every one of these functions is responsible for sending a result back on the link.
                case Install.CALL:                    call(ml);                 break;
                case Install.LOADASSEMBLY:            loadAssembly(ml);         break;
                case Install.LOADASSEMBLYFROMDIR:     loadAssemblyFromDir(ml);  break;
                case Install.LOADTYPE1:               loadType1(ml);            break;
                case Install.LOADTYPE2:               loadType2(ml);            break;
                case Install.LOADEXISTINGTYPE:        loadExistingType(ml);     break;
                case Install.GETASSEMBLYOBJ:          getAssemblyObject(ml);    break;
                case Install.GETTYPEOBJ:              getTypeObject(ml);        break;
                case Install.RELEASEOBJECT:           releaseInstance(ml);      break;
                case Install.MAKEOBJECT:              makeObject(ml);           break;
                case Install.VAL:                     val(ml);                  break;
                case Install.SETCOMPLEX:              setComplex(ml);           break;
                case Install.SAMEQ:                   sameObjectQ(ml);          break;
                case Install.INSTANCEOF:              instanceOf(ml);           break;
                case Install.CAST:                    cast(ml);                 break;

                case Install.PEEKTYPES:               peekTypes(ml);            break;
                case Install.PEEKOBJECTS:             peekObjects(ml);          break;
                case Install.PEEKASSEMBLIES:          peekAssemblies(ml);       break;
                case Install.REFLECTTYPE:             reflectType(ml);          break;
                case Install.REFLECTASM:              reflectAssembly(ml);      break;

                case Install.CREATEDELEGATE:          createDelegate(ml);       break;
                case Install.DEFINEDELEGATE:          defineDelegate(ml);       break;
                case Install.DLGTYPENAME:             delegateTypeName(ml);     break;
                case Install.ADDHANDLER:              addEventHandler(ml);      break;
                case Install.REMOVEHANDLER:           removeEventHandler(ml);   break;

                case Install.CREATEDLL1:              createDLL1(ml);           break;
                case Install.CREATEDLL2:              createDLL2(ml);           break;

                case Install.MODAL:                   doModal(ml);              break;
                case Install.SHOW:                    showForm(ml);             break;
                case Install.SHAREKERNEL:             doShareKernel(ml);        break;
                case Install.ALLOWUICOMPS:            allowUIComps(ml);         break;
                case Install.UILINK:                  uiLink(ml);               break;

                case Install.ISCOMPROP:               isCOMProp(ml);            break;
                case Install.CREATECOM:               createCOM(ml);            break;
                case Install.GETACTIVECOM:            getActiveCOM(ml);         break;
                case Install.RELEASECOM:              releaseCOM(ml);           break;
                case Install.LOADTYPELIBRARY:         loadTypeLibrary(ml);      break;

                case Install.GETEXCEPTION:            getException(ml);         break;

                case Install.CONNECTTOFE:             connectToFEServer(ml);    break;
                case Install.DISCONNECTTOFE:          disconnectToFEServer(ml); break;

                // For testing only.
                case Install.NOOP:                    ml.Put(43);             break;
                case Install.NOOP2:                   noop2(ml);                break;
                default: break;
            }
        } catch (Exception e) {
            // All functions in switch above must handle internally exceptions that occur before
            // anything is sent on link. This catch here is for exceptions thrown when link is in unknown
            // state (i.e., unknown whether partial or full expr has been sent.)
            ml.LastExceptionDuringCallPacketHandling = e;
        } finally {
            StdLink.remove();
            ml.ClearError();
            ml.NewPacket();
            ml.EndPacket();
            ml.Flush();
        }
    }


    /************************  Private methods to handle the various types of calls  *************************/

    private void loadAssembly(KernelLinkImpl ml) {
        
        Assembly asm = null;
        bool suppressErrors = false;
        try {
            // On link is assemblyNameOrPath_String, suppressErrors:(True | False)
            string assemblyNameOrPath = ml.GetString();
            suppressErrors = ml.GetBoolean();
            ml.NewPacket();
            asm = TypeLoader.LoadAssembly(assemblyNameOrPath);
        } catch (Exception e) {
            if (!((e is BadImageFormatException || e is System.IO.FileLoadException) && suppressErrors)) {
                handleCleanException(ml, e);
                return;
            }
        }
        if (asm != null) {
            ml.PutFunction("List", 2);
            ml.Put(asm.GetName().Name);
            ml.Put(asm.FullName);
        } else {
            ml.PutSymbol("$Failed");
        }
    }


    private void loadAssemblyFromDir(KernelLinkImpl ml) {
        
        Assembly asm = null;
        try {
            // On link is assemblyName_String, dir_String
            string assemblyName = ml.GetString();
            string dir = ml.GetString();
            ml.NewPacket();
            asm = TypeLoader.LoadAssembly(assemblyName, dir);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        if (asm != null) {
            ml.PutFunction("List", 2);
            ml.Put(asm.GetName().Name);
            ml.Put(asm.FullName);
        } else {
            ml.PutSymbol("$Failed");
        }
    }


    private void loadType1(KernelLinkImpl ml) {
        
        try {
            // On link is type_String, assemblyName_String. Type name can be assembly-qualified with varying
            // degrees of explicitness. If it has assembly info appended, assemblyName should be "".
            string typeName = ml.GetString();
            string assemblyName = ml.GetString();
            ml.NewPacket();
            Type t = TypeLoader.GetType(typeName, assemblyName, true);
            objectHandler.loadType(ml, t);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
    }


    private void loadType2(KernelLinkImpl ml) {
        
        try {
            // On link is type_String, assemblyObj_NETObject. Type name can be assembly-qualified with varying
            // degrees of explicitness. If it has assembly info appended, assemblyName should be "".
            string typeName = ml.GetString();
            Assembly assemblyObj = (Assembly) ml.GetObject();
            ml.NewPacket();
            Type t = TypeLoader.GetType(typeName, assemblyObj, true);
            objectHandler.loadType(ml, t);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
    }


    private void loadExistingType(KernelLinkImpl ml) {
        
        try {
            // On link is Type object (this is verified by M code).
            Type t = (Type) ml.GetObject();
            ml.NewPacket();
            objectHandler.loadType(ml, t);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
    }


    private void call(KernelLinkImpl ml) {

        object result = null;
        string typeName;
        string objSymbol;
        int callType;
        bool isByRef;
        string mmaMemberName;
        int[] argTypes;

        try {
            // On link is typeName_String, obj_Symbol, callType_Integer, isByRef_,
            //              fullMethNameAsMmaContext_String, argCount_Integer, argTypes:{___Integer}, args___
            typeName = ml.GetString();
            objSymbol = ml.GetSymbol();
            callType = ml.GetInteger();
            isByRef = ml.GetBoolean();
            mmaMemberName = ml.GetString();  // Full member name including possibly Mma context prefix.
            int numArgs = ml.GetInteger();
            argTypes = new int[numArgs];
            for (int i = 0; i < numArgs; i++)
                argTypes[i] = ml.GetInteger();
          } catch (Exception e) {
            // Exception here probably means that M-side code is broken.
            handleCleanException(ml, e);
            return;
        }
         
        // Have now drained everything from the link except the args themselves. The call() method will read args from the link.

        bool wasManual = ml.IsManual;  // wasManual keeps "manual" state correct for nested calls to .NET.
        try {
            ml.IsManual = false;
            result = objectHandler.call(ml, typeName, objSymbol, callType, mmaMemberName, argTypes, out var outParams);
            // Ensure all args are thrown away if call() failed to read them all:
            ml.NewPacket();
            if (ml.IsManual) {
                // This will force M to get $Aborted if the user forgot to put a complete expression before returning:
                ml.EndPacket();
                sendOutParams(ml, outParams);
                // This will satisfy the return read for exception info.
                ml.PutSymbol("Null");
            } else if (ml.WasInterrupted) {
                ml.PutFunction("Abort", 0);
            } else if (isByRef) {
                sendOutParams(ml, outParams);
                // Ctors are always ByRef.
                ml.PutReference(result);
            } else {
                sendOutParams(ml, outParams);
                ml.Put(result);
            }
        } catch (Exception e) {
            // It is the responsibility of ObjectHandler to catch ALL low-level exceptions and translate to CallNETException.
            // Thus, the vast majority of exceptions caught here will be CallNETException. The others might be
            // MathLinkExceptions during the sending of the result.
            if (ml.IsManual) {
                // This is set in handleCleanException() in all other branches.
                ml.LastExceptionDuringCallPacketHandling = e;
                ml.ClearError();
                // The EndPacket() and Flush() are crucial, as they trigger $Aborted to be sent if the user had already sent
                // a partial expression in their manual return. This separates the cases of an exception before a complete expr was
                // sent and an exception afterwards.
                ml.EndPacket();
                ml.Flush();
                ml.PutFunction(Install.MMA_MANUALEXCEPTION, 1);
                if (e is CallNETException)
                    ((CallNETException) e).writeToLink(ml);
                else
                    ml.Put(e.ToString());
            } else {
                // Guaranteed to be "clean", meaning that nothing has been sent on the link yet.
                handleCleanException(ml, e);
            }
        } finally {
            ml.IsManual = wasManual;
        }
    }


    private void getAssemblyObject(KernelLinkImpl ml) {

        // Link will have asmFullName_String.
        Assembly asm = null;
        try {
            string asmFullName = ml.GetString();
            ml.NewPacket();
            // Note that it will already have been loaded, despite calling LoadAssembly() here.
            asm = TypeLoader.LoadAssembly(asmFullName);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutReference(asm);
    }


    private void getTypeObject(KernelLinkImpl ml) {

        // Link will have aqName_String.
        Type t = null;
        try {
            string aqName = ml.GetString();
            ml.NewPacket();
            t = TypeLoader.GetType(aqName, true);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutReference(t);
    }


    private void releaseInstance(KernelLinkImpl ml) {

        // Link will have a list of one or more symbols. None can be "Null".
        try {
            string[] syms = ml.GetStringArray();
            ml.NewPacket();
            objectHandler.releaseInstance(syms);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutSymbol("Null");
    }

    
    private void makeObject(KernelLinkImpl ml) {

        object result = null;

        // Link has: typeName_String, argType_Integer, val_.
        try {
            string typeName = ml.GetString();
            int argType = ml.GetInteger();
            Type t = null;
            if (typeName.IndexOf('.') == -1) {
                // If type passed has no . in it, prepend System. This allows M users to specify "Int32" instead of "System.Int32".
                t = TypeLoader.GetType("System." + typeName, true);
            } else {
                t = TypeLoader.GetType(typeName, true);
            }
            try {
                result = Utils.readArgAs(ml, argType, t);
            } catch (Exception) {
                ml.ClearError();
                // TODO: put in some mechanism to get meaningful strings out of the exceptions (like the enum message).
                throw new ArgumentException("Expression cannot be read as the requested type.");
            }
            ml.NewPacket();
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }

        ml.PutReference(result);
    }


    private void createDelegate(KernelLinkImpl ml) {
    
        object dlgt;
        try {
            // On link is typeName_String, mFunc_String, sendTheseArgs_Integer, callsUnshare:(True | False), wrapInNETBlock:(True | False)
            string typeName = ml.GetString();
            string mFunc = ml.GetString();
            int argsToSend = ml.GetInteger();
            bool callsUnshare = ml.GetBoolean();
            bool wrapInNETBlock = ml.GetBoolean();
            ml.NewPacket();
            // This might throw an appropriate error if the type cannot be found.
            Type delegateType = TypeLoader.GetType(typeName, true);
            dlgt = Delegate.CreateDelegate(delegateType, DelegateHelper.createDynamicMethod(null, delegateType, mFunc, argsToSend, callsUnshare, wrapInNETBlock));
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutReference(dlgt);
    }


    private void val(KernelLinkImpl ml) {

        // Link will have an object ref.
        object obj, result;
        try {
            obj = ml.GetObject();
            ml.NewPacket();
            if (obj.GetType().IsEnum) {
                // This makes NETObjectToExpression convert Enum objects to their integer representation:
                result = Convert.ChangeType(obj, Enum.GetUnderlyingType(obj.GetType()));
            } else if (obj is System.Collections.ICollection && !(obj is Array)) {
                // This makes NETObjectToExpression convert ICollection objects to lists:
                object[] a = new object[((System.Collections.ICollection) obj).Count];
                ((System.Collections.ICollection) obj).CopyTo(a, 0);
                result = a;
            } else {
                result = obj;
            }
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(result);
    }


    private void setComplex(KernelLinkImpl ml) {

        bool result = true;
        // Link has: aqTypeName_String.
        try {
            string aqTypeName = ml.GetString();
            ml.NewPacket();
            Type t = TypeLoader.GetType(aqTypeName, true);
            ml.ComplexType = t;
        } catch (ArgumentException) {
            // This is the exception thrown by ComplexClassHandler if the class specified does not have the appropriate
            // members (e.g., Re, Im properties). Rather than propagate that exception to M, we will return False and
            // let M issue an error message specific to the SetComplexType[] function.
            result = false;
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(result);
    }

    
    private void sameObjectQ(KernelLinkImpl ml) {

        bool result;
        // Link has: obj1, obj2.
        try {
            object obj1 = ml.GetObject();
            object obj2 = ml.GetObject();
            ml.NewPacket();
            result = Object.ReferenceEquals(obj1, obj2);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(result);
    }

    
    private void instanceOf(KernelLinkImpl ml) {

        bool result;
        // Link has: obj, aqTypeName.
        try {
            object obj = ml.GetObject();
            string aqTypeName = ml.GetString();
            ml.NewPacket();
            Type t = TypeLoader.GetType(aqTypeName, true);
            result = t.IsInstanceOfType(obj);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(result);
    }

    
    private void cast(KernelLinkImpl ml) {

        // Link has: obj, aqTypeName.
        Type t;
        object obj;
        try {
            obj = ml.GetObject();
            string aqTypeName = ml.GetString();
            ml.NewPacket();
            t = TypeLoader.GetType(aqTypeName, true);
            if (!Utils.IsMono && System.Runtime.InteropServices.Marshal.IsComObject(obj)) {
                // Note how differently this is done for COM objects. We create a new
                // object and return it to M as its true runtime type (thus we set t to null).
                // For .NET objects in the branch below, we simply return the same object under
                // a different guise. Casting of COM objects is a very different process down inside
                // the .NET runtime.
                obj = COMUtilities.Cast(obj, t);
                t = null;
            } else {
                // Although PutReference() below will throw InvalidCastException, we want it to happen
                // here so that it goes through handleCleanException().
                if (!t.IsInstanceOfType(obj))
                    throw new InvalidCastException();
            }
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutReference(obj, t);
    }

    
    private void peekTypes(KernelLinkImpl ml) {

        objectHandler.peekTypes(ml);
    }


    private void peekObjects(KernelLinkImpl ml) {

        objectHandler.peekObjects(ml);
    }


    private void peekAssemblies(KernelLinkImpl ml) {

        objectHandler.peekAssemblies(ml);
    }


    private void reflectType(KernelLinkImpl ml) {

        // We create a loopback link and put everything on it, then transfer the whole pile at the end.
        // This means that we don't get "unexpected and of packet" errors if an exception is thrown by
        // one of the many methods we call in preparing the info. In other words, keep the main link
        // clean until we have all the info.
        ILoopbackLink loop = MathLinkFactory.CreateLoopbackLink();
        try {
            string aqTypeName = ml.GetString();
            ml.NewPacket();
            Type t = objectHandler.getType(aqTypeName);

            // There are 6 sublists: type info, ctor info, field info, prop info, method info, event info.
            loop.PutFunction("List", 6);
            // General type info
            loop.PutFunction("List", 10);
            // Want to send short name like Foo and fullName like Namespace.Foo. But for generic types, Name gives
            // just Foo`1, whereas we want to send Foo`1[genericParamType]. So for generics we use the technique
            // of truncating the namespace part from the beginning of fullName to get the short name.
            string fullName = ObjectHandler.fullTypeNameForMathematica(t);
            string shortName = (t.IsGenericType && fullName.IndexOf(t.Name) > 0) ? fullName.Substring(fullName.IndexOf(t.Name)) : t.Name;
            loop.Put(shortName);
            loop.Put(fullName);
            System.Collections.ArrayList parentTypes = new System.Collections.ArrayList();
            Type parent = t.BaseType;
            while (parent != null) {
                parentTypes.Add(ObjectHandler.fullTypeNameForMathematica(parent));
                parent = parent.BaseType;
            }
            loop.Put(parentTypes.ToArray());
            Type[] intfs = t.GetInterfaces();
            loop.PutFunction("List", intfs.Length);
            foreach (Type intfType in intfs)
                loop.Put(ObjectHandler.fullTypeNameForMathematica(intfType));
            loop.Put(t.IsValueType);
            loop.Put(t.IsEnum);
            loop.Put(t.IsSubclassOf(typeof(Delegate)));
            loop.Put(t.IsInterface);
            loop.Put(t.AssemblyQualifiedName);
            try {
                // This can throw for an assembly generated in memory (e.g., by LoadCOMTypeLibrary[]).
                loop.Put(t.Assembly.Location);
            } catch (Exception) {
                loop.Put("");
            }
            // Constructors
            ConstructorInfo[] ctors = objectHandler.getConstructors(aqTypeName);
            loop.PutFunction("List", ctors.Length);
            foreach (ConstructorInfo ci in ctors) {
                putParameterInfo(loop, ci.GetParameters());
            }
            // Fields
            FieldInfo[] fields = objectHandler.getFields(aqTypeName);
            loop.PutFunction("List", fields.Length);
            foreach (FieldInfo fi in fields) {
                if (t.IsEnum && fi.IsSpecialName) {
                    // For enums there is a special value__ field that we don't want to show.
                    loop.PutFunction("Sequence", 0);
                } else {
                    loop.PutFunction("List", 6);
                    loop.Put(fi.DeclaringType != t);
                    loop.Put(fi.IsStatic);
                    loop.Put(fi.IsLiteral);
                    loop.Put(fi.IsInitOnly);
                    loop.Put(ObjectHandler.fullTypeNameForMathematica(fi.FieldType));
                    loop.Put(fi.Name);
                }
            }
            // Properties
            PropertyInfo[] props = objectHandler.getProperties(aqTypeName);
            loop.PutFunction("List", props.Length);
            foreach (PropertyInfo pi in props) {
                loop.PutFunction("List", 10);
                bool isStatic = false;
                bool isVirtual = false;
                bool isOverride = false;
                bool isAbstract = false;
                bool canRead = false;
                bool canWrite = false;
                MethodInfo getter = pi.GetGetMethod();
                MethodInfo setter = pi.GetSetMethod();
                // See docs for IsFinal property for why it is necessary to remove methods marked as final from the virtual category.
                if (getter != null) {
                    canRead = true;
                    if (getter.IsStatic)
                        isStatic = true;
                    if (getter.IsVirtual && !getter.IsFinal)
                        isVirtual = true;
                    if (getter.IsVirtual && getter.DeclaringType == t &&
                            getter.GetBaseDefinition().DeclaringType != t && !getter.GetBaseDefinition().DeclaringType.IsInterface)
                        isOverride = true;
                    if (getter.IsAbstract)
                        isAbstract = true;
                }
                if (setter != null) {
                    canWrite = true;
                    if (setter.IsStatic)
                        isStatic = true;
                    if (setter.IsVirtual && !setter.IsFinal)
                        isVirtual = true;
                    if (setter.IsVirtual && setter.DeclaringType == t &&
                            setter.GetBaseDefinition().DeclaringType != t && !setter.GetBaseDefinition().DeclaringType.IsInterface)
                        isOverride = true;
                    if (setter.IsAbstract)
                        isAbstract = true;
                }
                loop.Put(pi.DeclaringType != t);
                loop.Put(isStatic);
                loop.Put(isVirtual);
                loop.Put(isOverride);
                loop.Put(isAbstract);
                loop.Put(canRead);
                loop.Put(canWrite);
                loop.Put(ObjectHandler.fullTypeNameForMathematica(pi.PropertyType));
                loop.Put(pi.Name);
                ParameterInfo[] pis = null;
                bool paramsAreFromSetter = false;
                if (getter != null)
                    pis = getter.GetParameters();
                else if (setter != null) {
                    pis = setter.GetParameters();
                    paramsAreFromSetter = true;
                }
                if (pis == null || paramsAreFromSetter && pis.Length == 1) {
                    loop.PutFunction("List", 0);
                } else {
                    if (paramsAreFromSetter) {
                        // If params list comes from setter method, then the last param in the list is the value param
                        // (the value the prop is being set to). We don't want to include this in the list.
                        ParameterInfo[] newPis = new ParameterInfo[pis.Length - 1];
                        Array.Copy(pis, 0, newPis, 0, newPis.Length);
                        pis = newPis;
                    }
                    putParameterInfo(loop, pis);
                }
            }
            // Methods
            MethodInfo[] methods = objectHandler.getMethods(aqTypeName);
            loop.PutFunction("List", methods.Length);
            foreach (MethodInfo mi in methods) {
                loop.PutFunction("List", 8);
                loop.Put(mi.DeclaringType != t);
                loop.Put(mi.IsStatic);
                // isVirtual
                loop.Put(mi.IsVirtual && !mi.IsFinal);
                // isOverride
                loop.Put(mi.IsVirtual && mi.DeclaringType == t &&
                        mi.GetBaseDefinition().DeclaringType != t && !mi.GetBaseDefinition().DeclaringType.IsInterface);
                loop.Put(mi.IsAbstract);
                // Not sure how to decide on the "new" keyword, so I will not bother with it.
                loop.Put(ObjectHandler.fullTypeNameForMathematica(mi.ReturnType));
                loop.Put(mi.Name);
                putParameterInfo(loop, mi.GetParameters());
            }
            // Events
            EventInfo[] events = objectHandler.getEvents(aqTypeName);
            loop.PutFunction("List", events.Length);
            foreach (EventInfo ei in events) {
                loop.PutFunction("List", 9);
                MethodInfo mi = ei.GetAddMethod();
                loop.Put(ei.DeclaringType != t);
                loop.Put(mi.IsStatic);
                // isVirtual
                loop.Put(mi.IsVirtual && !mi.IsFinal);
                // isOverride
                loop.Put(mi.IsVirtual && mi.DeclaringType == t &&
                        mi.GetBaseDefinition().DeclaringType != t && !mi.GetBaseDefinition().DeclaringType.IsInterface);
                // isAbstract
                loop.Put(mi.IsAbstract);
                loop.Put(ObjectHandler.fullTypeNameForMathematica(ei.EventHandlerType));
                loop.Put(ei.Name);
                // We want the signature of the delegate. We obtain this by looking at the signature of the
                // Invoke() method of the delegate type.
                MethodInfo invokeMeth = ei.EventHandlerType.GetMethod("Invoke");
                loop.Put(ObjectHandler.fullTypeNameForMathematica(invokeMeth.ReturnType));
                putParameterInfo(loop, invokeMeth.GetParameters());
            }

            // Now send the big expression we have built up on the loopback link.
            ml.TransferExpression(loop);
        } catch (Exception e) {
            handleCleanException(ml, e);
        } finally {
            loop.Close();
        }
    }


    private void reflectAssembly(KernelLinkImpl ml) {

        // We create a loopback link and put everything on it, then transfer the whole pile at the end.
        // This means that we don't get "unexpected and of packet" errors if an exception is thrown by
        // one of the many methods we call in preparing the info. In other words, keep the main link
        // clean until we have all the info.
        ILoopbackLink loop = MathLinkFactory.CreateLoopbackLink();
        try {
            // On link is: fullAsmName_String.
            string fullAsmName = ml.GetString();
            ml.NewPacket();
            // We call LoadAssembly, but it will already be loaded. This is really just a lookup.
            Assembly asm = TypeLoader.LoadAssembly(fullAsmName);
            Type[] types;
            try {
                types = asm.GetExportedTypes();
            } catch (NotSupportedException) {
                // We get here on a dynamically-created assembly such as from LoadCOMTypeLibrary.
                // Try an alternative way of getting all the public types.
                Type[] allTypes = asm.GetTypes();
                System.Collections.ArrayList publics = new System.Collections.ArrayList(allTypes.Length);
                foreach (Type t in allTypes) {
                    if (t.IsPublic)
                        publics.Add(t);
                }
                types = (Type[]) publics.ToArray(typeof(Type));
            }
            loop.PutFunction("List", types.Length + 3);
            loop.Put(asm.GetName().Name);
            loop.Put(asm.FullName);
            try {
                // This can throw for an assembly generated in memory (e.g., by LoadCOMTypeLibrary[]).
                loop.Put(asm.Location);
            } catch (Exception) {
                loop.Put("");
            }
            foreach (Type t in types) {
                loop.PutFunction("List", 6);
                loop.Put(ObjectHandler.fullTypeNameForMathematica(t));
                loop.Put(t.Namespace != null ? t.Namespace : "");
                loop.Put(t.IsValueType);
                loop.Put(t.IsEnum);
                loop.Put(t.IsSubclassOf(typeof(Delegate)));
                loop.Put(t.IsInterface);
            }
            // Now send the big expression we have built up on the loopback link.
            ml.TransferExpression(loop);
        } catch (Exception e) {
            handleCleanException(ml, e);
        } finally {
            loop.Close();
        }
    }


    private void defineDelegate(KernelLinkImpl ml) {

        string newDlgTypeName;
        try {
            // On link is: name_String, retTypeName_String, paramTypeNames:{___String}.
            string name = ml.GetString();
            string retTypeName = ml.GetString();
            string[] paramTypeNames = ml.GetStringArray();
            ml.NewPacket();
            newDlgTypeName = DelegateHelper.defineDelegate(name, retTypeName, paramTypeNames);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(newDlgTypeName);
    }


    private void delegateTypeName(KernelLinkImpl ml) {

        string dlgTypeName;
        try {
            // On link is: eventsObject, string aqTypeName_String, evtName_String.
            object eventsObject = ml.GetObject();
            string aqTypeName = ml.GetString();
            string evtName = ml.GetString();
            ml.NewPacket();
            dlgTypeName = EventHelper.getDelegateTypeName(eventsObject, aqTypeName, evtName);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(dlgTypeName);
    }


    private void addEventHandler(KernelLinkImpl ml) {

        Delegate dlg;
        try {
            // On link is: eventsObject, aqTypeName_String, evtName_String, delegate.
            object eventsObject = ml.GetObject();
            string aqTypeName = ml.GetString();
            string evtName = ml.GetString();
            dlg = (Delegate) ml.GetObject();
            ml.NewPacket();
            dlg = EventHelper.addHandler(eventsObject, aqTypeName, evtName, dlg);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutReference(dlg);
    }


    private void removeEventHandler(KernelLinkImpl ml) {

        try {
            // On link is: eventsObject, string aqTypeName_String, evtName_String, delegate.
            object eventsObject = ml.GetObject();
            string aqTypeName = ml.GetString();
            string evtName = ml.GetString();
            Delegate dlg = (Delegate) ml.GetObject();
            ml.NewPacket();
            EventHelper.removeHandler(eventsObject, aqTypeName, evtName, dlg);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutSymbol("Null");
    }


    private void createDLL1(KernelLinkImpl ml) {

        string newTypeName;
        try {
            // On link is: funcName_String, dllName_String, callConv_String, retTypeName_String,
            //                  argTypeNames:{___String}, areOutParams:{__T|F}, strFormat_String
            string funcName = ml.GetString();
            string dllName = ml.GetString();
            string callConv = ml.GetString();
            string retTypeName = ml.GetString();
            string[] argTypeNames = ml.GetStringArray();
            bool[] areOutParams = ml.GetBooleanArray();
            string strFormat = ml.GetString();
            ml.NewPacket();
            newTypeName = DLLHelper.CreateDLLCall(funcName, dllName, callConv, retTypeName, argTypeNames, areOutParams, strFormat);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(newTypeName);
    }


    private void createDLL2(KernelLinkImpl ml) {

        string[] result;
        try {
            // On link is: decl_String, refAsms:{___String}, lang_String
            string decl = ml.GetString();
            string[] refAsms = ml.GetStringArray();
            string lang = ml.GetString();
            ml.NewPacket();
            result = DLLHelper.CreateDLLCall(decl, refAsms, lang);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(result);
    }
    
    
    private void doModal(KernelLinkImpl ml) {

        bool enteringModal;
        System.Windows.Forms.Form form;
        try {
            enteringModal = ml.GetBoolean();
            form = ml.GetObject() as System.Windows.Forms.Form;
            ml.NewPacket();
            Reader.isInModalState = enteringModal;
            // It is required to do this here rather than as a separate step in the M code implementation of DoNETModal
            // because we must avoid sending anything to .NET after entering the modal state. If we try to call Show and
            // Activate from M after doModal(), and a Paint callback is wired up, we can get off-by-one problems. 
            if (enteringModal && form != null)
                activateWindow(form);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutSymbol("Null");
    }


    private void showForm(KernelLinkImpl ml) {

        System.Windows.Forms.Form form;
        try {
            form = ml.GetObject() as System.Windows.Forms.Form;
            ml.NewPacket();
            activateWindow(form);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutSymbol("Null");
    }


    private void doShareKernel(KernelLinkImpl ml) {
        
        bool startingSharing;
        try {
            startingSharing = ml.GetBoolean();
            ml.NewPacket();
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        Reader.shareKernel(startingSharing);
        ml.PutSymbol("Null");
    }
    

    private void allowUIComps(KernelLinkImpl ml) {
        
        bool allow;
        try {
            allow = ml.GetBoolean();
            ml.NewPacket();
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        Reader.allowUIComputations = allow;
        ml.PutSymbol("Null");
    }

    
    private void uiLink(KernelLinkImpl ml) {

        // Link will have the link name and protocol as strings.
        String linkName, protocol;
        try {
            linkName = ml.GetString();
            protocol = ml.GetString();
            ml.NewPacket();
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
		
        bool result = true;
        IKernelLink ui = null;
        try {
            ui = MathLinkFactory.CreateKernelLink("-linkname " + linkName + " -linkconnect -linkprotocol " + protocol);
            StdLink.UILink = ui;
            ((KernelLinkImpl) ui).copyStateFrom((KernelLinkImpl) ml);
        } catch (Exception) {
            if (ui != null)
                ui.Close();
            result = false;
        }
        ml.Put(result);
        ml.Flush();
        ui.Connect();
    }


    private void connectToFEServer(KernelLinkImpl ml) {

        bool result = false;

        try {
            string linkName = ml.GetString();
            ml.NewPacket();
            string mlArgs = "-linkmode connect -linkname " + linkName;
            FEServerLink = MathLinkFactory.CreateMathLink(mlArgs);
            // Do nothing if link open fails. Return value of "False" will be sufficient to indicate this,
            // although I cannot currently distinguish between "link open failed" and "problem during link setup".
            if (FEServerLink != null) {
                try {
                    FEServerLink.Connect();
                    FEServerLink.PutFunction("InputNamePacket", 1);
                    FEServerLink.Put("In[1]:=");
                    FEServerLink.Flush();
                    while (true) {
                        // Here we peel off the initialization that the FE sends to the kernel when it first starts up a link.
                        // We know that the first EnterTextPacket or EnterExpressionPacket is the content of the evaluating cell,
                        // so we are done.
                        string f = FEServerLink.GetFunction(out var ignore);
                        FEServerLink.NewPacket();
                        if (f == "EnterTextPacket" || f == "EnterExpressionPacket") {
                            result = true;
                            break;
                        } else if (f == "EvaluatePacket") {
                            FEServerLink.PutFunction("ReturnPacket", 1);
                            FEServerLink.PutSymbol("Null");
                        } else {
                            Debug.WriteLine("Unexpected packet during FE server setup: " + f);
                        }
                    }
                } catch (MathLinkException e) {
                    // These are exceptions dealing with the fe link, not the kernel link.
                    Debug.WriteLine("MathLinkException during FE server setup: " + e);
                    FEServerLink.Close();
                    FEServerLink = null;
                }
            }
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutFunction("ReturnPacket", 1);
        ml.Put(result);
        ml.EndPacket();
    }


    private void disconnectToFEServer(KernelLinkImpl ml) {

        FEServerLink.Close();
        FEServerLink = null;
        ml.PutFunction("ReturnPacket", 1);
        ml.PutSymbol("Null");
        ml.EndPacket();
    }


    private void isCOMProp(KernelLinkImpl ml) {

        // Link will have an object ref and a member name.
        object obj;
        string memberName;
        bool result;
        try {
            obj = ml.GetObject();
            memberName = ml.GetString();
            ml.NewPacket();
            result = COMUtilities.IsCOMProp(obj, memberName);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(result);
    }


    private void createCOM(KernelLinkImpl ml) {

        // Link will have a string that is a CLSID or ProgID.
        string clsIDOrProgID;
        object obj;
        try {
            clsIDOrProgID = ml.GetString();
            ml.NewPacket();
            obj = COMUtilities.createCOMObject(clsIDOrProgID);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutReference(obj);
    }


    private void getActiveCOM(KernelLinkImpl ml) {

        // Link will have a string that is a CLSID or ProgID.
        string clsIDOrProgID;
        object obj;
        try {
            clsIDOrProgID = ml.GetString();
            ml.NewPacket();
            obj = COMUtilities.getActiveCOMObject(clsIDOrProgID);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutReference(obj);
    }


    private void releaseCOM(KernelLinkImpl ml) {

        // Link will have an object ref.
        object obj;
        int result;
        try {
            obj = ml.GetObject();
            ml.NewPacket();
            result = COMUtilities.releaseCOMObject(obj);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(result);
    }


    private void loadTypeLibrary(KernelLinkImpl ml) {

        // Link will have: path to COM type lib, T/F for safeArrayAsSystemArray, string path to assem file to write.
        Assembly result;
        bool foundPIAInstead;
        try {
            string typeLibPath = ml.GetString();
            bool safeArrayAsArray = ml.GetBoolean();
            string assemFilePath = ml.GetString(); // Will be "" to mean don't create file.
            ml.NewPacket();
            result = COMTypeLibraryLoader.loadTypeLibrary(typeLibPath, safeArrayAsArray, assemFilePath, out foundPIAInstead);
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutFunction("List", 2);
        ml.PutReference(result);
        ml.Put(foundPIAInstead);
    }


    private void getException(KernelLinkImpl ml) {

        // Link will be empty.
        try {
            ml.NewPacket();
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.PutReference(ml.LastExceptionDuringCallPacketHandling);
    }


    // For testing only.
    private void noop2(KernelLinkImpl ml) {

Console.Error.WriteLine("in noop2, ");
        int argc = 0;
        try {
            argc = ml.GetInteger();
            for (int i = 0; i < argc; i++) {
                Utils.discardNext(ml);
            }
            ml.NewPacket();
        } catch (Exception e) {
            handleCleanException(ml, e);
            return;
        }
        ml.Put(argc);
    }


    /***************************************  Private utility methods  ***************************************/

    private void handleCleanException(KernelLinkImpl ml, Exception e) {

        ml.LastExceptionDuringCallPacketHandling = e;
        ml.ClearError();
        ml.NewPacket();
        if (ml.WasInterrupted) {
            ml.PutFunction("Abort", 0);
        } else {
            ml.PutFunction(Install.MMA_HANDLEEXCEPTION, 1);
            if (e is CallNETException) {
                ((CallNETException) e).writeToLink(ml);
            } else {
                // CR-LFs produce two newlines in the front end, so drop them.
                string msg = e.ToString().Replace("\r\n", "\n");
                // Assembly-loading exceptions might have a bunch of gibberish at the end in a "Fusion log" report.
                // Don't want users to see this.
                int fusionLogPos = msg.IndexOf("Fusion log");
                if (fusionLogPos > 0)
                    msg = msg.Substring(0, fusionLogPos - 1);  // -1 to take out the CR right before it starts.
                // More junk to strip out in some assembly-loading exceptions.
                int preBindPos = msg.IndexOf("=== Pre-bind state information");
                if (preBindPos > 0)
                    msg = msg.Substring(0, preBindPos - 1);  // -1 to take out the CR right before it starts.
                // TargetInvocationEexceptions include uninteresting stack trace info after they report the inner exception.
                // Most of this extra stuff is in .NET/Link itself, and the rest is reflection junk.
                int endInnerPos = msg.IndexOf("--- End of inner exception stack trace");
                if (endInnerPos > 0)
                    msg = msg.Substring(0, endInnerPos - 1);  // -1 to take out the CR right before it starts.
                ml.Put(msg);
            }
        }
        ml.EndPacket();
    }


    private void sendOutParams(IKernelLink ml, OutParamRecord[] outParams) {

        if (outParams != null) {
            foreach (OutParamRecord rec in outParams) {
                if (rec != null) {
                    ml.PutFunction("EvaluatePacket", 1);
                    ml.PutFunction(Install.MMA_OUTPARAM, 2);
                    // Convert to 1-based arg numbering for Mathematica.
                    ml.Put(rec.argPosition + 1);
                    ml.Put(rec.val);
                    ml.WaitAndDiscardAnswer();
                }
            }
        }
    }


    // Utility method used by reflect().
    private static void putParameterInfo(IMathLink ml, ParameterInfo[] pis) {

        ml.PutFunction("List", pis.Length);
        foreach (ParameterInfo pi in pis) {
            ml.PutFunction("List", 6);
            ml.Put(pi.IsOptional);
            // DBNull.Value means that there is no default value.
            if (pi.DefaultValue == DBNull.Value)
                ml.PutSymbol("Default");
            else if (pi.DefaultValue != null)
                ml.Put(pi.DefaultValue.ToString());
            else
                ml.Put("null");
            ml.Put(Utils.IsOutOnlyParam(pi));
            Type pt = pi.ParameterType;
            ml.Put(pt.IsByRef);
            ml.Put(ObjectHandler.fullTypeNameForMathematica(pt));
            // I have seen pi.Name be null (e.g., Salford Fortran Calculator example).
            ml.Put(pi.Name != null ? pi.Name : "noname");
        }
    }


    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int GetWindowThreadProcessId(IntPtr hwnd, IntPtr procIDPtr);
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern int GetCurrentThreadId();
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool AttachThreadInput(int idAttachedThread, int idAttachingThread, bool attachOrDetach);

    private void activateWindow(System.Windows.Forms.Form form) {

        int foregroundThread = 0;
        int thisThread = 0;
        
        if (Utils.IsWindows) {
            // We call AttachThreadInput to get around restrictions in Win 98 and later on who is allowed to set the
            // foreground window. .NET can't put its window to the front since it is not the foreground app, but this
            // trick lets us pretend that our thread is the foreground thread. The attach param is true when this is
            // called before toFront() and false when it is called after.
            foregroundThread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            thisThread = GetCurrentThreadId();
            if (foregroundThread != thisThread)
                AttachThreadInput(foregroundThread, thisThread, true);
        }
        form.Show();
        form.Activate();
        if (Utils.IsWindows) {
            if (foregroundThread != thisThread)
                AttachThreadInput(foregroundThread, thisThread, false);
        }
        // Restore the window to its un-minimized state. This doesn't seem to work if it is called
        // before unattaching thread input.
        if (form.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            form.WindowState = System.Windows.Forms.FormWindowState.Normal;
    }

}

}
