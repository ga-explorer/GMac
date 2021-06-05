namespace GMac.Engine.Compiler.Semantic.ASTConstants
{
    /// <summary>
    /// Names of default frame macros
    /// </summary>
    public static class DefaultMacro
    {
        public static class EuclideanUnary
        {
            public const string Negative = "Negative";

            public const string Reverse = "Reverse";

            public const string GradeInvolution = "GradeInv";

            public const string CliffordConjugate = "CliffConj";


            public const string Magnitude = "EMag";

            public const string MagnitudeSquared = "EMag2";


            public const string Dual = "EDual";

            public const string SelfGeometricProduct = "SelfEGP";

            public const string SelfGeometricProductReverse = "SelfEGPRev";
        }

        public static class MetricUnary
        {
            public const string Magnitude = "Mag";

            public const string MagnitudeSquared = "Mag2";

            public const string NormSquared = "Norm2";

            public const string Dual = "Dual";

            public const string SelfGeometricProduct = "SelfGP";

            public const string SelfGeometricProductReverse = "SelfGPRev";
        }

        public static class EuclideanBinary
        {
            public const string OuterProduct = "OP";

            public const string GeometricProduct = "EGP";

            public const string ScalarProduct = "ESP";

            public const string LeftContractionProduct = "ELCP";

            public const string RightContractionProduct = "ERCP";

            public const string FatDotProduct = "EFDP";

            public const string HestenesInnerProduct = "EHIP";

            public const string CommutatorProduct = "ECP";

            public const string AntiCommutatorProduct = "EACP";

            public const string GeometricProductDual = "EGPDual";

            public const string DirectSandwitchProduct = "EDWP";

            public const string GradeInvolutionSandwitchProduct = "EGWP";


            public const string TimesWithScalar = "Times";

            public const string DivideByScalar = "Divide";
        }

        public static class MetricBinary
        {
            public const string GeometricProduct = "GP";

            public const string ScalarProduct = "SP";

            public const string LeftContractionProduct = "LCP";

            public const string RightContractionProduct = "RCP";

            public const string FatDotProduct = "FDP";

            public const string HestenesInnerProduct = "HIP";

            public const string CommutatorProduct = "CP";

            public const string AntiCommutatorProduct = "ACP";

            public const string GeometricProductDual = "GPDual";

            public const string DirectSandwitchProduct = "DWP";

            public const string GradeInvolutionSandwitchProduct = "GWP";

        }

        /// <summary>
        /// Names of default frame macros for computing general linear transforms on multivectors
        /// </summary>
        public static class LinearTransform
        {
            public const string Apply = "ApplyLT";

            public const string Transpose = "TransLT";

            public const string Add = "AddLT";

            public const string Subtract = "SubtractLT";

            public const string Compose = "ComposeLT";

            public const string TimesWithScalar = "TimesLT";

            public const string DivideByScalar = "DivideLT";
        }

        /// <summary>
        /// Names of default frame macros for computing general outer-morphisms on multivectors
        /// </summary>
        public static class Outermorphism
        {
            public const string Apply = "ApplyOM";

            public const string ApplyToVector = "AVOM";

            public const string Transpose = "TransOM";

            public const string MetricDeterminant = "DetOM";

            public const string EuclideanDeterminant = "EDetOM";

            public const string ToLinearTransform = "OMToLT";

            public const string Add = "AddOM";

            public const string Subtract = "SubtractOM";

            public const string Compose = "ComposeOM";

            public const string TimesWithScalar = "TimesOM";

            public const string DivideByScalar = "DivideOM";
        }

        public static class EuclideanVersor
        {
            public const string Apply = "ApplyEVersor";

            public const string ApplyRotor = "ApplyERotor";

            public const string ApplyReflector = "ApplyEReflector";

            public const string Inverse = "InvEVersor";

            public const string ToLinearTransform = "EVersorToLT";

            public const string ToOutermorphism = "EVersorToOM";
        }

        public static class MetricVersor
        {
            public const string Apply = "ApplyVersor";

            public const string ApplyRotor = "ApplyRotor";

            public const string ApplyReflector = "ApplyReflector";

            public const string Inverse = "InvVersor";

            public const string ToLinearTransform = "VersorToLT";

            public const string ToOutermorphism = "VersorToOM";
        }
    }
}