using BuildMonitor.Core;

namespace BuildMonitor.TfsOnline
{
    public class BuildDefinition : IBuildDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsVNext { get; set; }
    }
}