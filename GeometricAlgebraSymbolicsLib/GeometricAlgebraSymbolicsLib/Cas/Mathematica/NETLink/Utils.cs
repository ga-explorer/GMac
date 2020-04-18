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

/// <exclude/>
/// <summary>
/// Some useful utility methods, all static.
/// </summary>
/// <remarks>
/// This class is public only in case some functionality proves to be needed "in the field" by a
/// programmer who can be told about its existence. It is not part of the .NET/Link API.
/// </remarks>
/// 
public abstract class Utils {

    private static bool isWin;
    private static bool isMac;
    private static bool isMono;
    private static bool is64Bit;

    static Utils() {
        string os = System.Environment.OSVersion.ToString();
        isWin = os.IndexOf("Windows") != -1;
        is64Bit = IntPtr.Size == 8;
        if (isWin) {
            isMac = false;
        } else {
            // Would be nice if there was a better way, but System.Environment.OSVersion is not
            // useful atm for differentiating Unix and OSX.
            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo("uname", "-s");
            si.RedirectStandardOutput = true;
            si.UseShellExecute = false;
            string outputString = string.Empty;
            using (System.Diagnostics.Process p = System.Diagnostics.Process.Start(si)) {
                p.Start();
                outputString = p.StandardOutput.ReadLine();
                p.WaitForExit();
            }
            isMac = string.Compare(outputString, "darwin", true) == 0;
        }

        isMono = false;
        try {
            // TODO: Update this when Mono decides on a documented way to determine this.
            // Until now, just check for the existence of a Mono type.
            Type.GetType("Mono.Math.BigInteger", true);
            isMono = true;
        } catch (Exception) {}
    }

    public static bool IsWindows {
        get { return isWin; }
    }

    public static bool Is64Bit {
        get { return is64Bit; }
    }

    public static bool IsMac {
        get { return isMac; }
    }

    public static bool IsMono {
        get { return isMono; }
    }


    public static string ConvertCRLF(string s) {

        if (s == null)
            return s;
        int crPos = s.IndexOf('\x000a');
        return crPos == -1 ? s : s.Replace("\u000a", "\u000d\u000a");
    }


    // Junk chars in reals; used in DecimalFromString.
    private static char[] junkChars = {' ', '\0'};


    // Throws FormatException, OverflowException.
    public static decimal DecimalFromString(string s) {
        
        // Need to accommodate InputForm bigdecimal, e.g. -1.234567...89e35\0`53.101 or -1.234567...89`53.101*^35\0`53.101.
        // Note that I probably need to respect the precision info that is supplied via the numbermark.
        // The idea is to extract the digits as a big integer and then determine the scale. These 
        // are the components we need for the BigDecimal constructor.
        // Note that I probably need to respect the precision info that is supplied via the numbermark.

        // Cannot start with a leading .
        if (s[0] == '.')
            s = "0" + s;

        // For some reason the kernel can write real numbers with spaces embedded and only junk afterwards,
        // and when reading reals MLGetString and related funcs will convert spaces into 0 chars (\0, not '0'),
        // so the first thing we do is truncate the string at the first \0 char (actually, because it is not
        // clear that all versions of the kernel will do this conversion, truncate at either ' ' or \0).
        int junkPos = s.IndexOfAny(junkChars);
        if (junkPos != -1)
            s = s.Substring(0, junkPos);

        int ePos = s.IndexOf('e');
        int numberMarkPos = s.IndexOf('`');
        int expMarkPos = ePos != -1 ? -1 : s.IndexOf('*');

        if (ePos != -1) {
            // Drop everything after `.
            if (numberMarkPos != -1)
                s = s.Substring(0, numberMarkPos);
            // Must have a sign after e (M leaves out + sign).
            if (Char.IsDigit(s[ePos + 1]))
                s = s.Insert(ePos + 1, "+");
        } else if (expMarkPos != -1) {
            int numberEnd = numberMarkPos != -1 ? numberMarkPos : expMarkPos;
            s = s.Substring(0, numberEnd) + "e" + (Char.IsDigit(s[expMarkPos + 2]) ? "+" : "") + s.Substring(expMarkPos + 2);
        } else {
            // No e or *^ notation. Just Drop everything after `.
            if (numberMarkPos != -1)
                s = s.Substring(0, numberMarkPos);
        }

        // Float style allows e notation.
        return Decimal.Parse(s, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo);
    }


