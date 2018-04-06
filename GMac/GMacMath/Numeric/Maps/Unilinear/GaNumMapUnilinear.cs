using System;
using System.Collections.Generic;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.GMacMath.Numeric.Maps.Unilinear
{
    public abstract class GaNumMapUnilinear : GaNumMap, IGaNumMapUnilinear
    {
        protected Matrix InternalMappingMatrix;

        public abstract int DomainVSpaceDimension { get; }

        public int DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public IGaNumMultivector this[int grade1, int index1]
            => this[GMacMathUtils.BasisBladeId(grade1, index1)];

        public abstract IGaNumMultivector this[int id1] { get; }

        public virtual GaNumMultivector this[GaNumMultivector mv1]
            => MapToTemp(mv1).ToMultivector();

        public Matrix MappingMatrix
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
            var matrixItems = new double[TargetGaSpaceDimension, DomainGaSpaceDimension];

            for (var col = 0; col < DomainGaSpaceDimension; col++)
            {
                var mv = this[col];

                if (mv.IsNullOrZero())
                    continue;

                for (var row = 0; row < TargetGaSpaceDimension; row++)
                    matrixItems[row, col] = mv[row];
            }

            InternalMappingMatrix = DenseMatrix.OfArray(matrixItems);
        }


        public virtual GaNumMapUnilinear Adjoint()
        {
            var exprArray = this.ToScalarsArray();

            var resultMap = GaNumMapUnilinearTree.Create(
                TargetVSpaceDimension,
                DomainVSpaceDimension
            );

            for (var id = 0; id < TargetGaSpaceDimension; id++)
            {
                var mv = GaNumMultivector.CreateFromRow(exprArray, id);

                if (!mv.IsNullOrZero())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public virtual GaNumMapUnilinear Inverse()
        {
            return (this.ToMatrix().Inverse() as Matrix).ToTreeMap();
        }

        public virtual GaNumMapUnilinear InverseAdjoint()
        {
            return (this.ToMatrix().Inverse().Transpose() as Matrix).ToTreeMap();
        }


        public abstract IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1);

        public abstract IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps();

        public virtual IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps()
        {
            for (var index = 0; index < DomainVSpaceDimension; index++)
            {
                var mv = this[GMacMathUtils.BasisBladeId(1, index)];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, IGaNumMultivector>(index, mv);
            }
        }
    }
}
