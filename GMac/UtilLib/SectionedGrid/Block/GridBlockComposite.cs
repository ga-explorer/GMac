using System.Collections.Generic;
using System.Linq;
using UtilLib.SectionedGrid.Section;

namespace UtilLib.SectionedGrid.Block
{
    public sealed class GridBlockComposite : GridBlock
    {
        public static GridBlockComposite Create()
        {
            return new GridBlockComposite();
        }

        public static GridBlockComposite Create(SectionBase rowPart, SectionBase colPart)
        {
            var block = new GridBlockComposite();

            return block.Add(rowPart, colPart);
        }

        public static GridBlockComposite Create(SectionBase rowPart, SectionsList colParts)
        {
            var block = new GridBlockComposite();

            return block.Add(rowPart, colParts);
        }

        public static GridBlockComposite Create(SectionsList rowParts, SectionBase colPart)
        {
            var block = new GridBlockComposite();

            return block.Add(rowParts, colPart);
        }

        public static GridBlockComposite Create(SectionsList rowParts, SectionsList colParts)
        {
            var block = new GridBlockComposite();

            return block.Add(rowParts, colParts);
        }

        public static GridBlockComposite Create(GridBlock block)
        {
            var newBlock = new GridBlockComposite();

            return newBlock.Add(block);
        }


        private readonly List<GridBlockSimple> _simpleBlocksList = new List<GridBlockSimple>();


        private GridBlockComposite()
        {
        }


        public void Clear()
        {
            _simpleBlocksList.Clear();
        }

        public GridBlockComposite Add(SectionBase rowPart, SectionBase colPart)
        {
            var block = GridBlockSimple.Create(rowPart, colPart);

            _simpleBlocksList.Add(block);

            return this;
        }

        public GridBlockComposite Add(SectionBase rowPart, SectionsList colParts)
        {
            var blocksList = 
                colParts
                .Select(colPart => GridBlockSimple.Create(rowPart, colPart));

            _simpleBlocksList.AddRange(blocksList);

            return this;
        }

        public GridBlockComposite Add(SectionsList rowParts, SectionBase colPart)
        {
            var blocksList = 
                rowParts
                .Select(rowPart => GridBlockSimple.Create(rowPart, colPart));

            _simpleBlocksList.AddRange(blocksList);

            return this;
        }

        public GridBlockComposite Add(SectionsList rowParts, SectionsList colParts)
        {
            var blocksList =
                colParts
                .SelectMany(
                    colPart => 
                        rowParts
                        .Select(
                            rowPart => 
                                GridBlockSimple.Create(rowPart, colPart)
                            )
                    );

            _simpleBlocksList.AddRange(blocksList);

            return this;
        }

        public GridBlockComposite Add(GridBlock block)
        {
            _simpleBlocksList.AddRange(block.SimpleBlocks);

            return this;
        }


        public override int SimpleBlocksCount
        {
            get { return _simpleBlocksList.Count; }
        }

        public override IEnumerable<GridBlockSimple> SimpleBlocks
        {
            get { return _simpleBlocksList; }
        }

        public override GridBlockSimple this[int i]
        {
            get { return _simpleBlocksList[i]; }
        }

    }
}
