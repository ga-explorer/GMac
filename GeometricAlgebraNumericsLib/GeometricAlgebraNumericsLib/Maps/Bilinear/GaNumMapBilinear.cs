using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Maps;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public abstract class GaNumMapBilinear : GaMap, IGaNumMapBilinear
    {
        public abstract int DomainVSpaceDimension { get; }

        public int DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public GaNumMapBilinearAssociativity Associativity { get; }

        public abstract IGaNumMultivector this[int id1, int id2] { get; }

        public abstract GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2] { get; }

        public abstract GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2] { get; }

        public virtual GaNumSarMultivector this[params GaNumSarMultivector[] mvList]
        {
            get
            {
                if (Associativity == GaNumMapBilinearAssociativity.NoneAssociative)
                    throw new InvalidOperationException();

                var n = mvList.Length - 1;
                GaNumSarMultivector resultMv;

                if (Associativity == GaNumMapBilinearAssociativity.RightAssociative)
                {
                    resultMv = mvList[0];

                    for (var i = 1; i <= n; i++)
                        resultMv = this[resultMv, mvList[i]];
                }
                else
                {
                    resultMv = mvList[n];

                    for (var i = n - 1; i >= 0; i--)
                        resultMv = this[mvList[i], resultMv];
                }

                return resultMv;
            }
        }

        public virtual GaNumDgrMultivector this[params GaNumDgrMultivector[] mvList]
        {
            get
            {
                if (Associativity == GaNumMapBilinearAssociativity.NoneAssociative)
                    throw new InvalidOperationException();

                var n = mvList.Length - 1;
                GaNumDgrMultivector resultMv;

                if (Associativity == GaNumMapBilinearAssociativity.RightAssociative)
                {
                    resultMv = mvList[0];

                    for (var i = 1; i <= n; i++)
                        resultMv = this[resultMv, mvList[i]];
                }
                else
                {
                    resultMv = mvList[n];

                    for (var i = n - 1; i >= 0; i--)
                        resultMv = this[mvList[i], resultMv];
                }

                return resultMv;
            }
        }

        public abstract IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps();

        public virtual IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension; index2++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, index1);
                var id2 = GaFrameUtils.BasisBladeId(1, index2);
                var mv = this[id1, id2];

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<int, int, IGaNumMultivector>(index1, index2, mv);
            }
        }


        protected GaNumMapBilinear(GaNumMapBilinearAssociativity associativity)
        {
            Associativity = associativity;
        }
    }
}
