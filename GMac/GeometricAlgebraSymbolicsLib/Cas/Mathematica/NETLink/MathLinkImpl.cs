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
using System.Diagnostics;

namespace Wolfram.NETLink {


/// <exclude/>
/// <summary>
/// An abstract class that provides much of the plumbing needed to implement the IMathLink interface.
/// </summary>
/// <remarks>
/// This class is public only so that WRI can subclass it from another assembly in the future (or perhaps so we
/// can assist certain external developers in doing this). It is not part of the .NET/Link API.
/// <para>
/// See the comments on the various methods as declared in the IMathLink interface for more information.
/// </para>
/// </remarks>
/// 
public abstract class MathLinkImpl : IMathLink {

    
    // For the Connect(timeout) method.
    private long timeoutMillis;
    private long startConnectTime;
    
    
    protected object yieldFunctionLock = new object();


    /*********  Non-virtual functions that can be implemented via other calls in the IMathLink interface  ********/

    public string GetStringCRLF() {
        return Utils.ConvertCRLF(GetString());
    }

    public void Put(bool b) {
        PutSymbol(b ? "True" : "False");
    }

    public bool GetBoolean() {
        return GetSymbol() == "True";
    }

    public void Put(long i) {
        Put((decimal) i);
    }

    public decimal GetDecimal() {
        return Utils.DecimalFromString(GetString());
    }

    public void Put(decimal d) {
        if (Decimal.Truncate(d) == d) {
            PutNext(ExpressionType.Integer);
        } else {
            PutNext(ExpressionType.Real);
        }
        String s = d.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
        byte[] data = new byte[s.Length];
        for (int i = 0; i < s.Length; i++)
            data[i] = (byte) s[i];
        PutSize(data.Length);
        PutData(data);
    }

    public bool[] GetBooleanArray() {
        return (bool[]) GetArray(typeof(bool), 1);
    }
    public byte[] GetByteArray() {
        return (byte[]) GetArray(typeof(byte), 1);
    }
    public char[] GetCharArray() {
        return (char[]) GetArray(typeof(char), 1);
    }
    public short[] GetInt16Array() {
        return (short[]) GetArray(typeof(short), 1);
    }
    public int[] GetInt32Array() {
        return (int[]) GetArray(typeof(int), 1);
    }
    public long[] GetInt64Array() {
        return (long[]) GetArray(typeof(long), 1);
    }
    public float[] GetSingleArray() {
        return (float[]) GetArray(typeof(float), 1);
    }
    public double[] GetDoubleArray() {
        return (double[]) GetArray(typeof(double), 1);
    }
    public decimal[] GetDecimalArray() {
        return (decimal[]) GetArray(typeof(decimal), 1);
    }
    public string[] GetStringArray() {
        return (string[]) GetArray(typeof(string), 1);
    }
    public object[] GetComplexArray() {
        if (ComplexType == null)
            throw new MathLinkException(MathLinkException.MLE_NO_COMPLEX);
        return (object[]) GetArray(ComplexType, 1);
    }    

    // This one is virtual because subclasses might want to provide their own more efficient implementation
    // (NativeLink does this).
    public virtual string GetFunction(out int argCount) {
        ExpressionType type = GetExpressionType();
        if (type != ExpressionType.Function || type != ExpressionType.Complex)
            throw new MathLinkException(MathLinkException.MLE_GETFUNCTION);
        argCount = GetArgCount();
        return GetSymbol();
    }

    public void PutFunction(string f, int argCount) {
        PutNext(ExpressionType.Function);
        PutArgCount(argCount);
        PutSymbol(f);
    }

    public void PutFunctionAndArgs(string f, params object[] args) {
        PutFunction(f, args.Length);
        foreach (object obj in args)
            Put(obj);
    }

    public int CheckFunction(string f) {
        ILinkMark mark = CreateMark();
        try {
            string s = GetFunction(out var argCount);
            if (s != f) {
                SeekMark(mark);
                throw new MathLinkException(MathLinkException.MLE_CHECKFUNCTION);
            }
            return argCount;
        } finally {
            DestroyMark(mark);
        }
    }

