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

namespace Wolfram.NETLink.Internal.COM {

/// <summary>
/// Associates a .NET interface with a raw COM object (Sytem.__ComObject). When the COM object is sent to
/// Mathematica, it will arrive "aliased" as the given interface. 
/// </summary>
/// <remarks>
/// This class is used when we know from reflection
/// that the object's type is some .NET interface (e.g., is is returned from a method typed to return a certain
/// interface), but its runtime class is __ComObject. We wrap the object with its its interface type, and use
/// that information later when it is sent to Mathematica via PutReference().
/// </remarks>

internal class COMObjectWrapper {

    internal object wrappedObject;
    internal Type type;  // Will always be an interface type, never a class. Can be null.

    internal COMObjectWrapper(object obj, Type t) {

        // Only COM objects should ever be wrapped.
        System.Diagnostics.Debug.Assert(System.Runtime.InteropServices.Marshal.IsComObject(obj));
        // t should be null or a .NET interface type.
        System.Diagnostics.Debug.Assert(t == null || t.IsInterface);
        wrappedObject = obj;
        type = t;
    }
}

}