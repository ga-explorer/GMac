using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Rendering.LaTeX;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo;
using GeometricAlgebraNumericsLib.Structures.Collections;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    public static class GaNumMultivectorUtils
    {
        public static GaMultivectorMutableImplementation DefaultTempMultivectorKind { get; set; } 
            = GaMultivectorMutableImplementation.DenseArrayRepresentation;

        public static bool DisableCompactifyMultivectors { get; set; } = false;


        public static GaNumVector CreateImuVectorMultivector(this IReadOnlyList<double> scalarValues)
        {
            return GaNumVector.Create(scalarValues);
        }


        public static double[] GetVectorPartValues(this IGaNumMultivector mv)
        {
            var array = new double[mv.VSpaceDimension];

            for (var i = 0; i < mv.VSpaceDimension; i++)
                array[i] = mv[1, i];

            return array;
        }

        public static double[] GetKVectorPartValues(this IGaNumMultivector mv, int grade)
        {
            var n = GaFrameUtils.KvSpaceDimension(mv.VSpaceDimension, grade);
            var array = new double[n];

            for (var i = 0; i < n; i++)
                array[i] = mv[grade, i];

            return array;
        }

        ///// <summary>
        ///// Element-wise product of two multivectors. The result is a multivector with each coefficient
        ///// equal to the product of corresponding coefficients of the two multivectors. This is mainly
        ///// used for implementing efficient linear maps on multivectors.
        ///// </summary>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static GaNumMultivector Ewp(this GaNumMultivector mv1, GaNumMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var resultMv = GaNumMultivector.CreateZero(mv1.GaSpaceDimension);

        //    var mvStack1 = mv1.TermsTree.CreateNodesStack();
        //    var mvStack2 = mv2.TermsTree.CreateNodesStack();

        //    while (mvStack1.Count > 0)
        //    {
        //        var node1 = mvStack1.Pop();
        //        var node2 = mvStack2.Pop();
        //        var id = node1.Id;

        //        if (node1.IsLeafNode)
        //        {
        //            resultMv.AddTerm((int)id, node1.Value * node2.Value);

        //            continue;
        //        }

        //        if (node1.HasChildNode0 && node2.HasChildNode0)
        //        {
        //            mvStack1.Push(node1.ChildNode0);
        //            mvStack2.Push(node2.ChildNode0);
        //        }

        //        if (node1.HasChildNode1 && node2.HasChildNode1)
        //        {
        //            mvStack1.Push(node1.ChildNode1);
        //            mvStack2.Push(node2.ChildNode1);
        //        }
        //    }

        //    return resultMv;
        //}

        /// <summary>
        /// This product treats the first multivector as a row and the second as a column and apply
        /// standard matrix multiplication to get a single scalar. This is mainly used for implementing
        /// efficient linear maps on multivectors.
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static double RowColumnProduct(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var result = 0.0d;

            var mvStack1 = mv1.BtrRootNode.CreateNodesStack();
            var mvStack2 = mv2.BtrRootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                
                if (node1.IsLeafNode)
                {
                    result += node1.Value * node2.Value;

                    continue;
                }

                if (node1.HasChildNode0 && node2.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode0);
                    mvStack2.Push(node2.ChildNode0);
                }

                if (node1.HasChildNode1 && node2.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);
                }
            }

            return result;
        }

        /// <summary>
        /// This product treats the first multivector as a row and the second as a column and apply
        /// standard matrix multiplication to get a single scalar. This is mainly used for implementing
        /// efficient linear maps on multivectors.
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static double RowColumnProduct(this GaNumSarMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var result = 0.0d;

            var mvStack1 = mv1.BtrRootNode.CreateNodesStack();
            var mvStack2 = GaBinaryTreeGradedNodeInfo1<double>.CreateStack(mv2.VSpaceDimension, (ulong)mv2.GetStoredGradesBitPattern());

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();

                if (node1.IsLeafNode)
                {
                    result += node1.Value * mv2[(int)node2.Id];

                    continue;
                }

                if (node1.HasChildNode0 && node2.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode0);
                    mvStack2.Push(node2.GetChildNodeInfo0());
                }

                if (node1.HasChildNode1 && node2.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.GetChildNodeInfo1());
                }
            }

            return result;
        }

        /// <summary>
        /// This product treats the first multivector as a row and the second as a column and apply
        /// standard matrix multiplication to get a single scalar. This is mainly used for implementing
        /// efficient linear maps on multivectors.
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static double RowColumnProduct(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var result = 0.0d;

            for (var grade = 0; grade <= mv1.VSpaceDimension; grade++)
            {
                if (!mv1.ContainsStoredTermOfGrade(grade) || !mv2.ContainsStoredTermOfGrade(grade)) 
                    continue;

                var kvSpaceDim = GaFrameUtils.KvSpaceDimension(mv1.VSpaceDimension, grade);

                for (var index = 0; index < kvSpaceDim; index++)
                    result += mv1[grade, index] * mv2[grade, index];
            }

            return result;
        }

        
        public static bool IsNullOrZero(this IGaNumMultivector mv)
        {
            return ReferenceEquals(mv, null) || mv.IsZero();
        }

        public static bool IsNullOrEmpty(this IGaNumMultivector mv)
        {
            return ReferenceEquals(mv, null) || mv.IsEmpty();
        }

        //public static IGaNumMultivectorTemp ToTempMultivector(this IGaNumMultivector mv)
        //{
        //    if (ReferenceEquals(mv, null))
        //        return null;

        //    var tempMv = GaNumMultivector.CreateZero(mv.GaSpaceDimension);

        //    foreach (var term in mv.NonZeroTerms)
        //        tempMv.SetTermCoef(term.Key, term.Value);

        //    return tempMv;
        //}

        //TODO: Revise this
        public static IGaNumMultivector Compactify(this IGaNumMultivector mv, bool returnZeroMvAsNull = false)
        {
            if (DisableCompactifyMultivectors)
                return mv;

            if (ReferenceEquals(mv, null))
                return null;

            //If it's zero return null or a zero term depending on returnZeroMvAsNull
            if (mv.IsZero())
                return returnZeroMvAsNull 
                    ? null 
                    : GaNumTerm.CreateZero(mv.VSpaceDimension);

            //If it's a non-zero term return it as is
            if (mv is GaNumTerm termMv)
                return termMv;

            //It's a full multivector
            var fullMv = (GaNumSarMultivector)mv;

            //If it's not a term return it after simplification
            if (!mv.IsTerm())
                return fullMv;

            //It's a full multivector containing a single term
            var term = mv.GetNonZeroTerms().FirstOrDefault();
            return GaNumTerm.Create(mv.VSpaceDimension, term.BasisBladeId, term.ScalarValue);
        }

        public static GaNumMultivectorHashTable1D Compactify(this GaSparseTable1D<int, GaNumSarMultivector> mvTable)
        {
            var resultTable = new GaNumMultivectorHashTable1D();

            foreach (var pair in mvTable)
            {
                var id = pair.Key;
                var mv = pair.Value.Compactify(true);

                if (!ReferenceEquals(mv, null))
                    resultTable[id] = mv;
            }

            return resultTable;
        }


        public static double[] GetDenseScalarValuesArray(this IGaNumMultivector mv)
        {
            var scalarValuesArray = new double[mv.GaSpaceDimension];

            for (var id = 0; id < mv.GaSpaceDimension; id++)
                scalarValuesArray[id] = mv[id];

            return scalarValuesArray;
        }


        public static IEnumerable<GaTerm<double>> GetNegativeTerms(this IGaNumMultivector mv1)
        {
            return mv1.GetNonZeroTerms().GaNegative();
        }

        public static IEnumerable<GaTerm<double>> GetReverseTerms(this IGaNumMultivector mv1)
        {
            return mv1.GetNonZeroTerms().GaReverse();
        }

        public static IEnumerable<GaTerm<double>> GetGradeInvTerms(this IGaNumMultivector mv1)
        {
            return mv1.GetNonZeroTerms().GaGradeInv();
        }

        public static IEnumerable<GaTerm<double>> GetCliffConjTerms(this IGaNumMultivector mv1)
        {
            return mv1.GetNonZeroTerms().GaCliffConj();
        }

        public static IEnumerable<GaTerm<double>> GetAdditionTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return mv1.GetNonZeroTerms().Concat(
                mv2.GetNonZeroTerms()
            );
        }

        public static IEnumerable<GaTerm<double>> GetSubtractionTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return mv1.GetNonZeroTerms().Concat(
                mv2.GetNonZeroTerms().GaNegative()
            );
        }

        public static IEnumerable<GaTerm<double>> GetScaledTerms(this IGaNumMultivector mv1, double scalingFactor)
        {
            return mv1.GetNonZeroTerms().GaScaleBy(scalingFactor);
        }

        public static IEnumerable<GaTerm<double>> GetEvenGradeTerms(this IGaNumMultivector mv1)
        {
            return mv1.GetNonZeroTerms().Where(t => t.BasisBladeId.BasisBladeHasEvenGrade());
        }

        public static IEnumerable<GaTerm<double>> GetOddGradeTerms(this IGaNumMultivector mv1)
        {
            return mv1.GetNonZeroTerms().Where(t => t.BasisBladeId.BasisBladeHasOddGrade());
        }


        public static GaNumSarMultivector Negative(this GaNumSarMultivector mv)
        {
            return mv.GetNegative().GetSarMultivector();
        }

        public static GaNumSarMultivector Reverse(this GaNumSarMultivector mv)
        {
            return mv.GetReverse().GetSarMultivector();
        }

        public static GaNumSarMultivector GradeInv(this GaNumSarMultivector mv)
        {
            return mv.GetGradeInv().GetSarMultivector();
        }

        public static GaNumSarMultivector CliffConj(this GaNumSarMultivector mv)
        {
            return mv.GetCliffConj().GetSarMultivector();
        }


        public static GaNumSgrMultivector Negative(this GaNumSgrMultivector mv)
        {
            return mv.GetNegative().GetSgrMultivector();
        }

        public static GaNumSgrMultivector Reverse(this GaNumSgrMultivector mv)
        {
            return mv.GetReverse().GetSgrMultivector();
        }

        public static GaNumSgrMultivector GradeInv(this GaNumSgrMultivector mv)
        {
            return mv.GetGradeInv().GetSgrMultivector();
        }

        public static GaNumSgrMultivector CliffConj(this GaNumSgrMultivector mv)
        {
            return mv.GetCliffConj().GetSgrMultivector();
        }


        public static GaNumDarMultivector Negative(this GaNumDarMultivector mv)
        {
            return mv.GetNegative().GetDarMultivector();
        }

        public static GaNumDarMultivector Reverse(this GaNumDarMultivector mv)
        {
            return mv.GetReverse().GetDarMultivector();
        }

        public static GaNumDarMultivector GradeInv(this GaNumDarMultivector mv)
        {
            return mv.GetGradeInv().GetDarMultivector();
        }

        public static GaNumDarMultivector CliffConj(this GaNumDarMultivector mv)
        {
            return mv.GetCliffConj().GetDarMultivector();
        }


        public static GaNumDgrMultivector Negative(this GaNumDgrMultivector mv)
        {
            return mv.GetNegative().GetDgrMultivector();
        }

        public static GaNumDgrMultivector Reverse(this GaNumDgrMultivector mv)
        {
            return mv.GetReverse().GetDgrMultivector();
        }

        public static GaNumDgrMultivector GradeInv(this GaNumDgrMultivector mv)
        {
            return mv.GetGradeInv().GetDgrMultivector();
        }

        public static GaNumDgrMultivector CliffConj(this GaNumDgrMultivector mv)
        {
            return mv.GetCliffConj().GetDgrMultivector();
        }


        public static bool IsEqualTo(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var termsDictionary = new Dictionary<int, double>();

            foreach (var term in mv1.GetStoredTerms())
            {
                if (termsDictionary.ContainsKey(term.BasisBladeId))
                    termsDictionary[term.BasisBladeId] = term.ScalarValue;
                else
                    termsDictionary.Add(term.BasisBladeId, term.ScalarValue);
            }

            foreach (var term in mv2.GetStoredTerms())
            {
                if (termsDictionary.ContainsKey(term.BasisBladeId))
                    termsDictionary[term.BasisBladeId] -= term.ScalarValue;
                else
                    termsDictionary.Add(term.BasisBladeId, -term.ScalarValue);
            }

            return termsDictionary.All(p => p.Value.IsNearZero());
        }


        public static string GetGaNumMultivectorText(this IGaNumMultivector mv)
        {
            return mv.GetNonZeroTerms().GetNumTermsListText();
        }

        public static string GetGaNumMultivectorLaTeX(this IGaNumMultivector mv)
        {
            var basisVectorNames = 
                Enumerable
                    .Range(1, mv.VSpaceDimension)
                    .Select(i => i.ToString())
                    .ToArray();

            var latexRenderer = 
                new GaNumLaTeXRenderer(GaLaTeXBasisBladeForm.BasisVectorsSubscripts, basisVectorNames);

            return latexRenderer.FormatNumMultivector(mv);
        }
    }
}
