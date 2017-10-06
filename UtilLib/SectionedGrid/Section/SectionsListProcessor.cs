using System;
using System.Collections.Generic;
using System.Linq;
using UtilLib.SectionedGrid.Filter;

namespace UtilLib.SectionedGrid.Section
{
    public static class SectionsListProcessor
    {
        public static IEnumerable<SectionBase> DistinctSections(this IEnumerable<SectionBase> sections)
        {
            var skipIdsList = new Dictionary<int, int>();

            var sectionsList = 
                sections
                .Where(section => skipIdsList.ContainsKey(section.SectionId) == false);

            foreach (var section in sectionsList)
            {
                skipIdsList.Add(section.SectionId, section.SectionId);

                yield return section;
            }
        }

        public static SectionsList ToSectionsList(this IEnumerable<SectionBase> sections)
        {
            return new SectionsList(sections);
        }

        public static IEnumerable<SectionBase> Filter(this IEnumerable<SectionBase> sections, ISectionFilter filter)
        {
            return sections.Where(filter.SectionAccepted);
        }

        public static IEnumerable<SectionBase> Filter(this IEnumerable<SectionBase> sections, Func<SectionBase, bool> predicate)
        {
            return sections.Where(predicate);
        }

        public static IEnumerable<SectionBase> Filter(this IEnumerable<SectionBase> sections, string sectionRole)
        {
            return sections.Where(x => x.SectionRole == sectionRole);
        }

    }
}
