using System.Linq;
using GMac.GMacAST;
using TextComposerLib.Text;

namespace GMac.GMacScripting
{
    /// <summary>
    /// This class represents a GMac script to be executed using an internal script interpreter object
    /// </summary>
    public sealed class TestScript : IGMacScript
    {
        /// <summary>
        /// The script interpreter object that executes all script commands
        /// </summary>
        public GMacScriptInterpreter Ipr { get; set; }

        /// <summary>
        /// The root GMacAST for this script
        /// </summary>
        public AstRoot Root => Ipr.Root;

        /// <summary>
        /// Compile and execute one or more GMac commands
        /// </summary>
        /// <param name="commandText">The GMac commands</param>
        private void Execute(string commandText) { Ipr.Execute(commandText); }

        /// <summary>
        /// The processing code to be executed
        /// </summary>
        public void Process()
        {
            Ipr.Reset("common");
            Ipr.OpenScope("common.geometry3d");

            var frame = Ipr.Frame("e3d");

            for (var i = 1; i <= frame.VSpaceDimension; i++)
            {
                Execute($" declare v{i} : Multivector ");

                for (var id = 1UL; id < frame.GaSpaceDimension; id = id << 1)
                    Execute(string.Format("let v{0} = v{0} + Multivector(#E{1}# = '1')", i, id));
                //Ipr.Exec(String.Format("let v{0} = v{0} + Multivector(#E{1}# = 'v{0}c{1}')", i, id));
            }

            //Ipr.Exec("let v = v1 op v2 op v3 op v4 op v5 ");
            Execute(
                Enumerable
                .Range(1, frame.VSpaceDimension)
                .Select(i => "v" + i)
                .Concatenate(" ^ ", "let v = ", "")
                );

            Ipr.Output.Store("v", Ipr.ValueOf("v"));
        }
    }
}
