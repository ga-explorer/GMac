using System;
using System.Runtime.InteropServices;

namespace Wolfram.NETLink.Internal
{

    /// <summary>
    /// This class provides the implementation of the low-level MathLink functions (functions
    /// named extMLxxx) for the OSX platform by delegating to calls in the native
    /// MathLink library.
    /// </summary>
    /// 
    internal class MacMathLinkAPIProvider : IMathLinkAPIProvider
    {


        /***********************************************************************
         *  These functions are just instance method --> static method mappings.
         *  They are duplicated in the Windows and Unix versions of this class.
         *  If you change any here, make sure you make parallel changes in the
         *  other class.
         ************************************************************************/

        public IntPtr extMLBegin(IntPtr zero)
        {
            return MLBegin(zero);
        }

        public void extMLEnd(IntPtr env)
        {
            MLEnd(env);
        }

        public IntPtr extMLOpenString(IntPtr env, string commandLine, out int err)
        {
            return MLOpenString(env, commandLine, out err);
        }

        public IntPtr extMLOpenInEnv(IntPtr env, int argc, string[] argv, out int err)
        {
            return MLOpenInEnv(env, argc, argv, out err);
        }

        public IntPtr extMLLoopbackOpen(IntPtr env, out int err)
        {
            return MLLoopbackOpen(env, out err);
        }

        public void extMLClose(IntPtr link)
        {
            MLClose(link);
        }

        public int extMLConnect(IntPtr link)
        {
            return MLConnect(link);
        }

        public string extMLName(IntPtr link) {
            IntPtr p = MLName(link);
            return Marshal.PtrToStringAnsi(p);
        }

        public int extMLNewPacket(IntPtr link) {
            return MLNewPacket(link);
        }

        public int extMLNextPacket(IntPtr link) {
            return MLNextPacket(link);
        }

        public int extMLEndPacket(IntPtr link) {
            return MLEndPacket(link);
        }

        public int extMLError(IntPtr link) {
            return MLError(link);
        }

        public int extMLClearError(IntPtr link) {
            return MLClearError(link);
        }

        public string extMLErrorMessage(IntPtr link) {
            IntPtr p = MLErrorMessage(link);
            return Marshal.PtrToStringAnsi(p);
        }

        public string extMLErrorString(IntPtr env, int err) {
            IntPtr p = MLErrorString(env, err);
            return Marshal.PtrToStringAnsi(p);
        }
        public int extMLSetError(IntPtr link, int err)
        {
            return MLSetError(link, err);
        }

        public int extMLReady(IntPtr link)
        {
            return MLReady(link);
        }

        public int extMLFlush(IntPtr link)
        {
            return MLFlush(link);
        }

        public int extMLGetNext(IntPtr link)
        {
            return MLGetNext(link);
        }

        public int extMLGetType(IntPtr link)
        {
            return MLGetType(link);
        }

        public int extMLGetNextRaw(IntPtr link)
        {
            return MLGetNextRaw(link);
        }

        public int extMLPutNext(IntPtr link, int type)
        {
            return MLPutNext(link, type);
        }

        public int extMLGetArgCount(IntPtr link, out int argCount)
        {
            return MLGetArgCount(link, out argCount);
        }

        public int extMLPutArgCount(IntPtr link, int argCount)
        {
            return MLPutArgCount(link, argCount);
        }

        public int extMLPutData(IntPtr link, byte[] buf, int len)
        {
            return MLPutData(link, buf, len);
        }

        public int extMLPutSize(IntPtr link, int len)
        {
            return MLPutSize(link, len);
        }

        public int extMLGetData(IntPtr link, IntPtr data, int len, out int num)
        {
            return MLGetData(link, data, len, out num);
        }

        public int extMLBytesToGet(IntPtr link, out int num)
        {
            return MLBytesToGet(link, out num);
        }

        public int extMLBytesToPut(IntPtr link, out int num)
        {
            return MLBytesToPut(link, out num);
        }

        public int extMLPutFunction(IntPtr link, string s, int argCount)
        {
            return MLPutFunction(link, s, argCount);
        }

        public int extMLGetUnicodeString(IntPtr link, out IntPtr strAddress, out int len)
        {
            return MLGetUCS2String(link, out strAddress, out len);
        }

        public void extMLDisownUnicodeString(IntPtr link, IntPtr strAddress, int len)
        {
            MLReleaseUCS2String(link, strAddress, len);
        }

        public int extMLPutUnicodeString(IntPtr link, string s, int len)
        {
            return MLPutUCS2String(link, s, len);
        }

        public int extMLGetByteString(IntPtr link, out IntPtr strAddress, out int len, int missing)
        {
            return MLGetByteString(link, out strAddress, out len, missing);
        }

        public void extMLDisownByteString(IntPtr link, IntPtr strAddress, int len)
        {
            MLReleaseByteString(link, strAddress, len);
        }

        public int extMLGetUnicodeSymbol(IntPtr link, out IntPtr strAddress, out int len)
        {
            return MLGetUCS2Symbol(link, out strAddress, out len);
        }

