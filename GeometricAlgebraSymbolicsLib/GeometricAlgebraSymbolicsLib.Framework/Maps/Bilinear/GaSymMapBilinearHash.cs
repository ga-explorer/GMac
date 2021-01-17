using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using GeometricAlgebraSymbolicsLib.Products;
using TextComposerLib.Text.Tabular;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    public sealed class GaSymMapBilinearHash : GaSymMapBilinear
    {
        public static GaSymMapBilinearHash Create(int targetVSpaceDim)
        {
            return new GaSymMapBilinearHash(
                targetVSpaceDim
            );
        }

        public static GaSymMapBilinearHash Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapBilinearHash(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }
        
        public static GaSymMapBilinearHash CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaSymOp(vSpaceDimension).ToHashMap();
        }

        public static GaSymMapBilinearHash CreateFromOuterProduct(IGaFrame frame)
        {
            return new GaSymOp(frame.VSpaceDimension).ToHashMap();
        }


        private readonly GaHashTable2D<IGaSymMultivector> _basisBladesMaps
            = new GaHashTable2D<IGaSymMultivector>(GaSymMultivectorUtils.IsNullOrZero);


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaSymMultivector this[int id1, int id2]
        {
            get
            {
                _basisBladesMaps.TryGetValue(id1, id2, out var resultMv);

                return resultMv
                       ?? GaSymMultivectorTerm.CreateZero(TargetGaSpaceDimension);
            }
        }

        public int TargetMultivectorsCount
            => _basisBladesMaps.Count;

        public int TargetMultivectorTermsCount
            => _basisBladesMaps.Sum(p => p.Item3.TermsCount);


        private GaSymMapBilinearHash(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaSymMapBilinearHash(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaSymMapBilinearHash SetBasisBladesMap(int id1, int id2, IGaSymMultivector value)
        {
            Debug.Assert(ReferenceEquals(value, null) || value.VSpaceDimension == TargetVSpaceDimension);

            _basisBladesMaps[id1, id2] = value.Compactify(true);

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            _basisBladesMaps.TryGetValue(id1, id2, out var resultMv);

            return resultMv?.ToTempMultivector() 
                   ?? GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var tempMv = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var biTerm in mv1.GetBiTermsForEGp(mv2))
            {
                _basisBladesMaps.TryGetValue(biTerm.Id1, biTerm.Id2, out var basisBladeMv);
                if (ReferenceEquals(basisBladeMv, null))
                    continue;

                tempMv.AddFactors(biTerm.ValuesProduct, basisBladeMv);
            }

            return tempMv;
        }

        public override IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisBladesMaps()
        {
            return _basisBladesMaps.Where(p => !p.Item3.IsNullOrZero());
        }

        public override string ToString()
        {
            var tableText = new TableComposer(TargetGaSpaceDimension, TargetGaSpaceDimension);
            var basisBladeIds = GaFrameUtils.BasisBladeIDs(TargetVSpaceDimension).ToArray();
            
            foreach (var basisBladeId in basisBladeIds)
            {
                tableText.ColumnsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
                tableText.RowsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
            }

            foreach (var pair in _basisBladesMaps)
                tableText.Items[pair.Item1, pair.Item2] =
                    pair.Item3.ToString();

            var text = tableText.ToString();

            return text;
        }
    }
}