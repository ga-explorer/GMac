using System;
using System.Collections.Generic;
using System.Linq;
using GMac.Engine.AST.Dependency;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler;
using GMac.Engine.Compiler.Semantic.AST;

namespace GMac.Engine.AST
{
    /// <summary>
    /// This class is the root of the GMacAST data structure
    /// </summary>
    public sealed class AstRoot : AstObject
    {
        /// <summary>
        /// The internal AST object
        /// </summary>
        public GMacAst AssociatedAst { get; }

        /// <summary>
        /// An expression compiler for translating value access strings into value access objects
        /// </summary>
        internal readonly GMacDynamicCompiler DynamicCompiler = new GMacDynamicCompiler();


        public override bool IsValid => AssociatedAst != null;

        public override AstRoot Root => this;

        public override bool IsValidAstRoot => AssociatedAst != null;

        /// <summary>
        /// The creation time of the root AST node
        /// </summary>
        public DateTime CreationTime => AssociatedAst.CreationTime;

        /// <summary>
        /// The primitive boolean type of this AST
        /// </summary>
        public AstType BooleanType => new AstType(AssociatedAst.BooleanType);

        /// <summary>
        /// The primitive integer type of this AST
        /// </summary>
        public AstType IntegerType => new AstType(AssociatedAst.IntegerType);

        /// <summary>
        /// The primitive scalar (real) type of this AST
        /// </summary>
        public AstType ScalarType => new AstType(AssociatedAst.ScalarType);

        /// <summary>
        /// All the main symbols of this AST in any level
        /// </summary>
        public IEnumerable<AstSymbol> MainSymbols
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .MainSymbols
                    .Select(pair => pair.Value.ToAstSymbol());

