using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstDatastoreValueAccess : AstExpression, IAstObjectWithDatastoreValueAccess
    {
        #region Static members
        #endregion


        internal LanguageValueAccess AssociatedValueAccess { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedValueAccess;


        public AstDatastoreValueAccess DatastoreValueAccess => this;

        public override bool IsValidDatastoreValueAccess => AssociatedValueAccess != null;

        /// <summary>
        /// The full name of this datastore value access
        /// </summary>
        public string ValueAccessName => AssociatedValueAccess.GetName();

        /// <summary>
        /// The full name of this datastore value access except for its root
        /// </summary>
        public string PartialAccessName
        {
            get
            {
                if (AssociatedValueAccess.IsFullAccess) return String.Empty;

                var name = ValueAccessName;

                return name.Substring(name.IndexOf('.') + 1);
            }
        }

        /// <summary>
        /// The root symbol of this datastore value access
        /// </summary>
        public AstSymbol RootSymbol => AssociatedValueAccess.RootSymbol.ToAstSymbol();

        /// <summary>
        /// The root macro parameter of this datastore value access
        /// </summary>
        public AstMacroParameter RootAsMacroParameter => new AstMacroParameter(AssociatedValueAccess.RootSymbolAsParameter);

        /// <summary>
        /// The root local variable of this datastore value access
        /// </summary>
        public AstLocalVariable RootAsLocalVariable => new AstLocalVariable(AssociatedValueAccess.RootSymbolAsLocalVariable);

        /// <summary>
        /// The root basis vector of this datastore value access
        /// </summary>
        public AstFrameBasisVector RootAsBasisVector => new AstFrameBasisVector(AssociatedValueAccess.RootSymbol as GMacFrameBasisVector);

        /// <summary>
        /// The root constant of this datastore value access
        /// </summary>
        public AstConstant RootAsConstant => new AstConstant(AssociatedValueAccess.RootSymbol as GMacConstant);

        /// <summary>
        /// The root structure data member of this datastore value access
        /// </summary>
        public AstStructureDataMember RootAsDataMember => new AstStructureDataMember(AssociatedValueAccess.RootSymbol as SymbolStructureDataMember);

        /// <summary>
        /// The multivector tyoe of this datastore value access
        /// </summary>
        public AstFrameMultivector TypeAsFrameMultivector 
        { 
            get 
            {
                var typeMv = AssociatedValueAccess.ExpressionType as GMacFrameMultivector;

                return 
                    ReferenceEquals(typeMv, null)
                    ? null
                    : new AstFrameMultivector(typeMv);
            } 
        }

        /// <summary>
        /// The structure type of this datastore value access
        /// </summary>
        public AstStructure TypeAsStructure
        {
            get
            {
                var typeStructure = AssociatedValueAccess.ExpressionType as GMacStructure;

                return 
                    ReferenceEquals(typeStructure, null)
                    ? null
                    : new AstStructure(typeStructure);
            }
        }

        /// <summary>
        /// True if this is a full value access
        /// </summary>
        public bool IsFullAccess => AssociatedValueAccess.IsFullAccess;

        /// <summary>
        /// True if this is a partial value access
        /// </summary>
        public bool IsPartialAccess => AssociatedValueAccess.IsPartialAccess;

        /// <summary>
        /// True if the type of this value access is a primitive type
        /// </summary>
        public bool IsPrimitive => AssociatedValueAccess.IsPrimitive;

        /// <summary>
        /// True if the type of this value access is a composite type
        /// </summary>
        public bool IsComposite => AssociatedValueAccess.IsPrimitive == false;

        /// <summary>
        /// True if the type of this value access is a scalar
        /// </summary>
        public bool IsScalar => AssociatedValueAccess.ExpressionType.IsScalar();

        /// <summary>
        /// True if the type of this value access is a multivector
        /// </summary>
        public bool IsMultivector => AssociatedValueAccess.IsMultivector();

        /// <summary>
        /// True if the type of this value access is a multivector with all coefficients fully accessed
        /// </summary>
        public bool IsFullMultivector => AssociatedValueAccess.IsFullMultivector();

        /// <summary>
        /// True if the type of this value access is a multivector with coefficients partially accessed
        /// </summary>
        public bool IsPartialMultivector => AssociatedValueAccess.IsPartialMultivector();

        /// <summary>
        /// True if the type of this value access is a scalar comming from a multivector coefficient
        /// </summary>
        public bool IsMultivectorCoefficient => AssociatedValueAccess.IsMultivectorCoefficient();

        /// <summary>
        /// True if the type of this value access is a structure
        /// </summary>
        public bool IsStructure => AssociatedValueAccess.IsStructure();

        /// <summary>
        /// True if the root symbol is a macro parameter
        /// </summary>
        public bool IsMacroParameter => AssociatedValueAccess.IsParameter;

        /// <summary>
        /// True if the root symbol is an input macro parameter
        /// </summary>
        public bool IsInputParameter => AssociatedValueAccess.IsInputParameter;

        /// <summary>
        /// True if the root symbol is an output macro parameter
        /// </summary>
        public bool IsOutputParameter => AssociatedValueAccess.IsOutputParameter;

        /// <summary>
        /// True if the root symbol is a local variable
        /// </summary>
        public bool IsLocalVariable => AssociatedValueAccess.IsLocalVariable;

        /// <summary>
        /// True if the root symbol is a names value (a constant or basis vector for example)
        /// </summary>
        public bool IsNamedValue => AssociatedValueAccess.IsNamedValue;

        /// <summary>
        /// True if the root symbol is a frame basis vector
        /// </summary>
        public bool IsBasisVector => AssociatedValueAccess.RootSymbol is GMacFrameBasisVector;

        /// <summary>
        /// True if the root symbol is a constant
        /// </summary>
        public bool IsConstant => AssociatedValueAccess.RootSymbol is GMacConstant;

        /// <summary>
        /// True if the root symbol is a structure data member
        /// </summary>
        public bool IsDataMember => AssociatedValueAccess.RootSymbol is SymbolStructureDataMember;


        internal AstDatastoreValueAccess(LanguageValueAccess valueAccess)
        {
            AssociatedValueAccess = valueAccess;
        }


        /// <summary>
        /// If this value access is on a multivector coefficient, get the basis blade of the coefficient
        /// </summary>
        /// <returns></returns>
        public AstFrameBasisBlade GetBasisBlade()
        {
            if (AssociatedValueAccess.IsPartialAccess == false) 
                return null;

            var mvType = 
                AssociatedValueAccess.NextToLastAccessStep.AccessStepType as GMacFrameMultivector;

            if (ReferenceEquals(mvType, null))
                return null;

            var lastStep = AssociatedValueAccess.LastAccessStep as ValueAccessStepByKey<int>;

            if (ReferenceEquals(lastStep, null))
                return null;

            return new AstFrameBasisBlade(mvType.ParentFrame, lastStep.AccessKey);
        }

        /// <summary>
        /// If this value access is on a multivector coefficient, get the ID of the basis blade
        /// </summary>
        /// <returns></returns>
        public int GetBasisBladeId()
        {
            return AssociatedValueAccess.GetBasisBladeId();
        }

        /// <summary>
        /// If this value access is on a multivector partial coefficients access, 
        /// get the basis blades of the coefficients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> GetBasisBladesList()
        {
            if (AssociatedValueAccess.IsPartialAccess == false)
                return null;

            var mvType =
                AssociatedValueAccess.NextToLastAccessStep.AccessStepType as GMacFrameMultivector;

            if (ReferenceEquals(mvType, null))
                return null;

            var lastStep = AssociatedValueAccess.LastAccessStep as ValueAccessStepByKeyList<int>;

            if (ReferenceEquals(lastStep, null))
                return null;

            return 
                lastStep
                .AccessKeyList
                .Select(
                    id => new AstFrameBasisBlade(mvType.ParentFrame, id)
                    );
        }

        /// <summary>
        /// If this value access is on a multivector partial coefficients access, 
        /// get the basis blades IDs of the coefficients
        /// </summary>
        /// <returns></returns>
        public List<int> GetBasisBladeIDsList()
        {
            var idsList = AssociatedValueAccess.GetBasisBladeIdsList();

            return idsList == null ? null : new List<int>(idsList);
        }

        /// <summary>
        /// Expand this datastore value access into a list of primitive value access expressions 
        /// on scalar values
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AstDatastoreValueAccess> ExpandAll()
        {
            return
                AssociatedValueAccess
                .ExpandAll()
                .Select(item => new AstDatastoreValueAccess(item));
        }

        /// <summary>
        /// Expand this datastore value access into a list of value access expressions 
        /// on scalar and multivector values
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AstDatastoreValueAccess> ExpandStructures()
        {
            return
                AssociatedValueAccess
                .ExpandStructures()
                .Select(item => new AstDatastoreValueAccess(item));
        }

        /// <summary>
        /// If this value access is on a multivector coefficient, split this value access into two parts: 
        /// A value access on the multivector type and a basis blade
        /// </summary>
        /// <returns></returns>
        public Tuple<AstDatastoreValueAccess, AstFrameBasisBlade> SplitAs_MultivectorCoefficient()
        {
            var id = AssociatedValueAccess.GetBasisBladeId();

            if (id < 0)
                return null;

            var frame = AssociatedValueAccess.LastAccessStep.AccessStepType.GetFrame();

            var newValueAccess = AssociatedValueAccess.DuplicateExceptLast();

            return new Tuple<AstDatastoreValueAccess, AstFrameBasisBlade>(
                new AstDatastoreValueAccess(newValueAccess),
                new AstFrameBasisBlade(frame, id)
                );
        }

        /// <summary>
        /// If this value access is on a partial multivector coefficients list, split this value access into 
        /// two parts: A value access on the multivector type and a list of basis blades
        /// </summary>
        /// <returns></returns>
        public Tuple<AstDatastoreValueAccess, List<AstFrameBasisBlade>> SplitAs_PartialMultivector()
        {
            var idsList = AssociatedValueAccess.GetBasisBladeIdsList();

            if (idsList == null)
                return null;

            var frame = AssociatedValueAccess.LastAccessStep.AccessStepType.GetFrame();

            var newValueAccess = AssociatedValueAccess.DuplicateExceptLast();

            return new Tuple<AstDatastoreValueAccess, List<AstFrameBasisBlade>>(
                new AstDatastoreValueAccess(newValueAccess),
                idsList.Select(id => new AstFrameBasisBlade(frame, id)).ToList()
                );
        }

        /// <summary>
        /// If this value access is a multivector this selects components belonging to the given subspace
        /// into a new value access of the same multivector type
        /// </summary>
        /// <param name="subspace"></param>
        /// <returns></returns>
        public AstDatastoreValueAccess SelectMultivectorComponents(AstFrameSubspace subspace)
        {
            return SelectMultivectorComponents(subspace.BasisBladeIDs);
        }

        /// <summary>
        /// If this value access is a multivector this selects components belonging to the given subspace
        /// into a new value access of the same multivector type
        /// </summary>
        /// <param name="idsList"></param>
        /// <returns></returns>
        public AstDatastoreValueAccess SelectMultivectorComponents(IEnumerable<int> idsList)
        {
            if (IsFullMultivector)
            {
                var frame = TypeAsFrameMultivector.ParentFrame;

                var newIdsList = idsList.Where(id => frame.IsValidBasisBladeId(id)).ToArray();

                if (newIdsList.Length == 0) return null;

                return 
                    AssociatedValueAccess
                    .Duplicate()
                    .Append(
                        ValueAccessStepByKeyList<int>.Create(
                            AssociatedValueAccess.ExpressionType,
                            newIdsList
                            )
                        )
                    .ToAstDatastoreValueAccess();
            }

            if (IsPartialMultivector)
            {
                var oldIdsList = AssociatedValueAccess.GetBasisBladeIdsList();

                var newIdsList = idsList.Where(id => oldIdsList.Contains(id)).ToArray();

                if (newIdsList.Length == 0) return null;

                return
                    AssociatedValueAccess
                    .DuplicateExceptLast()
                    .Append(
                        ValueAccessStepByKeyList<int>.Create(
                            AssociatedValueAccess.ExpressionType,
                            newIdsList
                            )
                        )
                    .ToAstDatastoreValueAccess();
            }

            return null;
        }

        /// <summary>
        /// If this value access is a multivector this selects components belonging to the given subspace
        /// into a new value access of the same multivector type
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public AstDatastoreValueAccess SelectMultivectorComponents(int grade)
        {
            if (IsFullMultivector)
            {
                var frame = TypeAsFrameMultivector.ParentFrame;

                if (frame.IsValidGrade(grade) == false) return null;

                var idsList = frame.BasisBladeIDsOfGrade(grade);

                return 
                    AssociatedValueAccess
                    .Duplicate()
                    .Append(
                        ValueAccessStepByKeyList<int>.Create(
                            AssociatedValueAccess.ExpressionType,
                            idsList
                            )
                        )
                    .ToAstDatastoreValueAccess();
            }

            if (IsPartialMultivector)
            {
                var frame = TypeAsFrameMultivector.ParentFrame;

                if (frame.IsValidGrade(grade) == false) return null;

                var idsList = frame.BasisBladeIDsOfGrade(grade);

                var oldIdsList = AssociatedValueAccess.GetBasisBladeIdsList();

                var newIdsList = idsList.Where(id => oldIdsList.Contains(id)).ToArray();

                if (newIdsList.Length == 0) return null;

                return
                    AssociatedValueAccess
                    .DuplicateExceptLast()
                    .Append(
                        ValueAccessStepByKeyList<int>.Create(
                            AssociatedValueAccess.ExpressionType,
                            newIdsList
                            )
                        )
                    .ToAstDatastoreValueAccess();
            }

            return null;
        }

        /// <summary>
        /// If this value access is a multivector this selects components belonging to the given subspace
        /// into a new value access of the same multivector type
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        /// <returns></returns>
        public AstDatastoreValueAccess SelectMultivectorComponents(int grade, IEnumerable<int> indexList)
        {
            if (IsFullMultivector)
            {
                var frame = TypeAsFrameMultivector.ParentFrame;

                if (frame.IsValidGrade(grade) == false) return null;

                var idsList = frame.BasisBladeIDsOfGrade(grade, indexList);

                return
                    AssociatedValueAccess
                    .Duplicate()
                    .Append(
                        ValueAccessStepByKeyList<int>.Create(
                            AssociatedValueAccess.ExpressionType,
                            idsList
                            )
                        )
                    .ToAstDatastoreValueAccess();
            }

            if (IsPartialMultivector)
            {
                var frame = TypeAsFrameMultivector.ParentFrame;

                if (frame.IsValidGrade(grade) == false) return null;

                var idsList = frame.BasisBladeIDsOfGrade(grade, indexList);

                var oldIdsList = AssociatedValueAccess.GetBasisBladeIdsList();

                var newIdsList = idsList.Where(id => oldIdsList.Contains(id)).ToArray();

                if (newIdsList.Length == 0) return null;

                return
                    AssociatedValueAccess
                    .DuplicateExceptLast()
                    .Append(
                        ValueAccessStepByKeyList<int>.Create(
                            AssociatedValueAccess.ExpressionType,
                            newIdsList
                            )
                        )
                    .ToAstDatastoreValueAccess();
            }

            return null;
        }


        public override string ToString()
        {
            return AssociatedValueAccess.GetName();
        }
    }
}
