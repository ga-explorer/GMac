//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

namespace Wolfram.NETLink {

/// <exclude/>
/// <summary>
/// Sets up definitions in <i>Mathematica</i> to enable calls into .NET.
/// </summary>
/// <remarks>
/// This class is only public because it must be called from another assembly (InstallableNET.exe). It is not
/// part of the .NET/Link API.
/// </remarks>
/// 
public class Install {

    // These are the types of all the CallPackets that .NET/Link knows about.

    internal const int CALL                  = 1;
    internal const int LOADTYPE1             = 2;
    internal const int LOADTYPE2             = 3;
    internal const int LOADEXISTINGTYPE      = 4;
    internal const int LOADASSEMBLY          = 5;
    internal const int LOADASSEMBLYFROMDIR   = 6;
    internal const int GETASSEMBLYOBJ        = 7;
    internal const int GETTYPEOBJ            = 8;
    internal const int RELEASEOBJECT         = 9;
    internal const int MAKEOBJECT            = 10;
    internal const int CREATEDELEGATE        = 11;
    internal const int VAL                   = 12;
    internal const int REFLECTTYPE           = 13;
    internal const int REFLECTASM            = 14;
    internal const int SETCOMPLEX            = 15;
    internal const int SAMEQ                 = 16;
    internal const int INSTANCEOF            = 17;
    internal const int CAST                  = 18;

    internal const int PEEKTYPES             = 20;
    internal const int PEEKOBJECTS           = 21;
    internal const int PEEKASSEMBLIES        = 22;

    internal const int MODAL                 = 31;
    internal const int SHOW                  = 32;
    internal const int SHAREKERNEL           = 33;
    internal const int ALLOWUICOMPS          = 34;
    internal const int UILINK                = 35;
    
    internal const int ISCOMPROP             = 40;
    internal const int CREATECOM             = 41;
    internal const int GETACTIVECOM          = 42;
    internal const int RELEASECOM            = 43;
    internal const int LOADTYPELIBRARY       = 44;

    internal const int DEFINEDELEGATE        = 50;
    internal const int DLGTYPENAME           = 51;
    internal const int ADDHANDLER            = 52;
    internal const int REMOVEHANDLER         = 53;

    internal const int CREATEDLL1            = 60;
    internal const int CREATEDLL2            = 61;

    internal const int GETEXCEPTION          = 70;

    internal const int CONNECTTOFE           = 80;
    internal const int DISCONNECTTOFE        = 81;

    internal const int NOOP                  = 90;
    internal const int NOOP2                 = 91;


    // Must agree with M code in CallNET.m
    internal const int CALLTYPE_CTOR                      = 0;
    internal const int CALLTYPE_FIELD_OR_SIMPLE_PROP_GET  = 1;
    internal const int CALLTYPE_FIELD_OR_SIMPLE_PROP_SET  = 2;
    // Static parameterized prop get or instance default param prop (indexer) get.
    internal const int CALLTYPE_PARAM_PROP_GET            = 3;
    // Parameterized prop sets including indexers.
    internal const int CALLTYPE_PARAM_PROP_SET            = 4;
    // Methods and non-static non-indexer parameterized prop gets. These cannot be distinguished on the Mathematica side.
    internal const int CALLTYPE_METHOD                    = 5;


    internal const string MMA_CREATEINSTANCEDEFS = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "createInstanceDefs";
    internal const string MMA_OBJECTSYMBOLPREFIX = NETLinkConstants.PACKAGE_CONTEXT + "Objects`NETObject$";
    internal const string MMA_LOADTYPE = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "loadTypeFromNET";
    internal const string MMA_HANDLEEXCEPTION = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "handleException";
    internal const string MMA_PREPAREFORMANUALRETURN = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "prepareForManualReturn";
    internal const string MMA_MANUALEXCEPTION = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "manualException";
    internal const string MMA_SPECIALEXCEPTION = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "specialException";
    internal const string MMA_OUTPARAM = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "outParam";
    internal const string MMA_CALLBACKWRAPPER = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "delegateCallbackWrapper";
    internal const string MMA_NETMETHODCALLBACKWRAPPER = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "methodCallbackWrapper";

