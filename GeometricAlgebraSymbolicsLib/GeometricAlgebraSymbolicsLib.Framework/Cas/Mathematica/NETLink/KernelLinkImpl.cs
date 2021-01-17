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
using System.Diagnostics;
using System.Drawing;
using Wolfram.NETLink.Internal;


namespace Wolfram.NETLink {

/// <exclude/>
/// <summary>
/// An abstract class that provides much of the plumbing needed to implement the IKernelLink interface.
/// </summary>
/// <remarks>
/// This class is public only so that WRI can subclass it from another assembly in the future (or perhaps so we
/// can assist certain external developers in doing this). It is not part of the .NET/Link API.
/// <para>
/// See the comments on the various methods as declared in the IKernelLink and IMathLink interfaces for more information.
/// </para>
/// </remarks>
/// 
public abstract class KernelLinkImpl : MathLinkImpl, IKernelLink {


    internal const string PACKAGE_INTERNAL_CONTEXT = NETLinkConstants.PACKAGE_CONTEXT + "Package`";

    private bool isPackageLoaded = false;

    private string graphicsFormat = "Automatic";
    private bool useFrontEnd = true;
    private bool useStdForm = true;

    private Exception lastError;
    // This supports the GetNETException[] function in Mathematica.
    private Exception lastExceptionDuringCallPacketHandling;
    private bool lastPktWasMsg = false; // Used only inside HandlePacket()
    private bool isManual = false;

    private ObjectHandler objectHandler;
    private CallPacketHandler callPktHandler;

    private System.Text.StringBuilder accumulatingPS;

    private int lastMessage = 0;
    private object msgLock = new object();

    private static YieldFunction bailoutYieldFunction = new YieldFunction(KernelLinkImpl.bailoutYielder);


    /*************************************  Constructor  ***************************************/

    public KernelLinkImpl() {
        objectHandler = new ObjectHandler();
        callPktHandler = new CallPacketHandler(objectHandler);
    }


    /*************************************  Properties  ***************************************/

    public virtual bool UseFrontEnd {
        get { return useFrontEnd; }
        set { useFrontEnd = value; }
    }


    // GIF, JPEG, Metafile, Automatic
    public virtual string GraphicsFormat {
        get { return graphicsFormat; }
        set {
            switch (value) {
                case "GIF":
                case "gif":
                    graphicsFormat = "GIF";
                    break;
                case "JPEG":
                case "jpeg":
                    graphicsFormat = "JPEG";
                    break;
                case "Metafile":
                case "metafile":
                case "METAFILE":
                    graphicsFormat = "Metafile";
                    break;
                default:
                    graphicsFormat = "Automatic";
                    break;
            }
        }
    }

    public virtual bool TypesetStandardForm {
        get { return useStdForm; }
        set { useStdForm = value; }
    }


    /***************************************  Methods  *****************************************/

    public virtual void Evaluate(string s) {

        PutFunction("EvaluatePacket", 1);
        PutFunction("ToExpression", 1);
        Put(s);
        EndPacket();
        Flush();
    }


    public virtual void Evaluate(Expr e) {

        PutFunction("EvaluatePacket", 1);
        Put(e);
        EndPacket();
        Flush();
    }


    public virtual string EvaluateToOutputForm(string s, int pageWidth) {
        return evalToString(s, pageWidth, "OutputForm");
    }

    public virtual string EvaluateToOutputForm(Expr e, int pageWidth) {
        return evalToString(e, pageWidth, "OutputForm");
    }

    public virtual string EvaluateToInputForm(string s, int pageWidth) {
        return evalToString(s, pageWidth, "InputForm");
    }

    public virtual string EvaluateToInputForm(Expr e, int pageWidth) {
        return evalToString(e, pageWidth, "InputForm");
    }

    public virtual Image EvaluateToTypeset(string s, int pageWidth) {
        return evalToImage(s, pageWidth, -1 /* ignored */, true);
    }
    
    public virtual Image EvaluateToTypeset(Expr e, int pageWidth) {
        return evalToImage(e, pageWidth, -1 /* ignored */, true);
    }
    
    public virtual Image EvaluateToImage(string s, int width, int height) {
        return evalToImage(s, width, height, false);
    }
    
    public virtual Image EvaluateToImage(Expr e, int width, int height) {
        return evalToImage(e, width, height, false);
    }
    

