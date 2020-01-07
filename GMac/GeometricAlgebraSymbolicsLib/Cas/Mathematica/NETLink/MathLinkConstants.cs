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


/// <summary>
/// Designates a MathLink packet type. Used by the <see cref="PacketHandler"/> delegate, and returned
/// by <see cref="IMathLink.NextPacket"/>.
/// </summary>
/// 
public enum PacketType {
    Illegal          = 0,
    Call             = 7,
    Evaluate         = 13,
    Return           = 3,
    InputName        = 8,
    EnterText        = 14,
    EnterExpression  = 15,
    OutputName       = 9,
    ReturnText       = 4,
    ReturnExpression = 16,
    Display          = 11,
    DisplayEnd       = 12,
    Message          = 5,
    Text             = 2,
    Input            = 1,
    InputString      = 21,
    Menu             = 6,
    Syntax           = 10,
    Suspend          = 17,
    Resume           = 18,
    BeginDialog      = 19,
    EndDialog        = 20,

    FirstUser        = 128,
    LastUser         = 255,
    FrontEnd         = 100,   // Catch-all for packets that need to go to FE.
    Expression       = 101    // Sent for Print output
}


/// <summary>
/// Designates the type of a <i>Mathematica</i> expression being read or written on a link, or in an <see cref="Expr"/>.
/// Used by the IMathLink methods <see cref="IMathLink.PutNext">PutNext</see>,
/// <see cref="IMathLink.GetExpressionType">GetExpressionType</see>, and <see cref="IMathLink.GetNextExpressionType">GetNextExpressionType</see>,
/// and by the <see cref="Expr"/> class.
/// </summary>
/// 
public enum ExpressionType {

    /// <summary>
    /// A <i>Mathematica</i> integer.
    /// </summary>
    Integer    = '+',
    /// <summary>
    /// A <i>Mathematica</i> real number.
    /// </summary>
    Real       = '*',
    /// <summary>
    /// A <i>Mathematica</i> string.
    /// </summary>
    String     = '"',
    /// <summary>
    /// A <i>Mathematica</i> symbol.
    /// </summary>
    Symbol     = 35, 
    /// <summary>
    /// A <i>Mathematica</i> symbol that is True or False.
    /// </summary>
    Boolean    = 'T', 
    /// <summary>
    /// A <i>Mathematica</i> Complex number.
    /// </summary>
    Complex    = 'C', 
    /// <summary>
    /// A NETObject expression (a <i>Mathematica</i> reference to a .NET object).
    /// </summary>
    Object     = '0',
    /// <summary>
    /// A <i>Mathematica</i> function, including List.
    /// </summary>
    Function   = 'F'
}


/// <summary>
/// Designates the type of a low-level MathLink message.
/// </summary>
/// <remarks>
/// Used in <see cref="IMathLink.PutMessage">PutMessage</see>. But most prgrammers will want to use the IKernelLink
/// methods <see cref="IKernelLink.AbortEvaluation">AbortEvaluation</see>,
/// <see cref="IKernelLink.InterruptEvaluation">InterruptEvaluation</see>,
/// or <see cref="IKernelLink.TerminateKernel">TerminateKernel</see>.
/// </remarks>
/// 
public enum MathLinkMessage {
    Terminate  = 1,
    Interrupt  = 2,
    Abort      = 3,
}


/// <summary>
/// A handful of constants, including the .NET/Link version number. 
/// </summary>
/// 
public class NETLinkConstants {

    /// <summary>
    /// The version string identifying this release of .NET/Link.
    /// </summary>
    /// 
    public const string VERSION = "1.6.2";

    /// <summary>
    /// The major version number identifying this release of .NET/Link.
    /// </summary>
    /// 
    public const double VERSION_NUMBER = 1.0 * 1.6;

    // The one and only place that must be changed if I change the package context for the
    // supporting .m file.
    /// <summary>
    /// The Mathematica package context for the .NET/Link support functions.
    /// </summary>
    /// <remarks>
    /// Using this is preferable to hard-coding "NETLink`" in your code if you need to explicitly load
    /// the .NET/Link package file.
    /// <code>
    /// BAD:  ml.Evaluate("Needs[\"NETLink`\"]");
    /// GOOD: ml.Evaluate("Needs[\"" + NETLinkConstants.PACKAGE_CONTEXT + "\"]");
    /// </code>
    /// </remarks>
    /// 
    public const string PACKAGE_CONTEXT = "NETLink`";


    // private ctor, as there is no reason to create an instance of this class.
    private NETLinkConstants() {}
}

}
