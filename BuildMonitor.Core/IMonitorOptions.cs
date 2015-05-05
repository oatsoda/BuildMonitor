using System.Net;

namespace BuildMonitor.Core
{
    public interface IMonitorOptions
    {
        string TfsApiUrl { get; }
        string ProjectName { get; }
        int IntervalSeconds { get; }
        bool RefreshDefintions { get; }
        int RefreshDefinitionIntervalSeconds { get; }
        bool UseCredentials { get; }
        NetworkCredential Credential { get; }
        string Username { get; }
        string UsernameEntropy { get; }
        string Password { get; }
        string PasswordEntropy { get; }
    }
}