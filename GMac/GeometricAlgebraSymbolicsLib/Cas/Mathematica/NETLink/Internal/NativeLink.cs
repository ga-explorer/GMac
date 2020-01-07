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
using System.Collections;
using System.Runtime.InteropServices;

namespace Wolfram.NETLink.Internal {

/// <summary>
/// NativeLink is an IMathLink implementation that communicates with the MathLink library.
/// </summary>
/// 
internal class NativeLink : MathLinkImpl {

    protected static IMathLinkAPIProvider api;

    // Make these protected later. Public for testing.
    public static IntPtr env;   // The MLEnvironment pointer.
    public IntPtr link;         // The MLINK pointer.
    protected static object envLock;

    internal const int MLE_LINK_IS_NULL           = MathLinkException.MLEUSER + 100;
    private const string LINK_NULL_MESSAGE        = "Link is not open.";
    private const string CREATE_FAILED_MESSAGE    = "Link failed to open.";

    /*************************************  Constructors  **************************************/

    static NativeLink() {

        if (Utils.IsWindows) {
            if (Utils.Is64Bit)
                api = new Win64MathLinkAPIProvider();
            else
                api = new WindowsMathLinkAPIProvider();
        } else if (Utils.IsMac) {
            api = new MacMathLinkAPIProvider();
        } else {
            if (Utils.Is64Bit)
                api = new Unix64MathLinkAPIProvider();
            else
                api = new UnixMathLinkAPIProvider();
        }
        env = api.extMLBegin(IntPtr.Zero);
        envLock = new object();
    }


    internal NativeLink(string cmdLine) {
        
        // "autolaunch" is a special value indicating the user wants to launch the default kernel.
        if (cmdLine == "autolaunch")
            cmdLine = getDefaultLaunchString();
        lock (envLock) {
            // MLForceYield forces yielding under Unix even in the presence of a yield function. Irrelevant on Windows,
            // but we look to the future...
            link = api.extMLOpenString(env, cmdLine + " -linkoptions MLForceYield", out var err);
            if (link == IntPtr.Zero)
                throw new MathLinkException(MathLinkException.MLE_CREATION_FAILED, api.extMLErrorString(env, err));
            establishYieldFunction();
            establishMessageHandler();
        }
    }


    internal NativeLink(string[] argv) {
        // Default marshalling cannot handle an empty string in the array, so replace any such elements
        // with a manually null-terminated string. Also add MLForceYield.
        string[] newArgs = new string[argv.Length + 2];
        Array.Copy(argv, newArgs, argv.Length);
        for (int i = 0; i < argv.Length; i++)
            if (newArgs[i].Length == 0)
                newArgs[i] = "\0";
        newArgs[newArgs.Length - 2] = "-linkoptions";
        newArgs[newArgs.Length - 1] = "MLForceYield";
        lock (envLock) {
            link = api.extMLOpenInEnv(env, newArgs.Length, newArgs, out var err);
            if (link == IntPtr.Zero)
                throw new MathLinkException(MathLinkException.MLE_CREATION_FAILED, api.extMLErrorString(env, err));
            establishYieldFunction();
            establishMessageHandler();
        }
    }


    // Exists only for NativeLoopbackLink.
    protected internal NativeLink() {}


    /************************************  IMathLink Interface  **********************************/

    public override void Close() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        lock (envLock) {
            api.extMLClose(link);
        }
        link = IntPtr.Zero;
    }