    // These next methods are called by implementors of the KernelLink "evaluateTo" methods. It is useful to separate the
    // code to create the appropriate expression to send (which is what these methods do) and the code that runs the
    // reading loop. In these methods, obj must be a string or Expr.

    // This is ready to accommodate an evaluateToMathML() function, but I have not added such a function to KernelLink yet.
    public static void WriteEvalToStringExpression(IMathLink ml, Object obj, int pageWidth, string format) {

        ml.PutFunction("EvaluatePacket", 1);
        ml.PutFunction("ToString", 3);
        if (obj is string)
            ml.PutFunction("ToExpression", 1);
        ml.Put(obj);
        ml.PutFunction("Rule", 2);
        ml.PutSymbol("FormatType");
        ml.PutSymbol(format);
        ml.PutFunction("Rule", 2);
        ml.PutSymbol("PageWidth");
        if (pageWidth > 0)
            ml.Put(pageWidth);
        else
            ml.PutSymbol("Infinity");
        ml.EndPacket();
    }

    public static void WriteEvalToTypesetExpression(IMathLink ml, object obj, int pageWidth, string graphicsFmt, bool useStdForm) {

        ml.PutFunction("EvaluatePacket", 1);
        int numArgs = 2 + (useStdForm ? 0 : 1) + (pageWidth > 0 ? 1 : 0);
        ml.PutFunction("EvaluateToTypeset", numArgs);
        ml.Put(obj);
        if (!useStdForm)
            ml.PutSymbol("TraditionalForm");
        if (pageWidth > 0)
            ml.Put(pageWidth);
        ml.Put(graphicsFmt);
        ml.EndPacket();
    }

    public static void WriteEvalToImageExpression(IMathLink ml, object obj, int width, int height, string graphicsFmt, int dpi, bool useFE) {
        
        ml.PutFunction("EvaluatePacket", 1);
        int numArgs = 2 + (useFE ? 1 : 0) + (dpi > 0 ? 1 : 0) + (width > 0 || height > 0 ? 1 : 0);
        ml.PutFunction("EvaluateToImage", numArgs);
        ml.Put(obj);
        if (useFE)
            ml.Put(true);
        ml.Put(graphicsFmt);
        if (dpi > 0) {
            ml.PutFunction("Rule", 2);
            ml.PutSymbol("ImageResolution");
            ml.Put(dpi);
        }
        if (width > 0 || height > 0) {
            ml.PutFunction("Rule", 2);
            ml.PutSymbol("ImageSize");
            ml.PutFunction("List", 2);
            if (width > 0)
                ml.Put(width);
            else
                ml.PutSymbol("Automatic");
            if (height > 0)
                ml.Put(height);
            else
                ml.PutSymbol("Automatic");
        }
        ml.EndPacket();
    }


    /**********************************  End of Public  ************************************/
   

    // Handles names that may have been mangled for Mma use by replacing _ with U.
    internal static bool memberNamesMatch(string actualMemberName, string nameFromMma) {

        if (nameFromMma.Length != actualMemberName.Length)
            return false;
        for (int i = 0; i < actualMemberName.Length; i++) {
            char c1 = nameFromMma[i];
            char c2 = actualMemberName[i];
            if (!(c1 == c2 || c1 == 'U' && c2 == '_'))
                return false;
        }
        return true;
    }


    // Updates an array of indices to march through an array sequentially. Updates indices in place
    // and returns true to indicate that more elements remain to be read (that is, true == indices
    // array refers to a valid unread element).
    internal static bool nextIndex(int[] indices, int[] lengths) {
    
        int lastDim = lengths.Length;
        while (lastDim-- > 0) {
            if (indices[lastDim] < lengths[lastDim] - 1) {
                indices[lastDim]++;
                break;
            } else if (lastDim == 0) {
                return false;
            } else {
                indices[lastDim] = 0;
            }
        }
        return true;
    }

    // probably create a LinkUtils class for this and other link-related funcs....

    // OK to throw various exceptions trying to read data as the wrong type.
    internal static object readArgAs(IKernelLink ml, int argType, Type t) {
        
        // For our purposes here, type& is the same as type. Thus we drop the &, which will otherwise
        // confound our type testing below.
        if (t.IsByRef)
            t = t.GetElementType();

        if (t == typeof(Expr))
            return ml.GetExpr();

