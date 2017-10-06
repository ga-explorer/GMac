using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacCompiler.Symbolic;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib.Text.Structured;

namespace GMac.GMacUtils
{
    public static class EuclideanUtils
    {
        /// <summary>
        /// Return true if the outer product of the given euclidean basis blades ids is always zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroOp(int id1, int id2)
        {
            return (id1 & id2) != 0;
        }

        /// <summary>
        /// Always returns true because the Euclidean geometriic product of any two basis blades is never zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroEuclideanGp(int id1, int id2)
        {
            return false;
        }

        /// <summary>
        /// Return true if the scalar product of the given euclidean basis blades ids is always zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroEuclideanSp(int id1, int id2)
        {
            return (id1 ^ id2) != 0;
        }

        /// <summary>
        /// Return true if the left contraction product of the given euclidean basis blades ids is always zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroEuclideanLcp(int id1, int id2)
        {
            return (id1 & ~id2) != 0;
        }

        /// <summary>
        /// Return true if the right contraction product of the given euclidean basis blades ids is always zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroEuclideanRcp(int id1, int id2)
        {
            return (id2 & ~id1) != 0;
        }

        /// <summary>
        /// Return true if the fat-dot product of the given euclidean basis blades ids is always zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroEuclideanFdp(int id1, int id2)
        {
            var g1 = id1.BasisBladeGrade();
            var g2 = id2.BasisBladeGrade();

            return
                (g1 == g2 && (id2 ^ id1) != 0) ||
                (g1 < g2 && (id1 & ~id2) != 0) ||
                (g1 > g2 && (id2 & ~id1) != 0);
        }

        /// <summary>
        /// Return true if the Hestenes inner product of the given euclidean basis blades ids is always zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroEuclideanHip(int id1, int id2)
        {
            var g1 = id1.BasisBladeGrade();
            var g2 = id2.BasisBladeGrade();

            if (g1 == 0 || g2 == 0)
                return true;

            return
                (g1 == g2 && (id2 ^ id1) != 0) ||
                (g1 < g2 && (id1 & ~id2) != 0) ||
                (g1 > g2 && (id2 & ~id1) != 0);
        }

        /// <summary>
        /// Return true if the commutator product of the given euclidean basis blades ids is always zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroEuclideanCp(int id1, int id2)
        {
            return FrameUtils.IsNegativeEGp(id1, id2) == FrameUtils.IsNegativeEGp(id2, id1);
        }

        /// <summary>
        /// Return true if the anti-commutator product of the given euclidean basis blades ids is always zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsZeroEuclideanAcp(int id1, int id2)
        {
            return FrameUtils.IsNegativeEGp(id1, id2) == !FrameUtils.IsNegativeEGp(id2, id1);
        }


        internal static IEnumerable<KeyValuePair<int, string>> FilterTermsForOp(this Dictionary<int, string> terms, int id1)
        {
            return terms.Where(pair => (id1 & pair.Key) == 0);
        }

        internal static IEnumerable<KeyValuePair<int, string>> FilterTermsUsing(this Dictionary<int, string> terms, int id1, Func<int, int, bool> termDiscardFunction)
        {
            return terms.Where(pair => termDiscardFunction(id1, pair.Key) == false);
        }

        internal static string Times(string coef1, string coef2)
        {
            var s = new StringBuilder();

            s.Append("Times[")
                .Append(coef1)
                .Append(",")
                .Append(coef2)
                .Append("]");

            return s.ToString();
        }

        internal static string Times(string coef1, string coef2, bool negativeFlag)
        {
            var s = new StringBuilder();

            s.Append(negativeFlag ? "Times[-1," : "Times[")
                .Append(coef1)
                .Append(",")
                .Append(coef2)
                .Append("]");

            return s.ToString();
        }

        internal static string Times(string coef1, string coef2, string factor, bool negativeFlag)
        {
            var s = new StringBuilder();

            s.Append(negativeFlag ? "Times[-1," : "Times[")
                .Append(coef1)
                .Append(",")
                .Append(coef2)
                .Append(",")
                .Append(factor)
                .Append("]");

            return s.ToString();
        }

