using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public sealed class GaSymMapUnilinearMatrix : GaSymMapUnilinear
    {
        public static GaSymMapUnilinearMatrix Create(ISymbolicMatrix mappingMatrix)
        {
            return new GaSymMapUnilinearMatrix(mappingMatrix);
        }


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaSymMultivector this[int id1]
            => GaSymMultivector.CreateFromColumn(InternalMappingMatrix, id1);


        private GaSymMapUnilinearMatrix(ISymbolicMatrix mappingMatrix)
        {
            InternalMappingMatrix = mappingMatrix;
            TargetVSpaceDimension = mappingMatrix.ColumnCount.ToVSpaceDimension();
            DomainVSpaceDimension = mappingMatrix.RowCount.ToVSpaceDimension();
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1)
        {
            var columnVector = InternalMappingMatrix.GetColumn(id1);

            return GaSymMultivector.CreateCopyTemp(columnVector);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1)
        {
            var columnVector = InternalMappingMatrix.Times(mv1);

            return GaSymMultivector.CreateCopyTemp(columnVector);
        }

        public override IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps()
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
