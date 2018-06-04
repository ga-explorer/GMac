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
using Wolfram.NETLink.Internal;

namespace Wolfram.NETLink {

/// <summary>
/// MathLinkFactory is the class that is used to construct objects of the various link interfaces
/// (<see cref="IKernelLink"/>, <see cref="IMathLink"/>, and <see cref="ILoopbackLink"/>).
/// </summary>
/// <remarks>
/// Programmers do not know, nor do they need to know, the names of the actual classes that implement the
/// various link interfaces. They never call a constructor to create a link class. Instead, so-called
/// "factory" methods are provided by the MathLinkFactory class to create the actual objects used.
/// <para>
/// Most programmers will use <see cref="CreateKernelLink"/> instead of <see cref="CreateMathLink"/>.
/// </para>
/// These methods correspond to calling one of the MLOpen functions in the C-language MathLink API.
/// </remarks>
/// 
public class MathLinkFactory {


    /*************************************  KernelLinks  *****************************************/

    /// <overloads>
    /// <summary>
    /// Creates a link to a <i>Mathematica</i> kernel. Use this method to create an <see cref="IKernelLink"/> object.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Launches the default <i>Mathematica</i> kernel and returns a link to it.
    /// </summary>
    /// <remarks>
    /// Use this method if you want to launch the kernel. This is probably the most commonly-used method in this class.
    /// <para>
    /// It looks in the Windows registry for the location of the most recently-installed copy of <i>Mathematica</i>.
    /// </para>
    /// </remarks>
    /// <returns>The created link.</returns>
    /// <exception cref="MathLinkException">If the opening of the link fails.</exception>
    /// 
    public static IKernelLink CreateKernelLink() {
        return new WrappedKernelLink(CreateMathLink());
    }

    /// <summary>
    /// Creates a link to a <i>Mathematica</i> kernel based on MathLink parameters supplied as a single string.
    /// </summary>
    /// <remarks>
    /// Use this method if you want to specify the path to the kernel, or if you want to establish a listen/connect
    /// link instead of launching the kernel.
    /// <para>
    /// You can use standard MathLink specifications for establishing a link, as documented in the <i>Mathematica</i>
    /// book or other MathLink documentation. For example, the -linkprotocol, -linkname, and -linkmode switches.
    /// </para>
    /// An example of a string argument would be:
    /// <code>
    /// string mlArgs = "-linkmode listen -linkname foo";
    /// IKernelLink ml = MathLinkFactory.CreateKernelLink(mlArgs);
    /// </code>
    /// If you give a pathname, make sure you surround it with quotes. Also, to avoid needing to use 4 \ characters
    /// for directory separators, use a forward slash (/) in the path:
    /// <code>
    /// string mlArgs = "-linkmode launch -linkname \"c:/path/to/my/mathematica/mathkernel.exe\"";
    /// IKernelLink ml = MathLinkFactory.CreateKernelLink(mlArgs);
    /// </code>
    /// </remarks>
    /// <param name="cmdLine">The string providing MathLink arguments.</param>
    /// <returns>The created link.</returns>
    /// <exception cref="MathLinkException">If the opening of the link fails.</exception>
    /// 
    public static IKernelLink CreateKernelLink(string cmdLine) {
        return createKernelLink0(cmdLine, null);
    }

    
    /// <summary>
    /// Creates a link to a <i>Mathematica</i> kernel based on MathLink parameters supplied as an argv-type array of strings.
    /// </summary>
    /// <remarks>
    /// Use this method if you want to specify the path to the kernel, or if you want to establish a listen/connect
    /// link instead of launching the kernel.
    /// <para>
    /// You can use standard MathLink specifications for establishing a link, as documented in the <i>Mathematica</i>
    /// book or other MathLink documentation. For example, -linkprotocol, -linkname, and -linkmode.
    /// </para>
    /// An example of a string array argument would be:
    /// <code>
    /// string[] mlArgs = {"-linkmode", "listen", "-linkname",  "foo"};
    /// IKernelLink ml = MathLinkFactory.CreateKernelLink(mlArgs);
    /// </code>
    /// To avoid worries about quoting \ characters used in a pathname, use a forward slash (/) in the path:
    /// <code>
    /// string[] mlArgs = {"-linkmode", "launch", "-linkname", "c:/path/to/my/mathematica/mathkernel.exe"};
    /// IKernelLink ml = MathLinkFactory.CreateKernelLink(mlArgs);
    /// </code>
    /// </remarks>
    /// <param name="argv">The array of MathLink arguments.</param>
    /// <returns>The created link.</returns>
    /// <exception cref="MathLinkException">If the opening of the link fails.</exception>
    /// 
    public static IKernelLink CreateKernelLink(string[] argv) {
        return createKernelLink0(null, argv);
    }

    
    /// <summary>
    /// Creates an IKernelLink from an <see cref="IMathLink"/>.
    /// </summary>
    /// <remarks>
    /// You can think of IKernelLink as a "decorator" for IMathLink. It builds on top of IMathLink by providing
    /// extra capabilities. If you have an IMathLink object, you can use this method to construct an
    /// IKernelLink implementation out of it.
    /// <para>
    /// Very few programmers will ever use this method. It is intended mainly for developers who are creating their
    /// own IKernelLink implementations. They only need to write the functionality of an IMathLink, and then they
    /// can use this method to get the extra features of an IKernelLink for free.
    /// </para>
    /// </remarks>
    /// <param name="ml">The IMathLink to "wrap".</param>
    /// <returns>The created link.</returns>
    /// <exception cref="MathLinkException">If the opening of the link fails.</exception>
    /// 
    public static IKernelLink CreateKernelLink(IMathLink ml) {
        return new WrappedKernelLink(ml);
    }
    

