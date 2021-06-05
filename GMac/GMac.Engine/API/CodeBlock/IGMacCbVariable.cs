namespace GMac.Engine.API.CodeBlock
{
    public interface IGMacCbVariable
    {
        /// <summary>
        /// The unique ID of the low-level item produced by the low-level generator
        /// </summary>
        int LowLevelId { get; }

        /// <summary>
        /// The name of the low-level item produced by the low-level generator
        /// </summary>
        string LowLevelName { get; }

        /// <summary>
        /// For input, output, and non-resused temp variables, this is the same
        /// as LowLevelName. For resused temp variables this is equal to "temp" +
        /// NameIndex
        /// </summary>
        string MidLevelName { get; }

        /// <summary>
        /// The final name given to this variable in the generated code of the target language
        /// </summary>
        string TargetVariableName { get; set; }

        /// <summary>
        /// The max number of computation steps needed to compute this variable.
        /// For input variables and constant computed variables this is always zero
        /// </summary>
        int MaxComputationLevel { get; }

        /// <summary>
        /// True if this is an input variable
        /// </summary>
        bool IsInput { get; }

        /// <summary>
        /// True if this is an output variable
        /// </summary>
        bool IsOutput { get; }

        /// <summary>
        /// True if this is an input or output variable
        /// </summary>
        bool IsParameter { get; }

        /// <summary>
        /// True if this is an input or temp variable
        /// </summary>
        bool IsRhsVariable { get; }

        /// <summary>
        /// True if this variable does not depend on any other variables; all input variables are independent
        /// while only constant computed variables are independet
        /// </summary>
        bool IsIndependent { get; }

        /// <summary>
        /// True if this is a computed variable
        /// </summary>
        bool IsComputed { get; }

        /// <summary>
        /// True if this is a temporary variable
        /// </summary>
        bool IsTemp { get; }

        /// <summary>
        /// True if this is a temporary variable resulting from common sub-expression factoring
        /// </summary>
        bool IsTempSubExpression { get; }

        /// <summary>
        /// True if this is a temporary variable not resulting from common sub-expression factoring
        /// </summary>
        bool IsTempNonSubExpression { get; }
    }
}