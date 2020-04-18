using System.Text;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;

namespace GMacSamples.CodeGen.Multivectors
{
    internal sealed class BaseMvClassFileGenerator : MvLibraryCodeFileGenerator
    {
        #region Templates

        private const string ClassCodeTemplateText = @"
namespace GMacModel.#frame#
{
    public abstract class #frame#Multivector
    {
        #region Static Members

        public static #zero_class_name# Zero { get; private set; }

        public static #double# Epsilon { get; set; }

        static #frame#Multivector()
        {
            Epsilon = 1e-12;
            Zero = new #zero_class_name#();
        }

        #endregion

        #declare_coefs#

        public int ClassId { get; protected set; }

        public abstract int ActiveClassId { get; }

        public abstract bool IsZero { get; }

        public abstract bool IsKVector { get; }

        public abstract bool IsBlade { get; }

        public abstract bool IsScalar { get; }

        public abstract bool IsVector { get; }

        public abstract bool IsPseudoVector { get; }

        public abstract bool IsPseudoScalar { get; }

        public abstract bool IsTerm { get; }

        public abstract bool IsEven { get; }

        public abstract bool IsOdd { get; }


        public abstract #double# Norm2 { get; }


        public abstract bool IsEqual(#frame#Multivector mv);

        public abstract #frame#Multivector Simplify();

        public abstract #frame#Multivector OP(#frame#Multivector mv);

        public abstract #frame#Multivector GP(#frame#Multivector mv);

        public abstract #frame#Multivector LCP(#frame#Multivector mv);

        public abstract #frame#Multivector RCP(#frame#Multivector mv);

        public abstract #double# SP(#frame#Multivector mv);

        public abstract #frame#Multivector Add(#frame#Multivector mv);

        public abstract #frame#Multivector Subtract(#frame#Multivector mv);
    }
}
";

        #endregion

        internal static void Generate(MvLibrary libGen)
        {
            var generator = new BaseMvClassFileGenerator(libGen);

            generator.Generate();
        }


        private BaseMvClassFileGenerator(MvLibrary libGen)
            : base(libGen)
        {
            
        }

        private string GenerateDeclareCoefsText()
        {
            var s = new StringBuilder();

            var template = new ParametricComposer("#", "#", "public abstract #double# #coef_name# { get; }")
            {
                ["double"] = GMacLanguage.ScalarTypeName
            };


            for (var id = 0; id < CurrentFrame.GaSpaceDimension; id++)
            {
                //var coefName = "Coef" + GaUtils.ID_To_BinaryString(CurrentFrame.VSpaceDimension, id);
                var coefName = "Coef" + id;

                s.AppendLine(template.GenerateText("coef_name", coefName));
            }

            return s.ToString();
        }

        public override void Generate()
        {
            var declareCoefsText = GenerateDeclareCoefsText();

            var template = new ParametricComposer("#", "#", ClassCodeTemplateText);

            TextComposer.Append(
                template,
                "frame", CurrentFrameName,
                "double", GMacLanguage.ScalarTypeName,
                "zero_class_name", MvLibraryGenerator.MultivectorClassesData[0].ClassName,
                "declare_coefs", declareCoefsText
                );

            FileComposer.FinalizeText();
        }
    }
}