    private static IKernelLink createKernelLink0(string cmdLine, string[] argv) {
    
        if (cmdLine == null && argv == null)
            throw new MathLinkException(MathLinkException.MLE_CREATION_FAILED, "Null argument to KernelLink constructor");
        // One or other of cmdLine and argv must be null.
        bool usingCmdLine = cmdLine != null;
        string protocol = usingCmdLine ? determineProtocol(cmdLine) : determineProtocol(argv);
        // TODO: Use some property-lookup mechanism to find a class name associated with a protocol.
        // Then instantiate such an object.
        //if (protocol != "native") {
        //    string implClassName = null;
        //}
        // Fall through to here means we look for a MathLink implementation to wrap.
        return new WrappedKernelLink(usingCmdLine ? CreateMathLink(cmdLine) : CreateMathLink(argv));
    }
    

    /*************************************  MathLinks  *****************************************/

    /// <overloads>
    /// <summary>
    /// Creates a link. Use this method to create an <see cref="IMathLink"/> object.
    /// </summary>
    /// <remarks>
    /// Most programmers will want to use the <see cref="CreateKernelLink"/> method instead, so they
    /// can work with the higher-level <see cref="IKernelLink"/> interface. Use
    /// CreateMathLink only if you want a link that does not attach to a <i>Mathematica</i> kernel.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// Launches the default <i>Mathematica</i> kernel and returns an IMathLink link to it.
    /// </summary>
    /// <remarks>
    /// Most programmers will want to use the <see cref="CreateKernelLink"/> method instead.
    /// </remarks>
    /// <returns>The created link.</returns>
    /// <seealso cref="CreateKernelLink"/>
    /// <exception cref="MathLinkException">If the opening of the link fails.</exception>
    /// 
    public static IMathLink CreateMathLink()  {
        return new NativeLink("autolaunch");
    }
    

    /// <summary>
    /// Creates a link based on MathLink parameters supplied as a single string.
    /// </summary>
    /// <remarks>
    /// Use this method if you want to establish a listen/connect link to a program other than a <i>Mathematica</i> kernel.
    /// <para>
    /// You can use standard MathLink specifications for establishing a link, as documented in the <i>Mathematica</i>
    /// book or other MathLink documentation. For example, the -linkprotocol, -linkname, and -linkmode switches.
    /// </para>
    /// An example of a string argument would be:
    /// <code>
    /// string mlArgs = "-linkmode listen -linkname foo";
    /// IMathLink ml = MathLinkFactory.CreateMathlLink(mlArgs);
    /// </code>
    /// Most programmers will want to use the <see cref="CreateKernelLink"/> method instead.
    /// </remarks>
    /// <param name="cmdLine">The string providing MathLink arguments.</param>
    /// <returns>The created link.</returns>
    /// <seealso cref="CreateKernelLink"/>
    /// <exception cref="MathLinkException">If the opening of the link fails.</exception>
    /// 
    public static IMathLink CreateMathLink(string cmdLine)  {
        return createMathLink0(cmdLine, null);
    }
    

