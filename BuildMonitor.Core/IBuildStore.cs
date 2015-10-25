using System.Collections.Generic;

namespace BuildMonitor.Core
{
    public interface IBuildStore
    {
        IEnumerable<string> GetProjects();
        IEnumerable<IBuildDefinition> GetDefinitions(string projectName);
        IBuildStatus GetLatestBuild(string projectName, IBuildDefinition definition);
    }
}