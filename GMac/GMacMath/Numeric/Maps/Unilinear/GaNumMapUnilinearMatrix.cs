using System;
using System.Collections.Generic;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.GMacMath.Numeric.Maps.Unilinear
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
            => GaNumMultivector.CreateFromColumn(InternalMappingMatrix, id1);


        private GaNumMapUnilinearMatrix(Matrix mappingMatrix)
        {
            InternalMappingMatrix = mappingMatrix;
            TargetVSpaceDimension = mappingMatrix.ColumnCount.ToVSpaceDimension();
            DomainVSpaceDimension = mappingMatrix.RowCount.ToVSpaceDimension();
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1)
        {
            return InternalMappingMatrix.MapToTemp(mv1);
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            for (var id = 0; id < DomainGaSpaceDimension; id++)
            {
                var mv = this[id];

                if (!mv.IsZero())
                    yield return Tuple.Create(id, mv);
            }
        }
    }
}
