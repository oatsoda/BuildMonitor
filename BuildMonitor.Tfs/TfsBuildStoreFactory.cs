using BuildMonitor.Core;

namespace BuildMonitor.Tfs
{
    public class TfsBuildStoreFactory : IBuildStoreFactory
    {
        public IBuildStore GetBuildStore(IMonitorOptions options)
        {
            return new TfsBuildStore(options);
        }
    }
}