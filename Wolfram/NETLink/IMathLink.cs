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

namespace Wolfram.NETLink {

/// <summary>
/// IMathLink is the low-level interface that is the root of all link objects in .NET/Link.
/// The methods in IMathLink correspond roughly to a subset of those in the C-language MathLink API.
/// </summary>
/// <remarks>
/// Most programmers will deal instead with objects of type <see cref="IKernelLink"/>, a higher-level interface
/// that extends IMathLink and incorporates the assumption that the program on the other side of the
/// link is a <i>Mathematica</i> kernel.
/// <para>
/// You create objects of type IMathLink via the <see cref="MathLinkFactory.CreateMathLink"/> method. Again, though,
/// most programmers will use IKernelLink instead of IMathLink.
/// </para>
/// Most IMathLink methods throw a MathLinkException if a link-related error occurs. Examples
/// would be calling EndPacket before sending a complete expression, or calling GetFunction
/// when an integer is waiting on the link.
/// <para>
/// For additional information about these methods, see the .NET/Link User Guide, and also
/// the MathLink documentation in the <i>Mathematica</i> book. Most of these methods are
/// substantially similar, if not identical, to their C counterparts as documented in
/// the book.
/// </para>
/// </remarks>
/// <seealso cref="IKernelLink"/>
/// <seealso cref="MathLinkFactory"/>
/// 
[type:CLSCompliant(true)]
public interface IMathLink {


    /// <summary>
    /// Closes the link.
    /// </summary>
    /// <remarks>
    /// Always call Close on every link when you are done using it.
    /// </remarks>
    /// 
    void Close();


    /// <overloads>
    /// <summary>
    /// Connects the link, if it has not already been connected.
    /// </summary>
    /// <remarks>
    /// There is a difference between opening a link (which is what the MathLinkFactory methods
    /// CreateMathLink and CreateKernelLink do) and connecting it, which verifies that it is alive and
    /// ready for data transfer.
    /// <para>
    /// All the methods that read from the link will connect it if necessary. The Connect method
    /// lets you deliberately control the point in the program where the connection occurs,
    /// without having to read anything.
    /// </para>
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Waits for the link to be connected.
    /// </summary>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Connect();


    /// <summary>
    /// Waits for the link to be connected for at most the specified number of milliseconds before throwing a MathLinkException.
    /// </summary>
    /// <param name="timeoutMillis">Number of milliseconds to wait before throwing an exception</param>
    /// <exception cref="MathLinkException">On any MathLink error, or if the timeout passes.</exception>
    /// 
    void Connect(long timeoutMillis);


    /// <summary>
    /// Discards the current packet, if it has been partially read. Has no effect if the previous packet was fully read.
    /// </summary>
    /// <remarks>
    /// This is a useful cleanup function. You can call it when you are finished examining the contents of a packet
    /// that was opened with <see cref="NextPacket"/> or <see cref="IKernelLink.WaitAndDiscardAnswer">WaitAndDiscardAnswer</see>,
    /// whether you have read the entire packet contents or not. You can be sure that the link is then in a state
    /// where you are ready to read the next packet.
    /// <para>
    /// It is also frequently used in a catch block for a MathLinkException, to clear off
    /// any unread data in a packet before returning to the normal program flow.
    /// </para>
    /// </remarks>
    /// <seealso cref="NextPacket"/>
    /// 
    void NewPacket();


    /// <summary>
    /// "Opens" the next packet arriving on the link.
    /// </summary>
    /// <remarks>
    /// It is an error to call NextPacket while the current packet has unread data; use <see cref="NewPacket"/>
    /// to discard the current packet first.
    /// <para>
    /// Most programmers will use this method rarely, if ever. .NET/Link provides higher-level
    /// functions in the <see cref="IKernelLink"/> interface that hide these low-level details of the packet loop.
    /// </para>                                      
    /// </remarks>
    /// <exception cref="MathLinkException">On any MathLink error, but typically because you have not finished reading
    /// the entire contents of the previous packet.
    /// </exception>
    /// 
    PacketType NextPacket();


    /// <summary>
    /// Call when you are finished writing the contents of a single packet.
    /// </summary>
    /// <remarks>
    /// Calling EndPacket is not strictly necessary, but it is good style, and it allows .NET/Link to immediately
    /// generate a MathLinkException if you are not actually finished with writing the data you promised to send.
    /// </remarks>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    ///
    void EndPacket();


    /// <summary>
    /// Clears the link error condition, if possible.
    /// </summary>
    /// <remarks>
    /// After an error has occurred, and a MathLinkException has been caught, you must call ClearError
    /// before doing anything else with the link. If it returns false, there is an unfixable problem, and
    /// you should close the link.
    /// </remarks>
    /// <returns>Whether the error state could be cleared successfully.</returns>
    /// <seealso cref="Error"/>
    /// 
    bool ClearError();
    

