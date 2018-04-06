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
using System.Drawing;


namespace Wolfram.NETLink {

/// <summary>
/// The link interface that most programmers will use.
/// </summary>
/// <remarks>
/// The <see cref="IMathLink"/> interface contains low-level methods for reading and writing data. IKernelLink
/// extends the IMathLink interface, adding some higher-level methods that are appropriate on the assumption
/// that the program on the other side of the link is a <i>Mathematica</i> kernel.
/// <para>
/// An example is the WaitAndDiscardAnswer method, which reads and discards the sequence of packets
/// the kernel will send in the course of a single evaluation.
/// </para>
/// Do not forget, however, that all the methods in the <see cref="IMathLink"/> interface are also available
/// for IKernelLinks.
/// <para>
/// Most programmers will use links that implement this interface. The actual implementation classes are not
/// documented and are of no concern. You will always interact with link objects via an interface type.
/// Use the <see cref="MathLinkFactory.CreateKernelLink">MathLinkFactory.CreateKernelLink</see> method to create
/// an IKernelLink.
/// </para>
/// <example>
/// Here is a simple example program in C#:
/// <code>
/// using System;
/// using Wolfram.NETLink;
/// 
/// public class LinkTest {
///     public static void Main(String[] args) {
///     
///         // This launches the Mathematica kernel:
///         IKernelLink ml = MathLinkFactory.CreateKernelLink();
///         
///         // Discard the initial InputNamePacket the kernel will send when launched.
///         ml.WaitAndDiscardAnswer();
///         
///         // Now compute 2+2 in several different ways.
///         
///         // The easiest way. Send the computation as a string and get the result in a single call:
///         string result = ml.EvaluateToOutputForm("2+2", 0);
///         Console.WriteLine("2 + 2 = " + result);
///         
///         // Use Evaluate() instead of EvaluateToXXX() if you want to read the result as a native type
///         // instead of a string.
///         ml.Evaluate("2+2");
///         ml.WaitForAnswer();
///         int intResult = ml.GetInteger();
///         Console.WriteLine("2 + 2 = " + intResult);
///         
///         // You can also get down to the metal by using methods from IMathLink:
///         ml.PutFunction("EvaluatePacket", 1);
///         ml.PutFunction("Plus", 2);
///         ml.Put(2);
///         ml.Put(2);
///         ml.EndPacket();
///         ml.WaitForAnswer();
///         intResult = ml.GetInteger();
///         Console.WriteLine("2 + 2 = " + intResult);
///         
///         // Always Close when done:
///         ml.Close();
///     }
/// }
/// </code>
/// Here is the same program in Visual Basic .NET:
/// <code>
/// Imports Wolfram.NETLink
/// 
/// Public Class LinkTest
///     Public Shared Sub Main(ByVal args As String())
/// 
///         ' This launches the Mathematica kernel:
///         Dim ml As IKernelLink = MathLinkFactory.CreateKernelLink()
/// 
///         ' Discard the initial InputNamePacket the kernel will send when launched.
///          ml.WaitAndDiscardAnswer()
/// 
///         ' Now compute 2+2 in several different ways.
/// 
///         ' The easiest way. Send the computation as a string and get the result in a single call:
///         Dim result As String = ml.EvaluateToOutputForm("2+2", 0)
///         Console.WriteLine("2 + 2 = " &amp; result)
/// 
///         ' Use Evaluate() instead of EvaluateToXXX() if you want to read the result
///         ' as a native type instead of a string.
///         ml.Evaluate("2+2")
///         ml.WaitForAnswer()
///         Dim intResult As Integer = ml.GetInteger()
///         Console.WriteLine("2 + 2 = " &amp; intResult)
/// 
///         ' You can also get down to the metal by using methods from IMathLink:
///         ml.PutFunction("EvaluatePacket", 1)
///         ml.PutFunction("Plus", 2)
///         ml.Put(2)
///         ml.Put(2)
///         ml.EndPacket()
///         ml.WaitForAnswer()
///         intResult = ml.GetInteger()
///         Console.WriteLine("2 + 2 = " &amp; intResult)
/// 
///         'Always Close when done:
///         ml.Close()
///      End Sub
/// End Class
/// </code>
/// </example>
/// </remarks>
/// <seealso cref="MathLinkFactory"/>
/// <seealso cref="IMathLink"/>
/// 
[type:CLSCompliant(true)]
public interface IKernelLink : IMathLink {
    
