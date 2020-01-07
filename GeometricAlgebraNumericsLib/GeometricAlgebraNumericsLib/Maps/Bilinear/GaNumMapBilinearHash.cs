﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Structures;
using TextComposerLib.Text.Tabular;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public sealed class GaNumMapBilinearHash : GaNumMapBilinear
    {
        public static GaNumMapBilinearHash Create(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
        {
            return new GaNumMapBilinearHash(
                targetVSpaceDim,
                associativity
            );
        }

        public static GaNumMapBilinearHash Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapBilinearHash(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaNumMapBilinearHash CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaNumOp(vSpaceDimension).ToHashMap();
        }

        public static GaNumMapBilinearHash CreateFromOuterProduct(IGaFrame frame)
        {
            return new GaNumOp(frame.VSpaceDimension).ToHashMap();
        }


        private readonly GaHashTable2D<IGaNumMultivector> _basisBladesMaps
            = new GaHashTable2D<IGaNumMultivector>(GaNumMultivectorUtils.IsNullOrZero);


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public int TargetMultivectorsCount 
            => _basisBladesMaps.Count;

        public int TargetMultivectorTermsCount
            => _basisBladesMaps.Sum(p => p.Item3.TermsCount);

        public override IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                _basisBladesMaps.TryGetValue(id1, id2, out var resultMv);

                return resultMv
                       ?? GaNumMultivector.CreateZero(TargetGaSpaceDimension);
            }
        }

        public override GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                foreach (var biTerm in mv1.GetBiTermsForEGp(mv2))
                {
                    _basisBladesMaps.TryGetValue(biTerm.Id1, biTerm.Id2, out var basisBladeMv);
                    if (ReferenceEquals(basisBladeMv, null))
                        continue;

                    tempMv.AddFactors(biTerm.ValuesProduct, basisBladeMv);
                }

                return tempMv;
            }
        }


        private GaNumMapBilinearHash(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
            : base(associativity)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapBilinearHash(int domainVSpaceDim, int targetVSpaceDim)
            : base(GaNumMapBilinearAssociativity.NoneAssociative)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaNumMapBilinearHash ClearBasisBladesMaps()
        {
            _basisBladesMaps.Clear();
            return this;
        }

        public GaNumMapBilinearHash SetBasisBladesMap(int id1, int id2, IGaNumMultivector value)
        {
            Debug.Assert(ReferenceEquals(value, null) || value.VSpaceDimension == TargetVSpaceDimension);

            _basisBladesMaps[id1, id2] = value.Compactify(true);

            return this;
        }

        public GaNumMapBilinearHash RemoveBasisBladesMap(int id1, int id2)
        {
            _basisBladesMaps.Remove(id1, id2);
            return this;
        }


        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            return _basisBladesMaps.Where(p => !p.Item3.IsNullOrEmpty());
        }

        public override string ToString()
        {
            var tableText = new TableComposer(TargetGaSpaceDimension, TargetGaSpaceDimension);
            var basisBladeIds = GaNumFrameUtils.BasisBladeIDs(TargetVSpaceDimension).ToArray();

            foreach (var basisBladeId in basisBladeIds)
            {
                tableText.ColumnsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
                tableText.RowsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
            }

            foreach (var pair in _basisBladesMaps)
                tableText.Items[pair.Item1, pair.Item2] = 
                    pair.Item3.ToString();

            var text = tableText.ToString();

            return text;
        }
    }
}