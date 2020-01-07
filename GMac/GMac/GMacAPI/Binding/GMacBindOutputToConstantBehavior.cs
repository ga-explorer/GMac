﻿namespace GMac.GMacAPI.Binding
{
    /// <summary>
    /// Determine the action that should be taken when trying to bind a macro output parameter to a
    /// constant expression.
    /// </summary>
    public enum GMacBindOutputToConstantBehavior
    {
        /// <summary>
        /// Binding an output parameter to a constant raises an error
        /// </summary>
        Prevent = 0,

        /// <summary>
        /// Binding an output parameter to a constant is ignored. No binding occures
        /// </summary>
        Ignore = 1,

        /// <summary>
        /// Binding an output parameter to a constant results in binding the output to a variable instead
        /// </summary>
        BindToVariable = 3
    }
}