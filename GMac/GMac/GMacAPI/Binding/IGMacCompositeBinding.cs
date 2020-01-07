namespace GMac.GMacAPI.Binding
{
    /// <summary>
    /// This is the base interface for composite tree binding patterns (macro, multivector, and structure)
    /// </summary>
    public interface IGMacCompositeBinding : IGMacBinding
    {
        /// <summary>
        /// Create a copy of this pattern containing only its constant scalar components and their parents
        /// </summary>
        /// <returns></returns>
        IGMacCompositeBinding PickConstantComponents();

        /// <summary>
        /// Create a copy of this pattern containing only scalar variable components and their parents
        /// </summary>
        /// <returns></returns>
        IGMacCompositeBinding PickVariableComponents();

        /// <summary>
        /// Create a copy of this pattern containing only constant scalar components and their parents
        /// and bind them in the new pattern as variables
        /// </summary>
        /// <returns></returns>
        IGMacCompositeBinding PickConstantComponentsAsVariables();
    }
}