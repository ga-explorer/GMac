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

using System;

namespace Wolfram.NETLink {

// I don't really like the interface to Marks. There is no encapsulation (their ctors and dtors do nothing,
// and they are manipulated entirely by MathLinks.

/// <summary>
/// Represents a mark in the incoming MathLink data stream that you can seek back to.
/// </summary>
/// <remarks>
/// Marks can returned to later, to re-read data. A common use is to create a mark, call some method for
/// reading data, and if a MathLinkException is thrown, seek back to the mark and try a different method
/// of reading the data. 
/// <para>
/// ILinkMark has no useful methods. It is an opaque type that is manipulated by methods in the
/// <see cref="IMathLink"/> interface.
/// </para>
/// Marks are created by the <see cref="IMathLink.CreateMark">CreateMark</see> method.
/// <para>
/// Make sure to always call <see cref="IMathLink.DestroyMark">DestroyMark</see> on any marks you create.
/// Failure to do so will cause a memory leak. 
/// Some of the usefulness of marks in the C-language MathLink API is obviated by .NET/Link's
/// <see cref="Expr"/> class.
/// </para>
/// </remarks>
/// 
public interface ILinkMark {

    /// <summary>
    /// Gets the link pointer for the link on which this mark was created. This property should not
    /// be used by programmers.
    /// </summary>
    /// 
    IntPtr Mark {
        get;
    }
}

}