    internal const string MMA_UIPACKET = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "UIPacket";
    internal const string MMA_YIELDNORETURN = KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "nYieldNoReturn";

    // These strings are keys sent to Mathematica for errors in many nXXX functions that call NET.
    // The actual error message is stored in Mathematica code and looked up based on the string value.
    // These string values must match M code. If you add a string, you must add a definition to the
    // error lookup table in Exceptions.m.
    internal const string NO_CTOR                         = "noctor";
    internal const string METHOD_NOT_FOUND                = "nomethod";
    internal const string FIELD_NOT_FOUND                 = "nofield";
    internal const string NO_INDEXER                      = "noindexer";
    internal const string PROP_NOT_FOUND                  = "noprop";

    internal const string COM_METHOD_NOT_FOUND            = "nocommeth";
    internal const string COM_PROP_NOT_FOUND              = "nocomprop";

    internal const string CTOR_ARG_COUNT                  = "ctorargc";
    internal const string METHOD_ARG_COUNT                = "methargc";
    internal const string INDEXER_ARG_COUNT               = "indxrargc";
    internal const string PARAM_PROP_ARG_COUNT            = "parampropargc";

    internal const string CTOR_BAD_ARGS                   = "ctorargs";
    internal const string METHOD_BAD_ARGS                 = "methodargs";
    internal const string FIELD_BAD_ARGS                  = "fieldtype";
    internal const string PROP_BAD_ARGS                   = "proptype";
    internal const string INDEXER_BAD_ARGS                = "indxrargs";
    internal const string PARAM_PROP_BAD_ARGS             = "parampropargs";

    internal const string FIELD_CANNOT_BE_SET             = "fieldnoset";
    internal const string PROP_CANNOT_BE_READ             = "propnoget";
    internal const string PROP_CANNOT_BE_SET              = "propnoset";
    internal const string INDEXER_CANNOT_BE_READ          = "indxrnoget";
    internal const string INDEXER_CANNOT_BE_SET           = "indxrnoset";

    internal const string IS_EVENT                        = "event";

    internal const string BAD_CAST                        = "cast";

    // Users should not see messages with these tags. I believe they only reflect bugs in .NET/Link.
    internal const string BAD_OBJECT_REFERENCE            = "badobj";
    internal const string BAD_TYPE                        = "badtype";


    // Order must be kept in sync with argTypePatterns array below, but has no dependence on any Mathematica code.
    // TODO: Make these an enum at some point.
    internal const int ARGTYPE_INTEGER     = 1;
    internal const int ARGTYPE_REAL        = 2;
    internal const int ARGTYPE_STRING      = 3;
    internal const int ARGTYPE_BOOLEAN     = 4;
    internal const int ARGTYPE_NULL        = 5;
    internal const int ARGTYPE_MISSING     = 6;
    internal const int ARGTYPE_OBJECTREF   = 7;
    internal const int ARGTYPE_VECTOR      = 8;
    internal const int ARGTYPE_MATRIX      = 9;
    internal const int ARGTYPE_TENSOR3     = 10;
    internal const int ARGTYPE_LIST        = 11;
    internal const int ARGTYPE_COMPLEX     = 12;
    internal const int ARGTYPE_OTHER       = 13;

    // Patterns used for the minimal argument-type identification that happens on the Mathematica side.
    // Testing against these patterns should be fast. They are intended to provide the most information
    // possible about the argument types being passed to .NET calls without slowing things down on the M side.
    // In calls to .NET, each argument is associated with an integer type code identifying its type. These are
    // the tests that determine those type codes.
    private static string[] argTypePatterns = {
        "_Integer",
        "_Real",
        "_String",
        "True | False",
        "Null",
        "Default",
        "_?NETObjectQ",
        "_?VectorQ",
        "_?MatrixQ",
        "x_ /; TensorRank[x] === 3",
        "_List",
        "_Complex",
        "_"
    };

