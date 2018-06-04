using System;
using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    public sealed class GaSymMapBilinearCombined : GaSymMapBilinear
    {
        private sealed class GaSymMapBilinearCombinedTerm
        {
            public MathematicaScalar Coef { get; }

            public IGaSymMapBilinear LinearMap { get; }


            internal GaSymMapBilinearCombinedTerm(MathematicaScalar coef, IGaSymMapBilinear linearMap)
            {
                Coef = coef;
                LinearMap = linearMap;
            }
        }


        public static GaSymMapBilinearCombined Create(int targetVSpaceDim)
        {
            return new GaSymMapBilinearCombined(
                targetVSpaceDim
            );
        }

        public static GaSymMapBilinearCombined Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapBilinearCombined(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }


        private readonly List<GaSymMapBilinearCombinedTerm> _termsList
            = new List<GaSymMapBilinearCombinedTerm>();


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaSymMultivector this[int id1, int id2] 
            => MapToTemp(id1, id2)?.ToMultivector()
               ?? GaSymMultivector.CreateZero(TargetGaSpaceDimension);


        private GaSymMapBilinearCombined(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaSymMapBilinearCombined(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaSymMapBilinearCombined ClearMappings()
        {
            _termsList.Clear();

            return this;
        }

        public GaSymMapBilinearCombined AddMapping(MathematicaScalar coef, IGaSymMapBilinear linearMap)
        {
            if (
                linearMap.DomainVSpaceDimension != DomainVSpaceDimension ||
                linearMap.TargetVSpaceDimension != TargetVSpaceDimension
            )
                throw new InvalidOperationException("Linear map dimensions mismatch");

            _termsList.Add(new GaSymMapBilinearCombinedTerm(coef, linearMap));

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var resultMv = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term in _termsList)
                resultMv.AddFactors(
                    term.Coef.Expression,
                    term.LinearMap.MapToTemp(id1, id2)
                );

            return resultMv;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term in _termsList)
                resultMv.AddFactors(
                    term.Coef.Expression,
                    term.LinearMap.MapToTemp(mv1, mv2)
                );

            return resultMv;
        }

        public override IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0; id2 < DomainGaSpaceDimension; id2++)
            {
                var mv = this[id1, id2];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, int, IGaSymMultivector>(id1, id2, mv);
            }
        }
    }
}