                //return
                //    AssociatedAst
                //    .MainSymbols()
                //    .Select(symbol => new GMacInfoAstSymbol(symbol));
            }
        }

        /// <summary>
        /// The direct child namespaces under this root
        /// </summary>
        public IEnumerable<AstNamespace> ChildNamespaces
        {
            get
            {
                return 
                    AssociatedAst
                    .ChildNamespaces
                    .Select(item => new AstNamespace(item));
            }
        }

        /// <summary>
        /// All the namespaces under this AST at any level
        /// </summary>
        public IEnumerable<AstNamespace> Namespaces
        {
            get
            {
                return 
                    AssociatedAst
                    .SymbolsCache
                    .Namespaces
                    .Select(pair => new AstNamespace(pair.Value));

                //return 
                //    AssociatedAst
                //    .Namespaces()
                //    .Select(item => new GMacInfoNamespace(item));
            }
        }

        /// <summary>
        /// All the frames under this AST at any level
        /// </summary>
        public IEnumerable<AstFrame> Frames
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .Frames
                    .Select(pair => new AstFrame(pair.Value));

                //return
                //    AssociatedAst
                //    .Frames()
                //    .Select(frame => new GMacInfoFrame(frame));
            }
        }

        /// <summary>
        /// All the frame basis vectors under this AST at any level
        /// </summary>
        public IEnumerable<AstFrameBasisVector> FrameBasisVectors
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .FrameBasisVectors
                    .Select(pair => new AstFrameBasisVector(pair.Value));

                //return
                //    AssociatedAst
                //    .FrameBasisVectors()
                //    .Select(mv => new GMacInfoFrameBasisVector(mv));
            }
        }

        /// <summary>
        /// All the frame multivector types under this AST at any level
        /// </summary>
        public IEnumerable<AstFrameMultivector> FrameMultivectors
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .FrameMultivectors
                    .Select(pair => new AstFrameMultivector(pair.Value));

                //return
                //    AssociatedAst
                //    .FrameMultivectors()
                //    .Select(mv => new GMacInfoFrameMultivector(mv));
            }
        }

        /// <summary>
        /// All the subspaces under this AST at any level
        /// </summary>
        public IEnumerable<AstFrameSubspace> Subspaces
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .FrameSubspaces
                    .Select(pair => new AstFrameSubspace(pair.Value));

                //return
                //    AssociatedAst
                //    .FrameSubspaces()
                //    .Select(symbol => new GMacInfoFrameSubspace(symbol));
            }
        }

        /// <summary>
        /// All the constants under this AST at any level
        /// </summary>
        public IEnumerable<AstConstant> Constants
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .Constants
                    .Select(pair => new AstConstant(pair.Value));

                //return
                //    AssociatedAst
                //    .Constants()
                //    .Select(item => new GMacInfoConstant(item));
            }
        }

        /// <summary>
        /// All the structures under this AST at any level
        /// </summary>
        public IEnumerable<AstStructure> Structures
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .Structures
                    .Select(pair => new AstStructure(pair.Value));

                //return
                //    AssociatedAst
                //    .Structures()
                //    .Select(structure => new GMacInfoStructure(structure));
            }
        }

        /// <summary>
        /// All the structures under this AST at any level
        /// </summary>
        public IEnumerable<AstTransform> Transforms
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .Transforms
                    .Select(pair => new AstTransform(pair.Value));

                //return
                //    AssociatedAst
                //    .Structures()
                //    .Select(structure => new GMacInfoStructure(structure));
            }
        }

        /// <summary>
        /// All the macros under this AST at any level
        /// </summary>
        public IEnumerable<AstMacro> Macros
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .Macros
                    .Select(pair => new AstMacro(pair.Value));

                //return
                //    AssociatedAst
                //    .Macros()
                //    .Select(macro => new GMacInfoMacro(macro));
            }
        }

        /// <summary>
        /// All the types under this AST at any level
        /// </summary>
        public IEnumerable<AstType> Types
        {
            get
            {
                return
                    AssociatedAst
                    .SymbolsCache
                    .Types
                    .Select(pair => new AstType(pair.Value));

                //return
                //    AssociatedAst
                //    .Types()
                //    .Select(symbol => new GMacInfoType(symbol));
            }
        }

        /// <summary>
        /// Find a symbol with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstSymbol Symbol(string accessName)
        {
            if (AssociatedAst.SymbolsCache.MainSymbols.TryGetValue(accessName, out var symbol))
                return symbol.ToAstSymbol();

            return null;
        }

        /// <summary>
        /// Find a namespace with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstNamespace Namespace(string accessName)
        {
            if (AssociatedAst.SymbolsCache.Namespaces.TryGetValue(accessName, out var symbol))
                return new AstNamespace(symbol);

            return null;
        }

        /// <summary>
        /// Find a frame with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrame Frame(string accessName)
        {
            AssociatedAst.SymbolsCache.Frames.TryGetValue(accessName, out var symbol);

            return new AstFrame(symbol);
        }

        /// <summary>
        /// Find a frame basis vector with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrameBasisVector BasisVector(string accessName)
        {
            AssociatedAst.SymbolsCache.FrameBasisVectors.TryGetValue(accessName, out var symbol);

            return new AstFrameBasisVector(symbol);
        }

        /// <summary>
        /// Find a frame multivector type with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrameMultivector FrameMultivector(string accessName)
        {
            AssociatedAst.SymbolsCache.FrameMultivectors.TryGetValue(accessName, out var symbol);

            return new AstFrameMultivector(symbol);
        }

        /// <summary>
        /// Find a subspace with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrameSubspace Subspace(string accessName)
        {
            AssociatedAst.SymbolsCache.FrameSubspaces.TryGetValue(accessName, out var symbol);

            return new AstFrameSubspace(symbol);
        }

        /// <summary>
        /// Find a constant with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstConstant Constant(string accessName)
        {
            AssociatedAst.SymbolsCache.Constants.TryGetValue(accessName, out var symbol);

            return new AstConstant(symbol);
        }

        /// <summary>
        /// Find a transform with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstTransform Transform(string accessName)
        {
            AssociatedAst.SymbolsCache.Transforms.TryGetValue(accessName, out var symbol);

            return new AstTransform(symbol);
        }

        /// <summary>
        /// Find a structure with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstStructure Structure(string accessName)
        {
            AssociatedAst.SymbolsCache.Structures.TryGetValue(accessName, out var symbol);

            return new AstStructure(symbol);
        }

        /// <summary>
        /// Find a macro with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstMacro Macro(string accessName)
        {
            AssociatedAst.SymbolsCache.Macros.TryGetValue(accessName, out var symbol);

            return new AstMacro(symbol);
        }

        /// <summary>
        /// Find a type with the given fully qualified name under this AST
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstType Type(string accessName)
        {
            AssociatedAst.SymbolsCache.Types.TryGetValue(accessName, out var symbol);

            return new AstType(symbol);
        }

        /// <summary>
        /// Construct a macro dependency graph of all macros under this AST
        /// </summary>
        /// <returns></returns>
        public AstMacroDependencyGraph GetMacroDependencies()
        {
            var dep = new AstMacroDependencyGraph(this);

            dep.Populate();

            return dep;
        }

        /// <summary>
        /// Construct a type dependency graph of all types under this AST
        /// </summary>
        /// <returns></returns>
        public AstTypeDependencyGraph GetTypeDependencies()
        {
            var dep = new AstTypeDependencyGraph(this);

            dep.Populate();

            return dep;
        }

        /// <summary>
        /// Clears all the symbolic math names associated with GMacAST symbols
        /// </summary>
        public void ClearSymbolicMathNames()
        {
            AssociatedAst.SymbolicMathNames.Clear();
        }

        public AstRoot(GMacAst ast)
        {
            AssociatedAst = ast;
        }
    }
}
