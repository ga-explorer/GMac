﻿namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal
{
    public abstract class GaGbtStack1 
        : GaGbtStack, IGaGbtStack1
    {
        /// <summary>
        /// Array containing node ID information in this stack
        /// </summary>
        protected ulong[] IdArray { get; }


        /// <summary>
        /// Top-of-stack node ID
        /// </summary>
        public ulong TosId { get; protected set; }


        /// <summary>
        /// Top-of-stack node child 0 ID
        /// </summary>
        public ulong TosChildId0
            => TosId;

        /// <summary>
        /// Top-of-stack node child 1 ID
        /// </summary>
        public ulong TosChildId1
            => TosId | (1ul << (TosTreeDepth - 1));


        public ulong RootId { get; }


        protected GaGbtStack1(int capacity, int rootTreeDepth, ulong rootId) 
            : base(capacity, rootTreeDepth)
        {
            IdArray = new ulong[Capacity];

            RootId = rootId;
        }


        /// <summary>
        /// Top-of-stack node has child 0
        /// </summary>
        /// <returns></returns>
        public abstract bool TosHasChild0();

        /// <summary>
        /// Top-of-stack node has child 1
        /// </summary>
        /// <returns></returns>
        public abstract bool TosHasChild1();


        /// <summary>
        /// Push data of top-of-stack node's child 0 into stack
        /// </summary>
        public abstract void PushDataOfChild0();

        /// <summary>
        /// Push data of top-of-stack node's child 1 into stack
        /// </summary>
        public abstract void PushDataOfChild1();
    }
}