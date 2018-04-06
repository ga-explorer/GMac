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
/// The exception thrown by methods in the <see cref="IMathLink"/> and <see cref="IKernelLink"/> interfaces when a link error occurs.
/// </summary>
/// <remarks>
/// MathLinkExceptions are mainly for errors that involve the low-level link itself. After you catch a MathLinkException,
/// the first thing you should do is call <see cref="IMathLink.ClearError">ClearError</see> to try to clear the error condition.
/// If you do not, then the next IMathLink or IKernelLink method you call will throw an exception again.
/// <para>
/// For programmers familiar with the C-language MathLink API, the throwing of a MathLinkException is equivalent to a
/// C-language API function returning a result code other than MLEOK. The <see cref="ErrCode"/> property gives the
/// integer code associated with the error. These integer values include those defined in the C-language header
/// file mathlink.h, and an additional .NET/Link-specific set of values defined in this class. Few programmers
/// will have any use for these integer codes; much more relevant are the textual descriptions of the errors,
/// available from the <see cref="Exception.Message">Message</see> property.
/// </para>
/// </remarks>
/// 
public class MathLinkException : ApplicationException {


    public const int MLEOK      = 0;
    public const int MLEUSER    = 1000;
    
    // TODO: Check these for relevance
    public const int MLE_NON_ML_ERROR            = MLEUSER;
    public const int MLE_OUT_OF_MEMORY           = MLEUSER + 1;
    public const int MLE_BAD_ARRAY_DEPTH         = MLEUSER + 2;
    public const int MLE_BAD_ARRAY_SHAPE         = MLEUSER + 3;
    public const int MLE_ARRAY_NOT_RECTANGULAR   = MLEUSER + 4;
    public const int MLE_BAD_ARRAY               = MLEUSER + 5;  // Bad data type in an array
    public const int MLE_EMPTY_ARRAY             = MLEUSER + 6;  // Array is empty and we have no type info
    public const int MLE_MULTIDIM_ARRAY_OF_ARRAY = MLEUSER + 7;  // Cannot have multidim at start of jagged: [,][]
    public const int MLE_ARRAY_OF_ARRAYCLASS     = MLEUSER + 8;  // Cannot have Array[]
    public const int MLE_BAD_COMPLEX             = MLEUSER + 9;  // Data on link not appropriate for getComplex()
    public const int MLE_NO_COMPLEX              = MLEUSER + 10; // Cannot read a Complex unless complex class is set
    public const int MLE_BAD_ENUM                = MLEUSER + 11; // Enum out of range
    public const int MLE_CREATION_FAILED         = MLEUSER + 12;
    public const int MLE_CONNECT_TIMEOUT         = MLEUSER + 13;
    public const int MLE_GETFUNCTION             = MLEUSER + 14;
    public const int MLE_CHECKFUNCTION           = MLEUSER + 15;
    public const int MLE_SYMBOL                  = MLEUSER + 16; // Found non-Null symbol when calling GetObject().
    public const int MLE_WRAPPED_EXCEPTION       = MLEUSER + 17;

    public const int MLE_FIRST_USER_EXCEPTION    = MLEUSER + 1000;
    
    //Error code in a MathLinkException when getObject() was called and a valid .NET object was not on the link.
    public const int MLE_BAD_OBJECT              = MLEUSER + 100;


    int code;

    /// <summary>
    /// Creates a MathLinkException given a code and textual description. Programmers will probably have no need to
    /// construct their own MathLinkExceptions.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="msg"></param>
    /// 
    public MathLinkException(int code, String msg) : base("Error code: " + code + ". " + msg) {
        this.code = code;
    }


    /// <summary>
    /// Creates a MathLinkException given an error code. Programmers will probably have no need to construct
    /// their own MathLinkExceptions.
    /// </summary>
    /// <remarks>
    /// Only use this form for non-MathLink errors.
    /// </remarks>
    /// 
    public MathLinkException(int code) : this(code, lookupMessageText(code)) {}

    
    /// <summary>
    /// Gets the integer code associated with the MathLink error.
    /// </summary>
    /// <remarks>
    /// These numbers are a superset of those defined in the mathlink.h header file.
    /// </remarks>
    /// 
    public int ErrCode {
        get { return code; }
    }
    

    private static string lookupMessageText(int code) {

        String res = null;
        switch (code) {
            case MLE_BAD_ARRAY_DEPTH:
                res = "You are attempting to read an array using a depth specification that does not match the incoming array.";
                break;
            case MLE_BAD_ARRAY_SHAPE:
                res = "The array being read has an irregular shape that cannot be read as any .NET type.";
                break;
            case MLE_ARRAY_NOT_RECTANGULAR:
                res = "The array being read is not rectangular. It is either jagged (e.g., {{1,2,3},{4,5}}) or misshapen (e.g., {{1,2,3},4}).";
                break;
            case MLE_BAD_ARRAY:
                res = "Array contains data of a type that cannot be read as a native .NET type (for example, a Mathematica symbol or function).";
                break;
            case MLE_MULTIDIM_ARRAY_OF_ARRAY:
                res = "Cannot read arays that are jagged but start with a multidimensionl array, for example int[,][].";
                break;
            case MLE_ARRAY_OF_ARRAYCLASS:
                res = "Cannot read arrays whose element type is the Array class, i.e. Array[].";
                break;
            case MLE_EMPTY_ARRAY:
                res = "The array being read is empty and there is no .NET type info available, so it is impossible to determine the correct .NET array type.";
                break;
            case MLE_BAD_COMPLEX:
                res = "Expression could not be read as a complex number.";
                break;
            case MLE_NO_COMPLEX:
                res = "Complex numbers cannot be read or sent unless a Type to represent them is designated using IMathLink.SetComplexType().";
                break;
            case MLE_CONNECT_TIMEOUT:
                res = "The link was not connected before the requested time limit elapsed.";
                break;
            case MLE_GETFUNCTION:
                res = "GetFunction() was called when the expression waiting on the link was not a function.";
                break;
            case MLE_CHECKFUNCTION:
                res = "The expression waiting on the link did not match the specification in CheckFunction() or CheckFunctionWithArgCount().";
                break;
            case MLE_BAD_OBJECT:
                res = "The expression waiting on the link is not a valid .NET object reference.";
                break;
            case MLE_SYMBOL:
                res = "The expression waiting on the link is a symbol. You cannot use GetObject() to read a symbol because it cannot be represented as a .NET object.";
                break;
            default:                            
                res = "Extended error message not available.";
                break;
        }
        return res;
    }

}

}
