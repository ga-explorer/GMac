using System.Collections.Generic;
using System.Linq;
using GMac.GMacCompiler.Semantic.ASTConstants;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;

namespace GMac.GMacCompiler.Semantic.AST
{
    internal static class GMacAstUtils
    {
        internal static IEnumerable<SymbolNamedValue> NamedValues(this GMacFrame parentFrame)
        {
            return
                parentFrame.FrameBasisVectors.Cast<SymbolNamedValue>()
                .Concat(parentFrame.ChildConstants);
        }

        internal static IEnumerable<ILanguageType> Types(this GMacFrame parentFrame)
        {
            return
                Enumerable
                .Repeat(parentFrame.MultivectorType as ILanguageType, 1)
                .Concat(parentFrame.Structures);
        }

        internal static IEnumerable<GMacMacro> Macros(this GMacFrame parentFrame)
        {
            return
                parentFrame.ChildMacros
                .Concat(parentFrame.Structures.SelectMany(s => s.Macros));
        }

        internal static IEnumerable<LanguageSymbol> MainSymbols(this GMacFrame parentFrame)
        {
            var roleNames = new[]
            {
                RoleNames.FrameMultivector,
                RoleNames.FrameSubspace,
                RoleNames.Constant,
                RoleNames.Macro,
                RoleNames.Structure,
                RoleNames.Transform
            };

            return parentFrame.ChildScope.Symbols(roleNames);
        }

        
        internal static IEnumerable<GMacNamespace> Namespaces(this GMacNamespace parentNameSpace)
        {
            var stack = new Stack<GMacNamespace>();

            stack.Push(parentNameSpace);

            while (stack.Count > 0)
            {
                var nameSpace = stack.Pop();

                yield return nameSpace;

                foreach (var child in nameSpace.ChildNamespaces)
                    stack.Push(child);
            }
        }

        internal static IEnumerable<SymbolNamedValue> NamedValues(this GMacNamespace parentNameSpace)
        {
            return
                Namespaces(parentNameSpace)
                .SelectMany(
                    nameSpace =>
                        nameSpace.ChildConstants
                        .Concat(nameSpace.ChildFrames.SelectMany(NamedValues))
                    );
        }

        internal static IEnumerable<SymbolNamedValue> FrameNamedValues(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(NamedValues);
        }

        internal static IEnumerable<GMacConstant> Constants(this GMacNamespace parentNameSpace)
        {
            return
                Namespaces(parentNameSpace)
                .SelectMany(
                    nameSpace =>
                        nameSpace
                        .ChildConstants
                        .Concat(
                            nameSpace.ChildFrames.SelectMany(frame => frame.ChildConstants)
                            )
                    );
        }

        internal static IEnumerable<GMacConstant> NamespaceConstants(this GMacNamespace parentNameSpace)
        {
            return Namespaces(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildConstants);
        }

        internal static IEnumerable<GMacConstant> FrameConstants(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(frame => frame.ChildConstants);
        }

        internal static IEnumerable<GMacFrame> Frames(this GMacNamespace parentNameSpace)
        {
            return Namespaces(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildFrames);
        }

        internal static IEnumerable<GMacFrameBasisVector> FrameBasisVectors(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(frame => frame.FrameBasisVectors);
        }

        internal static IEnumerable<GMacFrameMultivector> FrameMultivectors(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).Select(frame => frame.MultivectorType);
        }

        internal static IEnumerable<GMacFrameSubspace> FrameSubspaces(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(frame => frame.FrameSubspaces);
        }

        internal static IEnumerable<GMacStructure> Structures(this GMacNamespace parentNameSpace)
        {
            return
                Namespaces(parentNameSpace)
                .SelectMany(
                    nameSpace =>
                        nameSpace.ChildStructures
                        .Concat(
                            nameSpace.ChildFrames.SelectMany(frame => frame.Structures)
                            )
                    );
        }

        internal static IEnumerable<GMacStructure> NamespaceStructures(this GMacNamespace parentNameSpace)
        {
            return Namespaces(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildStructures);
        }

        internal static IEnumerable<GMacStructure> FrameStructures(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(frame => frame.Structures);
        }

        internal static IEnumerable<GMacMacro> Macros(this GMacNamespace parentNameSpace)
        {
            return
                Namespaces(parentNameSpace)
                .SelectMany(
                    nameSpace =>
                        nameSpace
                        .ChildMacros
                        .Concat(
                            nameSpace.ChildFrames.SelectMany(Macros)
                            )
                        .Concat(
                            nameSpace.ChildStructures.SelectMany(structure => structure.Macros)
                            )
                    );
        }

