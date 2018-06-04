using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    /// <summary>
    /// A linear map expressed as a linear combination of other linear maps
    /// </summary>
    public sealed class GaSymMapUnilinearCombined : GaSymMapUnilinear
    {
        private sealed class GaSymMapUnilinearCombinedTerm
        {
            public Expr Coef { get; }

            public IGaSymMapUnilinear LinearMap { get; }


            internal GaSymMapUnilinearCombinedTerm(Expr coef, IGaSymMapUnilinear linearMap)
            {
                Coef = coef;
                LinearMap = linearMap;
            }
        }


        public static GaSymMapUnilinearCombined Create(int targetVSpaceDim)
        {
            return new GaSymMapUnilinearCombined(
                targetVSpaceDim
            );
        }

        public static GaSymMapUnilinearCombined Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapUnilinearCombined(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }


        private readonly List<GaSymMapUnilinearCombinedTerm> _termsList
            = new List<GaSymMapUnilinearCombinedTerm>();


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaSymMultivector this[int id1] 
            => MapToTemp(id1)?.ToMultivector() 
               ?? GaSymMultivector.CreateZero(TargetGaSpaceDimension);


        private GaSymMapUnilinearCombined(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaSymMapUnilinearCombined(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaSymMapUnilinearCombined ClearMappings()
        {
            _termsList.Clear();

            return this;
        }

        public GaSymMapUnilinearCombined AddMapping(MathematicaScalar coef, IGaSymMapUnilinear linearMap)
        {
            if (
                linearMap.DomainVSpaceDimension != DomainVSpaceDimension ||
                linearMap.TargetVSpaceDimension != TargetVSpaceDimension
            )
                throw new InvalidOperationException("Linear map dimensions mismatch");

            _termsList.Add(new GaSymMapUnilinearCombinedTerm(coef.Expression, linearMap));

            return this;
        }

        public GaSymMapUnilinearCombined AddMapping(Expr coef, IGaSymMapUnilinear linearMap)
        {
            if (
                linearMap.DomainVSpaceDimension != DomainVSpaceDimension ||
                linearMap.TargetVSpaceDimension != TargetVSpaceDimension
            )
                throw new InvalidOperationException("Linear map dimensions mismatch");

            _termsList.Add(new GaSymMapUnilinearCombinedTerm(coef, linearMap));

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1)
        {
            var resultMv = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term in _termsList)
                resultMv.AddFactors(
                    term.Coef, 
                    term.LinearMap.MapToTemp(id1)
                );

            return resultMv;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            foreach (var term in _termsList)
                resultMv.AddFactors(
                    term.Coef,
                    term.LinearMap.MapToTemp(mv1)
                );

            return resultMv;
        }

        public override IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps()
        {
            return
                Enumerable
                    .Range(0, DomainGaSpaceDimension)
                    .Select(id => new Tuple<int, IGaSymMultivector>(id, this[id]))
                    .Where(t => !t.Item2.IsNullOrZero());
        }
    }
}
