using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine;
using GMac.Engine.Math;
using TextComposerLib.Text.Linear;

namespace GMac.IDE
{
    //TODO: Use members in GMacSystemUtils class instead of some private members of this form
    public partial class FormGMacSplash : Form
    {
        private readonly LinearTextComposer _log = new LinearTextComposer();

        private readonly string _iconsPath = 
            Path.Combine(
                Path.GetDirectoryName(Application.ExecutablePath) ?? "", 
                "Icons\\64"
                );


        public FormGMacSplash()
        {
            InitializeComponent();

            labelName.Text = GMacEngineUtils.AppName;
            labelVersion.Text = GMacEngineUtils.Version;
            labelCopyright.Text = GMacEngineUtils.Copyright;
        }

        private bool InitGaTables()
        {
            _log.Append("Initializing GA Data...");
            labelStatus.Text = _log.ToString();
            Application.DoEvents();

            try
            {
                GaLookupTables.Choose(1, 1);
                GMacMathUtils.IsNegativeEGp(0, 0);

                _log.AppendLine("Done").AppendLine();
                labelStatus.Text = _log.ToString();
                Application.DoEvents();

                return true;
            }
            catch (Exception e)
            {
                _log.AppendLine("Failed").AppendLine(e.Message).AppendLine();
                labelStatus.Text = _log.ToString();
                Application.DoEvents();

                return false;
            }
        }

        private bool InitSymbolicEngine()
        {
            try
            {
                _log.Append("Initializing Symbolic Engine...");
                labelStatus.Text = _log.ToString();
                Application.DoEvents();

                MathematicaScalar.Create(GaSymbolicsUtils.Cas, "0");

                _log.AppendLine("Done").AppendLine();
                labelStatus.Text = _log.ToString();
                Application.DoEvents();

                return true;
            }
            catch (Exception e)
            {
                _log.AppendLine("Failed").AppendLine(e.Message).AppendLine();
                labelStatus.Text = _log.ToString();
                Application.DoEvents();

                return false;
            }
        }

        private void SaveIcon(Image image, string iconName)
        {
            var filePath = Path.Combine(_iconsPath, iconName + ".png");

            if (File.Exists(filePath) == false)
                image.Save(filePath, ImageFormat.Png);
        }

        private bool InitGMacResources()
        {
            try
            {
                _log.Append("Initializing GMac Resources...");
                labelStatus.Text = _log.ToString();
                Application.DoEvents();

                if (Directory.Exists(_iconsPath) == false)
                    Directory.CreateDirectory(_iconsPath);

                //SaveIcon(Resources.BasisVector64, "BasisVector64");
                //SaveIcon(Resources.Compile64, "Compile64");
                //SaveIcon(Resources.Constant64, "Constant64");
                //SaveIcon(Resources.Filter64, "Filter64");
                //SaveIcon(Resources.Frame64, "Frame64");
                //SaveIcon(Resources.GMacAST64, "GMacAST64");
                //SaveIcon(Resources.GMac_Icon64, "GMac_Icon64");
                //SaveIcon(Resources.Idea64, "Idea64");
                //SaveIcon(Resources.Input64, "Input64");
                //SaveIcon(Resources.Macro64, "Macro64");
                //SaveIcon(Resources.Namespace64, "Namespace64");
                //SaveIcon(Resources.Output64, "Output64");
                //SaveIcon(Resources.Scalar64, "Scalar64");
                //SaveIcon(Resources.Structure64, "Structure64");
                //SaveIcon(Resources.Subspace64, "Subspace64");
                //SaveIcon(Resources.Transform64, "Transform64");

                _log.AppendLine("Done").AppendLine();
                labelStatus.Text = _log.ToString();
                Application.DoEvents();

                return true;
            }
            catch (Exception e)
            {
                _log.AppendLine("Failed").AppendLine(e.Message).AppendLine();
                labelStatus.Text = _log.ToString();
                Application.DoEvents();

                return false;
            }
        }

        private void InitialAction()
        {
            InitGaTables();

            InitGMacResources();

            InitSymbolicEngine();
        }

        private void FinalAction(DateTime startTime)
        {
            while ((DateTime.Now - startTime).TotalSeconds < 2)
                Application.DoEvents();

            startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalSeconds < 1)
                Application.DoEvents();

            Close();
        }

        private void FormGMacSplash_Load(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;

            Task.Factory
                .StartNew(
                    task => { },
                    TaskCreationOptions.None
                    )
                .ContinueWith(
                    task =>
                    {
                        //Put any UI interface modifications code to be executed after code generation task 
                        //completes here
                        InitialAction();
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext()
                    )
                .ContinueWith(
                    task =>
                    {
                        //Put any UI interface modifications code to be executed after code generation task 
                        //completes here
                        FinalAction(startTime);
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext()
                    );
        }

        private void labelLicense_Click(object sender, EventArgs e)
        {

        }
    }
}
