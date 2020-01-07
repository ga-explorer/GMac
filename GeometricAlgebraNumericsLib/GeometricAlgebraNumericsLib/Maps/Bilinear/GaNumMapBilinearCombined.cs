﻿using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public sealed class GaNumMapBilinearCombined : GaNumMapBilinear
    {
        private sealed class GaNumMapBilinearCombinedTerm
        {
            public double Coef { get; }

            public IGaNumMapBilinear LinearMap { get; }


            internal GaNumMapBilinearCombinedTerm(double coef, IGaNumMapBilinear linearMap)
            {
                Coef = coef;
                LinearMap = linearMap;
            }
        }


        public static GaNumMapBilinearCombined Create(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
        {
            return new GaNumMapBilinearCombined(
                targetVSpaceDim,
                associativity
            );
        }

        public static GaNumMapBilinearCombined Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapBilinearCombined(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }


        private readonly List<GaNumMapBilinearCombinedTerm> _termsList
            = new List<GaNumMapBilinearCombinedTerm>();


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                var resultMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                foreach (var term in _termsList)
                    resultMv.AddFactors(
                        term.Coef,
                        term.LinearMap[id1, id2]
                    );

                return resultMv.ToMultivector();
            }
        }

        public override GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                foreach (var term in _termsList)
                    resultMv.AddFactors(
                        term.Coef,
                        term.LinearMap[mv1, mv2]
                    );

                return resultMv;
            }
        }


        private GaNumMapBilinearCombined(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
            : base(associativity)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapBilinearCombined(int domainVSpaceDim, int targetVSpaceDim)
            : base(GaNumMapBilinearAssociativity.NoneAssociative)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaNumMapBilinearCombined ClearMaps()
        {
            _termsList.Clear();

            return this;
        }

        public GaNumMapBilinearCombined AddMap(double coef, IGaNumMapBilinear linearMap)
        {
            if (
                linearMap.DomainVSpaceDimension != DomainVSpaceDimension ||
                linearMap.TargetVSpaceDimension != TargetVSpaceDimension
            )
                throw new InvalidOperationException("Linear map dimensions mismatch");

            _termsList.Add(new GaNumMapBilinearCombinedTerm(coef, linearMap));

            return this;
        }


        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
                for (var id2 = 0; id2 < DomainGaSpaceDimension; id2++)
                {
                    var mv = this[id1, id2];

                    if (!mv.IsNullOrEmpty())
                        yield return new Tuple<int, int, IGaNumMultivector>(id1, id2, mv);
                }
        }
    }
}