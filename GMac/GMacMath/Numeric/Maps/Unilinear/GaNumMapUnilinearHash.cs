using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GMac.GMacMath.Numeric.Frames;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.GMacMath.Numeric.Maps.Unilinear
{
    public sealed class GaNumMapUnilinearHash : GaNumMapUnilinear
    {
        public static GaNumMapUnilinearHash Create(int targetVSpaceDim)
        {
            return new GaNumMapUnilinearHash(
                targetVSpaceDim
            );
        }

        public static GaNumMapUnilinearHash Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapUnilinearHash(
                domainVSpaceDim,
                targetVSpaceDim
                );
        }

        public static GaNumMapUnilinearHash Create(IGaNumMapUnilinear linearMap, IEnumerable<int> idsList)
        {
            var table = new GaNumMapUnilinearHash(
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


        private readonly GaNumMultivectorHashTable1D _basisBladeMaps
            = new GaNumMultivectorHashTable1D();


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1]
        {
            get
            {
                IGaNumMultivector basisBladeMv;
                _basisBladeMaps.TryGetValue(id1, out basisBladeMv);

                return basisBladeMv
                       ?? GaNumMultivector.CreateZero(TargetGaSpaceDimension);

            }
        }


        private GaNumMapUnilinearHash(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapUnilinearHash(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaNumMapUnilinearHash ClearBasisBladesMaps()
        {
            _basisBladeMaps.Clear();
            return this;
        }

        public GaNumMapUnilinearHash SetBasisBladeMap(int basisBladeId, IGaNumMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            _basisBladeMaps[basisBladeId] = targetMv.Compactify(true);

            return this;
        }

        public GaNumMapUnilinearHash RemoveBasisBladesMap(int id1)
        {
            _basisBladeMaps.Remove(id1);
            return this;
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GMacNumericException("Multivector size mismatch");

            var tempMv = GaNumMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term1 in mv1.NonZeroTerms)
            {
                IGaNumMultivector basisBladeMv;
                _basisBladeMaps.TryGetValue(term1.Key, out basisBladeMv);
                if (ReferenceEquals(basisBladeMv, null))
                    continue;

                tempMv.AddFactors(term1.Value, basisBladeMv);
            }

            return tempMv;
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            return
                _basisBladeMaps
                    .Where(p => !p.Value.IsNullOrZero())
                    .Select(
                        pair => new Tuple<int, IGaNumMultivector>(pair.Key, pair.Value)
                        );
        }
    }
}
