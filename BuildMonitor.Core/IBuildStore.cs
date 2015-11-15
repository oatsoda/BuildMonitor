using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildMonitor.Core
{
    public interface IBuildStore
    {
        Task<IEnumerable<string>> GetProjects();
        Task<IEnumerable<IBuildDefinition>> GetDefinitions(string projectName);
        Task<IBuildStatus> GetLatestBuild(string projectName, IBuildDefinition definition);
    }
}