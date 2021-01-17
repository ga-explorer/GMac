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
/// The native implementation of ILinkMark.
/// </summary>
/// 
internal class NativeMark : ILinkMark {

    private IntPtr mark;
    private IMathLink ml;

    internal NativeMark(IMathLink ml, IntPtr mark) {
        this.mark = mark;
        this.ml = ml;
    }

    public IntPtr Mark {
        get { return mark; }
    }
}

}
