using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Operator;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.AST;

namespace GMac.Engine.AST.Expressions
{
    public sealed class AstMultivectorConstructor : AstExpression
    {
        internal BasicPolyadic AssociatedPolyadicExpression { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedPolyadicExpression;

        internal ILanguageOperator AssociatedOperator => AssociatedPolyadicExpression.Operator;

        internal GMacFrameMultivectorConstructor AssociatedMultivectorConstructor => AssociatedPolyadicExpression.Operator as GMacFrameMultivectorConstructor;

        internal GMacFrameMultivector AssociatedMultivectorType => AssociatedMultivectorConstructor.MultivectorType;

        internal ILanguageExpressionAtomic AssociatedDefaultValueSource => AssociatedMultivectorConstructor.DefaultValueSource;


        public override bool IsValidMultivectorConstructor => AssociatedPolyadicExpression != null &&
                                                              AssociatedMultivectorConstructor != null;

        /// <summary>
        /// The multivector type constructed by this expression
        /// </summary>
        public AstFrameMultivector ConstructedMultivector => IsValidMultivectorConstructor ? new AstFrameMultivector(AssociatedMultivectorType) : null;

        /// <summary>
        /// True if this constructor has a default expression
        /// </summary>
        public bool HasDefaultExpression => IsValidMultivectorConstructor && AssociatedDefaultValueSource != null;

        /// <summary>
        /// The default expression of this constructor
        /// </summary>
        public AstExpression DefaultExpression => IsValidMultivectorConstructor ? AssociatedDefaultValueSource.ToAstExpression() : null;

        /// <summary>
        /// The basis blade IDs used in this constructor
        /// </summary>
        public IEnumerable<ulong> UsedBasisBladesIds
        {
            get
            {
                var assignments =
                    AssociatedPolyadicExpression.Operands as OperandsByIndex;

                if (ReferenceEquals(assignments, null)) return null;

                return
                    assignments.OperandsDictionary.Select(item => item.Key);
            }
        }

        /// <summary>
        /// The basis blades used in this constructor
        /// </summary>
        public IEnumerable<AstFrameBasisBlade> UsedBasisBlades
        {
            get
            {
                var assignments =
                    AssociatedPolyadicExpression.Operands as OperandsByIndex;

                if (ReferenceEquals(assignments, null)) return null;

                return
                    assignments.OperandsDictionary.Select(
                        item => new AstFrameBasisBlade(AssociatedMultivectorType.ParentFrame, item.Key)
                        );
            }
        }

        /// <summary>
        /// The basis blade IDs and their assigned values of this constructor
        /// </summary>
        public IEnumerable<KeyValuePair<AstFrameBasisBlade, AstExpression>> Assignments
        {
            get
            {
                var assignments = 
                    AssociatedPolyadicExpression.Operands as OperandsByIndex;

                if (ReferenceEquals(assignments, null)) return null;

                var frame = AssociatedMultivectorType.ParentFrame;

                return 
                    assignments.OperandsDictionary.Select(
                        item => new KeyValuePair<AstFrameBasisBlade, AstExpression>(
                            new AstFrameBasisBlade(frame, item.Key),
                            item.Value.ToAstExpression()
                            )
                        );
            }
        }


        internal AstMultivectorConstructor(BasicPolyadic expr)
        {
            AssociatedPolyadicExpression = expr;
        }
    }
}
