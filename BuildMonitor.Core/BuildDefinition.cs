namespace BuildMonitor.Core
{
    public class BuildDefinition
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public bool IsVNext { get; set; }
    }
}