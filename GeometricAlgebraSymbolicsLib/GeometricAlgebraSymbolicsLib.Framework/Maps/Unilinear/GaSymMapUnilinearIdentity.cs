using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Matrices;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public sealed class GaSymMapUnilinearIdentity : GaSymMapUnilinear
    {
        public static GaSymMapUnilinearIdentity Create(int vSpaceDim)
        {
            return new GaSymMapUnilinearIdentity(vSpaceDim);
        }


        private readonly int _vSpaceDimension;


        public override int DomainVSpaceDimension 
            => _vSpaceDimension;

        public override int TargetVSpaceDimension
            => _vSpaceDimension;

        public override IGaSymMultivector this[ulong id1] 
            => GaSymMultivector.CreateBasisBlade(TargetVSpaceDimension, id1);


        private GaSymMapUnilinearIdentity(int vSpaceDim)
        {
            _vSpaceDimension = vSpaceDim;
        }


        protected override void ComputeMappingMatrix()
        {
            InternalMappingMatrix = new GaSymMatrixIdentity((int)DomainGaSpaceDimension);
        }


        public override GaSymMapUnilinear Adjoint()
        {
            return this;
        }

        public override GaSymMapUnilinear Inverse()
        {
            return this;
        }

        public override GaSymMapUnilinear InverseAdjoint()
        {
            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(ulong id1)
        {
            return GaSymMultivector.CreateBasisBladeTemp(TargetVSpaceDimension, id1);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return mv1.ToTempMultivector();
        }

        public override IEnumerable<Tuple<ulong, IGaSymMultivector>> BasisBladeMaps()
        {
            return Enumerable.Range(0, (int)TargetGaSpaceDimension)
                .Select(id => new Tuple<ulong, IGaSymMultivector>(
                    (ulong) id,
                    GaSymMultivector.CreateBasisBlade(TargetVSpaceDimension, (ulong) id)
                ));
        }
    }
}
