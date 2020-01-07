using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Value;
using DataStructuresLib.Dictionary;
using GMac.GMacAST.Dependency;
using TextComposerLib.Text;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class TypeDependenciesToGraphViz : AstToGraphVizConverter
    {
        private readonly Dictionary<string, string> _dict = new ADictionary<string, string>();

        private AstTypeDependencyGraph _dependencyGraph;


        private DotNode AddNode(AstType astType)
        {
            if (astType.IsValidStructureType == false)
                return
                    Graph
                    .AddNode(astType.GMacTypeSignature)
                    .SetLabel(
                        Graph.Table(
                            Graph.SelectIconName(astType),
                            astType.GMacTypeSignature
                            )
                        );

            var astStruct = astType.ToStructure;

            var dict =
                astStruct
                .GroupDataMembersByType()
                .ToDictionary(
                    p => p.Key.GMacTypeSignature,
                    p => p.Value.Select(m => m.Name).Concatenate(Environment.NewLine)
                    );

            return
                Graph
                .AddNode(astStruct.GMacTypeSignature)
                .SetLabel(
                    Graph.Table(
                        "Structure",
                        astStruct.GMacTypeSignature,
                        Graph.SimpleTable(dict)
                        )
                    );
        }

        private DotEdge AddEdge(AstType usedType, AstType userType)
        {
            if (userType.IsValidStructureType)
                return Graph.AddEdge(
                    usedType.GMacTypeSignature.ToNodeRef(DotCompass.Center),
                    userType.GMacTypeSignature.ToNodeRef(usedType.GMacTypeSignature, DotCompass.West)
                    );

            return Graph.AddEdge(
                usedType.GMacTypeSignature, userType.GMacTypeSignature
                );
        }

        private void AddUserTypes(AstType astType)
        {
            var typeDep = _dependencyGraph[astType.GMacTypeSignature];

            if (typeDep.UserCount == 0)
                return;

            foreach (var userType in typeDep.UserItems)
            {
                if (_dict.ContainsKey(userType.GMacTypeSignature) == false)
                {
                    _dict.Add(userType.GMacTypeSignature, userType.GMacTypeSignature);

                    AddNode(userType)
                        .SetFillColor(Color.Wheat.ToDotRgbColor());
                }

                AddEdge(astType, userType);
                
                AddUserTypes(userType);
            }
        }

        private void AddUsedTypes(AstType astType)
        {
            var typeDep = _dependencyGraph[astType.GMacTypeSignature];

            if (typeDep.UsedCount == 0)
                return;

            foreach (var usedType in typeDep.UsedItems)
            {
                if (_dict.ContainsKey(usedType.GMacTypeSignature) == false)
                {
                    _dict.Add(usedType.GMacTypeSignature, usedType.GMacTypeSignature);

                    AddNode(usedType)
                        .SetFillColor(Color.White.ToDotRgbColor());
                }

                AddEdge(usedType, astType);

                AddUsedTypes(usedType);
            }
        }


        public void Visit(AstType astType)
        {
            _dependencyGraph = astType.Root.GetTypeDependencies();

            Graph.SetRankDir(DotRankDirection.LeftToRight);

            AddNode(astType)
                .SetFillColor(Color.GreenYellow.ToDotRgbColor());

            _dict.Clear();

            AddUserTypes(astType);

            _dict.Clear();

            AddUsedTypes(astType);
        }
    }
}
