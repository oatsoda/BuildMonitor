namespace BuildMonitor.Core.ADO
{
    public class ADOBuildStoreFactory : IBuildStoreFactory
    {
        public IBuildStore GetBuildStore(IMonitorOptions options)
        {
            return new ADOBuildStore(options);
        }
    }
}
