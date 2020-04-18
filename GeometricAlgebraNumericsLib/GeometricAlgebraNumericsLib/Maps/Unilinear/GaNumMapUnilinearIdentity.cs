using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public sealed class GaNumMapUnilinearIdentity : GaNumMapUnilinear
    {
        public static GaNumMapUnilinearIdentity Create(int vSpaceDim)
        {
            return new GaNumMapUnilinearIdentity(vSpaceDim);
        }


        private readonly int _vSpaceDimension;


        public override int DomainVSpaceDimension
            => _vSpaceDimension;

        public override int TargetVSpaceDimension
            => _vSpaceDimension;

        public override IGaNumMultivector this[int id1] 
            => GaNumSarMultivector.CreateBasisBlade(TargetVSpaceDimension, id1);

        public override GaNumSarMultivector this[GaNumSarMultivector mv]
            => mv.GetSarMultivector();

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv]
            => mv.GetDgrMultivector();


        private GaNumMapUnilinearIdentity(int vSpaceDim)
        {
            _vSpaceDimension = vSpaceDim;
        }


        public override Matrix GetMappingMatrix()
        {
            InternalMappingMatrix = DiagonalMatrix.CreateIdentity(DomainGaSpaceDimension);

            return InternalMappingMatrix;
        }


        public override GaNumMapUnilinear Adjoint()
        {
            return this;
        }

        public override GaNumMapUnilinear Inverse()
        {
            return this;
        }

        public override GaNumMapUnilinear InverseAdjoint()
        {
            return this;
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            return Enumerable.Range(0, TargetGaSpaceDimension)
                .Select(id => new Tuple<int, IGaNumMultivector>(
                    id,
                    GaNumSarMultivector.CreateBasisBlade(TargetVSpaceDimension, id)
                ));
        }
    }
}