    /// <summary>
    /// Creates a link based on MathLink parameters supplied as an argv-type array of strings.
    /// </summary>
    /// <remarks>
    /// Use this method if you want to establish a listen/connect link to a program other than the <i>Mathematica</i> kernel.
    /// <para>
    /// You can use standard MathLink specifications for establishing a link, as documented in the <i>Mathematica</i>
    /// book or other MathLink documentation. For example, -linkprotocol, -linkname, and -linkmode.
    /// </para>
    /// An example of a string array argument would be:
    /// <code>
    /// string[] mlArgs = {"-linkmode", "listen", "-linkname",  "foo"};
    /// IMathLink ml = MathLinkFactory.CreateMathLink(mlArgs);
    /// </code>
    /// </remarks>
    /// <param name="argv">The array of MathLink arguments.</param>
    /// <returns>The created link.</returns>
    /// <seealso cref="CreateKernelLink"/>
    /// <exception cref="MathLinkException">If the opening of the link fails.</exception>
    /// 
    public static IMathLink CreateMathLink(string[] argv)  {
        return createMathLink0(null, argv);
    }

    
    private static IMathLink createMathLink0(string cmdLine, string[] argv)  {
        
        if (cmdLine == null && argv == null)
            throw new MathLinkException(MathLinkException.MLE_CREATION_FAILED, "Null argument to MathLink constructor");
        // One or other of cmdLine and argv must be null.
        bool usingCmdLine = cmdLine != null;
        string protocol = usingCmdLine ? determineProtocol(cmdLine) : determineProtocol(argv);
        // TODO: Use some preprty-lookup mechanism to find a class name associated with a protocol.
        // Then instantiate such an object.
        //if (protocol != "native") {
        //    string implClassName = null;
        //}
        return usingCmdLine ? new NativeLink(cmdLine) : new NativeLink(argv);
    }


    /*************************************  LoopbackLinks  *****************************************/

    /// <summary>
    /// Creates an <see cref="ILoopbackLink"/>, a special type of link that is written to and read by the same program.
    /// </summary>
    /// <remarks>
    /// Loopback links are links that have both ends connected to the same program, much like a FIFO queue.
    /// Loopback links are useful as temporary holders of expressions that are being moved between links,
    /// or as scratchpads on which expressions can be built up and then transferred to other links in a single call.
    /// <para>
    /// Much of the utility of loopback links to users of the C-language MathLink API is obviated by .NET/Link's
    /// <see cref="Expr"/> class, which provides many of the same features in a more accessible way (Expr uses
    /// loopback links in its implementation).
    /// </para>
    /// </remarks>
    ///
    public static ILoopbackLink CreateLoopbackLink()  {
        return new NativeLoopbackLink();
    }
    

    /***********************************  Command-line parsing  ************************************/
    
    // The determineProtocol functions return "NATIVE" for protocols that are implemented
    // by the NativeLink class (TCP, filemap, PPC, pipes, etc.) For special link types
    // (e.g., HTTP), they return the exact name specified following the -linkprotocol
    // specifier, in upper case.
    
    private static string determineProtocol(string cmdLine) {
        return determineProtocol(cmdLine.Split());
    }
    
    private static string determineProtocol(string[] argv) {
        
        string prot = "native";
        bool nextTokenIsProtocol = false;
        foreach (string s in argv) {
            if (nextTokenIsProtocol) {
                prot = s.ToLower();
                break;
            }
            // Case-insensitive compare.
            if (String.Compare(s, "-linkprotocol", true) == 0)
                nextTokenIsProtocol = true;
        }
        return isNative(prot) ? "native" : prot;
    }
    
    
    // Incoming string is in lower case. It is not a problem if we are overly conservative here,
    // failing to identify as native types that are. The only cost is that we waste time
    // looking for non-existent classes that might implement them. What we must not do is
    // return true for a type that requires a special class.
    private static bool isNative(string prot) {
        return prot == "native" || prot == "local" || prot == "filemap" || prot == "tcpip" ||
                    prot == "tcp" || prot == "pipes" || prot == "sharedmemory" || prot == "";
    }

}

}