    /// <summary>
    /// Immediately transmits any data buffered for sending over the link.
    /// </summary>
    /// <remarks>
    /// Any calls that read from the link will flush it, so you only need to call Flush manually if you want
    /// to make sure data is sent right away even though you are <i>not</i> reading from the link immediately.
    /// Calls to Ready will not flush the link, so if you are sending something and then polling Ready waiting
    /// for the result to arrive (as opposed to just calling NextPacket or WaitForAnswer), you must call Flush
    /// to ensure that the data is sent.
    /// </remarks>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Flush();


    /// <summary>
    /// Gives the type of the next element in the expression currently being read.
    /// </summary>
    /// <remarks>
    /// To check the type of a partially read element without advancing to the next element,
    /// use <see cref="GetExpressionType"/>.
    /// </remarks>
    /// <seealso cref="GetExpressionType"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    ///
    ExpressionType GetNextExpressionType();
                

    /// <summary>
    /// Gives the type of the current element in the expression currently being read.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="GetNextExpressionType"/>, GetExpressionType will not advance to the next
    /// element if the current element has only been partially read.
    /// </remarks>
    /// <seealso cref="GetNextExpressionType"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    ///
    ExpressionType GetExpressionType();
    

    /// <summary>
    /// Identifies the type of data element that is to be sent next.
    /// </summary>
    /// <remarks>
    /// PutNext is rarely needed. The two most likely uses are to put expressions whose heads are
    /// not mere symbols (e.g., Derivative[2][f]), or to put data in so-called "textual" form. Calls to PutNext must
    /// be followed by <see cref="PutSize"/> and <see cref="PutData"/>, or by <see cref="PutArgCount"/> for the
    /// ExpressionType.Function type.
    /// <para>
    /// Here is how you could send Derivative[2][f]:
    /// </para>
    /// <example>
    /// <code>
    /// ml.PutNext(ExpressionType.Function);  // The func we are putting has head Derivative[2], arg f
    /// ml.PutArgCount(1);                    // this 1 is for the 'f'
    /// ml.PutNext(ExpressionType.Function);  // The func we are putting has head Derivative, arg 2
    /// ml.PutArgCount(1);                    // this 1 is for the '2'
    /// ml.PutSymbol("Derivative");
    /// ml.Put(2);
    /// ml.PutSymbol("f");
    /// </code>
    /// </example>
    /// <para>
    /// Here is an example of sending an integer in "textual" form. This would be useful if you happened to have the
    /// digits of the integer in the form of an array of bytes rather than an int type:
    /// </para>
    /// <example>
    /// <code>
    /// byte[] digits = {(byte)'1', (byte)'2', (byte)'3'};
    /// ml.PutNext(ExpressionType.Integer);
    /// ml.PutSize(digits.Length);
    /// ml.PutData(digits);
    /// </code>
    /// </example>
    /// </remarks>
    /// <param name="type">The type of expression you will be sending.</param>
    /// <seealso cref="PutSize"/>
    /// <seealso cref="PutData"/>
    /// <seealso cref="PutArgCount"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void PutNext(ExpressionType type);
    

    /// <summary>
    /// Reads the argument count of an expression being read manually.
    /// </summary>
    /// <remarks>
    /// This method can be used after <see cref="GetNextExpressionType"/> or <see cref="GetExpressionType"/>
    /// returns the value ExpressionType.Function. The argument count is always followed by the head
    /// of the expression. The head is followed by the arguments; the argument count tells how many there will be.
    /// </remarks>
    /// <returns>The number of arguments of the function.</returns>
    /// <seealso cref="GetNextExpressionType"/>
    /// <seealso cref="GetExpressionType"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    int GetArgCount();
    

    /// <summary>
    /// Specifies the argument count for a composite expression being sent manually.
    /// </summary>
    /// <remarks>
    /// Use it after a call to <see cref="PutNext"/> with the ExpressionType.Function type.
    /// <para>
    /// See the example for <see cref="PutNext"/>.
    /// </para>
    /// </remarks>
    /// <param name="argCount">The number of arguments you will be sending.</param>
    /// <seealso cref="PutNext"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void PutArgCount(int argCount);    


    /// <summary>
    /// Specifies the size in bytes of an element being sent in textual form.
    /// </summary>
    /// <param name="size">
    /// The size of the data, in bytes, that will be written with the following one or more calls to <see cref="PutData"/>.
    /// </param>
    /// <seealso cref="PutNext"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void PutSize(int size);
    

    /// <summary>
    /// Used for sending elements in so-called "textual" form.
    /// </summary>
    /// <remarks>
    /// After calling <see cref="PutNext"/> and <see cref="PutSize"/>, a series of PutData calls are used to send the actual data.
    /// <para>
    /// The textual means of sending data is rarely used. Its main use is to allow a number to be sent that is larger
    /// than can fit in any native .NET numeric type. See the example for <see cref="PutNext"/>.
    /// </para>
    /// </remarks>
    /// <param name="data">The data to send.</param>
    /// <seealso cref="PutNext"/>
    /// <seealso cref="PutSize"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void PutData(byte[] data);
    

