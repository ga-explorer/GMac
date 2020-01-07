using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComposerLib.RectGridGraph.Structures
{
    public class RectangularGrid
    {
        private readonly int[] _cellOffsetsArray;
 

        public int[] GridDimensions { get; }

        public int GridDimensionsCount
            => GridDimensions.Length;

        public int GridCellsCount { get; }


        public RectangularGrid(params int[] gridDimensions)
        {
            GridDimensions = gridDimensions;

            _cellOffsetsArray = new int[gridDimensions.Length];

            var n = GridDimensions[0];
            for (var i = 1; i < _cellOffsetsArray.Length; i++)
            {
                _cellOffsetsArray[i] = n;
            }
        }


        public int GetCellIndex(params int[] cellPosition)
        {
            return 0;
        }

        public int[] GetCellPosition(int cellIndex)
        {
            return new[] {0};
        }
    }

    public class RggGraph
    {
        private readonly int[] _cellOffsetsArray;


        public RectangularGrid Grid { get; }


        public RggGraph(RectangularGrid grid)
        {
            Grid = grid ?? throw new ArgumentNullException(nameof(grid));
        }
    }
}
