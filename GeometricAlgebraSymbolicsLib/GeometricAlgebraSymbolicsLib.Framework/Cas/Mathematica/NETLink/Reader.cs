//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

using System.Threading;
using System.Windows.Forms;

namespace Wolfram.NETLink {

/// <exclude/>
/// <summary>
/// Reader runs the thread that waits for calls to arrive from <i>Mathematica</i> and dispatches them when they do.
/// </summary>
/// <remarks>
/// The Reader thread is started up when the .NET runtime is launched via InstallNET[].
/// <para>
/// The Reader thread is also the thread on which <i>Mathematica</i>-programmed .NET user interfaces run. That is, it is their
/// event-dispatch thread.
/// </para>
/// This class is <i>not</i> part of the .NET/Link API. It is public only so that WRI can potentially assist very
/// advanced programmers in doing some special things with .NET programs and <i>Mathematica</i>. The vast majority
/// of .NET programs that use <i>Mathematica</i> will have no use for this class.
/// </remarks>
/// 
public class Reader {

    private static IKernelLink ml;
    private static bool quitWhenLinkEnds = true;
    private static bool linkHasDied = false;
    private static Thread readerThread; // Singleton instance
    private static bool isModal = false;
    private static bool isSharing = false;
    private static bool allowUIComps = true;
    private static bool isInNextPacket = false;

    private static YieldFunction yieldFunctionWithEvents = new YieldFunction(yielderWithEvents);
    private static YieldFunction yieldFunctionWithoutEvents = new YieldFunction(yielderWithoutEvents);

    private const int sleepInterval = 2;

    private const int BLOCKING = 1;
    private const int MODAL = 2;
    private const int SHARING_ALLOW_COMPUTATIONS = 3;
    private const int SHARING_DISALLOW_COMPUTATIONS = 4;


    // No one makes one of these but us!
    protected Reader() {}


    /// <summary>
    /// Starts the Reader thread.
    /// </summary>
    /// <param name="link">The link to wait for input on.</param>
    /// <param name="dieWhenLinkEnds">Whether to exit the thread when the link dies.</param>
    /// <returns>The Reader thread object.</returns>
    /// 
    public static Thread StartReader(IKernelLink link, bool dieWhenLinkEnds) {
    
        ml = link;
        quitWhenLinkEnds = dieWhenLinkEnds;
        linkHasDied = false;

        StdLink.Link = ml;
        StdLink.HasReader = true;
        ml.MessageArrived += new MessageHandler(terminateMsgHandler);

        readerThread = new Thread(new ThreadStart(Run));
        readerThread.Name = ".NET/Link Reader";
        readerThread.ApartmentState = ApartmentState.STA;
        readerThread.Start();

        return readerThread;
    }
    
    
    /// <summary>
    /// A "hard" abort for the Reader thread.
    /// </summary>
    /// <remarks>
    /// If you called StartReader with dieWhenLinkEnds = false, then call this method when you are done with the
    /// Reader thread, instead of just calling Abort on the Thread object (unless you are quitting the .NET runtime,
    /// in which case it doesn't matter how you shut down the Reader thread).
    /// <para>
    /// When .NET is launched by InstallNET[], this method is not used--the Reader thread dies when the link to
    /// the kernel is killed.
    /// </para>
    /// </remarks>
    /// 
    public static void StopReader() {
        
        readerThread.Abort();
        StdLink.HasReader = false;
    }
    

    /// <summary>
    /// This is the root method where the .NET runtime launched by InstallNET[] spends its life. It just sits here
    /// waiting for input to arrive from <i>Mathematica</i>.
    /// </summary>
    /// 
    public static void Run() {

        Application.ThreadException += new ThreadExceptionEventHandler(onThreadException);

        ml.Yield += yieldFunctionWithEvents;

        try {
            while (true) {
                if (isModal || isSharing) {
                    // Polling is much less efficient than blocking. It is used only in special circumstances (such as while the kernel is
                    // executing DoModal[], or after the kernel has called nAllowUIComputations[True]).

                    // If we are blocking in NextPacket(), the yield function is dispatching messages. But when we are
                    // polling Ready(), the yielder is not called, so we need to arrange for messages to be processed.
                    Application.DoEvents();
                    if (!ml.Ready)
                        Thread.Sleep(sleepInterval);
                    lock (ml) {
                        try {
                            if (ml.Error != 0)
                                throw new MathLinkException(ml.Error, ml.ErrorMessage);
                            if (ml.Ready) {
                                PacketType pkt = ml.NextPacket();
                                ml.HandlePacket(pkt);
                                ml.NewPacket();
                            }
                        } catch (MathLinkException e) {
                            // 11 is "other side closed link"; not sure why this succeeds clearError, but it does.
                            if (e.ErrCode == 11 || !ml.ClearError())
                                return;
                            ml.NewPacket();
                        }
                    }
                } else {
                    // Use blocking style (dive right into MLNextPacket and block there). Much more efficient.
                    lock (ml) {
                        try {
                            PacketType pkt = ml.NextPacket();
                            ml.HandlePacket(pkt);
                            ml.NewPacket();
                        } catch (MathLinkException e) {
                            // 11 is "other side closed link"; not sure why this succeeds clearError, but it does.
                            if (e.ErrCode == 11 || !ml.ClearError())
                                return;
                            ml.NewPacket();
                        }
                    } // end synchronized
                }
            }
        } finally {
            // Get here on unrecoverable MathLinkException, ThreadDeath exception caused by "hard" aborts
            // from Mathematica (see KernelLinkImpl.msgHandler()), or other Error exceptions (except during invoke()).
            if (quitWhenLinkEnds) {
                ml.Close();
                ml = null;
                //System.Diagnostics.Trace.Flush();
                Application.Exit();
            }
        }
    }


