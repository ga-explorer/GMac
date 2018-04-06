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
using System.Drawing;


namespace Wolfram.NETLink {

/************ 

TODO:

Cache the ToArray'ed outputs for the fields stored as ArrayList, so we don't have to create a new array every time.
Or perhaps not--that would allow a client to effectively write into the object.

Probably add an asynchronous method for computing and getting results. Perhaps fire an event when finished?

*************/

/// <summary>
/// MathKernel is a non-visual component that provides a very high-level interface for interacting with <i>Mathematica</i>.
/// It is especially intended for use in visual programming environments, as it is highly configurable via properties.
/// </summary>
/// <remarks>
/// MathKernel provides a higher-level interface than <see cref="IKernelLink"/>. For many types of .NET programs that use
/// <i>Mathematica</i> for computations, the IKernelLink interface provides ideal functionality. For some types of programs,
/// however, programmers might find the MathKernel object even easier to use. This is especially true for programs that
/// want to capture not just the result of a computation, but also messages, Print output, or graphics generated as side
/// effects of the computation.
/// <para>
/// The typical use of a MathKernel is to set the <see cref="Input"/> property, call the <see cref="Compute"/> method,
/// and when it returns you can get the various outputs produced by the computation via the <see cref="Result"/>,
/// <see cref="Messages"/>, <see cref="PrintOutput"/>, and <see cref="Graphics"/> properties.
/// </para>
/// The .NET/Link distribution contains a sample program showing the use of MathKernel. You can find it in
/// [Mathematica directory]\AddOns\NETLink\Examples\Part2\MathKernel.
/// </remarks>
/// 
public class MathKernel : System.ComponentModel.Component {

    /*********************************  Instance data  *****************************/

    private IKernelLink ml;
    private string linkArgs = null;

    private object input;
    private object result;
    private ArrayList prints = new ArrayList();
    private ArrayList messages = new ArrayList();
    private ArrayList graphics = new ArrayList();

    private string graphicsFormat = "Automatic";
    private int graphicsWidth = 0;
    private int graphicsHeight = 0;
    private int graphicsResolution = 0;
    private ResultFormatType resultFormat = ResultFormatType.OutputForm;
    private int pageWidth = 0;

    private bool captureMessages = true;
    private bool capturePrint = true;
    private bool captureGraphics = false;
    private bool useFrontEnd = true;
    private bool autoCloseLink = true;
    // Voltatile becaue they might be called from another thread and we use no synchronization
    // guards when getting or setting their values:
    private volatile bool isConnected = false;
    private volatile bool isComputing = false;
    private bool handleEvents = true;

    private bool lastPktWasMsg;


    /**************************************  Constructors  ***************************************/

    /// <summary>
    /// Constructs a new MathKernel object.
    /// </summary>
    /// <remarks>
    /// When the first call to the <see cref="Compute"/> or <see cref="Connect"/> methods occur, the default
    /// <i>Mathematica</i> kernel will be launched. Alternatively, you can set the <see cref="Link"/> property
    /// before the first computation.
    /// </remarks>
    /// 
    public MathKernel() {}


    /// <summary>
    /// Constructs a new MathKernel object that uses the specified link.
    /// </summary>
    /// <remarks>
    /// Use this constructor to "attach" this MathKernel to an existing link.
    /// </remarks>
    /// 
    public MathKernel(IKernelLink ml) {
        this.ml = ml;
    }


    /// <summary>
    /// Closes the link if the <see cref="AutoCloseLink"/> property is set to true.
    /// </summary>
    ///
    ~MathKernel() {
        if (ml != null && AutoCloseLink)
            ml.Close();
        ml = null;
    }


    /**************************************  Properties  ***************************************/

    /// <summary>
    /// Gets or sets the link that this MathKernel will use.
    /// </summary>
    /// 
    public IKernelLink Link {
        get { return ml; }
        set { ml = value; }
    }


    /// <summary>
    /// Gets or sets the command string that will be passed to <see cref="MathLinkFactory.CreateKernelLink"/> to
    /// attach to the <i>Mathematica</i> kernel.
    /// </summary>
    /// <remarks>
    /// The value will only be used if the link is actually created by this MathKernel object. In other words, if you
    /// are going to set this property, you must do so before the link is created (this happens in the first call to
    /// the <see cref="Compute"/> or <see cref="Connect"/> methods).
    /// </remarks>
    /// 
    public string LinkArguments {
        get { return linkArgs; }
        set { linkArgs = value; }
    }   


