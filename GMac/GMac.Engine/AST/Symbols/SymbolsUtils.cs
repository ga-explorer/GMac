using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;
using GMac.Engine.Compiler.Semantic.AST;

namespace GMac.Engine.AST.Symbols
{
    public static class SymbolsUtils
    {
        internal static AstNamespace ToAstNamespace(this GMacNamespace nameSpace)
        {
            return new AstNamespace(nameSpace);
        }

        internal static AstFrame ToAstFrame(this GMacFrame frame)
        {
            return new AstFrame(frame);
        }

        internal static AstFrameMultivector ToAstFrameMultivector(this GMacFrameMultivector mvClass)
        {
            return new AstFrameMultivector(mvClass);
        }

        internal static AstFrameBasisVector ToAstFrameBasisVector(this GMacFrameBasisVector basisVector)
        {
            return new AstFrameBasisVector(basisVector);
        }

        internal static AstFrameSubspace ToAstFrameSubspace(this GMacFrameSubspace subspace)
        {
            return new AstFrameSubspace(subspace);
        }

        internal static AstConstant ToAstConstant(this GMacConstant constant)
        {
            return new AstConstant(constant);
        }

        internal static AstStructure ToAstStructure(this GMacStructure structure)
        {
            return new AstStructure(structure);
        }

        internal static AstStructureDataMember ToAstStructureDataMember(this SymbolStructureDataMember member)
        {
            return new AstStructureDataMember(member);
        }

        internal static AstTransform ToAstTransform(this GMacMultivectorTransform transform)
        {
            return new AstTransform(transform);
        }

        internal static AstMacro ToAstMacro(this GMacMacro macro)
        {
            return new AstMacro(macro);
        }

        internal static AstMacroParameter ToAstMacroParameter(this SymbolProcedureParameter parameter)
        {
            return new AstMacroParameter(parameter);
        }

        internal static AstLocalVariable ToAstLocalVariable(this SymbolLocalVariable variable)
        {
            return new AstLocalVariable(variable);
        }

        internal static AstType ToAstType(this ILanguageType langType)
        {
            return new AstType(langType);
        }

        internal static AstSymbol ToAstSymbol(this ILanguageSymbol symbol)
        {
            var s1 = symbol as GMacNamespace;
            if (ReferenceEquals(s1, null) == false) return new AstNamespace(s1);

            var s2 = symbol as GMacFrame;
            if (ReferenceEquals(s2, null) == false) return new AstFrame(s2);

            var s3 = symbol as GMacFrameMultivector;
            if (ReferenceEquals(s3, null) == false) return new AstFrameMultivector(s3);

            var s4 = symbol as GMacFrameBasisVector;
            if (ReferenceEquals(s4, null) == false) return new AstFrameBasisVector(s4);

            var s5 = symbol as GMacFrameSubspace;
            if (ReferenceEquals(s5, null) == false) return new AstFrameSubspace(s5);

            var s6 = symbol as GMacConstant;
            if (ReferenceEquals(s6, null) == false) return new AstConstant(s6);

            var s7 = symbol as GMacStructure;
            if (ReferenceEquals(s7, null) == false) return new AstStructure(s7);

            var s8 = symbol as SymbolStructureDataMember;
            if (ReferenceEquals(s8, null) == false) return new AstStructureDataMember(s8);

            var s9 = symbol as GMacMacro;
            if (ReferenceEquals(s9, null) == false) return new AstMacro(s9);

            var s10 = symbol as SymbolProcedureParameter;
            if (ReferenceEquals(s10, null) == false) return new AstMacroParameter(s10);

            var s11 = symbol as SymbolLocalVariable;
            if (ReferenceEquals(s11, null) == false) return new AstLocalVariable(s11);

            var s12 = symbol as GMacMultivectorTransform;
            if (ReferenceEquals(s12, null) == false) return new AstTransform(s12);

            return null;
        }
    }
}