    public override void Connect() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLConnect(link);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override void NewPacket() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        api.extMLNewPacket(link);
    }

    public override PacketType NextPacket() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int pkt = api.extMLNextPacket(link);
        if (pkt == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        return (PacketType) pkt;
    }

    public override void EndPacket() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLEndPacket(link);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override bool ClearError() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        return api.extMLClearError(link) != 0;
    }
    
    public override void Flush() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLFlush(link);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override string GetFunction(out int argCount) {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int type = api.extMLGetType(link);
        if (type == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        else if (type != 'F')  // MLTKFUNC
            throw new MathLinkException(MathLinkException.MLE_GETFUNCTION);
        api.extMLGetArgCount(link, out argCount);
        return GetSymbol();
    }

    public override ExpressionType GetNextExpressionType() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int type = api.extMLGetNext(link);
        if (type == 0) {
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        } else if (type == 35) {
            // MLTKSYMBOL
            ILinkMark mark = CreateMark();
            string s = GetSymbol();
            if (s == "True" || s == "False")
                type = 'T';
            SeekMark(mark);
            DestroyMark(mark);
        } else if (type == 'F') {
            // MLTKFUNC. See if it needs to be changed to Complex.
            ILinkMark mark = CreateMark();
            api.extMLGetArgCount(link, out var len);
            if (len == 2 && GetNextExpressionType() == ExpressionType.Symbol && GetSymbol() == "Complex")
                type = 'C';
            SeekMark(mark);
            DestroyMark(mark);
        }
        return (ExpressionType) type;
    }

    public override ExpressionType GetExpressionType() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int type = api.extMLGetType(link);
        if (type == 0) {
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        } else if (type == 35) {
            // MLTKSYMBOL
            ILinkMark mark = CreateMark();
            string s = GetSymbol();
            if (s == "True" || s == "False")
                type = 'T';
            SeekMark(mark);
            DestroyMark(mark);
        } else if (type == 'F') {
            // MLTKFUNC. See if it needs to be changed to Complex.
            ILinkMark mark = CreateMark();
            api.extMLGetArgCount(link, out var len);
            if (len == 2 && GetNextExpressionType() == ExpressionType.Symbol && GetSymbol() == "Complex")
                type = 'C';
            SeekMark(mark);
            DestroyMark(mark);
        }
        return (ExpressionType) type;
    }

    public override void PutNext(ExpressionType type){

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        // There is no reason for calling PutNext() for types Object or Boolean, but we should handle them.
        if (type == ExpressionType.Object || type == ExpressionType.Boolean)
            type = ExpressionType.Symbol;
        else if (type == ExpressionType.Complex)
            type = ExpressionType.Function;
        int err = api.extMLPutNext(link, (int) type);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override int GetArgCount() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLGetArgCount(link, out var argCount);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        return argCount;
    }

    public override void PutArgCount(int argCount){

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLPutArgCount(link, argCount);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }
    
    public override void PutSize(int n){

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLPutSize(link, n);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override void PutData(byte[] data){

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLPutData(link, data, data.Length);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override byte[] GetData(int numRequested) {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        byte[] data = new byte[numRequested];
        GCHandle arrayHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
        try {
            int err = api.extMLGetData(link, Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), numRequested, out var got);
            if (err == 0)
                throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
            if (got < numRequested) {
                byte[] newData = new byte[got];
                Array.Copy(data, newData, got);
                return newData;
            } else {
                return data;
            }
        } finally {
            arrayHandle.Free();
        }
    }

     public override int BytesToPut() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLBytesToPut(link, out var num);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        return num;
    }

    public override int BytesToGet() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLBytesToGet(link, out var num);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        return num;
    }

    public override string GetString() {
        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLGetUnicodeString(link, out var strAddress, out var len);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        String result;
        // TODO: (Low priority) Remove this extra test for len==0 when Mono is fixed.
        // This test on len == 0 is a temp hack to avoid a bug in Mono 0.95, which cannot
        // handle a 0-length string in the unsafe ctor below.
        if (len == 0) {
            result = String.Empty;
        } else {
            unsafe {
                char* pSrc = (char*) strAddress.ToPointer();
                result = new string(pSrc, 0, len);
            }
        }
        api.extMLDisownUnicodeString(link, strAddress, len);
        return result;
    }

    public override string GetSymbol() {
        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLGetUnicodeSymbol(link, out var strAddress, out var len);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        string result;
        // TODO: (Low priority) Remove this extra test for len==0 when Mono is fixed.
        // This test on len == 0 is a temp hack to avoid a bug in Mono 0.95, which cannot
        // handle a 0-length string in the unsafe ctor below.
        if (len == 0) {
            result = String.Empty;
        } else {
            unsafe {
                char* pSrc = (char*) strAddress.ToPointer();
                result = new string(pSrc, 0, len);
            }
        }
        api.extMLDisownUnicodeSymbol(link, strAddress, len);
        return result;
    }

    public override void PutSymbol(string s) {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLPutUnicodeSymbol(link, s, s.Length);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override byte[] GetByteString(int missing) {
        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLGetByteString(link, out var strAddress, out var len, missing);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        byte[] result = new byte[len];
        unsafe {
            byte* pSrc = (byte*) strAddress.ToPointer();
            for (int i = 0; i < len; i++)
                result[i] = *pSrc++;
        }
        api.extMLDisownByteString(link, strAddress, len);
        return result;
    }
    
    public override int GetInteger() {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLGetInteger(link, out var i);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        return i;
    }

    public override void Put(int i) {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLPutInteger(link, i);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override double GetDouble() {
        
        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLGetDouble(link, out var d);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        return d;
    }

    public override void Put(double d) {

        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        int err = api.extMLPutDouble(link, d);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }

    public override void TransferExpression(IMathLink source) {

        if (source is NativeLink) {
            NativeLink loop = (NativeLink) source;
            if (link == IntPtr.Zero || loop.link == IntPtr.Zero)
                throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
            if (api.extMLTransferExpression(link, loop.link) == 0)
                throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        } else if (source is WrappedKernelLink) {
            // We want to drill down if the other side is a WrappedKernelLink to call
            // the efficient MLTransferExpression if the wrapped link is a NativeLink.
            TransferExpression(((WrappedKernelLink) source).GetMathLink());
        } else {
            Put(source.GetExpr());
        }
        if (source.Error != 0)
            throw new MathLinkException(source.Error, source.ErrorMessage);
    }

    public override void TransferToEndOfLoopbackLink(ILoopbackLink source) {
        
        if (source is NativeLoopbackLink) {
            NativeLoopbackLink loop = (NativeLoopbackLink) source;
            if (link == IntPtr.Zero || loop.link == IntPtr.Zero)
                throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
            if (api.extMLTransferToEndOfLoopbackLink(link, loop.link) == 0)
                throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        } else {
            // Not likely to ever be used. Would require someone to write an implementation
            // of ILoopbackLink other than NativeLoopbackLink.
            while (source.Ready)
                Put(source.GetExpr());
        }
        if (source.Error != 0)
            throw new MathLinkException(source.Error, source.ErrorMessage);
    }
    
    public override void PutMessage(MathLinkMessage msg) {
        api.extMLPutMessage(link, (int) msg);
    }

    public override ILinkMark CreateMark() {

        IntPtr nMark = api.extMLCreateMark(link);
        if (nMark == IntPtr.Zero)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        return new NativeMark(this, nMark);
    }
    
    public override void SeekMark(ILinkMark mark) {
        api.extMLSeekMark(link, mark.Mark, 0);
    }

    public override void DestroyMark(ILinkMark mark) {
        api.extMLDestroyMark(link, mark.Mark);
    }

    
    public override Array GetArray(Type leafType, int depth, out string[] heads) {
        
        if (link == IntPtr.Zero)
            throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
        Array result;
        IntPtr dataAddress, dimsAddress, headsAddress;
        int actualDepth;
        int[] lengths = new int[depth];
        switch (Type.GetTypeCode(leafType)) {
            case TypeCode.Int32: {
                int err = api.extMLGetIntegerArray(link, out dataAddress, out dimsAddress, out headsAddress, out actualDepth);
                if (err == 0)
                    throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
                try {
                    if (actualDepth != depth)
                        throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                    unsafe {
                        int* pSrc = (int*) dataAddress.ToPointer();
                        int* pDims = (int*) dimsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            lengths[i] = pDims[i];
                        if (depth == 1) {
                            int len = lengths[0];
                            int[] data = new int[len];
                            for (int i = 0; i < len; i++)
                                data[i] = *pSrc++;
                            result = data;
                        } else if (depth == 2) {
                            int len1 = lengths[0];
                            int len2 = lengths[1];
                            int[,] data = new int[len1, len2];
                            for (int i = 0; i < len1; i++)
                                for (int j = 0; j < len2; j++)
                                    data[i,j] = *pSrc++;
                            result = data;
                        } else {
                            result = Array.CreateInstance(typeof(int), lengths);
                            if (result.Length == 0)
                                break;
                            int[] indices = new int[depth];
                            do {
                                result.SetValue(*pSrc++, indices);
                            } while (Utils.nextIndex(indices, lengths));
                        }
                        sbyte** pHeads = (sbyte**) headsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            headsHolder[i] = new string(pHeads[i]);
                    }
                } finally {
                    api.extMLDisownIntegerArray(link, dataAddress, dimsAddress, headsAddress, actualDepth);
                }
                break;
            }
            case TypeCode.Byte: {
                int err = api.extMLGetByteArray(link, out dataAddress, out dimsAddress, out headsAddress, out actualDepth);
                if (err == 0)
                    throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
                try {
                    if (actualDepth != depth)
                        throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                    unsafe {
                        byte* pSrc = (byte*) dataAddress.ToPointer();
                        int* pDims = (int*) dimsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            lengths[i] = pDims[i];
                        if (depth == 1) {
                            int len = lengths[0];
                            byte[] data = new byte[len];
                            for (int i = 0; i < len; i++)
                                data[i] = *pSrc++;
                            result = data;
                        } else if (depth == 2) {
                            int len1 = lengths[0];
                            int len2 = lengths[1];
                            byte[,] data = new byte[len1, len2];
                            for (int i = 0; i < len1; i++)
                                for (int j = 0; j < len2; j++)
                                    data[i,j] = *pSrc++;
                            result = data;
                        } else {
                            result = Array.CreateInstance(typeof(byte), lengths);
                            if (result.Length == 0)
                                break;
                            int[] indices = new int[depth];
                            do {
                                result.SetValue(*pSrc++, indices);
                            } while (Utils.nextIndex(indices, lengths));
                        }
                        sbyte** pHeads = (sbyte**) headsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            headsHolder[i] = new string(pHeads[i]);
                    }
                } finally {
                    api.extMLDisownByteArray(link, dataAddress, dimsAddress, headsAddress, actualDepth);
                }
                break;
            }
            case TypeCode.SByte: {
                int err = api.extMLGetByteArray(link, out dataAddress, out dimsAddress, out headsAddress, out actualDepth);
                if (err == 0)
                    throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
                try {
                    if (actualDepth != depth)
                        throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                    unsafe {
                        sbyte* pSrc = (sbyte*) dataAddress.ToPointer();
                        int* pDims = (int*) dimsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            lengths[i] = pDims[i];
                        if (depth == 1) {
                            int len = lengths[0];
                            sbyte[] data = new sbyte[len];
                            for (int i = 0; i < len; i++)
                                data[i] = *pSrc++;
                            result = data;
                        } else if (depth == 2) {
                            int len1 = lengths[0];
                            int len2 = lengths[1];
                            sbyte[,] data = new sbyte[len1, len2];
                            for (int i = 0; i < len1; i++)
                                for (int j = 0; j < len2; j++)
                                    data[i,j] = *pSrc++;
                            result = data;
                        } else {
                            result = Array.CreateInstance(typeof(sbyte), lengths);
                            if (result.Length == 0)
                                break;
                            int[] indices = new int[depth];
                            do {
                                result.SetValue(*pSrc++, indices);
                            } while (Utils.nextIndex(indices, lengths));
                        }
                        sbyte** pHeads = (sbyte**) headsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            headsHolder[i] = new string(pHeads[i]);
                    }
                } finally {
                    api.extMLDisownByteArray(link, dataAddress, dimsAddress, headsAddress, actualDepth);
                }
                break;
            }
            case TypeCode.Char: {
                int err = api.extMLGetShortIntegerArray(link, out dataAddress, out dimsAddress, out headsAddress, out actualDepth);
                if (err == 0)
                    throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
                try {
                    if (actualDepth != depth)
                        throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                    unsafe {
                        char* pSrc = (char*) dataAddress.ToPointer();
                        int* pDims = (int*) dimsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            lengths[i] = pDims[i];
                        if (depth == 1) {
                            int len = lengths[0];
                            char[] data = new char[len];
                            for (int i = 0; i < len; i++)
                                data[i] = *pSrc++;
                            result = data;
                        } else if (depth == 2) {
                            int len1 = lengths[0];
                            int len2 = lengths[1];
                            char[,] data = new char[len1, len2];
                            for (int i = 0; i < len1; i++)
                                for (int j = 0; j < len2; j++)
                                    data[i,j] = *pSrc++;
                            result = data;
                        } else {
                            result = Array.CreateInstance(typeof(char), lengths);
                            if (result.Length == 0)
                                break;
                            int[] indices = new int[depth];
                            do {
                                result.SetValue(*pSrc++, indices);
                            } while (Utils.nextIndex(indices, lengths));
                        }
                        sbyte** pHeads = (sbyte**) headsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            headsHolder[i] = new string(pHeads[i]);
                    }
                } finally {
                    api.extMLDisownShortIntegerArray(link, dataAddress, dimsAddress, headsAddress, actualDepth);
                }
                break;
            }
            case TypeCode.Int16: {
                int err = api.extMLGetShortIntegerArray(link, out dataAddress, out dimsAddress, out headsAddress, out actualDepth);
                if (err == 0)
                    throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
                try {
                    if (actualDepth != depth)
                        throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                    unsafe {
                        short* pSrc = (short*) dataAddress.ToPointer();
                        int* pDims = (int*) dimsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            lengths[i] = pDims[i];
                        if (depth == 1) {
                            int len = lengths[0];
                            short[] data = new short[len];
                            for (int i = 0; i < len; i++)
                                data[i] = *pSrc++;
                            result = data;
                        } else if (depth == 2) {
                            int len1 = lengths[0];
                            int len2 = lengths[1];
                            short[,] data = new short[len1, len2];
                            for (int i = 0; i < len1; i++)
                                for (int j = 0; j < len2; j++)
                                    data[i,j] = *pSrc++;
                            result = data;
                        } else {
                            result = Array.CreateInstance(typeof(short), lengths);
                            if (result.Length == 0)
                                break;
                            int[] indices = new int[depth];
                            do {
                                result.SetValue(*pSrc++, indices);
                            } while (Utils.nextIndex(indices, lengths));
                        }
                        sbyte** pHeads = (sbyte**) headsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            headsHolder[i] = new string(pHeads[i]);
                    }
                } finally {
                    api.extMLDisownShortIntegerArray(link, dataAddress, dimsAddress, headsAddress, actualDepth);
                }
                break;
            }
            case TypeCode.UInt16: {
                int err = api.extMLGetShortIntegerArray(link, out dataAddress, out dimsAddress, out headsAddress, out actualDepth);
                if (err == 0)
                    throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
                try {
                    if (actualDepth != depth)
                        throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                    unsafe {
                        ushort* pSrc = (ushort*) dataAddress.ToPointer();
                        int* pDims = (int*) dimsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            lengths[i] = pDims[i];
                        if (depth == 1) {
                            int len = lengths[0];
                            ushort[] data = new ushort[len];
                            for (int i = 0; i < len; i++)
                                data[i] = *pSrc++;
                            result = data;
                        } else if (depth == 2) {
                            int len1 = lengths[0];
                            int len2 = lengths[1];
                            ushort[,] data = new ushort[len1, len2];
                            for (int i = 0; i < len1; i++)
                                for (int j = 0; j < len2; j++)
                                    data[i,j] = *pSrc++;
                            result = data;
                        } else {
                            result = Array.CreateInstance(typeof(ushort), lengths);
                            if (result.Length == 0)
                                break;
                            int[] indices = new int[depth];
                            do {
                                result.SetValue(*pSrc++, indices);
                            } while (Utils.nextIndex(indices, lengths));
                        }
                        sbyte** pHeads = (sbyte**) headsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            headsHolder[i] = new string(pHeads[i]);
                    }
                } finally {
                    api.extMLDisownShortIntegerArray(link, dataAddress, dimsAddress, headsAddress, actualDepth);
                }
                break;
            }
            case TypeCode.Double: {
                int err = api.extMLGetDoubleArray(link, out dataAddress, out dimsAddress, out headsAddress, out actualDepth);
                if (err == 0)
                    throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
                try {
                    if (actualDepth != depth)
                        throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                    unsafe {
                        double* pSrc = (double*) dataAddress.ToPointer();
                        int* pDims = (int*) dimsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            lengths[i] = pDims[i];
                        if (depth == 1) {
                            int len = lengths[0];
                            double[] data = new double[len];
                            for (int i = 0; i < len; i++)
                                data[i] = *pSrc++;
                            result = data;
                        } else if (depth == 2) {
                            int len1 = lengths[0];
                            int len2 = lengths[1];
                            double[,] data = new double[len1, len2];
                            for (int i = 0; i < len1; i++)
                                for (int j = 0; j < len2; j++)
                                    data[i,j] = *pSrc++;
                            result = data;
                        } else {
                            result = Array.CreateInstance(typeof(double), lengths);
                            if (result.Length == 0)
                                break;
                            int[] indices = new int[depth];
                            do {
                                result.SetValue(*pSrc++, indices);
                            } while (Utils.nextIndex(indices, lengths));
                        }
                        sbyte** pHeads = (sbyte**) headsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            headsHolder[i] = new string(pHeads[i]);
                    }
                } finally {
                    api.extMLDisownDoubleArray(link, dataAddress, dimsAddress, headsAddress, actualDepth);
                }
                break;
            }
            case TypeCode.Single: {
                int err = api.extMLGetFloatArray(link, out dataAddress, out dimsAddress, out headsAddress, out actualDepth);
                if (err == 0)
                    throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
                try {
                    if (actualDepth != depth)
                        throw new MathLinkException(MathLinkException.MLE_BAD_ARRAY_DEPTH);
                    unsafe {
                        float* pSrc = (float*) dataAddress.ToPointer();
                        int* pDims = (int*) dimsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            lengths[i] = pDims[i];
                        if (depth == 1) {
                            int len = lengths[0];
                            float[] data = new float[len];
                            for (int i = 0; i < len; i++)
                                data[i] = *pSrc++;
                            result = data;
                        } else if (depth == 2) {
                            int len1 = lengths[0];
                            int len2 = lengths[1];
                            float[,] data = new float[len1, len2];
                            for (int i = 0; i < len1; i++)
                                for (int j = 0; j < len2; j++)
                                    data[i,j] = *pSrc++;
                            result = data;
                        } else {
                            result = Array.CreateInstance(typeof(float), lengths);
                            if (result.Length == 0)
                                break;
                            int[] indices = new int[depth];
                            do {
                                result.SetValue(*pSrc++, indices);
                            } while (Utils.nextIndex(indices, lengths));
                        }
                        sbyte** pHeads = (sbyte**) headsAddress.ToPointer();
                        for (int i = 0; i < depth; i++)
                            headsHolder[i] = new string(pHeads[i]);
                    }
                } finally {
                    api.extMLDisownFloatArray(link, dataAddress, dimsAddress, headsAddress, actualDepth);
                }
                break;
            }
            default:
                // All types that cannot be handled by native ML API calls go to superclass for piecemeal reads.
                result = base.GetArray(leafType, depth, out heads);
                break;
        }
        heads = new string[depth];
        Array.Copy(headsHolder, heads, depth);
        return result;
    }

    
    public override void DeviceInformation(int selector, IntPtr buf, ref int len) {

        int err = api.extMLDeviceInformation(link, (uint) selector, buf, ref len);
        if (err == 0)
            throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
    }


    /***********************************  IMathLink Properties  ************************************/

    public override int Error {
        get {
            if (link == IntPtr.Zero)
                throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
            return api.extMLError(link);
        }
    }
    
    public override string ErrorMessage {
        get { return link == IntPtr.Zero ? LINK_NULL_MESSAGE : api.extMLErrorMessage(link); }
    }
    
    public override bool Ready {
        get {
            if (link == IntPtr.Zero)
                throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
            return api.extMLReady(link) != 0;
        }
    }

    public override string Name {
        get {
            if (link == IntPtr.Zero)
                throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
            return api.extMLName(link);
        }
    }
    

    /********************************  Non-public method implementations  ******************************/

    protected override void putArray(Array a, string[] heads) {

        Type elementType = a.GetType().GetElementType();
        bool elementsAreArrays = elementType.IsArray;

        if (!Utils.IsTrulyPrimitive(elementType) || elementType == typeof(bool) || elementsAreArrays) {
            // elementsAreArrays --> Array is jagged.
            putArrayPiecemeal(a, heads, 0);
        } else {
            // Rectangular array of a type that we can handle efficiently.
            int depth = a.Rank;
            int[] dims = new int[depth];
            for (int i = 0; i < depth; i++)
                dims[i] = a.GetLength(i);
            // If the array is empty, the fixed() blocks below will throw an "Array index out of bounds" exception.
            // Thus we handle them here.
            if (dims[depth - 1] == 0) {
                putArrayPiecemeal(a, heads, 0);
                return;
            }
            unsafe {
                switch (Type.GetTypeCode(elementType)) {
                    case TypeCode.Byte: {
                        switch (depth) {
                            case 1: {
                                byte[] ba = (byte[]) a;
                                fixed (byte* pData = ba) {
                                    api.extMLPutByteArray(link, pData, dims, heads, 1);
                                }
                                break;
                            }
                            case 2: {
                                byte[,] ba = (byte[,]) a;
                                fixed (byte* pData = ba) {
                                    api.extMLPutByteArray(link, pData, dims, heads, 2);
                                }
                                break;
                            }
                            case 3: {
                                byte[,,] ba = (byte[,,]) a;
                                fixed (byte* pData = ba) {
                                    api.extMLPutByteArray(link, pData, dims, heads, 3);
                                }
                                break;
                            }
                            default: {
                                // Faster than putArrayPiecemeal().
                                byte[] data = new byte[a.Length];
                                int i = 0;
                                foreach (byte b in a)
                                    data[i++] = b;
                                fixed (byte* pData = data) {
                                    api.extMLPutByteArray(link, pData, dims, heads, depth);
                                }
                                break;
                            }
                        }
                        break;
                    }
                    case TypeCode.SByte: {
                        short[] data = new short[a.Length];
                        int i = 0;
                        foreach (sbyte b in a)
                            data[i++] = b;
                        fixed (short* pData = data) {
                            api.extMLPutShortIntegerArray(link, pData, dims, heads, depth);
                        }
                        break;
                    }
                    case TypeCode.Char: {
                        int[] data = new int[a.Length];
                        int i = 0;
                        foreach (char b in a)
                            data[i++] = b;
                        fixed (int* pData = data) {
                            api.extMLPutIntegerArray(link, pData, dims, heads, depth);
                        }
                        break;
                    }
                    case TypeCode.UInt16: {
                        int[] data = new int[a.Length];
                        int i = 0;
                        foreach (ushort b in a)
                            data[i++] = b;
                        fixed (int* pData = data) {
                            api.extMLPutIntegerArray(link, pData, dims, heads, depth);
                        }
                        break;
                    }
                    case TypeCode.Int16: {
                        switch (depth) {
                            case 1: {
                                short[] sa = (short[]) a;
                                fixed (short* pData = sa) {
                                    api.extMLPutShortIntegerArray(link, pData, dims, heads, 1);
                                }
                                break;
                            }
                            case 2: {
                                short[,] sa = (short[,]) a;
                                fixed (short* pData = sa) {
                                    api.extMLPutShortIntegerArray(link, pData, dims, heads, 2);
                                }
                                break;
                            }
                            case 3: {
                                short[,,] sa = (short[,,]) a;
                                fixed (short* pData = sa) {
                                    api.extMLPutShortIntegerArray(link, pData, dims, heads, 3);
                                }
                                break;
                            }
                            default: {
                                short[] data = new short[a.Length];
                                int i = 0;
                                foreach (short b in a)
                                    data[i++] = b;
                                fixed (short* pData = data) {
                                    api.extMLPutShortIntegerArray(link, pData, dims, heads, depth);
                                }
                                break;
                            }
                        }
                        break;
                    }
                    case TypeCode.Int32: {
                        switch (depth) {
                            case 1: {
                                int[] ia = (int[]) a;
                                fixed (int* pData = ia) {
                                    api.extMLPutIntegerArray(link, pData, dims, heads, 1);
                                }
                                break;
                            }
                            case 2: {
                                int[,] ia = (int[,]) a;
                                fixed (int* pData = ia) {
                                    api.extMLPutIntegerArray(link, pData, dims, heads, 2);
                                }
                                break;
                            }
                            case 3: {
                                int[,,] ia = (int[,,]) a;
                                fixed (int* pData = ia) {
                                    api.extMLPutIntegerArray(link, pData, dims, heads, 3);
                                }
                                break;
                            }
                            default: {
                                int[] data = new int[a.Length];
                                int i = 0;
                                foreach (int b in a)
                                    data[i++] = b;
                                fixed (int* pData = data) {
                                    api.extMLPutIntegerArray(link, pData, dims, heads, depth);
                                }
                                break;
                            }
                        }
                        break;
                    }
                    case TypeCode.Single: {
                        switch (depth) {
                            case 1: {
                                float[] fa = (float[]) a;
                                fixed (float* pData = fa) {
                                    api.extMLPutFloatArray(link, pData, dims, heads, 1);
                                }
                                break;
                            }
                            case 2: {
                                float[,] fa = (float[,]) a;
                                fixed (float* pData = fa) {
                                    api.extMLPutFloatArray(link, pData, dims, heads, 2);
                                }
                                break;
                            }
                            case 3: {
                                float[,,] fa = (float[,,]) a;
                                fixed (float* pData = fa) {
                                    api.extMLPutFloatArray(link, pData, dims, heads, 3);
                                }
                                break;
                            }
                            default: {
                                float[] data = new float[a.Length];
                                int i = 0;
                                foreach (float b in a)
                                    data[i++] = b;
                                fixed (float* pData = data) {
                                    api.extMLPutFloatArray(link, pData, dims, heads, depth);
                                }
                                break;
                            }
                        }
                        break;
                    }
                    case TypeCode.Double: {
                        switch (depth) {
                            case 1: {
                                double[] da = (double[]) a;
                                fixed (double* pData = da) {
                                    api.extMLPutDoubleArray(link, pData, dims, heads, 1);
                                }
                                break;
                            }
                            case 2: {
                                double[,] da = (double[,]) a;
                                fixed (double* pData = da) {
                                    api.extMLPutDoubleArray(link, pData, dims, heads, 2);
                                }
                                break;
                            }
                            case 3: {
                                double[,,] da = (double[,,]) a;
                                fixed (double* pData = da) {
                                    api.extMLPutDoubleArray(link, pData, dims, heads, 3);
                                }
                                break;
                            }
                            default: {
                                double[] data = new double[a.Length];
                                int i = 0;
                                foreach (double b in a)
                                    data[i++] = b;
                                fixed (double* pData = data) {
                                    api.extMLPutDoubleArray(link, pData, dims, heads, depth);
                                }
                                break;
                            }
                        }
                        break;
                    }
                    default:
                        // UInt32, Int64, UInt64, Decimal. For other types, this is just a safety valve because we
                        // should never get here. Object types should never be seen by an IMathLink implementation.
                        putArrayPiecemeal(a, heads, 0);
                        break;
                }
            }
        }
    }


    protected override void putString(string s) {

         lock (this) {
            if (link == IntPtr.Zero)
                throw new MathLinkException(MLE_LINK_IS_NULL, LINK_NULL_MESSAGE);
            int err = api.extMLPutUnicodeString(link, s, s.Length);
            if (err == 0)
                throw new MathLinkException(api.extMLError(link), api.extMLErrorMessage(link));
        }
    }


    protected override void putComplex(object obj) {
        complexHandler.PutComplex(this, obj);
    }


    // TODO: Beef this up to be able to correctly answer the question implied by its name...
    internal static bool canUseMathLinkLibrary() {
        return true;
    }


    /**********************************  Yielder, MessageHandler  **********************************/

    public override event YieldFunction Yield;
    public override event MessageHandler MessageArrived;

    // Delegates are, in effect, typedefs. These delegates are the ones that map to the native MathLink API
    // function pointers MLYieldFunctionType and MLMessageHandlerType.
    internal delegate bool YielderCallback(IntPtr a, IntPtr b);
    internal delegate void MessageCallback(IntPtr link, int msg, int ignore);

    // We need to hold refs to these delegates in instance variables so that they cannot be garbage-collected.
    private YielderCallback yielder;
    private MessageCallback msgHandler;

    // This sets up a callback from C into .NET. This is distinct from the top-level yield handlers attached
    // to the Yield event.
    private void establishYieldFunction() {
        yielder = new YielderCallback(yielderCallbackFunction);
        IntPtr yfo = api.extMLCreateYieldFunction(env, yielder, IntPtr.Zero);
        api.extMLSetYieldFunction(link, yfo);
    }

    // This sets up a callback from C into .NET. This is distinct from the top-level message handlers attached
    // to the MessageArrived event.
    private void establishMessageHandler() {
        msgHandler = new MessageCallback(messageCallbackFunction);
        IntPtr mho = api.extMLCreateMessageHandler(env, msgHandler, IntPtr.Zero);
        api.extMLSetMessageHandler(link, mho);
    }


    // This is the delegate method called directly from C. We manually invoke the delegates attached to the Yield event.
    private bool yielderCallbackFunction(IntPtr a, IntPtr b) {

        bool backOut = false;
        if (Yield != null) {
            Delegate[] yielders = Yield.GetInvocationList();
            if (yielders.Length > 0) {
                foreach (Delegate d in yielders) {
                    backOut = (bool) d.DynamicInvoke(null);
                    if (backOut)
                        break;
                }
            }
        }
        return backOut;
    }


    // This is the delegate method called directly from C. We manually invoke the delegates attached to the MessageArrived event.
    private void messageCallbackFunction(IntPtr link, int msg, int ignore) {

        System.Diagnostics.Debug.WriteLine("In messageCallBackFunction");
        if (MessageArrived != null)
            MessageArrived((MathLinkMessage) msg);
    }
 

    /***************************************  Private  ******************************************/

    // Use VersionComparer as a means to compare version numbers like 9.0 and 10.0.
    // Can have any number of period-separated components. Returns 1 if v1 < v2, -1 if v1 > v2, and 0 if they are equal.
    // In terms of direction, it's like subtraction: v2-v1.
    private class VersionComparer : IComparer {    
        public int Compare(Object v1, Object v2) {
            String[] parts1 = ((String) v1).Split('.');
            String[] parts2 = ((String) v2).Split('.');

            for (int i = 0; i < Math.Max(parts1.Length, parts2.Length); i++) {
                int digits1 = (parts1.Length > i) ? Int32.Parse(parts1[i]) : 0;
                int digits2 = (parts2.Length > i) ? Int32.Parse(parts2[i]) : 0;
                if (digits1 > digits2)
                    return -1;
                else if (digits1 < digits2)
                    return 1;
            }
            return 0;
        }
    }
    
    // Returns a cmdLine string suitable for launching the default mathkernel.exe on the system.
    private string getDefaultLaunchString() {

        try {
            string path;
            // First, look for the WolframProductRegistry file, which exists for 4.2 and later.
            string wriFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                                        @"\Mathematica\WolframProductRegistry";
            if (System.IO.File.Exists(wriFile)) {
                SortedList recs = new SortedList(new VersionComparer());
                using (System.IO.FileStream strm = System.IO.File.OpenRead(wriFile)) {
                    System.IO.StreamReader reader = new System.IO.StreamReader(strm);
                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine()) {
                        // Look for lines like: "Mathematica x.x=path"
                        if (line.StartsWith("Mathematica ")) {
                            string version = line.Split(' ', '=')[1];
                            path = line.Split('=')[1];
                            try { recs.Add(version, path); } catch (Exception) {/* ignore duplicate keys */}
                        }
                    }
                }
                // recs will be sorted with highest version numbers first.
                foreach (DictionaryEntry de in recs) {
                    path = (string)de.Value;
                    if (System.IO.File.Exists(path)) {
                        // To avoid backslash/quoting problems, just replace \ with /, which work fine.
                        path = path.Replace(@"\", "/").Replace("Mathematica.exe", "MathKernel.exe");
                        return "-linkmode launch -linkname \"" + path + "\"";
                    }
                }
            }

            // Will get here if either the WolframProductRegistry was not found or it did not have any useful info.
            // Next technique is to look at registry key HKCR/.nb.
            string bestKey = (string) Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(".nb").GetValue("");
            path = (string) Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(bestKey).OpenSubKey("DefaultIcon").GetValue("");
            path = path.Split(',')[0];
            // Possible (maybe always?) that this string in the registry has literal "" chars around it, so strip them if they exist.
            if (path.StartsWith("\""))
                path = path.Substring(1, path.Length - 2);
            if (System.IO.File.Exists(path)) {
                // To avoid backslash/quoting problems, just replace \ with /, which work fine.
                path = path.Replace(@"\", "/").Replace("Mathematica.exe", "MathKernel.exe");
                return "-linkmode launch -linkname \"" + path + "\"";
            }
        }
        catch (Exception) {
            // Don't want to propagate exceptions. Just return a basic launch string and let MathLink's
            // "find a program to launch" dialog appear.
        }
        return "-linkmode launch";
    }

}

}