    /// <summary>
    /// Gets or sets the input to <i>Mathematica</i> that will be used in the next call to <see cref="Compute"/>.
    /// </summary>
    /// <remarks>
    /// The value must be a string or <see cref="Expr"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">If the value is not a string or Expr.</exception>
    /// 
    public object Input {
        get { return input; }
        set {
            if (value == null || value is string || value is Expr)
                input = value;
            else
                throw new ArgumentException("Input must be a string or Expr.");
        }
    }


    /// <summary>
    /// Gets or sets the format in which you want the result of evaluations to be returned.
    /// </summary>
    /// <remarks>
    /// The type and format of the <see cref="Result"/> will depend on the value of this property.
    /// <list type="table">
    /// <listheader><term>Value of ResultFormat</term><description>Type of Result</description></listheader>
    /// <item><term>ResultFormatType.InputForm, ResultFormatType.OutputForm, ResultFormatType.MathML</term><description>string</description></item>
    /// <item><term>ResultFormatType.StandardForm, ResultFormatType.TraditionalForm</term><description><see cref="Image"/></description></item>
    /// <item><term>ResultFormatType.Expr</term><description><see cref="Expr"/></description></item>
    /// </list>
    /// </remarks>
    /// <seealso cref="Result"/>
    /// 
    public ResultFormatType ResultFormat {
        get { return resultFormat; }
        set { resultFormat = value; }
    }


    /// <summary>
    /// Gets or sets the image format for <i>Mathematica</i> graphics generated during a computation.
    /// </summary>
    /// <remarks>
    /// The default value is "Automatic", which is a good compromise between speed and image quality. You can use any of the
    /// values "GIF", "JPEG", "Metafile", and "Automatic".
    /// <para>
    /// Graphics output will only be captured if you set the <see cref="CaptureGraphics"/> property to true.
    /// </para>
    /// </remarks>
    /// <seealso cref="CaptureGraphics"/>
    /// <seealso cref="Graphics"/>
    /// 
    public string GraphicsFormat {
        get { return graphicsFormat; }
        set { graphicsFormat = value; }
    }


    /// <summary>
    /// Gets or sets the width to which output should be wrapped.
    /// </summary>
    /// <remarks>
    /// If <see cref=" ResultFormat"/> indicates a string result, this is the page width in characters;
    /// if ResultFormat indicates an Image result, this is the width in pixels.
    /// </remarks>
    /// <seealso cref="Result"/>
    /// 
    public int PageWidth {
        get { return pageWidth; }
        set { pageWidth = value; }
    }


    /// <summary>
    /// Gets or sets the width, in pixels, for <i>Mathematica</i> graphics generated during a computation.
    /// </summary>
    /// <remarks>
    /// The default value, 0, uses the default size in <i>Mathematica</i>.
    /// </remarks>
    /// <seealso cref="GraphicsHeight"/>
    /// <seealso cref="Graphics"/>
    /// 
    public int GraphicsWidth {
        get { return graphicsWidth; }
        set { graphicsWidth = value; }
    }


    /// <summary>
    /// Gets or sets the height, in pixels, for <i>Mathematica</i> graphics generated during a computation.
    /// </summary>
    /// <remarks>
    /// The default value, 0, uses the default size in <i>Mathematica</i>.
    /// </remarks>
    /// <seealso cref="GraphicsWidth"/>
    /// <seealso cref="Graphics"/>
    /// 
    public int GraphicsHeight {
        get { return graphicsHeight; }
        set { graphicsHeight = value; }
    }


    /// <summary>
    /// Gets or sets the resolution, in pixels per inch, for <i>Mathematica</i> graphics generated during a computation.
    /// </summary>
    /// <remarks>
    /// The default value, 0, uses the default resolution in <i>Mathematica</i>.
    /// <para>
    /// Most users will leave this value alone.
    /// </para>
    /// </remarks>
    /// <seealso cref="Graphics"/>
    /// 
    public int GraphicsResolution {
        get { return graphicsResolution; }
        set { graphicsResolution = value; }
    }


    /// <summary>
    /// Get or sets whether this MathKernel object should capture <i>Mathematica</i> Message output generated during a computation.
    /// </summary>
    /// <remarks>
    /// This value must be true to use the <see cref="Messages"/> property.
    /// <para>
    /// The default value is true.
    /// </para>
    /// </remarks>
    /// <seealso cref="Messages"/>
    /// 
    public bool CaptureMessages {
        get { return captureMessages; }
        set { captureMessages = value; }
    }


