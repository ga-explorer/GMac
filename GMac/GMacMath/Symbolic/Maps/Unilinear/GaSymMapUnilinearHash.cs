using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Multivectors.Intermediate;
using SymbolicInterface.Mathematica.ExprFactory;

namespace GMac.GMacMath.Symbolic.Maps.Unilinear
{
    public sealed class GaSymMapUnilinearHash : GaSymMapUnilinear
    {
        public static GaSymMapUnilinearHash Create(int targetVSpaceDim)
        {
            return new GaSymMapUnilinearHash(
                targetVSpaceDim
            );
        }

        public static GaSymMapUnilinearHash Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapUnilinearHash(
                domainVSpaceDim, 
                targetVSpaceDim
                );
        }


        private readonly GaSymMultivectorHashTable1D _basisBladeMaps
            = new GaSymMultivectorHashTable1D();


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaSymMultivector this[int id1]
        {
            get
            {
                IGaSymMultivector basisBladeMv;
                _basisBladeMaps.TryGetValue(id1, out basisBladeMv);

                return basisBladeMv
                       ?? GaSymMultivector.CreateZero(TargetGaSpaceDimension);

            }
        }
        

        private GaSymMapUnilinearHash(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaSymMapUnilinearHash(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaSymMapUnilinearHash ClearBasisBladesMaps()
        {
            _basisBladeMaps.Clear();
            return this;
        }

        public GaSymMapUnilinearHash SetBasisBladeMap(int basisBladeId, IGaSymMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            _basisBladeMaps[basisBladeId] = targetMv.Compactify(true);

            return this;
        }

        public GaSymMapUnilinearHash RemoveBasisBladesMap(int id1)
        {
            _basisBladeMaps.Remove(id1);
            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1)
        {
            IGaSymMultivector basisBladeMv;
            _basisBladeMaps.TryGetValue(id1, out basisBladeMv);

            return basisBladeMv?.ToTempMultivector() 
                   ?? GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GMacSymbolicException("Multivector size mismatch");

            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term1 in mv1.NonZeroExprTerms)
            {
                var id1 = term1.Key;
                var coef1 = term1.Value;

                IGaSymMultivector basisBladeMv;
                _basisBladeMaps.TryGetValue(id1, out basisBladeMv);
                if (ReferenceEquals(basisBladeMv, null))
                    continue;

                foreach (var basisBladeMvTerm in basisBladeMv.NonZeroExprTerms)
                    tempMultivector.AddFactor(
                        basisBladeMvTerm.Key,
                        Mfs.Times[basisBladeMvTerm.Value, coef1]
                    );
            }

            return tempMultivector;
        }

        public override IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps()
        {
            return 
                _basisBladeMaps
                    .Where(p => !p.Value.IsNullOrZero())
                    .Select(
                        pair => new Tuple<int, IGaSymMultivector>(pair.Key, pair.Value)
                        );
        }
    }
}
