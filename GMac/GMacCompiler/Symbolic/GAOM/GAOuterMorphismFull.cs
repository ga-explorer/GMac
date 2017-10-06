using System.Collections.Generic;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib.Text.Structured;

namespace GMac.GMacCompiler.Symbolic.GAOM
{
    public sealed class GaOuterMorphismFull : GaOuterMorphism
    {
        public static GaOuterMorphismFull Create(GaMultivector[] transformedBasisBlades)
        {
            return new GaOuterMorphismFull(transformedBasisBlades);
        }

        public static GaOuterMorphismFull Create(ISymbolicMatrix vectorTransformMatrix)
        {
            var domainGaSpaceDim = FrameUtils.GaSpaceDimension(vectorTransformMatrix.Columns);
            var codomainGaSpaceDim = FrameUtils.GaSpaceDimension(vectorTransformMatrix.Rows);

            var transformedBasisBlades  = new GaMultivector[domainGaSpaceDim];

            transformedBasisBlades[0] = GaMultivector.CreateScalar(codomainGaSpaceDim, MathematicaScalar.Create(vectorTransformMatrix.CasInterface, 1));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                if (id.IsValidBasisVectorId())
                {
                    transformedBasisBlades[id] = CreateFromMatrixColumn(vectorTransformMatrix, id.BasisBladeIndex());
                }
                else
                {
                    int id1, id2;
                    id.SplitBySmallestBasicPattern(out id1, out id2);

                    transformedBasisBlades[id] = transformedBasisBlades[id1].OuterProduct(transformedBasisBlades[id2]);
                }
            }

            return new GaOuterMorphismFull(transformedBasisBlades);
        }


        private readonly GaMultivector[] _transformedBasisBlades;

        private MathematicaMatrix _vectorTransformMatrix;

        private MathematicaMatrix _multivectorTransformMatrix;


        private GaOuterMorphismFull(GaMultivector[] transformedBasisBlades)
        {
            _transformedBasisBlades = transformedBasisBlades;

            DomainVSpaceDim = FrameUtils.VSpaceDimension(_transformedBasisBlades.Length);

            CodomainVSpaceDim = FrameUtils.VSpaceDimension(_transformedBasisBlades[0].GaSpaceDim);
        }

        //public GAOuterMorphismFull(ISymbolicMatrix vector_transform_matrix)
        //    : base(vector_transform_matrix.CAS)
        //{
        //    int domain_ga_space_dim = GAUtils.GASpaceDim(vector_transform_matrix.Columns);
        //    int codomain_ga_space_dim = GAUtils.GASpaceDim(vector_transform_matrix.Rows);

        //    _TransformedBasisBlades  = new Multivector[domain_ga_space_dim];

        //    _TransformedBasisBlades[0] = Multivector.CreateScalar(codomain_ga_space_dim, MathematicaScalar.Create(CAS, 1));

        //    for (int id = 1 ; id <= domain_ga_space_dim - 1)
        //    {
        //        if (GAUtils.is_ID_Vector(id))
        //        {
        //            _TransformedBasisBlades[id] = CreateFromMatrixColumn(vector_transform_matrix, GAUtils.ID_To_Index(id));
        //        }
        //        else
        //        {
        //            int id1, id2;
        //            GAUtils.SeparateIDs(id, out id1, out id2);

        //            _TransformedBasisBlades[id] = Multivector.OuterProduct(_TransformedBasisBlades[id1], _TransformedBasisBlades[id2]);
        //        }
        //    }
        //}

        private void ComputeVectorTransformMatrix()
        {
            var matrixArray = new MathematicaScalar[CodomainVSpaceDim, DomainVSpaceDim];

            for (var col = 0; col < DomainVSpaceDim; col++)
            {
                var id = FrameUtils.BasisBladeId(1, col);
                var mv = _transformedBasisBlades[id];

                foreach (var term in mv)
                {
                    var row = term.Key.BasisBladeIndex();
                    matrixArray[row, col] = term.Value;
                }
            }

            _vectorTransformMatrix = MathematicaMatrix.CreateFullMatrix(CasInterface, matrixArray);
        }

