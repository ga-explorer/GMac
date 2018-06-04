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

namespace Wolfram.NETLink.Internal {

/// <summary>
/// The exception type thrown during calls to .NET from Mathematica.
/// </summary>
/// <remarks>
/// These exceptions often wrap another exception, like InvocationTargetException.
/// Not a user-visible exception, so no need to override ToString().
/// </remarks>
///
internal class CallNETException : ApplicationException {

    string tag;
    // These two strings are usually, but not always, a type and member name.
    string typeName;     
    string memberName;


    internal CallNETException(string tag, string typeName, string memberName) {
        this.tag = tag;
        this.typeName = typeName;
        this.memberName = memberName;
    }

    internal CallNETException(Exception innerException, string memberName) : base("", innerException) {
        this.memberName = memberName;
    }

    // Sepcial exceptions are sent as specialException[tag_String, typeName_String, memberName_String]. Generic exceptions are sent as
    // strings.
    internal void writeToLink(IKernelLink ml) {

        if (InnerException == null) {
            // A special exception that Mathematica code should know about (such as NET::methodnotfound).
            ml.PutFunction(Install.MMA_SPECIALEXCEPTION, 3);
            ml.Put(tag);
            ml.Put(memberName);
            ml.Put(typeName);
        } else {
            // Some exception not specific to the workings of ObjectHandler (such as InvocationTargetExceptions
            // thrown by the Invoke() call). These go as NET::excptn messages with their text just coming from
            // the exception itself.
            Exception exceptionToReport = InnerException;
            if (exceptionToReport is System.Reflection.TargetInvocationException)
                exceptionToReport = exceptionToReport.InnerException;
            // CR-LFs produce two newlines in the front end, so drop them.
            ml.Put(exceptionToReport.ToString().Replace("\r\n", "\n"));
        }
    }
}

}
