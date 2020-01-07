using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Operator;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstStructureConstructor : AstExpression 
    {
        internal BasicPolyadic AssociatedPolyadicExpression { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedPolyadicExpression;

        internal ILanguageOperator AssociatedOperator => AssociatedPolyadicExpression.Operator;

        internal GMacStructureConstructor AssociatedStructureConstructor => AssociatedPolyadicExpression.Operator as GMacStructureConstructor;

        internal GMacStructure AssociatedStructuType => AssociatedStructureConstructor.Structure;

        internal ILanguageExpressionAtomic AssociatedDefaultValueSource => AssociatedStructureConstructor.DefaultValueSource;


        public override bool IsValidStructureConstructor => AssociatedPolyadicExpression != null &&
                                                            AssociatedStructureConstructor != null;

        /// <summary>
        /// The structure type constructed by this expression
        /// </summary>
        public AstStructure ConstructedStructure => IsValidStructureConstructor ? new AstStructure(AssociatedStructuType) : null;

        /// <summary>
        /// True if this constructor has a default expression
        /// </summary>
        public bool HasDefaultExpression => IsValidStructureConstructor && AssociatedDefaultValueSource != null;

        /// <summary>
        /// The default expression of this constructor
        /// </summary>
        public AstExpression DefaultExpression => IsValidStructureConstructor ? AssociatedDefaultValueSource.ToAstExpression() : null;

        /// <summary>
        /// The structure member datastore value access used in this constructor
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedDataMembers
        {
            get
            {
                var assignments =
                    AssociatedPolyadicExpression.Operands as OperandsByValueAccess;

                if (ReferenceEquals(assignments, null)) return null;

                return
                    assignments.AssignmentsList.Select(
                        item => new AstDatastoreValueAccess(item.LhsValueAccess)
                        );
            }
        }

        /// <summary>
        /// The member assignments used in this constructor
        /// </summary>
        public IEnumerable<KeyValuePair<AstDatastoreValueAccess, AstExpression>> Assignments
        {
            get
            {
                var assignments =
                    AssociatedPolyadicExpression.Operands as OperandsByValueAccess;

                if (ReferenceEquals(assignments, null)) return null;

                return
                    assignments.AssignmentsList.Select(
                        item => new KeyValuePair<AstDatastoreValueAccess, AstExpression>(
                            new AstDatastoreValueAccess(item.LhsValueAccess),
                            item.RhsExpression.ToAstExpression()
                            )
                        );
            }
        }


        internal AstStructureConstructor(BasicPolyadic expr)
        {
            AssociatedPolyadicExpression = expr;
        }
    }
}
