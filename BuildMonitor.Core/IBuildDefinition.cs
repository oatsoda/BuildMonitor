namespace BuildMonitor.Core
{
    public interface IBuildDefinition
    {
        int Id { get; }
        string Name { get; }
        string Url { get; }
        string TriggerType { get; }
        string DropLocation { get; }
    }
}