    public void CheckFunctionWithArgCount(string f, int argCount) {
        ILinkMark mark = CreateMark();
        try {
            string s = GetFunction(out var argc);
            if (s != f || argc != argCount) {
                SeekMark(mark);
                throw new MathLinkException(MathLinkException.MLE_CHECKFUNCTION);
            }
        } finally {
            DestroyMark(mark);
        }
    }

    public Expr GetExpr() {
        return Expr.CreateFromLink(this);
    }
    
    public Expr PeekExpr() {
        ILinkMark mark = CreateMark();
        try {
            return Expr.CreateFromLink(this);
        } finally {
            SeekMark(mark);
            DestroyMark(mark);
        }
    }
        

    public void Connect(long timeoutMillis) {

        this.timeoutMillis = timeoutMillis;
        startConnectTime = System.Environment.TickCount;
        YieldFunction connectTimeoutYieldFunction = new YieldFunction(connectTimeoutYielder);
        Yield += connectTimeoutYieldFunction;
        try {
            Connect();
        } finally {
            Yield -= connectTimeoutYieldFunction;
        }
    }
    
    private bool connectTimeoutYielder() {
        return System.Environment.TickCount - startConnectTime > timeoutMillis;
    }

    /******************************  GetObject  ******************************/

    public virtual object GetObject() {

        // Note not GetNextExpressionType(), as that will likely already have been called.
        ExpressionType leafExprType = GetExpressionType();
        switch (leafExprType) {
            case ExpressionType.Integer:
                return GetInteger();
            case ExpressionType.Real:
                return GetDouble();
            case ExpressionType.String:
                return GetString();
            case ExpressionType.Object:
                return getObj();
            case ExpressionType.Boolean:
                return GetBoolean();
            case ExpressionType.Complex:
                return GetComplex();
            case ExpressionType.Symbol: {
                ILinkMark mark = CreateMark();
                try {
                    string s = GetSymbol();
                    if (s == "Null")
                        return null;
                    SeekMark(mark);
                    throw new MathLinkException(MathLinkException.MLE_SYMBOL);
                } finally {
                    DestroyMark(mark);
                }
            }
            case ExpressionType.Function:
                return Utils.readArbitraryArray(this, typeof(Array));
            default:
                Debug.Fail("Unhandled ExpressionType value in MathLinkImpl.GetObject()");
                return null;
        }
    }

    /******************************  Complex  ******************************/

    protected ComplexClassHandler complexHandler = new ComplexClassHandler();

    public virtual object GetComplex() {
        return complexHandler.GetComplex(this);
    }

    public virtual Type ComplexType {
        get { return complexHandler.ComplexType; }
        set { complexHandler.ComplexType = value; }
    }


    /*****************************  Arrays  ********************************/

    // Place to hold heads as they are accumulated in GetArray(). Note the arbitrary max depth for GetArray() of 32 dimensions
    // (at least if you are calling thr signature that retrieves the heads). Much lower limits are probably (?) imposed
    // elsewhere in the code.
    protected string[] headsHolder = new string[32];  

        
    public virtual Array GetArray(Type leafType, int depth) {
        return GetArray(leafType, depth, out var ignoredHeads);
    }
    
    // This implementation can read every type of array, but derived classes are free to override it with more
    // efficient implementations if possible (for example NativeLink, which can handle arrays of primitive type
    // with a single call into the MathLink library). It only works correctly for rectangular (not jagged) data.
    // a MathLinkException will be thrown reading non-rectangular data. More precisely, the incoming array
    // must be rectangular down to the depth specified by the 'depth' argument. If you give a leafType of int[] and a depth
    // of 2, then the data can be ragged in the last dimension.
    public virtual Array GetArray(Type leafType, int depth, out string[] heads) {
        
        TypeCode leafTypeCode = Type.GetTypeCode(leafType);
        Array result = null;

        int actualDepth = Utils.determineIncomingArrayDepth(this);
        // For Expr and object arrays, or arrays-of-arrays, actual depth only needs to be at least as great as the
        // requested depth (because the leaf elements can be arrays).
        bool depthOK;
        if (leafType.IsArray || leafType == typeof(object) || leafType == typeof(Expr))
            depthOK = actualDepth >= depth;
        else
            depthOK = actualDepth == depth;
        if (!depthOK)
            throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);

