using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;
using GMac.GMacMath.Numeric.Products;
using TextComposerLib.Text.Tabular;

namespace GMac.GMacMath.Numeric.Maps.Bilinear
{
    public sealed class GaNumMapBilinearArray : GaNumMapBilinear
    {
        public static GaNumMapBilinearArray Create(int targetVSpaceDim)
        {
            return new GaNumMapBilinearArray(
                targetVSpaceDim
            );
        }

        public static GaNumMapBilinearArray Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapBilinearArray(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaNumMapBilinearArray CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaNumOp(vSpaceDimension).ToArrayMap();
        }

        public static GaNumMapBilinearArray CreateFromOuterProduct(IGMacFrame frame)
        {
            return new GaNumOp(frame.VSpaceDimension).ToArrayMap();
        }


        private readonly IGaNumMultivector[,] _basisBladesMaps;


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        //public int FactorsCount
        //{
        //    get
        //    {
        //        var count = 0;

        //        for (var id1 = 0; id1 < DomainGaSpaceDimension1; id1++)
        //        for (var id2 = 0; id2 < DomainGaSpaceDimension2; id2++)
        //        {
        //            var mv = _basisBladesMaps[id1, id2];

        //            if (!mv.IsNullOrZero())
        //                count += mv.NonZeroTerms.Count();
        //        }

        //        return count;
        //    }
        //}

        public override IGaNumMultivector this[int id1, int id2] 
            => _basisBladesMaps[id1, id2]
               ?? GaNumMultivectorTerm.CreateZero(TargetGaSpaceDimension);


        private GaNumMapBilinearArray(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            _basisBladesMaps = new IGaNumMultivector[DomainGaSpaceDimension, DomainGaSpaceDimension];
        }

        private GaNumMapBilinearArray(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            _basisBladesMaps = new IGaNumMultivector[DomainGaSpaceDimension, DomainGaSpaceDimension];
        }


        public GaNumMapBilinearArray SetBasisBladesMap(int basisBladeId1, int basisBladeId2, IGaNumMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            _basisBladesMaps[basisBladeId1, basisBladeId2] = targetMv.Compactify(true);

            return this;
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GMacNumericException("Multivector size mismatch");

            var tempMv = GaNumMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            var mvNodeStack1 = mv1.TermsTree.CreateNodesStack();
            var mvNodeStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            while (mvNodeStack1.Count > 0)
            {
                var mvNode1 = mvNodeStack1.Pop();
                var mvNode2 = mvNodeStack2.Pop();

                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();

                if (mvNode1.IsLeafNode)
                {
                    var basisBladeMv = _basisBladesMaps[id1, id2];

                    if (!ReferenceEquals(basisBladeMv, null))
                        tempMv.AddFactors(mvNode1.Value * mvNode2.Value, basisBladeMv);

                    continue;
                }

                if (mvNode1.HasChildNode0)
                {
                    if (mvNode2.HasChildNode0)
                    {
                        mvNodeStack1.Push(mvNode1.ChildNode0);
                        mvNodeStack2.Push(mvNode2.ChildNode0);

                        idStack1.Push(id1);
                        idStack2.Push(id2);
                    }

                    if (mvNode2.HasChildNode1)
                    {
                        mvNodeStack1.Push(mvNode1.ChildNode0);
                        mvNodeStack2.Push(mvNode2.ChildNode1);

                        idStack1.Push(id1);
                        idStack2.Push(id2 | mvNode2.ChildNode1.BitMask);
                    }
                }

                if (mvNode1.HasChildNode1)
                {
                    if (mvNode2.HasChildNode0)
                    {
                        mvNodeStack1.Push(mvNode1.ChildNode1);
                        mvNodeStack2.Push(mvNode2.ChildNode0);

                        idStack1.Push(id1 | mvNode1.ChildNode1.BitMask);
                        idStack2.Push(id2);
                    }

                    if (mvNode2.HasChildNode1)
                    {
                        mvNodeStack1.Push(mvNode1.ChildNode1);
                        mvNodeStack2.Push(mvNode2.ChildNode1);

                        idStack1.Push(id1 | mvNode1.ChildNode1.BitMask);
                        idStack2.Push(id2 | mvNode2.ChildNode1.BitMask);
                    }
                }
            }
            
            //foreach (var biTerm in mv1.GetBiTermsForEGp(mv2))
            //{
            //    var basisBladeMv = _basisBladesMaps[biTerm.Id1, biTerm.Id2];
            //    if (ReferenceEquals(basisBladeMv, null))
            //        continue;

            //    tempMv.AddFactors(biTerm.ValuesProduct, basisBladeMv);
            //}

            return tempMv;
        }

        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
                for (var id2 = 0; id2 < DomainGaSpaceDimension; id2++)
                {
                    var mv = _basisBladesMaps[id1, id2];

                    if (!mv.IsNullOrZero())
                        yield return new Tuple<int, int, IGaNumMultivector>(id1, id2, mv);
                }
        }


        public override string ToString()
        {
            var tableText = new TableComposer(TargetGaSpaceDimension, TargetGaSpaceDimension);
            var basisBladeIds = GMacMathUtils.BasisBladeIDs(TargetVSpaceDimension).ToArray();

            foreach (var basisBladeId in basisBladeIds)
            {
                tableText.ColumnsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
                tableText.RowsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
            }

            for (var basisBladeId1 = 0; basisBladeId1 < TargetGaSpaceDimension; basisBladeId1++)
                for (var basisBladeId2 = 0; basisBladeId2 < TargetGaSpaceDimension; basisBladeId2++)
                {
                    var mv = _basisBladesMaps[basisBladeId1, basisBladeId2];
                    if (mv != null)
                        tableText.Items[basisBladeId1, basisBladeId2] = mv.ToString();
                }

            var text = tableText.ToString();

            return text;
        }
    }
}
