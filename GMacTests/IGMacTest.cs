namespace GMacTests
{
    public interface IGMacTest
    {
        string Title { get; }

        string Execute();
    }
}
