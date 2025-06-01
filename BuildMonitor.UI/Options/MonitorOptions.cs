using BuildMonitor.Core;
using BuildMonitor.UI.Protection;
using System.Configuration;

namespace BuildMonitor.UI.Options
{

    public sealed class MonitorOptions : ApplicationSettingsBase, IMonitorOptions
    {
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool IncludeRunningBuilds
        {
            get { return (bool)this["IncludeRunningBuilds"]; }
            set { this["IncludeRunningBuilds"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("60")]
        public int IntervalSeconds
        {
            get { return (int)this["IntervalSeconds"]; }
            set { this["IntervalSeconds"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool RefreshDefintions
        {
            get { return (bool)this["RefreshDefintions"]; }
            set { this["RefreshDefintions"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("3600")]
        public int RefreshDefinitionIntervalSeconds
        {
            get { return (int)this["RefreshDefinitionIntervalSeconds"]; }
            set { this["RefreshDefinitionIntervalSeconds"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool HideStaleDefinitions
        {
            get { return (bool)this["HideStaleDefinitions"]; }
            set { this["HideStaleDefinitions"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("30")]
        public int StaleDefinitionDays
        {
            get { return (int)this["StaleDefinitionDays"]; }
            set { this["StaleDefinitionDays"] = value; }
        }

        [UserScopedSetting]
        public string AzureDevOpsOrganisation
        {
            get { return (string)this["AzureDevOpsOrganisation"]; }
            set { this["AzureDevOpsOrganisation"] = value; }
        }

        [UserScopedSetting]
        public string ProjectName
        {
            get { return (string)this["ProjectName"]; }
            set { this["ProjectName"] = value; }
        }

        [UserScopedSetting]
        public string PersonalAccessTokenCipher
        {
            get { return (string)this["PersonalAccessTokenCipher"]; }
            set { this["PersonalAccessTokenCipher"] = value; }
        }

        [UserScopedSetting]
        public string PersonalAccessTokenEntropy
        {
            get { return (string)this["PersonalAccessTokenEntropy"]; }
            set { this["PersonalAccessTokenEntropy"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool ValidOptions
        {
            get { return (bool)this["ValidOptions"]; }
            set { this["ValidOptions"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool SettingsUpgradeRequired
        {
            get { return (bool)this["SettingsUpgradeRequired"]; }
            set { this["SettingsUpgradeRequired"] = value; }
        }

        public string PersonalAccessTokenPlainText =>
            ProtectionMethods.Unprotect(PersonalAccessTokenProtected);

        public ProtectedInformation PersonalAccessTokenProtected
        {
            get { return new ProtectedInformation(PersonalAccessTokenCipher, PersonalAccessTokenEntropy); }
            set
            {
                PersonalAccessTokenCipher = value.DataCipher;
                PersonalAccessTokenEntropy = value.DataEntropy;
            }
        }

        public MonitorOptions()
        {
            if (!SettingsUpgradeRequired)
                return;

            Upgrade();
            SettingsUpgradeRequired = false;
            Save();
        }

        public MonitorOptions(IMonitorOptions existingOptions)
        {
            IncludeRunningBuilds = existingOptions.IncludeRunningBuilds;
            IntervalSeconds = existingOptions.IntervalSeconds;
            RefreshDefintions = existingOptions.RefreshDefintions;
            RefreshDefinitionIntervalSeconds = existingOptions.RefreshDefinitionIntervalSeconds;

            AzureDevOpsOrganisation = existingOptions.AzureDevOpsOrganisation;
            ProjectName = existingOptions.ProjectName;

            PersonalAccessTokenCipher = existingOptions.PersonalAccessTokenCipher;
            PersonalAccessTokenEntropy = existingOptions.PersonalAccessTokenEntropy;
            ValidOptions = existingOptions.ValidOptions;
        }
    }
}