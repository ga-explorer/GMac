using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine.Math;
using TextComposerLib.Text.Linear;

namespace GMac
{
    public static class GMacSystemUtils
    {
        private static readonly string IconsPath =
            Path.Combine(
                Path.GetDirectoryName(Application.ExecutablePath) ?? "",
                "Icons\\64"
                );


        internal static void SaveIcon(Image image, string iconName)
        {
            var filePath = Path.Combine(IconsPath, iconName + ".png");

            if (File.Exists(filePath) == false)
                image.Save(filePath, ImageFormat.Png);
        }

        public static string InitializeGMac()
        {
            var log = new LinearTextComposer();

            try
            {
                log.Append("Initializing GA Data...");

                GaLookupTables.Choose(1, 1);
                GMacMathUtils.IsNegativeEGp(0, 0);

                log.AppendLine("Done").AppendLine();

                log.Append("Initializing GMac Resources...");

                if (Directory.Exists(IconsPath) == false)
                    Directory.CreateDirectory(IconsPath);

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

                log.AppendLine("Done").AppendLine();

                log.Append("Initializing Symbolic Engine...");

                MathematicaScalar.Create(GaSymbolicsUtils.Cas, "0");

                log.AppendLine("Done").AppendLine();
            }
            catch (Exception e)
            {
                log.AppendLine("Failed").AppendLine(e.Message).AppendLine();
            }

            return log.ToString();
        }
    }
}