    /// <summary>
    /// Get or sets whether this MathKernel object should capture <i>Mathematica</i> Print output generated during a computation.
    /// </summary>
    /// <remarks>
    /// This value must be true to use the <see cref="PrintOutput"/> property.
    /// <para>
    /// The default value is true.
    /// </para>
    /// </remarks>
    /// <seealso cref="PrintOutput"/>
    /// 
    public bool CapturePrint {
        get { return capturePrint; }
        set { capturePrint = value; }
    }


    /// <summary>
    /// Get or sets whether this MathKernel object should capture <i>Mathematica</i> graphics output generated during a computation.
    /// </summary>
    /// <remarks>
    /// You must set this to true to use the <see cref="Graphics"/> property.
    /// <para>
    /// The default value is false.
    /// </para>
    /// </remarks>
    /// <seealso cref="Graphics"/>
    /// 
    public bool CaptureGraphics {
        get { return captureGraphics; }
        set { captureGraphics = value; }
    }


    /// <summary>
    /// Gets or sets whether the <i>Mathematica</i> notebook front end should be used in the background
    /// for graphics rendering services.
    /// </summary>
    /// <remarks>
    /// This property is deprecated when using <i>Mathematica</i> 5.1 and later--the front end is always used for rendering.
    /// <para>
    /// The default value is true.
    /// </para>
    /// See the <see cref="IKernelLink.UseFrontEnd">IKernelLink.UseFrontEnd</see> property for more information.
    /// </remarks>
    /// 
    public bool UseFrontEnd {
        get { return useFrontEnd; }
        set { useFrontEnd = value; }
    }


    /// <summary>
    /// Gets or sets whether the link should be closed when this MathKernel object is disposed.
    /// </summary>
    /// <remarks>
    /// In the typical case, where this MathKernel object launched the kernel, you should leave this
    /// property at its default value of true.
    /// </remarks>
    /// <seealso cref="Dispose"/>
    /// 
    public bool AutoCloseLink {
        get { return autoCloseLink; }
        set { autoCloseLink = value; }
    }


    /// <summary>
    /// Gets whether the link has been connected (typically, this means that the kernel has been launched and is ready for use).
    /// </summary>
    /// <remarks>
    /// The link will be connected during the first call to <see cref=" Connect"/> or <see cref="Compute"/>.
    /// </remarks>
    /// <seealso cref="Connect"/>
    /// 
    public bool IsConnected {
        get { return isConnected; }
    }


    /// <summary>
    /// Gets whether <i>Mathematica</i> is currently busy with a computation (that is, whether <see cref="Compute"/> is currently running).
    /// </summary>
    /// <remarks>
    /// You can use this property in an event handler method to see whether it is safe to make a call to <see cref="Compute"/>.
    /// With the <see cref="HandleEvents"/> property set to its default value of true, event handlers can fire while the Compute
    /// method is running. To prevent a reentrant call to Compute, you can check this property in an event handler to decide
    /// whether it is safe to proceed with a call to Compute.
    /// <para>
    /// If you go ahead and try to make a reentrant call to Compute, an InvalidOperationException will be thrown.
    /// </para>
    /// </remarks>
    /// <seealso cref="Compute"/>
    /// <seealso cref="HandleEvents"/>
    /// 
    public bool IsComputing {
        get { return isComputing; }
    }


    /// <summary>
    /// Gets or sets whether the the thread on which <see cref="Compute"/> is called should continue to handle application events
    /// during the period that Compute is waiting for the result.
    /// </summary>
    /// <remarks>
    /// Compute will not return until the <i>Mathematica</i> computation is finished. If you are calling Compute on your application's
    /// user-interface thread (this is typically the case, such as if you call Compute in a button-click handler), then if
    /// the HandleEvents property is set to false your application will not be responsive to user input until Compute returns. Users
    /// will not be able to click buttons, type text, or even drag the application window. You probably don't want this, which is why the
    /// default is true.
    /// <para>
    /// When HandleEvents is set to true, you must prevent users from triggering a call to Compute while another such call is currently
    /// in progress. One way to do this is to disable any means of triggering computations as soon as one is started, such as by
    /// disabling a "Compute" button once it has been clicked. Another possibility is to have your event handler methods check
    /// the <see cref="IsComputing"/> property and not call Compute if that property gives true.
    /// </para>
    /// If you go ahead and try to make a reentrant call to Compute, an InvalidOperationException will be thrown by the reentrant
    /// call to Compute.
    /// <para>
    /// The event handling is implemented via a <see cref="IMathLink.Yield"/> event handler that calls the .NET Framework method
    /// <see cref="System.Windows.Forms.Application.DoEvents">Application.DoEvents</see>.
    /// </para>
    /// </remarks>
    /// <seealso cref="Compute"/>
    /// <seealso cref="IsComputing"/>
    /// 
    public bool HandleEvents {
        get { return handleEvents; }
        set { handleEvents = value; }
    }