        private void ComputeMultivectorTransformMatrix()
        {
            var matrixArray = new MathematicaScalar[CodomainGaSpaceDim, DomainGaSpaceDim];

            for (var col = 0; col < DomainGaSpaceDim; col++)
            {
                var mv = _transformedBasisBlades[col];

                foreach (var term in mv)
                    matrixArray[term.Key, col] = term.Value;
            }

            _multivectorTransformMatrix = MathematicaMatrix.CreateFullMatrix(CasInterface, matrixArray);
        }

        public override ISymbolicMatrix VectorTransformMatrix
        {
            get 
            {
                if (ReferenceEquals(_vectorTransformMatrix, null))
                    ComputeVectorTransformMatrix();

                return _vectorTransformMatrix;
            }
        }

        public override ISymbolicMatrix MultivectorTransformMatrix
        {
            get 
            {
                if (ReferenceEquals(_multivectorTransformMatrix, null))
                    ComputeMultivectorTransformMatrix();

                return _multivectorTransformMatrix;
            }
        }

        public override MathematicaScalar Determinant => _transformedBasisBlades[CodomainVSpaceDim - 1][0];

        public override int DomainVSpaceDim { get; }

        public override int CodomainVSpaceDim { get; }

        //TODO: This requires more acceleration (try to build the expressions then evaluate once per basis blade id for result_mv)
        //public override GAMultivectorCoefficients Transform(GAMultivectorCoefficients mv1)
        //{
        //    if (mv1.GASpaceDim != this.DomainGASpaceDim)
        //        throw new GMacSymbolicException("Multivector GA space dimension does not agree with domain GA space dimension");

        //    GAMultivectorCoefficients result_mv = GAMultivectorCoefficients.CreateZero(CodomainGASpaceDim);

        //    foreach (var term1 in mv1)
        //    {
        //        var coef1 = term1.Value;

        //        var mv2 = _TransformedBasisBlades[term1.Key];

        //        foreach (var term2 in mv2)
        //        {
        //            var coef2 = term2.Value;

        //            var result_id = term2.Key;

        //            result_mv[result_id] += coef1 * coef2;
        //        }
        //    }

        //    return result_mv;
        //}

        public override GaMultivector Transform(GaMultivector mv1)
        {
            if (mv1.GaSpaceDim != DomainGaSpaceDim)
                throw new GMacSymbolicException("Multivector GA space dimension does not agree with domain GA space dimension");

            var accumExprDict = new Dictionary<int, ListComposer>();

            var terms1 = mv1.ToStringsDictionary();

            foreach (var term1 in terms1)
            {
                var terms2 = _transformedBasisBlades[term1.Key].ToStringsDictionary();

                foreach (var term2 in terms2)
                    accumExprDict.AddTerm(term2.Key, EuclideanUtils.Times(term1.Value, term2.Value));
            }

            return accumExprDict.ToMultivector(CodomainGaSpaceDim);
        }

        public override GaOuterMorphism AdjointOm()
        {
            return Create(VectorTransformMatrix.Transpose());
        }

        public override GaOuterMorphism InverseOm()
        {
            return Create(VectorTransformMatrix.Inverse());
        }

        public override GaOuterMorphism InverseAdjointOm()
        {
            return Create(VectorTransformMatrix.InverseTranspose());
        }

        
        private static GaMultivector CreateFromMatrixColumn(ISymbolicMatrix matrix, int col)
        {
            var gaSpaceDim = FrameUtils.GaSpaceDimension(matrix.Rows);

            var mv = GaMultivector.CreateZero(gaSpaceDim);

            for (var index = 0; index < matrix.Rows; index++)
                mv[1, index] = matrix[index, col];

            return mv;
        }
    }
}
