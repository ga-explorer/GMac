using System.Collections.Generic;

namespace UtilLib.DataStructures.Table
{
    public sealed class ContentsRow<TRowContents, TCellContents> : List<ContentsCell<TCellContents>>
    {
        public TRowContents Contents { get; set; }


        public ContentsRow<TRowContents, TCellContents> AddCell(TCellContents cellContents)
        {
            Add(new ContentsCell<TCellContents>(cellContents));

            return this;
        }

        public ContentsRow<TRowContents, TCellContents> AddCell(ContentsCell<TCellContents> cell)
        {
            Add(cell);

            return this;
        }

        public ContentsRow<TRowContents, TCellContents> AddCells(params ContentsCell<TCellContents>[] cells)
        {
            AddRange(cells);

            return this;
        }

        public ContentsRow<TRowContents, TCellContents> AddCells(IEnumerable<ContentsCell<TCellContents>> cells)
        {
            AddRange(cells);

            return this;
        }

    }

    public sealed class ContentsRow<TCellContents> : List<ContentsCell<TCellContents>>
    {
        
    }
}
