using System.Net;

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

        string TfsApiUrl { get; }
        string ProjectName { get; }

        bool UseCredentials { get; }
        NetworkCredential Credential { get; }
        string Username { get; }
        string UsernameEntropy { get; }
        string Password { get; }
        string PasswordEntropy { get; }
        bool ValidOptions { get; }
    }
}