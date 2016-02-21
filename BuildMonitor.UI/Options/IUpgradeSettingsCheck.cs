namespace BuildMonitor.UI.Options
{
    public interface IUpgradeSettingsCheck
    {
        bool UpgradeSettings { get; set; }
        void Save();
    }
}
