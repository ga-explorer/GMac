using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Products;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib
{
    public static class GaSymbolicsUtils
    {
        public static MathematicaInterface Cas => MathematicaInterface.DefaultCas;

        public static MathematicaEvaluator Evaluator => Cas.Evaluator;

        public static MathematicaConstants Constants => Cas.Constants;


        public static bool IsNullOrZero(this MathematicaScalar scalar)
        {
            return ReferenceEquals(scalar, null) || scalar.IsZero();
        }

        public static bool IsNullOrEqualZero(this MathematicaScalar scalar)
        {
            return ReferenceEquals(scalar, null) || scalar.IsEqualZero();
        }

        public static MathematicaScalar ToMathematicaScalar(this Expr e)
        {
            return ReferenceEquals(e, null)
                ? Constants.Zero
                : MathematicaScalar.Create(Cas, e);
        }

        public static MathematicaScalar ToMathematicaScalar(this double e)
        {
            return MathematicaScalar.Create(Cas, e.ToExpr());
        }

        public static MathematicaScalar ToMathematicaScalar(this int e)
        {
            return MathematicaScalar.Create(Cas, e.ToExpr());
        }

        //public static BinaryTreeNode<MathematicaScalar> ComputeBasisBladesSignatures(this IReadOnlyList<MathematicaScalar> basisVectorsSignaturesList)
        //{
        //    var vSpaceDim = basisVectorsSignaturesList.Count;
        //    var bbsList = new BinaryTreeNode<MathematicaScalar>(vSpaceDim);

        //    bbsList.SetValue(0ul, Constants.One);

        //    for (var m = 0; m < vSpaceDim; m++)
        //    {
        //        var bvs = basisVectorsSignaturesList[m];

        //        if (bvs.IsNullOrZero()) continue;

        //        bbsList.SetValue(1ul << m, bvs);
        //    }

        //    var idsSeq = FrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
        //    foreach (var id in idsSeq)
        //    {
        //        int id1, id2;
        //        id.SplitBySmallestBasisVectorId(out id1, out id2);

        //        var bvs1 = bbsList.GetValue((ulong)id1);
        //        if (bvs1.IsNullOrZero()) continue;

        //        var bvs2 = bbsList.GetValue((ulong)id2);
        //        if (bvs2.IsNullOrZero()) continue;

        //        bbsList.SetValue((ulong)id, bvs1 * bvs2);
        //    }

        //    return bbsList;
        //}

        public static GaBtrInternalNode<int> ComputeBasisBladesSignatures(this IReadOnlyList<int> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GaBtrInternalNode<int>();

            bbsList.SetLeafValue(vSpaceDim, 0ul, 1);

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m];

                if (bvs == 0) continue;

                bbsList.SetLeafValue(vSpaceDim, 1ul << m, bvs);
            }

            var idsSeq = GaFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                id.SplitBySmallestBasisVectorId(out var id1, out var id2);

                var bvs1 = bbsList.GetLeafValue(vSpaceDim, (ulong)id1);
                if (bvs1 == 0) continue;

                var bvs2 = bbsList.GetLeafValue(vSpaceDim, (ulong)id2);
                if (bvs2 == 0) continue;

                bbsList.SetLeafValue(vSpaceDim, (ulong)id, bvs1 * bvs2);
            }

            return bbsList;
        }

        public static GaBtrInternalNode<double> ComputeBasisBladesSignatures(this IReadOnlyList<double> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GaBtrInternalNode<double>();

            bbsList.SetLeafValue(vSpaceDim, 0ul, 1.0d);

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m];

                if (bvs == 0.0d) continue;

                bbsList.SetLeafValue(vSpaceDim, 1ul << m, bvs);
            }

            var idsSeq = GaFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                id.SplitBySmallestBasisVectorId(out var id1, out var id2);

                var bvs1 = bbsList.GetLeafValue(vSpaceDim, (ulong)id1);
                if (bvs1 == 0.0d) continue;

                var bvs2 = bbsList.GetLeafValue(vSpaceDim, (ulong)id2);
                if (bvs2 == 0.0d) continue;

                bbsList.SetLeafValue(vSpaceDim, (ulong)id, bvs1 * bvs2);
            }

            return bbsList;
        }


        public static GaBtrInternalNode<MathematicaScalar> ToSymbolicTree(this GaBtrInternalNode<int> rootTreeNode)
        {
            return rootTreeNode?.MapTree(
                i => MathematicaScalar.Create(Cas, i)
            );
        }

        public static GaBtrInternalNode<MathematicaScalar> ToSymbolicTree(this GaBtrInternalNode<double> rootTreeNode)
        {
            return rootTreeNode?.MapTree(
                i => MathematicaScalar.Create(Cas, i)
            );
        }



        public static GaSymMultivector ToSymbolic(this GaNumSarMultivector mv)
        {
            var resultMv = GaSymMultivector.CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.GetNonZeroTerms())
                resultMv.SetTermCoef(term.BasisBladeId, term.ScalarValue.ToExpr());

            return resultMv;
        }

        public static GaNumSarMultivector ToNumeric(this GaSymMultivector mv)
        {
            var resultMv = new GaNumSarMultivectorFactory(mv.VSpaceDimension);

            foreach (var term in mv.NonZeroExprTerms)
                resultMv.SetTerm(term.Key, term.Value.ToNumber());

            return resultMv.GetSarMultivector();
        }


        public static double ToNumber(this Expr value)
        {
            if (ReferenceEquals(value, null))
                return 0.0d;

            if (!value.NumberQ())
                return 0.0d;

            var exprText = value.ToString();
            if (exprText == "0")
                return 0.0d;

            var textValue =
                Cas.Connection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value]]);

            return double.TryParse(textValue, out var doubleValue)
                ? doubleValue : 0.0d;
        }

        public static double ToNumber(this MathematicaScalar value)
        {
            if (ReferenceEquals(value, null))
                return 0.0d;

            if (!value.Expression.NumberQ())
                return 0.0d;

            var exprText = value.ExpressionText;
            if (exprText == "0")
                return 0.0d;

            var textValue =
                value.CasConnection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value.Expression]]);

            return double.TryParse(textValue, out var doubleValue)
                ? doubleValue : 0.0d;
        }


        public static bool IsNullOrNearNumericZero(this Expr value, double epsilon)
        {
            if (ReferenceEquals(value, null))
                return true;

            if (!value.NumberQ())
                return false;

            var exprText = value.ToString();
            if (exprText == "0")
                return true;

            var textValue =
                Cas.Connection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value]]);

            if (!double.TryParse(textValue, out var doubleValue))
                return false;

            return Math.Abs(doubleValue) <= epsilon;
        }


        public static Expr GetScalarExpr(this GaRandomGenerator randomGenerator)
        {
            return randomGenerator.GetScalar().ToExpr();
        }

        public static Expr GetScalarExpr(this GaRandomGenerator randomGenerator, double maxLimit)
        {
            return randomGenerator.GetScalar(maxLimit).ToExpr();
        }

        public static Expr GetScalarExpr(this GaRandomGenerator randomGenerator, double minLimit, double maxLimit)
        {
            return randomGenerator.GetScalar(minLimit, maxLimit).ToExpr();
        }


        public static Expr GetIntegerExpr(this GaRandomGenerator randomGenerator)
        {
            return randomGenerator.GetInteger().ToExpr();
        }

        public static Expr GetIntegerExpr(this GaRandomGenerator randomGenerator, int maxLimit)
        {
            return randomGenerator.GetInteger(maxLimit).ToExpr();
        }

        public static Expr GetIntegerExpr(this GaRandomGenerator randomGenerator, int minLimit, int maxLimit)
        {
            return randomGenerator.GetInteger(minLimit, maxLimit).ToExpr();
        }


        public static MathematicaScalar GetSymbolicInteger(this GaRandomGenerator randomGenerator)
        {
            return MathematicaScalar.Create(Cas, randomGenerator.GetInteger());
        }

        public static MathematicaScalar GetSymbolicInteger(this GaRandomGenerator randomGenerator, int maxLimit)
        {
            return MathematicaScalar.Create(Cas, randomGenerator.GetInteger(maxLimit));
        }

        public static MathematicaScalar GetSymbolicInteger(this GaRandomGenerator randomGenerator, int minLimit, int maxLimit)
        {
            return MathematicaScalar.Create(Cas, randomGenerator.GetInteger(minLimit, maxLimit));
        }


        public static MathematicaScalar GetSymbolicScalar(this GaRandomGenerator randomGenerator)
        {
            return MathematicaScalar.Create(Cas, randomGenerator.GetScalar());
        }

        public static MathematicaScalar GetSymbolicScalar(this GaRandomGenerator randomGenerator, double maxLimit)
        {
            return MathematicaScalar.Create(Cas, randomGenerator.GetScalar(maxLimit));
        }

        public static MathematicaScalar GetSymbolicScalar(this GaRandomGenerator randomGenerator, double minLimit, double maxLimit)
        {
            return MathematicaScalar.Create(Cas, randomGenerator.GetScalar(minLimit, maxLimit));
        }


        public static GaSymMultivector GetSymMultivectorFull(this GaRandomGenerator randomGenerator, int gaSpaceDim)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

            return mv;
        }

        public static GaSymMultivector GetSymMultivectorFull(this GaRandomGenerator randomGenerator, int gaSpaceDim, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

            return mv;
        }

        public static GaSymMultivector GetSymMultivectorFull(this GaRandomGenerator randomGenerator, int gaSpaceDim, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

            return mv;
        }

        public static GaSymMultivector GetSymMultivectorByTerms(this GaRandomGenerator randomGenerator, int gaSpaceDim, params int[] basisBladeIDs)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

            return mv;
        }

        public static GaSymMultivector GetSymMultivectorByTerms(this GaRandomGenerator randomGenerator, int gaSpaceDim, IEnumerable<int> basisBladeIDs)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

            return mv;
        }

        public static GaSymMultivector GetSymMultivectorByGrades(this GaRandomGenerator randomGenerator, int gaSpaceDim, params int[] grades)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            var basisBladeIDs =
                GaFrameUtils.BasisBladeIDsOfGrades(
                    mv.VSpaceDimension,
                    grades
                );

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

            return mv;
        }

        public static GaSymMultivector GetSymMultivector(this GaRandomGenerator randomGenerator, int gaSpaceDim)
        {
            //Randomly select the number of terms in the multivector
            var termsCount = randomGenerator.GetInteger(gaSpaceDim - 1);

            //Randomly select the terms basis blades in the multivectors
            var basisBladeIDs = randomGenerator.GetRangePermutation(gaSpaceDim - 1).Take(termsCount);

            //Randomly generate the multivector's coefficients
            return randomGenerator.GetSymMultivectorByTerms(gaSpaceDim, basisBladeIDs);
        }

        public static GaSymMultivector GetSymMultivector(this GaRandomGenerator randomGenerator, int gaSpaceDim, string baseCoefName)
        {
            //Randomly select the number of terms in the multivector
            var termsCount = randomGenerator.GetInteger(gaSpaceDim - 1);

            //Randomly select the terms basis blades in the multivectors
            var basisBladeIDs = randomGenerator.GetRangePermutation(gaSpaceDim - 1).Take(termsCount);

            //Generate the multivector's symbolic coefficients
            return GaSymMultivector.CreateSymbolic(gaSpaceDim, baseCoefName, basisBladeIDs);
        }


        public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int gaSpaceDim, string baseCoefName)
        {
            //Randomly select the number of terms in the multivector
            var basisBladeId = randomGenerator.GetInteger(gaSpaceDim - 1);

            //Generate the multivector's symbolic coefficients
            return GaSymMultivector.CreateSymbolicTerm(gaSpaceDim, baseCoefName, basisBladeId);
        }


        public static GaSymMultivector GetSymVector(this GaRandomGenerator randomGenerator, int gaSpaceDim)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

            return mv;
        }

        public static GaSymMultivector GetSymVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

            return mv;
        }

        public static GaSymMultivector GetSymVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

            return mv;
        }


        public static GaSymMultivector GetSymIntegerVector(this GaRandomGenerator randomGenerator, int gaSpaceDim)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr());

            return mv;
        }

        public static GaSymMultivector GetSymIntegerVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int maxLimit)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(maxLimit));

            return mv;
        }

        public static GaSymMultivector GetSymIntegerVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int minLimit, int maxLimit)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(minLimit, maxLimit));

            return mv;
        }


        public static IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(this GaRandomGenerator randomGenerator, int gaSpaceDim, int count)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (count < 1 || count > vSpaceDim)
                yield break;

            var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
            while (count > 0)
            {
                var v = randomGenerator.GetSymIntegerVector(gaSpaceDim);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                count--;

                yield return v;
            }
        }

        public static IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(this GaRandomGenerator randomGenerator, int gaSpaceDim, int count, int maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (count < 1 || count > vSpaceDim)
                yield break;

            var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
            while (count > 0)
            {
                var v = randomGenerator.GetSymIntegerVector(gaSpaceDim, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                count--;

                yield return v;
            }
        }

        public static IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(this GaRandomGenerator randomGenerator, int gaSpaceDim, int count, int minValue, int maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (count < 1 || count > vSpaceDim)
                yield break;

            var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
            while (count > 0)
            {
                var v = randomGenerator.GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                count--;

                yield return v;
            }
        }


        public static GaSymMultivector GetSymKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

            return mv;
        }

        public static GaSymMultivector GetSymKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

            return mv;
        }

        public static GaSymMultivector GetSymKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

            return mv;
        }


        public static GaSymMultivector GetSymIntegerKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr());

            return mv;
        }

        public static GaSymMultivector GetSymIntegerKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(maxValue));

            return mv;
        }

        public static GaSymMultivector GetSymIntegerKVector(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int minValue, int maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GaFrameUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, randomGenerator.GetIntegerExpr(minValue, maxValue));

            return mv;
        }


        public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int gaSpaceDim, int basisBladeId)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

            return mv;
        }

        public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int gaSpaceDim, int basisBladeId, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

            return mv;
        }

        public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int gaSpaceDim, int basisBladeId, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

            return mv;
        }

        public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int index)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GaFrameUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar());

            return mv;
        }

        public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int index, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GaFrameUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(maxValue));

            return mv;
        }

        public static GaSymMultivector GetSymTerm(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int index, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GaFrameUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, randomGenerator.GetSymbolicScalar(minValue, maxValue));

            return mv;
        }


        public static GaSymMultivector GetSymBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return randomGenerator.GetSymKVector(gaSpaceDim, grade);

            var mv = randomGenerator.GetSymVector(gaSpaceDim);
            grade--;

            while (grade > 0)
            {
                var v = randomGenerator.GetSymVector(gaSpaceDim);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public static GaSymMultivector GetSymBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, double maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return randomGenerator.GetSymKVector(gaSpaceDim, grade, maxValue);

            var mv = randomGenerator.GetSymVector(gaSpaceDim, maxValue);
            grade--;

            while (grade > 0)
            {
                var v = randomGenerator.GetSymVector(gaSpaceDim, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public static GaSymMultivector GetSymBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, double minValue, double maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return randomGenerator.GetSymKVector(gaSpaceDim, grade, minValue, maxValue);

            var mv = randomGenerator.GetSymVector(gaSpaceDim, minValue, maxValue);
            grade--;

            while (grade > 0)
            {
                var v = randomGenerator.GetSymVector(gaSpaceDim, minValue, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }


        public static GaSymMultivector GetSymIntegerBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return randomGenerator.GetSymIntegerKVector(gaSpaceDim, grade);

            var mv = randomGenerator.GetSymIntegerVector(gaSpaceDim);
            grade--;

            while (grade > 0)
            {
                var v = randomGenerator.GetSymIntegerVector(gaSpaceDim);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public static GaSymMultivector GetSymIntegerBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return randomGenerator.GetSymIntegerKVector(gaSpaceDim, grade, maxValue);

            var mv = randomGenerator.GetSymIntegerVector(gaSpaceDim, maxValue);
            grade--;

            while (grade > 0)
            {
                var v = randomGenerator.GetSymIntegerVector(gaSpaceDim, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public static GaSymMultivector GetSymIntegerBlade(this GaRandomGenerator randomGenerator, int gaSpaceDim, int grade, int minValue, int maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return randomGenerator.GetSymIntegerKVector(gaSpaceDim, grade, minValue, maxValue);

            var mv = randomGenerator.GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
            grade--;

            while (grade > 0)
            {
                var v = randomGenerator.GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }


        public static GaSymMultivector GetSymNonNullVector(this GaRandomGenerator randomGenerator, GaSymFrame frame)
        {
            GaSymMultivector mv;

            do
                mv = randomGenerator.GetSymVector(frame.GaSpaceDimension);
            while (!frame.Norm2(mv).IsZero());

            return mv;
        }


        public static GaSymMultivector GetSymVersor(this GaRandomGenerator randomGenerator, GaSymFrame frame, int vectorsCount)
        {
            var mv = randomGenerator.GetSymNonNullVector(frame);
            vectorsCount--;

            while (vectorsCount > 0)
            {
                mv = frame.Gp[mv, randomGenerator.GetSymNonNullVector(frame)];
                vectorsCount--;
            }

            return mv;
        }

    }
}
