using System.Collections.Generic;

namespace UtilLib.SectionedGrid.Block
{
    public abstract class GridBlock
    {
        public abstract int SimpleBlocksCount { get; }

        public abstract IEnumerable<GridBlockSimple> SimpleBlocks { get; }

        public abstract GridBlockSimple this[int i] { get; }
    }
}
