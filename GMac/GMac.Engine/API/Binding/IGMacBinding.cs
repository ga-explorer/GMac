namespace GMac.Engine.API.Binding
{
    /// <summary>
    /// This is the base interface for all GMac tree binding patterns
    /// </summary>
    public interface IGMacBinding
    {
        /// <summary>
        /// True if this pattern or any of its sub-patterns have one or more constants
        /// </summary>
        bool HasConstantComponent { get; }

        /// <summary>
        /// True if this pattern or any of its sub-patterns have one or more variables
        /// </summary>
        bool HasVariableComponent { get; }

        /// <summary>
        /// Create an exact copy of this pattern and replace any constant scalar components by variable components
        /// </summary>
        /// <returns></returns>
        IGMacBinding ToConstantsFreePattern();
    }
}