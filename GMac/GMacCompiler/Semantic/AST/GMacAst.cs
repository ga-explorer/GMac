using System;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Semantic.ASTDebug;
using GMac.GMacMath.Symbolic;
using IronyGrammars.Semantic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Type;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib;
using TextComposerLib.Text.Linear;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// The GMac DSL
    /// </summary>
    public sealed class GMacAst : IronyAst
    {
        /// <summary>
        /// All child namespaces
        /// </summary>
        internal IEnumerable<GMacNamespace> ChildNamespaces { get; private set; }

        /// <summary>
        /// The number of frames defined inside this AST
        /// </summary>
        internal int FramesCount { get; set; }

        /// <summary>
        /// The built-in scalar type
        /// </summary>
        internal TypePrimitive ScalarType { get; private set; }

        /// <summary>
        /// The built-in integer type
        /// </summary>
        internal TypePrimitive IntegerType { get; private set; }

        /// <summary>
        /// The built-in boolean type
        /// </summary>
        internal TypePrimitive BooleanType { get; private set; }

        /// <summary>
        /// Used to store the symbolic (LaTeX, or MathML for example) names associated GMacAST symbols
        /// like frames, subspaces, basis vectors, structures, etc.
        /// </summary>
        internal GMacSymbolicMathNames SymbolicMathNames { get; private set; }

        /// <summary>
        /// The symbols cache used to speed access to sub-symbols
        /// </summary>
        internal GMacAstSymbolsCache SymbolsCache { get; private set; }


        internal GMacAst()
            : base(new GMacAstDescription())
        {
            UseNamespaces = true;

            ChildNamespaces = RootScope.Symbols(RoleNames.Namespace).Cast<GMacNamespace>();

            SymbolicMathNames = new GMacSymbolicMathNames(this);
        }


        /// <summary>
        /// Create all GMac DSL symbol roles
        /// </summary>
        protected override void InitializeLanguageRoles()
        {
            DefineLanguageRole(RoleNames.MacroTemplate, "Macro Template", true);

            DefineLanguageRole(RoleNames.Transform, "Multivector Linear Transformation", true);

            DefineLanguageRole(RoleNames.FrameBasisVector, "GA Frame Basis Vector", true);

            DefineLanguageRole(RoleNames.FrameSubspace, "GA Frame Subspace", true);

            DefineLanguageRole(RoleNames.Namespace, "Namespace", true);

            DefineLanguageRole(RoleNames.Frame, "GA Frame", true);

            DefineLanguageRole(RoleNames.FrameMultivector, "Frame Multivector (GMac Type)", true);

            DefineLanguageRole(RoleNames.Macro, "Macro", true);

            DefineLanguageRole(RoleNames.Structure, "Structure (GMac Type)", true);

            DefineLanguageRole(RoleNames.Constant, "Constant", true);

            DefineLanguageRole(RoleNames.StructureDataMember, "Structure Data Member", false);

            DefineLanguageRole(RoleNames.BuiltinType, "Builtin Type", false);

            DefineLanguageRole(RoleNames.MacroParameter, "Macro Parameter", false);

            DefineLanguageRole(RoleNames.LocalVariable, "Local Variable", false);

            //DefineLanguageRole(RoleNames.AccessScheme, "Target Access Scheme", true);

            //DefineLanguageRole(RoleNames.Binding, "Target Binding", true);
        }

        /// <summary>
        /// Assign some primary symbol roles to the fixed members of the GMac DSL for ease of use
        /// </summary>
        protected override void InitializeFixedLanguageRoleNames()
        {
            //Built-in types
            TypePrimitiveRoleName = RoleNames.BuiltinType;

            //Procedures
            ProcedureRoleName = RoleNames.Macro;

            //Procedure parameters
            ProcedureParameterRoleName = RoleNames.MacroParameter;

            //Local variables
            LocalVariableRoleName = RoleNames.LocalVariable;

            //Namespaces
            NamespaceRoleName = RoleNames.Namespace;

            //Structures
            StructureRoleName = RoleNames.Structure;

            //Structure data members
            StructureDataMemberRoleName = RoleNames.StructureDataMember;
        }


        /// <summary>
        /// Create language operator names used in expressions
        /// </summary>
        protected override void InitializeLanguageOperators()
        {
            foreach (var opInfo in GMacOpInfo.AllOps)
                if (opInfo.HasSymbol)
                    DefineLanguageOperatorPrimitive(opInfo.OpName, opInfo.OpSymbol);
                else
                    DefineLanguageOperatorPrimitive(opInfo.OpName);
        }

        /// <summary>
        /// Create built-in types
        /// </summary>
        protected override void InitializePrimitiveLanguageTypes()
        {
            //base.InitializePrimitiveLanguageTypes()

            DefineTypePrimitiveUnit(TypeNames.Unit);

            BooleanType = DefineTypePrimitive(TypeNames.Bool);
            ScalarType = DefineTypePrimitive(TypeNames.Scalar);
            IntegerType = DefineTypePrimitive(TypeNames.Integer);
        }

        public override ILanguageValue CreateDefaultValue(ILanguageType langType)
        {
            if (ReferenceEquals(langType, null))
                throw new ArgumentNullException();

            if (langType.IsSameType(BooleanType))
                return ValuePrimitive<bool>.Create((TypePrimitive)langType, false);

            if (langType.IsSameType(IntegerType))
                return ValuePrimitive<int>.Create((TypePrimitive)langType, 0);

            if (langType.IsSameType(ScalarType))
                return ValuePrimitive<MathematicaScalar>.Create((TypePrimitive)langType, SymbolicUtils.Constants.Zero);

            var typeStructure = langType as GMacStructure;

            if (typeStructure != null)
            {
                var structure = typeStructure;

                var valueSparse = ValueStructureSparse.Create(structure);

                //This code is not required for a sparse structure value
                //foreach (var data_member in structure.DataMembers)
                //    value_sparse[data_member.ObjectName] = this.CreateDefaultValue(data_member.SymbolType);

                return valueSparse;
            }

            if (!(langType is GMacFrameMultivector)) 
                throw new InvalidOperationException("GMac type not recognized!");

            var mvType = (GMacFrameMultivector)langType;

            var value = GMacValueMultivector.CreateZero(mvType);

            //This code is not required for a sparse multivector value
            //for (int id = 0; id < mv_type.ParentFrame.GASpaceDimension; id++)
            //    value[id] = SymbolicUtils.Constants.Zero;

            return value;
        }

        /// <summary>
        /// Create a root namespace of the GMac DSL
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <returns></returns>
        internal GMacNamespace DefineRootNamespace(string namespaceName)
        {
            return new GMacNamespace(namespaceName, RootScope);
        }


        internal bool LookupRootNamespace(string symbolName, out GMacNamespace nameSpace)
        {
            return RootScope.LookupSymbol(symbolName, RoleNames.Namespace, out nameSpace);
        }

        public override string Describe(IIronyAstObject astItem)
        {
            if (ReferenceEquals(astItem, null))
                return "<null ast item>";

            var astDescr = new GMacAstDescription();

            astDescr.Log.Clear();

            astItem.AcceptVisitor(astDescr);

            return astDescr.Log.ToString();
        }

        /// <summary>
        /// Create the symbols cache for this AST
        /// </summary>
        internal void CreateSymbolsCache()
        {
            SymbolsCache = new GMacAstSymbolsCache(this);
        }

        public override void FinalizeAst()
        {
            foreach (var astNamespace in this.Namespaces())
                astNamespace.CreateSymbolsCache();

            foreach (var frame in this.Frames())
                frame.CreateSymbolsCache();

            CreateSymbolsCache();
        }


        /// <summary>
        /// This method generates a full report regarding any errors in the structure
        /// of this AST
        /// </summary>
        /// <returns></returns>
        public string VerifyAst()
        {
            var composer = new LinearComposer();

            //Chack for macros without a command body
            foreach (var macro in this.Macros())
            {
                if (macro.SymbolBody == null)
                    composer
                        .AppendAtNewLine("Macro <")
                        .Append(macro.SymbolAccessName)
                        .AppendLine("> Has null Command Body!");
            }

            return composer.ToString();
        }
    }
}