    // Will return one of the 4 "answer" packets (return, returntext, returnexpr, inputname). The link
    // will be in the state just after nextPacket().
    public virtual PacketType WaitForAnswer() {

        PacketType pkt;

        // This is set to null on DisplayEndPacket, but do it here as a safety net in case the
        // packet loop doesn't end nornally.
        accumulatingPS = null;
        
        while (true) {
            pkt = NextPacket();
            bool allowDefaultProcessing = OnPacketArrived(pkt);
            if (allowDefaultProcessing)
                HandlePacket(pkt);
            if (pkt == PacketType.Return || pkt == PacketType.InputName || pkt == PacketType.ReturnText || pkt == PacketType.ReturnExpression)
                // These are the only ones that cause this function to exit (that is, they qualify as "answers").
                break;
            else
                NewPacket();
        }
        return pkt;
    }


    public virtual void WaitAndDiscardAnswer() {

        PacketType pkt = WaitForAnswer();
        NewPacket();
        // These are the only two packet types that constitute the absolute end of an eval.
        while (pkt != PacketType.Return && pkt != PacketType.InputName) {
            // This loop will only happen once, of course, but might as well be defensive.
            pkt = WaitForAnswer();
            NewPacket();
        }
    }


    public virtual Exception LastError {
        get { return Error != MathLinkException.MLEOK ? new MathLinkException(Error, ErrorMessage) : lastError; }
    }


