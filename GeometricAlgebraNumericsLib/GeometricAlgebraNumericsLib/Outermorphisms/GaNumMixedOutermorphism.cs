using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Outermorphisms
{
    public sealed class GaNumMixedOutermorphism : GaNumMapUnilinear
    {
        public static GaNumMixedOutermorphism Create(Matrix vectorsMappingMatrix)
        {
            return new GaNumMixedOutermorphism(vectorsMappingMatrix, 12);
        }

        public static GaNumMixedOutermorphism Create(Matrix vectorsMappingMatrix, int splitAt)
        {
            return new GaNumMixedOutermorphism(vectorsMappingMatrix, splitAt);
        }


        private readonly GaNumMapUnilinearKVectorsArray _subMap1;

        private readonly GaNumMapUnilinearKVectorsArray _subMap2;
        

        public int SplitAt { get; }

        public override int DomainVSpaceDimension
            => ReferenceEquals(_subMap2, null) 
                ? _subMap1.DomainVSpaceDimension 
                : _subMap1.DomainVSpaceDimension + _subMap2.DomainVSpaceDimension;

        public override int TargetVSpaceDimension
            => _subMap1.TargetVSpaceDimension;
        
        public double Determinant
        {
            get
            {
                if (DomainVSpaceDimension <= SplitAt)
                    return _subMap1.DomainPseudoScalarMap[0];

                var mv1 = _subMap1.DomainPseudoScalarMap.GetSarMultivector();
                var mv2 = _subMap2.DomainPseudoScalarMap.GetSarMultivector();
                var op = mv1.Op(mv2);

                return op[0];
            }
        }
        
        public override IGaNumMultivector this[int id]
        {
            get
            {
                if (id == 0)
                    return GaNumTerm.CreateScalar(TargetGaSpaceDimension, 1);

                var idTuple = SplitId(id);

                if (idTuple.Item2 == 0)
                    return _subMap1.GetBasisBladeMap(idTuple.Item1);

                if (idTuple.Item1 == 0)
                    return _subMap2.GetBasisBladeMap(idTuple.Item2);

                var mv1 = _subMap1.GetBasisBladeMap(idTuple.Item1);
                var mv2 = _subMap2.GetBasisBladeMap(idTuple.Item2);

                return mv1.Op(mv2);
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
                    var basisBladeMv = this[term1.BasisBladeId];
                    if (basisBladeMv.IsNullOrEmpty())
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
                    var basisBladeMv = this[term1.BasisBladeId];
                    if (basisBladeMv.IsNullOrEmpty())
                        continue;

                    resultMv.AddTerms(term1.ScalarValue, basisBladeMv);
                }

                return resultMv.GetDgrMultivector();
            }
        }


        private GaNumMixedOutermorphism(Matrix vectorsMappingMatrix, int splitAt)
        {
            SplitAt = splitAt;

            if (vectorsMappingMatrix.ColumnCount <= SplitAt)
            {
                _subMap1 = GaNumMapUnilinearKVectorsArray.CreateOutermorphism(vectorsMappingMatrix);
                _subMap2 = null;
            }
            else
            {
                var matrix1 = (Matrix) vectorsMappingMatrix.SubMatrix(
                    0, 
                    vectorsMappingMatrix.RowCount, 
                    0, 
                    SplitAt
                );

                var matrix2 = (Matrix) vectorsMappingMatrix.SubMatrix(
                    0,
                    vectorsMappingMatrix.RowCount,
                    SplitAt,
                    vectorsMappingMatrix.ColumnCount - SplitAt
                );

                _subMap1 = GaNumMapUnilinearKVectorsArray.CreateOutermorphism(matrix1);
                _subMap2 = GaNumMapUnilinearKVectorsArray.CreateOutermorphism(matrix2);
            }
        }


        public Tuple<int, int> SplitId(int id)
        {
            return new Tuple<int, int>(
                id & ((1 << SplitAt) - 1),
                id >> SplitAt
            );
        }

        public int LowerSplitId(int id)
        {
            return id & ((1 << SplitAt) - 1);
        }

        public int UpperSplitId(int id)
        {
            return id >> SplitAt;
        }

        public override GaNumMapUnilinear Adjoint()
        {
            return new GaNumMixedOutermorphism(
                GetVectorsMappingMatrix().Transpose() as Matrix,
                SplitAt
            );
        }

        public override GaNumMapUnilinear Inverse()
        {
            return new GaNumMixedOutermorphism(
                GetVectorsMappingMatrix().Inverse() as Matrix,
                SplitAt
            );
        }

        public override GaNumMapUnilinear InverseAdjoint()
        {
            return new GaNumMixedOutermorphism(
                GetVectorsMappingMatrix().Inverse().Transpose() as Matrix,
                SplitAt
            );
        }


        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            if (DomainVSpaceDimension <= SplitAt)
            {
                foreach (var pair in _subMap1.BasisBladeMaps())
                    yield return pair;

                yield break;
            }

            for (var id = 0; id < DomainGaSpaceDimension; id++)
            {
                var basisBladeMv = this[id];
                if (!ReferenceEquals(basisBladeMv, null))
                    yield return new Tuple<int, IGaNumMultivector>(id, basisBladeMv);
            }
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps()
        {
            if (DomainVSpaceDimension <= SplitAt)
                return _subMap1.BasisVectorMaps();

            return _subMap1
                .BasisVectorMaps()
                .Concat(_subMap2.BasisVectorMaps());
        }
    }
}