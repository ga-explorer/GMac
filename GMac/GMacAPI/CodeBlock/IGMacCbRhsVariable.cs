using System.Collections.Generic;

namespace GMac.GMacAPI.CodeBlock
{
    /// <summary>
    /// This interface is implemented by input and temp variables that can be used in the RHS of 
    /// computed variables
    /// </summary>
    public interface IGMacCbRhsVariable : IGMacCbVariable
    {
        /// <summary>
        /// A list of computed variables that use this variable in their RHS expression
        /// </summary>
        IEnumerable<GMacCbComputedVariable> UserVariables { get; }

        /// <summary>
        /// True if this is a RHS variable used in a following computation
        /// </summary>
        bool IsUsed { get; }

        /// <summary>
        /// The last computed variable using this rhs variable in its computation
        /// </summary>
        GMacCbComputedVariable LastUsingVariable { get; }

        /// <summary>
        /// The computation order of the last computed variable using this rhs variable in its computation
        /// </summary>
        int LastUsingComputationOrder { get; }
    }
}