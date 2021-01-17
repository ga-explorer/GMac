using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using GeometricAlgebraSymbolicsLib.Products;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    public sealed class GaSymMapBilinearTree : GaSymMapBilinear
    {
        public static GaSymMapBilinearTree Create(int targetVSpaceDim)
        {
            return new GaSymMapBilinearTree(
                targetVSpaceDim
            );
        }

        public static GaSymMapBilinearTree Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapBilinearTree(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaSymMapBilinearTree CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaSymOp(vSpaceDimension).ToTreeMap();
        }

        public static GaSymMapBilinearTree CreateFromOuterProduct(IGaFrame frame)
        {
            return new GaSymOp(frame.VSpaceDimension).ToTreeMap();
        }


        private readonly GaQuadTreeInternalNode<IGaSymMultivector> _basisBladesMaps;


        public override int DomainVSpaceDimension
            => _basisBladesMaps.TreeDepth;

        public override int TargetVSpaceDimension { get; }

        public override IGaSymMultivector this[int id1, int id2]
        {
            get
            {
                _basisBladesMaps.TryGetLeafValue((ulong)id1, (ulong)id2, out var mv);

                return mv
                       ?? GaSymMultivectorTerm.CreateZero(TargetGaSpaceDimension);
            }
        }

        
        private GaSymMapBilinearTree(int targetVSpaceDim)
        {
            _basisBladesMaps = new GaQuadTreeInternalNode<IGaSymMultivector>(
                0ul,
                0ul,
                targetVSpaceDim
            );

            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaSymMapBilinearTree(int domainVSpaceDim, int targetVSpaceDim)
        {
            _basisBladesMaps = new GaQuadTreeInternalNode<IGaSymMultivector>(
                0ul, 
                0ul,
                domainVSpaceDim
            );

            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaSymMapBilinearTree ClearBasisBladesMaps()
        {
            _basisBladesMaps.RemoveChildNodes();

            return this;
        }

        public GaSymMapBilinearTree SetBasisBladesMap(int id1, int id2, IGaSymMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            targetMv = targetMv.Compactify(true);

            if (ReferenceEquals(targetMv, null))
                return this;

            _basisBladesMaps.SetLeafValue((ulong)id1, (ulong)id2, targetMv);

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            _basisBladesMaps.TryGetLeafValue((ulong)id1, (ulong)id2, out var mv);

            return mv?.ToTempMultivector()
                   ?? GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var tempMv = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            var mapNodeStack = _basisBladesMaps.CreateNodesStack();

            var mvNodeStack1 = mv1.TermsTree.CreateNodesStack();
            var mvNodeStack2 = mv2.TermsTree.CreateNodesStack();

            while (mapNodeStack.Count > 0)
            {
                var mapNode = mapNodeStack.Pop();

                var mvNode1 = mvNodeStack1.Pop();
                var mvNode2 = mvNodeStack2.Pop();

                if (mapNode.IsLeafNode)
                {
                    tempMv.AddFactors(
                        Mfs.Times[mvNode1.Value, mvNode2.Value],
                        mapNode.Value
                    );

                    continue;
                }

                if (mvNode1.HasChildNode0)
                {
                    if (mvNode2.HasChildNode0 && mapNode.HasChildNode00)
                    {
                        mapNodeStack.Push(mapNode.ChildNode00);

                        mvNodeStack1.Push(mvNode1.ChildNode0);
                        mvNodeStack2.Push(mvNode2.ChildNode0);
                    }

                    if (mvNode2.HasChildNode1 && mapNode.HasChildNode10)
                    {
                        mapNodeStack.Push(mapNode.ChildNode10);

                        mvNodeStack1.Push(mvNode1.ChildNode0);
                        mvNodeStack2.Push(mvNode2.ChildNode1);
                    }
                }

                if (mvNode1.HasChildNode1)
                {
                    if (mvNode2.HasChildNode0 && mapNode.HasChildNode01)
                    {
                        mapNodeStack.Push(mapNode.ChildNode01);

                        mvNodeStack1.Push(mvNode1.ChildNode1);
                        mvNodeStack2.Push(mvNode2.ChildNode0);
                    }

                    if (mvNode2.HasChildNode1 && mapNode.HasChildNode11)
                    {
                        mapNodeStack.Push(mapNode.ChildNode11);

                        mvNodeStack1.Push(mvNode1.ChildNode1);
                        mvNodeStack2.Push(mvNode2.ChildNode1);
                    }
                }
            }

            return tempMv;
        }

        public override IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisBladesMaps()
        {
            return _basisBladesMaps
                .LeafValuePairs
                .Where(pair => !pair.Item3.IsNullOrZero())
                .Select(pair => new Tuple<int, int, IGaSymMultivector>(
                    (int)pair.Item1,
                    (int)pair.Item2,
                    pair.Item3
                ));
        }
    }
}