        // Since enums are objects but have integer TypeCodes, it is easiest to deal with them separately.
        if (depth == 1 && !leafType.IsEnum) {
            int i;
            string funcName = GetFunction(out var len);
            heads = new string[1];
            heads[0] = funcName;
            // The cases here for primitive types will not be used in a "typical" link.
            // They get forwarded to more efficient code in the NativeLink class. This code
            // here is for the benefit of other subclasses that might not have a native MathLink
            // library at hand.
            switch (leafTypeCode) {
                case TypeCode.Byte: {
                    byte[] data = new byte[len];
                    for (i = 0; i < len; i++)
                        data[i] = (byte) GetInteger();
                    result = data;
                    break;
                }
                case TypeCode.SByte: {
                    sbyte[] data = new sbyte[len];
                    for (i = 0; i < len; i++)
                        data[i] = (sbyte) GetInteger();
                    result = data;
                    break;
                }
                case TypeCode.Char: {
                    char[] data = new char[len];
                    for (i = 0; i < len; i++)
                        data[i] = (char) GetInteger();
                    result = data;
                    break;
                }
                case TypeCode.Int16: {
                    short[] data = new short[len];
                    for (i = 0; i < len; i++)
                        data[i] = (short) GetInteger();
                    result = data;
                    break;
                }
                case TypeCode.UInt16: {
                    ushort[] data = new ushort[len];
                    for (i = 0; i < len; i++)
                        data[i] = (ushort) GetInteger();
                    result = data;
                    break;
                }
                case TypeCode.Int32: {
                    int[] data = new int[len];
                    for (i = 0; i < len; i++)
                        data[i] = (int) GetInteger();
                    result = data;
                    break;
                }
                case TypeCode.UInt32: {
                    uint[] data = new uint[len];
                    for (i = 0; i < len; i++)
                        data[i] = (uint) GetDecimal();
                    result = data;
                    break;
                }
                case TypeCode.Int64: {
                    long[] data = new long[len];
                    for (i = 0; i < len; i++)
                        data[i] = (long) GetDecimal();
                    result = data;
                    break;
                }
                case TypeCode.UInt64: {
                    ulong[] data = new ulong[len];
                    for (i = 0; i < len; i++)
                        data[i] = (ulong) GetDecimal();
                    result = data;
                    break;
                }
                case TypeCode.Single: {
                    float[] data = new float[len];
                    for (i = 0; i < len; i++)
                        data[i] = (float) GetDouble();
                    result = data;
                    break;
                }
                case TypeCode.Double: {
                    double[] data = new double[len];
                    for (i = 0; i < len; i++)
                        data[i] = GetDouble();
                    result = data;
                    break;
                }
                case TypeCode.Decimal: {
                    decimal[] data = new decimal[len];
                    for (i = 0; i < len; i++)
                        data[i] = GetDecimal();
                    result = data;
                    break;
                }
                case TypeCode.Boolean: {
                    bool[] data = new bool[len];
                    for (i = 0; i < len; i++)
                        data[i] = GetBoolean();
                    result = data;
                    break;
                }
                case TypeCode.String: {
                    string[] data = new string[len];
                    for (i = 0; i < len; i++)
                        data[i] = GetString();
                    result = data;
                    break;
                }
                default: {
                    // TypeCode.Object, TypeCode.DateTime, and TypeCode.DBNull.
                    if (leafType == typeof(Expr)) {
                        Expr[] data = new Expr[len];
                        for (i = 0; i < len; i++)
                            data[i] = GetExpr();
                        result = data;
                    } else if (leafType == ComplexType) {
                        result = Array.CreateInstance(leafType, len);
                        for (i = 0; i < len; i++)
                            result.SetValue(GetComplex(), i);
                    } else {
                        result = Array.CreateInstance(leafType, len);
                        for (i = 0; i < len; i++)
                            result.SetValue(GetObject(), i);
                    }
                    break;
                }
            }
        } else {
            // Enum vectors and all multidimensional arrays.
            int[] dims = new int[depth];
            for (int i = 0; i < depth; i++) {
                string funcName = GetFunction(out var len);
                dims[i] = len;
                headsHolder[i] = funcName;
            }
            // We are now poised to read the first leaf element.
            int[] indices = new int[depth];
            // Note that result will have incorrect dimensions if the incoming array is jagged or misshapen.
            // That's OK because a MathLinkException will be thrown while reading the data.
            result = Array.CreateInstance(leafType, dims);
            bool isEnum = leafType.IsEnum;
            // Length of 0 means empty array (0 in last dimension).
            if (result.Length != 0) {
                do {
                    result.SetValue(readAs(leafTypeCode, leafType, isEnum), indices);
                    discardInnerHeads(indices, dims);
                } while (Utils.nextIndex(indices, dims));
            }
            heads = new string[depth];
            Array.Copy(headsHolder, heads, depth);
        }
        return result;
    }
    
   
    public virtual void Put(object obj) {

        if (obj == null) {
            PutSymbol("Null");
            return;
        }

        // This series of 'is' tests is actually extremely fast.
        Type t = obj.GetType();
        if (obj is int) {
            Put((int) obj);
        } else if (obj is double) {
            Put((double) obj);
        } else if (obj is string) {
            putString((string) obj);
        } else if (obj is byte) {
            Put((byte) obj);
        } else if (obj is sbyte) {
            Put((sbyte) obj);
        } else if (obj is char) {
            Put((char) obj);
        } else if (obj is short) {
            Put((short) obj);
        } else if (obj is ushort) {
            Put((ushort) obj);
        } else if (obj is uint) {
            Put((decimal) (uint) obj);
        } else if (obj is long) {
            Put((decimal) (long) obj);
        } else if (obj is ulong) {
            Put((decimal) (ulong) obj);
        } else if (obj is bool) {
            Put((bool) obj);
        } else if (obj is float) {
            Put((float) obj);
        } else if (obj is decimal) {
            Put((decimal) obj);
        } else if (obj is Expr) {
            ((Expr) obj).Put(this);
        } else if (t.IsArray || t == typeof(Array)) {
            putArray((Array) obj, null);
        } else if (ComplexType != null && ComplexType == t) {
            putComplex(obj);
        } else {
            // Only KernelLink implementations will override putRef to do something meaingful.
            putRef(obj);
        }
    }
    
