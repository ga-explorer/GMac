using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;
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
            => GaNumMultivector.CreateBasisBlade(TargetGaSpaceDimension, id1);

        public override GaNumMultivector this[GaNumMultivector mv]
            => mv;


        private GaNumMapUnilinearIdentity(int vSpaceDim)
        {
            _vSpaceDimension = vSpaceDim;
        }


        protected override void ComputeMappingMatrix()
        {
            InternalMappingMatrix = DiagonalMatrix.CreateIdentity(DomainGaSpaceDimension);
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

        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector.CreateCopyTemp(mv1);
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            return Enumerable.Range(0, TargetGaSpaceDimension)
                .Select(id => new Tuple<int, IGaNumMultivector>(
                    id,
                    GaNumMultivector.CreateBasisBlade(TargetGaSpaceDimension, id)
                ));
        }
    }
}
