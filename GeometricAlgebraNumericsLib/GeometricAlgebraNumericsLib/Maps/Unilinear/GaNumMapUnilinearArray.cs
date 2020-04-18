using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
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

                if (!resultMv.IsNullOrEmpty())
                    table._basisBladeMaps[id1] = resultMv;
            }

            return table;
        }


        private readonly IGaNumMultivector[] _basisBladeMaps;


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1] 
            => _basisBladeMaps[id1]
                ?? GaNumTerm.CreateZero(TargetGaSpaceDimension);

        public override GaNumSarMultivector this[GaNumSarMultivector mv]
        {
            get
            {
                if (mv.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                foreach (var term1 in mv.GetNonZeroTerms())
                {
                    var basisBladeMv = _basisBladeMaps[term1.BasisBladeId];
                    if (ReferenceEquals(basisBladeMv, null))
                        continue;

                    resultMv.AddTerms(term1.ScalarValue, basisBladeMv);
                }

                return resultMv.GetSarMultivector();
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv]
        {
            get
            {
                if (mv.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = new GaNumDgrMultivectorFactory(TargetVSpaceDimension);

                foreach (var term1 in mv.GetNonZeroTerms())
                {
                    var basisBladeMv = _basisBladeMaps[term1.BasisBladeId];
                    if (ReferenceEquals(basisBladeMv, null))
                        continue;

                    resultMv.AddTerms(term1.ScalarValue, basisBladeMv);
                }

                return resultMv.GetDgrMultivector();
            }
        }


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
