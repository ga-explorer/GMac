using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public sealed class GaSymMapUnilinearArray : GaSymMapUnilinear
    {
        public static GaSymMapUnilinearArray Create(int targetVSpaceDim)
        {
            return new GaSymMapUnilinearArray(
                targetVSpaceDim
            );
        }

        public static GaSymMapUnilinearArray Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapUnilinearArray(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaSymMapUnilinearArray Create(IGaSymMapUnilinear linearMap, IEnumerable<int> idsList)
        {
            var table = new GaSymMapUnilinearArray(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var id1 in idsList)
            {
                var resultMv = linearMap[id1];

                if (!resultMv.IsNullOrZero())
                    table._basisBladeMaps[id1] = resultMv;
            }

            return table;
        }


        private readonly IGaSymMultivector[] _basisBladeMaps;


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaSymMultivector this[int id1]
            => _basisBladeMaps[id1]
                ?? GaSymMultivectorTerm.CreateZero(TargetGaSpaceDimension);


        private GaSymMapUnilinearArray(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new IGaSymMultivector[DomainGaSpaceDimension];
        }

        private GaSymMapUnilinearArray(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new IGaSymMultivector[DomainGaSpaceDimension];
        }


        public GaSymMapUnilinearArray ClearBasisBladesMaps()
        {
            for (var id = 0; id < DomainGaSpaceDimension; id++)
                _basisBladeMaps[id] = null;

            return this;
        }

        public GaSymMapUnilinearArray SetBasisBladeMap(int basisBladeId, IGaSymMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            _basisBladeMaps[basisBladeId] = targetMv.Compactify(true);

            return this;
        }

        public GaSymMapUnilinearArray RemoveBasisBladesMap(int id1)
        {
            _basisBladeMaps[id1] = null;

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1)
        {
            return _basisBladeMaps[id1].ToTempMultivector();
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var tempMv = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term1 in mv1.NonZeroExprTerms)
            {
                var basisBladeMv = _basisBladeMaps[term1.Key];
                if (ReferenceEquals(basisBladeMv, null))
                    continue;

                tempMv.AddFactors(term1.Value, basisBladeMv);
            }

            return tempMv;
        }

        public override IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps()
        {
            for (var id = 0; id < DomainGaSpaceDimension; id++)
            {
                var basisBladeMv = _basisBladeMaps[id];
                if (!ReferenceEquals(basisBladeMv, null))
                    yield return new Tuple<int, IGaSymMultivector>(id, basisBladeMv);
            }
        }
    }
}