    /// <summary>
    /// Gets a specified number of bytes in the textual form of the expression currently being read.
    /// </summary>
    /// <remarks>
    /// You can use <see cref="BytesToGet"/> to determine if more GetData calls are needed to completely read
    /// the element.
    /// <para>
    /// The returned array will have a length of at most <paramref name="numRequested"/>.
    /// </para>
    /// </remarks>
    /// <param name="numRequested">The maximum number of bytes to read.</param>
    /// <seealso cref="BytesToGet"/>
    /// <seealso cref="GetNextExpressionType"/>
    /// <seealso cref="GetExpressionType"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    byte[] GetData(int numRequested);


    /// <summary>
    /// Gives the number of bytes that remain to be sent in the element that is currently being sent in textual form.
    /// </summary>
    /// <remarks>
    /// After you have called <see cref="PutSize"/>, the link knows how many bytes you have promised to send. This method lets you
    /// determine how many you still need to send.
    /// </remarks>
    /// <returns>The number of bytes that remain to be sent.</returns>
    /// <seealso cref="PutSize"/>
    /// <seealso cref="PutData"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    int BytesToPut();


    /// <summary>
    /// Gives the number of bytes that remain to be read in the element that is currently being read in textual form.
    /// </summary>
    /// <remarks>
    /// BytesToGet lets you keep track of your progress reading an element through a series of <see cref="GetData"/> calls.
    /// </remarks>
    /// <returns>The number of bytes that remain to be read in the current element.</returns>
    /// <seealso cref="GetData"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    int BytesToGet();


    /// <summary>
    /// Reads a <i>Mathematica</i> character string.
    /// </summary>
    /// <remarks>
    /// Because both .NET and <i>Mathematica</i> strings are in Unicode, the string as read is an exact
    /// match to its <i>Mathematica</i> representation.
    /// <para>
    /// Use <see cref="GetStringCRLF"/> if you need a string with the Windows newline convention.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetStringCRLF"/>
    /// <seealso cref="GetByteString"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    string GetString();


    /// <summary>
    /// Reads a <i>Mathematica</i> character string and translates newlines into Windows format.
    /// </summary>
    /// <remarks>
    /// <i>Mathematica</i> strings use a \n character (ASCII 10) for newlines. If you want to display a string read
    /// from <i>Mathematica</i> in a Windows text control, the string needs to have newlines in the \r\n Windows convention.
    /// This method reads the string and converts to the Windows newline convention.
    /// </remarks>
    /// <seealso cref="GetString"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    string GetStringCRLF();


    /// <summary>
    /// Reads a <i>Mathematica</i> symbol as a string.
    /// </summary>
    /// <remarks>
    /// Because both .NET strings and <i>Mathematica</i> symbols are in Unicode, the string as read is an exact
    /// match to its <i>Mathematica</i> representation.
    /// </remarks>
    /// <seealso cref="GetString"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    string GetSymbol();
    

    /// <summary>
    /// Sends a symbol.
    /// </summary>
    /// <remarks>
    /// Both .NET strings and <i>Mathematica</i> symbols are in Unicode, so you can send symbols with the full
    /// Unicode character set.
    /// </remarks>
    /// <seealso cref="Put"/>
    /// <seealso cref="Put"/>
    /// <param name="s">The name of the symbol to send.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void PutSymbol(string s);
    

    /// <summary>
    /// Reads a <i>Mathematica</i> string as an array of bytes.
    /// </summary>
    /// <remarks>
    /// In contrast with <see cref="GetString"/>, this method strips the incoming (16-bit Unicode) character data into a
    /// single-byte representation. Characters that cannot be represented faithfully in single-byte form are replaced
    /// by the byte specified by the <paramref name="missing"/> parameter. This method is primarily useful if you know
    /// the incoming data contains only ASCII characters and you want the data in the form of a byte array.
    /// </remarks>
    /// <param name="missing">The byte to use in place of non-ASCII characters (i.e., 16-bit characters).</param>
    /// <seealso cref="GetString"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    byte[] GetByteString(int missing);


    /// <summary>
    /// Reads the <i>Mathematica</i> symbols True or False as a bool.
    /// </summary>
    /// <remarks>
    /// It returns true if the symbol True is read, and false if False (or any other non-True symbol) is read.
    /// If you want to make sure that either True or False is on the link, don't use GetBoolean; instead, read
    /// the symbol with <see cref="GetSymbol"/> and test its value yourself.
    /// </remarks>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    bool GetBoolean();
        

    /// <summary>
    /// Reads a <i>Mathematica</i> integer as a 32-bit integer.
    /// </summary>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    int GetInteger();
        

    /// <summary>
    /// Reads a <i>Mathematica</i> real number or integer as a double.
    /// </summary>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    double GetDouble();
    

    /// <summary>
    /// Reads a <i>Mathematica</i> integer or real number or integer as a decimal.
    /// </summary>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    decimal GetDecimal();
    

    /// <overloads>
    /// <summary>
    /// Sends a .NET type as its <i>Mathematica</i> representation.
    /// </summary>
    /// <remarks>
    /// Put sends .NET numbers, strings, bools, arrays, and objects.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Sends a bool value as the <i>Mathematica</i> symbol True or False.
    /// </summary>
    /// <param name="b">The boolean value to send.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Put(bool b);
    

