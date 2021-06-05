using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.Compiler.Semantic.AST
{
    public static class GMacAstUtils
    {
        public static IEnumerable<SymbolNamedValue> NamedValues(this GMacFrame parentFrame)
        {
            return
                parentFrame.FrameBasisVectors.Cast<SymbolNamedValue>()
                .Concat(parentFrame.ChildConstants);
        }

        public static IEnumerable<ILanguageType> Types(this GMacFrame parentFrame)
        {
            return
                Enumerable
                .Repeat(parentFrame.MultivectorType as ILanguageType, 1)
                .Concat(parentFrame.Structures);
        }

        public static IEnumerable<GMacMacro> Macros(this GMacFrame parentFrame)
        {
            return
                parentFrame.ChildMacros
                .Concat(parentFrame.Structures.SelectMany(s => s.Macros));
        }

        public static IEnumerable<LanguageSymbol> MainSymbols(this GMacFrame parentFrame)
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

        
        public static IEnumerable<GMacNamespace> Namespaces(this GMacNamespace parentNameSpace)
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

        public static IEnumerable<SymbolNamedValue> NamedValues(this GMacNamespace parentNameSpace)
        {
            return
                Namespaces(parentNameSpace)
                .SelectMany(
                    nameSpace =>
                        nameSpace.ChildConstants
                        .Concat(nameSpace.ChildFrames.SelectMany(NamedValues))
                    );
        }

        public static IEnumerable<SymbolNamedValue> FrameNamedValues(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(NamedValues);
        }

        public static IEnumerable<GMacConstant> Constants(this GMacNamespace parentNameSpace)
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

        public static IEnumerable<GMacConstant> NamespaceConstants(this GMacNamespace parentNameSpace)
        {
            return Namespaces(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildConstants);
        }

        public static IEnumerable<GMacConstant> FrameConstants(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(frame => frame.ChildConstants);
        }

        public static IEnumerable<GMacFrame> Frames(this GMacNamespace parentNameSpace)
        {
            return Namespaces(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildFrames);
        }

        public static IEnumerable<GMacFrameBasisVector> FrameBasisVectors(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(frame => frame.FrameBasisVectors);
        }

        public static IEnumerable<GMacFrameMultivector> FrameMultivectors(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).Select(frame => frame.MultivectorType);
        }

        public static IEnumerable<GMacFrameSubspace> FrameSubspaces(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(frame => frame.FrameSubspaces);
        }

        public static IEnumerable<GMacStructure> Structures(this GMacNamespace parentNameSpace)
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

        public static IEnumerable<GMacStructure> NamespaceStructures(this GMacNamespace parentNameSpace)
        {
            return Namespaces(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildStructures);
        }

        public static IEnumerable<GMacStructure> FrameStructures(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(frame => frame.Structures);
        }

        public static IEnumerable<GMacMacro> Macros(this GMacNamespace parentNameSpace)
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

        public static IEnumerable<GMacMacro> NamespaceMacros(this GMacNamespace parentNameSpace)
        {
            return Namespaces(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildMacros);
        }

        public static IEnumerable<GMacMacro> FrameMacros(this GMacNamespace parentNameSpace)
        {
            return Frames(parentNameSpace).SelectMany(nameSpace => nameSpace.ChildMacros);
        }

        public static IEnumerable<GMacMacro> StructureMacros(this GMacNamespace parentNameSpace)
        {
            return Structures(parentNameSpace).SelectMany(nameSpace => nameSpace.Macros);
        }

        public static IEnumerable<ILanguageType> Types(this GMacNamespace parentNameSpace)
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

        public static IEnumerable<ILanguageType> FrameTypes(this GMacNamespace parentNameSpace)
        {
            return
                Namespaces(parentNameSpace)
                .SelectMany(
                    nameSpace =>
                        nameSpace.ChildFrames.Select(frame => frame.MultivectorType as ILanguageType)
                        .Concat(nameSpace.ChildFrames.SelectMany(frame => frame.Structures))
                    );
        }

        public static IEnumerable<LanguageSymbol> MainChilsSymbols(this GMacNamespace parentNameSpace)
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

        public static IEnumerable<LanguageSymbol> MainSymbols(this GMacNamespace parentNameSpace)
        {
            return 
                parentNameSpace
                .Namespaces()
                .SelectMany(MainChilsSymbols)
                .Concat(
                    parentNameSpace.ChildFrames.SelectMany(MainSymbols)
                    );
        }


        public static IEnumerable<GMacNamespace> Namespaces(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Namespaces);
        }

        public static IEnumerable<SymbolNamedValue> NamedValues(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(NamedValues);
        }

        public static IEnumerable<SymbolNamedValue> FrameNamedValues(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameNamedValues);
        }

        public static IEnumerable<GMacConstant> Constants(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Constants);
        }

        public static IEnumerable<GMacConstant> NamespaceConstants(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(NamespaceConstants);
        }

        public static IEnumerable<GMacConstant> FrameConstants(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameConstants);
        }

        public static IEnumerable<GMacFrame> Frames(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Frames);
        }

        public static IEnumerable<GMacFrameBasisVector> FrameBasisVectors(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameBasisVectors);
        }

        public static IEnumerable<GMacFrameMultivector> FrameMultivectors(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameMultivectors);
        }

        public static IEnumerable<GMacFrameSubspace> FrameSubspaces(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameSubspaces);
        }

        public static IEnumerable<GMacStructure> Structures(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Structures);
        }

        public static IEnumerable<GMacStructure> NamespaceStructures(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(NamespaceStructures);
        }

        public static IEnumerable<GMacStructure> FrameStructures(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameStructures);
        }

        public static IEnumerable<GMacMacro> Macros(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Macros);
        }

        public static IEnumerable<GMacMacro> NamespaceMacros(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(NamespaceMacros);
        }

        public static IEnumerable<GMacMacro> FrameMacros(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameMacros);
        }

        public static IEnumerable<GMacMacro> StructureMacros(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(StructureMacros);
        }

        public static IEnumerable<ILanguageType> Types(this GMacAst rootAst)
        {
            var primitiveTypes = new[] {rootAst.BooleanType, rootAst.IntegerType, rootAst.ScalarType};

            return primitiveTypes.Concat(rootAst.ChildNamespaces.SelectMany(Types));
        }

        public static IEnumerable<ILanguageType> NamespaceTypes(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(Types);
        }

        public static IEnumerable<ILanguageType> FrameTypes(this GMacAst rootAst)
        {
            return rootAst.ChildNamespaces.SelectMany(FrameTypes);
        }

        public static IEnumerable<LanguageSymbol> MainSymbols(this GMacAst rootAst)
        {
            return
                rootAst.ChildNamespaces.Concat(
                    rootAst.Namespaces().SelectMany(MainSymbols)
                    );
        }

        //TODO: Make more overloads for other GMacAST symbols
        public static void SetSymbolicMathName(this GMacFrame frame, string mathName)
        {
            frame.GMacRootAst.SymbolicMathNames[frame] = mathName;
        }
    }
}
