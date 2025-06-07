namespace BuildMonitor.Core
{
    public class BuildDetail
    {
        public BuildDefinition Definition { get; private set; }
        public BuildStatus Status { get; private set; }

        public BuildDetail(BuildDefinition definition, BuildStatus status)
        {
            Status = status;
            Definition = definition;
        }
    }
}
