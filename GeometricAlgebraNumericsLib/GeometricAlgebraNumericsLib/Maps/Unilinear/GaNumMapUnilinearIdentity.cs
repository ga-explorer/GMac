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

        public override IGaNumMultivector this[ulong id1] 
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
            InternalMappingMatrix = DiagonalMatrix.CreateIdentity((int)DomainGaSpaceDimension);

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

        public override IEnumerable<Tuple<ulong, IGaNumMultivector>> BasisBladeMaps()
        {
            return Enumerable.Range(0, (int)TargetGaSpaceDimension)
                .Select(id => new Tuple<ulong, IGaNumMultivector>(
                    (ulong)id,
                    GaNumSarMultivector.CreateBasisBlade(TargetVSpaceDimension, (ulong)id)
                ));
        }
    }
}
