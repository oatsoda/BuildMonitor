using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildMonitor.Core
{
    public interface IBuildStore
    {
        Task<IEnumerable<string>> GetProjects();
        Task<IEnumerable<BuildDefinition>> GetDefinitions(string projectName);
        Task<BuildStatus> GetLatestBuild(string projectName, BuildDefinition definition);
    }
}