    public virtual void HandlePacket(PacketType pkt) {

        switch (pkt) {
            // If you ever change the default behavior on the 4 "answer" packets to read off the link,
            // you'll need to add a seekMark in NativeKernelLink.waitForAnswer...
            case PacketType.Return:
            case PacketType.InputName:
            case PacketType.ReturnText:
            case PacketType.ReturnExpression:
            case PacketType.Menu:
            case PacketType.Message:
                break;

            // From here on, the cases do actual work.
            case PacketType.Call: {
                ExpressionType type = GetExpressionType();
                if (type == ExpressionType.Integer) {
                    // A normal CallPacket representing a call to .NET via nCall.
                    callPktHandler.handleCallPacket(this);
                } else {
                    // A CallPacket destined for the FE via MathLink`CallFrontEnd[] and routed through
                    // .NET due to ShareFrontEnd[]. This would only be in a 5.1 FE, as earlier
                    // versions do not use CallPacket and later versions would use the FE's Service Link.
                    IMathLink feServerLink = callPktHandler.FEServerLink;
                    if (feServerLink != null) {
                        feServerLink.PutFunction("CallPacket", 1);
                        feServerLink.TransferExpression(this);
                        // FE will always reply to a CallPacket. Note that it is technically possible for
                        // the FE to send back an EvaluatePacket, which means that we really need to run a
                        // little loop here, not just write the result back to the kernel. But this branch
                        // is only for a 5.1 FE, and I don't think that the 5.1 FE ever does that.
                        TransferExpression(feServerLink);
                    }
                }
                break;
            }
            case PacketType.Display:
            case PacketType.DisplayEnd: {
                IMathLink feServerLink = callPktHandler.FEServerLink;
                if (feServerLink != null) {
                    if (accumulatingPS == null)
                        accumulatingPS = new System.Text.StringBuilder(34000);  // 34K is large enough to hold an entire packet
                    accumulatingPS.Append(GetString());
                    if (pkt == PacketType.DisplayEnd) {
                        // XXXPacket[stuff] ---> Cell[GraphicsData["PostScript", stuff], "Graphics"]
                        feServerLink.PutFunction("FrontEnd`FrontEndExecute", 1);
                        feServerLink.PutFunction("FrontEnd`NotebookWrite", 2);
                        feServerLink.PutFunction("FrontEnd`SelectedNotebook", 0);
                        feServerLink.PutFunction("Cell", 2);
                        feServerLink.PutFunction("GraphicsData", 2);
                        feServerLink.Put("PostScript");
                        feServerLink.Put(accumulatingPS.ToString());
                        feServerLink.Put("Graphics");
                        feServerLink.Flush();
                        accumulatingPS = null;
                    }
                } else {
                    Debug.WriteLine("Got PacketType.Display in handlePacket, but no FE link");
                }
                break;
            }
            case PacketType.Input:
            case PacketType.InputString: {
                IMathLink feServerLink = callPktHandler.FEServerLink;
                if (feServerLink != null) {
                    feServerLink.PutFunction(pkt == PacketType.InputString ? "InputStringPacket" : "InputPacket", 1);
                    feServerLink.Put(GetString());
                    feServerLink.Flush();
                    NewPacket();
                    Put(feServerLink.GetString());
                    Flush();
                }
                break;
            }
            case PacketType.Text:
            case PacketType.Expression: {
                // Print output, or message text.
                IMathLink feServerLink = callPktHandler.FEServerLink;
                if (feServerLink != null) {
                    // XXXPacket[stuff] ---> Cell[stuff, "Print"]
                    feServerLink.PutFunction("FrontEnd`FrontEndExecute", 1);
                    feServerLink.PutFunction("FrontEnd`NotebookWrite", 2);
                    feServerLink.PutFunction("FrontEnd`SelectedNotebook", 0);
                    feServerLink.PutFunction("Cell", 2);
                    feServerLink.TransferExpression(this);
                    feServerLink.Put((lastPktWasMsg) ? "Message" : "Print");
                    feServerLink.Flush();
                } else {
                    // For one type of PacketType.Expression, no part of it has been read yet. Thus we must "open" the
                    // packet so that later calls to newPacket() throw it away.
                    if (pkt == PacketType.Expression) {
                        GetFunction(out var ignore);
                    }
                }
                break;
            }
            case PacketType.FrontEnd: {
                // This case is different from the others. At the point of entry, the link is at the point
                // _before_ the "packet" has been read. As a result, we must at least open the packet.
                // Note that PacketType.FrontEnd is really just a fall-through for unrecognized packets. We don't have any
                // checks that it is truly intended for the FE.
                IMathLink feServerLink = callPktHandler.FEServerLink;
                if (feServerLink != null) {
                    ILinkMark mark = CreateMark();
                    try {
                        // Wrap FrontEndExecute around it if not already there.
                        string wrapper = GetFunction(out var ignore);
                        if (wrapper != "FrontEnd`FrontEndExecute") {
                            feServerLink.PutFunction("FrontEnd`FrontEndExecute", 1);
                        }
                    } finally {
                        SeekMark(mark);
                        DestroyMark(mark);
                    }
                    feServerLink.TransferExpression(this);
                    feServerLink.Flush();
                    // Wait until either the fe is ready (because what we just sent causes a return value)
                    // or kernel is ready (the computation is continuing because the kernel is not waiting
                    // for a return value).
                    do {
                        System.Threading.Thread.Sleep(50);
                    } while (!feServerLink.Ready && !Ready);
                    if (feServerLink.Ready) {
                        // fe link has something to return to kernel from last PacketType.FrontEnd we sent it.
                        TransferExpression(feServerLink);
                        Flush();
                    }
                } else {
                    // It's OK to get here. For example, this happens if you don't share the fe, but have a
                    // button that calls NotebookCreate[]. This isn't a very good example, because that
                    // function expects the fe to return something, so Java will hang. you will get into
                    // trouble if you make calls on the fe that expect a return. Everything is OK for calls
                    // that don't expect a return, though.
                    GetFunction(out var ignore); // Must at least open the packet, so newPacket (back in caller) will get rid of it.
                }
                break;
            }
            default:
                break;
        }
        lastPktWasMsg = pkt == PacketType.Message;
    }


    public virtual void PutReference(object obj) {
        PutReference(obj, null);
    }


    public virtual void PutReference(object obj, Type upCastCls) {

        if (obj == null) {
            PutSymbol("Null");
        } else {
            objectHandler.putReference(this, obj, upCastCls);
        }
    }


    public virtual void EnableObjectReferences() {
        
        Evaluate("Needs[\"" + NETLinkConstants.PACKAGE_CONTEXT + "\"]");
        WaitAndDiscardAnswer();
        Evaluate("InstallNET[$ParentLink]");
        Flush();
        Install.install(this);
        WaitAndDiscardAnswer();
    }
        
    
    public virtual void InterruptEvaluation() {
        try { PutMessage(MathLinkMessage.Interrupt); } catch (MathLinkException) {}
    }
    
    public virtual void AbortEvaluation() {
        try { PutMessage(MathLinkMessage.Abort); } catch (MathLinkException) {}
    }
    