    public void Put(Array a, string[] heads) {

        if (a == null) {
            PutSymbol("Null");
        } else {
            putArray(a, heads);
        }
    }
    

    /******************************************    **************************************************/
    
    // headIndex is the index into the heads array that should be used for the expr at the current level.
    protected void putArrayPiecemeal(Array a, string[] heads, int headIndex) {

        if (a == null) {
            PutSymbol("Null");
            return;
        }
        Type t = a.GetType();
        int depth = a.Rank;
        string thisHead = (heads != null && heads.Length > headIndex) ? heads[headIndex] : "List";
        int len = a.GetLength(0);
        int lowerBound = a.GetLowerBound(0);
        PutFunction(thisHead, len);
        Type elementType = a.GetType().GetElementType();
        bool elementsAreArrays = elementType.IsArray;

        if (depth == 1) {
             for (int i = 0; i < len; i++) {
                if (elementsAreArrays)
                    putArrayPiecemeal((Array) a.GetValue(lowerBound + i), heads, headIndex + 1);
                else
                    Put(a.GetValue(lowerBound + i));
            }
        } else if (depth == 2) {
            int len2 = a.GetLength(1);
            int lowerBound2 = a.GetLowerBound(1);
            string thisHead2 = (heads != null && heads.Length > headIndex + 1) ? heads[headIndex + 1] : "List";
             for (int i = 0; i < len; i++) {
                PutFunction(thisHead2, len2);
                 for (int j = 0; j < len2; j++) {
                    if (elementsAreArrays)
                        putArrayPiecemeal((Array) a.GetValue(lowerBound + i, lowerBound2 + j), heads, headIndex + 2);
                    else
                        Put(a.GetValue(lowerBound + i, lowerBound2 + j));
                }
            }
        } else if (depth == 3 || depth == 4 || depth == 5) {
            int[] lengths = new int[depth];
            int[] lowerBounds = new int[depth];
            for (int i = 0; i < depth; i++) {
                lengths[i] = a.GetLength(i);
                lowerBounds[i] = a.GetLowerBound(i);
            }
            int[] elementIndices = new int[depth];
            thisHead = (heads != null && heads.Length > headIndex + 1) ? heads[headIndex + 1] : "List";
             for (int i = 0; i < lengths[0]; i++) {
                elementIndices[0] = lowerBounds[0] + i;
                PutFunction(thisHead, lengths[1]);
                thisHead = (heads != null && heads.Length > headIndex + 2) ? heads[headIndex + 2] : "List";
                 for (int j = 0; j < lengths[1]; j++) {
                    elementIndices[1] = lowerBounds[1] + j;
                    PutFunction(thisHead, lengths[2]);
                    if (depth == 3) {
                         for (int k = 0; k < lengths[2]; k++) {
                            elementIndices[2] = lowerBounds[2] + k;
                            if (elementsAreArrays)
                                putArrayPiecemeal((Array) a.GetValue(elementIndices), heads, headIndex + 3);
                            else
                                Put(a.GetValue(elementIndices));
                        }
                    } else {
                        thisHead = (heads != null && heads.Length > headIndex + 3) ? heads[headIndex + 3] : "List";
                         for (int k = 0; k < lengths[2]; k++) {
                            elementIndices[2] = lowerBounds[2] + k;
                            PutFunction(thisHead, lengths[3]);
                            if (depth == 4) {
                                 for (int m = 0; m < lengths[3]; m++) {
                                    elementIndices[3] = lowerBounds[3] + m;
                                    if (elementsAreArrays)
                                        putArrayPiecemeal((Array) a.GetValue(elementIndices), heads, headIndex + 4);
                                    else
                                        Put(a.GetValue(elementIndices));
                                }
                            } else {
                                thisHead = (heads != null && heads.Length > headIndex + 4) ? heads[headIndex + 4] : "List";
                                 for (int m = 0; m < lengths[3]; m++) {
                                    elementIndices[3] = lowerBounds[3] + m;
                                    PutFunction(thisHead, lengths[4]);
                                    // Depth must be 5.
                                     for (int n = 0; n < lengths[4]; n++) {
                                        elementIndices[4] = lowerBounds[4] + n;
                                        if (elementsAreArrays)
                                            putArrayPiecemeal((Array) a.GetValue(elementIndices), heads, headIndex + 5);
                                        else
                                            Put(a.GetValue(elementIndices));
                                    }
                                }   
                            }
                        }
                    }
                }
            }
        } else {
            // Depth > 5
            throw new ArgumentException("Cannot send an array deeper than 5 dimensions using .NET/Link.");
        }
    }


