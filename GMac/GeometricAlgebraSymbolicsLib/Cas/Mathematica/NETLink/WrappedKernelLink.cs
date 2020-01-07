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
/// Implements the IKernelLink interface by "wrapping" an IMathLink implementation.
/// </summary>
/// <remarks>
/// This class is public only so that WRI can subclass it from another assembly in the future (or perhaps so we
/// can assist certain external developers in doing this). It is not part of the .NET/Link API.
/// <para>
/// This is the class that IKernelLinks are implemented with throughout .NET/Link.
/// </para>
/// See the comments on the various methods as declared in the IKernelLink and IMathLink interfaces for more information.
/// </remarks>
/// 
public class WrappedKernelLink : KernelLinkImpl {


    protected IMathLink impl;

    private bool linkConnected = false;

    
    /***************************************  Constructor  **************************************/

    public WrappedKernelLink() : this(null) {}
    
    public WrappedKernelLink(IMathLink ml) {
        SetMathLink(ml);
        MessageArrived += new MessageHandler(interruptDetector);
    }


    /*************************  Setting and retrieving the wrapped MathLink  ********************/
    
    public IMathLink GetMathLink() {
        return impl;
    }

    public void SetMathLink(IMathLink ml) {
        impl = ml;
    }


    /******************  Implementation of IMathLink Interface by wrapping  *********************/

    public override void Close() {
        impl.Close();
    }
    public override void Connect() {
        impl.Connect();
    }
    public override void NewPacket() {
        impl.NewPacket();
    }
    public override void EndPacket() {
        impl.EndPacket();
    }
    public override bool ClearError() {
        return impl.ClearError();
    }
    public override string GetFunction(out int argCount) {
        return impl.GetFunction(out argCount);
    }
    public override void Flush() {
        impl.Flush();
    }
    public override void PutNext(ExpressionType type) {
        impl.PutNext(type);
    }
    public override int GetArgCount() {
        return impl.GetArgCount();
    }
    public override void PutArgCount(int argCount) {
        impl.PutArgCount(argCount);
    }
    public override void PutSize(int size) {
        impl.PutSize(size);
    }
    public override void PutData(byte[] data) {
        impl.PutData(data);
    }
    public override byte[] GetData(int numRequested) {
        return impl.GetData(numRequested);
    }
    public override int BytesToPut() {
        return impl.BytesToPut();
    }
     public override int BytesToGet() {
        return impl.BytesToGet();
    }
    public override string GetString() {
        return impl.GetString();
    }
    public override string GetSymbol() {
        return impl.GetSymbol();
    }
    public override byte[] GetByteString(int missing) {
        return impl.GetByteString(missing);
    }
    public override void PutSymbol(string s) {
        impl.PutSymbol(s);
    }
    public override int GetInteger() {
        return impl.GetInteger();
    }
    public override void Put(int i) {
        impl.Put(i);
    }
    public override double GetDouble() {
        return impl.GetDouble();
    }
    public override void Put(double d) {
        impl.Put(d);
    }
    public override void TransferExpression(IMathLink source) {
        impl.TransferExpression(source);
    }
    public override void TransferToEndOfLoopbackLink(ILoopbackLink source) {
        impl.TransferToEndOfLoopbackLink(source);
    }
    public override void PutMessage(MathLinkMessage msg) {
        impl.PutMessage(msg);
    }
    public override ILinkMark CreateMark() {
        return impl.CreateMark();
    }
    public override void SeekMark(ILinkMark mark) {
        impl.SeekMark(mark);
    }
    public override void DestroyMark(ILinkMark mark) {
        impl.DestroyMark(mark);
    }

    public override object GetComplex() {
        return impl.GetComplex();
    }

    public override void DeviceInformation(int selector, IntPtr buf, ref int len) {
        impl.DeviceInformation(selector, buf, ref len);
    }


    /***************  Properties  ****************/

    public override int Error {
        get { return impl.Error; }
    }
    
    public override string ErrorMessage {
        get { return impl.ErrorMessage; }
    }
    
    public override bool Ready {
        get { return impl.Ready; }
    }

    public override string Name {
        get { return impl.Name; }
    }

    // This property must be overriden, as it is crucial to use the impl's notion of the Complex class, not ours.
    public override Type ComplexType {
        get { return impl.ComplexType; }
        set { impl.ComplexType = value; }
    }


    /*****************  Events  *****************/

    // Here we are using the "event property" pattern. We use it because we need to forward add/remove calls
    // to impl.

    public override event YieldFunction Yield {
        add { impl.Yield += value; }
        remove { impl.Yield -= value; }
    }

    public override event MessageHandler MessageArrived {
        add { impl.MessageArrived += value; }
        remove { impl.MessageArrived -= value; }
    }


    /*************  Methods requiring more than a simple call to impl  **************/
    
    public override ExpressionType GetNextExpressionType() {

         ExpressionType result = impl.GetNextExpressionType();
         if (result == ExpressionType.Symbol && isObject())
             result = ExpressionType.Object;
         return result;
    }
    
