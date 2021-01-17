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
using System.Runtime.InteropServices;
//using System.Runtime.InteropServices.ComTypes;


namespace Wolfram.NETLink.Internal.COM {

/// <summary>
/// COMDispatchHandler handles calls to the IDispatch interface on "raw" COM objects (i.e., they have type System.__ComObject).
/// </summary>
internal class COMDispatchHandler {

    /// <summary>
    /// This method is used for calling members on COM objects via IDispatch.
    /// </summary>
    /// <remarks>
    /// All objects that get here will have type __ComObject. They may have been "typed" as a .NET interface, but if
    /// the member in question was found in that interface the call would have been handled through normal channels.
    /// When we get here we have given up hope for any help from the managed side of things.
    /// Note that in one case we can be calling a true managed method here. This is the case where a method from the Object class
    /// (e.g., GetHashCode()) is called on a COM object typed as a .NET interface. The method is not part of the interface, so we
    /// get here, thinking it is a COM method not part of the managed interface. But no matter--the technique used here to call
    /// methods works for managed methods in the Object class as well as true COM calls via IDispatch (note that it is really the
    /// MarshalByRefObject class that the methods might belong to, as that is the parent class of __ComObject).
    /// </remarks>
    /// <param name="ml"></param>
    /// <param name="obj"></param>
    /// <param name="memberName"></param>
    /// <param name="callType"></param>
    /// <param name="argTypes"></param>
    /// <param name="outParams"></param>
    /// <returns>the result of the call</returns>
    internal object callDispatch(IKernelLink ml, object obj, string memberName, int callType, int[] argTypes, out OutParamRecord[] outParams) {

        System.Diagnostics.Debug.Assert(obj != null && obj.GetType().Name == "__ComObject");

        outParams = null;

        bool isBeingCalledWithZeroArgs = argTypes.Length == 0;
        // 'endsWithParamArray' tells us if the function ends in a VB-style ParamArray arg. Args for a ParamArray must be
        // supplied as a sequence, not packed into a list. If there is a ParamArray, COMUtilities.getMemberInfo() won't
        // return a paramTypes or paramFlags entry for it (it always shows up as a ByRef array of Variant, but since the args
        // must be sent as a sequence it is easiest to treat it specially).
        // We cannot just call InvokeMember even if we know the args can be converted, because we don't know if the member
        // name needs U -> _ conversion, or if any parameters are out params.
        // Note that isValidMember does not guarantee it's valid, only that it is not _known_ to be invalid. If there is no
        // type info available, isValidMember will be true. MemberName is byref since it may need to be massaged (U -> _) by
        // GetMemberInfo(). ParamTypes amd paramFlags values only relevant if function return value is true, in which case
        // they are set to null if no type info is available or if there are no args being passed to this method (in which
        // case type info is irrelevant).
        bool isValidMember = COMUtilities.GetMemberInfo(obj, ref memberName, callType, isBeingCalledWithZeroArgs, out var paramTypes, out var paramFlags, out var endsWithParamArray);

        if (!isValidMember) {
            string err;
            if (callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_GET || callType == Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_SET)
                err = Install.COM_PROP_NOT_FOUND;
            else
                err = Install.COM_METHOD_NOT_FOUND;
            throw new CallNETException(err, "System.__ComObject", memberName);
        }
        
        // Note this will also get the value false if isBeingCalledWithZeroArgs==true.
        bool typeInfoIsAvailable = paramTypes != null;
        // If the method ends with a VB-style ParamArray arg, all these params are out params, but they don't show up at all
        // in paramFlags, so we use this information to juice the initial value of hasOutParams. 
        bool hasOutParams = endsWithParamArray;

        // If type info is available, we can extract some information from it. We check the arg count for too few and too many.
        // We also decide whether the args can be converted automatically by the innards of the IDispatch mechanism.
        // This means checking whether any of the args are out params (ByRef), because COM will automatically convert
        // args to the appropriate type of Variant unless it is ByRef (this conversion includes narrowing or widening primitive
        // types like ints and reals). This means that we don't need to care that we will read an integer as type int, but
        // the arg slot is a short. Arrays are always ByRef so they cannot be converted automatically. If the args can be
        // converted automatically, we can read them as their most natural type; otherwise we have to read them as the proper
        // type for that arg slot or COM will give a "Type mismatch" error.
        // Note that we will not enter this block if 0 args are being passed. This means that for 0-arg
        // calls, we will not detect it here if more than 0 args are required. Instead, an error will occur during InvokeMember().
        // That's not a big problem, since the error will say something about the number of args. We prefer the optimization of
        // not taking the time to look up argument type info (in COMUtilities.GetMemberInfo() above) to the goal of getting
        // the cleanest possible error messages.
        if (typeInfoIsAvailable) {

            // Get this check out of the way first. Note that it is OK to pass more args than there are entries in
            // paramFlags if the function ends with a ParamArray.
            if (argTypes.Length > paramFlags.Length && !endsWithParamArray)
                // Too many args were passed.
                throw new CallNETException(Install.METHOD_ARG_COUNT, "System.__ComObject", memberName);

            // At this point, argTypes.Length could be greater or less than paramFlags.Length. Less if we are leaving out optional
            // params, more if we are passing args to a ParamArray at the end.
            int minimumAllowableArgCount = 0;
            for (int i = 0; i < Math.Max(paramFlags.Length, argTypes.Length); i++) {
                // If not an optional param, increment minimumAllowableArgCount. 
                if (i < paramFlags.Length && (paramFlags[i] & PARAMFLAG.PARAMFLAG_FOPT) == 0)
                    minimumAllowableArgCount++;
                // Decide if this arg can be converted to the correct type automatically by the innards of the IDispatch mechanism.
                if (i < argTypes.Length) {
                    int argType = argTypes[i];
                    if (argType == Install.ARGTYPE_OTHER) {
                        throw new CallNETException(Install.METHOD_BAD_ARGS, "System.__ComObject", memberName);
                    } else if (i < paramFlags.Length  && (paramFlags[i] & PARAMFLAG.PARAMFLAG_FOUT) != 0) {
                        // Out params (ByRef) cannot be converted.
                        hasOutParams = true;
                    }
                }
            }

            if (argTypes.Length < minimumAllowableArgCount)
                // Too few args were passed.
                throw new CallNETException(Install.METHOD_ARG_COUNT, "System.__ComObject", memberName);
        }