    /// <summary>
    /// This is called to notify the Reader thread that <i>Mathematica</i> is entering the ShareKernel state on
    /// the link to .NET. This means that calls to <i>Mathematica</i> initiated in .NET can be allowed to proceed.
    /// </summary>
    /// 
    internal static void shareKernel(bool entering) {
        isSharing = entering;
        // It is normal to exit the ShareKernel state without having a chance to reset the
        // allowUIComps flag to true. That means that the next time we started sharing
        // that flag would have the incorrect value of false. To avoid this, we set it to true
        // every time we start sharing.
        if (entering) {
            allowUIComps = true;
            ml.Yield -= yieldFunctionWithEvents;
            ml.Yield += yieldFunctionWithoutEvents;
        } else {
            ml.Yield -= yieldFunctionWithoutEvents;
            ml.Yield += yieldFunctionWithEvents;
        }
    }

    /// <summary>
    /// Gets or sets whether computations originating in .NET can proceed. This is read by StdLink.RequestTransaction.
    /// </summary>
    /// 
    internal static bool allowUIComputations {
        get { return allowUIComps; }
        set { allowUIComps = value; }
    }

    /// <summary>
    /// Gets or sets whether the Reader thread has alreay entered NextPacket. This is read by StdLink.RequestTransaction.
    /// </summary>
    /// 
    internal static bool isInsideNextPacket {
        get { return isInNextPacket; }
        set { isInNextPacket = value; }
    }

    /// <summary>
    /// Gets or sets whether the Reader thread is in the "modal" state (e.g., DONETModal[] is running).
    /// This is read by StdLink.RequestTransaction.
    /// </summary>
    /// 
    internal static bool isInModalState {
        get { return isModal; }
        set {
            isModal = value;
            if (isModal) {
                ml.Yield += yieldFunctionWithoutEvents;
                ml.Yield -= yieldFunctionWithEvents;
            } else {
                ml.Yield += yieldFunctionWithEvents;
                ml.Yield -= yieldFunctionWithoutEvents;
            }
        }
    }

    // In some circumstances we want a yield function that runs a Windows message loop. Other times, not.
    // These two yielders are for those two cases.
    // Specifically, we do not allow message processing in the yielder when we are in a domodal or sharekernel
    // state. Since we are polling in those states, not blocking in NextPacket, we can process messages in the
    // polling loop, outside the yield function. If we allowed message processing in the yielder, we would
    // have reentrancy problems, becaue the user could click a button that fired a callback to Mathematica while they
    // were already in the middle of another callback that had notreturned yet.
    // The basic issue is that we cannot allow calls to Mathematica on the yielder callback thread.
    // They would block waiting to acquire the lock on the link, but then the yielder would never
    // return, and this would block MathLink, leading to deadlock.

    internal static bool yielderWithEvents() {
        if (Utils.IsWindows) {
            // By setting isInsideNextPacket, we can tell StdLink.RequestTransaction() that we are already inside
            // a NextPacket() call, and therefore it is not safe to initiate a call from .NET into Mathematica.
            isInsideNextPacket = true;
            Application.DoEvents();
            isInsideNextPacket = false;
        }
        return false;
    }
    internal static bool yielderWithoutEvents() {
        return false;
    }


    /// <summary>
    /// This is what causes .NET to quit when user selects "Kill linked program" from <i>Mathematica</i>'s Interrupt dialog.
    /// </summary>
    /// <param name="msg"></param>
    /// 
    public static void terminateMsgHandler(MathLinkMessage msg) {

        if (msg == MathLinkMessage.Terminate)
            readerThread.Abort();
    }


    /// <summary>
    /// This handler is for when user triggers an event in .NET that fires a callback to <i>Mathematica</i>, but\
    /// <i>Mathematica</i> is not ready because it is not in DoModal[] or ShareKernel[]. This will cause
    /// StdLink.RequestTransaction() to throw MathematicaNotReadyException, which we catch here.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="t"></param>
    /// 
    public static void onThreadException(object sender, ThreadExceptionEventArgs t) {

        // TODO: Do something here for Unix.
        if (Utils.IsWindows) {
            if (t.Exception is MathematicaNotReadyException) {
                // TODO: Is there some way to salvage the dialog box here? The problem is that the yielder running
                // when this is called does not call DoEvents, so the thread hangs (and so does M because it calls
                // nAllowSharingComps, which cannot proceed). Perhaps I can put the messagebox on its own separate thread
                // and just return here.
                MessageBeep(0);
                //MessageBox.Show(t.Exception.ToString(), ".NET/Link", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            } else {
                // We need a message for other, unexpected, uncaught exceptions in event handling. By installing our own handler
                // we lose the default .NET dialog for unhandled exceptions. The linkHasDied check is to prevent
                // issuing a number of MessageBoxes for queued up events if things go badly from a UI callback into
                // Mathematica and the user has to kill the link or the kernel.
                if (!linkHasDied)
                    MessageBox.Show("An unhandled exception has occurred:\n" + t.Exception.ToString() + ".\n\n.NET/Link will attempt to continue.", ".NET/Link", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (t.Exception is MathLinkException) {
                    int err = ((MathLinkException) (t.Exception)).ErrCode;
                    if ((err == 11 || err == 1))
                        linkHasDied = true;
                }
            }
        }
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")] 
    private static extern int MessageBeep(uint n);  
}

}