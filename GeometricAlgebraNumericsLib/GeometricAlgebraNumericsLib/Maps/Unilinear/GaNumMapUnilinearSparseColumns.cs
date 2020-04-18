using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.Collections;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public sealed class GaNumMapUnilinearSparseColumns : GaNumMapUnilinear
    {
        public static GaNumMapUnilinearSparseColumns Create(int targetVSpaceDim)
        {
            return new GaNumMapUnilinearSparseColumns(
                targetVSpaceDim
            );
        }

        public static GaNumMapUnilinearSparseColumns Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapUnilinearSparseColumns(
                domainVSpaceDim,
                targetVSpaceDim
                );
        }

        public static GaNumMapUnilinearSparseColumns Create(IGaNumMapUnilinear linearMap, IEnumerable<int> idsList)
        {
            var table = new GaNumMapUnilinearSparseColumns(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var id1 in idsList)
            {
                var resultMv = linearMap[id1];

                if (!resultMv.IsNullOrEmpty())
                    table._columnMultivectors[id1] = resultMv;
            }

            return table;
        }


        private readonly GaNumMultivectorHashTable1D _columnMultivectors
            = new GaNumMultivectorHashTable1D();


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1]
        {
            get
            {
                _columnMultivectors.TryGetValue(id1, out var basisBladeMv);

                return basisBladeMv
                       ?? GaNumSarMultivector.CreateZero(TargetVSpaceDimension);

            }
        }

        public override GaNumSarMultivector this[GaNumSarMultivector mv]
        {
            get
            {
                if (mv.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                foreach (var term1 in mv.GetNonZeroTerms())
                {
                    _columnMultivectors.TryGetValue(term1.BasisBladeId, out var basisBladeMv);
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
                    _columnMultivectors.TryGetValue(term1.BasisBladeId, out var basisBladeMv);
                    if (ReferenceEquals(basisBladeMv, null))
                        continue;

                    resultMv.AddTerms(term1.ScalarValue, basisBladeMv);
                }

                return resultMv.GetDgrMultivector();
            }
        }


        private GaNumMapUnilinearSparseColumns(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapUnilinearSparseColumns(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaNumMapUnilinearSparseColumns ClearColumns()
        {
            _columnMultivectors.Clear();
            return this;
        }

        public GaNumMapUnilinearSparseColumns SetColumn(int columnId, IGaNumMultivector columnMv)
        {
            Debug.Assert(ReferenceEquals(columnMv, null) || columnMv.VSpaceDimension == TargetVSpaceDimension);

            _columnMultivectors[columnId] = columnMv.Compactify(true);

            return this;
        }

        public GaNumMapUnilinearSparseColumns RemoveColumn(int id1)
        {
            _columnMultivectors.Remove(id1);
            return this;
        }


        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            return
                _columnMultivectors
                    .Where(p => !p.Value.IsNullOrEmpty())
                    .Select(
                        pair => new Tuple<int, IGaNumMultivector>(pair.Key, pair.Value)
                        );
        }
    }
}
