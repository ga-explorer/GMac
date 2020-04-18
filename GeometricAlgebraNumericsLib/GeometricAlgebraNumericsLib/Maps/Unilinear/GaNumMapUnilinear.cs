using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public abstract class GaNumMapUnilinear : GaNumMap, IGaNumMapUnilinear
    {
        protected Matrix InternalMappingMatrix;


        public abstract int DomainVSpaceDimension { get; }

        public int DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public IGaNumMultivector this[int grade1, int index1]
            => this[GaNumFrameUtils.BasisBladeId(grade1, index1)];

        public abstract IGaNumMultivector this[int id1] { get; }

        public abstract GaNumSarMultivector this[GaNumSarMultivector mv1] { get; }

        public abstract GaNumDgrMultivector this[GaNumDgrMultivector mv1] { get; }

        public virtual IGaNumMultivector DomainPseudoScalarMap 
            => this[DomainGaSpaceDimension - 1];

        public Matrix MappingMatrix
        {
            get
            {
                if (ReferenceEquals(InternalMappingMatrix, null))
                    GetMappingMatrix();

                return InternalMappingMatrix;
            }
        }


        public virtual Matrix GetMappingMatrix()
        {
            var matrixItems = new double[TargetGaSpaceDimension, DomainGaSpaceDimension];

            for (var col = 0; col < DomainGaSpaceDimension; col++)
            {
                var mv = this[col];

                if (mv.IsNullOrEmpty())
                    continue;

                for (var row = 0; row < TargetGaSpaceDimension; row++)
                    matrixItems[row, col] = mv[row];
            }

            InternalMappingMatrix = DenseMatrix.OfArray(matrixItems);

            return InternalMappingMatrix;
        }

        public virtual Matrix GetVectorsMappingMatrix()
        {
            var matrixItems = new double[TargetVSpaceDimension, DomainVSpaceDimension];

            foreach (var pair in BasisVectorMaps())
            {
                var col = pair.Item1;

                foreach (var term in pair.Item2)
                {
                    var row = term.BasisBladeId.BasisBladeIndex();

                    matrixItems[row, col] = term.ScalarValue;
                }
            }

            return DenseMatrix.OfArray(matrixItems);
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
                var mv = GaNumSarMultivector.CreateFromRow(exprArray, id);

                if (!mv.IsNullOrEmpty())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public virtual GaNumMapUnilinear Inverse()
        {
            return (this.ToDenseMatrix().Inverse() as Matrix).ToTreeMap();
        }

        public virtual GaNumMapUnilinear InverseAdjoint()
        {
            return (this.ToDenseMatrix().Inverse().Transpose() as Matrix).ToTreeMap();
        }


        public abstract IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps();

        public virtual IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps()
        {
            for (var index = 0; index < DomainVSpaceDimension; index++)
            {
                var mv = this[GaNumFrameUtils.BasisBladeId(1, index)];

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<int, IGaNumMultivector>(index, mv);
            }
        }
    }
}