    /// <summary>
    /// Sends an integer value.
    /// </summary>
    /// <param name="i">The int value to send.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Put(int i);
    

    /// <summary>
    /// Sends a long integer value.
    /// </summary>
    /// <param name="i">The long value to send.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    // This is here only because compiler cannot resolve Put(long) unambiguously to Put(double) or Put(decimal).
    void Put(long i);
    

    /// <summary>
    /// Sends a double value.
    /// </summary>
    /// <param name="d">The double value to send.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Put(double d);
    

    /// <summary>
    /// Sends a decimal value as an integer or real.
    /// </summary>
    /// <param name="d">The decimal value to send.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Put(decimal d);
    

    /// <summary>
    /// Sends an object, including strings and arrays.
    /// </summary>
    /// <remarks>
    /// Only a limited set of .NET objects can be usefully sent across a link with this method. These are
    /// objects whose "values" have a meaningful representation in <i>Mathematica</i>:
    /// <list type="table">
    /// <listheader><term>Type</term><description>Sent As:</description></listheader>
    /// <item><term>null</term><description>the symbol Null</description></item>
    /// <item><term>string</term><description><i>Mathematica</i> string</description></item>
    /// <item><term>array</term><description>list of the appropriate dimensions</description></item>
    /// <item><term>boxed primitive (<see cref="Int32"/>, <see cref="Boolean"/>, etc.)</term><description>the unboxed value</description></item>
    /// <item><term><see cref="Expr"/></term><description>expression</description></item>
    /// <item><term>Complex class</term><description>Complex number</description></item>
    /// </list>
    /// All other objects have no meaningful "value" representation in <i>Mathematica</i>. For these objects, the relatively
    /// useless obj.ToString() is sent. The <see cref="IKernelLink.Put">version of Put in the IKernelLink interface</see>,
    /// which is the interface most programmers will be using, will put objects "by reference" if they have no meaningful
    /// value representation in <i>Mathematica</i>, meaning that they show up as NETObject expressions.
    /// </remarks>
    /// <param name="obj">The object to send.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Put(object obj);


    /// <summary>
    /// Sends an array object. Unlike Put(object), this method lets you specify the heads you want for each dimension.
    /// </summary>
    /// <example>
    /// <code>
    /// int[,] a = {{1,2},{3,4}};
    /// // The following are equivalent, sending to <i>Mathematica</i> the matrix: {{1,2},{3,4}}
    /// ml.Put(a);
    /// ml.Put(a, null);
    /// ml.Put(a, new String[] {"List", "List"});
    /// // The following sends the expression: foo[bar[{1,2],[3,4]]
    /// ml.Put(a, new String[] {"foo", "bar"});
    /// </code>
    /// </example>
    /// <param name="obj">The array object to send.</param>
    /// <param name="heads">The heads in each dimension".</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void Put(Array obj, string[] heads);


    /// <summary>
    /// Reads a list as a one-dimensional array of bools.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of the symbols True and False.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[False, True].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    bool[] GetBooleanArray();
        

    /// <summary>
    /// Reads a list as a one-dimensional array of bytes.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of integers. Values outside
    /// the range of a byte are converted via casting.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[1,2].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    byte[] GetByteArray();


    /// <summary>
    /// Reads a list as a one-dimensional array of chars.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of integers. Values outside
    /// the range of a char are converted via casting.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[1,2].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    char[] GetCharArray();


    /// <summary>
    /// Reads a list as a one-dimensional array of shorts.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of integers. Values outside
    /// the range of a short are converted via casting.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[1,2].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    short[] GetInt16Array();


    /// <summary>
    /// Reads a list as a one-dimensional array of ints.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of integers. Values outside
    /// the range of an int are converted via casting.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[1,2].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    int[] GetInt32Array();


    /// <summary>
    /// Reads a list as a one-dimensional array of longs.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of integers. Values outside
    /// the range of a long are converted via casting.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[1,2].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    long[] GetInt64Array();


    /// <summary>
    /// Reads a list as a one-dimensional array of floats.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of integers or reals. Values outside
    /// the range of a float are converted via casting.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[1.0, 2.0].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    float[] GetSingleArray();


    /// <summary>
    /// Reads a list as a one-dimensional array of doubles.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of integers or reals. Values outside
    /// the range of a double are converted via casting.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[1.0, 2.0].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    double[] GetDoubleArray();


    /// <summary>
    /// Reads a list as a one-dimensional array of decimals.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of integers or reals. Values outside
    /// the range of a decimal are converted via casting.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo[1.0, 2.0].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    decimal[] GetDecimalArray();


    /// <summary>
    /// Reads a list as a one-dimensional array of strings.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of strings.
    /// <para>
    /// The expression does not need to have head List. In other words, it could be Foo["Abc", "Def"].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </para>
    /// </remarks>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    string[] GetStringArray();


