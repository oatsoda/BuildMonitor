using BuildMonitor.Core;

namespace BuildMonitor.ADO
{
    public class ADOBuildStoreFactory : IBuildStoreFactory
    {
        public IBuildStore GetBuildStore(IMonitorOptions options)
        {
            return new ADOBuildStore(options);
        }
    }
}
