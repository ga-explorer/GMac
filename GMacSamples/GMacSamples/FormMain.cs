using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using GMacSamples.CodeGen;
using GMacSamples.GMacBase;
using UtilLib.SampleTasks;

namespace GMacSamples
{
    public partial class FormMain : Form
    {
        //styles
        private readonly TextStyle _blueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        private readonly TextStyle _boldStyle = new TextStyle(null, null, FontStyle.Bold);// | FontStyle.Underline);
        private readonly TextStyle _grayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        private readonly TextStyle _magentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        private readonly TextStyle _greenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        private readonly TextStyle _brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        private TextStyle _maroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        private readonly MarkerStyle _sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        public FormMain()
        {
            InitializeComponent();
        }

        private void SourceCodeTextEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            SourceCodeTextEditor.LeftBracket = '(';
            SourceCodeTextEditor.RightBracket = ')';
            SourceCodeTextEditor.LeftBracket2 = '\x0';
            SourceCodeTextEditor.RightBracket2 = '\x0';


            //clear style of changed range
            e.ChangedRange.ClearStyle(_blueStyle, _boldStyle, _grayStyle, _magentaStyle, _greenStyle, _brownStyle);

            //comment highlighting
            e.ChangedRange.SetStyle(_greenStyle, @"//.*$", RegexOptions.Multiline);

            //string highlighting
            e.ChangedRange.SetStyle(_brownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");

            //comment highlighting
            e.ChangedRange.SetStyle(_greenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(_greenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);

            //number highlighting
            e.ChangedRange.SetStyle(_magentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");

            //attribute highlighting
            e.ChangedRange.SetStyle(_grayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);

            //class name highlighting
            //e.ChangedRange.SetStyle(BoldStyle, @"\b(namespace|constant|macro|frame|structure|class|member)\s+(?<range>\w+?)\b");

            //keyword highlighting
            e.ChangedRange.SetStyle(_blueStyle, @"\b(breakpoint|using|begin|end|frame|macro|transform|euclidean|IPM|CBM|orthogonal|orthonormal|reciprocal|class|ga|constants|declare|let|return|namespace|constant|structure|from|to|open|template|implement|subspace|access|binding|on|bind|to|with|result)\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"begin\b", @"end\b");//allow to collapse begin\end blocks
            //e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        private void menuItemTools_CodeGenerators_Click(object sender, EventArgs e)
        {
            var codeGenForm = new CodeGenSamples();

            codeGenForm.ShowDialog(this);
        }

        private void menuItemTools_BaseSamples_BitUtils_Click(object sender, EventArgs e)
        {
            var sampleTasks = GMacBaseSampleTasks.BitUtilsSampleTasks();

            var form = new FormSampleTasks(sampleTasks);

            form.ShowDialog(this);
        }

        private void menuItemTools_BaseSamples_MultivectorUtils_Click(object sender, EventArgs e)
        {
            var sampleTasks = GMacBaseSampleTasks.MultivectorUtilsSampleTasks();

            var form = new FormSampleTasks(sampleTasks);

            form.ShowDialog(this);
        }
    }
}
