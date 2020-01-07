using UtilLib.SectionedGrid.Section;

namespace UtilLib.SectionedGrid.Filter
{
    public sealed class FilterBySectionLevel : ISectionFilter
    {
        public FilterBySectionLevel Create(int minLevel, int maxLevel)
        {
            return new FilterBySectionLevel(minLevel, maxLevel);
        }


        public int MinLevel { get; private set; }

        public int MaxLevel { get; private set; }


        private FilterBySectionLevel(int minLevel, int maxLevel)
        {
            MinLevel = minLevel;
            MaxLevel = maxLevel;
        }


        public bool SectionAccepted(SectionBase section)
        {
            return section.SectionLevel >= MinLevel && section.SectionLevel <= MaxLevel;
        }
    }
}
