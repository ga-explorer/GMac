namespace GMac.Benchmarks.Samples.Computations
{
    public interface IGMacSample
    {
        string Title { get; }

        string Description { get; }

        string Execute();
    }
}
