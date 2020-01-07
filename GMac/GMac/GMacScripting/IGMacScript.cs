using GMac.GMacAST;

namespace GMac.GMacScripting
{
    public interface IGMacScript
    {
        /// <summary>
        /// The interpreter for the script
        /// </summary>
        GMacScriptInterpreter Ipr { get; set; }

        /// <summary>
        /// The root of the GMacAST for this script
        /// </summary>
        AstRoot Root { get; }

        /// <summary>
        /// The code to be executed
        /// </summary>
        void Process();
    }
}