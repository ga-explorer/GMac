//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;


namespace Wolfram.NETLink.UI {

/// <summary>
/// A simple class intended to be used from <i>Mathematica</i> for flicker-free drawing in
/// <i>Mathematica</i> code called from a Paint event handler.
/// </summary>
/// <remarks>
/// This class calls the necessary methods to enable double-buffered drawing, which eliminates flicker.
/// The methods needed to enable this condition are protected, so they cannot be scripted from <i>Mathematica</i> code
/// for an arbitrary component. Instead, a special subclass is required.
/// </remarks>
/// 
public class DoubleBufferedPanel : Panel {

    public DoubleBufferedPanel() {
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
    }

    /// <remarks>
    /// Catches and discards any MathematicaNotReadyException thrown during painting. Exceptions thrown during
    /// painting seem to be treated specially by .NET: Once one is thrown, the component paints itself with
    /// a red X and then never enters the OnPaint() method again. But MathematicaNotReadyExceptions are easy
    /// to generate in components that have a Paint event handler written in <i>Mathematica</i>. For example, the
    /// component is brought to the foreground and needs repainting but the kernel is busy with another
    /// computation. We don't want such exceptions to prevent the component from ever being drawn correctly again.
    /// </remarks>
    /// <param name="e"></param>
    /// 
    protected override void OnPaint(PaintEventArgs e) {
        try {
            base.OnPaint(e);
        } catch (Wolfram.NETLink.MathematicaNotReadyException) {}
    }

}

}
