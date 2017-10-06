using System;
using System.Collections.Generic;
using UtilLib.SectionedGrid.Section;

namespace UtilLib.SectionedGrid.Block
{
    public sealed class GridBlockSimple : GridBlock
    {
        public static GridBlockSimple Create(SectionBase rowPart, SectionBase colPart)
        {
            return new GridBlockSimple(rowPart, colPart);
        }


        public SectionBase RowPart { get; private set; }

        public SectionBase ColumnPart { get; private set; }


        public int FirstRowIndex { get { return RowPart.FirstIndex; } }

        public int LastRowIndex { get { return RowPart.LastIndex; } }

        public int FirstColumnIndex { get { return ColumnPart.FirstIndex; } }

        public int LastColumnIndex { get { return ColumnPart.LastIndex; } }


        private GridBlockSimple(SectionBase rowPart, SectionBase colPart)
        {
            RowPart = rowPart;
            ColumnPart = colPart;
        }


        public override int SimpleBlocksCount
        {
            get { return 1; }
        }

        public override IEnumerable<GridBlockSimple> SimpleBlocks
        {
            get { yield return this; }
        }

        public override GridBlockSimple this[int i]
        {
            get
            {
                if (i == 0) 
                    return this; 
                
                throw new IndexOutOfRangeException();
            }
        }
    }
}
