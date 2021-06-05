using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Type;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using Wolfram.NETLink;

namespace GMac.Engine.Compiler.Semantic.AST.Extensions
{
    internal static class LanguageValueExtensions
    {
        /// <summary>
        /// Low level processing requires the use of MathematicaScalar primitive values only. This method preforms
        /// the required conversion of primitive values of other forms.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ValuePrimitive<MathematicaScalar> ToScalarValue(this ILanguageValuePrimitive value)
        {
            var primitive1 = value as ValuePrimitive<MathematicaScalar>;

            if (primitive1 != null)
                return primitive1;

            //Convert into equivalent Mathematica scalar value
            var primitive2 = value as ValuePrimitive<int>;

            if (primitive2 != null)
                return ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)value.ExpressionType,
                    MathematicaScalar.Create(GaSymbolicsUtils.Cas, primitive2.Value)
                    );

            //Convert into equivalent Mathematica scalar value
            var primitive3 = value as ValuePrimitive<bool>;

            if (primitive3 != null)
                return ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)value.ExpressionType,
                    MathematicaScalar.Create(GaSymbolicsUtils.Cas, primitive3.Value ? "True" : "False")
                    );

            //if (value is ValuePrimitive<double>)
            //{
            //return ValuePrimitive<MathematicaScalar>.Create(
            //    (TypePrimitive)value.ExpressionType,
            //    MathematicaScalar.Create(SymbolicUtils.CAS, ((ValuePrimitive<double>)value).Value)
            //    );
            //}

            //if (value is ValuePrimitive<float>)
            //{
            //return ValuePrimitive<MathematicaScalar>.Create(
            //    (TypePrimitive)value.ExpressionType,
            //    MathematicaScalar.Create(SymbolicUtils.CAS, ((ValuePrimitive<float>)value).Value)
            //    );
            //}

            //This should never happen
            throw new InvalidOperationException();
        }

        public static Expr ToExpr(this ILanguageValuePrimitive value)
        {
            var primitive1 = value as ValuePrimitive<MathematicaScalar>;

            if (primitive1 != null)
                return primitive1.Value.Expression;

            //Convert into equivalent Mathematica scalar value
            var primitive2 = value as ValuePrimitive<int>;

            if (primitive2 != null)
                return primitive2.Value.ToExpr();

            //Convert into equivalent Mathematica scalar value
            var primitive3 = value as ValuePrimitive<bool>;

            if (primitive3 != null)
                return primitive3.Value.ToExpr();

            //if (value is ValuePrimitive<double>)
            //{
            //return ValuePrimitive<MathematicaScalar>.Create(
            //    (TypePrimitive)value.ExpressionType,
            //    MathematicaScalar.Create(SymbolicUtils.CAS, ((ValuePrimitive<double>)value).Value)
            //    );
            //}

            //if (value is ValuePrimitive<float>)
            //{
            //return ValuePrimitive<MathematicaScalar>.Create(
            //    (TypePrimitive)value.ExpressionType,
            //    MathematicaScalar.Create(SymbolicUtils.CAS, ((ValuePrimitive<float>)value).Value)
            //    );
            //}

            //This should never happen
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Go through every leaf MathematicaScalar in this value and try to simplify its internal 
        /// expression if possible
        /// </summary>
        /// <param name="value"></param>
        public static void Simplify(this ILanguageValue value)
        {
            if (ReferenceEquals(value, null)) return;

            var scalarValue = value as ValuePrimitive<MathematicaScalar>;

            if (ReferenceEquals(scalarValue, null) == false)
            {
                scalarValue.Value.Simplify();
                return;
            }

            var mvValue = value as GMacValueMultivector;

            if (ReferenceEquals(mvValue, null) == false)
            {
                mvValue.SymbolicMultivector.Simplify();
                return;
            }

            var stValue = value as ValueStructureSparse;

            if (ReferenceEquals(stValue, null)) return;

            var removeMembersList = new List<string>(stValue.Count);

            foreach (var memberValue in stValue)
            {
                Simplify(memberValue.Value);

                var memberScalarValue = memberValue.Value as ValuePrimitive<MathematicaScalar>;

                if (ReferenceEquals(memberScalarValue, null) == false)
                {
                    if (memberScalarValue.Value.IsZero()) 
                        removeMembersList.Add(memberValue.Key);

                    continue;
                }

                var memberMvValue = memberValue.Value as GMacValueMultivector;

                if (ReferenceEquals(memberMvValue, null) == false)
                {
                    if (!memberMvValue.SymbolicMultivector.ExprTerms.Any()) 
                        removeMembersList.Add(memberValue.Key);

                    continue;
                }

                var memberStValue = memberValue.Value as ValueStructureSparse;

                if (ReferenceEquals(memberStValue, null)) continue;

                if (memberStValue.Count == 0)
                    removeMembersList.Add(memberValue.Key);
            }

            foreach (var memberName in removeMembersList)
                stValue.Remove(memberName);
        }

    }
}
