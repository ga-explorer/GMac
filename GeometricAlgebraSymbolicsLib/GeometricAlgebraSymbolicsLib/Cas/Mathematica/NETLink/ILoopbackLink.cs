//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement, which can be found at
//   www.wolfram.com/solutions/mathlink/netlink.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

namespace Wolfram.NETLink {

/// <summary>
/// Represents a special type of link known as a loopback link.
/// </summary>
/// <remarks>
/// Loopback links are links that have both ends connected to the same program, much like a FIFO queue.
/// Loopback links are useful as temporary holders of expressions that are being moved between links,
/// or as scratchpads on which expressions can be built up and then transferred to other links in a single call.
/// <para>
/// Much of the utility of loopback links to users of the C-language MathLink API is obviated by .NET/Link's
/// <see cref="Expr"/> class, which provides many of the same features in a more accessible way (Expr uses
/// loopback links in its implementation).
/// </para>
/// Objects of type ILoopbackLink are created by the <see>MathLinkFactory.CreateLoopbackLink</see> method.
/// <para>
/// ILoopbackLink has no methods; it is simply a type that marks certain links as having special properties. 
/// </para>
/// </remarks>
///
public interface ILoopbackLink : IMathLink {}

}
