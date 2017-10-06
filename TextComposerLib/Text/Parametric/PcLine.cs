using System.Collections.Generic;

namespace TextComposerLib.Text.Parametric
{
    internal sealed class PcLine
    {
        internal string LineText { get; set; }

        internal List<PcLineSegment> LineSegments { get; }


        internal PcLine(string lineText)
        {
            LineText = lineText;

            LineSegments = new List<PcLineSegment>();
        }


        internal int AddFixedSegment(int segmentTextStart, int segmentTextEnd)
        {
            if (segmentTextEnd < segmentTextStart)
                return -1;

            LineSegments.Add(
                new PcLineSegment(
                    LineText.Substring(
                        segmentTextStart, 
                        segmentTextEnd - segmentTextStart + 1
                        )
                    )
                );

            //Return the location of the start of the next segment
            return segmentTextEnd + 1;
        }

        internal int AddParametricSegment(int segmentTextStart, PcParameter parameter)
        {
            LineSegments.Add(
                new PcLineSegment(parameter)
                );

            //Return the location of the start of the next segment
            return segmentTextStart + parameter.ParameterPlaceholderLength;
        }


        public override string ToString()
        {
            return LineText;
        }
    }
}
