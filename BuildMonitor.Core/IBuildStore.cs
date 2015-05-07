using System.Collections.Generic;

namespace BuildMonitor.Core
{
    public interface IBuildStore
    {
        IEnumerable<IBuildDefinition> GetDefinitions(string projectName);
        IBuildStatus GetLatestBuild(IBuildDefinition definition);
    }
}