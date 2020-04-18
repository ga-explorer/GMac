using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public static class GaGbtUtils
    {
        public static ulong IdBitMask(this IGaGbtNode1 node)
        {
            return 1ul << node.TreeDepth;
        }

        public static ulong ChildIdBitMask(this IGaGbtNode1 node)
        {
            return 1ul << (node.TreeDepth - 1);
        }

        public static bool IsLeafNode(this IGaGbtNode node)
        {
            return node.TreeDepth == 0;
        }

        public static bool IsLeafParentNode(this IGaGbtNode node)
        {
            return node.TreeDepth == 1;
        }

        public static bool IsInternalNode(this IGaGbtNode node)
        {
            return node.TreeDepth > 0;
        }


        public static GaGbtBasisVectorNode GetBasisVectorGbtRootNode(this int basisVectorIndex, int vSpaceDimension)
        {
            return GaGbtBasisVectorNode.CreateRootNode(vSpaceDimension, basisVectorIndex);
        }

        public static GaGbtBasisBladeNode GetBasisBladeGbtRootNode(this int basisBladeId, int vSpaceDimension)
        {
            return GaGbtBasisBladeNode.CreateRootNode(vSpaceDimension, (ulong)basisBladeId);
        }

        public static double GetValue(this IGaGbtNode1 node)
        {
            var node1 = (IGaGbtNode1<double>)node;

            return node1.Value;
        }

        public static double GetValuesProduct(this GaGbtNode2 node)
        {
            var node1 = (IGaGbtNode1<double>)node.GbtNode1;
            var node2 = (IGaGbtNode1<double>)node.GbtNode2;

            return node1.Value * node2.Value;
        }

        public static GaTerm<double> GetNumEGpTerm(this GaGbtNode2 node)
        {
            var id1 = (int)node.Id1;
            var id2 = (int)node.Id2;

            var node1 = (IGaGbtNode1<double>)node.GbtNode1;
            var node2 = (IGaGbtNode1<double>)node.GbtNode2;

            var scalarValue = node1.Value * node2.Value;
            if (GaNumFrameUtils.IsNegativeEGp(id1, id2))
                scalarValue = -scalarValue;

            return new GaTerm<double>(id1 ^ id2, scalarValue);
        }

        public static GaTerm<double> GetNumEGpTerm(this GaGbtNode2 node, double scalingFactor)
        {
            var id1 = (int)node.Id1;
            var id2 = (int)node.Id2;

            var node1 = (IGaGbtNode1<double>)node.GbtNode1;
            var node2 = (IGaGbtNode1<double>)node.GbtNode2;

            var scalarValue = scalingFactor * node1.Value * node2.Value;
            if (GaNumFrameUtils.IsNegativeEGp(id1, id2))
                scalarValue = -scalarValue;

            return new GaTerm<double>(id1 ^ id2, scalarValue);
        }

        public static GaTerm<double> GetNumGpTerm(this GaGbtNode2 node, IGaNumMetricOrthogonal metric)
        {
            var node1 = (IGaGbtNode1<double>)node.GbtNode1;
            var node2 = (IGaGbtNode1<double>)node.GbtNode2;

            var term = metric.ScaledGp((int)node.Id1, (int)node.Id2, node1.Value * node2.Value);

            return term;
        }

        public static double GetValuesProduct(this GaGbtNode3 node)
        {
            var node1 = (IGaGbtNode1<double>)node.GbtNode1;
            var node2 = (IGaGbtNode1<double>)node.GbtNode2;
            var node3 = (IGaGbtNode1<double>)node.GbtNode3;

            return node1.Value * node2.Value * node3.Value;
        }
    }
}
