using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public sealed class GaNumMapUnilinearMatrix : GaNumMapUnilinear
    {
        public static GaNumMapUnilinearMatrix Create(Matrix mappingMatrix)
        {
            return new GaNumMapUnilinearMatrix(mappingMatrix);
        }


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1]
            => GaNumSarMultivector.CreateFromColumn(InternalMappingMatrix, id1);

        public override GaNumSarMultivector this[GaNumSarMultivector mv1]
            => InternalMappingMatrix.MapMultivector(mv1);

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1]
            => InternalMappingMatrix.MapMultivector(mv1);


        private GaNumMapUnilinearMatrix(Matrix mappingMatrix)
        {
            InternalMappingMatrix = mappingMatrix;
            TargetVSpaceDimension = mappingMatrix.ColumnCount.ToVSpaceDimension();
            DomainVSpaceDimension = mappingMatrix.RowCount.ToVSpaceDimension();
        }


        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            for (var id = 0; id < DomainGaSpaceDimension; id++)
            {
                var mv = this[id];

                if (!mv.IsNullOrEmpty())
                    yield return Tuple.Create(id, mv);
            }
        }
    }
}
