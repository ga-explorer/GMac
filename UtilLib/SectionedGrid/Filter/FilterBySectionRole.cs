using UtilLib.SectionedGrid.Section;

namespace UtilLib.SectionedGrid.Filter
{
    public sealed class FilterBySectionRole : ISectionFilter
    {
        public static FilterBySectionRole Create(string sectionRole)
        {
            return new FilterBySectionRole(sectionRole);
        }


        public string SectionRole { get; private set; }


        private FilterBySectionRole(string sectionRole)
        {
            SectionRole = sectionRole;
        }


        public bool SectionAccepted(SectionBase section)
        {
            return section.SectionRole == SectionRole;
        }
    }
}
