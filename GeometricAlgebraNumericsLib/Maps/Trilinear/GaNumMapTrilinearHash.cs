using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;

namespace GeometricAlgebraNumericsLib.Maps.Trilinear
{
    public sealed class GaNumMapTrilinearHash : GaNumMapTrilinear
    {
        public static GaNumMapTrilinearHash Create(int targetVSpaceDim)
        {
            return new GaNumMapTrilinearHash(
                targetVSpaceDim
            );
        }

        public static GaNumMapTrilinearHash Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapTrilinearHash(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }


        private readonly GaNumMultivectorHashTable3D _basisBladesMaps
            = new GaNumMultivectorHashTable3D();


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1, int id2, int id3]
        {
            get
            {
                IGaNumMultivector basisBladeMv;
                return
                    !_basisBladesMaps.TryGetValue(id1, id2, id3, out basisBladeMv) || ReferenceEquals(basisBladeMv, null)
                        ? GaNumMultivector.CreateZero(TargetGaSpaceDimension)
                        : basisBladeMv;
            }
        }


        private GaNumMapTrilinearHash(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapTrilinearHash(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaNumMapTrilinearHash ClearBasisBladesMaps()
        {
            _basisBladesMaps.Clear();
            return this;
        }

        public GaNumMapTrilinearHash SetBasisBladesMap(int id1, int id2, int id3, IGaNumMultivector value)
        {
            Debug.Assert(ReferenceEquals(value, null) || value.VSpaceDimension == TargetVSpaceDimension);

            _basisBladesMaps[id1, id2, id3] = value.Compactify(true);

            return this;
        }

        public GaNumMapTrilinearHash RemoveBasisBladesMap(int id1, int id2, int id3)
        {
            _basisBladesMaps.Remove(id1, id2, id3);
            return this;
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2, GaNumMultivector mv3)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension || mv3.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var tempMv = GaNumMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term1 in mv1.NonZeroTerms)
            {
                var id1 = term1.Key;
                var coef1 = term1.Value;

                foreach (var term2 in mv2.NonZeroTerms)
                {
                    var id2 = term2.Key;
                    var coef2 = term2.Value;

                    foreach (var term3 in mv3.NonZeroTerms)
                    {
                        var id3 = term3.Key;
                        var coef3 = term3.Value;

                        IGaNumMultivector basisBladeMv;
                        if (!_basisBladesMaps.TryGetValue(id1, id2, id3, out basisBladeMv) ||
                            basisBladeMv == null)
                            continue;

                        foreach (var basisBladeMvTerm in basisBladeMv.NonZeroTerms)
                            tempMv.AddFactor(
                                basisBladeMvTerm.Key,
                                basisBladeMvTerm.Value * coef1 * coef2 * coef3
                            );
                    }
                }
            }

            return tempMv;
        }

        public override IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            foreach (var pair in _basisBladesMaps)
            {
                var id1 = pair.Key.Item1;
                var id2 = pair.Key.Item2;
                var id3 = pair.Key.Item3;
                var mv = pair.Value;

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, int, int, IGaNumMultivector>(id1, id2, id3, mv);
            }
        }
    }
}