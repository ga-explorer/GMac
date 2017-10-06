using UtilLib.SectionedGrid.Section;

namespace UtilLib.SectionedGrid
{
    public sealed class SectionedGrid
    {
        public SectionRoot RootRowPart { get; private set; }

        public SectionRoot RootColumnPart { get; private set; }


        public SectionedGrid()
        {
            RootRowPart = SectionRoot.CreateRow(this, "RootRow");
            RootColumnPart = SectionRoot.CreateColumn(this, "RootCol");
        }


        public void Clear()
        {
            RootRowPart = SectionRoot.CreateRow(this, "RootRow");
            RootColumnPart = SectionRoot.CreateColumn(this, "RootColumn");
        }

        public void ComputeRowColumnIndexRange(int firstRowIndex = 0, int firstColumnIndex = 0)
        {
            RootRowPart.ComputeIndexRange(firstRowIndex);
            RootColumnPart.ComputeIndexRange(firstColumnIndex);
        }

        //public GridBlock SelectBlockByName()
        //{
        //}

        //public GridBlock SelectBlockByRole()
        //{
        //}

    }
}
