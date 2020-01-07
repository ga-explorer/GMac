using CodeComposerLib.Irony.Semantic.Type;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacCompiler.Semantic.AST.Extensions
{
    internal static class LanguageTypeExtensions
    {
        /// <summary>
        /// True for built-in types
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsPrimitiveType(this ILanguageType langType)
        {
            if (ReferenceEquals(langType, null))
                return false;

            return (langType is TypePrimitive);
        }

        /// <summary>
        /// True for builtin integer type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsInteger(this ILanguageType langType)
        {
            if (ReferenceEquals(langType, null))
                return false;

            if (! (langType is TypePrimitive) ) 
                return false;

            return ((TypePrimitive)langType).ObjectName == TypeNames.Integer;
        }

        /// <summary>
        /// True for built-in scalar type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsScalar(this ILanguageType langType)
        {
            if (ReferenceEquals(langType, null))
                return false;

            if (!(langType is TypePrimitive))
                return false;

            return ((TypePrimitive)langType).ObjectName == TypeNames.Scalar;
        }

        /// <summary>
        /// True for built-in integer or scalar type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsNumber(this ILanguageType langType)
        {
            if (ReferenceEquals(langType, null))
                return false;

            if (!(langType is TypePrimitive))
                return false;

            return ((TypePrimitive)langType).ObjectName == TypeNames.Integer || ((TypePrimitive)langType).ObjectName == TypeNames.Scalar;
        }

        /// <summary>
        /// True for built-in boolean type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsBoolean(this ILanguageType langType)
        {
            if (ReferenceEquals(langType, null))
                return false;

            if (!(langType is TypePrimitive))
                return false;

            return ((TypePrimitive)langType).ObjectName == TypeNames.Bool;
        }

        /// <summary>
        /// True for a frame multivector type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsFrameMultivector(this ILanguageType langType)
        {
            if (ReferenceEquals(langType, null))
                return false;

            return (langType is GMacFrameMultivector);
        }

        /// <summary>
        /// If the type is a frame multivector get the parent frame
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static GMacFrame GetFrame(this ILanguageType langType)
        {
            var mvClass = langType as GMacFrameMultivector;

            return mvClass?.ParentFrame;
        }

        /// <summary>
        /// True for a GMac structure type
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static bool IsStructure(this ILanguageType langType)
        {
            if (ReferenceEquals(langType, null))
                return false;

            return (langType is TypeStructure);
        }

        /// <summary>
        /// Retutns true if the two types are multivector types and have the same frame
        /// </summary>
        /// <param name="langType"></param>
        /// <param name="rhsType"></param>
        /// <returns></returns>
        public static bool HasSameFrame(this ILanguageType langType, ILanguageType rhsType)
        {
            return
                langType != null &&
                rhsType != null &&
                langType is GMacFrameMultivector &&
                rhsType is GMacFrameMultivector &&
                ((GMacFrameMultivector)langType).ParentFrame.ObjectId == ((GMacFrameMultivector)rhsType).ParentFrame.ObjectId;
        }

        /// <summary>
        /// Retutns true if the two types are of the same structure type
        /// </summary>
        /// <param name="langType"></param>
        /// <param name="rhsType"></param>
        /// <returns></returns>
        public static bool IsSameStructure(this ILanguageType langType, ILanguageType rhsType)
        {
            return IsStructure(langType) && langType.IsSameType(rhsType);
        }

        /// <summary>
        /// Retutns true if the two types are of the same multivector type
        /// </summary>
        /// <param name="langType"></param>
        /// <param name="rhsType"></param>
        /// <returns></returns>
        public static bool IsSameMultivector(this ILanguageType langType, ILanguageType rhsType)
        {
            return IsFrameMultivector(langType) && langType.IsSameType(rhsType);
        }

        /// <summary>
        /// Returns true if an expression of type rhs_type can be assigned to an expression of type lhs_type
        /// </summary>
        /// <param name="lhsType"></param>
        /// <param name="rhsType"></param>
        /// <returns></returns>
        public static bool CanAssignValue(this ILanguageType lhsType, ILanguageType rhsType)
        {
            return
                (IsBoolean(lhsType) && IsBoolean(rhsType)) ||
                (IsInteger(lhsType) && IsInteger(rhsType)) ||
                (IsScalar(lhsType) && IsNumber(rhsType)) ||
                (IsStructure(lhsType) && lhsType.IsSameType(rhsType)) ||
                (IsFrameMultivector(lhsType) && IsNumber(rhsType)) ||
                (HasSameFrame(lhsType, rhsType));
        }
    }
}