        if (argType == Install.ARGTYPE_OBJECTREF || argType == Install.ARGTYPE_NULL) {
            object obj = ml.GetObject();
            if (Object.ReferenceEquals(obj, System.Reflection.Missing.Value))
                // Don't try to convert Missing. Just pass it on.
                return obj;
            else if ((Utils.IsTrulyPrimitive(t) || t == typeof(decimal)) && obj.GetType() != t)
                // If the type to read as is primitive/numeric but the arriving arg is an object ref, convert the object
                // to the correct type. We are just casting to the correct type here in the case where the arg arrives as an
                // object ref instead of a raw value.
                return Convert.ChangeType(obj, t);
            else
                return obj;
        }

        if (argType == Install.ARGTYPE_MISSING) {
            // Throw away the "Default" symbol.
            ml.GetSymbol();
            return System.Reflection.Missing.Value;
        }

        // Allow enums to be sent as ints from Mathematica.
        if (argType == Install.ARGTYPE_INTEGER && t.IsEnum) {
            Type enumBaseType = Enum.GetUnderlyingType(t);
            object val = readArgAs(ml, argType, enumBaseType);
            // Enum.IsDefined(t, val) is not suitable for testing the validity of the value unless the enum is _not_ a bitflag,
            // because a bitor of values will not pass that test. We just forego range checking for bitflags enums.
            if (!Enum.IsDefined(t, val) && t.GetCustomAttributes(typeof(FlagsAttribute), false).Length == 0)
                throw new MathLinkException(MathLinkException.MLE_BAD_ENUM, "Enum value " + val + " out of range for type " + t.FullName + ".");
            return Enum.ToObject(t, val);
        }

        if ((argType == Install.ARGTYPE_INTEGER || argType == Install.ARGTYPE_REAL ||
                argType == Install.ARGTYPE_COMPLEX) && t == ml.ComplexType) {
            return ml.GetComplex();
        }

        // We have already handled the cases where the arg on the link is an object ref and where it is
        // an int but the type to read as is an enum. Also if t is the complex type and args match that.

        switch (Type.GetTypeCode(t)) {
            case TypeCode.Object: {
                // Note that we allow "weak" boxing here in that you can pass a primitive or array from M for an
                // object argument. But you don't get control of the type that it is read as (e.g., M Integer
                // goes to .NET int, not byte).
                if (argType == Install.ARGTYPE_INTEGER)
                    return ml.GetInteger();
                else if (argType == Install.ARGTYPE_REAL)
                    return ml.GetDouble();
                else if (argType == Install.ARGTYPE_STRING)
                    return ml.GetString();
                else if (argType == Install.ARGTYPE_NULL) {
                    ml.GetSymbol();
                    return null;
                } else if (argType == Install.ARGTYPE_BOOLEAN)
                    return ml.GetBoolean();
                else if (argType == Install.ARGTYPE_COMPLEX)
                    return ml.GetComplex();
                else if (argType == Install.ARGTYPE_OTHER)
                    throw new ArgumentException();
                else {
                    // VECTOR, MATRIX, TENSOR3, LIST
                    if (t == typeof(Array) || t == typeof(object)) {
                        return readArbitraryArray(ml, typeof(Array));
                    } else if (t.IsArray) {
                        Type elementType = t.GetElementType();
                        // All arrays-of-arrays are read the slow way.
                        if (elementType.IsArray) {
                            return readArbitraryArray(ml, t);
                        } else {
                            // Must be an array like [,...], not [,...][,...].
                            return ml.GetArray(elementType, t.GetArrayRank());
                        }
                    } else if (t.IsPointer && argType == Install.ARGTYPE_VECTOR) {
                        // Allow flat M lists to be passed for pointer args.
                        Type elementType = t.GetElementType();
                        return readArgAs(ml, Install.ARGTYPE_VECTOR, TypeLoader.GetType(elementType.FullName + "[]", true));
                    } else {
                        throw new ArgumentException();
                    }
                }
            }
            case TypeCode.Byte: 
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return (byte) ml.GetInteger();
            case TypeCode.SByte:
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return (sbyte) ml.GetInteger();
            case TypeCode.Char:
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return (char) ml.GetInteger();
            case TypeCode.Int16:
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return (short) ml.GetInteger();
            case TypeCode.UInt16:
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return (ushort) ml.GetInteger();
            case TypeCode.Int32:
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return ml.GetInteger();
            case TypeCode.UInt32: 
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return (uint) ml.GetDecimal();
            case TypeCode.Int64:
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return (long) ml.GetDecimal();
            case TypeCode.UInt64:
                if (argType != Install.ARGTYPE_INTEGER) 
                    throw new ArgumentException();
                return (ulong) ml.GetDecimal();
            case TypeCode.Single:
                if (argType != Install.ARGTYPE_REAL && argType != Install.ARGTYPE_INTEGER)
                    throw new ArgumentException();
                return (float) ml.GetDouble();
            case TypeCode.Double:
                if (argType != Install.ARGTYPE_REAL && argType != Install.ARGTYPE_INTEGER)
                    throw new ArgumentException();
                return ml.GetDouble();
            case TypeCode.Decimal:
                if (argType != Install.ARGTYPE_REAL && argType != Install.ARGTYPE_INTEGER)
                    throw new ArgumentException();
                return ml.GetDecimal();
            case TypeCode.Boolean:
                if (argType != Install.ARGTYPE_BOOLEAN)
                    throw new ArgumentException();
                return ml.GetBoolean();
            case TypeCode.String:
                if (argType != Install.ARGTYPE_STRING)
                    throw new ArgumentException();
                return ml.GetString();
            case TypeCode.DateTime:
                return (DateTime) ml.GetObject();
            case TypeCode.DBNull:
                // ???
                ml.GetSymbol();
                return DBNull.Value;
            default:
                // No cases unhandled.
                throw new ArgumentException();
        }
    }


