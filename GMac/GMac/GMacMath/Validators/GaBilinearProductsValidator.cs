using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Frames;

namespace GMac.GMacMath.Validators
{
    public sealed class GaBilinearProductsValidator : GMacMathValidator
    {
        private readonly GaBilinearProductImplementation[] _methods = 
        {
            GaBilinearProductImplementation.LookupArray,
            GaBilinearProductImplementation.LookupHash,
            GaBilinearProductImplementation.LookupTree,
            GaBilinearProductImplementation.LookupCoefSums
        };

        private readonly string[] _methodNames = 
        {
            "Lookup Array",
            "Lookup Hash",
            "Lookup Tree",
            "Lookup CoefSums"
        };


        public GaNumFrame NumericFrame { get; set; }

        public GaSymFrame SymbolicFrame { get; set; }


        private void ValidateSymbolicFrame()
        {
            if (SymbolicFrame == null)
                return;

            ReportComposer.AppendHeader("Symbolic Bilinear Products Validations");

            //Initialize multivectors with random coefficients
            var mv1 = RandomGenerator.GetSymMultivectorFull(SymbolicFrame.VSpaceDimension);
            var mv2 = RandomGenerator.GetSymMultivectorFull(SymbolicFrame.VSpaceDimension);

            var mvComputedGp = SymbolicFrame.ComputedGp[mv1, mv2];
            var mvComputedOp = SymbolicFrame.ComputedOp[mv1, mv2];
            var mvComputedSp = SymbolicFrame.ComputedSp[mv1, mv2];
            var mvComputedLcp = SymbolicFrame.ComputedLcp[mv1, mv2];
            var mvComputedRcp = SymbolicFrame.ComputedRcp[mv1, mv2];
            var mvComputedFdp = SymbolicFrame.ComputedFdp[mv1, mv2];
            var mvComputedHip = SymbolicFrame.ComputedHip[mv1, mv2];
            var mvComputedAcp = SymbolicFrame.ComputedAcp[mv1, mv2];
            var mvComputedCp = SymbolicFrame.ComputedCp[mv1, mv2];

            //Compute their products using several methods
            for (var i = 0; i < _methods.Length; i++)
            {
                var method = _methods[i];
                var methodName = _methodNames[i];

                if (SymbolicFrame.IsNonOrthogonal)
                    SymbolicFrame.BaseOrthogonalFrame.SetProductsImplementation(method);

                SymbolicFrame.SetProductsImplementation(method);

                ReportComposer.AppendLineAtNewLine();
                ReportComposer.AppendHeader(methodName, 2);

                ValidateEqual("Geometric Product: ", mvComputedGp, SymbolicFrame.Gp[mv1, mv2]);
                ValidateEqual("Outer Product: ", mvComputedOp, SymbolicFrame.Op[mv1, mv2]);
                ValidateEqual("Scalar Product: ", mvComputedSp, SymbolicFrame.Sp[mv1, mv2]);
                ValidateEqual("Left Contraction Product: ", mvComputedLcp, SymbolicFrame.Lcp[mv1, mv2]);
                ValidateEqual("Right Contraction Product: ", mvComputedRcp, SymbolicFrame.Rcp[mv1, mv2]);
                ValidateEqual("Fat-Dot Product: ", mvComputedFdp, SymbolicFrame.Fdp[mv1, mv2]);
                ValidateEqual("Hestenes Inner Product: ", mvComputedHip, SymbolicFrame.Hip[mv1, mv2]);
                ValidateEqual("Anti-Commutator Product: ", mvComputedAcp, SymbolicFrame.Acp[mv1, mv2]);
                ValidateEqual("Commutator Product: ", mvComputedCp, SymbolicFrame.Cp[mv1, mv2]);

                ReportComposer.AppendLineAtNewLine();
            }

            ReportComposer.AppendLineAtNewLine();
        }

