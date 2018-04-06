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
/// The exception thrown by the "AsXXX" methods of the <see cref="Expr"/> class (e.g.,
/// <see cref="Expr.AsInt64"/>, <see cref="Expr.AsDouble"/>, <see cref="Expr.AsArray"/>, etc.)
/// </summary>
/// <remarks>
/// The Expr conversion methods attempt to return a native .NET representation of the Expr's contents,
/// and if this cannot be done because the Expr cannot be represented in the requested form, an ExprFormatException
/// is thrown. For example, if you called AsArray on an Expr that held a string.
/// </remarks>
/// <seealso cref="Expr"/>
/// 
public class ExprFormatException : ApplicationException {

    internal ExprFormatException(String msg) : base(msg) {}

}

}
