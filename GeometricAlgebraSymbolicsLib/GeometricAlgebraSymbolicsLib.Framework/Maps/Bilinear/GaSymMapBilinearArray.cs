using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using GeometricAlgebraSymbolicsLib.Products;
using TextComposerLib.Text.Tabular;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    public sealed class GaSymMapBilinearArray : GaSymMapBilinear
    {
        public static GaSymMapBilinearArray Create(int targetVSpaceDim)
        {
            return new GaSymMapBilinearArray(
                targetVSpaceDim
            );
        }

        public static GaSymMapBilinearArray Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapBilinearArray(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaSymMapBilinearArray CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaSymOp(vSpaceDimension).ToArrayMap();
        }

        public static GaSymMapBilinearArray CreateFromOuterProduct(IGaFrame frame)
        {
            return new GaSymOp(frame.VSpaceDimension).ToArrayMap();
        }


        private readonly IGaSymMultivector[,] _basisBladesMaps;

            
        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaSymMultivector this[ulong id1, ulong id2] 
            => _basisBladesMaps[id1, id2] 
               ?? GaSymMultivector.CreateZero(TargetVSpaceDimension);


        private GaSymMapBilinearArray(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            _basisBladesMaps = new IGaSymMultivector[DomainGaSpaceDimension, DomainGaSpaceDimension];
        }

        private GaSymMapBilinearArray(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            _basisBladesMaps = new IGaSymMultivector[DomainGaSpaceDimension, DomainGaSpaceDimension];
        }


        public GaSymMapBilinearArray SetBasisBladesMap(ulong basisBladeId1, ulong basisBladeId2, IGaSymMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            _basisBladesMaps[basisBladeId1, basisBladeId2] = targetMv.Compactify(true);

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2)
        {
            return _basisBladesMaps[id1, id2]?.ToTempMultivector() 
                   ?? GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var tempMv = GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);

            foreach (var biTerm in mv1.GetBiTermsForEGp(mv2))
            {
                var basisBladeMv = _basisBladesMaps[biTerm.Id1, biTerm.Id2];
                if (ReferenceEquals(basisBladeMv, null))
                    continue;

                tempMv.AddFactors(biTerm.ValuesProduct, basisBladeMv);
            }
            
            return tempMv;
        }

        public override IEnumerable<Tuple<ulong, ulong, IGaSymMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0UL; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0UL; id2 < DomainGaSpaceDimension; id2++)
            {
                var mv = _basisBladesMaps[id1, id2];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<ulong, ulong, IGaSymMultivector>(id1, id2, mv);
            }
        }


        public override string ToString()
        {
            var tableText = new TableComposer((int)TargetGaSpaceDimension, (int)TargetGaSpaceDimension);
            var basisBladeIds = GaFrameUtils.BasisBladeIDs(TargetVSpaceDimension).ToArray();

            foreach (var basisBladeId in basisBladeIds)
            {
                tableText.ColumnsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
                tableText.RowsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
            }

            for (var basisBladeId1 = 0UL; basisBladeId1 < TargetGaSpaceDimension; basisBladeId1++)
                for (var basisBladeId2 = 0UL; basisBladeId2 < TargetGaSpaceDimension; basisBladeId2++)
                {
                    var mv = _basisBladesMaps[basisBladeId1, basisBladeId2];
                    if (mv != null)
                        tableText.Items[basisBladeId1, basisBladeId2] = mv.ToString();
                }

            var text = tableText.ToString();

            return text;
        }
    }
}