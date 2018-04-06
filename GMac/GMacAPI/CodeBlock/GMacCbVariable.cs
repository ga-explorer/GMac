using System;

namespace GMac.GMacAPI.CodeBlock
{
    /// <summary>
    /// This is the base class for all low-level Code Block Variables
    /// </summary>
    public abstract class GMacCbVariable : IGMacCbVariable
    {
        private string _targetVariableName;


        /// <summary>
        /// A flag used during low-level optimization operations
        /// </summary>
        internal bool LowLevelFlag { get; set; }


        /// <summary>
        /// The max number of computation steps needed to compute this variable.
        /// For input variables and constant computed variables this is always zero
        /// </summary>
        public int MaxComputationLevel { get; internal set; }
        
        /// <summary>
        /// The unique ID of the low-level item produced by the low-level generator
        /// </summary>
        public int LowLevelId { get; protected set; }

        /// <summary>
        /// The name of the low-level item produced by the low-level generator
        /// </summary>
        public string LowLevelName { get; }

        /// <summary>
        /// The final name given to this variable in the generated code of the target language
        /// </summary>
        public string TargetVariableName
        {
            get
            {
                if (String.IsNullOrEmpty(_targetVariableName) == false)
                    return _targetVariableName;

                return 
                    IsTemp 
                    ? LowLevelName 
                    : ((IGMacCbParameterVariable) this).ValueAccessName;
            }
            set
            {
                _targetVariableName = value;
            }
        }

        /// <summary>
        /// True if this is an input variable
        /// </summary>
        public bool IsInput => this is GMacCbInputVariable;

        /// <summary>
        /// True if this is a temporary variable
        /// </summary>
        public bool IsTemp => this is GMacCbTempVariable;

        /// <summary>
        /// True if this is an output variable
        /// </summary>
        public bool IsOutput => this is GMacCbOutputVariable;

        /// <summary>
        /// True if this is a computed variable
        /// </summary>
        public bool IsComputed => this is GMacCbComputedVariable;

        /// <summary>
        /// True if this is a macro parameter (input or output variable)
        /// </summary>
        public bool IsParameter => (this is GMacCbTempVariable) == false;

        /// <summary>
        /// True if this is an input or temp variable
        /// </summary>
        public bool IsRhsVariable => (this is GMacCbOutputVariable) == false;

        /// <summary>
        /// True for input variables and constant computed variables
        /// </summary>
        public bool IsIndependent
        {
            get
            {
                if (this is GMacCbInputVariable) return true;

                return ((IGMacCbComputedVariable) this).IsConstant;
            }
        }

        /// <summary>
        /// True if this is a temporary variable resulting from common sub-expression factoring
        /// </summary>
        public bool IsTempSubExpression
        {
            get
            {
                var tempVar = (this) as GMacCbTempVariable;

                return ReferenceEquals(tempVar, null) == false && tempVar.IsFactoredSubExpression;
            }
        }

        /// <summary>
        /// True if this is a temporary variable not resulting from common sub-expression factoring
        /// </summary>
        public bool IsTempNonSubExpression
        {
            get
            {
                var tempVar = (this) as GMacCbTempVariable;

                return ReferenceEquals(tempVar, null) == false && tempVar.IsFactoredSubExpression == false;
            }
        }


        protected GMacCbVariable(string lowLevelName)
        {
            LowLevelName = lowLevelName;
        }


        /// <summary>
        /// Clear all computation dependency data stored in this variable
        /// </summary>
        internal abstract void ClearDependencyData();
    }
}