    public virtual void AbandonEvaluation() {
        Yield += bailoutYieldFunction;
    }
    
    public virtual void TerminateKernel() {
        try { PutMessage(MathLinkMessage.Terminate); } catch (MathLinkException) {}
    }
    
    public static bool bailoutYielder() {
        return true;
    }


    /****************************************  Event  *****************************************/

    public virtual event PacketHandler PacketArrived;

    public virtual bool OnPacketArrived(PacketType pkt) {

        if (PacketArrived == null)
            return true;

        bool continueProcessing = true;
        Delegate[] pktHandlers = PacketArrived.GetInvocationList();
        if (pktHandlers.Length > 0) {
            object[] args = new object[]{this, pkt};
            ILinkMark mark = CreateMark();
            try {
                foreach (Delegate d in pktHandlers) {
                    try {
                        continueProcessing = (bool) d.DynamicInvoke(args);
                    } catch (Exception) {
                        // Don't want an exception thrown by a PacketHandler method to
                        // prevent all handlers from being notified. 
                    }
                    ClearError();
                    SeekMark(mark);
                    if (!continueProcessing)
                        break;
                }
            } finally {
                DestroyMark(mark);
            }
        }
        return continueProcessing;
    }


    /**********************************  These are for "StdLink"'s only.  **********************************/
    
    public virtual void Print(string s) {

        try {
            PutFunction("EvaluatePacket", 1);
            PutFunction("Print", 1);
            Put(s);
            EndPacket();
            WaitAndDiscardAnswer();
        } catch (MathLinkException) {
            // Not guaranteed to be a complete or useful cleanup.
            ClearError();
            NewPacket();
        }
    }


    public virtual void Message(string symtag, params string[] args) {

        try {
            PutFunction("EvaluatePacket", 1);
            PutFunction("Apply", 2);
            PutFunction("ToExpression", 1);
            Put("Function[Null, Message[#1, ##2], HoldFirst]");
            PutFunction("Join", 2);
            PutFunction("ToHeldExpression", 1);
            Put(symtag);
            PutFunction("Hold", args.Length);
            foreach (string s in args)
                Put(s);
            EndPacket();
            WaitAndDiscardAnswer();
        } catch (MathLinkException) {
            // Not guaranteed to be a complete or useful cleanup.
            ClearError();
            NewPacket();
        }
    }


    public virtual void BeginManual() {
        IsManual = true;
    }
    
    public virtual bool WasInterrupted {
        get { 
            lock (msgLock) {
                return (MathLinkMessage) lastMessage == MathLinkMessage.Interrupt;
            }
        }
        set {
            lock (msgLock) {
                lastMessage = value ? (int) MathLinkMessage.Interrupt : 0;
            }
        }
    }


    /******************************************  Implementation  ****************************************/

    // These next two properties are part fo the CallPacket-handling logic, but they cannot be put into
    // the CallPacketHandler class because an instance of that class must be sharable between two links.

    internal Exception LastExceptionDuringCallPacketHandling {
        get { return lastExceptionDuringCallPacketHandling; }
        set {lastExceptionDuringCallPacketHandling = value; }
    }

    internal bool IsManual {
        get { return isManual; }
        set {
            if (value && !isManual) {
                try {
                    PutFunction(Install.MMA_PREPAREFORMANUALRETURN, 1);
                    PutSymbol("$CurrentLink");
                    Flush(); // Because we won't be reading.
                } catch (MathLinkException) {
                    ClearError(); // What to do????
                }
            }
            isManual = value;
        }
    }

    
    // Share the state from another link into this link (because it is pointing at the same kernel).
    // This is public because it turns out to be useful in some specialized applications, like UnityLink
    // and GrasshopperLink, where we need to create two links into the same kernel from the program,
    // and both need to have object refs flow across them.
    public void copyStateFrom(KernelLinkImpl other) {
        objectHandler = other.objectHandler;
        callPktHandler = other.callPktHandler;
    }


    protected override void putRef(object obj) {
        PutReference(obj);
    }

