using System.Net;

namespace BuildMonitor.Core
{
    public interface IMonitorOptions
    {
        string TfsApiUrl { get; }
        string ProjectName { get; }
        bool UseCredentials { get; }
        NetworkCredential Credential { get; }
        int Interval { get; }
        bool RefreshDefintions { get; }
        int RefreshDefinitionInterval { get; }
    }
}