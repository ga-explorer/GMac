﻿using Wolfram.NETLink;

namespace SymbolicInterface.Mathematica
{
    /// <summary>
    /// The main interface to Mathematica containing connection, evaluation, and constant expressions objects
    /// </summary>
    public sealed class MathematicaInterface
    {
        /// <summary>
        /// The default Mathematica interface object
        /// </summary>
        public static readonly MathematicaInterface DefaultCas = new MathematicaInterface();

        /// <summary>
        /// The Mathematica connection object of the default Mathematica interface
        /// </summary>
        public static MathematicaConnection DefaultCasConnection => DefaultCas.Connection;

        /// <summary>
        /// The expression evaluator object of the default Mathematica interface
        /// </summary>
        public static MathematicaEvaluator DefaultCasEvaluator => DefaultCas.Evaluator;

        /// <summary>
        /// The constant expressions collection object of the default Mathematica interface
        /// </summary>
        public static MathematicaConstants DefaultCasConstants => DefaultCas.Constants;


        public static MathematicaInterface Create()
        {
            return new MathematicaInterface();
        }


        private MathematicaConstants _constants;

        /// <summary>
        /// The Mathematica connection object
        /// </summary>
        public MathematicaConnection Connection { get; }

        /// <summary>
        /// The expression evaluation object
        /// </summary>
        public MathematicaEvaluator Evaluator { get; }

        /// <summary>
        /// The constant expressions collection object
        /// </summary>
        public MathematicaConstants Constants => _constants ?? (_constants = new MathematicaConstants(this));


        /// <summary>
        /// Evaluates the given expression using the internal evaluator and returns the evaluated expression object
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Expr this[Expr e] => Connection.EvaluateToExpr(e);

        /// <summary>
        /// Evaluates the given text expression using the internal evaluator and return the evaluated expression object
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Expr this[string e] => Connection.EvaluateToExpr(e);


        private MathematicaInterface()
        {
            Connection = new MathematicaConnection(this);

            Evaluator = new MathematicaEvaluator(this);
        }
    }
}
