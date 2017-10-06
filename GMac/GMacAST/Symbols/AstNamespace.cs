using System.Collections.Generic;
using System.Linq;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;

namespace GMac.GMacAST.Symbols
{
    /// <summary>
    /// This class represents a namespace in GMacAST
    /// </summary>
    public sealed class AstNamespace : AstSymbol
    {
        #region Static members
        #endregion


        internal GMacNamespace AssociatedNamespace { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedNamespace;


        public override bool IsValidNamespace => AssociatedNamespace != null;

        /// <summary>
        /// True if this namespace is a direct child of the AST root object
        /// </summary>
        public bool IsRootNamespace => AssociatedNamespace.HasParentSymbol == false;

        /// <summary>
        /// The child symbols of this namsepace
        /// </summary>
        public IEnumerable<AstSymbol> ChildSymbols
        {
            get
            {
                return
                    AssociatedNamespace
                    .ChildSymbols
                    .Select(item => item.ToAstSymbol());
            }
        }

        /// <summary>
        /// The child namespaces of this namespace
        /// </summary>
        public IEnumerable<AstNamespace> ChildNamespaces
        {
            get
            {
                return
                    AssociatedNamespace
                    .ChildNamespaces
                    .Select(item => new AstNamespace(item));
            }
        }

        /// <summary>
        /// The child frames of this namespace
        /// </summary>
        public IEnumerable<AstFrame> ChildFrames
        {
            get
            {
                return
                    AssociatedNamespace
                    .ChildFrames
                    .Select(item => new AstFrame(item));
            }
        }

        /// <summary>
        /// The child constants of this namespace
        /// </summary>
        public IEnumerable<AstConstant> ChildConstants
        {
            get
            {
                return
                    AssociatedNamespace
                    .ChildConstants
                    .Select(item => new AstConstant(item));
            }
        }

        /// <summary>
        /// The child structures of this namespace
        /// </summary>
        public IEnumerable<AstStructure> ChildStructures
        {
            get
            {
                return
                    AssociatedNamespace
                    .ChildStructures
                    .Select(item => new AstStructure(item));
            }
        }

        /// <summary>
        /// The child structures of this namespace
        /// </summary>
        public IEnumerable<AstTransform> ChildTransforms
        {
            get
            {
                return
                    AssociatedNamespace
                    .ChildTransforms
                    .Select(item => new AstTransform(item));
            }
        }

        /// <summary>
        /// The child macros of this namespace
        /// </summary>
        public IEnumerable<AstMacro> ChildMacros
        {
            get
            {
                return
                    AssociatedNamespace
                    .ChildMacros
                    .Select(item => new AstMacro(item));
            }
        }


        /// <summary>
        /// Returns this namespace AST object followed by all its parents up to the root namespace
        /// </summary>
        public IEnumerable<AstNamespace> NamespacesChain
        {
            get
            {
                for (var nameSpaceInfo = this; nameSpaceInfo.IsNotNullAndValid(); nameSpaceInfo = nameSpaceInfo.ParentNamespace)
                    yield return nameSpaceInfo;
            }
        }

        /// <summary>
        /// All the main symbols of this namespace in any level
        /// </summary>
        public IEnumerable<AstSymbol> MainSymbols
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .MainSymbols
                    .Select(pair => pair.Value.ToAstSymbol());

                //return
                //    AssociatedNamespace
                //    .MainSymbols()
                //    .Select(symbol => new GMacInfoAstSymbol(symbol));
            }
        }

        /// <summary>
        /// All the namespaces under this namespace in any level
        /// </summary>
        public IEnumerable<AstNamespace> Namespaces
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .Namespaces
                    .Select(pair => new AstNamespace(pair.Value));