        internal static IEnumerable<GMacMacro> NamespaceMacros(this GMacNamespace parentNameSpace)
        {
            return Namespaces(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildMacros);
        }

        internal static IEnumerable<GMacMacro> FrameMacros(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildMacros);
        }

        internal static IEnumerable<GMacMacro> StructureMacros(this GMacNamespace parentNameSpace)
        {
            return Structures(parentNameSpace).SelectMany(nameSpace => nameSpace.Macros);
        }

        internal static IEnumerable<ILanguageType> Types(this GMacNamespace parentNameSpace)
        {
            return
                Namespaces(parentNameSpace)
                .SelectMany(
                    nameSpace =>
                        nameSpace
                        .ChildStructures
                        .Concat(nameSpace.ChildFrames.SelectMany(Types))
                    );
        }

        internal static IEnumerable<ILanguageType> FrameTypes(this GMacNamespace parentNameSpace)
        {
            return
                Namespaces(parentNameSpace)
                .SelectMany(
                    nameSpace =>
                        nameSpace.ChildFrames.Select(frame => frame.MultivectorType as ILanguageType)
                        .Concat(nameSpace.ChildFrames.SelectMany(frame => frame.Structures))
                    );
        }

        internal static IEnumerable<LanguageSymbol> MainChilsSymbols(this GMacNamespace parentNameSpace)
        {
            var roleNames = new[]
            {
                RoleNames.Constant,
                RoleNames.Frame,
                RoleNames.Macro,
                RoleNames.Namespace,
                RoleNames.Structure,
                RoleNames.Transform
            };

            return parentNameSpace.ChildScope.Symbols(roleNames);
        }

        internal static IEnumerable<LanguageSymbol> MainSymbols(this GMacNamespace parentNameSpace)
        {
            return 
                parentNameSpace
                .Namespaces()
                .SelectMany(MainChilsSymbols)
                .Concat(
                    parentNameSpace.ChildFrames.SelectMany(MainSymbols)
                    );
        }


        internal static IEnumerable<GMacNamespace> Namespaces(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Namespaces);
        }

        internal static IEnumerable<SymbolNamedValue> NamedValues(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(NamedValues);
        }

        internal static IEnumerable<SymbolNamedValue> FrameNamedValues(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameNamedValues);
        }

        internal static IEnumerable<GMacConstant> Constants(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Constants);
        }

        internal static IEnumerable<GMacConstant> NamespaceConstants(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(NamespaceConstants);
        }

        internal static IEnumerable<GMacConstant> FrameConstants(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameConstants);
        }

        internal static IEnumerable<GMacFrame> Frames(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Frames);
        }

        internal static IEnumerable<GMacFrameBasisVector> FrameBasisVectors(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameBasisVectors);
        }

        internal static IEnumerable<GMacFrameMultivector> FrameMultivectors(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameMultivectors);
        }

        internal static IEnumerable<GMacFrameSubspace> FrameSubspaces(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameSubspaces);
        }

        internal static IEnumerable<GMacStructure> Structures(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Structures);
        }

        internal static IEnumerable<GMacStructure> NamespaceStructures(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(NamespaceStructures);
        }

        internal static IEnumerable<GMacStructure> FrameStructures(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameStructures);
        }

        internal static IEnumerable<GMacMacro> Macros(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Macros);
        }

        internal static IEnumerable<GMacMacro> NamespaceMacros(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(NamespaceMacros);
        }

        internal static IEnumerable<GMacMacro> FrameMacros(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameMacros);
        }

        internal static IEnumerable<GMacMacro> StructureMacros(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(StructureMacros);
        }

        internal static IEnumerable<ILanguageType> Types(this GMacAst rootAst)
        {
            var primitiveTypes = new[] {rootAst.BooleanType, rootAst.IntegerType, rootAst.ScalarType};

            return primitiveTypes.Concat(rootAst.ChildNamespaces.SelectMany(Types));
        }

        internal static IEnumerable<ILanguageType> NamespaceTypes(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Types);
        }

        internal static IEnumerable<ILanguageType> FrameTypes(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameTypes);
        }

        internal static IEnumerable<LanguageSymbol> MainSymbols(this GMacAst rootAst)
        {
            return
                rootAst.ChildNamespaces.Concat(
                    rootAst.Namespaces().SelectMany(MainSymbols)
                    );
        }

        //TODO: Make more overloads for other GMacAST symbols
        internal static void SetSymbolicMathName(this GMacFrame frame, string mathName)
        {
            frame.GMacRootAst.SymbolicMathNames[frame] = mathName;
        }
    }
}
