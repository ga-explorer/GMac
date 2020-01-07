namespace GMac.GMacCompiler.Semantic.ASTConstants
{
    /// <summary>
    /// GMac built-in type names
    /// </summary>
    public static class TypeNames
    {
        public static readonly string Scalar = "scalar";
        public static readonly string Integer = "int";
        public static readonly string Bool = "bool";
        public static readonly string Unit = "unit";

        public static string[] BuiltinTypeNames { get; } = { Unit, Scalar, Integer, Bool };
    }
}