    /// <summary>
    /// Reads a list as a one-dimensional array of complex numbers.
    /// </summary>
    /// <remarks>
    /// The expression being read must be a list or other depth-1 expression of Complex.
    /// <para>
    /// This method will read the expression, but return null, if no .NET class has yet been
    /// established to use for complex numbers by setting the <see cref="ComplexType"/> property.
    /// </para>
    /// The expression does not need to have head List. In other words, it could be Foo[I, 2-I].
    /// The information about the head is lost; if you need this information you can either use
    /// <see cref="GetArray">GetArray(Type leafType, int depth, out string[] heads)</see>, or read the
    /// expression as an <see cref="Expr"/> and examine it using the Expr methods.
    /// </remarks>
    /// <seealso cref="ComplexType"/>
    /// <seealso cref="GetArray"/>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    object[] GetComplexArray();
    

    /// <overloads>
    /// <summary>
    /// Reads an array of the specified type and depth.
    /// </summary>
    /// <remarks>
    /// Use this method when you want to read a 2-dimensional or deeper array. You will typically cast
    /// the result of this method to the desired array type, as in:
    /// <code>
    /// // C#
    /// int[,,] result = (int[,,]) ml.GetArray(typeof(int), 3);
    ///    
    /// // VB
    /// Dim result(,,) as Integer = CType(ml.GetArray(GetType(Integer), 3), GetType(Integer(,,)))
    /// </code>
    /// The expression does not need to have head List. It can have any heads, at any depth,
    /// and the heads do not have to agree at each depth, meaning that you could read the
    /// expression {foo[1, 2], bar[3, 4]} as an int[,].
    /// <para>
    /// This method only works for arrays that are rectangular, not jagged. In other words, you can
    /// read a <i>Mathematica</i> list like this: {{1,2,3},{4,5,6}} but not like this: {{1,2,3},{4,5}}.
    /// </para>
    /// For reading one-dimensional arrays, there are convenience functions for all the simple types, such
    /// as <see cref="GetInt32Array"/>, <see cref="GetBooleanArray"/>, <see cref="GetStringArray"/>, and so on.
    /// These methods are typed to return the specified type of array, so no casting is required.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Reads an array and discards information about the heads at each level.
    /// </summary>
    /// <remarks>
    /// The information about the heads is lost; if you need this information you can use the alternative
    /// overload GetArray(int type, int depth, out string[] heads), or read the expression as an
    /// <see cref="Expr"/> and examine it using the Expr methods.
    /// </remarks>
    /// <param name="leafType">The type of the leaf elements of the returned array.</param>
    /// <param name="depth">The requested depth.</param>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    Array GetArray(Type leafType, int depth);


    /// <summary>
    /// Reads an array and records information about the heads at each level.
    /// </summary>
    /// <remarks>
    /// This method does not enforce a requirement that the heads be identical across a level. If the expression looks
    /// like {foo[1, 2], bar[3, 4]} then the <paramref name="heads"/> array would become {"List", "foo"}, ignoring the
    /// fact that foo was not the head of every subexpression at level 1. In other words, if heads[i] is "foo", then it
    /// is only guaranteed that <i>the first</i> expression at level i had head foo, not that <i>all</i> of them did.
    /// If you want to be absolutely sure about the heads of every subpart, read the expression as an Expr and use the
    /// Expr methods to inspect it.
    /// </remarks>
    /// <param name="leafType">The type of the leaf elements of the returned array.</param>
    /// <param name="depth">The requested depth.</param>
    /// <param name="heads">Gets the heads at each level.</param>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    Array GetArray(Type leafType, int depth, out string[] heads);


    /// <summary>
    /// Reads a function name and argument count.
    /// </summary>
    /// <param name="argCount">Gets the argument count of the function.</param>
    /// <returns>The name of the function.</returns>
    /// <exception cref="MathLinkException">If the waiting data is not the head of a function, or on any other MathLink error.</exception>
    /// 
    string GetFunction(out int argCount);


    /// <summary>
    /// Sends a function name and argument count.
    /// </summary>
    /// <remarks>
    /// Follow this with calls to put the arguments.
    /// </remarks>
    /// <param name="f">The function name.</param>
    /// <param name="argCount">The number of arguments to follow.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void PutFunction(string f, int argCount);


    /// <summary>
    /// Sends a function name and its arguments.
    /// </summary>
    /// <remarks>
    /// This method is a convenient way to send a function with its arguments in a single call.
    /// <para>
    /// The arguments are sent as they would be if you used the <see cref="Put"/> method on each one individually.
    /// </para>
    /// <code>
    /// // This sends 1+2+3+4
    /// ml.PutFunctionAndArgs("Plus", 1, 2, 3, 4); 
    /// </code>
    /// In languages other than C# or VB.NET that do not support variable numbers of arguments, the args
    /// must be packaged into an array:
    /// <code>
    /// ml.PutFunctionAndArgs("Plus", new int[]{1, 2, 3, 4}); 
    /// </code>
    /// </remarks>
    /// <param name="f">The function name.</param>
    /// <param name="args">The arguments to the function.</param>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void PutFunctionAndArgs(string f, params object[] args);


