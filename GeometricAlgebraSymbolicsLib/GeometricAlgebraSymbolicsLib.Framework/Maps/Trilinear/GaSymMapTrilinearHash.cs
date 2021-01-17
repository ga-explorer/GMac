using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Maps.Trilinear
{
    public sealed class GaSymMapTrilinearHash : GaSymMapTrilinear
    {
        public static GaSymMapTrilinearHash Create(int targetVSpaceDim)
        {
            return new GaSymMapTrilinearHash(
                targetVSpaceDim
            );
        }

        public static GaSymMapTrilinearHash Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapTrilinearHash(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }


        private readonly GaSymMultivectorHashTable3D _basisBladesMaps
            = new GaSymMultivectorHashTable3D();


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaSymMultivector this[int id1, int id2, int id3]
        {
            get
            {
                return
                    !_basisBladesMaps.TryGetValue(id1, id2, id3, out var basisBladeMv) || ReferenceEquals(basisBladeMv, null)
                        ? GaSymMultivector.CreateZero(TargetGaSpaceDimension)
                        : basisBladeMv;
            }
        }


        private GaSymMapTrilinearHash(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaSymMapTrilinearHash(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaSymMapTrilinearHash ClearBasisBladesMaps()
        {
            _basisBladesMaps.Clear();
            return this;
        }

        public GaSymMapTrilinearHash SetBasisBladesMap(int id1, int id2, int id3, IGaSymMultivector value)
        {
            Debug.Assert(ReferenceEquals(value, null) || value.VSpaceDimension == TargetVSpaceDimension);

            _basisBladesMaps[id1, id2, id3] = value.Compactify(true);

            return this;
        }

        public GaSymMapTrilinearHash RemoveBasisBladesMap(int id1, int id2, int id3)
        {
            _basisBladesMaps.Remove(id1, id2, id3);
            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2, int id3)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            if (!_basisBladesMaps.TryGetValue(id1, id2, id3, out var basisBladeMv) || basisBladeMv == null)
                return tempMultivector;

            tempMultivector.AddFactors(
                Expr.INT_ONE,
                basisBladeMv
            );

            return tempMultivector;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension || mv3.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term1 in mv1.NonZeroExprTerms)
            {
                var id1 = term1.Key;
                var coef1 = term1.Value;

                foreach (var term2 in mv2.NonZeroExprTerms)
                {
                    var id2 = term2.Key;
                    var coef2 = term2.Value;

                    foreach (var term3 in mv3.NonZeroExprTerms)
                    {
                        var id3 = term3.Key;
                        var coef3 = term3.Value;

                        if (!_basisBladesMaps.TryGetValue(id1, id2, id3, out var basisBladeMv) ||
                            basisBladeMv == null)
                            continue;

                        tempMultivector.AddFactors(
                            Mfs.Times[coef1, coef2, coef3], 
                            basisBladeMv
                        );
                    }
                }
            }

            return tempMultivector;
        }

        public override IEnumerable<Tuple<int, int, int, IGaSymMultivector>> BasisBladesMaps()
        {
            foreach (var pair in _basisBladesMaps)
            {
                var id1 = pair.Key.Item1;
                var id2 = pair.Key.Item2;
                var id3 = pair.Key.Item3;
                var mv = pair.Value;

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, int, int, IGaSymMultivector>(id1, id2, id3, mv);
            }
        }
    }
}