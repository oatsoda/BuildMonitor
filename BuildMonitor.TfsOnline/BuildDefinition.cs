using BuildMonitor.Core;

namespace BuildMonitor.Tfs
{
    public class BuildDefinition : IBuildDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}