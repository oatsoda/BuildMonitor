namespace BuildMonitor.Core
{
    public interface IBuildStoreFactory
    {
        IBuildStore GetBuildStore(IMonitorOptions options);
    }
}