    /// <summary>
    /// Analogous to the MLInstall function in mprep-generated C programs.
    /// </summary>
    /// <param name="ml"></param>
    /// <returns>Whether it succeeded or not.</returns>
    /// 
    public static bool install(IMathLink ml) {

        // Adding nXX functions here requires also adding the symbols to the NETLink`NET.m file.
        try {
            ml.Connect();
            ml.Put("Begin[\"" + KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "\"]");
            definePattern(ml, "nCall[typeName_String, obj_Symbol, callType_Integer, isByVal_, memberName_String, argCount_Integer, typesAndArgs___]", "{typeName, obj, callType, isByVal, memberName, argCount, typesAndArgs}", CALL);
            definePattern(ml, "nLoadType1[type_String, assemblyName_]", "{type, assemblyName}", LOADTYPE1);
            definePattern(ml, "nLoadType2[type_String, assemblyObj_]", "{type, assemblyObj}", LOADTYPE2);
            definePattern(ml, "nLoadExistingType[typeObject_?NETObjectQ]", "{typeObject}", LOADEXISTINGTYPE);
            definePattern(ml, "nLoadAssembly[assemblyNameOrPath_String, suppressErrors_]", "{assemblyNameOrPath, suppressErrors}", LOADASSEMBLY);
            definePattern(ml, "nLoadAssemblyFromDir[assemblyName_String, dir_String]", "{assemblyName, dir}", LOADASSEMBLYFROMDIR);
            definePattern(ml, "nGetAssemblyObject[asmName_String]", "{asmName}", GETASSEMBLYOBJ);
            definePattern(ml, "nGetTypeObject[aqTypeName_String]", "{aqTypeName}", GETTYPEOBJ);
            definePattern(ml, "nReleaseObject[instances:{__Symbol}]", "{instances}", RELEASEOBJECT);
            definePattern(ml, "nMakeObject[typeName_String, argType_Integer, val_]", "{typeName, argType, val}", MAKEOBJECT);
            definePattern(ml, "nVal[obj_?NETObjectQ]", "{obj}", VAL);
            definePattern(ml, "nReflectType[typeName_String]", "{typeName}", REFLECTTYPE);
            definePattern(ml, "nReflectAsm[asmName_String]", "{asmName}", REFLECTASM);
            definePattern(ml, "nSetComplex[typeName_String]", "{typeName}", SETCOMPLEX);
            definePattern(ml, "nSameQ[obj1_?NETObjectQ, obj2_?NETObjectQ]", "{obj1, obj2}", SAMEQ);
            definePattern(ml, "nInstanceOf[obj_?NETObjectQ, aqTypeName_String]", "{obj, aqTypeName}", INSTANCEOF);
            definePattern(ml, "nCast[obj_?NETObjectQ, aqTypeName_String]", "{obj, aqTypeName}", CAST);
            definePattern(ml, "nPeekTypes[]", "{}", PEEKTYPES);
            definePattern(ml, "nPeekObjects[]", "{}", PEEKOBJECTS);
            definePattern(ml, "nPeekAssemblies[]", "{}", PEEKASSEMBLIES);

            definePattern(ml, "nCreateDelegate[typeName_String, mFunc_String, sendTheseArgs_Integer, callsUnshare:(True | False), wrapInNETBlock:(True | False)]", "{typeName, mFunc, sendTheseArgs, callsUnshare, wrapInNETBlock}", CREATEDELEGATE);
            definePattern(ml, "nDefineDelegate[name_String, retTypeName_String, paramTypeNames_List]", "{name, retTypeName, paramTypeNames}", DEFINEDELEGATE);
            definePattern(ml, "nDlgTypeName[eventObject_?NETObjectQ, aqTypeName_String, evtName_String]", "{eventObject, aqTypeName, evtName}", DLGTYPENAME);
            definePattern(ml, "nAddHandler[eventObject_?NETObjectQ, aqTypeName_String, evtName_String, delegate_?NETObjectQ]", "{eventObject, aqTypeName, evtName, delegate}", ADDHANDLER);
            definePattern(ml, "nRemoveHandler[eventObject_?NETObjectQ, aqTypeName_String, evtName_String, delegate_?NETObjectQ]", "{eventObject, aqTypeName, evtName, delegate}", REMOVEHANDLER);

            definePattern(ml, "nCreateDLL1[funcName_String, dllName_String, callConv_String, retTypeName_String, argTypeNames_, areOutParams_, strFormat_String]", "{funcName, dllName, callConv, retTypeName, argTypeNames, areOutParams, strFormat}", CREATEDLL1);
            definePattern(ml, "nCreateDLL2[decl_String, refAsms_, lang_String]", "{decl, refAsms, lang}", CREATEDLL2);

            definePattern(ml, "nModal[modal:(True | False), formToActivate_?NETObjectQ]", "{modal, formToActivate}", MODAL);
            definePattern(ml, "nShow[formToActivate_?NETObjectQ]", "{formToActivate}", SHOW);
            definePattern(ml, "nShareKernel[sharing:(True | False)]", "{sharing}", SHAREKERNEL);
            definePattern(ml, "nAllowUIComputations[allow:(True | False)]", "{allow}", ALLOWUICOMPS);
            definePattern(ml, "nUILink[name_String, prot_String]", "{name, prot}", UILINK);

            definePattern(ml, "nIsCOMProp[obj_?NETObjectQ, memberName_String]", "{obj, memberName}", ISCOMPROP);
            definePattern(ml, "nCreateCOM[clsIDOrProgID_String]", "{clsIDOrProgID}", CREATECOM);
            definePattern(ml, "nGetActiveCOM[clsIDOrProgID_String]", "{clsIDOrProgID}", GETACTIVECOM);
            definePattern(ml, "nReleaseCOM[obj_?NETObjectQ]", "{obj}", RELEASECOM);
            definePattern(ml, "nLoadTypeLibrary[tlbPath_String, safeArrayAsArray_, assemblyFile_String]", "{tlbPath, safeArrayAsArray, assemblyFile}", LOADTYPELIBRARY);

            definePattern(ml, "nGetException[]", "{}", GETEXCEPTION);

            definePattern(ml, "nConnectToFEServer[linkName_String]", "{linkName}", CONNECTTOFE);
            definePattern(ml, "nDisconnectToFEServer[]", "{}", DISCONNECTTOFE);

            // For speed testing:
            definePattern(ml, "noop[]", "{}", NOOP);
            definePattern(ml, "noop2[argc_Integer, args___]", "{argc, args}", NOOP2);

            // Here we define the argTypeToInteger function by sending
            // MapThread[(argTypeToInteger[#1] = #2)&, {ToExpression /@ argTypePatterns, {... ARGTYPE_ constants ...}}]
            ml.PutFunction("MapThread", 2);
              ml.PutFunction("Function", 1);
                ml.PutFunction("Set", 2);
                  ml.PutFunction("argTypeToInteger", 1);
                    ml.PutFunction("Slot", 1);
                      ml.Put(1);
                  ml.PutFunction("Slot", 1);
                    ml.Put(2);
              ml.PutFunction("List", 2);
                ml.PutFunction("Map", 2);
                  ml.PutSymbol("ToExpression");
                  ml.Put(argTypePatterns);
                ml.PutFunction("List", 13);
                  ml.Put(ARGTYPE_INTEGER);
                  ml.Put(ARGTYPE_REAL);
                  ml.Put(ARGTYPE_STRING);
                  ml.Put(ARGTYPE_BOOLEAN);
                  ml.Put(ARGTYPE_NULL);
                  ml.Put(ARGTYPE_MISSING);
                  ml.Put(ARGTYPE_OBJECTREF);
                  ml.Put(ARGTYPE_VECTOR);
                  ml.Put(ARGTYPE_MATRIX);
                  ml.Put(ARGTYPE_TENSOR3);
                  ml.Put(ARGTYPE_LIST);
                  ml.Put(ARGTYPE_COMPLEX);
                  ml.Put(ARGTYPE_OTHER);

            ml.Put("End[]");
            ml.PutSymbol("End");
            ml.Flush();

            return true;
        } catch (MathLinkException) {
            return false;
        }
    }

    // Analogous to the C function of the same name created by mprep for C installable MathLink programs.
    // Note that the built-in DefineExternal function in Mathematica is not abort-safe, so .NET/Link
    // uses its own version, netlinkDefineExternal.
    private static void definePattern(IMathLink ml, string patt, string args, int index) {
        
        ml.PutFunction(KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "netlinkDefineExternal", 3);
        ml.Put(patt);
        ml.Put(args);
        ml.Put(index);
    }

}

}