        internal static void AddTerm(this Dictionary<int, ListComposer> accumExprDict, int resultId, string resultCoefDelta)
        {
            ListComposer resultCoefTermsList;

            if (accumExprDict.TryGetValue(resultId, out resultCoefTermsList) == false)
            {
                resultCoefTermsList = new ListComposer(",") { FinalPrefix = "Plus[", FinalSuffix = "]" };

                accumExprDict.Add(resultId, resultCoefTermsList);
            }

            resultCoefTermsList.Add(resultCoefDelta);
        }

        internal static GaMultivector ToMultivector(this Dictionary<int, ListComposer> accumExprDict, int gaSpaceDim)
        {
            var resultMv = GaMultivector.CreateZero(gaSpaceDim);

            foreach (var pair in accumExprDict)
            {
                var resultCoef = SymbolicUtils.Cas[pair.Value.ToString()];

                if (!(resultCoef.Args.Length == 0 && resultCoef.ToString() == "0"))
                    resultMv.Add(pair.Key, MathematicaScalar.Create(SymbolicUtils.Cas, resultCoef));
            }

            return resultMv;
        }

        internal static GaMultivector EuclideanBilinearProduct(this GaMultivector mv1, GaMultivector mv2, Func<int, int, bool> termDiscardFunction)
        {
            if (mv1.GaSpaceDim != mv2.GaSpaceDim)
                throw new GMacSymbolicException("Multivector size mismatch");

            var terms1 = mv1.ToStringsDictionary();

            var terms2 = mv2.ToStringsDictionary();

            var accumExprDict = new Dictionary<int, ListComposer>();

            foreach (var term1 in terms1)
            {
                var id1 = term1.Key;
                var coef1 = term1.Value;

                foreach (var term2 in terms2.FilterTermsUsing(id1, termDiscardFunction))
                {
                    var id2 = term2.Key;
                    var coef2 = term2.Value;

                    var resultId = id1 ^ id2;

                    var resultCoefDelta =
                        Times(coef1, coef2, FrameUtils.IsNegativeEGp(id1, id2));

                    accumExprDict.AddTerm(resultId, resultCoefDelta);
                }
            }

            return accumExprDict.ToMultivector(mv1.GaSpaceDim);
        }


        public static GaMultivector OuterProduct(this GaMultivector mv1, GaMultivector mv2)
        {
            if (mv1.GaSpaceDim != mv2.GaSpaceDim)
                throw new GMacSymbolicException("Multivector size mismatch");

            var terms1 = mv1.ToStringsDictionary();

            var terms2 = mv2.ToStringsDictionary();

            var accumExprDict = new Dictionary<int, ListComposer>();

            foreach (var term1 in terms1)
            {
                var id1 = term1.Key;
                var coef1 = term1.Value;

                foreach (var term2 in terms2.FilterTermsForOp(id1))
                {
                    var id2 = term2.Key;
                    var coef2 = term2.Value;

                    var resultId = id1 ^ id2;

                    var resultCoefDelta =
                        Times(coef1, coef2, FrameUtils.IsNegativeEGp(id1, id2));

                    accumExprDict.AddTerm(resultId, resultCoefDelta);
                }
            }

            return accumExprDict.ToMultivector(mv1.GaSpaceDim);
        }

        public static GaMultivector EuclideanGp(this GaMultivector mv1, GaMultivector mv2)
        {
            return EuclideanBilinearProduct(mv1, mv2, IsZeroEuclideanGp);
        }

        public static GaMultivector EuclideanSp(this GaMultivector mv1, GaMultivector mv2)
        {
            return EuclideanBilinearProduct(mv1, mv2, IsZeroEuclideanSp);
        }

        public static GaMultivector EuclideanLcp(this GaMultivector mv1, GaMultivector mv2)
        {
            return EuclideanBilinearProduct(mv1, mv2, IsZeroEuclideanLcp);
        }

        public static GaMultivector EuclideanRcp(this GaMultivector mv1, GaMultivector mv2)
        {
            return EuclideanBilinearProduct(mv1, mv2, IsZeroEuclideanRcp);
        }

        public static GaMultivector EuclideanFdp(this GaMultivector mv1, GaMultivector mv2)
        {
            return EuclideanBilinearProduct(mv1, mv2, IsZeroEuclideanFdp);
        }

        public static GaMultivector EuclideanHip(this GaMultivector mv1, GaMultivector mv2)
        {
            return EuclideanBilinearProduct(mv1, mv2, IsZeroEuclideanHip);
        }