    /**************************************  Methods  ***************************************/

    /// <summary>
    /// Closes the link used by this MathKernel, if the <see cref="AutoCloseLink"/> property is true.
    /// </summary>
    /// 
    public new void Dispose() {

        base.Dispose();
        if (ml != null && AutoCloseLink)
            ml.Close();
        ml = null;
        GC.SuppressFinalize(this);
    }


    /// <summary>
    /// Creates and connects the link, if it has not already been connected. 
    /// </summary>
    /// <remarks>
    /// This will launch the kernel unless you have set the <see cref="Link"/> or <see cref="LinkArguments"/>
    /// properties.
    /// <para>
    /// This method is called automatically by <see cref="Compute"/>, but you can call it yourself if you want to
    /// force the kernel to launch before any computations are actually triggered.
    /// </para>
    /// </remarks>
    /// 
    public void Connect() {

        if (!isConnected) {
            if (ml == null)
                ml = linkArgs == null ? MathLinkFactory.CreateKernelLink() : MathLinkFactory.CreateKernelLink(linkArgs);
            ml.Yield += new YieldFunction(yielder);
            ml.Connect();
            ml.Evaluate("Needs[\"NETLink`\"]");
            // If we launched the kernel, we will get the first In[1] prompt. Either way, we get a
            // ReturnPacket from the Needs call.
            PacketType pkt = ml.WaitForAnswer();
            ml.NewPacket();
            if (pkt == PacketType.InputName)
                ml.WaitAndDiscardAnswer();
            ml.PacketArrived += new PacketHandler(MathKernelPacketHandler);
            isConnected = true;
        }
    }


    /// <overloads>
    /// <summary>
    /// Triggers a <i>Mathematica</i> computation.
    /// </summary>
    /// <remarks>
    /// This method does not return until the computation is finished.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Sends the current value of the <see cref="Input"/> property to <i>Mathematica</i> for evaluation.
    /// </summary>
    /// 
    public void Compute() {

        if (IsComputing)
            throw new InvalidOperationException("The Mathematica kernel is currently busy (you cannot make a reentrant call to MathKernel.Compute()).");
        isComputing = true;
        try {
            Clear();
            Connect();
            lock (ml) {
                try {
                    ml.PutFunction("EvaluatePacket", 1);
                    ml.PutFunction(KernelLinkImpl.PACKAGE_INTERNAL_CONTEXT + "computeWrapper", 9);
                    ml.Put(Input);
                    ml.Put(ResultFormat.ToString());
                    ml.Put(PageWidth);
                    ml.Put(GraphicsFormat);
                    ml.Put(GraphicsWidth);
                    ml.Put(GraphicsHeight);
                    ml.Put(GraphicsResolution);
                    ml.Put(UseFrontEnd);
                    ml.Put(CaptureGraphics);

                    ml.WaitForAnswer();

                    switch (ResultFormat) {
                        case ResultFormatType.Expr:
                            result = ml.GetExpr();
                            break;
                        case ResultFormatType.StandardForm:
                        case ResultFormatType.TraditionalForm:
                            result = readImage();
                            break;
                        default: {
                            result = ml.GetStringCRLF();
                            break;
                        }
                    }
                } catch (MathLinkException e) {
                    ml.ClearError();
                    ml.NewPacket();
                    throw e;
                }
            }
        } finally {
            isComputing = false;
        }
    }


    /// <summary>
    /// Sends the given string to <i>Mathematica</i> for evaluation.
    /// </summary>
    /// <param name="input">The input to evaluate in <i>Mathematica</i>.</param>
    /// 
    public void Compute(string input) {
        Input = input;
        Compute();
    }


    /// <summary>
    /// Sends a request to abort the computation currently in progress.
    /// </summary>
    /// <remarks>
    /// This method will be called from a different thread than the one that is executing
    /// the <see cref="Compute"/> method.
    /// <para>
    /// See the <see cref="IKernelLink.AbortEvaluation">IKernelLink.AbortEvaluation</see> method
    /// for more information.
    /// </para>
    /// </remarks>
    /// 
    public void Abort() {
        ml.AbortEvaluation();
    }


    /// <summary>
    /// Clears all the "output" values from the previous computation.
    /// </summary>
    /// <remarks>
    /// Clear out the values stored in the <see cref="Result"/>, <see cref="Messages"/>, <see cref="PrintOutput"/>,
    /// and <see cref="Graphics"/> properties. You can call this method to free memory if you no longer need the values.
    /// </remarks>
    /// 
    public void Clear() {

        result = null;
        messages.Clear();
        prints.Clear();
        graphics.Clear();
    }