        public void extMLDisownUnicodeSymbol(IntPtr link, IntPtr strAddress, int len)
        {
            MLReleaseUCS2Symbol(link, strAddress, len);
        }

        public int extMLPutUnicodeSymbol(IntPtr link, string s, int len)
        {
            return MLPutUCS2Symbol(link, s, len);
        }

        public int extMLGetInteger(IntPtr link, out int i)
        {
            return MLGetInteger(link, out i);
        }

        public int extMLPutInteger(IntPtr link, int i)
        {
            return MLPutInteger(link, i);
        }

        public int extMLGetDouble(IntPtr link, out double d)
        {
            return MLGetDouble(link, out d);
        }

        public int extMLPutDouble(IntPtr link, double d)
        {
            return MLPutDouble(link, d);
        }

        public int extMLGetByteArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth)
        {
            return MLGetByteArray(link, out dataAddress, out dimsAddress, out headsAddress, out depth);
        }

        public void extMLDisownByteArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth)
        {
            MLReleaseByteArray(link, dataAddress, dimsAddress, headsAddress, depth);
        }

        public int extMLGetShortIntegerArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth)
        {
            return MLGetInteger16Array(link, out dataAddress, out dimsAddress, out headsAddress, out depth);
        }

        public void extMLDisownShortIntegerArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth)
        {
            MLReleaseInteger16Array(link, dataAddress, dimsAddress, headsAddress, depth);
        }

        public int extMLGetIntegerArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth)
        {
            return MLGetInteger32Array(link, out dataAddress, out dimsAddress, out headsAddress, out depth);
        }

        public void extMLDisownIntegerArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth)
        {
            MLReleaseInteger32Array(link, dataAddress, dimsAddress, headsAddress, depth);
        }

        public int extMLGetFloatArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth)
        {
            return MLGetReal32Array(link, out dataAddress, out dimsAddress, out headsAddress, out depth);
        }

        public void extMLDisownFloatArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth)
        {
            MLReleaseReal32Array(link, dataAddress, dimsAddress, headsAddress, depth);
        }

        public int extMLGetDoubleArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth)
        {
            return MLGetReal64Array(link, out dataAddress, out dimsAddress, out headsAddress, out depth);
        }

        public void extMLDisownDoubleArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth)
        {
            MLReleaseReal64Array(link, dataAddress, dimsAddress, headsAddress, depth);
        }

        public unsafe int extMLPutByteArray(IntPtr link, byte* data, int[] dims, string[] heads, int depth)
        {
            return MLPutByteArray(link, data, dims, heads, depth);
        }

        public unsafe int extMLPutShortIntegerArray(IntPtr link, short* data, int[] dims, string[] heads, int depth)
        {
            return MLPutInteger16Array(link, data, dims, heads, depth);
        }

        public unsafe int extMLPutIntegerArray(IntPtr link, int* data, int[] dims, string[] heads, int depth)
        {
            return MLPutInteger32Array(link, data, dims, heads, depth);
        }

        public unsafe int extMLPutFloatArray(IntPtr link, float* data, int[] dims, string[] heads, int depth)
        {
            return MLPutReal32Array(link, data, dims, heads, depth);
        }

        public unsafe int extMLPutDoubleArray(IntPtr link, double* data, int[] dims, string[] heads, int depth)
        {
            return MLPutReal64Array(link, data, dims, heads, depth);
        }

        public IntPtr extMLCreateMark(IntPtr link)
        {
            return MLCreateMark(link);
        }

        public void extMLSeekMark(IntPtr link, IntPtr mark, int mustBeZero)
        {
            MLSeekMark(link, mark, mustBeZero);
        }

        public void extMLDestroyMark(IntPtr link, IntPtr mark)
        {
            MLDestroyMark(link, mark);
        }

        public int extMLTransferExpression(IntPtr dest, IntPtr source)
        {
            return MLTransferExpression(dest, source);
        }

        public int extMLTransferToEndOfLoopbackLink(IntPtr dest, IntPtr source)
        {
            return MLTransferToEndOfLoopbackLink(dest, source);
        }

        public void extMLPutMessage(IntPtr link, int msg)
        {
            MLPutMessage(link, msg);
        }

        public IntPtr extMLCreateYieldFunction(IntPtr env, NativeLink.YielderCallback yf, IntPtr zero)
        {
            return MLCreateYieldFunction(env, yf, zero);
        }

        public int extMLSetYieldFunction(IntPtr link, IntPtr yfObject)
        {
            return MLSetYieldFunction(link, yfObject);
        }

        public IntPtr extMLYieldFunction(IntPtr link)
        {
            return MLYieldFunction(link);
        }

        public void extMLDestroyYieldFunction(IntPtr yfObject)
        {
            MLDestroyYieldFunction(yfObject);
        }

        public IntPtr extMLCreateMessageHandler(IntPtr env, NativeLink.MessageCallback mf, IntPtr zero)
        {
            return MLCreateMessageHandler(env, mf, zero);
        }

        public void extMLSetMessageHandler(IntPtr link, IntPtr mhObject)
        {
            MLSetMessageHandler(link, mhObject);
        }

        public IntPtr extMLMessageHandler(IntPtr link)
        {
            return MLMessageHandler(link);
        }

        public void extMLDestroyMessageHandler(IntPtr mhObject)
        {
            MLDestroyMessageHandler(mhObject);
        }

        public int extMLDeviceInformation(IntPtr link, uint selector, IntPtr buf, ref int buflen)
        {
            return MLDeviceInformation(link, selector, buf, ref buflen);
        }



        /*********************************  MathLink Library Declarations  ******************************/

        [DllImport("mathlink")]
        protected internal static extern IntPtr MLBegin(IntPtr zero);
        [DllImport("mathlink")]
        protected internal static extern void MLEnd(IntPtr env);
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLOpenString(IntPtr env, [MarshalAs(UnmanagedType.LPStr)] string commandLine, out int err);
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLOpenInEnv(IntPtr env, int argc, [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] argv, out int err);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        private static extern IntPtr MLLoopbackOpen(IntPtr env, out int err);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLClose(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLConnect(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLName(IntPtr link);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLNewPacket(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLNextPacket(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLEndPacket(IntPtr link);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLError(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLClearError(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLErrorMessage(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLErrorString(IntPtr env, int err);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLSetError(IntPtr link, int err);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLReady(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLFlush(IntPtr link);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetNext(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetType(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetNextRaw(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutNext(IntPtr link, int type);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        [System.Security.SuppressUnmanagedCodeSecurity()]
        protected internal static extern int MLGetArgCount(IntPtr link, out int argCount);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutArgCount(IntPtr link, int argCount);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutData(IntPtr link, [In] byte[] buf, int len);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutSize(IntPtr link, int len);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetData(IntPtr link, IntPtr data, int len, out int num);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLBytesToGet(IntPtr link, out int num);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLBytesToPut(IntPtr link, out int num);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutFunction(IntPtr link, string s, int argCount);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetUCS2String(IntPtr link, out IntPtr strAddress, out int len);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLReleaseUCS2String(IntPtr link, IntPtr strAddress, int len);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutUCS2String(IntPtr link, [MarshalAs(UnmanagedType.LPWStr)] string s, int len);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetByteString(IntPtr link, out IntPtr strAddress, out int len, int missing);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLReleaseByteString(IntPtr link, IntPtr strAddress, int len);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetUCS2Symbol(IntPtr link, out IntPtr strAddress, out int len);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLReleaseUCS2Symbol(IntPtr link, IntPtr strAddress, int len);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutUCS2Symbol(IntPtr link, [MarshalAs(UnmanagedType.LPWStr)] string s, int len);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetInteger(IntPtr link, out int i);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutInteger(IntPtr link, int i);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetDouble(IntPtr link, out double d);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLPutDouble(IntPtr link, double d);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetByteArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLReleaseByteArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetInteger16Array(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLReleaseInteger16Array(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetInteger32Array(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLReleaseInteger32Array(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetReal32Array(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLReleaseReal32Array(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLGetReal64Array(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLReleaseReal64Array(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal unsafe static extern int MLPutByteArray(IntPtr link, byte* data, [In] int[] dims, [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] heads, int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal unsafe static extern int MLPutInteger16Array(IntPtr link, short* data, [In] int[] dims, [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] heads, int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal unsafe static extern int MLPutInteger32Array(IntPtr link, int* data, [In] int[] dims, [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] heads, int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal unsafe static extern int MLPutReal32Array(IntPtr link, float* data, [In] int[] dims, [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] heads, int depth);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal unsafe static extern int MLPutReal64Array(IntPtr link, double* data, [In] int[] dims, [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] heads, int depth);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLCreateMark(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLSeekMark(IntPtr link, IntPtr mark, int mustBeZero);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLDestroyMark(IntPtr link, IntPtr mark);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLTransferExpression(IntPtr dest, IntPtr source);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLTransferToEndOfLoopbackLink(IntPtr dest, IntPtr source);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLPutMessage(IntPtr link, int msg);

        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLCreateYieldFunction(IntPtr env, NativeLink.YielderCallback yf, IntPtr zero);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLSetYieldFunction(IntPtr link, IntPtr yfObject);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLYieldFunction(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLDestroyYieldFunction(IntPtr yfObject);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLCreateMessageHandler(IntPtr env, NativeLink.MessageCallback mf, IntPtr zero);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLSetMessageHandler(IntPtr link, IntPtr mhObject);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern IntPtr MLMessageHandler(IntPtr link);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern void MLDestroyMessageHandler(IntPtr mhObject);
        [System.Security.SuppressUnmanagedCodeSecurity()]
        [DllImport("mathlink")]
        protected internal static extern int MLDeviceInformation(IntPtr link, uint selector, IntPtr buf, ref int buflen);

    }

}