    /// <summary>
    /// Gets or sets whether the <i>Mathematica</i> notebook front end should be used in the background to assist
    /// in rendering graphics.
    /// </summary>
    /// <remarks>
    /// This property is deprecated when using <i>Mathematica</i> 5.1 and later--the front end is always used for rendering.
    /// <para>
    /// The front end does a better job for many types of graphics than the standalone renderers that can be used instead,
    /// especially for 3D graphics with gradations of color. It is also necessary to use the front end to get typeset expressions
    /// in text in graphics, such as plot labels.
    /// </para>
    /// With this property set to true, the front end will typically launch in a special "server" mode the first time a graphic
    /// is created. This server instance of the front end has no user interface. It cannot be brought to the foreground
    /// and used like a normal instance.
    /// <para>
    /// The default value is true.
    /// </para>
    /// </remarks>
    /// 
    bool UseFrontEnd {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets the format in which <i>Mathematica</i> graphics will be rendered.
    /// </summary>
    /// <remarks>
    /// The current supported values for this property are "Automatic", "GIF", "JPEG", and "Metafile".
    /// <para>
    /// The default value is "Automatic", which tries to provide a good compromise between speed and quality for most graphics.
    /// When the setting is "Automatic", GIF will be used in most cases, JPEG in some.
    /// </para>
    /// You are encouraged to try "Metafile", which uses the Windows Metafile format. These images can be
    /// dynamically resized after they have been created, and will redraw in excellent quality at any size.
    /// </remarks>
    /// 
    string GraphicsFormat {
        get;
        set;
    }

    
    /// <summary>
    /// Gets or sets whether images created by the <see cref="EvaluateToTypeset"/> method should use <i>Mathematica</i>
    /// StandardForm (vs. TraditionalForm).
    /// </summary>
    /// <remarks>
    /// The default is true. False means to use TraditionalForm.
    /// </remarks>
    /// 
    bool TypesetStandardForm {
        get;
        set;
    }


    /// <overloads>
    /// <summary>
    /// Sends code for evaluation.
    /// </summary>
    /// <remarks>
    /// This method only sends the computation--it does not read any resulting packets off the link. You would
    /// typically follow a call to Evaluate with either <see cref="WaitForAnswer"/> or <see cref="WaitAndDiscardAnswer"/>,
    /// which read data from the link.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Sends a string of code for evaluation.
    /// </summary>
    /// <param name="s">The <i>Mathematica</i> code to evaluate, as a string.</param>
    /// <seealso cref="WaitForAnswer"/>
    /// <seealso cref="WaitAndDiscardAnswer"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Evaluate(string s);


    /// <summary>
    /// Sends an <see cref="Expr"/> for evaluation.
    /// </summary>
    /// <param name="e">The <i>Mathematica</i> code to evaluate, as an <see cref="Expr"/>.</param>
    /// <seealso cref="WaitForAnswer"/>
    /// <seealso cref="WaitAndDiscardAnswer"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Evaluate(Expr e);


    /// <overloads>
    /// <summary>
    /// Sends code for evaluation and returns the result as a string formatted in <i>Mathematica</i> InputForm.
    /// </summary>
    /// <remarks>
    /// This method sends the evaluation and waits for the result to come back as a string. It discards any packets
    /// other than the result (for example, <i>Mathematica</i> messages or Print output).
    /// <para>
    /// The string will have newlines in the Windows CR/LF format.
    /// </para>
    /// It does not throw a MathLinkException. Instead it returns null if there was an error. You can use the
    /// <see cref="LastError"/> property to see the MathLinkException that was generated.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Sends the code to evaluate as a string.
    /// </summary>
    /// <param name="s">The string to evaluate.</param>
    /// <param name="pageWidth">The page width, in characters, for line breaks in the result. Use 0 for Infinity.</param>
    /// <returns>
    /// The result of the evaluation, as an InputForm string, or null if an error occurred. The string will
    /// have newlines in the Windows CR/LF format.
    /// </returns>
    /// <seealso cref="LastError"/>
    ///  
    string EvaluateToInputForm(string s, int pageWidth);


    /// <summary>
    /// Sends the code to evaluate as an <see cref="Expr"/>.
    /// </summary>
    /// <param name="e">The Expr to evaluate.</param>
    /// <param name="pageWidth">The page width, in characters, for line breaks in the result. Use 0 for Infinity.</param>
    /// <returns>The result of the evaluation, as an InputForm string, or null if an error occurred. The string will
    /// have newlines in the Windows CR/LF format.
    /// </returns>
    /// <seealso cref="LastError"/>
    /// 
    string EvaluateToInputForm(Expr e, int pageWidth);


    /// <overloads>
    /// <summary>
    /// Sends code for evaluation and returns the result as a string formatted in <i>Mathematica</i> OutputForm.
    /// </summary>
    /// <remarks>
    /// This method sends the evaluation and waits for the result to come back as a string. It discards any packets
    /// other than the result (for example, <i>Mathematica</i> messages or Print output).
    /// <para>
    /// The string will have newlines in the Windows CR/LF format.
    /// </para>
    /// It does not throw a MathLinkException. Instead it returns null if there was an error. You can use the
    /// <see cref="LastError"/> property to see the MathLinkException that was generated.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Sends the code to evaluate as a string.
    /// </summary>
    /// <param name="s">The string to evaluate.</param>
    /// <param name="pageWidth">The page width, in characters, for line breaks in the result. Use 0 for Infinity.</param>
    /// <returns>The result of the evaluation, as an OutputForm string, or null if an error occurred. The string will
    /// have newlines in the Windows CR/LF format.
    /// </returns>
    /// <seealso cref="LastError"/>
    /// 
    string EvaluateToOutputForm(string s, int pageWidth);


    /// <summary>
    /// Sends the code to evaluate as an <see cref="Expr"/>.
    /// </summary>
    /// <param name="e">The <see cref="Expr"/> to evaluate.</param>
    /// <param name="pageWidth">The page width, in characters, for line breaks in the result. Use 0 for Infinity.</param>
    /// <returns>The result of the evaluation, as an InputForm string, or null if an error occurred. The string will
    /// have newlines in the Windows CR/LF format.
    /// </returns>
    /// <seealso cref="LastError"/>
    /// 
    string EvaluateToOutputForm(Expr e, int pageWidth);
    

    /// <overloads>
    /// <summary>
    /// Sends graphics or plotting code for evaluation and returns the result as an <see cref="Image"/> object.
    /// </summary>
    /// <remarks>
    /// This method sends the evaluation and waits for the result to come back as an Image. It discards any packets
    /// other than the result (for example, <i>Mathematica</i> messages or Print output).
    /// <para>
    /// It does not throw a MathLinkException. Instead it returns null if there was an error. You can use the
    /// <see cref="LastError"/> property to see the MathLinkException that was generated.
    /// </para>
    /// The image will be sized to just fit within a box of <paramref name="width"/> x <paramref name="height"/>, without
    /// changing its aspect ratio. This means that the image might not have exactly these dimensions, but it will never be larger.
    /// <para>
    /// You can use the <see cref="GraphicsFormat"/> property to control the format in which the Image is returned.
    /// </para>
    /// If the input does not evaluate to a graphics expression, null is returned. It is not enough that the computation
    /// causes a plot to be generated--the <i>return</i> value of the computation must be a <i>Mathematica</i> Graphics
    /// (or Graphics3D, SurfaceGraphics, etc.) expression. For example:
    /// <code>
    /// BAD:  ml.EvaluateToImage("Plot[x,{x,0,1}];", 400, 400);
    /// GOOD: ml.EvaluateToImage("Plot[x,{x,0,1}]", 400, 400);
    /// </code>
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Sends the code to evaluate as a string.
    /// </summary>
    /// <param name="s">The string to evaluate.</param>
    /// <param name="width">The desired image width, in pixels. Pass 0 for Automatic, or if the expression itself specifies the width.</param>
    /// <param name="height">The desired image height, in pixels. Pass 0 for Automatic, or if the expression itself specifies the height.</param>
    /// <returns>
    /// The resulting Image, or null if <paramref name="s"/> does not evaluate to a graphics expression or if a MathLinkException occurred.
    /// </returns>
    /// <seealso cref="LastError"/>
    ///
    Image EvaluateToImage(string s, int width, int height);


    /// <summary>
    /// Sends the code to evaluate as an <see cref="Expr"/>.
    /// </summary>
    /// <param name="e">The <see cref="Expr"/> to evaluate.</param>
    /// <param name="width">The desired image width, in pixels. Pass 0 for Automatic, or if the expression itself specifies the width.</param>
    /// <param name="height">The desired image height, in pixels. Pass 0 for Automatic, or if the expression itself specifies the height.</param>
    /// <returns>
    /// The resulting Image, or null if <paramref name="e"/> does not evaluate to a graphics expression or if a MathLinkException occurred.
    /// </returns>
    /// <seealso cref="LastError"/>
    ///
    Image EvaluateToImage(Expr e, int width, int height);


    /// <overloads>
    /// <summary>
    /// Sends code for evaluation and returns the result formatted in typeset form as an <see cref="Image"/> object.
    /// </summary>
    /// <remarks>
    /// This method sends the evaluation and waits for the result to come back as an Image. It discards any packets
    /// other than the result (for example, <i>Mathematica</i> messages or Print output).
    /// <para>
    /// It does not throw a MathLinkException. Instead it returns null if there was an error. You can use the
    /// <see cref="LastError"/> property to see the MathLinkException that was generated.
    /// </para>
    /// You can use the <see cref="TypesetStandardForm"/> property to control whether the result is formatted in
    /// <i>Mathematica</i> StandardForm (the default) or TraditionalForm.
    /// <para>
    /// The image will have linebreaks to keep it within the specified width, measured in pixels.
    /// </para>
    /// You can use the <see cref="GraphicsFormat"/> property to control the format in which the Image is returned.
    /// If GraphicsFormat is set to "Metafile", however, then the default format of GIF is used instead, as Metafile
    /// results in poor quality typeset output.
    /// <para>
    /// The notebook front end will always be used for rendering services. See the <see cref="UseFrontEnd"/> property for more
    /// information about this process.
    /// </para>
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Sends the code to evaluate as a string.
    /// </summary>
    /// <param name="s">The string to evaluate.</param>
    /// <param name="width">
    /// The width to wrap the output to during typesetting, in pixels (a rough measure). Pass 0 for Infinity, or
    /// if the expression itself specifies the width.
    /// </param>
    /// <returns> The resulting Image, or null if a MathLinkException occurred.</returns>
    /// <seealso cref="LastError"/>
    ///
    Image EvaluateToTypeset(string s, int width);


    /// <summary>
    /// Sends the code to evaluate as an <see cref="Expr"/>.
    /// </summary>
    /// <param name="e">The <see cref="Expr"/> to evaluate.</param>
    /// <param name="width">
    /// The width to wrap the output to during typesetting, in pixels (a rough measure). Pass 0 for Infinity, or
    /// if the expression itself specifies the width.
    /// </param>
    /// <returns> The resulting Image, or null if a MathLinkException occurred.</returns>
    /// <seealso cref="LastError"/>
    ///
    Image EvaluateToTypeset(Expr e, int width);


    /// <summary>
    /// Reads and discards all packets that arrive up until the packet that contains the result of the computation.
    /// </summary>
    /// <remarks>
    /// After this method returns, it is your responsibility to read the contents of the packet that holds the result.
    /// If you are not interested in examining the result of the evaluation, use <see cref="WaitAndDiscardAnswer"/> instead.
    /// <para>
    /// Use this method after sending an expression to evaluate with <see cref="Evaluate"/> or a manual sequence of Put methods.
    /// </para>
    /// <example>
    /// <code>
    /// ml.Evaluate("2+2");
    /// ml.WaitForAnwser();
    /// int result = ml.GetInteger();
    /// // It is not strictly necessary to call newPacket, since we have read the entire packet contents, but it is good style.
    /// ml.NewPacket();
    /// </code>
    /// </example>
    /// Examples of packets that arrive before the result and are discarded are PacketType.Text,
    /// PacketType.Message, PacketType.Display, etc. If you want to examine or operate on the incoming packets
    /// that are discarded by this method, use the <see cref="PacketArrived"/> event.
    /// <para>
    /// It returns the packet type that held the result of the computation. Typically, this will
    /// be PacketType.Return. However, in the unlikely event that you are manually sending evaluations
    /// inside an EnterTextPacket or EnterExpressionPacket, the packet will be different, and there are some
    /// further issues that you need to understand; consult the .NET/Link User Guide for details.
    /// </para>
    /// </remarks>
    /// <returns>The packet type that held the answer, typically PacketType.Return.</returns>
    /// <seealso cref="WaitAndDiscardAnswer"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    PacketType WaitForAnswer();


    /// <summary>
    /// Reads and discards all packets from a computation, including the result.
    /// </summary>
    /// <remarks>
    /// Use this method if you are not interested in the result of a computation.
    /// If you <i>are</i> interested in examining the result of the evaluation, use <see cref="WaitForAnswer"/> instead.
    /// <para>
    /// Use this method after sending an expression to evaluate with <see cref="Evaluate"/> or a manual sequence of Put methods.
    /// </para>
    /// <example>
    /// <code>
    /// ml.Evaluate("Needs[\"Algebra`FiniteFields`\"]");
    /// ml.WaitAndDiscardAnswer();
    /// </code>
    /// </example>
    /// Examples of packets that arrive and are discarded other than the result are PacketType.Text,
    /// PacketType.Message, PacketType.Display, etc. If you want to examine or operate on the incoming packets
    /// that are discarded by this method, use the <see cref="PacketArrived"/> event.
    /// </remarks>
    /// <seealso cref="WaitForAnswer"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void WaitAndDiscardAnswer();


    /// <summary>
    /// Call this to invoke .NET/Link's internal handling of special packet types.
    /// </summary>
    /// <remarks>
    /// If you absolutely must write your own packet loop instead of using the <see cref="PacketArrived"/> event,
    /// (this is strongly discouraged), you should call this method if a call to NextPacket returns a packet type
    /// that you are not handling entirely with your own code. In fact, you can call HandlePacket for <i>every</i> packet
    /// you read with NextPacket.
    /// <para>
    /// <example>
    /// For example, here is a basic packet loop:
    /// <code>
    /// bool done = false;
    /// while (!done) {
    ///     PacketType pkt = ml.NextPacket();
    ///     if (ml.OnPacketArrived(pkt))
    ///         ml.HandlePacket(pkt);
    ///     switch (pkt) {
    ///         case PacketType.Return:
    ///             // read and handle contents of ReturnPacket ...
    ///             done = true;
    ///             break;
    ///          case PacketType.Text:
    ///             // read and handle contents of TextPacket ...
    ///             break;
    ///         .. etc for other packet types
    ///     }
    ///     ml.NewPacket();
    /// }
    /// </code>
    /// </example>
    /// </para>
    /// After HandlePacket returns you should call <see cref="IMathLink.NewPacket">NewPacket</see>.
    /// <para>
    /// To remind again, writing your own packet loop like this is strongly discouraged. Use
    /// WaitForAnswer, WaitAndDiscardAnswer, or one of the "EvaluateTo" methods instead. These
    /// methods hide the packet loop within them. If you want more information about what packet
    /// types arrive and their contents, simply use the <see cref="PacketArrived"/> event.
    /// </para>
    /// An example of the special type of packets that your packet loop might encounter is PacketType.Call.
    /// Encountering a PacketType.Call means that <i>Mathematica</i> code is trying to call into .NET using the
    /// mechanism described in Part 1 of the .NET/Link User Guide. Only the internals of .NET/Link know
    /// how to manage these callbacks, so the HandlePacket method provides a means to invoke
    /// this handling for you.
    /// <para>
    /// If you are using WaitForAnswer, WaitAndDiscardAnswer, or any of the "EvaluateTo"
    /// methods, and therefore not writing your own packet loop, you do not need to be concerned
    /// with HandlePacket.
    /// </para>
    /// </remarks>
    /// <param name="pkt">The packet type that was read using <see cref="IMathLink.NextPacket">NextPacket</see>.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void HandlePacket(PacketType pkt);


    /// <summary>
    /// Raises the <see cref="PacketArrived"/> event.
    /// </summary>
    /// <remarks>
    /// Very few programmers will call this directly. It is normally called by the internal packet loop that
    /// .NET/Link runs when you call WaitForAnswer, WaitAndDiscardAnswer, or one of the "EvaluateTo" methods.
    /// Programmers who for some reason choose to write their own packet loop must call this method to raise
    /// the <see cref="PacketArrived"/> event so that delegates attached to that event can be notified.
    /// <para>
    /// See the example for <see cref="HandlePacket"/>.
    /// </para>
    /// </remarks>
    /// <param name="pkt">The packet type that has just arrived.</param>
    /// <returns>Whether to continue processing the packet.</returns>
    /// <seealso cref="HandlePacket"/>
    /// 
    bool OnPacketArrived(PacketType pkt);


    /// <summary>
    /// Sends an object, including strings and arrays. Overrides the IMathLink version to allow
    /// you to send objects "by reference" that have no meaningful value representation in <i>Mathematica</i>.
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <listheader><term>Type</term><description>Sent As:</description></listheader>
    /// <item><term>null</term><description>the symbol Null</description></item>
    /// <item><term>string</term><description><i>Mathematica</i> string</description></item>
    /// <item><term>array</term><description>list of the appropriate dimensions</description></item>
    /// <item><term>boxed primitive (Int32, Boolean, etc.)</term><description>the unboxed value</description></item>
    /// <item><term>Expr</term><description>expression</description></item>
    /// <item><term>Complex class</term><description>Complex number</description></item>
    /// <item><term>all other objects</term><description>NETObject expression</description></item>
    /// </list>
    /// <para>
    /// Put sends objects that meaningful "value" representations in <i>Mathematica</i> as their values, and
    /// behaves like <see cref="PutReference"/> for objects that have no meaningful value and therefore must
    /// be sent by reference (that is, as NETObject expressions).
    /// </para>
    /// You must call <see cref="EnableObjectReferences"/> before Put will be able to send objects by reference.
    /// </remarks>
    /// <param name="obj">The object to send.</param>
    /// <seealso cref="EnableObjectReferences"/>
    /// <seealso cref="PutReference"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    new void Put(object obj);


    /// <overloads>
    /// <summary>
    /// Sends an object to <i>Mathematica</i> "by reference".
    /// </summary>
    /// <remarks>
    /// Use this method to pass .NET objects to <i>Mathematica</i> so that methods can be invoked directly
    /// from <i>Mathematica</i> code as described in Part 1 of the .NET/Link User Guide.
    /// <para>
    /// Objects sent to Mathematica using this method arrive as NETObject expressions.
    /// </para>
    /// You must call <see cref="EnableObjectReferences"/> before PutReference will function properly.
    /// <para>
    /// The Put method (inherited from <see cref="IMathLink.Put">IMathLink</see>) will also send some objects by reference,
    /// but there are three reasons to call PutReference instead:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// You want to force an object that would normally be sent by value (for example, a string) to be sent by reference.
    /// </item>
    /// <item>
    /// You know that the object is a type that Put would send by reference, but you think it makes your code clearer
    /// to call PutReference explicitly.
    /// </item>
    /// <item>
    /// You want to control the type by which the object is seen as in <i>Mathematica</i>.
    /// </item>
    /// </list>
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Sends the object as its actual runtime type.
    /// </summary>
    /// <param name="obj">The object to send by reference.</param>
    /// <seealso cref="EnableObjectReferences"/>
    /// <seealso cref="GetObject"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void PutReference(object obj);
        

    /// <summary>
    /// Sends the object cast as a parent type or interface.
    /// </summary>
    /// <remarks>
    /// Use this form of PutReference if you want to "upcast" the object so that it is seen in <i>Mathematica</i>
    /// as an instance of a parent class or interface. You would want to do this if you needed to call 
    /// from <i>Mathematica</i> an inherited version of a method that was hidden by a "new" declaration lower in
    /// the inheritance hierarchy. For example, consider the following classes:
    /// <code>
    /// public class Parent {
    ///     public virtual string Foo() { return "parent foo"; }
    /// }
    /// 
    /// public class Child {
    ///     public new string Foo() { return "child foo"; }
    /// }
    /// </code>
    /// In C# code, if you had an object of type Child and you wanted to call the Parent version of Foo(),
    /// you would upcast the object like this:
    /// <code>
    /// Child childObject = new Child();
    /// string result = ((Parent) childObject).Foo();
    /// </code>
    /// This PutReference() method is the equivalent for <i>Mathematica</i> programmers. You would send the
    /// object to <i>Mathematica</i> typed as Parent, so that when you called the Foo() method you would
    /// get the version from the Parent class.
    /// <para>
    /// An alternative is to use the other signature of PutReference() to send the object to <i>Mathematica</i>
    /// as its normal type and then call the <i>Mathematica</i> function CastNETObject[] to upcast it to a
    /// parent type.
    /// </para>
    /// For more examples of when you would want to use this method, see the .NET/Link User Guide for its
    /// discussion of the CastNETObject Mathematica function. 
    /// <para>
    /// This method is virtually never used as a "downcast". Downcasting is generally irrelevant in
    /// .NET/Link because objects are normally sent to <i>Mathematica</i> as their true runtime type
    /// (there is no lower-down type to cast to). It is only useful to upcast to a parent type or interface.
    /// The one exception to this is if you have an object that you have previously upcast to a parent type
    /// and you want to downcast it back to its true runtime type.
    /// </para>
    /// </remarks>
    /// <param name="obj">The object to send by reference.</param>
    /// <param name="t">The type the object will be seen as in <i>Mathematica</i>.</param>
    /// <seealso cref="EnableObjectReferences"/>
    /// <seealso cref="GetObject"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// <exception cref="InvalidCastException">If the object cannot be cast to the given type.</exception>
    /// 
    void PutReference(object obj, Type t);
        

    /// <summary>
    /// Gives the type of the next element in the expression currently being read. Overrides the IMathLink
    /// method by allowing the extra return type ExpressionType.Object.
    /// </summary>
    /// <remarks>
    /// ExpressionType.Object is returned whenever a NETObject expression, or the symbol Null, is waiting on the link.
    /// Otherwise this method behaves just like the <see cref="IMathLink.GetNextExpressionType"> version
    /// in the IMathLink interface</see>.
    /// <para>
    /// To check the type of a partially read element without advancing to the next element, use <see cref="GetExpressionType"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetExpressionType"/>
    /// <seealso cref="EnableObjectReferences"/>
    /// <seealso cref="GetObject"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    ///
    new ExpressionType GetNextExpressionType();
    

    /// <summary>
    /// Gives the type of the current element in the expression currently being read. Overrides the IMathLink
    /// method by allowing the extra return type ExpressionType.Object.
    /// </summary>
    /// <remarks>
    /// ExpressionType.Object is returned whenever a NETObject expression, or the symbol Null, is waiting on the link.
    /// Otherwise this method behaves just like the <see cref="IMathLink.GetExpressionType"> version
    /// in the IMathLink interface</see>.
    /// <para>
    /// Unlike <see cref="GetNextExpressionType"/>, GetExpressionType will not advance to the next element if the current
    /// element has only been partially read.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetNextExpressionType"/>
    /// <seealso cref="EnableObjectReferences"/>
    /// <seealso cref="GetObject"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    ///
    new ExpressionType GetExpressionType();
    

    /// <summary>
    /// Reads any single expression off the link and returns an appropriate object. Overrides the IMathLink
    /// version to allow you to read NETObject expressions.
    /// </summary>
    /// <remarks>
    /// This method works to read any expression that has a "natural" mapping into a .NET type. The following table shows how
    /// expressions are read:
    /// <list type="table">
    /// <listheader><term>Incoming <i>Mathematica</i> expression</term><description>Read as:</description></listheader>
    /// <item><term>Integer</term><description><see cref="Int32"/></description></item>
    /// <item><term>Real</term><description><see cref="Double"/></description></item>
    /// <item><term>True or False</term><description><see cref="Boolean"/></description></item>
    /// <item><term>Null</term><description><c>null</c></description></item>
    /// <item><term>String or Symbol</term><description><see cref="String"/></description></item>
    /// <item><term>Complex number</term><description>Complex type</description></item>
    /// <item><term>Function</term><description><see cref="Array"/></description></item>
    /// <item><term>NETObject</term><description>the .NET object it refers to</description></item>
    /// </list>
    /// This behavior is exactly like the <see cref="IMathLink.GetObject">version
    /// in the IMathLink interface</see> except that it addds the ability to read NETObject expressions as the objects
    /// they refer to. In other words, you can read .NET objects sent from <i>Mathematica</i>.
    /// <para>
    /// You must call <see cref="EnableObjectReferences"/> before you can send or receive object references.
    /// </para>
    /// </remarks>
    /// <seealso cref="EnableObjectReferences"/>
    /// <seealso cref="GetNextExpressionType"/>
    /// <seealso cref="GetExpressionType"/>
    /// <seealso cref="PutReference"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    new Object GetObject();
    

    /// <overloads>
    /// <summary>
    /// Reads an array of the specified type and depth. Overrides the IMathLink version by allowing you
    /// to specify an object type as the leaf type of the array.
    /// </summary>
    /// <remarks>
    /// See the <see cref="IMathLink.GetArray">IMathLink version of this method</see> for more information.
    /// <para>
    /// The additional ability in the IKernelLink override of this method is to read arrays of object references. For example,
    /// if a list of three NETObject expressions was waiting on the link, you could read them in a single call as follows:
    /// </para>
    /// <code>
    /// // C#
    /// object[] result = (object[]) ml.GetArray(typeof(object), 1);
    ///   
    /// // VB
    /// Dim result() as Object = CType(ml.GetArray(GetType(Object), 1), GetType(Object()))
    /// </code>
    /// You must call <see cref="EnableObjectReferences"/> before you can send or receive object references.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Reads an array and discards information about the heads at each level.
    /// </summary>
    /// <remarks>
    /// The information about the heads is lost; if you need this information you can use the alternative
    /// overload GetArray(int type, int depth, out string[] heads), or read the expression as an Expr and
    /// examine it using the Expr methods.
    /// </remarks>
    /// <param name="leafType">The type of the leaf elements of the returned array.</param>
    /// <param name="depth">The requested depth.</param>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    new Array GetArray(Type leafType, int depth);
    

    /// <summary>
    /// Reads an array and records information about the heads at each level.
    /// </summary>
    /// <remarks>
    /// This method does not enforce a requirement that the heads be identical across a level. If the expression looks
    /// like {foo[1, 2], bar[3, 4]} then the <paramref name="heads"/> array would become {"List", "foo"}, ignoring the
    /// fact that foo was not the head of every subexpression at level 1. In other words, if heads[i] is "foo", then it
    /// is only guaranteed that <i>the first</i> expression at level i had head foo, not that <i>all</i> of them did.
    /// </remarks>
    /// <param name="leafType">The type of the leaf elements of the returned array.</param>
    /// <param name="depth">The requested depth.</param>
    /// <param name="heads">Gets the heads at each level.</param>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    new Array GetArray(Type leafType, int depth, out string[] heads);
    
    
    /// <summary>
    /// Call this method to enable the ability to pass .NET objects "by reference" to <i>Mathematica</i>. 
    /// </summary>
    /// <remarks>
    /// You must call this before attempting to call <see cref="PutReference"/>, or Put on an object that will be sent by reference.
    /// <para>
    /// You should call this only once on a given link, typically when it is first created.
    /// </para>
    /// </remarks>
    /// <seealso cref="PutReference"/>
    /// <seealso cref="GetObject"/>
    /// 
    void EnableObjectReferences();


    /// <summary>
    /// Sends a request to the kernel to abort the current evaluation.
    /// </summary>
    /// <remarks>
    /// This method is typically called from a different thread than the one that is performing
    /// the computation. That "computation" thread is typically blocking in WaitForAnswer or some
    /// other method that is waiting for the result to arrive from <i>Mathematica</i>. If you want to
    /// abort the computation so that the main thread does not have to continue waiting, you
    /// can call AbortEvaluation on some other thread (perhaps in response to some user action
    /// like clicking an "abort" button). The <i>Mathematica</i> computation will terminate and return the symbol $Aborted.
    /// <para>
    /// Be aware that <i>Mathematica</i> is not always in a state where it is receptive to abort requests. A quick
    /// return of $Aborted is not always guaranteed.
    /// </para>
    /// What this method does is simply use <see cref="IMathLink.PutMessage">PutMessage</see> to send a MathLinkMessage.Abort to the
    /// kernel. It is provided as a convenience to shield programmers from such low-level details.
    /// </remarks>
    /// <seealso cref="InterruptEvaluation"/>
    /// <seealso cref="AbandonEvaluation"/>
    /// <seealso cref="TerminateKernel"/>
    /// 
    void AbortEvaluation();
    

    /// <summary>
    /// Sends a request to the kernel to interrupt the current evaluation.
    /// </summary>
    /// <remarks>
    /// Interrupt requests should not be confused with abort requests. Interrupt requests generate a special
    /// MenuPacket from the kernel that needs a response. You must be prepared to handle this packet if you
    /// call InterruptEvaluation. Most programmers want to simply abort the evaluation--use
    /// <see cref="AbortEvaluation"/> for that.
    /// <para>
    /// This method is typically called from a different thread than the one that is performing
    /// the computation. That "computation" thread is typically blocking in WaitForAnswer or some
    /// other method that is waiting for the result to arrive from <i>Mathematica</i>. If you want to
    /// interrupt the computation to provide your users with choices ("abort", "continue", "enter dialog",
    /// etc.), you can call InterruptEvaluation on some other thread (perhaps in response to
    /// the user clicking an "interrupt" button).
    /// </para>
    /// Be aware that <i>Mathematica</i> is not always in a state where it is receptive to interrupt requests.
    /// <para>
    /// What this method does is simply use <see cref="IMathLink.PutMessage">PutMessage</see> to send a MathLinkMessage.Interrupt to the
    /// kernel. It is provided as a convenience to shield programmers from such low-level details.
    /// </para>
    /// </remarks>
    /// <seealso cref="AbortEvaluation"/>
    /// <seealso cref="AbandonEvaluation"/>
    /// <seealso cref="TerminateKernel"/>
    /// 
    void InterruptEvaluation();
    

    /// <summary>
    /// Causes any method that is blocking waiting for output from the kernel to return immediately
    /// and throw a MathLinkException.
    /// </summary>
    /// <remarks>
    /// This is a "last-ditch" method when you absolutely need to break out of any methods that are
    /// waiting for a result from the kernel. You should should always close the link after you call this.
    /// <para>
    /// This method is typically called from a different thread than the one that is performing
    /// the computation. That "computation" thread is typically blocking in WaitForAnswer or some
    /// other method that is waiting for the result to arrive from <i>Mathematica</i>. If you want to force that
    /// method to return immediately (it will throw a MathLinkException), call AbandonEvaluation in a different thread.
    /// </para>
    /// The code in the catch handler for the MathLinkException will find that ClearError returns
    /// false, indicating that the link is irrevocably damaged. You should then call <see cref="TerminateKernel"/>
    /// followed by <see cref="IMathLink.Close">Close</see>.
    /// </remarks>
    /// <seealso cref="AbortEvaluation"/>
    /// <seealso cref="InterruptEvaluation"/>
    /// <seealso cref="TerminateKernel"/>
    /// 
    void AbandonEvaluation();
    

    /// <summary>
    /// Sends a request to the kernel to shut down.
    /// </summary>
    /// <remarks>
    /// Most of the time, when you call Close on a link, the kernel will quit. If the kernel is busy
    /// with a computation, however, it will not stop just because the link closes. Use TerminateKernel
    /// to force the kernel to quit even though it may be busy. This is not an operating system-level "kill"
    /// command, and it is not absolutely guaranteed that the kernel will die immediately.
    /// <para>
    /// This method is safe to call from any thread. Any method that is blocking waiting for a result
    /// from <i>Mathematica</i> (such as WaitForAnswer) will return immediately and throw a MathLinkException.
    /// You will typically call <see cref="IMathLink.Close">Close</see> immediately after TerminateKernel,
    /// as the link will die when the kernel quits.
    /// </para>
    /// A typical usage scenario is as follows. You have a thread that is blocking in WaitForAnswer
    /// waiting for the result of some computation, and you decide that it must return right away and
    /// you are willing to sacrifice the kernel to guarantee this. You then call AbandonEvaluation on
    /// a separate thread. This causes WaitForAnswer to immediately throw a MathLinkException. You
    /// catch this exception, discover that ClearError returns false indicating that the link is hopeless,
    /// and then you call TerminateKernel followed by Close. The reason TerminateKernel is useful
    /// here is that because you called AbandonEvaluation, the kernel may still be computing and it
    /// may not die when you call Close. You call TerminateKernel to give it a little help.
    /// <para>
    /// What this method does is simply use <see cref="IMathLink.PutMessage">PutMessage</see> to send a MathLinkMessage.Terminate to the
    /// kernel. It is provided as a convenience to shield programmers from such low-level details.
    /// </para>
    /// </remarks>
    /// <seealso cref="AbortEvaluation"/>
    /// <seealso cref="InterruptEvaluation"/>
    /// <seealso cref="AbandonEvaluation"/>
    /// 
    void TerminateKernel();
    

    /***************************************  Properties  ************************************************/

    /// <summary>
    /// Gets the Exception object that represents any exception detected during the last call
    /// of one of the "EvaluateTo" methods (EvaluateToInputForm, EvaluateToOutputForm,
    /// EvaluateToImage, EvaluateToTypeset).
    /// </summary>
    /// <remarks>
    /// For convenience, the "EvaluateTo" methods don't throw MathLinkException. Instead, they catch
    /// any such exceptions and simply return null if one occurred. Sometimes you want to see what the exact
    /// exception was when you get back a null result. The LastError property shows you that exception.
    /// Typically, it will be a MathLinkException, but there are some other rare cases.
    /// </remarks>
    /// <seealso cref="EvaluateToInputForm"/>
    /// <seealso cref="EvaluateToOutputForm"/>
    /// <seealso cref="EvaluateToImage"/>
    /// <seealso cref="EvaluateToTypeset"/>
    /// 
    Exception LastError {
        get;
    }
    

    ////////////////////////////////////////  Rest are for "StdLink"'s only.  ///////////////////////////////////////
    
    // It is perhaps a design error that these methods are here, as they are not relevant to all uses of an
    // IKernelLink. The alternative is that they be static methods in the StdLink class. It is not feasible
    // to put them in an IStdLink subinterface of IKernelLink, as there is no way to know at the time the link
    // class is created whether it will ever need the extra capabilities of an IStdLink (an ordinary IKernelLink
    // can become an "IStdlink" at runtime if the user calls Install[$ParentLink] amd Install.install(kl)).

    // Note that these "output" funcs don't throw MathLinkException.

    /// <summary>
    /// Prints the specified text in the user's <i>Mathematica</i> session.
    /// </summary>
    /// <remarks>
    /// This method is usable only in .NET code that is invoked in a call from <i>Mathematica</i>, as described in Part 1
    /// of the .NET/Link User Guide. In other words, it is only used in code that is called from a <i>Mathematica</i>
    /// session via the "installable .NET" mechanism. Programmers who are launching the kernel and controlling it
    /// from a .NET program will have no use for this method.
    /// <para>
    /// The IKernelLink object on which this method will be called will probably be obtained via the
    /// <see cref="StdLink.Link">StdLink.Link</see> property.
    /// </para>
    /// </remarks>
    /// <param name="s">The text to print.</param>
    /// <seealso cref="Message"/>
    /// 
    void Print(string s);


    /// <summary>
    /// Prints the specified message in the user's <i>Mathematica</i> session.
    /// </summary>
    /// <remarks>
    /// This method is usable only in .NET code that is invoked in a call from <i>Mathematica</i>, as described in Part 1
    /// of the .NET/Link User Guide. In other words, it is only used in code that is called from a <i>Mathematica</i>
    /// session via the "installable .NET" mechanism. Programmers who are launching the kernel and controlling it
    /// from a .NET program will have no use for this method.
    /// <para>
    /// The IKernelLink object on which this method will be called will probably be obtained via the
    /// <see cref="StdLink.Link">StdLink.Link</see> property.
    /// </para>
    /// </remarks>
    /// <param name="symtag">The message designation, in the usual Symbol::tag style, like <i>Mathematica</i>'s Message function.</param>
    /// <param name="args">The arguments to the Message.</param>
    /// <seealso cref="Print"/>
    /// 
    void Message(string symtag, params string[] args);


    /// <summary>
    /// Informs .NET/Link that your code will be manually sending a result back to <i>Mathematica</i>.
    /// </summary>
    /// <remarks>
    /// This circumvents the normal automatic return to <i>Mathematica</i> of whatever the method being called returns.
    /// <para>
    /// This method is usable only in .NET code that is invoked in a call from <i>Mathematica</i>, as described in Part 1
    /// of the .NET/Link User Guide. In other words, it is only used in code that is called from a <i>Mathematica</i>
    /// session via the "installable .NET" mechanism. Programmers who are launching the kernel and controlling it
    /// from a .NET program will have no use for this method.
    /// </para>
    /// The IKernelLink object on which this method will be called will probably be obtained via the
    /// <see cref="StdLink.Link">StdLink.Link</see> property.
    /// <para>
    /// The name "BeginManual" was chosen instead of, say, "SetManual" to emphasize that the link enters
    /// a special mode the moment this method is called. To allow the most graceful exception handling, you
    /// should delay calling BeginManual until right before you begin to write the result on the link.
    /// </para>
    /// </remarks>
    /// 
    void BeginManual();


    /// <summary>
    /// Tells whether the user has attempted to abort the computation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method is usable only in .NET code that is invoked in a call from <i>Mathematica</i>, as described in Part 1
    /// of the .NET/Link User Guide. In other words, it is only used in code that is called from a <i>Mathematica</i>
    /// session via the "installable .NET" mechanism. Programmers who are launching the kernel and controlling it
    /// from a .NET program will have no use for this method.
    /// </para>
    /// The IKernelLink object on which this method will be called will probably be obtained via the
    /// <see cref="StdLink.Link">StdLink.Link</see> property.
    /// <para>
    /// When the user tries to interrupt a <i>Mathematica</i> computation that is in the middle of a call
    /// into .NET, the interrupt request is sent to .NET. If a .NET method makes no attempt to
    /// honor interrupt requests, then after the method call completes .NET/Link will execute the
    /// <i>Mathematica</i> function Abort[], causing the entire <i>Mathematica</i> computation to end and return the result
    /// $Aborted. If you want to detect interrupts <i>within</i> a .NET method, for example to break out of
    /// a long .NET computation, call WasInterrupted to determine if an interrupt request has
    /// been received. If it returns true, then you can simply return from your method, and .NET/Link
    /// will take care of calling Abort[] for you. If your method returns a value, the value will
    /// be ignored. For example, you could put code like the following into a time-intensive loop
    /// you were running:
    /// </para>
    /// <code>
    /// IKernelLink ml = StdLink.Link;
    /// if (ml.WasInterrupted)
    ///     return;
    /// </code>
    /// If you want to do something other than call Abort[] in response to the interrupt you should
    /// call BeginManual, send back a result manually, and then set <c>WasInterrupted = false</c>. Setting it to
    /// false tells .NET/Link that you have handled the interrupt manually and therefore .NET/Link should not
    /// try to send back Abort[]:
    /// <code>
    /// IKernelLink ml = StdLink.Link;
    /// if (ml.WasInterrupted) {
    ///     ml.BeginManual();
    ///     ml.Put("Interrupted at iteration " + i);
    ///     ml.WasInterrupted = false;
    ///     return;
    /// }
    /// </code>
    /// </remarks>
    /// <returns>Whether the <i>Mathematica</i> user tried to interrupt this computation.</returns>
    /// 
    bool WasInterrupted {
        get;
        set;
    }


    /********************************************  Event  ***************************************/

    /// <summary>
    /// Occurs when a MathLink packet arrives.
    /// </summary>
    /// <remarks>
    /// Normal .NET/Link programs do not implement their own "packet loop", repeatedly calling NextPacket to read each
    /// packet and decide how to handle it. Instead, they use higher-level methods that handle the packet loop internally.
    /// These methods are WaitForAnswer, WaitAndDiscardAnswer, EvaluateToInputForm, EvaluateToOutputForm, EvaluateToImage,
    /// and EvaluateToTypeset. The internal packet loop discards all packets other than those that contain the result
    /// of the evaluation.
    /// <para>
    /// Sometimes, however, you want to see the intermediate packets that get sent. Examples of such packets are TextPackets
    /// containing output from <i>Mathematica</i>'s Print function, and MessagePackets containing <i>Mathematica</i> warning messages.
    /// Seeing warning messages can be especially useful.
    /// </para>
    /// Rather than forcing you to write your own packet loop, .NET/Link fires the PacketArrived event every time a packet
    /// arrives from <i>Mathematica</i>. You can install one or more handlers for this event and be able to inspect every packet
    /// that is sent.
    /// <para>
    /// Your event handler method(s) can consume or ignore the packet without affecting the internal packet loop in any way.
    /// You won't interfere with anything whether you read none, some, or all of the packet contents. 
    /// </para>
    /// At the point that PacketArrived is fired, the packet has already been "opened" with NextPacket, so your handler can
    /// begin reading the packet contents immediately.
    /// <para>
    /// The PacketHandler event handler is passed two arguments: the link and the packet type.
    /// </para>
    /// Very advanced programmers can optionally indicate that the internal packet loop should not see the packet. This
    /// is done by returning false from your handler method. Normally, you will return true.
    /// <para>
    /// <example>
    /// Here is an example of installing a PacketArrived event handler that prints out the contents of
    /// all incoming packets. This can be very useful for debugging. In your program, add a line like this:
    /// <code>
    /// // C#
    /// ml.PacketArrived += new PacketHandler(PacketPrinterMethod);
    /// 
    /// // VB
    /// AddHandler ml.PacketArrived, AddressOf PacketPrinterMethod
    /// </code>
    /// Elsewhere, the definition of PacketPrinterMethod:
    /// <code>
    /// // C#
    /// public static bool PacketPrinterMethod(IKernelLink ml, PacketType pkt) {
    ///     Console.WriteLine("Packet of type {1} arrived. Its contents are: {2}", pkt, ml.PeekExpr());
    ///     return true;
    /// }
    /// 
    /// // VB
    /// Public Shared Function PacketPrinterMethod(ByVal ml As IKernelLink, ByVal pkt As PacketType) As Boolean
    ///     Console.WriteLine("Packet of type {1} arrived. Its contents are: {2}", pkt, ml.PeekExpr())
    ///     Return True
    /// End Function
    /// </code>
    /// </example>
    /// </para>
    /// </remarks>
    /// 
    event PacketHandler PacketArrived;
    
}

/// <summary>
/// Represents the method that will handle the PacketArrived event.
/// </summary>
/// <remarks>
/// See the discussion for the <see cref="IKernelLink.PacketArrived"/> event for more information.
/// </remarks>
/// <param name="ml">The link on which the packet arrived.</param>
/// <param name="pkt">The type of packet.</param>
/// <returns>You should always return true.</returns>
/// <seealso cref="IKernelLink.PacketArrived"/>
/// 
public delegate bool PacketHandler(IKernelLink ml, PacketType pkt);


// This is an NDOC trick. Define a class with the special name NamespaceDoc and 
// the summary for that class will be used on the summary page for the namespace.
/// <summary>
/// The main .NET/Link namespace. Looking for a place to start?
/// Try the <a href="Wolfram.NETLink.IKernelLink.html">IKernelLink</a> interface.
/// </summary>
class NamespaceDoc {}


}