using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Color;
using CodeComposerLib.GraphViz.Dot.Value;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.Rendering
{
    public static class RenderingUtils
    {
        
        //public static DotGraph ToDotGraph<T>(this GMacBinaryTree<T> binaryTree, bool showFullGraph)
        //{
        //    var maxTreeDepth = binaryTree.TreeDepth;
        //    var activeIDs = binaryTree.LeafNodeIDs.ToArray();

        //    var inactiveColor = DotColor.Rgb(Color.Gray);
        //    var activeColor = DotColor.Rgb(Color.Black);

        //    var dotGraph = DotGraph.Directed("Multivector");
        //    dotGraph.SetRankDir(DotRankDirection.LeftToRight);

        //    var nodeDefaults = dotGraph.AddNodeDefaults();
        //    nodeDefaults.SetShape(DotNodeShape.Circle);
        //    nodeDefaults.SetStyle(DotNodeStyle.Dashed);
        //    nodeDefaults.SetColor(inactiveColor);
        //    nodeDefaults.SetFontColor(inactiveColor);

        //    var edgeDefaults = dotGraph.AddEdgeDefaults();
        //    edgeDefaults.SetArrowHead(DotArrowType.Vee);
        //    edgeDefaults.SetStyle(DotEdgeStyle.Dashed);
        //    edgeDefaults.SetColor(inactiveColor);
        //    edgeDefaults.SetFontColor(inactiveColor);

        //    var idsStack = new Stack<ulong>();
        //    idsStack.Push(0ul);

        //    var treeDepthStack = new Stack<int>();
        //    treeDepthStack.Push(maxTreeDepth);

        //    var dotNodeName = "".PadRight(maxTreeDepth, '-');
        //    var dotNode = dotGraph.AddNode(dotNodeName);
        //    dotNode.SetStyle(DotNodeStyle.Solid);
        //    dotNode.SetColor(activeColor);
        //    dotNode.SetFontColor(activeColor);

        //    while (idsStack.Count > 0)
        //    {
        //        var id = idsStack.Pop();
        //        var treeDepth = treeDepthStack.Pop();

        //        dotNodeName =
        //            treeDepth == maxTreeDepth
        //                ? "".PadRight(treeDepth, '-')
        //                : id.PatternToStringPadRight(maxTreeDepth, treeDepth);

        //        dotNode = dotGraph.AddNode(dotNodeName);

        //        var id0 = id;
        //        var dotNodeName0 = id0.PatternToStringPadRight(maxTreeDepth, treeDepth - 1);
        //        var dotNode0 = dotGraph.AddNode(dotNodeName0);
        //        var dotEdge0 = dotNode.AddEdgeTo(dotNode0);


        //        var childNodesBitMask = 1ul << (treeDepth - 1);

        //        if (id0.IsActiveBinaryTreeNodeId(childNodesBitMask, activeIDs))
        //        {
        //            dotNode0.SetStyle(DotNodeStyle.Solid);
        //            dotNode0.SetColor(activeColor);
        //            dotNode0.SetFontColor(activeColor);

        //            dotEdge0.SetStyle(DotEdgeStyle.Solid);
        //            dotEdge0.SetColor(activeColor);
        //            dotEdge0.SetFontColor(activeColor);
        //        }

        //        var id1 = id | childNodesBitMask;
        //        var dotNodeName1 = id1.PatternToStringPadRight(maxTreeDepth, treeDepth - 1);
        //        var dotNode1 = dotGraph.AddNode(dotNodeName1);
        //        var dotEdge1 = dotNode.AddEdgeTo(dotNode1).SetLabel("e" + treeDepth);

        //        if (id1.IsActiveBinaryTreeNodeId(childNodesBitMask, activeIDs))
        //        {
        //            dotNode1.SetStyle(DotNodeStyle.Solid);
        //            dotNode1.SetColor(activeColor);
        //            dotNode1.SetFontColor(activeColor);

        //            dotEdge1.SetStyle(DotEdgeStyle.Solid);
        //            dotEdge1.SetColor(activeColor);
        //            dotEdge1.SetFontColor(activeColor);
        //        }

        //        if (treeDepth == 1)
        //        {
        //            dotNode0.SetShape(DotNodeShape.Square);
        //            dotNode1.SetShape(DotNodeShape.Square);
        //        }
        //        else if (treeDepth > 1)
        //        {
        //            idsStack.Push(id0);
        //            treeDepthStack.Push(treeDepth - 1);

        //            idsStack.Push(id1);
        //            treeDepthStack.Push(treeDepth - 1);
        //        }
        //    }

        //    return dotGraph;
        //}


        private static void AddChildNode(DotNode dotParentNode, ulong childNodeId, int treeDepth, int maxTreeDepth, bool isActive, DotRgbColor activeColor)
        {
            var dotChildNodeName = childNodeId.PatternToStringPadRight(maxTreeDepth, treeDepth);
            var dotChildNodeLabel = dotChildNodeName;

            var dotChildNode = dotParentNode.MainGraph.AddNode(dotChildNodeName);
            dotChildNode.SetLabel(dotChildNodeLabel);

            var dotEdge = dotParentNode.AddEdgeTo(dotChildNode);

            if (treeDepth == 0)
            {
                dotChildNode.SetShape(DotNodeShape.Square);
                dotChildNode.SetStyle(DotNodeStyle.Rounded);
            }

            if (!isActive)
                return;

            dotChildNode.SetPenWidth(2.0f);
            dotChildNode.SetColor(activeColor);
            dotChildNode.SetFontColor(activeColor);

            //dotEdge.SetPenWidth(2.0f);
            dotEdge.SetStyle(DotEdgeStyle.Solid);
            dotEdge.SetColor(activeColor);
            dotEdge.SetFontColor(activeColor);
        }

        private static void AddChildNode(DotNode dotParentNode, Tuple<ulong, ulong> childNodeId, int treeDepth, int maxTreeDepth, bool isActive, DotRgbColor activeColor)
        {
            var s1 = childNodeId.Item1.PatternToStringPadRight(maxTreeDepth, treeDepth);
            var s2 = childNodeId.Item2.PatternToStringPadRight(maxTreeDepth, treeDepth);

            var dotChildNodeName = s1 + ", " + s2;
            var dotChildNodeLabel = s1 + '\n' + s2;

            var dotChildNode = dotParentNode.MainGraph.AddNode(dotChildNodeName);
            dotChildNode.SetLabel(dotChildNodeLabel);

            var dotEdge = dotParentNode.AddEdgeTo(dotChildNode);

            if (treeDepth == 0)
            {
                dotChildNode.SetShape(DotNodeShape.Square);
                dotChildNode.SetStyle(DotNodeStyle.Rounded);
            }

            if (!isActive)
                return;

            dotChildNode.SetPenWidth(2.0f);
            dotChildNode.SetColor(activeColor);
            dotChildNode.SetFontColor(activeColor);

            //dotEdge.SetPenWidth(2.0f);
            dotEdge.SetStyle(DotEdgeStyle.Solid);
            dotEdge.SetColor(activeColor);
            dotEdge.SetFontColor(activeColor);
        }


        public static DotGraph AddBinaryTree<T>(this DotGraph dotGraph, GaBtrInternalNode<T> binaryTree, bool showFullGraph)
        {
            var maxTreeDepth = binaryTree.GetTreeDepth();

            var activeIDs = 
                binaryTree
                    .GetNodeInfo(maxTreeDepth, 0)
                    .GetTreeLeafNodeIDs()
                    .ToArray();

            var inactiveColor = DotColor.Rgb(Color.Gray);
            var activeColor = DotColor.Rgb(Color.Black);

            dotGraph.SetRankDir(DotRankDirection.LeftToRight);
            dotGraph.SetSplines(DotSplines.Spline);
            dotGraph.SetOverlap(DotOverlap.False);
            dotGraph.SetNodeMarginDelta(12);

            var nodeDefaults = dotGraph.AddNodeDefaults();
            nodeDefaults.SetShape(DotNodeShape.Circle);
            nodeDefaults.SetStyle(DotNodeStyle.Solid);
            nodeDefaults.SetColor(inactiveColor);
            nodeDefaults.SetFontColor(inactiveColor);
            nodeDefaults.SetPenWidth(1.0f);

            var edgeDefaults = dotGraph.AddEdgeDefaults();
            edgeDefaults.SetArrowHead(DotArrowType.Vee);
            edgeDefaults.SetStyle(DotEdgeStyle.Dashed);
            edgeDefaults.SetColor(inactiveColor);
            edgeDefaults.SetFontColor(inactiveColor);
            edgeDefaults.SetPenWidth(1.0f);

            var idsStack = new Stack<ulong>();
            idsStack.Push(0ul);

            var treeDepthStack = new Stack<int>();
            treeDepthStack.Push(maxTreeDepth);

            var dotNodeName = "".PadRight(maxTreeDepth, '-');
            var dotNodeLabel = dotNodeName;
            var dotNode = dotGraph.AddNode(dotNodeName);
            dotNode.SetShape(DotNodeShape.DoubleCircle);
            dotNode.SetStyle(DotNodeStyle.Solid);
            dotNode.SetColor(activeColor);
            dotNode.SetFontColor(activeColor);
            dotNode.SetPenWidth(2.0f);
            dotNode.SetLabel(dotNodeLabel);

            while (idsStack.Count > 0)
            {
                var parentNodeId = idsStack.Pop();
                var parentNodeTreeDepth = treeDepthStack.Pop();

                dotNodeName = 
                    parentNodeTreeDepth == maxTreeDepth
                        ? "".PadRight(parentNodeTreeDepth, '-')
                        : parentNodeId.PatternToStringPadRight(maxTreeDepth, parentNodeTreeDepth);

                dotNode = dotGraph.AddNode(dotNodeName);

                var childNodesBitMask = 1ul << (parentNodeTreeDepth - 1);

                //Add child node 0
                var childNodeId = parentNodeId;
                var isActive = childNodeId.IsActiveBinaryTreeNodeId(childNodesBitMask, activeIDs);
                if (showFullGraph || isActive)
                {
                    AddChildNode(
                        dotNode,
                        childNodeId,
                        parentNodeTreeDepth - 1,
                        maxTreeDepth,
                        isActive,
                        activeColor
                    );

                    if (parentNodeTreeDepth > 1)
                    {
                        idsStack.Push(childNodeId);
                        treeDepthStack.Push(parentNodeTreeDepth - 1);
                    }
                }

                //Add child node 1
                childNodeId = parentNodeId | childNodesBitMask;
                isActive = childNodeId.IsActiveBinaryTreeNodeId(childNodesBitMask, activeIDs);
                if (showFullGraph || isActive)
                {
                    AddChildNode(
                        dotNode,
                        childNodeId,
                        parentNodeTreeDepth - 1,
                        maxTreeDepth,
                        isActive,
                        activeColor
                    );

                    if (parentNodeTreeDepth > 1)
                    {
                        idsStack.Push(childNodeId);
                        treeDepthStack.Push(parentNodeTreeDepth - 1);
                    }
                }
            }

            return dotGraph;
        }

        public static DotGraph AddQuadTree<T>(this DotGraph dotGraph, GaQuadTreeInternalNode<T> quadTree, bool showFullGraph)
        {
            var maxTreeDepth = quadTree.TreeDepth;
            var activeIDs = quadTree.LeafNodeIDs.ToArray();

            var inactiveColor = DotColor.Rgb(Color.Gray);
            var activeColor = DotColor.Rgb(Color.Black);

            dotGraph.SetRankDir(DotRankDirection.LeftToRight);
            dotGraph.SetSplines(DotSplines.Spline);
            dotGraph.SetOverlap(DotOverlap.False);
            dotGraph.SetNodeMarginDelta(12);

            var nodeDefaults = dotGraph.AddNodeDefaults();
            nodeDefaults.SetShape(DotNodeShape.Circle);
            nodeDefaults.SetStyle(DotNodeStyle.Solid);
            nodeDefaults.SetColor(inactiveColor);
            nodeDefaults.SetFontColor(inactiveColor);
            nodeDefaults.SetPenWidth(1.0f);

            var edgeDefaults = dotGraph.AddEdgeDefaults();
            edgeDefaults.SetArrowHead(DotArrowType.Vee);
            edgeDefaults.SetStyle(DotEdgeStyle.Dashed);
            edgeDefaults.SetColor(inactiveColor);
            edgeDefaults.SetFontColor(inactiveColor);
            edgeDefaults.SetPenWidth(1.0f);

            var idsStack1 = new Stack<ulong>();
            idsStack1.Push(0ul);

            var idsStack2 = new Stack<ulong>();
            idsStack2.Push(0ul);

            var treeDepthStack = new Stack<int>();
            treeDepthStack.Push(maxTreeDepth);

            var s = "".PadRight(maxTreeDepth, '-');
            var dotNodeName = s + ", " + s;
            var dotNodeLabel = s + '\n' + s;
            var dotNode = dotGraph.AddNode(dotNodeName);
            dotNode.SetShape(DotNodeShape.DoubleCircle);
            dotNode.SetStyle(DotNodeStyle.Solid);
            dotNode.SetColor(activeColor);
            dotNode.SetFontColor(activeColor);
            dotNode.SetPenWidth(2.0f);
            dotNode.SetLabel(dotNodeLabel);

            while (idsStack1.Count > 0)
            {
                var idPart1 = idsStack1.Pop();
                var idPart2 = idsStack2.Pop();
                var treeDepth = treeDepthStack.Pop();

                var s1 = treeDepth == maxTreeDepth
                    ? "".PadRight(treeDepth, '-')
                    : idPart1.PatternToStringPadRight(maxTreeDepth, treeDepth);

                var s2 = treeDepth == maxTreeDepth
                    ? "".PadRight(treeDepth, '-')
                    : idPart2.PatternToStringPadRight(maxTreeDepth, treeDepth);

                dotNodeName = s1 + ", " + s2;
                dotNode = dotGraph.AddNode(dotNodeName);

                var childNodesBitMask = 1ul << (treeDepth - 1);

                //Add child node 00
                var childNodeId = Tuple.Create(idPart1, idPart2);
                var isActive = childNodeId.IsActiveQuadTreeNodeId(childNodesBitMask, activeIDs);
                if (showFullGraph || isActive)
                {
                    AddChildNode(
                        dotNode,
                        childNodeId,
                        treeDepth - 1,
                        maxTreeDepth,
                        isActive,
                        activeColor
                    );

                    if (treeDepth > 1)
                    {
                        idsStack1.Push(childNodeId.Item1);
                        idsStack2.Push(childNodeId.Item2);
                        treeDepthStack.Push(treeDepth - 1);
                    }
                }

                //Add child node 01
                childNodeId = Tuple.Create(idPart1 | childNodesBitMask, idPart2);
                isActive = childNodeId.IsActiveQuadTreeNodeId(childNodesBitMask, activeIDs);
                if (showFullGraph || isActive)
                {
                    AddChildNode(
                        dotNode,
                        childNodeId,
                        treeDepth - 1,
                        maxTreeDepth,
                        isActive,
                        activeColor
                    );

                    if (treeDepth > 1)
                    {
                        idsStack1.Push(childNodeId.Item1);
                        idsStack2.Push(childNodeId.Item2);
                        treeDepthStack.Push(treeDepth - 1);
                    }
                }

                //Add child node 10
                childNodeId = Tuple.Create(idPart1, idPart2 | childNodesBitMask);
                isActive = childNodeId.IsActiveQuadTreeNodeId(childNodesBitMask, activeIDs);
                if (showFullGraph || isActive)
                {
                    AddChildNode(
                        dotNode,
                        childNodeId,
                        treeDepth - 1,
                        maxTreeDepth,
                        isActive,
                        activeColor
                    );

                    if (treeDepth > 1)
                    {
                        idsStack1.Push(childNodeId.Item1);
                        idsStack2.Push(childNodeId.Item2);
                        treeDepthStack.Push(treeDepth - 1);
                    }
                }

                //Add child node 11
                childNodeId = Tuple.Create(idPart1 | childNodesBitMask, idPart2 | childNodesBitMask);
                isActive = childNodeId.IsActiveQuadTreeNodeId(childNodesBitMask, activeIDs);
                if (showFullGraph || isActive)
                {
                    AddChildNode(
                        dotNode,
                        childNodeId,
                        treeDepth - 1,
                        maxTreeDepth,
                        isActive,
                        activeColor
                    );

                    if (treeDepth > 1)
                    {
                        idsStack1.Push(childNodeId.Item1);
                        idsStack2.Push(childNodeId.Item2);
                        treeDepthStack.Push(treeDepth - 1);
                    }
                }
            }

            return dotGraph;
        }


        public static DotGraph ToGraphViz(this GaNumSarMultivector mv, bool showFullGraph = false, bool showLeafValues = false)
        {
            var dotGraph = DotGraph.Directed("Graph");
            dotGraph.AddBinaryTree(mv.BtrRootNode, showFullGraph);

            if (!showLeafValues)
                return dotGraph;

            foreach (var term in mv.GetStoredTerms())
            {
                var parentNodeName = term.BasisBladeId.PatternToString(mv.VSpaceDimension);
                var node = dotGraph.AddNode("coef" + parentNodeName);

                node.SetLabel(term.ScalarValue.ToString("G"));
                node.SetShape(DotNodeShape.Note);
                node.SetStyle(DotNodeStyle.Solid);
                node.SetColor(DotColor.Rgb(Color.Black));
                node.SetFontColor(DotColor.Rgb(Color.Black));

                var dotEdge = node.AddEdgeFrom(parentNodeName);
                dotEdge.SetArrowHead(DotArrowType.Inv);
                dotEdge.SetArrowTail(DotArrowType.Inv);
                dotEdge.SetStyle(DotEdgeStyle.Solid);
                dotEdge.SetColor(DotColor.Rgb(Color.Black));
                dotEdge.SetFontColor(DotColor.Rgb(Color.Black));
            }

            return dotGraph;
        }

        public static DotGraph ToGraphViz(this GaNumMapBilinearTree bilinearMap, bool showFullGraph = false, bool showLeafValues = false)
        {
            var dotGraph = DotGraph.Directed("Graph");
            dotGraph.AddQuadTree(bilinearMap.BasisBladesMapTree, showFullGraph);

            if (!showLeafValues)
                return dotGraph;

            foreach (var leafPair in bilinearMap.BasisBladesMapTree.LeafValuePairs)
            {
                var dotHtmlTable = DotUtils.Table();
                dotHtmlTable.SetBorder(0);
                dotHtmlTable.SetCellBorder(1);
                var dotHtmlRow = dotHtmlTable.AddRow();
                var mv = leafPair.Item3;

                foreach (var term in mv.GetStoredTerms())
                {
                    var columnTable = DotUtils.Table();
                    columnTable.SetBorder(0);
                    columnTable.SetCellBorder(0);
                    columnTable.AddRow(
                        term.BasisBladeId.PatternToString(mv.VSpaceDimension).ToHtmlString()
                    );
                    columnTable.AddRow(term.ScalarValue.ToString("G"));

                    dotHtmlRow.AddCell(columnTable);
                }

                var parentNodeName = 
                    leafPair.Item1.PatternToString(bilinearMap.DomainVSpaceDimension) + ", " +
                    leafPair.Item2.PatternToString(bilinearMap.DomainVSpaceDimension);
                var node = dotGraph.AddNode("coef" + parentNodeName);

                node.SetLabel(dotHtmlTable);
                node.SetShape(DotNodeShape.Note);
                node.SetStyle(DotNodeStyle.Solid);
                node.SetColor(DotColor.Rgb(Color.Black));
                node.SetFontColor(DotColor.Rgb(Color.Black));

                var dotEdge = node.AddEdgeFrom(parentNodeName);
                dotEdge.SetArrowHead(DotArrowType.Inv);
                dotEdge.SetArrowTail(DotArrowType.Inv);
                dotEdge.SetStyle(DotEdgeStyle.Solid);
                dotEdge.SetColor(DotColor.Rgb(Color.Black));
                dotEdge.SetFontColor(DotColor.Rgb(Color.Black));
            }

            return dotGraph;
        }

        //public static DotGraph ToGraphViz(this GaNumMapUnilinearTree unilinearMap, bool showFullGraph = false, bool showLeafValues = false)
        //{
        //    var dotGraph = DotGraph.Directed("Graph");
        //    dotGraph.AddBinaryTree(unilinearMap.BasisBladesMapTree, showFullGraph);

        //    if (!showLeafValues)
        //        return dotGraph;

        //    var leafValuePairs =
        //        unilinearMap
        //            .BasisBladesMapTree
        //            .GetNodeInfo(unilinearMap.DomainVSpaceDimension, 0)
        //            .GetTreeLeafValuePairs();

        //    foreach (var leafPair in leafValuePairs)
        //    {
        //        var dotHtmlTable = DotUtils.Table();
        //        dotHtmlTable.SetBorder(0);
        //        dotHtmlTable.SetCellBorder(1);
        //        var dotHtmlRow = dotHtmlTable.AddRow();
        //        var mv = leafPair.Value;

        //        foreach (var term in mv.Terms)
        //        {
        //            var columnTable = DotUtils.Table();
        //            columnTable.SetBorder(0);
        //            columnTable.SetCellBorder(0);
        //            columnTable.AddRow(
        //                term.Key.PatternToString(mv.VSpaceDimension).ToHtmlString()
        //            );
        //            columnTable.AddRow(term.Value.ToString("G"));

        //            dotHtmlRow.AddCell(columnTable);
        //        }

        //        var parentNodeName = leafPair.Key.PatternToString(unilinearMap.DomainVSpaceDimension);
        //        var node = dotGraph.AddNode("coef" + parentNodeName);

        //        node.SetLabel(dotHtmlTable);
        //        node.SetShape(DotNodeShape.Note);
        //        node.SetStyle(DotNodeStyle.Solid);
        //        node.SetColor(DotColor.Rgb(Color.Black));
        //        node.SetFontColor(DotColor.Rgb(Color.Black));

        //        var dotEdge = node.AddEdgeFrom(parentNodeName);
        //        dotEdge.SetArrowHead(DotArrowType.Inv);
        //        dotEdge.SetArrowTail(DotArrowType.Inv);
        //        dotEdge.SetStyle(DotEdgeStyle.Solid);
        //        dotEdge.SetColor(DotColor.Rgb(Color.Black));
        //        dotEdge.SetFontColor(DotColor.Rgb(Color.Black));
        //    }

        //    return dotGraph;
        //}
    }
}