        private void ValidateNumericFrame()
        {
            if (NumericFrame == null)
                return;

            ReportComposer.AppendHeader("Numeric Bilinear Products Validations");

            //Initialize multivectors with random coefficients
            var mv1 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(NumericFrame.VSpaceDimension)
                    .CreateDgrMultivector(NumericFrame.VSpaceDimension);

            var mv2 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(NumericFrame.VSpaceDimension)
                    .CreateDgrMultivector(NumericFrame.VSpaceDimension);

            var mvComputedGp = NumericFrame.ComputedGp[mv1, mv2];
            var mvComputedOp = NumericFrame.ComputedOp[mv1, mv2];
            var mvComputedSp = NumericFrame.ComputedSp[mv1, mv2];
            var mvComputedLcp = NumericFrame.ComputedLcp[mv1, mv2];
            var mvComputedRcp = NumericFrame.ComputedRcp[mv1, mv2];
            var mvComputedFdp = NumericFrame.ComputedFdp[mv1, mv2];
            var mvComputedHip = NumericFrame.ComputedHip[mv1, mv2];
            var mvComputedAcp = NumericFrame.ComputedAcp[mv1, mv2];
            var mvComputedCp = NumericFrame.ComputedCp[mv1, mv2];

            //Compute their products using several methods
            for (var i = 0; i < _methods.Length; i++)
            {
                var method = _methods[i];
                var methodName = _methodNames[i];

                if (NumericFrame.IsNonOrthogonal)
                    NumericFrame.BaseOrthogonalFrame.SetProductsImplementation(method);

                NumericFrame.SetProductsImplementation(method);

                ReportComposer.AppendLineAtNewLine();
                ReportComposer.AppendHeader(methodName, 2);

                ValidateEqual("Geometric Product: ", mvComputedGp, NumericFrame.Gp[mv1, mv2]);
                ValidateEqual("Outer Product: ", mvComputedOp, NumericFrame.Op[mv1, mv2]);
                ValidateEqual("Scalar Product: ", mvComputedSp, NumericFrame.Sp[mv1, mv2]);
                ValidateEqual("Left Contraction Product: ", mvComputedLcp, NumericFrame.Lcp[mv1, mv2]);
                ValidateEqual("Right Contraction Product: ", mvComputedRcp, NumericFrame.Rcp[mv1, mv2]);
                ValidateEqual("Fat-Dot Product: ", mvComputedFdp, NumericFrame.Fdp[mv1, mv2]);
                ValidateEqual("Hestenes Inner Product: ", mvComputedHip, NumericFrame.Hip[mv1, mv2]);
                ValidateEqual("Anti-Commutator Product: ", mvComputedAcp, NumericFrame.Acp[mv1, mv2]);
                ValidateEqual("Commutator Product: ", mvComputedCp, NumericFrame.Cp[mv1, mv2]);

                ReportComposer.AppendLineAtNewLine();
            }

            ReportComposer.AppendLineAtNewLine();
        }

        private void ValidateBothFrame()
        {
            if (NumericFrame == null || SymbolicFrame == null)
                return;

            ReportComposer.AppendHeader("Numeric Bilinear Products Validations using Symbolic Computations");

            SymbolicFrame.SetProductsImplementation(GaBilinearProductImplementation.Computed);
            NumericFrame.SetProductsImplementation(GaBilinearProductImplementation.Computed);

            //Initialize multivectors with random coefficients
            var numMv1 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(SymbolicFrame.VSpaceDimension)
                    .CreateSarMultivector(SymbolicFrame.VSpaceDimension);

            var numMv2 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(SymbolicFrame.VSpaceDimension)
                    .CreateSarMultivector(SymbolicFrame.VSpaceDimension);

            var symMv1 = numMv1.ToSymbolic();
            var symMv2 = numMv2.ToSymbolic();

            ValidateEqual(
                "Geometric Product: ", 
                SymbolicFrame.Gp[symMv1, symMv2], 
                NumericFrame.Gp[numMv1, numMv2]
            );

            ValidateEqual(
                "Outer Product: ",
                SymbolicFrame.Op[symMv1, symMv2],
                NumericFrame.Op[numMv1, numMv2]
            );

            ValidateEqual(
                "Scalar Product: ",
                SymbolicFrame.Sp[symMv1, symMv2],
                NumericFrame.Sp[numMv1, numMv2]
            );

            ValidateEqual(
                "Left Contraction Product: ",
                SymbolicFrame.Lcp[symMv1, symMv2],
                NumericFrame.Lcp[numMv1, numMv2]
            );

            ValidateEqual(
                "Right Contraction Product: ",
                SymbolicFrame.Rcp[symMv1, symMv2],
                NumericFrame.Rcp[numMv1, numMv2]
            );

            ValidateEqual(
                "Fat-Dot Product: ",
                SymbolicFrame.Fdp[symMv1, symMv2],
                NumericFrame.Fdp[numMv1, numMv2]
            );

            ValidateEqual(
                "Hestenes Inner Product: ",
                SymbolicFrame.Hip[symMv1, symMv2],
                NumericFrame.Hip[numMv1, numMv2]
            );

            ValidateEqual(
                "Anti-Commutator Product: ",
                SymbolicFrame.Acp[symMv1, symMv2],
                NumericFrame.Acp[numMv1, numMv2]
            );

            ValidateEqual(
                "Commutator Product: ",
                SymbolicFrame.Cp[symMv1, symMv2],
                NumericFrame.Cp[numMv1, numMv2]
            );
        }

        public override string Validate()
        {
            ValidateSymbolicFrame();

            ValidateNumericFrame();

            ValidateBothFrame();

            return ReportComposer.ToString();
        }
    }
}