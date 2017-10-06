namespace TextComposerLib.Text.Tabular.Columns
{
    public enum TextRowAlignment
    {
        Top, Bottom
    }

    internal sealed class TextRowSpecs
    {
        public TextRowAlignment Alignment { get; set; }


        public TextRowSpecs()
        {
            Alignment = TextRowAlignment.Top;
        }
    }
}
