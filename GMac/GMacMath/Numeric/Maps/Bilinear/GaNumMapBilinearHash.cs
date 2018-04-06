using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;
using GMac.GMacMath.Numeric.Products;
using GMac.GMacMath.Structures;
using TextComposerLib.Text.Tabular;

namespace GMac.GMacMath.Numeric.Maps.Bilinear
{
    public sealed class GaNumMapBilinearHash : GaNumMapBilinear
    {
        public static GaNumMapBilinearHash Create(int targetVSpaceDim)
        {
            return new GaNumMapBilinearHash(
                targetVSpaceDim
            );
        }

        public static GaNumMapBilinearHash Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapBilinearHash(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaNumMapBilinearHash CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaNumOp(vSpaceDimension).ToHashMap();
        }

        public static GaNumMapBilinearHash CreateFromOuterProduct(IGMacFrame frame)
        {
            return new GaNumOp(frame.VSpaceDimension).ToHashMap();
        }


        private readonly GMacHashTable2D<IGaNumMultivector> _basisBladesMaps
            = new GMacHashTable2D<IGaNumMultivector>(GaNumMultivectorUtils.IsNullOrZero);


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public int TargetMultivectorsCount 
            => _basisBladesMaps.Count;

        public int TargetMultivectorTermsCount
            => _basisBladesMaps.Sum(p => p.Item3.TermsCount);

        public override IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                IGaNumMultivector resultMv;
                _basisBladesMaps.TryGetValue(id1, id2, out resultMv);

                return resultMv
                       ?? GaNumMultivector.CreateZero(TargetGaSpaceDimension);
            }
        }


        private GaNumMapBilinearHash(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapBilinearHash(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaNumMapBilinearHash ClearBasisBladesMaps()
        {
            _basisBladesMaps.Clear();
            return this;
        }

        public GaNumMapBilinearHash SetBasisBladesMap(int id1, int id2, IGaNumMultivector value)
        {
            Debug.Assert(ReferenceEquals(value, null) || value.VSpaceDimension == TargetVSpaceDimension);

            _basisBladesMaps[id1, id2] = value.Compactify(true);

            return this;
        }

        public GaNumMapBilinearHash RemoveBasisBladesMap(int id1, int id2)
        {
            _basisBladesMaps.Remove(id1, id2);
            return this;
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GMacNumericException("Multivector size mismatch");

            var tempMv = GaNumMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var biTerm in mv1.GetBiTermsForEGp(mv2))
            {
                IGaNumMultivector basisBladeMv;
                _basisBladesMaps.TryGetValue(biTerm.Id1, biTerm.Id2, out basisBladeMv);
                if (ReferenceEquals(basisBladeMv, null))
                    continue;

                tempMv.AddFactors(biTerm.ValuesProduct, basisBladeMv);
            }

            return tempMv;
        }

        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            return _basisBladesMaps.Where(p => !p.Item3.IsNullOrZero());
        }

        public override string ToString()
        {
            var tableText = new TableComposer(TargetGaSpaceDimension, TargetGaSpaceDimension);
            var basisBladeIds = GMacMathUtils.BasisBladeIDs(TargetVSpaceDimension).ToArray();

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