    // This is intended as a quick substitute for Utils.readArgAs(). We don't need everything that method has to offer,
    // and this is called in a tight loop.
    private object readAs(TypeCode leafTypeCode, Type leafType, bool isEnum) {

        if (isEnum) {
            int n = GetInteger();
            if (!Enum.IsDefined(leafType, n))
                throw new MathLinkException(MathLinkException.MLE_BAD_ENUM, "Enum value " + n + " out of range for type " + leafType.FullName + ".");
            return Enum.ToObject(leafType, n);
        } else {
            switch (leafTypeCode) {
                case TypeCode.Int32:
                    return GetInteger();
                case TypeCode.Double:
                    return GetDouble();
                case TypeCode.Char:
                    return (char) GetInteger();
                case TypeCode.String:
                    return GetString();
                case TypeCode.Boolean:
                    return GetBoolean();
                case TypeCode.Byte:
                    return (byte) GetInteger();
                case TypeCode.SByte:
                    return (sbyte) GetInteger();
                case TypeCode.Int16:
                    return (short) GetInteger();
                case TypeCode.UInt16:
                    return (ushort) GetInteger();
                case TypeCode.UInt32:
                    return (uint) GetInteger();
                case TypeCode.Int64:
                    return (long) GetDecimal();
                case TypeCode.UInt64:
                    return (ulong) GetDecimal();
                case TypeCode.Decimal:
                    return GetDecimal();
                case TypeCode.Single:
                    return (float) GetDouble();
                default:
                    if (leafType == typeof(Expr))
                        return GetExpr();
                    else if (leafType == ComplexType)
                        return GetComplex();
                    else
                        return GetObject();
            }
        }
    }


