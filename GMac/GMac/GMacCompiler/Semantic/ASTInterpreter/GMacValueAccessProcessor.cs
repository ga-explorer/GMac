using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.DSLInterpreter;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Symbol;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter
{
    internal sealed class GMacValueAccessProcessor : LanguageValueAccessPrecessor
    {
        protected override ILanguageValue ReadPartialValue(ILanguageValue sourceValue, ValueAccessStep valueAccessStep)
        {
            if (sourceValue.ExpressionType is GMacStructure)
            {
                var structureValue = (ValueStructureSparse)sourceValue;

                return structureValue[((ValueAccessStepByKey<string>)valueAccessStep).AccessKey];
            }

            if (!(sourceValue.ExpressionType is GMacFrameMultivector))
                throw new InvalidOperationException("Invalid source value type");

            var mvValue = (GMacValueMultivector)sourceValue;

            var stepByKey = valueAccessStep as ValueAccessStepByKey<int>;

            if (stepByKey != null)
                return mvValue[stepByKey.AccessKey];

            var stepByKeyList = valueAccessStep as ValueAccessStepByKeyList<int>;

            if (stepByKeyList != null)
                return mvValue[stepByKeyList.AccessKeyList];

            throw new InvalidOperationException("Invalid access step for a multivector");
        }

        protected override ILanguageValue WritePartialValue(ILanguageValue sourceValue, ValueAccessStep valueAccessStep, ILanguageValue value)
        {
            if (sourceValue.ExpressionType is GMacStructure)
            {
                var structureValue = (ValueStructureSparse)sourceValue;

                structureValue[((ValueAccessStepByKey<string>)valueAccessStep).AccessKey] = value;

                return value;
            }

            if (!(sourceValue.ExpressionType is GMacFrameMultivector))
                throw new InvalidOperationException("Invalid source value type");

            var mvValue = (GMacValueMultivector)sourceValue;

            var stepByKey = valueAccessStep as ValueAccessStepByKey<int>;

            if (stepByKey != null)
                mvValue[stepByKey.AccessKey] = (ValuePrimitive<MathematicaScalar>)value;

            else
            {
                var stepByKeyList = valueAccessStep as ValueAccessStepByKeyList<int>;

                if (stepByKeyList == null)
                    throw new InvalidOperationException("Invalid access step for a multivector");
                
                mvValue[stepByKeyList.AccessKeyList] = (GMacValueMultivector) value;
            }

            return value;
        }


        public override IEnumerable<SymbolLValue> GetLValues(ILanguageOperator langOperator)
        {
            var structureConstructor = langOperator as GMacStructureConstructor;

            if (structureConstructor != null)
            {
                var structureCons = structureConstructor;

                if (structureCons.HasDefaultValueSource)
                    return GetLValues(structureCons.DefaultValueSource);
            }

            else
            {
                var multivectorConstructor = langOperator as GMacFrameMultivectorConstructor;

                if (multivectorConstructor == null) 
                    return Enumerable.Empty<SymbolLValue>();

                var mvTypeCons = multivectorConstructor;

                if (mvTypeCons.HasDefaultValueSource)
                    return GetLValues(mvTypeCons.DefaultValueSource);
            }

            return Enumerable.Empty<SymbolLValue>();
        }

        public override ILanguageOperator ReplaceLValueByExpression(ILanguageOperator oldLangOperator, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
        {
            var structureConstructor = oldLangOperator as GMacStructureConstructor;

            if (structureConstructor != null)
            {
                var structureCons = structureConstructor;

                if (structureCons.HasDefaultValueSource)
                    structureCons.SetDefaultValueSource(
                        ReplaceLValueByExpression(structureCons.DefaultValueSource, oldLvalue, newExpr)
                        );

                return oldLangOperator;
            }

            var multivectorConstructor = oldLangOperator as GMacFrameMultivectorConstructor;

            if (multivectorConstructor == null) 
                return oldLangOperator;

            var mvTypeCons = multivectorConstructor;

            if (mvTypeCons.HasDefaultValueSource)
                mvTypeCons.SetDefaultValueSource(
                    ReplaceLValueByExpression(mvTypeCons.DefaultValueSource, oldLvalue, newExpr)
                    );

            return oldLangOperator;
        }
    }
}
