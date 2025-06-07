namespace BuildMonitor.Core
{
    public record BuildDetail(BuildDefinition Definition, BuildStatus? Status);
}