        public static GaMultivector EuclideanCp(this GaMultivector mv1, GaMultivector mv2)
        {
            return EuclideanBilinearProduct(mv1, mv2, IsZeroEuclideanCp);
        }

        public static GaMultivector EuclideanAcp(this GaMultivector mv1, GaMultivector mv2)
        {
            return EuclideanBilinearProduct(mv1, mv2, IsZeroEuclideanAcp);
        }

        public static MathematicaScalar EuclideanMagnitude(this GaMultivector mv)
        {
            return EuclideanSp(mv, mv.Reverse())[0].Sqrt();
        }

        public static MathematicaScalar EuclideanMagnitude2(this GaMultivector mv)
        {
            return EuclideanSp(mv, mv.Reverse())[0];
        }



        //public static void AddTerm(this Dictionary<int, List<Expr>> accumExprDict, int resultId, Expr resultCoefDelta)
        //{
        //    List<Expr> resultCoefTermsList;

        //    if (accumExprDict.TryGetValue(resultId, out resultCoefTermsList))
        //        accumExprDict[resultId].Add(resultCoefDelta);

        //    else
        //    {
        //        resultCoefTermsList = new List<Expr>(1) {resultCoefDelta};
        //        accumExprDict.Add(resultId, resultCoefTermsList);
        //    }
        //}

        //public static GaMultivectorCoefficients ToMultivector(this Dictionary<int, List<Expr>> accumExprDict, int gaSpaceDim)
        //{
        //    var resultMv = GaMultivectorCoefficients.CreateZero(gaSpaceDim);

        //    foreach (var pair in accumExprDict)
        //    {
        //        var resultCoef = SymbolicUtils.Cas[new Expr(Mfs.Plus[pair.Value.ToArray()])];

        //        if (!(resultCoef.NumberQ() && resultCoef.ToString() == "0"))
        //            resultMv.Add(pair.Key, MathematicaScalar.Create(SymbolicUtils.Cas, resultCoef));
        //    }

        //    return resultMv;
        //}

        //public static GaMultivectorCoefficients OuterProduct(GaMultivectorCoefficients mv1, GaMultivectorCoefficients mv2)
        //{
        //    if (mv1.GaSpaceDim != mv2.GaSpaceDim)
        //        throw new GMacSymbolicException("GA space dimension must be equal for outer product of multivectors");

        //    var resultMv = CreateZero(mv1.GaSpaceDim);

        //    foreach (var term1 in mv1)
        //    {
        //        var id1 = term1.Key;
        //        var coef1 = term1.Value;

        //        foreach (var term2 in mv2.FilterTermsForOp(id1))
        //        {
        //            var id2 = term2.Key;
        //            var coef2 = term2.Value;

        //            var id = id1 ^ id2;

        //            if (GaUtils.IDs_To_EGP_Sign(id1, id2))
        //                resultMv[id] -= coef1 * coef2;
        //            else
        //                resultMv[id] += coef1 * coef2;
        //        }
        //    }

        //    return resultMv;
        //}

        //private GAMultivectorCoefficients EuclideanBilinearProduct(GAMultivectorCoefficients mv1, GAMultivectorCoefficients mv2, Func<int, int, bool> term_discard_function)
        //{
        //    if (mv1.GASpaceDim != mv2.GASpaceDim || mv1.GASpaceDim != GASpaceDim)
        //        throw new GMacSymbolicException("Multivector size mismatch");

        //    GAMultivectorCoefficients result_mv = GAMultivectorCoefficients.CreateZero(mv1.GASpaceDim);

        //    foreach (var term1 in mv1)
        //    {
        //        int id1 = term1.Key;
        //        MathematicaScalar coef1 = term1.Value;

        //        foreach (var term2 in mv2.FilterTermsUsing(id1, term_discard_function))
        //        {
        //            int id2 = term2.Key;
        //            MathematicaScalar coef2 = term2.Value;

        //            int id = id1 ^ id2;

        //            if (GAUtils.IDs_To_EGP_Sign(id1, id2))
        //                result_mv[id] -= coef1 * coef2;
        //            else
        //                result_mv[id] += coef1 * coef2;
        //        }
        //    }

        //    return result_mv;
        //}
    }
}
