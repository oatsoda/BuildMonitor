using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildMonitor.Core
{
    public interface IBuildStore
    {
        Task<IEnumerable<string>> GetProjects();
        Task<IEnumerable<BuildDefinition>> GetDefinitions();
        Task<BuildStatus> GetLatestBuild(BuildDefinition definition);
    }
}