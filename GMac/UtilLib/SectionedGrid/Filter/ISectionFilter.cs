using UtilLib.SectionedGrid.Section;

namespace UtilLib.SectionedGrid.Filter
{
    public interface ISectionFilter
    {
        bool SectionAccepted(SectionBase section);
    }
}
