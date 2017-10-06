using System.Collections.Generic;

namespace UtilLib.DataStructures.Table
{
    //TODO: Complete this on the abstract model of TextComposerLib.GraphViz.Dot.Value.HTML.Table
    public class ContentsTable<TTableContents, TRowContents, TCellContents> : List<ContentsRow<TRowContents, TCellContents>>
    {
        public TTableContents Contents { get; set; }
    }

    public class ContentsTable<TTableContents, TCellContents> : List<ContentsRow<TCellContents>>
    {
        public TTableContents Contents { get; set; }
    }
}
