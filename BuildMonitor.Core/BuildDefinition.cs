namespace BuildMonitor.Core
{
    public class BuildDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsVNext { get; set; }
    }
}