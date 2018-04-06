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

namespace Wolfram.NETLink {

/// <summary>
/// This exception is thrown in <see cref="StdLink.RequestTransaction"/> when the kernel is not in a state where it is
/// receptive to calls that <i>originate</i> in .NET, such as calls initiated by user actions in a .NET user interface.
/// </summary>
/// <remarks>
/// These exceptions typically propagate up into the internals of .NET/Link, where they are caught
/// and display a message box.
/// <para>
/// You can catch and discard them if you wish.
/// </para>
/// Most programmers will have no interest in this class.
/// </remarks>
///
public class MathematicaNotReadyException : ApplicationException {

    // In J/Link, calls from Java to M block in RequestTransaction. In .NET, for various thread-related
    // reasons this is not feasible. Instead, a MathematicaNotReadyException is thrown by RequestTransaction.

    internal const int KERNEL_NOT_SHARED = 0;
    internal const int FE_HAS_KERNEL_ATTENTION = 0;

    private int type;

    internal MathematicaNotReadyException(int type) {
        this.type = type;
    }

    public override string ToString() {

        if (type == KERNEL_NOT_SHARED)
            return "Mathematica is not in a state where it is receptive to calls initiated in .NET.\n\n" +
                "You must call one of the Mathematica functions DoModal or ShareKernel\n" +
                "before calls from .NET into Mathematica can succeeed.";
        else
            return "Mathematica is not in a state where it is receptive to calls initiated in .NET.\n\n" +
                "Although ShareKernel has been called, the kernel is currently in use by the front end. " +
                "Calls from .NET into Mathematica cannot occur until the kernel is no longer busy.";
    }
}

}