    /********************************  Available after Compute()  *********************************/

    /// <summary>
    /// Gets the result from the last call to <see cref="Compute"/>.
    /// </summary>
    /// <remarks>
    /// The type and format of the result will depend on the value of the <see cref="ResultFormat"/> property.
    /// <list type="table">
    /// <listheader><term>Value of ResultFormat</term><description>Type of Result</description></listheader>
    /// <item><term>ResultFormatType.InputForm, ResultFormatType.OutputForm, ResultFormatType.MathML</term><description>string</description></item>
    /// <item><term>ResultFormatType.StandardForm, ResultFormatType.TraditionalForm</term><description><see cref="Image"/></description></item>
    /// <item><term>ResultFormatType.Expr</term><description><see cref="Expr"/></description></item>
    /// </list>
    /// </remarks>
    /// 
    public object Result {
        get { return result; }
    }


    /// <summary>
    /// Gets the accumulated strings from <i>Mathematica</i> Message output during the last call to <see cref="Compute"/>.
    /// </summary>
    /// <remarks>
    /// You must set the <see cref="CaptureMessages"/> property to true to use this property.
    /// </remarks>
    /// 
    public string[] Messages {
        get { return (string[]) messages.ToArray(typeof(string)); }
    }


    /// <summary>
    /// Gets the accumulated strings from <i>Mathematica</i> Print output during the last call to <see cref="Compute"/>.
    /// </summary>
    /// <remarks>
    /// You must set the <see cref="CapturePrint"/> property to true to use this property.
    /// </remarks>
    /// 
    public string[] PrintOutput {
        get { return (string[]) prints.ToArray(typeof(string)); }
    }


    /// <summary>
    /// Gets the accumulated Images from <i>Mathematica</i> graphics output during the last call to <see cref="Compute"/>.
    /// </summary>
    /// <remarks>
    /// You must set the <see cref="CaptureGraphics"/> property to true to use this property.
    /// </remarks>
    /// <seealso cref="GraphicsFormat"/>
    /// 
    public Image[] Graphics {
        get { return (Image[]) graphics.ToArray(typeof(Image)); }
    }


    /***********************************  ResultFormatType enum  *************************************/

    // These names must match the string forms expected by the ComputeWrapper Mathematica function.

    /// <summary>
    /// Values for the <see cref="ResultFormat"/> property. These values specify the format in which results
    /// from computations should be returned.
    /// </summary>
    /// 
    public enum ResultFormatType {
        /// <summary>
        /// A string in <i>Mathematica</i> InputForm.
        /// </summary>
        InputForm,
        /// <summary>
        /// A string in <i>Mathematica</i> OutputForm.
        /// </summary>
        OutputForm,
        /// <summary>
        /// An Image of output typeset in <i>Mathematica</i> StandardForm.
        /// </summary>
        StandardForm,
        /// <summary>
        /// An Image of output typeset in <i>Mathematica</i> TraditionalForm.
        /// </summary>
        TraditionalForm,
        /// <summary>
        /// A string of MathML.
        /// </summary>
        MathML,
        /// <summary>
        /// An <see cref="Wolfram.NETLink.Expr"/>.
        /// </summary>
        Expr
    }


    /****************************************  Private  ********************************************/

    // Accumulates the various packets for the Graphics, Messages, and PrintOutput properties.
    private bool MathKernelPacketHandler(IKernelLink ml, PacketType pkt) {

        switch (pkt) {
            case PacketType.DisplayEnd:
                // DisplayEndPacket is hijacked for the wrapper for image data for graphics generated during computation.
                graphics.Add(readImage());
                break;
            case PacketType.Text:
                if (lastPktWasMsg && captureMessages)
                    messages.Add(ml.GetStringCRLF());
                else if (capturePrint)
                    prints.Add(ml.GetStringCRLF());
                break;
            default:
                break;
        }
        lastPktWasMsg = pkt == PacketType.Message;
        return true;
    }


    // This reads the string result from the Mathematica functions EvaluateToImage or EvaluateToTypeset
    // (EvaluateToImage is also used to create images for graphics generated during a computation) and
    // converts to an Image object.
    private Image readImage() {
        byte[] imgData = ml.GetByteString(-1);
        return Image.FromStream(new System.IO.MemoryStream(imgData));
    }

    
    private bool yielder() {
        if (HandleEvents)
            System.Windows.Forms.Application.DoEvents();
        return false;
    }

}

}
