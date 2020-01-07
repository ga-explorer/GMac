//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

/*
   TODO: Support depth > 2 in AsArray.
   
   Add types for wider primitive arrays: LONGARRAY1, DECIMALARRAY1.
    
   Use IKernelLink instead of IMathLink? That enables object support....   
*/


using System;
using System.Runtime.Serialization;
using System.Diagnostics;
using Wolfram.NETLink.Internal;

namespace Wolfram.NETLink {

/// <summary>
/// A representation of arbitrary <i>Mathematica</i> expressions in .NET.
/// </summary>
/// <remarks>
/// The Expr class is a representation of arbitrary <i>Mathematica</i> expressions in .NET. Exprs are created by reading an
/// expression from a link (using the <see cref="IMathLink.GetExpr"/> method), they can be decomposed into component
/// Exprs with properties and methods like <see cref="Head"/> and <see cref="Part"/>, and their structure can be queried
/// with methods like <see cref="Length"/>, <see cref="NumberQ"/>, and <see cref="VectorQ"/>. All these methods will be familiar
/// to <i>Mathematica</i> programmers, and their Expr counterparts work similarly.
/// <para>
/// Like <i>Mathematica</i> expressions, Exprs are immutable, meaning they can never be changed once they are created.
/// Operations that might appear to modify an Expr (like <see cref="Delete"/>) return new modified Exprs without changing the original.
/// Because Exprs are immutable, they are also thread-safe, meaning that any number of threads can access a given
/// Expr at the same time.
/// </para>
/// Exprs are stored initially in a very efficient way, and they can be created and written to links very quickly.
/// When you call operations that inspect their structure or that extract component parts, however, it is likely
/// that they must be unpacked into a more .NET-native form that requires more memory. 
/// <para>
/// In its present state, Expr has four main uses:
/// </para>
/// (1) Storing expressions read from a link so that they can be later written to another link. This use replaces
/// functionality that C-language programmers would use a loopback link for. (.NET/Link has an <see cref="ILoopbackLink"/> interface
/// as well, but Expr affords an even easier method.) 
/// <code>
/// Expr e = ml.GetExpr();
/// // ... Later, write it to a different MathLink:
/// otherML.Put(e);
/// e.Dispose();
/// </code>
/// Note that if you just want to move an expression immediately from one link to another, you can use the IMathLink method
/// <see cref="IMathLink.TransferExpression"/> and avoid creating an Expr to store it. 
/// <para>
/// (2) Many of the <see cref="IKernelLink"/> methods take either a string or an Expr. If it is not convenient to build a string of
/// <i>Mathematica</i> input, you can use an Expr. There are two ways to build an Expr: you can use a constructor, or you
/// can create a loopback link as a scratchpad, build the expression on this link with a series of Put calls, then read the
/// expression off the loopback link using GetExpr. Here is an example that creates an Expr that represents 2+2 and computes
/// it in <i>Mathematica</i> using these two techniques:
/// </para>
/// <code>
/// // First method: Build it using Expr constructors:
/// Expr symbolPlus = new Expr(ExpressionType.Symbol, "Plus");
/// Expr e1 = new Expr(symbolPlus, 2, 2);
/// // ml is a KernelLink
/// string result = ml.EvaluateToOutputForm(e1, 0);
///    
/// // Second method: Build it on an ILoopbackLink with MathLink calls:
/// ILoopbackLink loop = MathLinkFactory.CreateLoopbackLink();
/// loop.PutFunction("Plus", 2);
/// loop.Put(2);
/// loop.Put(2);
/// Expr e2 = loop.GetExpr();
/// loop.Close();
/// result = ml.EvaluateToOutputForm(e2, 0);
/// e2.Dispose();
/// </code>
/// (3) Getting a string representation of an expression. Sometimes you want to be able to produce a readable string form
/// of an entire expression, particularly for debugging. The <see cref="ToString"/> method will do this for you: 
/// <code>
/// // This code will print out the next expression waiting on the link without
/// // consuming it, so that the state of the link is unchanged:
/// Console.WriteLine("Next expression is: " + ml.PeekExpr());
/// </code>
/// (4) Examining the structure or properties of an expression. Although it is possible to do this sort of thing
/// with MathLink calls, it is very difficult in general. Expr lets you read an entire expression from a link
/// and then examine it using a very high-level interface and without having to worry about managing your current
/// position in an incoming stream of data. Expr has <i>Mathematica</i>-like methods and properties like
/// <see cref="Head"/>, <see cref="Part"/>, <see cref="IntegerQ"/>, <see cref="VectorQ"/>, and many others to
/// assist in examining an expression.
/// </remarks>
/// 
[Serializable]
public sealed class Expr : IDisposable, ISerializable {

    // INTEGER or REAL could have val be a decimal
    private const int INTEGER = 1;
    private const int REAL = 2;
    private const int STRING = 3;
    private const int SYMBOL = 4;

    private const int UNKNOWN = 0;  // The loopback link hasn't been unwound.
    // Next val is never assigned to any Expr; just a sentinel value.
    private const int FIRST_COMPOSITE = 100;
    private const int FUNCTION = 100;
    // Next val is never assigned to any Expr; just a sentinel value. All array types must be larger.
    private const int FIRST_ARRAY_TYPE = 200;
    private const int INTARRAY1 = 200;
    private const int REALARRAY1 = 201;
    private const int INTARRAY2 = 202;
    private const int REALARRAY2 = 203;

    public static readonly Expr SYM_SYMBOL   = new Expr(ExpressionType.Symbol, "Symbol");  // Must be first among the SYM_xxx defs.
    public static readonly Expr SYM_INTEGER = new Expr(ExpressionType.Symbol, "Integer");
    public static readonly Expr SYM_REAL = new Expr(ExpressionType.Symbol, "Real");
    public static readonly Expr SYM_STRING = new Expr(ExpressionType.Symbol, "String");
    public static readonly Expr SYM_LIST = new Expr(ExpressionType.Symbol, "List");
    public static readonly Expr SYM_TRUE = new Expr(ExpressionType.Symbol, "True");
    public static readonly Expr SYM_FALSE = new Expr(ExpressionType.Symbol, "False");
    public static readonly Expr INT_ONE = new Expr(1);
    public static readonly Expr INT_ZERO = new Expr(0);
    public static readonly Expr INT_MINUSONE = new Expr(-1);


    /***********************  Private instance data  ****************************/

    private int type;
    private Expr head;
    private Expr[] args;
    private object val;  // Used for exprs not stored in head/args form (arrays of real/int, atomic types)
    [NonSerialized] private ILoopbackLink link;
    // Cache the hash since Exprs are immutable. Make it volatile, as Exprs are thread-safe.
    private volatile int cachedHashCode = 0;


    /*********************************  Constructors  ***************************************/

    private Expr() {}