    /// <summary>
    /// Reads a function name and argument count and requires that it match the specified function name.
    /// </summary>
    /// <remarks>
    /// If the incoming function is not the specified one, a MathLinkException is thrown.
    /// <para>
    /// This method is similar to <see cref="GetFunction"/> in that it reads the name and argument count off the link.
    /// Use it in situations where you want an error to occur if the function is not what you expect.
    /// </para>
    /// </remarks>
    /// <param name="f">The function name that you are expecting.</param>
    /// <returns>The argument count of the incoming function.</returns>
    /// <seealso cref="CheckFunctionWithArgCount"/>
    /// <seealso cref="GetFunction"/>
    /// <exception cref="MathLinkException">If the data waiting on the link is not the named function, or on any other MathLink error.</exception>
    /// 
    int CheckFunction(string f);


    /// <summary>
    /// Reads a function name and argument count and requires that it match the specified function name and arg count.
    /// </summary>
    /// <remarks>
    /// If the incoming function is not the specified one, or it does not have the specified number of arguments,
    /// a MathLinkException is thrown.
    /// <para>
    /// This method is similar to <see cref="GetFunction"/> in that it reads the name and argument count off the link.
    /// Use it in situations where you want an error to occur if the function and argument count are not what you expect.
    /// </para>
    /// </remarks>
    /// <param name="f">The function name that you are expecting.</param>
    /// <param name="argCount">The argument count that you are expecting.</param>
    /// <seealso cref="CheckFunction"/>
    /// <seealso cref="GetFunction"/>
    /// <exception cref="MathLinkException">If the data waiting on the link is not the named function with the named arg count, or on any other MathLink error.</exception>
    /// 
    void CheckFunctionWithArgCount(string f, int argCount);
    

    /// <summary>
    /// Reads a complex number. This can be an integer, real, or a <i>Mathematica</i> expression with head Complex.
    /// </summary>
    /// <remarks>
    /// You must first designated a class to be used for complex numbers using the <see cref="ComplexType"/> property.
    /// </remarks>
    /// <seealso cref="ComplexType"/>
    /// <returns>
    /// The complex number as an instance of the class previously designated using the  <see cref="ComplexType"/>
    /// property, or null if no class has been designated.
    /// </returns>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    object GetComplex();
    

    /// <summary>
    /// Reads a single expression off the link and returns an appropriate object.
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
    /// <item><term>String</term><description><see cref="String"/></description></item>
    /// <item><term>Complex number</term><description>Complex type</description></item>
    /// <item><term>Function</term><description><see cref="Array"/></description></item>
    /// </list>
    /// Note that all incoming functions are read as an Array.
    /// <para>
    /// The <see cref="IKernelLink"/> interface overrides GetObject to allow object references to be read (that is,
    /// NETObject expressions arriving from <i>Mathematica</i> are read as the objects they refer to). 
    /// </para>
    /// This method is convenient in some circumstances because it lets .NET/Link figure out the correct way to read whatever
    /// expression is waiting. For example, you may simply want to discard the expression, or you may prefer to use mechanisms
    /// in your programming language to determine what type it is (like the C# <c>is</c> operator) rather than MathLink mechanisms.
    /// Another example is if you will be passing the expression to a method typed to take a generic Array, you can use GetObject
    /// to read any <i>Mathematica</i> array (any type, any depth).
    /// </remarks>
    /// <exception cref="MathLinkException">If the waiting data cannot be read in this format, or on any other MathLink error.</exception>
    /// 
    object GetObject();


    /// <summary>
    /// Reads a complete expression from the named link and writes it to this link.
    /// </summary>
    /// <remarks>
    /// This is an exceedingly fast way to move data from one link to another. It is often used to read data
    /// from a link and store it on a loopback link.
    /// </remarks>
    /// <param name="source">The link to read from.</param>
    /// <seealso cref="ILoopbackLink"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void TransferExpression(IMathLink source);
    

    /// <summary>
    /// Reads the entire contents of a loopback link and writes it to this link.
    /// </summary>
    /// <param name="source">The <see cref="ILoopbackLink"/> to read from.</param>
    /// <seealso cref="TransferExpression"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    void TransferToEndOfLoopbackLink(ILoopbackLink source);
    

    /// <summary>
    /// Reads an arbitrary expression from the link and creates an <see cref="Expr"/> from it.
    /// </summary>
    /// <remarks>
    /// The returned Expr can be examined and manipulated later.
    /// </remarks>
    /// <seealso cref="Expr"/>
    /// <seealso cref="PeekExpr"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    Expr GetExpr();
    

    /// <summary>
    /// Creates an <see cref="Expr"/> from the current expression, but does not drain it off the link.
    /// </summary>
    /// <remarks>
    /// This method is like <see cref="GetExpr"/>, but PeekExpr does not actually remove anything from the link. In other
    /// words, it leaves the link in the same state it was in before PeekExpr was called. It is useful for examining
    /// the next expression on the link without actually consuming it. That means you can insert a line like this in
    /// your program without disturbing your other code for reading the data:
    /// <code>
    /// Console.WriteLine("The next expression is: " + ml.PeekExpr());
    /// </code>
    /// </remarks>
    /// <seealso cref="Expr"/>
    /// <seealso cref="GetExpr"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    Expr PeekExpr();
    

