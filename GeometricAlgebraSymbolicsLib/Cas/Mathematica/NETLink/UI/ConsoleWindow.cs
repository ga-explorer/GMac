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
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Wolfram.NETLink.UI {

/// <summary>
/// A Form that displays text written to the standard streams Console.Out and/or Console.Error.
/// </summary>
/// <remarks>
/// This simple class is used by the Mathematica function ShowNETConsole, but you can also use it in
/// .NET programs. It is particularly useful in Windows applications (as opposed to console applications),
/// which typically do not have the Console.Out and Console.Error streams defined.
/// <para>
/// This class is a singleton, and you interact with it mainly through static methods and properties.
/// To acquire a reference to the singleton instance, use the Instance property. Here is a typical C# code
/// fragment demonstrating how to use this class:
/// <code>
///     ConsoleWindow.StreamsToCapture = ConsoleWindow.StreamType.Out;
///     ConsoleWindow.Instance.Show();
///     ConsoleWindow.Instance.Activate();
/// </code>
/// </para>
/// </remarks>
/// 
public class ConsoleWindow : System.Windows.Forms.Form {

    private static ConsoleWindow singleton;
    private static StreamType strms = StreamType.Out | StreamType.Error;
    private static int maxLines = 500;

    private TextWriter origOut, origErr;
    private StreamWriter writer;

    
    /// <summary>
    /// Gets the singleton instance of the ConsoleWindow class.
    /// </summary>
    ///
    public static ConsoleWindow Instance {
        get {
            if (singleton == null || singleton.IsDisposed)
                singleton = new ConsoleWindow();
            return singleton;
        }
    }


    /// <summary>
    /// Gets or sets the streams whose output should be displayed in the window.
    /// </summary>
    ///
    public static StreamType StreamsToCapture {
        get { return strms; }
        set {
            StreamType origValue = strms;
            strms = value;
            // Don't call Instance property, as we don't want to create the control just because
            // we set this property.
            if (singleton != null)
                singleton.updateStreams(origValue);
        }
    }


    /// <summary>
    /// Gets or sets the maximum number of lines to display in the window.
    /// </summary>
    /// <remarks>
    /// As lines are added beyond this limit, the oldest ones will be deleted from the buffer.
    /// <para>
    /// This property affects how many lines are stored in the window's buffer, not the phsyical
    /// size of the display area. A scroll bar appears in the window to allow you to view all lines
    /// in the buffer. 
    /// </para>
    /// </remarks>
    ///
    public static int MaxLines {
        get { return maxLines; }
        set { maxLines = value; }
    }


    
    /// <summary>
    /// Values for which stream to capture.
    /// </summary>
    /// 
    [Flags]
    public enum StreamType {
        None = 0,
        Out  = 1,
        Error  = 2
    }


    /// <summary>
    /// Clears the window contents. All stored lines are deleted.
    /// </summary>
    /// 
    public static void Clear() {
        Instance.textBox1.Lines = new string[1]{String.Empty};
    }


    /*************************************  TextBoxStream class  ***********************************/

    /// <summary>
    /// A stream that writes its output into a TextBox.
    /// </summary>
    /// 
    internal class TextBoxStream : Stream {

        private TextBox tb;

        internal TextBoxStream(TextBox tb) {
            this.tb = tb;
        }

        public override void Write(byte[] buffer, int offset, int count) {

            // Don't try to write if the textbox hasn't been created yet or has deen disposed.
            if (!tb.IsHandleCreated || tb.IsDisposed)
                return;

            StringBuilder sb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
                sb.Append((char) buffer[offset + i]);
            tb.AppendText(sb.ToString());
            if (tb.Lines.Length > MaxLines) {
                string[] newLines = new string[MaxLines];
                Array.Copy(tb.Lines, tb.Lines.Length - MaxLines, newLines, 0, MaxLines);
                tb.Lines = newLines;
            }
            tb.SelectionStart = Int32.MaxValue;
            tb.SelectionLength = 0;
            tb.ScrollToCaret();
        }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotSupportedException();
        }
        public override bool CanRead {
            get { return false; }
        }
        public override bool CanWrite {
            get { return true; }
        }
        public override bool CanSeek {
            get { return false; }
        }
        public override long Length {
            get { throw new NotSupportedException(); }
        }
        public override long Position {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        public override void Flush() {}
        public override long Seek(long i, SeekOrigin org) { throw new NotSupportedException(); }
        public override void SetLength(long i) { throw new NotSupportedException(); }
    }


    /****************************  Private and Forms Designer-Generated Code  ****************************/

    private void updateStreams(StreamType oldValue) {

        if ((strms & StreamType.Out) != 0 && (oldValue & StreamType.Out) == 0) {
            // Was currently not capturing, and requested to start.
            origOut = Console.Out;
            Console.SetOut(writer);
        } else if ((strms & StreamType.Out) == 0 && (oldValue & StreamType.Out) != 0) {
            // Was currently capturing, and requested to stop.
            Console.SetOut(origOut);
        }
        if ((strms & StreamType.Error) != 0 && (oldValue & StreamType.Error) == 0) {
            // Was currently not capturing, and requested to start.
            origErr = Console.Error;
            Console.SetError(writer);
        } else if ((strms & StreamType.Error) == 0 && (oldValue & StreamType.Error) != 0) {
            // Was currently capturing, and requested to stop.
            Console.SetError(origErr);
        }
    }


    private System.Windows.Forms.TextBox textBox1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    /// 
    private System.ComponentModel.Container components = null;


    private ConsoleWindow() {
        //
        // Required for Windows Form Designer support
        //
        InitializeComponent();

        writer = new StreamWriter(new TextBoxStream(textBox1));
        writer.AutoFlush = true;
        textBox1.Text =
            ".NET/Link Version " + NETLinkConstants.VERSION + "\r\n" + 
            ".NET Framework Version " + System.Environment.Version + "\r\n" + 
            "===========================================\r\n";
        // This call starts capturing according to the current setting of the static property StreamToCapture.
        // The StreamType.None argument is treated as the "old" value.
        updateStreams(StreamType.None);
    }


    // Without tweaking the selection, the initial text in the window (the .NET/Link version info) is highlighted
    // when the window first appears. We get errors if we try to change the selection before the control is created,
    // hence we use the OnCreateControl() method.
    protected override void OnCreateControl() {
        textBox1.SelectionStart = 10000; 
        base.OnCreateControl();
    }


    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
        if( disposing )
        {
            if(components != null)
            {
                components.Dispose();
            }
            if (origOut != null)
                Console.SetOut(origOut);
            if (origErr != null)
                Console.SetError(origErr);
        }
        base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // textBox1
        // 
        this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBox1.Font = new System.Drawing.Font("Courier New", 10F);
        this.textBox1.Multiline = true;
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.textBox1.Size = new System.Drawing.Size(448, 397);
        this.textBox1.TabIndex = 0;
        this.textBox1.Text = "";
        // 
        // ConsoleWindow
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.ClientSize = new System.Drawing.Size(448, 397);
        this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.textBox1});
        this.Name = "ConsoleWindow";
        this.Text = ".NET Console";
        this.ResumeLayout(false);

    }
	#endregion
}

}