    public override ExpressionType GetExpressionType() {

         ExpressionType result = impl.GetExpressionType();
         if (result == ExpressionType.Symbol && isObject())
             result = ExpressionType.Object;
         return result;
    }
    

    public override PacketType NextPacket() {

        // Code here is not just a simple call to impl.nextPacket(). For a KernelLink, nextPacket() returns a
        // wider set of packet constants than the MathLink C API itself. We want nextPacket() to work on the
        // non-packet types of heads the kernel and FE send back and forth.
        // Because for some branches below we seek back to the start before returning, it is not guaranteed
        // that when nextPacket() returns, the current packet has been "opened". Thus it is not safe to write
        // a J/Link program that loops calling nextPacket()/newPacket(). You need to call handlePacket() after
        // nextPacket() to ensure that newPacket() will throw away the curent "packet".

        // createMark will fail on an unconnected link, and the user might call this
        // before any reading that would connect the link.
        if (!linkConnected) {
            Connect();
            linkConnected = true;
        }

        PacketType pkt;
        ILinkMark mark = CreateMark();
        try {
            pkt = impl.NextPacket();
        } catch (MathLinkException e) {
            if (e.ErrCode == 23 /* MLEUNKNOWNPACKET */) {
                ClearError();
                SeekMark(mark);
                string f = GetFunction(out var argCount);
                if (f == "ExpressionPacket")
                    pkt = PacketType.Expression;
                else if (f == "BoxData") {
                    // Probably more than just BoxData will need to join this group. I thought that kernel
                    // always sent cell contents (e.g. Print output) wrapped in ExpressionPacket, but not so.
                    // "Un-wrapped" BoxData is also possible, perhaps others. We need to treat this case
                    // specially, since there is no packet wrapper.
                    SeekMark(mark);
                    pkt = PacketType.Expression;
                } else {
                    // Note that all other non-recognized functions get labelled as PacketType.FrontEnd. I could perhaps be
                    // more discriminating, but then I risk not recognizing a legitimate communication
                    // intended for the front end. This means that PacketType.FrontEnd is not a specific indication that
                    // a front-end-related packet is on the link. Because there is no diagnostic packet head,
                    // we need to seek back to before the function was read. That way, when programs call
                    // methods to read the "contents" of the packet, they will in fact get the whole thing.
                    SeekMark(mark);
                    pkt = PacketType.FrontEnd;
                }
            } else {
                // The other MathLink error from MLNextPacket is MLENEXTPACKET, which is a common one saying
                // that you have called MLNextPacket when more data is in the current packet.
                throw e;
            }
        } finally {
            DestroyMark(mark);
        }
        return pkt;
    }
    

    public override Array GetArray(Type leafType, int depth, out string[] heads) {
        
        // The only type that must be forwarded to impl is the complex class, as the impl stores this class.
        // The only types that must _not_ be forwarded are any object types (impl is an IMathLink, and therefore cannot handle them).
        // Every type other than Complex could be handled by the base implementation here, but it is likely that the wrapped link
        // is a NativeLink, which can handle many primitive types (those that are native in the MathLink C API) in a 
        // very efficient way, so we want to forward these.
        if (leafType == ComplexType || Utils.IsTrulyPrimitive(leafType))
            return impl.GetArray(leafType, depth, out heads);
        else
            return base.GetArray(leafType, depth, out heads);
    }


    public override object GetObject() {
        // Implementation is trivial because GetObject() only appears in IKernelLink for documentation purposes.
        // There is no KernelLink-specific implementation for GetObject(). Recall that object references are read
        // with a getObj() virtual method that is overridden in IKernelLink implementations. In other words, calls to
        // get object refs "bubble up" to the IKernelLink level from wiring that is in place down inside the IMathLink
        // implementation of GetObject().
        return base.GetObject();
    }


    public override void Put(object obj) {
        // Implementation is trivial because Put(object) only appears in IKernelLink for documentation purposes.
        // There is no KernelLink-specific implementation. Recall that object references are sent
        // with a putRef() virtual method that is overridden in IKernelLink implementations. In other words, calls to
        // put object refs "bubble up" to the IKernelLink level from wiring that is in place down inside the IMathLink
        // implementation of Put(object).
        base.Put(obj);
    }


    /***********************************  Protected/Private  ************************************/

    protected override void putString(string s) {
        impl.Put(s);
    }
    
    protected override void putArray(Array obj, string[] heads) {

        // Must not forward to impl arrays that hold any object types (and the only thing that impl
        // can do efficiently anyway is arrays of primitives).
        Type elementType = obj.GetType().GetElementType();
        if (Utils.IsTrulyPrimitive(elementType))
            impl.Put(obj, heads);
        else
            putArrayPiecemeal(obj, heads, 0);
    }

    protected override void putComplex(object obj) {
        impl.Put(obj);
    }


    // This MessageHandler method supports the ability for code called from M to call ml.WasInterrupted.
    private void interruptDetector(MathLinkMessage msg) {
        WasInterrupted = (msg == MathLinkMessage.Abort || msg == MathLinkMessage.Interrupt);
    }

}

}