    /// <summary>
    /// Sends a low-level MathLink message.
    /// </summary>
    /// <remarks>
    /// Do not confuse this type of message, used mainly for communicating requests to interrupt or abort
    /// computations, with <i>Mathematica</i> warning messages, which are unrelated.
    /// <para>
    /// To abort a <i>Mathematica</i> computation, use PutMessage(MathLinkMessage.Abort). If a computation
    /// is successfully aborted, it will return the symbol $Aborted.
    /// </para>
    /// Few programmers will use this method directly, as they will be working with the higher-level
    /// <see cref="IKernelLink"/> interface, which has methods for aborting and interrupting computations.
    /// </remarks>
    /// <param name="msg">The message to send; will be one of MathLinkMessage.Abort or MathLinkMessage.Interrupt.</param>
    /// <seealso cref="MathLinkMessage"/>
    /// 
    void PutMessage(MathLinkMessage msg);
    

    /// <summary>
    /// Creates a mark at the current point in the incoming MathLink data stream.
    /// </summary>
    /// <remarks>
    /// Marks can returned to later, to re-read data. A common use is to create a mark, call some method for reading
    /// data, and if a MathLinkException is thrown, seek back to the mark and try a different method of reading the data.
    /// <para>
    /// If you create a mark, be sure to call DestroyMark to destroy it, or you will create a memory leak.
    /// </para>
    /// <example>
    /// One common reason to use a mark is if you want to examine an incoming expression and branch to different code
    /// depending on some property of the expression. You want the code that actually handles the expression to see the
    /// entire expression, but you will need to read at least a little bit of the expression to decide how it must be
    /// handled (perhaps just calling GetFunction to see the head). Here is a code fragment demonstrating this technique:
    /// <code>
    /// string head = null;
    /// ILinkMark mark = ml.CreateMark();
    /// try {
    ///     int argc;
    ///     head = ml.GetFunction(out argc);
    ///     ml.SeekMark(mark);
    /// } finally {
    ///     ml.DestroyMark(mark);
    /// }
    /// if (head == "foo")
    ///     readAndHandleFoo(ml);
    /// else if (head == "bar")
    ///     readAndHandleBar(ml);
    /// </code>
    /// Note that we use try/finally to ensure that the mark is destroyed even if an exception is thrown.
    /// </example>
    /// <para>
    /// Some of the usefulness of marks in the C-language MathLink API is obviated by .NET/Link's Expr class.
    /// </para>
    /// </remarks>
    /// <seealso cref="SeekMark"/>
    /// <seealso cref="DestroyMark"/>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    ///
    ILinkMark CreateMark();
    

    /// <summary>
    /// Resets the current position in the incoming MathLink data stream to an earlier point.
    /// </summary>
    /// <remarks>
    /// See the example in <see cref="CreateMark"/>.
    /// </remarks>
    /// <param name="mark">The mark, created by <see cref="CreateMark"/>, that identifies the desired position to reset to.</param>
    /// <seealso cref="CreateMark"/>
    /// <seealso cref="DestroyMark"/>
    /// 
    void SeekMark(ILinkMark mark);


    /// <summary>
    /// Destroys a mark.
    /// </summary>
    /// <remarks>
    /// Always call DestroyMark on any marks you create with CreateMark. See the example in <see cref="CreateMark"/>.
    /// </remarks>
    /// <param name="mark">The mark to destroy.</param>
    /// <seealso cref="CreateMark"/>
    /// <seealso cref="SeekMark"/>
    /// 
    void DestroyMark(ILinkMark mark);


    /// <summary>
    /// A low-level function that retrieves special internal information from the MathLink device.
    /// </summary>
    /// <remarks>
    /// Some MathLink devices (devices are software components that implement the actual protocol for sending and
    /// receiving data, such as the MathLink TCP device) can be queried for internal information.
    /// Very few programmers will ever call this. It is provided mainly for diagnostics.
    /// </remarks>
    /// 
    void DeviceInformation(int selector, IntPtr buffer, ref int bufLen);


    /********************************************  Properties  ***************************************/

    /// <summary>
    /// Gets the current error state for the link.
    /// </summary>
    /// <remarks>
    /// The actual integer code returned is probably not very useful to most programmers, except to note that 0 means
    /// "no error" and anything else means there was an error.
    /// <para>
    /// You can use <see cref="ErrorMessage"/> to get a readable string describing the error.
    /// </para>
    /// </remarks>
    /// <returns>The error code; will be 0 if no error.</returns>
    /// <seealso cref="ErrorMessage"/>
    /// <seealso cref="ClearError"/>
    /// 
    int Error {
        get;
    }
    

    /// <summary>
    /// Gets a textual message describing the current error state for the link.
    /// </summary>
    /// <remarks>
    /// The message provides a brief description if there is an error.
    /// </remarks>
    /// <returns>The error message.</returns>
    /// <seealso cref="Error"/>
    /// <seealso cref="ClearError"/>
    /// 
    string ErrorMessage {
        get;
    }
    

