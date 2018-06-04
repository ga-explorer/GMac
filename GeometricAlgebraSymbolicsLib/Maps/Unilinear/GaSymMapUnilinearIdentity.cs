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

        public override IGaSymMultivector this[int id1] 
            => GaSymMultivector.CreateBasisBlade(TargetGaSpaceDimension, id1);


        private GaSymMapUnilinearIdentity(int vSpaceDim)
        {
            _vSpaceDimension = vSpaceDim;
        }


        protected override void ComputeMappingMatrix()
        {
            InternalMappingMatrix = new GaSymMatrixIdentity(DomainGaSpaceDimension);
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


        public override IGaSymMultivectorTemp MapToTemp(int id1)
        {
            return GaSymMultivector.CreateBasisBladeTemp(TargetGaSpaceDimension, id1);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return mv1.ToTempMultivector();
        }

        public override IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps()
        {
            return Enumerable.Range(0, TargetGaSpaceDimension)
                .Select(id => new Tuple<int, IGaSymMultivector>(
                    id,
                    GaSymMultivector.CreateBasisBlade(TargetGaSpaceDimension, id)
                ));
        }
    }
}
