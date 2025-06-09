namespace BuildMonitor.Core.ADO
{
    public class ADOBuildStoreFactory : IBuildStoreFactory
    {
        public IBuildStore GetBuildStore(IMonitorOptions options, bool forValidatingOnly)
        {
            return new ADOBuildStore(options, forValidatingOnly);
        }
    }
}
