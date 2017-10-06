namespace TextComposerLib.Text.Parametric
{
    internal sealed class PcLineSegment
    {
        internal string SegmentText { get; }

        internal PcParameter SegmentParameter { get; }

        internal bool IsFixed => SegmentParameter == null;

        internal bool IsParametric => SegmentParameter != null;


        internal PcLineSegment(string segmentText)
        {
            SegmentText = segmentText;

            SegmentParameter = null;
        }

        internal PcLineSegment(PcParameter segmentParameter)
        {
            SegmentText = segmentParameter.ParameterPlaceholder;

            SegmentParameter = segmentParameter;
        }


        public override string ToString()
        {
            return SegmentText;
        }
    }
}
