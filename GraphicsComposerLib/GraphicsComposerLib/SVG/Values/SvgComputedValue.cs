namespace GraphicsComposerLib.SVG.Values
{
    public abstract class SvgComputedValue : ISvgValue
    {
        public abstract string ValueText { get; }

        public override string ToString()
        {
            return ValueText;
        }
    }
}
