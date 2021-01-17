using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraStructuresLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public sealed class GaNumMapUnilinearKVectorsArray : GaNumMapUnilinear
    {
        public static GaNumMapUnilinearKVectorsArray Create(int targetVSpaceDim)
        {
            return new GaNumMapUnilinearKVectorsArray(
                targetVSpaceDim
            );
        }

        public static GaNumMapUnilinearKVectorsArray Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapUnilinearKVectorsArray(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaNumMapUnilinearKVectorsArray CreateOutermorphism(Matrix linearVectorMapsMatrix)
        {
            var domainGaSpaceDim =
                linearVectorMapsMatrix.ColumnCount.ToGaSpaceDimension();

            var targetVSpaceDim =
                linearVectorMapsMatrix.RowCount;

            var targetGaSpaceDim =
                targetVSpaceDim.ToGaSpaceDimension();

            var omMap = Create(
                linearVectorMapsMatrix.ColumnCount,
                linearVectorMapsMatrix.RowCount
            );

            //Add unit scalar as the image of the 0-basis blade
            omMap.SetBasisBladeMap(
                0,
                GaNumDarKVector.CreateScalar(targetGaSpaceDim, 1)
            );

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaNumDarKVector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage =
                        GaNumDarKVector.CreateFromColumn(
                            targetVSpaceDim,
                            1,
                            linearVectorMapsMatrix,
                            id.BasisBladeIndex()
                        );
                }
                else
                {
                    //Add images of a higher dimensional basis blade using the outer product
                    //of the images of two lower dimensional basis blades
                    id.SplitBySmallestBasicPattern(out var id1, out var id2);

                    var kVector1 = omMap._basisBladeMaps[id1];
                    var kVector2 = omMap._basisBladeMaps[id2];

                    basisBladeImage = kVector1.Op(kVector2);
                }

                omMap._basisBladeMaps[id] = basisBladeImage;
            }

            return omMap;
        }


        private readonly GaNumDarKVector[] _basisBladeMaps;


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1]
            => _basisBladeMaps[id1]
               ?? GaNumDarKVector.CreateScalar(TargetVSpaceDimension, 0);

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


        private GaNumMapUnilinearKVectorsArray(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new GaNumDarKVector[DomainGaSpaceDimension];
        }

        private GaNumMapUnilinearKVectorsArray(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new GaNumDarKVector[DomainGaSpaceDimension];
        }


        public GaNumMapUnilinearKVectorsArray ClearBasisBladesMaps()
        {
            for (var id = 0; id < DomainGaSpaceDimension; id++)
                _basisBladeMaps[id] = null;

            return this;
        }

        public GaNumDarKVector GetBasisBladeMap(int basisBladeId)
        {
            return _basisBladeMaps[basisBladeId];
        }

        public GaNumMapUnilinearKVectorsArray SetBasisBladeMap(int basisBladeId, GaNumDarKVector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            _basisBladeMaps[basisBladeId] = targetMv;

            return this;
        }

        public GaNumMapUnilinearKVectorsArray RemoveBasisBladesMap(int id1)
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