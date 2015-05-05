using BuildMonitor.Core;

namespace BuildMonitor.TfsOnline
{
    public class TfsOnlineBuildStoreFactory : IBuildStoreFactory
    {
        public IBuildStore GetBuildStore(IMonitorOptions options)
        {
            return new TfsOnlineBuildStore(options);
        }
    }
}
