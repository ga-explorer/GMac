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
using System.Runtime.InteropServices;

namespace Wolfram.NETLink.Internal.COM {

/// <summary>
/// Various static methods for dealing with "raw" COM objects (runtime type is System.__ComObject).
/// </summary>
/// 
internal class COMUtilities {

    [DllImport("kernel32.dll")]
    private static extern int GetUserDefaultLCID();
    [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
    private static extern int ProgIDFromCLSID(ref Guid clsid, out string progID);

    private static Guid IID_NULL = new Guid(0,0,0,0,0,0,0,0,0,0,0);


    /// <summary>
    /// Determines whether the name belongs to a COM property on the given raw COM object.
    /// </summary>
    /// <remarks>
    /// Looks up type information using the COM ITypeInfo interface.
    /// Caches the result in the object's built-in COM data area so that subsequent queries for the same name are fast.
    /// </remarks>
    /// <param name="obj"></param>
    /// <param name="memberName"></param>
    /// <returns>false if no type information is available, or for any error condition.</returns>
    /// 
    internal static bool IsCOMProp(object obj, string memberName) {

        if (Utils.IsMono)
            return false;

        // The memberName can be changed by the call to GetMemberDispID below (U to _ conversion), but we want
        // to cache the isComProp value for the unmodified member name, so we save it.
        string unmodifiedMemberName = memberName;
        object cachedValue = Marshal.GetComObjectData(obj, "NETLinkIsCOMProp" + unmodifiedMemberName);
        if (cachedValue != null) {
            return (bool) cachedValue;
        } else {
            UCOMIDispatch iDisp;
            UCOMITypeInfo iTypeInfo;
            try {
                iDisp = GetIDispatch(obj);
                iTypeInfo = GetITypeInfo(obj, iDisp);
            } catch (Exception) {
                Marshal.SetComObjectData(obj, "NETLinkIsCOMProp" + unmodifiedMemberName, false);
                return false;
            }
            // GetMemberDispID() does not throw on failure--it returns -1. 
            int memberDispId = GetMemberDispID(obj, iDisp, ref memberName);
            if (memberDispId == -1) {
                Marshal.SetComObjectData(obj, "NETLinkIsCOMProp" + unmodifiedMemberName, false);
                return false;
            }

            // We have successfully passed GetIDsOfNames() and GetITypeInfo(), so we know that type info is available and that
            // the name is a member of the dispatch interface. The name may also have been modified to reflect U to _ conversion.

            // Previous versions of this method acquired an ITypeInfo2 from the ITypeInfo and called GetFuncIndexOfMemId() to get
            // a function index suitable for GetFuncDesc(). There were problems with that technique, in that the index returned
            // sometimes (not always) needed to have 7 added to it. Because we cache the T/F result from this method anyway,
            // we now just exhaustively call GetFuncDesc() on all the members, looking for one with the right memberDispId.
            // Then we can check its func info to see if it is a property with no args.
            bool result = false;
            iTypeInfo.GetTypeAttr(out var pTypeAttr);
            TYPEATTR typeAttr = (TYPEATTR) Marshal.PtrToStructure(pTypeAttr, typeof(TYPEATTR));
            int numFuncs = typeAttr.cFuncs;
            iTypeInfo.ReleaseTypeAttr(pTypeAttr);
            for (int i = 0; i < numFuncs; i++) {
                iTypeInfo.GetFuncDesc(i, out var pFuncDesc);
                FUNCDESC funcDesc = (FUNCDESC) Marshal.PtrToStructure(pFuncDesc, typeof(FUNCDESC));
                int thisMemberID = funcDesc.memid;
                INVOKEKIND invokeKind = funcDesc.invkind;
                int numParams = funcDesc.cParams;
                iTypeInfo.ReleaseFuncDesc(pFuncDesc);
                if (thisMemberID == memberDispId) {
                    result = invokeKind == INVOKEKIND.INVOKE_PROPERTYGET && numParams == 0;
                    break;
                }
            }
            Marshal.SetComObjectData(obj, "NETLinkIsCOMProp" + unmodifiedMemberName, result);
            return result;
        }
    }


    /// <summary>
    /// Gets parameter information for the given member.
    /// </summary>
    /// <remarks>
    /// Uses the COM ITypeInfo interface to get the information. Does not throw. If it returns false then the out params
    /// will not hold useful information.
    /// </remarks>
    /// <param name="obj"></param>
    /// <param name="memberName"></param>
    /// <param name="callType"></param>
    /// <param name="isBeingCalledWithZeroArgs"></param>
    /// <param name="paramTypes"></param>
    /// <param name="paramFlags"></param>
    /// <param name="endsWithParamArray"></param>
    /// <returns>true if the name refers to a valid member or if no type information is available</returns>
    /// 
    internal static bool GetMemberInfo(object obj, ref string memberName, int callType, bool isBeingCalledWithZeroArgs,
                                        out Type[] paramTypes, out PARAMFLAG[] paramFlags, out bool endsWithParamArray) {

        // There are some early returns in this method, so we initialize the out params right away. To the caller, if these values
        // come back null it means that the information could not be determined.
        paramTypes = null;
        paramFlags = null;
        endsWithParamArray = false;

        int defaultLCID = GetUserDefaultLCID();
        try {
            // Throws in this outer try cause true to be returned. They mean "type info could not be obtained, so the member is not
            // known to be invalid."

            UCOMIDispatch iDisp = GetIDispatch(obj);
            int memberDispId = GetMemberDispID(obj, iDisp, ref memberName);
            if (memberDispId == -1)
                // Name not found by GetIDsOfNames.
                return false;

            // If no args are being passed, then we can skip all the machinations below about type info.
            if (isBeingCalledWithZeroArgs)
                return true;

            UCOMITypeInfo iTypeInfo = GetITypeInfo(obj, iDisp);
            IntPtr pFuncDesc;

            // Check to see if the func index for this member name is already cached from a previous call.
            FuncIndexHolder fih = (FuncIndexHolder) Marshal.GetComObjectData(obj, "NETLinkFuncIndex" + memberName);
            if (fih == null) {
                // This will be populated below.
                fih = new FuncIndexHolder();
                Marshal.SetComObjectData(obj, "NETLinkFuncIndex" + memberName, fih);
            }
            int funcIndex = -1;
            if (callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_GET) {
                if (fih.funcIndexForCALLTYPE_FIELD_OR_SIMPLE_PROP_GET != -1)
                    funcIndex = fih.funcIndexForCALLTYPE_FIELD_OR_SIMPLE_PROP_GET;
            } else if (callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_SET || callType == Install.CALLTYPE_PARAM_PROP_SET) {
                if (fih.funcIndexForCALLTYPE_FIELD_OR_ANY_PROP_SET != -1)
                    funcIndex = fih.funcIndexForCALLTYPE_FIELD_OR_ANY_PROP_SET;
            } else if (callType == Install.CALLTYPE_METHOD || callType == Install.CALLTYPE_PARAM_PROP_GET) {
                // COM objects are treated by M code as if they all have indexers, so calls like obj[1] will
                // come in as CALLTYPE_PARAM_PROP_GET with a property name of Item. We can just treat these like
                // method calls.
                if (fih.funcIndexForCALLTYPE_METHOD != -1)
                    funcIndex = fih.funcIndexForCALLTYPE_METHOD;
            }

            // Did not have the func index for this call type cached.
            if (funcIndex == -1) {
                iTypeInfo.GetTypeAttr(out var pTypeAttr);
                TYPEATTR typeAttr = (TYPEATTR) Marshal.PtrToStructure(pTypeAttr, typeof(TYPEATTR));
                int numFuncs = typeAttr.cFuncs;
                iTypeInfo.ReleaseTypeAttr(pTypeAttr);
                bool foundDispId = false;
                for (int thisIndex = 0; thisIndex < numFuncs; thisIndex++) {
                    // Be aware that GetFuncDesc() can (I think) throw a cryptic "Element not found" exception,
                    // such as for a hidden property like (Excel) _Application.ActiveDialog.
                    iTypeInfo.GetFuncDesc(thisIndex, out pFuncDesc);
                    FUNCDESC funcDesc = (FUNCDESC) Marshal.PtrToStructure(pFuncDesc, typeof(FUNCDESC));
                    int thisMemberId = funcDesc.memid;
                    INVOKEKIND invokeKind = funcDesc.invkind;
                    iTypeInfo.ReleaseFuncDesc(pFuncDesc);
                    if (thisMemberId == memberDispId) {
                        foundDispId = true;
                        // Verify that it is a member of the correct call type.
                        if (callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_GET && invokeKind == INVOKEKIND.INVOKE_PROPERTYGET) {
                            fih.funcIndexForCALLTYPE_FIELD_OR_SIMPLE_PROP_GET = thisIndex;
                            funcIndex = thisIndex;
                            break;
                        } else if ((callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_SET || callType == Install.CALLTYPE_PARAM_PROP_SET) &&
                                            (invokeKind == INVOKEKIND.INVOKE_PROPERTYPUT || invokeKind == INVOKEKIND.INVOKE_PROPERTYPUTREF)) {
                            fih.funcIndexForCALLTYPE_FIELD_OR_ANY_PROP_SET = thisIndex;
                            funcIndex = thisIndex;
                            break;
                        } else if ((callType == Install.CALLTYPE_METHOD && (invokeKind == INVOKEKIND.INVOKE_FUNC || invokeKind == INVOKEKIND.INVOKE_PROPERTYGET)) ||
                                    (callType == Install.CALLTYPE_PARAM_PROP_GET && invokeKind == INVOKEKIND.INVOKE_PROPERTYGET)) {
                            // Parameterized prop gets, not sets, look like CALLTYPE_METHOD. Also, as discussed in an earlier comment,
                            // indexer notation calls (obj[1]) are supported for all COM objects and come in as CALLTYPE_PARAM_PROP_GET.
                            fih.funcIndexForCALLTYPE_METHOD = thisIndex;
                            funcIndex = thisIndex;
                            break;
                        }           
                    }
                }
                if (funcIndex == -1) {
                    // We didn't find the member in our search. This can happen in two ways. First, the member might
                    // exist but not be the right call type. For this case we want to return false. Second, we didn't
                    // even find the dispid. This can happen in unusual cases I don't understan An example of this
                    // is the IWebBrowser2 interface obtained by CreateCOMObject["InternetExplorer.Application"].
                    // For this case we want to return true, as if there was no type info at all available, so the
                    // call can still proceed.
                    return !foundDispId;
                }
            }

            // If we get to here, we have a valid funcIndex and memberDispId, and the member is of the correct variety
            // that corresponds to how it was called (e.g., it's a method and it was called as such). All that
            // remains is to get the parameter types.

            iTypeInfo.GetFuncDesc(funcIndex, out pFuncDesc);
            try {
                FUNCDESC funcDesc = (FUNCDESC) Marshal.PtrToStructure(pFuncDesc, typeof(FUNCDESC));
                Debug.Assert(funcDesc.memid == memberDispId);
                int paramCount = funcDesc.cParams;
                // Functions that end with a VB-style ParamArray (a variable-length argument sequence) have -1 for the
                // cParamsOpt member. It is convenient to treat them specially, hence the 'endsWithParamArray' variable
                // (which is an out param for this method). The special treatment involves leaving the ParamArray arg off
                // the list of paramFlags and paramTypes. We just pretend there is one less arg to the function but
                // record that it ends with a ParamArray. We always know the type of this arg anyway: ByRef Variant[].
                // To the caller, though, the individual args are sent as a sequence, not packed into an array.
                endsWithParamArray = funcDesc.cParamsOpt == -1;
                if (endsWithParamArray)
                    paramCount--;
                paramTypes = new Type[paramCount];
                paramFlags = new PARAMFLAG[paramCount];
                IntPtr pElemDescArray = funcDesc.lprgelemdescParam;
                for (int paramIndex = 0; paramIndex < paramCount; paramIndex++) {
                    ELEMDESC elemDesc = (ELEMDESC) Marshal.PtrToStructure((IntPtr)(pElemDescArray.ToInt64() +
                                                        paramIndex * Marshal.SizeOf(typeof(ELEMDESC))), typeof(ELEMDESC));
                    TYPEDESC typeDesc = elemDesc.tdesc;
                    paramFlags[paramIndex] = elemDesc.desc.paramdesc.wParamFlags;
                    // I think I should never see a retval param here. They have been automatically converted to return types.
                    Debug.Assert((paramFlags[paramIndex] & PARAMFLAG.PARAMFLAG_FRETVAL) == 0);
                    VarEnum variantType = (VarEnum) typeDesc.vt;
                    bool isArray = variantType == VarEnum.VT_SAFEARRAY;
                    bool isPtr = variantType == VarEnum.VT_PTR;
                    bool isOut = (paramFlags[paramIndex] & PARAMFLAG.PARAMFLAG_FOUT) != 0;
                    // VB array params will have isPtr and !isArray, because they are always ByRef (thus ptrs).
                    // In general (always?), out params will be VT_PTR.
                    if (isArray || isPtr) {
                        IntPtr pElementTypeDesc = typeDesc.lpValue;
                        TYPEDESC elementTypeDesc = (TYPEDESC) Marshal.PtrToStructure(pElementTypeDesc, typeof(TYPEDESC));
                        variantType = (VarEnum) elementTypeDesc.vt;
                        // If the arg was a ptr to an array (e.g., as in VB objects), do it again to get the element type.
                        if (variantType == VarEnum.VT_SAFEARRAY) {
                            isArray = true;
                            pElementTypeDesc = elementTypeDesc.lpValue;
                            elementTypeDesc = (TYPEDESC) Marshal.PtrToStructure(pElementTypeDesc, typeof(TYPEDESC));
                            variantType = (VarEnum) elementTypeDesc.vt;
                        }
                    }
                    paramTypes[paramIndex] = managedTypeForVariantType(variantType, isArray, isPtr, isOut);
                }
            } finally {
                iTypeInfo.ReleaseFuncDesc(pFuncDesc);
            }
            return true;
        } catch (Exception) {
            return true;
        }
    }


    /// <summary>
    /// Gets the name of the default COM interface for this object.
    /// </summary>
    /// <remarks>
    /// Uses the COM ITypeInfo interface to get the information. Does not throw.
    /// </remarks>
    /// <param name="obj"></param>
    /// <returns>The name of the interface; null on any exception</returns>
    /// 
    internal static string GetDefaultCOMInterfaceName(object obj) {

        try {
            string intfName = (string) Marshal.GetComObjectData(obj, "NETLinkCOMInterface");
            if (intfName == null) {
                UCOMIDispatch iDisp = GetIDispatch(obj);
                UCOMITypeInfo iTypeInfo = GetITypeInfo(obj, iDisp);
                iTypeInfo.GetDocumentation(-1, out intfName, out var docStr, out var helpContext, out var helpFile);
                iTypeInfo.GetContainingTypeLib(out var iTypeLib, out var index);
                iTypeLib.GetDocumentation(-1, out var typeLibName, out docStr, out helpContext, out helpFile);
                intfName = typeLibName + "." + intfName;
                Marshal.SetComObjectData(obj, "NETLinkCOMInterface", intfName);
                // Not strictly necessary to release here, but better to force it now than rely on GC to do it later.
                // Cannot release iDisp or iTypeInfo, as they might be cached in the object.
                Marshal.ReleaseComObject(iTypeLib);
            }
            return intfName;
        } catch (Exception) {
            return null;
        }
    }


    /*****************  These are called from Mathematica  ***************/


    /// <summary>
    /// "Casts" the __ComObject to the given type.
    /// </summary>
    /// <remarks>
    /// The type can be a class or an interface. If it is a class, you get back an object typed as that class.
    /// If it is an interface you get back a COMObjectWrapper that associates the object with that interface.
    /// Allows exceptions to be thrown.
    /// </remarks>
    /// <param name="obj">The COM object to cast.</param>
    /// <param name="t">The type to cast to.</param>
    /// <returns></returns>
    /// 
    internal static object Cast(object obj, Type t) {
        
        if (t.IsInterface) {
            IntPtr pUnk = Marshal.GetIUnknownForObject(obj);
            object result = Marshal.GetTypedObjectForIUnknown(pUnk, t);
            Marshal.Release(pUnk);
            return new COMObjectWrapper(result, t);
        } else {
            return Marshal.CreateWrapperOfType(obj, t);
        }
    }

    /// <summary>
    /// Creates a new COM object from the given CLSID or ProgID. Used by the Mathematica function CreateCOMObject.
    /// </summary>
    /// <param name="clsIDOrProgID"></param>
    /// <returns></returns>
    /// 
    internal static object createCOMObject(string clsIDOrProgID) {
        
        Type t;
        if (clsIDOrProgID.IndexOf("-") != -1) {
            // clsIDOrProgID is a CLSID. Last arg means "throw on error".
            t = Type.GetTypeFromCLSID(new Guid(clsIDOrProgID), true);
        } else {
            // clsIDOrProgID is a ProgID. Last arg means "throw on error".
            t = Type.GetTypeFromProgID(clsIDOrProgID, true);
        }
        return Activator.CreateInstance(t);
    }

    /// <summary>
    /// Gets a currently-active COM object with the given CLSID or ProgID. Used by the Mathematica function GetActiveCOMObject.
    /// </summary>
    /// <param name="clsIDOrProgID"></param>
    /// <returns></returns>
    /// 
    internal static object getActiveCOMObject(string clsIDOrProgID) {
        
        if (clsIDOrProgID.IndexOf("-") != -1) {
            // clsIDOrProgID is a CLSID. Must convert to a ProgID.
            Guid g = new Guid(clsIDOrProgID);
            ProgIDFromCLSID(ref g, out var progID);
            clsIDOrProgID = progID;
        } 
        object obj = Marshal.GetActiveObject(clsIDOrProgID);
        // Don't throw if managed type cannot be found. Just return the raw COM object instead.
        Type t = Type.GetTypeFromProgID(clsIDOrProgID, false);
        return t != null ? Marshal.CreateWrapperOfType(obj, t) : obj;
    }


    /// <summary>
    /// Like Marshal.ReleaseCOMObject, but also frees IDispatch and ITypeInfo objects held in the object's cache.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>The new COM refcount.</returns>
    /// 
    internal static int releaseCOMObject(object obj) {

        int newRefCount = Marshal.ReleaseComObject(obj);
        if (newRefCount == 0) {
            UCOMITypeInfo iTypeInfo = (UCOMITypeInfo) Marshal.GetComObjectData(obj, "NETLinkITypeInfo");
            if (iTypeInfo != null)
                Marshal.ReleaseComObject(iTypeInfo);
            UCOMIDispatch iDisp = (UCOMIDispatch) Marshal.GetComObjectData(obj, "NETLinkIDispatch");
            if (iDisp != null)
                Marshal.ReleaseComObject(iDisp);
        }
        return newRefCount;
    }


    /********************************************  Private  *************************************************/

    // Will throw if cast is not valid (COM object does not support IDispatch). Although this operation
    // is just a cast, for readability I like it written as a function call.
    private static UCOMIDispatch GetIDispatch(object obj) {
        return (UCOMIDispatch) obj;
    }


    // Allows exceptions to be thrown.
    private static UCOMITypeInfo GetITypeInfo(object obj, UCOMIDispatch iDisp) {

        UCOMITypeInfo iTypeInfo = (UCOMITypeInfo) Marshal.GetComObjectData(obj, "NETLinkITypeInfo");
        if (iTypeInfo == null) {
            iDisp.GetTypeInfo(0, GetUserDefaultLCID(), out iTypeInfo);
            Marshal.SetComObjectData(obj, "NETLinkITypeInfo", iTypeInfo);
        }
        return iTypeInfo;
    }


    // Returns -1 on failure. Does not throw.
    private static int GetMemberDispID(object obj, UCOMIDispatch iDisp, ref string memberName) {

        object val = Marshal.GetComObjectData(obj, "NETLinkDispID" + memberName);
        if (val != null) {
            // We also need to extract the modified (U --> _ converted) name.
            memberName = (string) Marshal.GetComObjectData(obj, "NETLinkModifiedName" + memberName);
            return (int) val;
        } else {
            int memberDispId;
            int defaultLCID = GetUserDefaultLCID();
            string[] substrs = memberName.Split('U');
            int numCombinations = (int) Math.Pow(2, substrs.Length - 1);
            for (int i = 0; i < numCombinations; i++) {
                string modifiedName = substrs[0];
                // Use bit ops to take i and pick 0 -> U, 1 -> _ for replacement.
                for (int j = 1; j < substrs.Length; j++)
                    modifiedName += ((((i >> (j-1)) & 1) == 0) ? "U" : "_") + substrs[j];
                try {
                    iDisp.GetIDsOfNames(ref IID_NULL, new string[]{modifiedName}, 1, defaultLCID, out memberDispId);
                } catch (Exception) {
                    // Try next combination.
                    continue;
                }
                // If we get here, name was found, and is held in the variable modifiedName. Cache the correct dispID
                // for the unaltered name as it arrives from M, cache the modified name, and then set the ref
                // parameter memberName to correct name.
                Marshal.SetComObjectData(obj, "NETLinkDispID" + memberName, memberDispId);
                Marshal.SetComObjectData(obj, "NETLinkModifiedName" + memberName, modifiedName);
                memberName = modifiedName;
                return memberDispId;
            }
            // Name not found by GetIDsOfNames().
            return -1;
        }
    }


    private static Type managedTypeForVariantType(VarEnum vt, bool isArray, bool isPtr, bool isOut) {

        if (isArray) {
            switch (vt) {
                case VarEnum.VT_I1:
                    return typeof(sbyte[]);
                case VarEnum.VT_I2:
                    return typeof(short[]);
                case VarEnum.VT_I4:
                case VarEnum.VT_INT:
                case VarEnum.VT_HRESULT:
                    return typeof(int[]);
                case VarEnum.VT_I8:
                    return typeof(long[]);
                case VarEnum.VT_UI1:
                    return typeof(byte[]);
                case VarEnum.VT_UI2:
                    return typeof(ushort[]);
                case VarEnum.VT_UI4:
                case VarEnum.VT_UINT:
                    return typeof(uint[]);
                case VarEnum.VT_UI8:
                    return typeof(ulong[]);
                case VarEnum.VT_DECIMAL:
                    return typeof(decimal[]);
                case VarEnum.VT_R4:
                    return typeof(float[]);
                case VarEnum.VT_R8:
                    return typeof(double[]);
                case VarEnum.VT_BOOL:
                    return typeof(bool[]);
                case VarEnum.VT_BSTR:
                case VarEnum.VT_LPSTR:  // Are these LP types legal here? Will they ever actually be encountered?
                case VarEnum.VT_LPWSTR:
                    return typeof(string[]);
                default:
                    return typeof(object[]);
            }
        } else {
            switch (vt) {
                case VarEnum.VT_I1:
                    return typeof(sbyte);
                case VarEnum.VT_I2:
                    return typeof(short);
                case VarEnum.VT_UI1:
                    return typeof(byte);
                case VarEnum.VT_UI2:
                    return typeof(ushort);
                case VarEnum.VT_I4:
                case VarEnum.VT_INT:
                case VarEnum.VT_HRESULT:
                    return typeof(int);
                case VarEnum.VT_I8:
                    return typeof(long);
                case VarEnum.VT_UINT:
                case VarEnum.VT_UI4:
                    return typeof(uint);
                case VarEnum.VT_UI8:
                    return typeof(ulong);
                case VarEnum.VT_DECIMAL:
                    return typeof(decimal);
                case VarEnum.VT_R4:
                    return typeof(float);
                case VarEnum.VT_R8:
                    return typeof(double);
                case VarEnum.VT_BOOL:
                    return typeof(bool);
                case VarEnum.VT_BSTR:
                case VarEnum.VT_LPSTR:  // Are these LP types legal here? Will they ever actually be encountered?
                case VarEnum.VT_LPWSTR:
                    return typeof(string);
                default:
                    return typeof(object);
            }
        }
    }


    // A simple class for caching func indices of given member names in a COM object.
    internal class FuncIndexHolder {
        internal int funcIndexForCALLTYPE_METHOD = -1;
        internal int funcIndexForCALLTYPE_FIELD_OR_SIMPLE_PROP_GET = -1;
        internal int funcIndexForCALLTYPE_FIELD_OR_ANY_PROP_SET = -1;
    }

}


/// <summary>
/// Managed version of the IDispatch interface.
/// </summary>
/// <remarks>
/// This is all we need to do to define an RCW that wraps an IDispatch interface
/// pointer. We can use Marshal.GetTypedObjectForIUnknown() to wrap a raw IUnknown IntPtr in a managed object of this type.
/// This is not a full implementation of IDispatch--just enough for our needs. The GUID in the metadata below is that of IDispatch.
/// </remarks>
/// 
[ComImport, Guid("00020400-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface UCOMIDispatch {

    void GetTypeInfoCount(
                [Out] out int pctinfo
            );

    void GetTypeInfo(
                [In] int iTInfo,
                [In] int lcid,
                [Out] out UCOMITypeInfo typeInfo
            );

    // This tweaked version will only work for one name (last arg would need to be fixed for more than one name).
    void GetIDsOfNames(
                [In] ref Guid riid,
                [In, MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.BStr)] string[] rgszNames,
                [In] int cNames,
                [In] int lcid,
                [Out] out int rgDispId
            );

    void Invoke(
                /* DUMMY ONLY 
                [In] DISPID dispIdMember,
                [In] REFIID riid,
                [In] LCID lcid,
                [In] WORD wFlags,
                [In, Out] DISPPARAMS * pDispParams,
                [Out] VARIANT * pVarResult,
                [Out] EXCEPINFO * pExcepInfo,
                [Out] UINT * puArgErr
                */
            );
}

}
