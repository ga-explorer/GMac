using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public abstract class GaSymMapUnilinear : GaSymMap, IGaSymMapUnilinear
    {
        protected ISymbolicMatrix InternalMappingMatrix;

        public abstract int DomainVSpaceDimension { get; }

        public int DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public IGaSymMultivector this[int grade1, int index1]
            => this[GaNumFrameUtils.BasisBladeId(grade1, index1)];

        public ISymbolicMatrix MappingMatrix
        {
            get
            {
                if (ReferenceEquals(InternalMappingMatrix, null))
                    ComputeMappingMatrix();

                return InternalMappingMatrix;
            }
        }


        protected virtual void ComputeMappingMatrix()
        {
            var matrixItems = new Expr[TargetGaSpaceDimension, DomainGaSpaceDimension];

            for (var col = 0; col < DomainGaSpaceDimension; col++)
            {
                var mv = this[col];

                if (mv.IsNullOrZero())
                {
                    for (var row = 0; row < TargetGaSpaceDimension; row++)
                        matrixItems[row, col] = Expr.INT_ZERO;

                    continue;
                }

                for (var row = 0; row < TargetGaSpaceDimension; row++)
                {
                    var scalar = mv[row];

                    matrixItems[row, col] =
                        scalar.IsNullOrZero()
                            ? Expr.INT_ZERO
                            : scalar;
                }
            }

            InternalMappingMatrix = 
                MathematicaMatrix.CreateFullMatrix(GaSymbolicsUtils.Cas, matrixItems);
        }


        public virtual GaSymMapUnilinear Adjoint()
        {
            var exprArray = this.ToExprScalarsArray();

            var resultMap = GaSymMapUnilinearTree.Create(
                TargetVSpaceDimension,
                DomainVSpaceDimension
            );

            for (var id = 0; id < TargetGaSpaceDimension; id++)
            {
                var mv = GaSymMultivector.CreateFromRow(exprArray, id);

                if (!mv.IsNullOrZero())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public virtual GaSymMapUnilinear Inverse()
        {
            return this.ToMatrix().Inverse().ToTreeMap();
        }

        public virtual GaSymMapUnilinear InverseAdjoint()
        {
            return this.ToMatrix().InverseTranspose().ToTreeMap();
        }


        public abstract IGaSymMultivector this[int id1] { get; }

        public GaSymMultivector this[GaSymMultivector mv1]
            => MapToTemp(mv1).ToMultivector();

        public abstract IGaSymMultivectorTemp MapToTemp(int id1);

        public abstract IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1);

        public abstract IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps();

        public virtual IEnumerable<Tuple<int, IGaSymMultivector>> BasisVectorMaps()
        {
            for (var index = 0; index < DomainVSpaceDimension; index++)
            {
                var mv = this[GaNumFrameUtils.BasisBladeId(1, index)];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, IGaSymMultivector>(index, mv);
            }
        }
    }
}
