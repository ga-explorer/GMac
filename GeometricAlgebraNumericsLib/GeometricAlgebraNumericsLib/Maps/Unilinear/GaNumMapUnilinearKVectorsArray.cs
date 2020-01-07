using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;
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

            var targetGaSpaceDim =
                linearVectorMapsMatrix.RowCount.ToGaSpaceDimension();

            var omMap = Create(
                linearVectorMapsMatrix.ColumnCount,
                linearVectorMapsMatrix.RowCount
            );

            //Add unit scalar as the image of the 0-basis blade
            omMap.SetBasisBladeMap(
                0,
                GaNumKVector.CreateScalar(targetGaSpaceDim, 1)
            );

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaNumKVector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage =
                        GaNumKVector.CreateVectorFromColumn(
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


        private readonly GaNumKVector[] _basisBladeMaps;


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1]
            => _basisBladeMaps[id1]
               ?? GaNumKVector.CreateScalar(TargetGaSpaceDimension);

        public override GaNumMultivector this[GaNumMultivector mv]
        {
            get
            {
                if (mv.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                foreach (var term1 in mv.NonZeroTerms)
                {
                    var basisBladeMv = _basisBladeMaps[term1.Key];

                    if (ReferenceEquals(basisBladeMv, null))
                        continue;

                    resultMv.AddFactors(term1.Value, basisBladeMv);
                }

                return resultMv;
            }
        }


        private GaNumMapUnilinearKVectorsArray(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new GaNumKVector[DomainGaSpaceDimension];
        }

        private GaNumMapUnilinearKVectorsArray(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new GaNumKVector[DomainGaSpaceDimension];
        }


        public GaNumMapUnilinearKVectorsArray ClearBasisBladesMaps()
        {
            for (var id = 0; id < DomainGaSpaceDimension; id++)
                _basisBladeMaps[id] = null;

            return this;
        }

        public GaNumKVector GetBasisBladeMap(int basisBladeId)
        {
            return _basisBladeMaps[basisBladeId];
        }

        public GaNumMapUnilinearKVectorsArray SetBasisBladeMap(int basisBladeId, GaNumKVector targetMv)
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