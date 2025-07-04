namespace BuildMonitor.Core
{
    public interface IMonitorOptions
    {
        bool IncludeRunningBuilds { get; }
        int IntervalSeconds { get; }

        bool RefreshDefintions { get; }
        int RefreshDefinitionIntervalSeconds { get; }

        bool HideStaleDefinitions { get; }
        int StaleDefinitionDays { get; }

        bool OrderByMostRecent { get; }

        string AzureDevOpsOrganisation { get; }
        string ProjectName { get; }
        int[]? SpecificDefinitionIds { get; }

        string? PersonalAccessTokenCipher { get; }
        string? PersonalAccessTokenEntropy { get; }

        string PersonalAccessTokenPlainText { get; }

        bool ValidOptions { get; }
    }
}