    /// <overloads>
    /// <summary>
    /// Creates a new Expr object.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Creates an Expr representing a Mathematica Integer, Real, String, or Symbol whose value is
    /// given by the supplied string (for example "2", "3.14", "Plus", or "True").
    /// </summary>
    /// <remarks>
    /// Creates only the following types: ExpressionType.Integer, ExpressionType.Real, ExpressionType.String,
    /// ExpressionType.Symbol, ExpressionType.Boolean.
    /// </remarks>
    /// <param name="type">The type of Expr to create.</param>
    /// <param name="val">The Expr's value.</param>
    /// <exception cref="ArgumentException">If an unsupported type is specified.</exception>
    /// 
    public Expr(ExpressionType type, string val) {
        
        this.type = internalTypeFromExpressionType(type);
        switch (type) {
            case ExpressionType.Integer:
                this.head = SYM_INTEGER;
                try {
                    this.val = Convert.ToInt64(val);
                } catch (Exception) {
                    this.val = Convert.ToDecimal(val);
                }
                break;
            case ExpressionType.Real:
                this.head = SYM_REAL;
                try {
                    this.val = Convert.ToDouble(val, System.Globalization.NumberFormatInfo.InvariantInfo);
                } catch (Exception) {
                    this.val = Convert.ToDecimal(val, System.Globalization.NumberFormatInfo.InvariantInfo);
                }
                break;
            case ExpressionType.String:
                this.head = SYM_STRING;
                this.val = val;
                break;
            case ExpressionType.Symbol:
                if (val == "Symbol")
                    this.head = this; // Can't use SYM_SYMBOL here as that triggers recursion in the SYM_SYMBOL initializer.
                else
                    this.head = SYM_SYMBOL;
                this.val = val;
                break;
            case ExpressionType.Boolean:
                this.head = SYM_SYMBOL;
                this.val = val == "True" || val == "true" ? true : false;
                break;
            default:
                throw new ArgumentException("ExpressionType " + type + " is not supported in the Expr(ExpressionType, string) constructor.");
        }
    }

    
    /// <summary>
    /// Creates an Expr whose value is given by the supplied object.
    /// </summary>
    /// <remarks>
    /// The object can be a boxed primitive (e.g, Int32, Double, Boolean etc.) a String, an Expr, or an array.
    /// <para>
    /// If it is an array, it must be a 1- or 2-dimensional array of primitive types or strings. It cannot be jagged
    /// (that is, it must be typed like int[,], not int[][]). The data in the array is copied into the Expr, so future
    /// changes to the original array will not be reflected in the Expr.
    /// </para>
    /// </remarks>
    /// <param name="obj">The object whose value will be used for the Expr.</param>
    /// <exception cref="ArgumentException">If the object is of a type that cannot be handled.</exception>
    ///
    public Expr(object obj) {
        
        if (obj == null) {
            val = "Null";
            type = SYMBOL;
            head = SYM_SYMBOL;
        } else if (obj is int || obj is byte || obj is sbyte || obj is short || obj is ushort || 
                     obj is char || obj is uint || obj is long) {
            val = Convert.ToInt64(obj);
            type = INTEGER;
            head = SYM_INTEGER;
        } else if (obj is ulong) {
            val = Convert.ToDecimal(obj);
            type = INTEGER;
            head = SYM_INTEGER;
        } else if (obj is double) {
            val = obj;
            type = REAL;
            head = SYM_REAL;
        } else if (obj is float) {
            val = Convert.ToDouble(obj);
            type = REAL;
            head = SYM_REAL;
        } else if (obj is decimal) {
            val = obj;
            if (Decimal.Truncate((decimal)this.val) == (decimal) this.val) {
                type = INTEGER;
                head = SYM_INTEGER;
            } else {
                type = REAL;
                head = SYM_REAL;
            }
        } else if (obj is string) {
            val = obj;
            type = STRING;
            head = SYM_STRING;
        } else if (obj is bool) {
            val = ((bool) obj) ? "True" : "False";
            type = SYMBOL;
            head = SYM_SYMBOL;
        } else if (obj is Expr) {
            // Since Exprs are immutable, there is little reason for this "copy constructor". The only known reason
            // is that because Dispose disposes all subExprs, you might want to duplicate an existing Exprif you want to call Dispose on an Expr but want to be sure tha
            ((Expr)obj).prepareFromLoopback();
            type = ((Expr)obj).type;
            head = ((Expr)obj).head;
            args = ((Expr)obj).args;
            val = ((Expr)obj).val;
        } else if (obj is Array || obj.GetType() == typeof(Array)) {
            Array a = (Array) obj;
            if (a is int[] || a is byte[] || a is sbyte[] || a is short[] || a is ushort[] || a is char[]) {
                val = new int[a.Length];
                Array.Copy(a, (Array) val, a.Length);
                type = INTARRAY1;
            } else if (a is uint[] || a is long[] || a is ulong[]) {
                int[] newArray = new int[a.Length];
                // Manually truncate for arrays wider than int. Fix this later by adding wider tpyes: LONGARRAY1.
                for (int i = 0; i < a.Length; i++)
                    newArray[i] = (int) a.GetValue(i);
                val = newArray;
                type = INTARRAY1;
            } else if (a is double[] || a is float[]) {
                val = new double[a.Length];
                Array.Copy(a, (Array) val, a.Length);
                type = REALARRAY1;
            } else if (a is decimal[]) {
                // Note that decimal arrays are always typed as REALARRAY.
                double[] newArray = new double[a.Length];
                // Manually truncate for arrays wider than double. Fix this later by adding wider types: DECIMALARRAY1.
                for (int i = 0; i < a.Length; i++)
                    newArray[i] = (double) a.GetValue(i);
                val = newArray;
                type = REALARRAY1;
            } else if (a is string[]) {
                val = new string[a.Length];
                Array.Copy(a, (Array) val, a.Length);
                type = FUNCTION;
            } else if (a is bool[]) {
                val = new string[a.Length];
                for (int i = 0; i < a.Length; i++)
                    ((Array) val).SetValue(((bool) a.GetValue(i)) ? "True" : "False", i);
                type = FUNCTION;
            } else if (a is int[,] || a is byte[,] || a is sbyte[,] || a is short[,] || a is ushort[,] || a is char[,]) {
                val = new int[a.GetLength(0), a.GetLength(1)];
                Array.Copy(a, (Array) val, a.Length);
                type = INTARRAY2;
            } else if (a is double[,] || a is float[,]) {
                val = new double[a.GetLength(0), a.GetLength(1)];
                Array.Copy(a, (Array) val, a.Length);
                type = REALARRAY2;
            } else if (a is ulong[,] || a is uint[,] || a is long[,]) {
                // Manually truncate for arrays wider than int. Fix this later by adding wider tpyes: LONGARRAY2.
                int[,] newArray = new int[a.GetLength(0), a.GetLength(1)];
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); i++)
                        newArray[i,j] = (int) a.GetValue(i,j);
                val = newArray;
                type = INTARRAY2;
            } else if (a is decimal[,]) {
                // Manually truncate for arrays wider than int. Fix this later by adding wider tpyes: DECIMALARRAY2.
                double[,] newArray = new double[a.GetLength(0), a.GetLength(1)];
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); i++)
                        newArray[i,j] = (double) a.GetValue(i,j);
                val = newArray;
                type = REALARRAY2;
            } else if (a is string[,]) {
                val = new string[a.GetLength(0), a.GetLength(1)];
                Array.Copy(a, (Array) val, a.Length);
                type = FUNCTION;
            } else if (a is bool[,]) {
                val = new string[a.GetLength(0), a.GetLength(1)];
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                        ((Array) val).SetValue(((bool) a.GetValue(i,j)) ? "True" : "False", i, j);
                type = FUNCTION;
            } else {
                throw new ArgumentException("Cannot construct an Expr from the supplied array object: " + obj);
            }
        } else {
            throw new ArgumentException("Cannot construct an Expr from the supplied object: " + obj);
        }
    }


    /// <summary>
    /// Creates an Expr with the given head and arguments.
    /// </summary>
    /// <remarks>
    /// The head and args are made into Exprs by calling the Expr constructor on each object, unless they are already Exprs,
    /// in which case they are used directly.
    /// </remarks>
    /// <param name="head">An object giving the head of the new Expr.</param>
    /// <param name="args">A sequence or array of objects giving the arguments of this Expr; pass null or an empty array for no arguments.</param>
    /// <exception cref="ArgumentException">If any of the objects are of a type that cannot be handled.</exception>
    /// 
    public Expr(object head, params object[] args) {

        this.type = FUNCTION;
        this.head = head is Expr ? (Expr) head : new Expr(head);
        this.args = new Expr[args.Length];
        for (int i = 0; i < args.Length; i++)
            this.args[i] = args[i] is Expr ? (Expr) args[i] : new Expr(args[i]);
    }

    
    /***************************************  Static Factory  *************************************/
    
    /// <summary>
    /// Creates an Expr by reading it off a link.
    /// </summary>
    /// <remarks>
    /// This factory method will only be used by advanced programmers who are creating their own classes that
    /// implement the <see cref="IMathLink"/> interface. You would call this method in your implementation of
    /// <see cref="IMathLink.GetExpr">GetExpr</see>. In other words, this method exists not as a means for casual
    /// users to create Exprs from a link (use the  <see cref="IMathLink.GetExpr">GetExpr</see> method instead),
    /// but so that IMathLink implementors can write their own GetExpr methods without having to know anything
    ///  about the internals of the Expr class. Exprs know how to read themselves off a link.
    /// </remarks>
    /// <param name="ml">The link from which the Expr should be read.</param>
    /// <returns>The created Expr</returns>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    public static Expr CreateFromLink(IMathLink ml) {
        return createFromLink(ml, true);
    }
    

    /***************************************  Finalizer  *************************************/
    
    /// <summary>
    /// The finalizer frees resources that the Expr uses internally.
    /// </summary>
    /// <remarks>
    /// This finalizer does nothing but call the <see cref="Dispose"/> method.
    /// </remarks>
    /// 
    ~Expr() {
        disposer();
    }

    /****************************  IDisposable and ISerializable  *****************************/


    /// <summary>
    /// Frees resources that the Expr uses internally.
    /// </summary>
    /// <remarks>
    /// Although this method is called when an Expr object is garbage-collected, you should get in the habit
    /// of calling Dispose when you are finished with an Expr.
    /// <para>
    /// The Expr should not be used after Dispose() has been called.
    /// </para>
    /// Calling Dispose is not necessary, and in fact it has no effect, on Exprs that are created by calling
    /// Expr constructors. It only affects Exprs that are created by reading from a link using the
    /// <see cref="IMathLink.GetExpr">GetExpr</see> or <see cref="IMathLink.PeekExpr">PeekExpr</see> methods.
    /// </remarks>
    /// 
    public void Dispose() {

        disposer();
        GC.SuppressFinalize(this);
    }

    private void disposer() {

        lock (this) {
            if (link != null) {
                link.Close();
                link = null;
            }
        }
    }

    private Expr(SerializationInfo info, StreamingContext context) {

        type = info.GetInt32("type");
        head = (Expr) info.GetValue("head", typeof(Expr));
        args = (Expr[]) info.GetValue("args", typeof(Expr[]));
        val = info.GetValue("val", typeof(object));
        link = null;
        cachedHashCode = 0;
    }


    /// <summary>
    /// Populates the SerializationInfo object with the Expr's internal state information.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    /// 
    public void GetObjectData(SerializationInfo info, StreamingContext context) {

        prepareFromLoopback();
        info.AddValue("type", type);
        info.AddValue("head", head);
        info.AddValue("args", args);
        info.AddValue("val", val);
    }


    /***************************************  ToString  ****************************************/

    /// <summary>
    /// Returns a representation of the Expr as a <i>Mathematica</i> InputForm string.
    /// </summary>
    /// 
    public override string ToString() {

        string s = null;
        
        prepareFromLoopback();
        switch (type) {
            case INTEGER:
            case SYMBOL:
                s = val.ToString();
                break;
            case REAL:
                s = doubleToInputFormString(Convert.ToDouble(val));
                break;
            case STRING: {
                s = val.ToString();
                System.Text.StringBuilder buf = new System.Text.StringBuilder(s.Length + 10);
                buf.Append('"');
                int len = s.Length;
                for (int i = 0; i < len; i++) {
                    char c = s[i];
                    if (c == '\\' || c == '"')
                        buf.Append('\\');
                    buf.Append(c);
                }
                buf.Append('"');
                s = buf.ToString();
                break;
            }
            case FUNCTION: {
                bool isList = ListQ();
                int len = Length;
                System.Text.StringBuilder buf = new System.Text.StringBuilder(len * 2);
                buf.Append(isList ? "{" : (Head.ToString() + "["));
                for (int i = 1; i <= len; i++) {
                    buf.Append(this[i].ToString());
                    if (i < len)
                        buf.Append(',');
                }
                buf.Append(isList ? '}' : ']');
                s = buf.ToString();
                break;
            }
            case INTARRAY1:
            case REALARRAY1: {
                int len = ((Array) val).GetLength(0);
                int[] ia = type == INTARRAY1 ? (int[]) val : null;
                double[] da = type == REALARRAY1 ? (double[]) val : null;
                System.Text.StringBuilder buf = new System.Text.StringBuilder(len * 2);
                buf.Append('{');
                for (int i = 0; i < len; i++) {
                    buf.Append(type == INTARRAY1 ? ia[i].ToString() : doubleToInputFormString(da[i]));
                    if (i < len - 1)
                        buf.Append(',');
                }
                buf.Append('}');
                s = buf.ToString();
                break;
            }
            case INTARRAY2:
            case REALARRAY2: {
                int len1 = ((Array) val).GetLength(0);
                int len2 = ((Array) val).GetLength(1);
                int[,] ia = type == INTARRAY2 ? (int[,]) val : null;
                double[,] da = type == REALARRAY2 ? (double[,]) val : null;
                System.Text.StringBuilder buf = new System.Text.StringBuilder(len1 * len2 * 2);
                buf.Append('{');
                for (int i = 0; i < len1; i++) {
                    buf.Append('{');
                    for (int j = 0; j < len2; j++) {
                        buf.Append(type == INTARRAY2 ? ia[i,j].ToString() : doubleToInputFormString(da[i,j]));
                        if (j < len2 - 1)
                            buf.Append(',');
                    }
                    buf.Append(i < len1 - 1 ? "}," : "}");
                }
                buf.Append('}');
                s = buf.ToString();
                break;
            }
            default:
                Debug.WriteLine("Bad type in Expr.ToString(): " + type);
                break;
        }
        return s;
    }

    /***************************************  Equals and HashCode  ****************************************/


    /// <summary>
    /// Implements a value-based equality comparison that is similar to <i>Mathematica</i>'s SameQ.
    /// </summary>
    /// <seealso cref="Equals"/>
    /// 
    public static bool operator ==(Expr x, Expr y) {
        if ((object)x == null)
            return ((object)y == null);
        else
            return x.Equals(y);
    }


    /// <summary>
    /// Implements a value-based inequality comparison.
    /// </summary>
    /// <seealso cref="Equals"/>
    /// 
    public static bool operator !=(Expr x, Expr y) {
        return !(x == y);
    }


    /// <summary>
    /// Implements a value-based equality comparison that is similar to <i>Mathematica</i>'s SameQ.
    /// </summary>
    /// <remarks>
    /// It is not guaranteed to have the same behavior as SameQ.
    /// </remarks>
    ///
    public override bool Equals(Object obj) {

        if (obj == null || GetType() != obj.GetType()) 
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        Expr otherExpr = (Expr) obj;

        if (cachedHashCode != 0 && otherExpr.cachedHashCode != 0 && cachedHashCode != otherExpr.cachedHashCode)
            return false;

        otherExpr.prepareFromLoopback();
        prepareFromLoopback();
        if (type != otherExpr.type)
            return false;
        if (val != null) {
            // This Expr is of the val != null type.
            if (otherExpr.val == null)
                return false;
            switch (type) {
                case INTEGER:
                case REAL:
                case STRING:
                case SYMBOL:
                    return val.Equals(otherExpr.val);
                case INTARRAY1: {
                    int[] a = (int[]) val;
                    int[] oa = (int[]) otherExpr.val;
                    if (a.Length != oa.Length)
                        return false;
                    for (int i = 0; i < a.Length; i++)
                        if (a[i] != oa[i])
                            return false;
                    return true;
                }
                case REALARRAY1: {
                    double[] a = (double[]) val;
                    double[] oa = (double[]) otherExpr.val;
                    if (a.Length != oa.Length)
                        return false;
                    for (int i = 0; i < a.Length; i++)
                        if (a[i] != oa[i])
                            return false;
                    return true;
                }
                case INTARRAY2: {
                    int[,] a = (int[,]) val;
                    int[,] oa = (int[,]) otherExpr.val;
                    if (a.GetLength(0) != oa.GetLength(0) || a.GetLength(1) != oa.GetLength(1))
                        return false;
                    for (int i = 0; i < a.GetLength(0); i++)
                        for (int j = 0; i < a.GetLength(1); i++)
                            if (a[i,j] != oa[i,j])
                                return false;
                    return true;
                }
                case REALARRAY2: {
                    double[,] a = (double[,]) val;
                    double[,] oa = (double[,]) otherExpr.val;
                    if (a.GetLength(0) != oa.GetLength(0) || a.GetLength(1) != oa.GetLength(1))
                        return false;
                    for (int i = 0; i < a.GetLength(0); i++)
                        for (int j = 0; i < a.GetLength(1); i++)
                            if (a[i,j] != oa[i,j])
                                return false;
                    return true;
                }
                default:
                    // Just to make the compiler happy; should never get here.
                    return false;
            }
        } else {
            // This Expr is of the head/args != null, val == null type.
            if (otherExpr.val != null)
                return false;
            if (!head.Equals(otherExpr.head))
                return false;
            if (args.Length != otherExpr.args.Length)
                return false;
            for (int i = 0; i < args.Length; i++)
                if (!args[i].Equals(otherExpr.args[i]))
                    return false;
            return true;
        }
    }


    public override int GetHashCode() {

        if (cachedHashCode != 0)
            return cachedHashCode;
        
        prepareFromLoopback();
        
        // As always, at some point we must stop recursing into heads, as that is never-ending. We stop when
        // we get a "true" atomic Expr.
        if (type < FIRST_COMPOSITE)
            return val.GetHashCode();
            
        // Algorithm from "Effective Java" item 8.
        int hash = 17;
        hash = 37 * hash + type;
        if (head != null)
            hash = 37 * hash + head.GetHashCode();
        if (args != null)
            for (int i = 0; i < args.Length; i++)
                hash = 37 * hash + args[i].GetHashCode();
        if (val != null) {
            if (type < FIRST_ARRAY_TYPE) {
                // Safe to call GetHashCode() on val for all these types, since their hashcodes are
                // direct reflections of their values (unlike arrays, whose hashcodes are not).
                hash = 37 * hash + val.GetHashCode();
            } else if (type == INTARRAY1) {
                int[] ia = (int[]) val;
                foreach (int i in ia)
                    hash += i;
            } else if (type == REALARRAY1) {
                double[] da = (double[]) val;
                foreach (double d in da)
                    hash += (int) d;
            } else if (type == INTARRAY2) {
                int[,] iaa = (int[,]) val;
                foreach (int i in iaa)
                    hash += i;
            } else if (type == REALARRAY2) {
                double[,] daa = (double[,]) val;
                foreach (double d in daa)
                    hash += (int) d;
            }
        }

        cachedHashCode = hash;
        return hash;
    }


    // The real GetHashCode() method is quite expensive, as it forces the expression to be unwound from its
    // loopback link. We want to avoid this cost when Exprs are returned to Mathematica (they are stored
    // in a hashtable in .NET), so we provide a separate method that just uses the default
    // Object.GetHashCode(). This is perfectly legitimate, as there is no particular reason to require the
    // actual GetHashCode() method to be called, as long as we are consitent and always use this method
    // for Exprs (this happens in the ObjectHandler.InstanceCollection class).
    internal int inheritedHashCode() {
        return base.GetHashCode();
    }
    
    
    /*********************************  Indexer and Properties  **********************************/

    /// <summary>
    /// Gets a part based on its position index. This is the indexer for the class.
    /// </summary>
    /// <remarks>
    /// Part numbers are just like in <i>Mathematica</i>: 0 gives the head, and arguments are counted from 1 onward.
    /// </remarks>
    /// <seealso cref="Head"/>
    /// <seealso cref="Part"/>
    /// <exception cref="IndexOutOfRangeException">If the part index is greater than the number of arguments.</exception>
    ///
    public Expr this[int part] {
        get {
            return Part(part);
        }
    }


    /// <summary>
    /// Gets the Expr representing the head of this Expr. Works like the <i>Mathematica</i> function Head.
    /// </summary>
    /// <seealso cref="Part"/>
    /// <seealso cref="Args"/>
    ///
    public Expr Head {
        get {
            prepareFromLoopback();
            return type < FIRST_ARRAY_TYPE ? head : SYM_LIST;
        }
    }

    
    /// <summary>
    /// Gets an array of Exprs representing the arguments of this Expr.
    /// </summary>
    /// <remarks>
    /// For Exprs that have head Rational or Complex, this gives a two-argument array giving the numerator/denominator
    /// or re/im parts, repsectively.
    /// <para>
    /// A 0-length array is returned if this Expr is a function with zero arguments, or an atomic type
    /// (integer, real, string, symbol).
    /// </para>
    /// This property can be expensive to call if the Expr represents a large array of integers or reals. Such data is
    /// normally held within the Expr in an efficient format, and asking for it to be "unpacked" into many individual Expr
    /// objects is costly.
    /// </remarks>
    /// <seealso cref="Head"/>
    /// <seealso cref="Part"/>
    ///
    public Expr[] Args {
        get {
            // Defensive copying a la "Effective Java" item 24.
            return (Expr[]) nonCopyingArgs().Clone();
        }
    }


    /// <summary>
    /// Gets the length of this Expr. Works like <i>Mathematica</i>'s Length function.
    /// </summary>
    /// <seealso cref="Head"/>
    /// <seealso cref="Args"/>
    /// <seealso cref="Part"/>
    ///
    public int Length {
        get {
            prepareFromLoopback();
            if (type >= FIRST_ARRAY_TYPE) {
                return ((Array) val).GetLength(0);
            } else {
                // If it's not an array, we know the args cache field is already filled in.
                return args != null ? args.Length : 0;
            }
        }
    }


    /// <summary>
    /// Gets an array of integers representing the dimensions of this Expr. Works like the
    /// <i>Mathematica</i> function Dimensions.
    /// </summary>
    /// 
    public int[] Dimensions {
        get {
            prepareFromLoopback();
            int[] dims = null;
            if (type < FIRST_COMPOSITE) {
                dims = new int[0];
            } else {
                switch (type) {
                    case INTARRAY1:
                    case REALARRAY1:
                        dims = new int[1];
                        dims[0] = ((Array) val).GetLength(0);
                        break;
                    case INTARRAY2:
                    case REALARRAY2:
                        dims = new int[2];
                        dims[0] = ((Array) val).GetLength(0);
                        dims[1] = ((Array) val).GetLength(1);
                        break;
                    case FUNCTION: {
                        if (args.Length == 0) {
                            dims = new int[1];
                            dims[0] = 0;
                            break;
                        }
                        int[] leafDims = args[0].Dimensions;
                        int[] agreed = new int[leafDims.Length + 1];
                        agreed[0] = args.Length;
                        // Fill agreed with leafDims, starting at position 1. agreed never needs to get modified
                        // again. Only depthOK can change.
                        Array.Copy(leafDims, 0, agreed, 1, leafDims.Length);
                        // Gives the number or elements of 'agreed' that should be used in result. It can only get smaller.
                        int depthOK = 1 + leafDims.Length;
                        for (int i = 1; i < args.Length; i++) {
                            // A simple optimization--if depthOK ever gets to 1, then we can stop immediately 
                            if (depthOK == 1)
                                break;
                            int[] otherLeafDims = args[i].Dimensions;
                            depthOK = Math.Min(depthOK, 1 + otherLeafDims.Length);
                            // Because of the line above, depthOK is a suitable limit for the iteration below to ensure we won't walk off
                            // the end of either array.
                            for (int j = 1; j < depthOK; j++) {
                                if (agreed[j] != otherLeafDims[j - 1]) {
                                    depthOK = j;
                                    break;
                                }
                            }
                        }
                        // Now go back and verify the heads. Walk down to level depthOK and see if everything you find has the right head.
                        string headStr = Head.ToString();
                        int headsAgreeDepth = checkHeads(headStr, 0, depthOK);
                        dims = new int[headsAgreeDepth];
                        Array.Copy(agreed, 0, dims, 0, headsAgreeDepth);
                        break;
                    }
                    default:
                        Debug.WriteLine("Bad type in Expr.dimensions");
                        break;
                }
            }
            return dims;
        }
    }


    /****************************************  Conversion Methods  *****************************************/

    /// <summary>
    /// Converts the Expr to a long integer value. This is the same operation as calling the <see cref="AsInt64"/> method.
    /// </summary>
    /// <param name="e">The Expr to convert.</param>
    /// <exception cref="ExprFormatException">If the Expr cannot be converted to this format.</exception>
    /// 
    public static explicit operator long(Expr e) {
        return e.AsInt64();
    }


    /// <summary>
    /// Converts the Expr to a double value. This is the same operation as calling the <see cref="AsDouble"/> method.
    /// </summary>
    /// <param name="e">The Expr to convert.</param>
    /// <exception cref="ExprFormatException">If the Expr cannot be converted to this format.</exception>
    /// 
    public static explicit operator double(Expr e) {
        return e.AsDouble();
    }


    /// <summary>
    /// Converts the Expr to a string representation. This is the same operation as calling the <see cref="ToString"/> method.
    /// </summary>
    /// <param name="e">The Expr to convert.</param>
    /// 
    public static explicit operator string(Expr e) {
        return e.ToString();
    }


    /// <summary>
    /// Gives the Int64 value for Exprs that can be represented as integers. 
    /// </summary>
    /// <exception cref="ExprFormatException">If the Expr cannot be converted to this format.</exception>
    /// 
    public long AsInt64() {
 
        prepareFromLoopback();
        try {
            if (type == INTEGER)
                return Convert.ToInt64(val);
            else
                throw new ArgumentException();
        } catch (Exception) {
            throw new ExprFormatException("This Expr cannot be represented as a .NET Int64 value.");
        }
    }


    /// <summary>
    /// Gives the double value for Exprs that can be represented as doubles. 
    /// </summary>
    /// <exception cref="ExprFormatException">If the Expr cannot be converted to this format.</exception>
    /// 
    public double AsDouble() {
 
        prepareFromLoopback();
        try {
            if (type == REAL || type == INTEGER)
                return Convert.ToDouble(val);
            else if (RationalQ())
                return Part(1).AsDouble() / Part(2).AsDouble();
            else
                throw new ArgumentException();
        } catch (Exception) {
            throw new ExprFormatException("This Expr cannot be represented as a .NET Double value.");
        }
    }


    /// <summary>
    /// Converts the Expr to an array of the requested type and depth.
    /// </summary>
    /// <remarks>
    /// The requested element type must be either ExpressionType.Integer or ExpressionType.Real, and the depth must be 1 or 2.
    /// In other words, 1- or 2-dimensionals arrays of integers or doubles can be extracted from an Expr object using this method.
    /// <para>
    /// Some Exprs represent large lists or arrays of integers or reals. This method is the only efficient way of
    /// extracting the raw array data from out of the Expr. To preserve the immutability of Exprs, however, the
    /// returned array is a copy of the Expr's internal array data.
    /// </para>
    /// </remarks>
    /// <param name="reqType">The type of the leaf elements. Must be ExpressionType.Integer or ExpressionType.Real.</param>
    /// <param name="depth">The desired depth of the array. Must be 1 or 2.</param>
    /// <exception cref="ArgumentException">If depth > 2 or the requested type is not supported.</exception>
    /// <exception cref="ExprFormatException">If the Expr cannot be converted to this format.</exception>
    /// 
    public Array AsArray(ExpressionType reqType, int depth) {

        prepareFromLoopback();
        if (depth > 2)
            throw new ArgumentException("Depths > 2 are not supported in Expr.AsArray()");
        if (reqType != ExpressionType.Integer && reqType != ExpressionType.Real)
            throw new ArgumentException("Unsupported type in Expr.AsArray(): " + reqType);
        switch (type) {
            case INTARRAY1: {
                if (depth != 1 || reqType != ExpressionType.Integer)
                    throw new ExprFormatException("This Expr cannot be represented as a .NET array of the requested type and/or depth.");
                // Note the defensive copying of arrays to preserve immutability of Exprs (item 24 in "Effective Java").
                return (int[]) ((int[]) val).Clone();
            }
            case REALARRAY1: {
                if (depth != 1 || reqType != ExpressionType.Real)
                    throw new ExprFormatException("This Expr cannot be represented as a .NET array of the requested type and/or depth.");
                return (double[]) ((double[]) val).Clone();
            }
            case INTARRAY2: {
                if (depth != 2 || reqType != ExpressionType.Integer)
                    throw new ExprFormatException("This Expr cannot be represented as a .NET array of the requested type and/or depth.");
                return (int[,]) ((int[,]) val).Clone();
            }
            case REALARRAY2: {
                if (depth != 2 || reqType != ExpressionType.Real)
                    throw new ExprFormatException("This Expr cannot be represented as a .NET array of the requested type and/or depth.");
                return (double[,]) ((double[,]) val).Clone();
            }
            case FUNCTION: {
                if (depth == 1) {
                    if (reqType == ExpressionType.Integer) {
                        int[] ia = new int[args.Length];
                        for (int i = 0; i < args.Length; i++) {
                            if (!args[i].IntegerQ())
                                throw new ExprFormatException("This Expr cannot be represented as a .NET array of ints because some elements are not integers");
                            ia[i] = Convert.ToInt32(args[i].val);
                        }
                        return ia;
                    } else {
                        // reqType will be ExpressionType.Real
                        double[] da = new double[args.Length];
                        for (int i = 0; i < args.Length; i++) {
                            if (!args[i].RealQ() && !args[i].IntegerQ())
                                throw new ExprFormatException("This Expr cannot be represented as a .NET array of doubles because some elements are not real numbers");
                            da[i] = Convert.ToDouble(args[i].val);
                        }
                        return da;
                    }
                } else {
                    // depth will be 2. Wrap the whole thing in try because there are several exceptions that could be thrown
                    // in what follows. We want them all to appear to the user as an ExprFormatException. 
                    try {
                        if (reqType == ExpressionType.Integer) {
                            int[,] iaa = new int[args.Length, args[0].Length];
                            for (int i = 0; i < args.Length; i++) {
                                int[] subArray = (int[]) args[i].AsArray(reqType, 1);
                                for (int j = 0; j < subArray.Length; j++)
                                    iaa[i,j] = subArray[j];
                            }
                            return iaa;
                        } else {
                            // reqType will be ExpressionType.Real
                            double[,] daa = new double[args.Length, args[0].Length];
                            for (int i = 0; i < args.Length; i++) {
                                double[] subArray = (double[]) args[i].AsArray(reqType, 1);
                                for (int j = 0; j < subArray.Length; j++)
                                    daa[i,j] = subArray[j];
                            }
                            return daa;
                        }
                    } catch (Exception) {
                        throw new ExprFormatException("This Expr cannot be represented as a .NET array of the requested type and/or depth.");
                    }
                }
            }
            default:
                throw new ExprFormatException("This Expr cannot be represented as a .NET array of the requested type and/or depth.");
        }
    }


    /****************************************  Structural Methods  *****************************************/

    /// <overloads>
    /// <summary>
    /// Gives the Expr representing the specified part of this Expr. Works like the <i>Mathematica</i> function Part.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Gives the Expr representing the specified part of this Expr.
    /// </summary>
    /// <param name="i">The index of the desired part. 0 gives the head; arguments are counted from 1.</param>
    /// <exception cref="IndexOutOfRangeException">If the index is beyond the bounds of the expression.</exception>
    ///
    public Expr Part(int i) {
        
        prepareFromLoopback();
        if (Math.Abs(i) > Length)
            throw new IndexOutOfRangeException("Cannot take part " + i + " from this Expr because it has length " + Length + ".");
        else if (i == 0)
            return Head;
        else if (i > 0)
            return nonCopyingArgs()[i - 1];
        else 
            return nonCopyingArgs()[Length + i];
    }


    /// <summary>
    /// Extracts a part more than one level deep.
    /// </summary>
    /// <remarks>
    /// This version allows you to extract a part more than one level deep. Thus, e.Part(new int[] {3,4}) is like the
    /// <i>Mathematica</i> function Part[e, 3, 4] or e[[3,4]].
    /// </remarks>
    /// <param name="ia">The index of the desired part.</param>
    /// <exception cref="IndexOutOfRangeException">If the index is beyond the bounds of the expression.</exception>
    /// 
    public Expr Part(int[] ia) {
        
        try {
            int len = ia.Length;
            if (len == 1) {
                return Part(ia[0]);
            } else {
                int[] newia = new int[len - 1];
                Array.Copy(ia, 0, newia, 0, len - 1);
                return Part(newia).Part(ia[len - 1]);
            }
        } catch (IndexOutOfRangeException) {
            // Catch the exception thrown by one of the subsidiary part() calls so we can issue a better message.
            throw new IndexOutOfRangeException("Part " + (new Expr(ia)) + " of this Expr does not exist.");        
        }
    }

    
    /// <summary>
    /// Returns a new Expr that has the same head but only the first n elements of this Expr
    /// (or last n elements if n is negative). Works like the <i>Mathematica</i> function Take.
    /// </summary>
    /// <param name="n">The number of elements to take from the beginning (or end if n is negative).</param>
    /// <returns>The shortened Expr.</returns>
    /// <exception cref="ArgumentException">If n is beyond the bounds of the expression.</exception>
    /// 
    public Expr Take(int n) {
        
        int num = Math.Abs(n);
        int curLen = nonCopyingArgs().Length;
        if (num > curLen)
            throw new ArgumentException("Cannot take " + n + " elements from this Expr because it has length " + curLen + ".");
        Expr[] newArgs = new Expr[num];
        if (n >= 0)
            Array.Copy(args, 0, newArgs, 0, num);
        else
            Array.Copy(args, curLen - num, newArgs, 0, num);
        return new Expr(head, newArgs);
    }


    /// <summary>
    /// Returns a new Expr that has the same head but the nth element deleted (counted from the
    /// end if n is negative). Works like the <i>Mathematica</i> function Delete.
    /// </summary>
    /// <param name="n">The index of the element to delete (counted from the end if n is negative).</param>
    /// <returns>The shortened Expr.</returns>
    /// <exception cref="ArgumentException">If n is beyond the bounds of the expression.</exception>
    /// 
    public Expr Delete(int n) {
        
        int curLen = nonCopyingArgs().Length;
        if (n == 0 || Math.Abs(n) > curLen)
            throw new ArgumentException(n + " is an invalid deletion position in this Expr.");
        Expr[] newArgs = new Expr[curLen - 1];
        if (n > 0) {
            Array.Copy(args, 0, newArgs, 0, n - 1);
            Array.Copy(args, n, newArgs, n - 1, curLen - n);
        } else {
            Array.Copy(args, 0, newArgs, 0, curLen + n);
            Array.Copy(args, curLen + n + 1, newArgs, curLen + n, -n - 1);
        }
        return new Expr(head, newArgs);
    }
    

    /// <summary>
    /// Returns a new Expr that has the same head but with e inserted into position n (counted from the
    /// end if n is negative). Works like the <i>Mathematica</i> function Insert.
    /// </summary>
    /// <param name="e">The element to insert.</param>
    /// <param name="n">The index at which to perform the insertion (counted from the end if n is negative).</param>
    /// <returns>The new Expr.</returns>
    /// <exception cref="ArgumentException">If n is beyond the bounds of the expression.</exception>
    /// 
    public Expr Insert(Expr e, int n) {
        
        int curLen = nonCopyingArgs().Length;
        if (n == 0 || Math.Abs(n) > curLen + 1)
            throw new ArgumentException(n + " is an invalid insertion position into this Expr.");
        Expr[] newArgs = new Expr[curLen + 1];
        if (n > 0) {
            Array.Copy(args, 0, newArgs, 0, n - 1);
            newArgs[n - 1] = e;
            Array.Copy(args, n - 1, newArgs, n, curLen - (n - 1));
        } else {
            Array.Copy(args, 0, newArgs, 0, curLen + n + 1);
            newArgs[curLen + n + 1] = e;
            Array.Copy(args, curLen + n + 1, newArgs, curLen + n + 2, -n - 1);
        }
        return new Expr(head, newArgs);
    }
    

    /****************************************  "Q" Methods  *****************************************/

    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> atom. Works like the <i>Mathematica</i> function AtomQ.
    /// </summary>
    /// <remarks>
    /// Like <i>Mathematica</i>'s AtomQ, this returns true if the Expr has head Rational or Complex.
    /// </remarks>
    /// <returns>true, if the Expr is an atom; false otherwise.</returns>
    /// 
    public bool AtomQ() {
        prepareFromLoopback();
        if (type < FIRST_COMPOSITE)
            return true;
        if (type == FUNCTION) {
            object headVal = Head.val;
            // val will be null if this is a compound head. (Fix for 188595)
            if (headVal != null) {
                String s = headVal.ToString();
                if (s.Equals("Rational") || s.Equals("Complex"))
                    return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> string. Works like the <i>Mathematica</i> function StringQ.
    /// </summary>
    /// <returns>true, if the Expr is a string; false otherwise.</returns>
    /// 
    public bool StringQ() {
        prepareFromLoopback();
        return type == STRING;
    }


    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> symbol.
    /// </summary>
    /// <remarks>
    /// Works like the following test in <i>Mathematica</i>: Head[e] === Symbol.
    /// </remarks>
    /// <returns>true, if the Expr is a symbol; false otherwise.</returns>
    /// 
    public bool SymbolQ() {
        prepareFromLoopback();
        return type == SYMBOL;
    }


    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> integer. Works like the <i>Mathematica</i> function IntegerQ.
    /// </summary>
    /// <returns>true, if the Expr is an integer; false otherwise.</returns>
    /// 
    public bool IntegerQ() {
        prepareFromLoopback();
        return type == INTEGER;
    }

    
    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> Real number.
    /// </summary>
    /// <remarks>
    /// Works like the following test in <i>Mathematica</i>: Head[e] === Real.
    /// </remarks>
    /// <returns>true, if the Expr is a non-integer real number; false otherwise.</returns>
    /// 
    public bool RealQ() {
        prepareFromLoopback();
        return type == REAL;
    }


    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> Rational number.
    /// </summary>
    /// <remarks>
    /// Works like the following test in <i>Mathematica</i>: Head[e] === Rational.
    /// </remarks>
    /// <returns>true, if the Expr is a non-integer Rational number; false otherwise.</returns>
    /// 
    public bool RationalQ() {
        prepareFromLoopback();
        return type == FUNCTION && Head.ToString() == "Rational";
    }

    
    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> Complex number.
    /// </summary>
    /// <remarks>
    /// Works like the following test in <i>Mathematica</i>: Head[e] === Complex.
    /// </remarks>
    /// <returns>true, if the Expr is a Complex number; false otherwise.</returns>
    /// 
    public bool ComplexQ() {
        prepareFromLoopback();
        return type == FUNCTION && Head.ToString() == "Complex";
    }

    
    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> number (real, integer, rational, or complex).
    /// Works like the <i>Mathematica</i> function NumberQ.
    /// </summary>
    /// <returns>true, if the Expr is a number type; false otherwise.</returns>
    /// 
    public bool NumberQ() {
        return IntegerQ() || RealQ() || RationalQ() || ComplexQ();
    }

    
    /// <summary>
    /// Tells whether the Expr represents the <i>Mathematica</i> symbol True. Works like the <i>Mathematica</i> function TrueQ.
    /// </summary>
    /// <returns>true, if the Expr is the symbol True; false otherwise.</returns>
    /// 
    public bool TrueQ() {
        prepareFromLoopback();
        return type == SYMBOL && val.ToString() == "True"; 
    }

    
    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> list (that is, it has head List).
    /// Works like the <i>Mathematica</i> function ListQ.
    /// </summary>
    /// <returns>true, if the Expr has head List; false otherwise.</returns>
    /// 
    public bool ListQ() {
        prepareFromLoopback();
        return type >= FIRST_ARRAY_TYPE || type == FUNCTION && head.type == SYMBOL && head.val.ToString() == "List"; 
    }

    
    /// <overloads>
    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> vector (that is, it has head List,
    /// and no parts are themselves lists). Works like the <i>Mathematica</i> function VectorQ.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// The elements can be of any type (except lists).
    /// </summary>
    /// <returns>true, if the Expr is a vector; false otherwise.</returns>
    /// 
    public bool VectorQ() {

        prepareFromLoopback();
        if (type == INTARRAY1 || type == REALARRAY1)
            return true;
        if (type == INTARRAY2 || type == REALARRAY2 || !ListQ())
            return false;
        // No need to force cache filling (by calling nonCopyingArgs()), since I've already ruled out the types
        // where the args field wouldn't have been filled.
        for (int i = 0; i < args.Length; i++) {
            if (args[i].ListQ())
                return false;
        }
        return true;
    }


    /// <summary>
    /// Requires that every element be of the specified type.
    /// </summary>
    /// <param name="elementType">The type of the vector's elements.</param>
    /// <returns>true, if the Expr is a vector with elements of the specified type; false otherwise.</returns>
    /// 
    public bool VectorQ(ExpressionType elementType) {
        
        if (!VectorQ())
            return false;
        switch (type) {
            case INTARRAY1:
                return elementType == ExpressionType.Integer;
            case REALARRAY1:
                return elementType == ExpressionType.Real;
            case INTARRAY2:
            case REALARRAY2:
                return false;
            default: {
                // Fall-through to here means we must painstakingly verify every leaf.
                int internalType = internalTypeFromExpressionType(elementType);
                int len = Length;
                for (int i = 0; i < len; i++) {
                    // There might (?) be cases where args need to be fleshed out before accessing each one's type field
                    // directly. Thus the call to prepareFromLoopback.
                    args[i].prepareFromLoopback();
                    if (args[i].type != internalType)
                        return false;
                }
                break;
            }
        }
        return true;
    }


    /// <overloads>
    /// <summary>
    /// Tells whether the Expr represents a <i>Mathematica</i> matrix (that is, it has head List,
    /// every element has head List, and no deeper parts are themselves lists).
    /// Works like the <i>Mathematica</i> function MatrixQ.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// The elements can be of any type (except lists).
    /// </summary>
    /// <returns>true, if the Expr is a matrix; false otherwise.</returns>
    /// 
    public bool MatrixQ() {

        // Note a bug: does not verify that matrix is fully rectangular.
        prepareFromLoopback();
        if (type == INTARRAY2 || type == REALARRAY2)
            return true;
        if (type == INTARRAY1 || type == REALARRAY1 || !ListQ())
            return false;
        // No need to force cache filling (by calling nonCopyingArgs()), since I've already ruled out the types
        // where the args field wouldn't have been filled.
        if (args.Length == 0)
            return false;
        for (int i = 0; i < args.Length; i++) {
            if (!args[i].VectorQ())
                return false;
        }
        // So far, we have verified that we have a list of lists (and no deeper lists). Now we
        // just have to verify that the length of the dimensions is at least 2.
        return Dimensions.Length >= 2;
    }

    
    /// <summary>
    /// Requires that every element be of the specified type.
    /// </summary>
    /// <param name="elementType">The type of the matrix's elements.</param>
    /// <returns>true, if the Expr is a matrix with elements of the specified type; false otherwise.</returns>
    /// 
    public bool MatrixQ(ExpressionType elementType) {
        
        // Note a bug: does not verify that matrix is fully rectangular.
        if (!MatrixQ())
            return false;
        int internalTypeRequested = internalTypeFromExpressionType(elementType);
        if (internalTypeRequested == INTEGER && type == INTARRAY2 ||
                internalTypeRequested == REAL && type == REALARRAY2)
            return true;
        int len = Length;
        // Here we need to force cache filling. We could get here if we had an array type
        // (e.g., an array of reals and asking if it's of type INTEGER).
        nonCopyingArgs();
        for (int i = 0; i < len; i++) {
            if (!args[i].VectorQ(elementType))
                return false;
        }
        return true;
    }


    /****************************************  Put  *****************************************/

    /// <summary>
    /// Not intended for general use.
    /// </summary>
    /// <remarks>
    /// To write an Expr on a link, use the <see cref="IMathLink.Put">IMathLink.Put</see> method instead.
    /// This method is only public because developers of IMathLink implementations must call it inside their
    /// Put methods if the object's type is Expr. Exprs know how to write themselves on a link.
    /// </remarks>
    ///
    public void Put(IMathLink ml) {

        lock (this) {
            if (link != null) {
                ILinkMark mark = link.CreateMark();
                try {
                    ml.TransferExpression(link);
                } finally {
                    ml.ClearError();  // probably not actually an error state here
                    link.SeekMark(mark);
                    link.DestroyMark(mark);
                }
            } else {
                if (val != null) {
                    if (type == SYMBOL) {
                        ml.PutSymbol((String) val);
                    } else {
                        ml.Put(val);
                    }
                } else {
                    ml.PutNext(ExpressionType.Function);
                    ml.PutArgCount(args.Length);
                    ml.Put(head);
                    for (int i = 0; i < args.Length; i++)
                        ml.Put(args[i]);
                }
            }
        }
    }


   /***************************************  Private  ****************************************/

    // Converts from the "user world" of types, which come from the ExpressionType enum, to the internal
    // codes that Expr uses to represent the data type.
    private int internalTypeFromExpressionType(ExpressionType t) {

        switch (t) {
            case ExpressionType.Integer:   return INTEGER;
            case ExpressionType.Real:      return REAL;
            case ExpressionType.String:    return STRING;
            case ExpressionType.Symbol:    return SYMBOL;
            case ExpressionType.Complex:   return FUNCTION;
            case ExpressionType.Boolean:   return SYMBOL;
            case ExpressionType.Object:
                throw new ArgumentException("You cannot currently create an Expr of type Object.");
            case ExpressionType.Function:
                throw new ArgumentException("You cannot directly create an Expr of type Function. You must build it out of other Exprs.");
            default:
                throw new ArgumentException("Unknown ExpressionType: " + t);
        }
    }


    // Important to ensure that on exit the conditions are set so that it will never
    // be called again, even if it fails. Right now, this means just setting link to null.
    private void prepareFromLoopback() {
        
        lock (this ) {
            if (link != null) {
                try {
                    fillFromLink(link);
                } catch (MathLinkException e) {
                    // This should never happen. An exception should have been thrown when transferring
                    // onto loopback from the native link.
                    Debug.WriteLine("MathLinkException reading Expr from loopback: " + e);
                } finally {
                    link.Close();
                    link = null;
                }
            }
        }
    }


    // Factory method that reads an expression from the link and returns a corresponding Expr.
    private static Expr createFromLink(IMathLink ml, bool allowLoopback) {

        ExpressionType type = ml.GetNextExpressionType();
        // We don't bother to ever use a loopback link to hold atomic expressions.
        if (type == ExpressionType.Integer || type == ExpressionType.Real || type == ExpressionType.Boolean ||
                type == ExpressionType.String || type == ExpressionType.Symbol) {
            return createAtomicExpr(ml, type);
        } else if (type == ExpressionType.Object) {
            // Note that Expr really cannot do much with object refs. The best we can do is hold them as 
            // symbols, which of course is their actual M representation, so this is the natural thing to do.
            return createAtomicExpr(ml, ExpressionType.Symbol);
        } else {
            Expr result = new Expr();
            // This test is "will an attempt to use a loopback link NOT cause the MathLink library to be loaded
            // for the first time?" We want to allow Expr operations to avoid native code as much as possible,
            // so they can be performed on platforms for which no native library is available (e.g., handhelds).
            if (allowLoopback && NativeLink.canUseMathLinkLibrary()) {
                result.link = MathLinkFactory.CreateLoopbackLink();
                result.link.TransferExpression(ml);
                result.type = UNKNOWN;
            } else {
                result.fillFromLink(ml);
            }
            return result;
        }
    }

    
    // Fills out the fields of an existing Expr by reading from a link (typically, but not always, this link is
    // the Expr's own loopback link that was first used to store its contents).
    // Up to the caller to ensure that link != null.
    private void fillFromLink(IMathLink ml) {
        
        lock (this) {
            ExpressionType mlType = ml.GetExpressionType();  // Not GetNextExpressionType() here.
            if (mlType == ExpressionType.Function || mlType == ExpressionType.Complex) {
                try {
                    int argc = ml.GetArgCount();
                    head = createFromLink(ml, false);
                    // Do the full Expr form for all args.
                    type = FUNCTION;
                    args = new Expr[argc];
                    for (int i = 0; i < argc; i++)
                        args[i] = createFromLink(ml, false);
                } catch (MathLinkException e) {
                    // This branch only entered when an illegal expression was on the link.
                    Debug.WriteLine("MathLinkException reading Expr from link: " + e.ToString());
                    throw e;
                } finally {
                    ml.ClearError();
                }
            } else if (mlType == ExpressionType.Integer || mlType == ExpressionType.Real || mlType == ExpressionType.Boolean ||
                    mlType == ExpressionType.String || mlType == ExpressionType.Symbol) {
                // Atomic types should never be encountered by fillFromLink. They should be detected in readFromLink and
                // routed through createAtomicExpr. 
                Debug.WriteLine("Atomic type in fillFromLink: " + type);
            } else {
                Debug.WriteLine("Unexpected type in fillFromLink: " + type);
            }
        }
    }


    private static Expr createAtomicExpr(IMathLink ml, ExpressionType type) {
        
        Expr result = null;
        switch (type) {
            case ExpressionType.Integer: {
                string s = ml.GetString();
                // Reuse cached instances for common ints.
                if (s == "0")
                    result = INT_ZERO;
                else if (s == "1")
                    result = INT_ONE;
                else if (s == "-1")
                    result = INT_MINUSONE;
                else {
                    result = new Expr();
                    result.head = SYM_INTEGER;
                    try {
                        result.val = Convert.ToInt64(s);
                    } catch (Exception) {
                        result.val = Convert.ToDecimal(s);
                    }
                    result.type = INTEGER;
                }
                break;
            }
            case ExpressionType.Real: {
                result = new Expr();
                result.head = SYM_REAL;
                // If we call getDouble() here, MathLink will return a double that may have been truncated. Thus
                // we get the data as a string and interpret it ourselves.
                string s = ml.GetString();
                try {
                    result.val = Convert.ToDouble(s, System.Globalization.NumberFormatInfo.InvariantInfo);
                } catch (Exception) {
                    // Will get here if number has too many digits (even if its magnitude is small), or if the number
                    // has some InputForm gunk in it (like `). In this latter case, the number might be representable as
                    // a double, but we don't bother trying. We also ignore the precision and accuracy spec following the `.
                    result.val = Utils.DecimalFromString(s);
                }
                result.type = REAL;
                break;
            }
            case ExpressionType.String: {
                result = new Expr();
                result.type = STRING;
                result.head = SYM_STRING;
                result.val = ml.GetString();
                break;
            }
            case ExpressionType.Symbol: {
                string sym = ml.GetSymbol();
                if (sym == "List") {
                    result = SYM_LIST;
                } else {
                    result = new Expr();
                    result.type = SYMBOL;
                    result.head = SYM_SYMBOL;
                    result.val = sym;
                }
                break;
            }
            case ExpressionType.Boolean: {
                bool b = ml.GetBoolean();
                result = b ? SYM_TRUE : SYM_FALSE;
                break;
            }
            default:
                Debug.WriteLine("Bad type passed to createAtomicExpr");
                break;
        }
        return result;
    }

    // For internal calls to args, we don't need the defensive copying in the one that is public.
    // That's the reason for this separate method.
    private Expr[] nonCopyingArgs() {

        lock (this) {
            prepareFromLoopback();
            if (args == null) {
                if (type < FIRST_COMPOSITE) {
                    // Flesh out args as empty array.
                    args = new Expr[0];
                } else if (type == INTARRAY1 || type == REALARRAY1) {
                    // args not used until now; val instead. Must now create Expr form for args array.
                    Array a = (Array) val;
                    args = new Expr[a.GetLength(0)];
                    for (int i = 0; i < args.Length; i++)
                        args[i] = new Expr(a.GetValue(i));
                } else if (type == INTARRAY2 || type == REALARRAY2) {
                    // args not used until now; val instead. Must now create Expr form for args array.
                    args = new Expr[((Array) val).GetLength(0)];
                    int lenInSecondDimension = ((Array) val).GetLength(1);
                    for (int i = 0; i < args.Length; i++) {
                        // Don't use the Expr(int[]) or Expr(double[]) ctors, as they do defensive copying of the array,
                        // which is not necessary here.
                        args[i] = new Expr();
                        args[i].head = SYM_LIST;
                        if (type == INTARRAY2) {
                            int[,] valArray = (int[,]) val;  // pre-cast
                            int[] subArray = new int[lenInSecondDimension];
                            for (int j = 0; j < lenInSecondDimension; j++)
                                subArray[j] = valArray[i,j];
                            args[i].type = INTARRAY1;
                            args[i].val = subArray;
                        } else {
                            // REALARRAY2:
                            double[,] valArray = (double[,]) val;  // pre-cast
                            double[] subArray = new double[lenInSecondDimension];
                            for (int j = 0; j < lenInSecondDimension; j++)
                                subArray[j] = valArray[i,j];
                            args[i].type = REALARRAY1;
                            args[i].val = subArray;
                        }
                    }
                }
            }
            return args;
        }
    }


    // Returns the deepest level to which all subexprs have the given head. Checks down to at most level maxDepth.
    // This must return a number > 0 (0 would mean that the Expr did not have the same head as itself). Returning
    // 1 means that none of the top-level children have the same head as the Expr itself.
    private int checkHeads(string head, int curDepth, int maxDepth) {
        
        if (args == null || curDepth > maxDepth || Head.ToString() != head)
            return curDepth;
        curDepth++;
        for (int i = 0; i < args.Length; i++) {
            int thisArgDepth = args[i].checkHeads(head, curDepth, maxDepth);
            if (thisArgDepth < maxDepth)
                maxDepth = thisArgDepth;
        }
        return maxDepth;
    }
    

    // Double.ToString() can return strs with e notation. This fixes these cases.
    private static string doubleToInputFormString(double d) {
        
        string s = d.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
        // Docs say that a lowercase e is used; they lie.
        int epos = s.LastIndexOf('e');
        if (epos == -1)
            epos = s.LastIndexOf('E');
        if (epos != -1)
            s = s.Substring(0, epos) + "*^" + s.Substring(epos + 1);
        // ToString() of a double like 2.0 gives just 2, so we want to add the decimal point.
        if (s.IndexOf('.') == -1)
            s = s + ".";
        return s;
    }

}

}