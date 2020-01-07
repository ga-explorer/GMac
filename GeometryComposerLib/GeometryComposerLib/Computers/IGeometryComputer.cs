using DataStructuresLib.Statistics;

namespace GeometryComposerLib.Computers
{
    public interface IGeometryComputer
    {
        EventSummaryCollection EventCounters { get; }
    }
}