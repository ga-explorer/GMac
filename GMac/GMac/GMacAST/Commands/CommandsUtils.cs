using CodeComposerLib.Irony.Semantic.Command;

namespace GMac.GMacAST.Commands
{
    public static class CommandsUtils
    {
        internal static AstCommandBlock ToAstCommandBlock(this CommandBlock command)
        {
            return new AstCommandBlock(command);
        }

        internal static AstCommandDeclare ToAstCommandDeclare(this CommandDeclareVariable command)
        {
            return new AstCommandDeclare(command);
        }

        internal static AstCommandLet ToAstCommandLet(this CommandAssign command)
        {
            return new AstCommandLet(command);
        }

        internal static AstCommand ToAstCommand(this LanguageCommand command)
        {
            var s1 = command as CommandBlock;
            if (ReferenceEquals(s1, null) == false) return new AstCommandBlock(s1);

            var s2 = command as CommandDeclareVariable;
            if (ReferenceEquals(s2, null) == false) return new AstCommandDeclare(s2);

            var s3 = command as CommandAssign;
            if (ReferenceEquals(s3, null) == false) return new AstCommandLet(s3);

            return null;
        }
    }
}
