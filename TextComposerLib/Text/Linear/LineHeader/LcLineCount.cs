namespace TextComposerLib.Text.Linear.LineHeader
{
    public sealed class LcLineCount : LcLineHeader
    {
        public string FormatString = "D4";

        public LinearComposer ParentComposer { get; }


        public LcLineCount(LinearComposer parentComposer)
        {
            ParentComposer = parentComposer;
        }

        public LcLineCount(LinearComposer parentComposer, string formatString)
        {
            ParentComposer = parentComposer;
            FormatString = formatString;
        }


        public override void Reset()
        {
        }

        public override string GetHeaderText()
        {
            return 
                string.IsNullOrEmpty(FormatString) 
                ? ParentComposer.LinesCount.ToString() 
                : ParentComposer.LinesCount.ToString(FormatString);
        }
    }
}
