using System.Threading.Tasks;

namespace BuildMonitor.UI.Updater
{
    public interface IAppUpdater
    {
        Task<bool> CheckForUpdates();
    }
}
