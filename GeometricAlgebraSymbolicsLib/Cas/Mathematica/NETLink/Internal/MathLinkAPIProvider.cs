using System;

namespace Wolfram.NETLink.Internal {

/// <summary>
/// Summary description for IMathLinkAPIProvider.
/// </summary>
internal interface IMathLinkAPIProvider {
    
    IntPtr extMLBegin(IntPtr zero);
    void extMLEnd(IntPtr env);
    
    IntPtr extMLOpenString(IntPtr env, string commandLine, out int err);
    IntPtr extMLOpenInEnv(IntPtr env, int argc, string[] argv, out int err);
    IntPtr extMLLoopbackOpen(IntPtr env, out int err);

    void extMLClose(IntPtr link);
    int extMLConnect(IntPtr link);
    string extMLName(IntPtr link);
    int extMLNewPacket(IntPtr link);
    int extMLNextPacket(IntPtr link);
    int extMLEndPacket(IntPtr link);
    int extMLError(IntPtr link);
    int extMLClearError(IntPtr link);
    string extMLErrorMessage(IntPtr link);
    string extMLErrorString(IntPtr env, int err);
    int extMLSetError(IntPtr link, int err);
    int extMLReady(IntPtr link);
    int extMLFlush(IntPtr link);
    int extMLGetNext(IntPtr link);
    int extMLGetType(IntPtr link);
    int extMLGetNextRaw(IntPtr link);
    int extMLPutNext(IntPtr link, int type);
    int extMLGetArgCount(IntPtr link, out int argCount);
    int extMLPutArgCount(IntPtr link, int argCount);
    int extMLPutData(IntPtr link, byte[] buf, int len);
    int extMLPutSize(IntPtr link, int len);
    int extMLGetData(IntPtr link, IntPtr data, int len, out int num);
    int extMLBytesToGet(IntPtr link, out int num);
    int extMLBytesToPut(IntPtr link, out int num);
    int extMLPutFunction(IntPtr link, string s, int argCount);
    int extMLGetUnicodeString(IntPtr link, out IntPtr strAddress, out int len);
    void extMLDisownUnicodeString(IntPtr link, IntPtr strAddress, int len);
    int extMLPutUnicodeString(IntPtr link, string s, int len);
    int extMLGetByteString(IntPtr link, out IntPtr strAddress, out int len, int missing);
    void extMLDisownByteString(IntPtr link, IntPtr strAddress, int len);
    int extMLGetUnicodeSymbol(IntPtr link, out IntPtr strAddress, out int len);
    void extMLDisownUnicodeSymbol(IntPtr link, IntPtr strAddress, int len);
    int extMLPutUnicodeSymbol(IntPtr link, string s, int len);
    int extMLGetInteger(IntPtr link, out int i);
    int extMLPutInteger(IntPtr link, int i);
    int extMLGetDouble(IntPtr link, out double d);
    int extMLPutDouble(IntPtr link, double d);
    int extMLGetByteArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
    void extMLDisownByteArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);
    int extMLGetShortIntegerArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
    void extMLDisownShortIntegerArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);
    int extMLGetIntegerArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
    void extMLDisownIntegerArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);
    int extMLGetFloatArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
    void extMLDisownFloatArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);
    int extMLGetDoubleArray(IntPtr link, out IntPtr dataAddress, out IntPtr dimsAddress, out IntPtr headsAddress, out int depth);
    void extMLDisownDoubleArray(IntPtr link, IntPtr dataAddress, IntPtr dimsAddress, IntPtr headsAddress, int depth);
    unsafe int extMLPutByteArray(IntPtr link, byte* data, int[] dims, string[] heads, int depth);
    unsafe int extMLPutShortIntegerArray(IntPtr link, short* data, int[] dims, string[] heads, int depth);
    unsafe int extMLPutIntegerArray(IntPtr link, int* data, int[] dims, string[] heads, int depth);
    unsafe int extMLPutFloatArray(IntPtr link, float* data, int[] dims, string[] heads, int depth);
    unsafe int extMLPutDoubleArray(IntPtr link, double* data, int[] dims, string[] heads, int depth);
    IntPtr extMLCreateMark(IntPtr link);
    void extMLSeekMark(IntPtr link, IntPtr mark, int mustBeZero);
    void extMLDestroyMark(IntPtr link, IntPtr mark);
    int extMLTransferExpression(IntPtr dest, IntPtr source);
    int extMLTransferToEndOfLoopbackLink(IntPtr dest, IntPtr source);
    void extMLPutMessage(IntPtr link, int msg);
    IntPtr extMLCreateYieldFunction(IntPtr env, NativeLink.YielderCallback yf, IntPtr zero);
    int extMLSetYieldFunction(IntPtr link, IntPtr yfObject);
    IntPtr extMLYieldFunction(IntPtr link);
    void extMLDestroyYieldFunction(IntPtr yfObject);
    IntPtr extMLCreateMessageHandler(IntPtr env, NativeLink.MessageCallback mf, IntPtr zero);
    void extMLSetMessageHandler(IntPtr link, IntPtr mhObject);
    IntPtr extMLMessageHandler(IntPtr link);
    void extMLDestroyMessageHandler(IntPtr mhObject);
		
    int extMLDeviceInformation(IntPtr link, uint selector, IntPtr buf, ref int buflen);
}

}