                //return 
                //    AssociatedNamespace
                //    .Namespaces()
                //    .Select(item => new GMacInfoNamespace(item));
            }
        }

        /// <summary>
        /// All the frames under this namespace in any level
        /// </summary>
        public IEnumerable<AstFrame> Frames
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .Frames
                    .Select(pair => new AstFrame(pair.Value));

                //return
                //    AssociatedNamespace
                //    .Frames()
                //    .Select(frame => new GMacInfoFrame(frame));
            }
        }

        /// <summary>
        /// All the basis vectors under this namespace in any level
        /// </summary>
        public IEnumerable<AstFrameBasisVector> FrameBasisVectors
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .FrameBasisVectors
                    .Select(pair => new AstFrameBasisVector(pair.Value));

                //return
                //    AssociatedNamespace
                //    .FrameBasisVectors()
                //    .Select(mv => new GMacInfoFrameBasisVector(mv));
            }
        }

        /// <summary>
        /// All the multivector types under this namespace in any level
        /// </summary>
        public IEnumerable<AstFrameMultivector> FrameMultivectors
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .FrameMultivectors
                    .Select(pair => new AstFrameMultivector(pair.Value));

                //return
                //    AssociatedNamespace
                //    .FrameMultivectors()
                //    .Select(mv => new GMacInfoFrameMultivector(mv));
            }
        }

        /// <summary>
        /// All the frame subspaces under this namespace in any level
        /// </summary>
        public IEnumerable<AstFrameSubspace> Subspaces
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .FrameSubspaces
                    .Select(pair => new AstFrameSubspace(pair.Value));
            }
        }

        /// <summary>
        /// All the constants under this namespace in any level
        /// </summary>
        public IEnumerable<AstConstant> Constants
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .Constants
                    .Select(pair => new AstConstant(pair.Value));

                //return
                //    AssociatedNamespace
                //    .Constants()
                //    .Select(item => new GMacInfoConstant(item));
            }
        }

        /// <summary>
        /// All the structures under this namespace in any level
        /// </summary>
        public IEnumerable<AstStructure> Structures
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .Structures
                    .Select(pair => new AstStructure(pair.Value));

                //return
                //    AssociatedNamespace
                //    .Structures()
                //    .Select(structure => new GMacInfoStructure(structure));
            }
        }

        /// <summary>
        /// All the transforms under this namespace in any level
        /// </summary>
        public IEnumerable<AstTransform> Transforms
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .Transforms
                    .Select(pair => new AstTransform(pair.Value));

                //return
                //    AssociatedNamespace
                //    .Structures()
                //    .Select(structure => new GMacInfoStructure(structure));
            }
        }

        /// <summary>
        /// All the macros under this namespace in any level
        /// </summary>
        public IEnumerable<AstMacro> Macros
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .Macros
                    .Select(pair => new AstMacro(pair.Value));

                //return
                //    AssociatedNamespace
                //    .Macros()
                //    .Select(macro => new GMacInfoMacro(macro));
            }
        }

        /// <summary>
        /// All the types under this namespace in any level
        /// </summary>
        public IEnumerable<AstType> Types
        {
            get
            {
                return
                    AssociatedNamespace
                    .SymbolsCache
                    .Types
                    .Select(pair => new AstType(pair.Value));

                //return
                //    AssociatedNamespace
                //    .Types()
                //    .Select(symbol => new GMacInfoType(symbol));
            }
        }

        /// <summary>
        /// Find a symbol with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstSymbol Symbol(string accessName)
        {
            LanguageSymbol symbol;

            AssociatedNamespace.SymbolsCache.MainSymbols.TryGetValue(accessName, out symbol);

            return symbol.ToAstSymbol();
        }

        /// <summary>
        /// Find a namespace with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstNamespace Namespace(string accessName)
        {
            GMacNamespace symbol;

            AssociatedNamespace.SymbolsCache.Namespaces.TryGetValue(accessName, out symbol);

            return new AstNamespace(symbol);
        }

        /// <summary>
        /// Find a frame with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrame Frame(string accessName)
        {
            GMacFrame symbol;

            AssociatedNamespace.SymbolsCache.Frames.TryGetValue(accessName, out symbol);

            return new AstFrame(symbol);
        }

        /// <summary>
        /// Find a basis vector with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrameBasisVector BasisVector(string accessName)
        {
            GMacFrameBasisVector symbol;

            AssociatedNamespace.SymbolsCache.FrameBasisVectors.TryGetValue(accessName, out symbol);

            return new AstFrameBasisVector(symbol);
        }

        /// <summary>
        /// Find a multivector type with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrameMultivector FrameMultivector(string accessName)
        {
            GMacFrameMultivector symbol;

            AssociatedNamespace.SymbolsCache.FrameMultivectors.TryGetValue(accessName, out symbol);

            return new AstFrameMultivector(symbol);
        }

        /// <summary>
        /// Find a subspace with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrameSubspace Subspace(string accessName)
        {
            GMacFrameSubspace symbol;

            AssociatedNamespace.SymbolsCache.FrameSubspaces.TryGetValue(accessName, out symbol);

            return new AstFrameSubspace(symbol);
        }

        /// <summary>
        /// Find a constant with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstConstant Constant(string accessName)
        {
            GMacConstant symbol;

            AssociatedNamespace.SymbolsCache.Constants.TryGetValue(accessName, out symbol);

            return new AstConstant(symbol);
        }

        /// <summary>
        /// Find a structure with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstStructure Structure(string accessName)
        {
            GMacStructure symbol;

            AssociatedNamespace.SymbolsCache.Structures.TryGetValue(accessName, out symbol);

            return new AstStructure(symbol);
        }

        /// <summary>
        /// Find a transform with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstTransform Transform(string accessName)
        {
            GMacMultivectorTransform symbol;

            AssociatedNamespace.SymbolsCache.Transforms.TryGetValue(accessName, out symbol);

            return new AstTransform(symbol);
        }

        /// <summary>
        /// Find a macro with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstMacro Macro(string accessName)
        {
            GMacMacro symbol;

            AssociatedNamespace.SymbolsCache.Macros.TryGetValue(accessName, out symbol);

            return new AstMacro(symbol);
        }

        /// <summary>
        /// Find a type with the given qualified name under this namespace
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstType Type(string accessName)
        {
            ILanguageType symbol;

            AssociatedNamespace.SymbolsCache.Types.TryGetValue(accessName, out symbol);

            return new AstType(symbol);
        }


        internal AstNamespace(GMacNamespace nameSpave)
        {
            AssociatedNamespace = nameSpave;
        }
    }
}