    /// <summary>
    /// Indicates whether the link has data waiting to be read.
    /// </summary>
    /// <remarks>
    /// Ready tells you whether the next call that reads data will block or not.
    /// </remarks>
    /// <exception cref="MathLinkException">On any MathLink error.</exception>
    /// 
    bool Ready {
        get;
    }


    /// <summary>
    /// Sets or gets the class that you want to map to <i>Mathematica</i>'s Complex numbers.
    /// </summary>
    /// <remarks>
    /// After setting ComplexType, you can use <see cref="GetComplex"/> to read an incoming integer, real, or Complex number
    /// as instance of your class, and you can use <see cref="Put"/> to send objects of your class to <i>Mathematica</i> as Complex.
    /// <para>
    /// To be suitable, the Type you specify must have have appropriate members. It must have each of the following:
    /// </para>
    ///     A constructor with one of these signatures:
    ///     <code>
    ///         (double re, double im)
    ///         (float re, float im)
    ///     </code>
    ///     A method with one of the following signatures:
    ///     <code>
    ///         double Re()
    ///         double Real()
    ///         float Re()
    ///         float Real()
    ///     </code>
    ///     OR, a property or field:
    ///     <code>
    ///         double Re
    ///         double Real
    ///         float Re
    ///         float Real
    ///     </code>
    ///     A method with one of the following signatures:
    ///     <code>
    ///         double Im()
    ///         double Imag()
    ///         double Imaginary()
    ///         float Im()
    ///         float Imag()
    ///         float Imaginary()
    ///     </code>
    ///     OR, a property or field:
    ///     <code>
    ///         double Im
    ///         double Imag
    ///         double Imaginary
    ///         float Im
    ///         float Imag
    ///         float Imaginary
    ///     </code>
    /// </remarks>
    /// <seealso cref="GetComplex"/>
    /// <exception cref="ArgumentException">If the type does not have appropriate members as described above.</exception>
    /// 
    Type ComplexType {
        get;
        set;
    }

    /// <summary>
    /// Gets the name of the link.
    /// </summary>
    /// <remarks>
    /// The name of the link is generally not useful if the link is opened in "launch" mode
    /// (where one program launches another via MathLink and attaches to it--for example when a
    /// .NET program launches the kernel). You might want to get the name if you open a link
    /// in listen mode, because in that case you need to tell the other program what link name
    /// to connect to.
    /// <para>
    /// The name of the link is dependent on the protocol used. Using the Shared Memory protocol
    /// on Windows, the name might be just a short string of meaningless characters, whereas with the
    /// TCP or TCPIP protocols it will include a port number and machine name, such as
    /// 1234@machine.domain.com.
    /// </para>
    /// </remarks>
    /// 
    string Name {
        get;
    }


    /********************************************  Events  ***************************************/

    /// <summary>
    /// Occurs periodically when the link is blocking in a reading call.
    /// </summary>
    /// <remarks>
    /// The Yield event is raised at unspecified intervals (but generally many times a second) while
    /// the link is blocked in a "Get" call waiting for data to arrive, or a "Put" call waiting for
    /// buffer space to free up inside MathLink.
    /// <para>
    /// In a single-threaded program, a handler for the Yield event can be used as a place to perform
    /// actions necessary to keep the program responsive (like calling Application.DoEvents()).
    /// For most programs, a better choice is to use multiple threads, so that your user interface thread
    /// is not blocked waiting for the result of a <i>Mathematica</i> computation.
    /// </para>
    /// The Yield handler is passed no arguments. It should return true or false to indicate whether to back
    /// out of the read call or not. Returning true means to back out, which will cause a MathLinkException
    /// to be thrown in the Get call that is currently blocked waiting for data.
    /// </remarks>
    /// 
    event YieldFunction Yield;


    /// <summary>
    /// Occurs when a low-level MathLink message arrives.
    /// </summary>
    /// <remarks>
    /// Do not confuse this type of message, used mainly for communicating requests to interrupt or
    /// abort a computation, with <i>Mathematica</i> warning and error messages, which are unrelated.
    /// <para>
    /// The MessageArrived event handler is passed a single argument of type MathLinkMessage that
    /// indicates the type of message. The event handler is called on a separate thread than the application's
    /// main thread (this thread is created by MathLink and not by your program).
    /// </para>
    /// Do not attempt to use the link from within your event handler. 
    /// <para>
    /// Few programmers will need to use this event.
    /// </para>
    /// </remarks>
    /// 
    event MessageHandler MessageArrived;
}


/// <summary>
/// Represents the method that will handle the Yield event.
/// </summary>
/// <returns>true to force the currently blocked reading call to back out and throw a MathLinkException; false to continue waiting.</returns>
/// <seealso cref="Wolfram.NETLink.IMathLink.Yield"/>
/// 
public delegate bool YieldFunction();


/// <summary>
/// Represents the method that will handle the MessageArrived event.
/// </summary>
/// <param name="msgType">The type of message that arrived.</param>
/// <seealso cref="Wolfram.NETLink.IMathLink.MessageArrived"/>
/// 
public delegate void MessageHandler(MathLinkMessage msgType);

}
