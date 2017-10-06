namespace UtilLib.DataStructures.Table
{
    public sealed class ContentsCell<TCellContents>
    {
        public TCellContents Contents { get; set; }


        public ContentsCell()
        {
            Contents = default(TCellContents);
        }

        public ContentsCell(TCellContents contents)
        {
            Contents = contents;
        }
    }
}