    // Can read any kind of array, except for jagged arrays that are multidimensional at the beginning, like [][,]
    // (jagged at start like [,][] or [,...][][] is OK, but only if we know the type, not for an Array slot). Recall
    // that mixed array types are read backwards, so [][,] is a 2-deep array of element type [] (i.e., it is multidimensional
    // at the start, not the end). One more limitation: For an Array argument slot, if the incoming array is 3-deep or deeper,
    // it must be rectangular, not jagged.
    // We know the incoming expression has head List.
    internal static Array readArbitraryArray(IMathLink ml, Type t) {

        int len;
        if (t == typeof(Array)) {
            ExpressionType leafExprType;
            Type leafType;
            // For the Array type, we have no clue about how the leaf elements will be read. Thus we need to peek into
            // the incoming data to decide what type it is.
            int depth = determineIncomingArrayDepth(ml);
            ILinkMark mark = ml.CreateMark();
            ILoopbackLink loop = null;
            try {
                if (depth == 1) {
                    len = ml.CheckFunction("List");
                    if (len == 0)
                        throw new MathLinkException(MathLinkException.MLE_EMPTY_ARRAY);
                    leafExprType = ml.GetNextExpressionType();
                    ml.SeekMark(mark);
                    leafType = netTypeFromExpressionType(leafExprType, ml);
                    // Fail if data could only be read as Expr. This means that we cannot pass a list of arbitrary expressions
                    // to an arg slot typed as Array. It would have to be typed as Expr[]. We make this choice to provide
                    // more meaningful error reporting for the cases where the array has bogus data in it. We assume that
                    // this convenience outweighs the very rare cases where a programmer would want to pass an array of exprs
                    // for an Array slot (they could always create the array separately and pass it as an object reference).
                    if (leafType == typeof(Expr)) {
                        if (leafExprType == ExpressionType.Complex)
                            throw new MathLinkException(MathLinkException.MLE_NO_COMPLEX);
                        else
                            throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY);
                    }
                    return (Array) ml.GetArray(leafType, 1);
                } else if (depth == 2) {
                    // The loopback link is just a utility for quickly reading expressions off the link (via TransferExpression).
                    // Nothing is ever read back off the loopback link.
                    loop = MathLinkFactory.CreateLoopbackLink();
                    // Determine if the array is rectangular or jagged.
                    len = ml.CheckFunction("List");
                    bool isJagged = false;
                    bool foundLeafType = false;
                    // This next assignment is strictly for the compiler. The value will never be used
                    // unless it is set to an actual value below. We have an existing function that will get
                    // the leafExprType of the incoming array (getLeafExprType()), but we also need to check if the array
                    // is jagged here, so we will do both tasks at once and save a little time.
                    leafExprType = ExpressionType.Integer;
                    int lenAtLevel2 = ml.CheckFunction("List");
                    if (lenAtLevel2 > 0) {
                        leafExprType = ml.GetNextExpressionType();
                        foundLeafType = true;
                    }
                    // peel off all the elements in the first sublist.
                    for (int j = 0; j < lenAtLevel2; j++)
                        loop.TransferExpression(ml);
                    // For each remaining sublist, check its length and peel off its members. At this point, though, we
                    // cannot be guaranteed that all elements are lists (or, at least, guaranteed that it is an error
                    // if they are not). They could be Null if the array is jagged: {{1, 2}, Null}.
                    for (int i = 1; i < len; i++) {
                        ExpressionType nextExprType = ml.GetNextExpressionType();
                        if (nextExprType == ExpressionType.Object) {
                            isJagged = true;
                            // OK to have null as an element in a jagged array.
                            if (ml.GetObject() != null)
                                throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_SHAPE);
                        } else if (nextExprType == ExpressionType.Function) {
                            int thisLength = ml.CheckFunction("List");
                            if (!foundLeafType && thisLength > 0) {
                                leafExprType = ml.GetNextExpressionType();
                                foundLeafType = true;
                            }
                            if (thisLength != lenAtLevel2) {
                                isJagged = true;
                                break;
                            } else {
                                for (int j = 0; j < thisLength; j++)
                                    loop.TransferExpression(ml);
                            }
                        } else {
                            throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                        }
                    }
                    // If the array is empty we cannot create a .NET array for it, as there is no type info for the array we are creating.
                    if (!foundLeafType)
                        throw new MathLinkException(MathLinkException.MLE_EMPTY_ARRAY);
                    leafType = netTypeFromExpressionType(leafExprType, ml);
                    ml.SeekMark(mark);
                    if (isJagged) {
                        ml.CheckFunction("List");
                        Array result = Array.CreateInstance(Array.CreateInstance(leafType, 0).GetType(), len);
                        for (int i = 0; i < len; i++) {
                            // Have to check if elements are lists or Null.
                            ExpressionType nextExprType = ml.GetNextExpressionType();
                            if (nextExprType == ExpressionType.Function) {
                                result.SetValue(ml.GetArray(leafType, 1), i);
                            } else {
                                string sym = ml.GetSymbol();
                                if (sym != "Null")
                                    throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_SHAPE);
                                result.SetValue(null, i);
                            }
                        }
                        return result;
                    } else {
                        return ml.GetArray(leafType, 2);
                    }
                } else {
                    // For an Array argument slot, we only support passing >2-deep rectangular arrays, not jagged. 
                    for (int i = 0; i < depth; i++)
                        ml.CheckFunction("List");
                    leafExprType = ml.GetNextExpressionType();
                    leafType = netTypeFromExpressionType(leafExprType, ml);
                    ml.SeekMark(mark);
                    return ml.GetArray(leafType, depth);
                }
            } finally {
                if (loop != null)
                    loop.Close();
                ml.DestroyMark(mark);
            }
        } else {
            // We have the actual array shape encoded in the type. Either [], [,..], or array-of-arrays: [][]....
            int arrayRank = t.GetArrayRank();
            Type elementType = t.GetElementType();
            if (elementType.IsArray) {
                if (arrayRank > 1)
                    // Don't support multidimensional array at start of jagged array: [][,]. Recall that mixed array types
                    // are read backwards, so [][,] is a 2-deep array of element type [].
                    throw new MathLinkException(MathLinkException.MLE_MULTIDIM_ARRAY_OF_ARRAY);
                len = ml.CheckFunction("List");
                Array result = Array.CreateInstance(elementType, len);
                for (int i = 0; i < len; i++) {
                    ExpressionType nextExprType = ml.GetNextExpressionType();
                    if (nextExprType == ExpressionType.Function) {
                        result.SetValue(readArbitraryArray(ml, elementType), i);
                    } else {
                        string sym = ml.GetSymbol();
                        if (sym != "Null")
                            throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_SHAPE);
                        result.SetValue(null, i);
                    }
                }
                return result;
            } else if (elementType == typeof(Array)) {
                // Don't support Array[].
                throw new MathLinkException(MathLinkException.MLE_ARRAY_OF_ARRAYCLASS);
            } else {
                return ml.GetArray(elementType, arrayRank);
            }
        }
    }


    internal static void discardNext(IKernelLink ml) {

        switch (ml.GetNextExpressionType()) {
            case ExpressionType.Integer:
                ml.GetInteger();
                break;
            case ExpressionType.Real:
                ml.GetDouble();
                break;
            case ExpressionType.Boolean:
            case ExpressionType.Symbol:
                ml.GetSymbol();
                break;
            case ExpressionType.String:
                ml.GetString();
                break;
            case ExpressionType.Object:
                ml.GetObject();
                break;
            // We would get an exception if we called GetComplex() and no complex class was set.
            case ExpressionType.Complex:
            case ExpressionType.Function: {
                IMathLink loop = MathLinkFactory.CreateLoopbackLink();
                try {
                    loop.TransferExpression(ml);
                } finally {
                    loop.Close();
                }
                break;
            }
            default:
                System.Diagnostics.Debug.Fail("Unhandled ExpressionType enum value in Utils.discardNext().");
                break;
        }
    }


    // Leaves the link in the same state as when it started. The out param leafExprType will be Function if
    // the array is empty in the first bottom-level subarray: {{}, {1,2,3}}.
    internal static int determineIncomingArrayDepth(IMathLink ml/*, out ExpressionType leafExprType*/) {

        int actualDepth = 0;
        ILinkMark mark = ml.CreateMark();
//        // Initial value just to satisfy compiler; if depth of 0 is returned, then the leafExprType will be ignored by callers.
//        leafExprType = ExpressionType.Function;
        try {
            string head = ml.GetFunction(out var len);
            actualDepth = 1;
            while (len > 0) {
                ExpressionType leafExprType = ml.GetNextExpressionType();
                if (leafExprType == ExpressionType.Function) {
                    head = ml.GetFunction(out len);
                    actualDepth++;
                } else {
                    break;
                }
            }
//            // If the last sublist is empty (len == 0), we will walk off the end of the above loop with
//            // leafExprType == ExpressionType.Function, which is what we want.
        } catch (MathLinkException) {
            // Do nothing but clear it. Returning 0 is enough.
            ml.ClearError();
        } finally {
            ml.SeekMark(mark);
            ml.DestroyMark(mark);
        }
        return actualDepth;
    }


    // Docs for Type.IsPrimitive lie! They fail to note that IntPtr also gives true. I want a function that says
    // false for IntPtr, as it will be an object reference in .NET/Link. This function should be used probably
    // everywhere instead of Type.IsPrimitive.
    internal static bool IsTrulyPrimitive(Type t) {
        return t.IsPrimitive && t != typeof(IntPtr);
    }


    // Adds "System." to beginning of type name if it has no namespace. This allows users to specify types
    // like Int32 instead of System.Int32.
    internal static string addSystemNamespace(string typeName) {
        return typeName.IndexOf('.') == -1 ? ("System." + typeName) : typeName;
    }


    // It is not enought to just test IsOut, because you can have a method tagged as [in][out]. Normally
    // "in/out" params are byref, and show up in the IL as type& without any [in] or [out] metadata tags.
    // But users could explicitly use [in][out], and another common case is an interop assembly created
    // by tlbimp, which will have [in][out] tags for pointer params (e.g., VB ByRef params).
    internal static bool IsOutOnlyParam(System.Reflection.ParameterInfo pi) {
        return pi.IsOut && !pi.IsIn;
    }


    // Gives default types to read expressions as. Used when two conditions are met: 1) we have no .NET type info
    // about what we are trying to create, and (2) we have no argType info from M because this is a member of an array
    // (argType tells us merely that it is an array). Will return typeof(Expr) for anything that doesn't map to a .NET type.
    private static Type netTypeFromExpressionType(ExpressionType exprType, IMathLink ml) {

        switch (exprType) {
            case ExpressionType.Integer:
                return typeof(int);
            case ExpressionType.Real:
                return typeof(double);
            case ExpressionType.String:
                return typeof(string);
            case ExpressionType.Boolean:
                return typeof(bool);
            case ExpressionType.Symbol:
            case ExpressionType.Function:
                return typeof(Expr);
            case ExpressionType.Object:
                return typeof(object);
            case ExpressionType.Complex:
                return ml.ComplexType != null ? ml.ComplexType : typeof(Expr);
            default:
                // Protection in case we ever add a new value to the enum.
                System.Diagnostics.Debug.Fail("Unhandled ExpressionType enum value in Utils.netTypeFromExpressionType");
                return null;
        }
    }

}

}
