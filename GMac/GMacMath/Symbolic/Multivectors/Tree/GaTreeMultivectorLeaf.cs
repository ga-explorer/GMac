﻿using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacMath.Symbolic.Multivectors.Tree
{
    public sealed class GaTreeMultivectorLeaf : IGaTreeMultivectorNode
    {
        public MathematicaScalar Value { get; internal set; }

        public bool IsRoot { get; } = false;

        public bool IsInternal { get; } = false;

        public bool IsLeaf { get; } = true;


        internal GaTreeMultivectorLeaf(MathematicaScalar value)
        {
            Value = value;
        }
    }
}
