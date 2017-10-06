namespace UtilLib.Pools
{
    public interface IPoolObject
    {
        bool EnableInitializeOnCreate { get; }

        bool EnableResetOnRelease { get; }

        bool EnableResetOnAcquire { get; }

        void InitializeOnCreate();

        void ResetOnRelease();

        void ResetOnAcquire();
    }
}