        object[] args = new object[argTypes.Length];

        try {
            for (int i = 0; i < argTypes.Length; i++) {

                // Get this one out of the way first.
                if (argTypes[i] == Install.ARGTYPE_MISSING) {
                    args[i] = System.Reflection.Missing.Value;
                    ml.GetSymbol();
                } else if (!typeInfoIsAvailable || i >= paramFlags.Length /*|| argCanBeConvertedAutomatically[i]*/) {
                    args[i] = ml.GetObject();
                } else if ((paramFlags[i] & PARAMFLAG.PARAMFLAG_FOUT) != 0 && (paramFlags[i] & PARAMFLAG.PARAMFLAG_FIN) == 0) {
                    // Out-only params can be discarded from the link.
                    Utils.discardNext(ml);
                    args[i] = null;
                } else {
                    args[i] = Utils.readArgAs(ml, argTypes[i], paramTypes[i]);
                }
            }
        } catch (Exception) {
            // Typically ArgumentException from Utils.readArgAs(). Don't need a special COM_ version of this error message.
            throw new CallNETException(Install.METHOD_BAD_ARGS, "System.__ComObject", memberName);
        }

        // The type will be __ComObject, but we must get it from the object itself (cannot use typeof()).
        Type t = obj.GetType();
        object result = null;

        switch (callType) {
            case Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_GET:
                result = t.InvokeMember(memberName, BindingFlags.GetProperty, null, obj, args);
                break;
            case Install.CALLTYPE_FIELD_OR_SIMPLE_PROP_SET:
                result = t.InvokeMember(memberName, BindingFlags.SetProperty, null, obj, args);
                break;
            case Install.CALLTYPE_METHOD:
                // Recall that non-static parameterized prop gets masquerade as CALLTYPE_METHOD, hence the added BindingFlag.GetProperty
                // in the InvokeMember() call. Example is worksheet@Range["A1"].
                if (hasOutParams && args.Length > 0) {
                    // paramModifiers allows us to have out params returned with new values.
                    ParameterModifier pm = new ParameterModifier(args.Length);
                    // Note that if i >= paramFlags.Length we are daling with the trailing sequence of args to a ParamArray.
                    // Such args are always out params so they need a ParameterModifier (and they don't show up in paramFlags).
                    for (int i = 0; i < args.Length; i++)
                        pm[i] = i >= paramFlags.Length || (paramFlags[i] & PARAMFLAG.PARAMFLAG_FOUT) != 0;
                    ParameterModifier[] paramModifiers = new ParameterModifier[]{pm};
                    result = t.InvokeMember(memberName, BindingFlags.InvokeMethod | BindingFlags.GetProperty, null, obj, args, paramModifiers, null, null);
                } else {
                    result = t.InvokeMember(memberName, BindingFlags.InvokeMethod | BindingFlags.GetProperty, null, obj, args);
                }
                break;
            case Install.CALLTYPE_PARAM_PROP_GET:
                // Don't think we can ever get here, as only static param prop gets come in as CALLTYPE_PARAM_PROP_GET.
                // Still I will leave this code in, in case instance param prop gets ever get properly distinguished on the M side.
                result = t.InvokeMember(memberName, BindingFlags.GetProperty, null, obj, args);
                break;
            case Install.CALLTYPE_PARAM_PROP_SET:
                result = t.InvokeMember(memberName, BindingFlags.SetProperty, null, obj, args);
                break;
        }

        // Now do out params.
        if (hasOutParams) {
            for (int i = 0; i < args.Length; i++) {
                if (i >= paramFlags.Length || (paramFlags[i] & PARAMFLAG.PARAMFLAG_FOUT) != 0) {
                    if (outParams == null)
                        outParams = new OutParamRecord[args.Length];
                    outParams[i] = new OutParamRecord(i, args[i]);
                }
            }
        }

        return result;
    }
}

}

