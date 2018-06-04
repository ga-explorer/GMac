using System;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Frames;

namespace GMac.GMacMath.Validators
{
    public sealed class GaBladeOperationsValidator : GMacMathValidator
    {
        public GaNumFrame NumericFrame { get; set; }

        public GaSymFrame SymbolicFrame { get; set; }


        private void ValidateComputingMainProductsSymbolic(int grade1, int grade2)
        {
            ReportComposer.AppendLineAtNewLine();
            ReportComposer.AppendHeader("Grades: <" + grade1 + ", " + grade2 + ">", 2);

            //Initialize blades with random integer coefficients
            var mv1 = RandomGenerator.GetSymBlade(SymbolicFrame.GaSpaceDimension, grade1);
            var mv2 = RandomGenerator.GetSymBlade(SymbolicFrame.GaSpaceDimension, grade2);

            //Compute their geometric product
            var mvGp = SymbolicFrame.Gp[mv1, mv2];

            //Compute their Outer Product and test its relation to the geometric product
            var mvOp1 = SymbolicFrame.Op[mv1, mv2];
            var mvOp2 = mvGp.GetKVectorPart(grade1 + grade2);
            ValidateEqual("Outer Product: ", mvOp1, mvOp2);

            //Compute their Scalar Product and test its relation to the geometric product
            var mvSp1 = SymbolicFrame.Sp[mv1, mv2];
            var mvSp2 = mvGp.GetKVectorPart(0);
            ValidateEqual("Scalar Product: ", mvSp1, mvSp2);

            //Compute their Left Contraction Product and test its relation to the geometric product
            var mvLcp1 = SymbolicFrame.Lcp[mv1, mv2];
            var mvLcp2 = mvGp.GetKVectorPart(grade2 - grade1);
            ValidateEqual("Left Contraction Product: ", mvLcp1, mvLcp2);

            //Compute their Right Contraction Product and test its relation to the geometric product
            var mvRcp1 = SymbolicFrame.Rcp[mv1, mv2];
            var mvRcp2 = mvGp.GetKVectorPart(grade1 - grade2);
            ValidateEqual("Right Contraction Product: ", mvRcp1, mvRcp2);

            //Compute their Fat-Dot Product and test its relation to the geometric product
            var mvFdp1 = SymbolicFrame.Fdp[mv1, mv2];
            var mvFdpGrade = grade1 == grade2 ? 0 : Math.Abs(grade1 - grade2);
            var mvFdp2 = mvGp.GetKVectorPart(mvFdpGrade);
            ValidateEqual("Fat-Dot Product: ", mvFdp1, mvFdp2);

            //Compute their Anti-Commutator Product and test its relation to the geometric product
            var mvAcp1 = SymbolicFrame.Acp[mv1, mv2];
            var mvAcp2 = (SymbolicFrame.Gp[mv1, mv2] + SymbolicFrame.Gp[mv2, mv1]) / 2;
            ValidateEqual("Anti-Commutator Product: ", mvAcp1, mvAcp2);

            //Compute their Commutator Product and test its relation to the geometric product
            var mvCp1 = SymbolicFrame.Cp[mv1, mv2];
            var mvCp2 = (SymbolicFrame.Gp[mv1, mv2] - SymbolicFrame.Gp[mv2, mv1]) / 2;
            ValidateEqual("Commutator Product: ", mvCp1, mvCp2);

            ReportComposer.AppendLineAtNewLine();
        }

        public override string Validate()
        {
            ReportComposer.AppendHeader("Blade Operations Validations");

            for (var grade1 = 0; grade1 < SymbolicFrame.VSpaceDimension; grade1++)
            for (var grade2 = 0; grade2 < SymbolicFrame.VSpaceDimension; grade2++)
            {
                ValidateComputingMainProductsSymbolic(grade1, grade2);
            }

            return ReportComposer.ToString();
        }

    }
}
