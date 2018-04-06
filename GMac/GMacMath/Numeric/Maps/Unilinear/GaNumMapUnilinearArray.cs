using System;
using System.Collections.Generic;
using System.Diagnostics;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;

namespace GMac.GMacMath.Numeric.Maps.Unilinear
{
    public sealed class GaNumMapUnilinearArray : GaNumMapUnilinear
    {
        public static GaNumMapUnilinearArray Create(int targetVSpaceDim)
        {
            return new GaNumMapUnilinearArray(
                targetVSpaceDim
            );
        }

        public static GaNumMapUnilinearArray Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapUnilinearArray(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaNumMapUnilinearArray Create(IGaNumMapUnilinear linearMap, IEnumerable<int> idsList)
        {
            var table = new GaNumMapUnilinearArray(
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


        private readonly IGaNumMultivector[] _basisBladeMaps;


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1] 
            => _basisBladeMaps[id1]
                ?? GaNumMultivectorTerm.CreateZero(TargetGaSpaceDimension);


        private GaNumMapUnilinearArray(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new IGaNumMultivector[DomainGaSpaceDimension];
        }

        private GaNumMapUnilinearArray(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new IGaNumMultivector[DomainGaSpaceDimension];
        }


        public GaNumMapUnilinearArray ClearBasisBladesMaps()
        {
            for (var id = 0; id < DomainGaSpaceDimension; id++)
                _basisBladeMaps[id] = null;

            return this;
        }

        public GaNumMapUnilinearArray SetBasisBladeMap(int basisBladeId, IGaNumMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            _basisBladeMaps[basisBladeId] = targetMv.Compactify(true);

            return this;
        }

        public GaNumMapUnilinearArray RemoveBasisBladesMap(int id1)
        {
            _basisBladeMaps[id1] = null;

            return this;
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GMacNumericException("Multivector size mismatch");

            var tempMv = GaNumMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term1 in mv1.NonZeroTerms)
            {
                var basisBladeMv = _basisBladeMaps[term1.Key];
                if (ReferenceEquals(basisBladeMv, null))
                    continue;

                tempMv.AddFactors(term1.Value, basisBladeMv);
            }

            return tempMv;
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            for (var id = 0; id < DomainGaSpaceDimension; id++)
            {
                var basisBladeMv = _basisBladeMaps[id];
                if (!ReferenceEquals(basisBladeMv, null))
                    yield return new Tuple<int, IGaNumMultivector>(id, basisBladeMv);
            }
        }
    }
}