    protected override object getObj() {

        object obj = null;
        try {
            string sym = GetSymbol();
            if (sym == "Null")
                return null;
            // Verify that the symbol even looks like an object ref.
            if (sym.StartsWith(Install.MMA_OBJECTSYMBOLPREFIX))
                obj = objectHandler.lookupObject(sym);
        } catch (Exception) {
            // Catch exceptions thrown by GetSymbol() (wasn't a symbol at all) or ObjectHandler.lookupObject().
            // Will have obj == null and thus will throw MLE_BAD_OBJECT below.
        }
        if (obj == null)
            throw new MathLinkException(MathLinkException.MLE_BAD_OBJECT);
        return obj;
    }


    // Tests whether symbol waiting on link is a valid object reference or the symbol Null.
    // Called by GetNextExpressionType() and GetExpressionType(), after it has already been verified that the type is MLTKSYM. 
    protected bool isObject() {
        
         ILinkMark mark = CreateMark();
         try {
             getObj();
            return true;
         } catch (MathLinkException) {
             ClearError();
             return false;
         } finally {
             SeekMark(mark);
             DestroyMark(mark);
         }
    }
    
    
    // obj will be a String or Expr
    private string evalToString(object obj, int pageWidth, string format) {

        string res = null;
        
        lastError = null;
        try {
            Utils.WriteEvalToStringExpression(this, obj, pageWidth, format);
            WaitForAnswer();
            res = GetStringCRLF();
        } catch (MathLinkException e) {
            ClearError();
            lastError = e;
        } finally {
            NewPacket();
        }
        return res;
    }


    // obj will be a String or Expr
    private Image evalToImage(object obj, int width, int height, bool isTypeset) {

        lastError = null;
        try {
            loadNETLinkPackage();
            if (isTypeset)
                // Don't allow Metafile as format, as that produces very poor typeset results.
                Utils.WriteEvalToTypesetExpression(this, obj, width, GraphicsFormat == "Metafile" ? "Automatic" : GraphicsFormat, TypesetStandardForm);
            else
                // Note that I have dropped the dpi arg to the public methods. It's still supported in the M code for
                // EvaluateToImage, so rather than rip out the handling of that arg in Utils.WriteEvalToImageExpression,
                // I'll just hard-code a 0 here (0 --> Automatic).
                Utils.WriteEvalToImageExpression(this, obj, width, height, GraphicsFormat, 0, UseFrontEnd);
            WaitForAnswer();
        } catch (MathLinkException e) {
            ClearError();
            lastError = e;
            NewPacket();  // Just a guess. Hope that we are on the last packet.
            return null;
        } catch (ApplicationException ee) {
            // Thrown by loadNETLinkPackage when J/Link version not correct.
            lastError = ee;
            return null;
        }
        
        // From here on, the link will be OK no matter what happens if we ensure that we call newPacket.
        Image im = null;
        try {
            if (GetNextExpressionType() == ExpressionType.String) {
                byte[] imageData = GetByteString((byte) 0);
                //  GIF, JPEG, Metafile can be done automatically.
                im = Image.FromStream(new System.IO.MemoryStream(imageData));
            }
        } catch (Exception e) {
            // I don't want this method to throw an outofmem exception if the byte array allocation fails.
            // That might need multi-megs. Just quietly return null if it fails.
            // TODO: Make this return an image that displays the text "out of memory".
            ClearError();
            lastError = e;
        } finally {
            NewPacket();
        }
        return im;
    }

    
    // Throws an exception if J/Link is not current enough.
    private void loadNETLinkPackage() {

        if (!isPackageLoaded) {
            PutFunction("EvaluatePacket", 1);
            PutFunction("Needs", 1);
            Put(NETLinkConstants.PACKAGE_CONTEXT);
            // Here we turn off PacketHandlers for this "hidden" evaluation, so as not to confuse
            // programmers.
            PacketHandler oldPacketArrived = PacketArrived;
            PacketArrived = null;
            try {
                WaitAndDiscardAnswer();
                // Check that we have J/Link 2.1 or later.
                string jVers = EvaluateToInputForm("JLink`Information`$VersionNumber", 0);
                if (Double.Parse(jVers, System.Globalization.NumberFormatInfo.InvariantInfo) < 2.1)
                    throw new ApplicationException("The J/Link application in your Mathematica installation must be updated " +
                                                    "to at least version 2.1. See www.wolfram.com/solutions/mathlink/jlink.");
            } finally {
                PacketArrived = oldPacketArrived;
            }
            isPackageLoaded = true;
        }
    }

}

}