    // Discards interior array heads in array reads by calling GetFunction() once for every index at the end of its dimension.
    private void discardInnerHeads(int[] indices, int[] dims) {

        int numDimsAboutToWrap = 0;
        for (int j = indices.Length - 1; j >= 0; j--) {
            if (indices[j] == dims[j] - 1)
                numDimsAboutToWrap++;
            else
                break;
        }
        // If every dim is about to wrap, we are at the end of the array, so no more calls to GetFunction().
        if (numDimsAboutToWrap < indices.Length) {
            while (numDimsAboutToWrap > 0) {
                GetFunction(out var len);
                // Check that the array is the expected shape.
                if (len != dims[dims.Length - numDimsAboutToWrap])
                    throw new MathLinkException(MathLinkException.MLE_ARRAY_NOT_RECTANGULAR);
                numDimsAboutToWrap--;
            }
        }
    }

    /***************************************  Virtuals  *******************************************/
    
    // Must be implemented by derived classes to put strings.
    protected abstract void putString(string s);
    
    // This is the only method that derived classes must implement to put arrays.
    // Implementations can call putArrayPiecemeal to put arrays of anything,
    // but most will want to something more efficient in at least some circumstances
    // (for example, for primitive arrays). In other words, PutArray is where you have
    // your chance to do things in some efficient or special way, but you can always
    // call PutArrayPiecemeal to do as much of the work as you want. In your PutArray
    // implementation, you are guaranteed that the object is an array. Note that most
    // implementations will call PutArrayPiecemeal anyway to Put arrays of objects.
    protected abstract void putArray(Array a, string[] heads);
    
    // This is not implemented here because it relies on state (the setting for the complex class).
    // Both WrappedKErnelLink and the MathLink it wraps will inherit from this class, and we don't
    // want any ambiguity about whose state will be used.
    protected abstract void putComplex(object obj);
    
    // This method is the one that knows how to read object references off a link.
    // This method is called from the GetObject() if the incoming expr type is ExpressionType.Object.
    // That can only happen in an IKernelLink implementation, so here we throw an exception and 
    // let IKernelLink implementations override this to do the work of reading object refs.
    // If a programmer were to create an implementation of IKernelLink by subclassing this class
    // (instead of the more obvious choice KernelLinkImpl), they would need to override this method
    // to give it the ability to actually read objects.
    protected virtual object getObj() {
        // Would be a bug in .NET/Link (or a developer who was subclassing MathLinkImpl) if this exception ever occurred.
        throw new InvalidOperationException("Object references can only be read by an IKernelLink instance, not an IMathLink.");
    }
    
    // If a programmer were to create an implementation of IKernelLink by subclassing this class
    // (instead of the more obvious choice KernelLinkImpl), they would want to override this method
    // to give it the ability to actually send object refs.
    protected virtual void putRef(object obj) {
        putString(obj.ToString());
    }

    /************************************  Unimplemented  ***********************************/

    public abstract void Close();
    public abstract void Connect();
    public abstract void NewPacket();
    public abstract PacketType NextPacket();
    public abstract void EndPacket();
    public abstract bool ClearError();
    public abstract void Flush();
    public abstract ExpressionType GetNextExpressionType();
    public abstract ExpressionType GetExpressionType();
    public abstract void PutNext(ExpressionType type);
    public abstract int GetArgCount();
    public abstract void PutArgCount(int argCount);    
    public abstract void PutSize(int n);
    public abstract void PutData(byte[] data);
    public abstract byte[] GetData(int numRequested);
    public abstract int BytesToGet();
    public abstract int BytesToPut();
    public abstract string GetString();
    public abstract string GetSymbol();
    public abstract void PutSymbol(string s);
    public abstract byte[] GetByteString(int missing);
    public abstract int GetInteger();
    public abstract void Put(int i);
    public abstract double GetDouble();
    public abstract void Put(double d);
    public abstract void TransferExpression(IMathLink source);
    public abstract void TransferToEndOfLoopbackLink(ILoopbackLink source);    
    public abstract void PutMessage(MathLinkMessage msg);
    public abstract ILinkMark CreateMark();    
    public abstract void SeekMark(ILinkMark mark);
    public abstract void DestroyMark(ILinkMark mark);
    public abstract void DeviceInformation(int selector, IntPtr buf, ref int len);

    public abstract int Error {
        get;
    }
    public abstract string ErrorMessage {
        get;
    }
    public abstract bool Ready {
        get;
    }
    public abstract string Name {
        get;
    }

    public abstract event YieldFunction Yield;
    public abstract event MessageHandler MessageArrived;

}

}

