using BuildMonitor.Core;
using BuildMonitor.UI.Protection;
using System.Configuration;

namespace BuildMonitor.UI.Options
{

    public sealed class MonitorOptions : ApplicationSettingsBase, IMonitorOptions
    {
        // ** General Options
        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool IncludeRunningBuilds
        {
            get { return (bool)this[nameof(IncludeRunningBuilds)]; }
            set { this[nameof(IncludeRunningBuilds)] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("60")]
        public int IntervalSeconds
        {
            get { return (int)this[nameof(IntervalSeconds)]; }
            set { this[nameof(IntervalSeconds)] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool RefreshDefintions
        {
            get { return (bool)this[nameof(RefreshDefintions)]; }
            set { this[nameof(RefreshDefintions)] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("3600")]
        public int RefreshDefinitionIntervalSeconds
        {
            get { return (int)this[nameof(RefreshDefinitionIntervalSeconds)]; }
            set { this[nameof(RefreshDefinitionIntervalSeconds)] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool HideStaleDefinitions
        {
            get { return (bool)this[nameof(HideStaleDefinitions)]; }
            set { this[nameof(HideStaleDefinitions)] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("90")]
        public int StaleDefinitionDays
        {
            get { return (int)this[nameof(StaleDefinitionDays)]; }
            set { this[nameof(StaleDefinitionDays)] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool OrderByMostRecent
        {
            get { return (bool)this[nameof(OrderByMostRecent)]; }
            set { this[nameof(OrderByMostRecent)] = value; }
        }


        // ** ADO Options

        [UserScopedSetting]
        public string AzureDevOpsOrganisation
        {
            get { return (string)this[nameof(AzureDevOpsOrganisation)]; }
            set { this[nameof(AzureDevOpsOrganisation)] = value; }
        }

        [UserScopedSetting]
        public string ProjectName
        {
            get { return (string)this[nameof(ProjectName)]; }
            set { this[nameof(ProjectName)] = value; }
        }

        [UserScopedSetting]
        public int[]? SpecificDefinitionIds
        {
            get { return (int[]?)this[nameof(SpecificDefinitionIds)]; }
            set { this[nameof(SpecificDefinitionIds)] = value; }
        }

        [UserScopedSetting]
        public string? PersonalAccessTokenCipher
        {
            get { return (string?)this[nameof(PersonalAccessTokenCipher)]; }
            set { this[nameof(PersonalAccessTokenCipher)] = value; }
        }

        [UserScopedSetting]
        public string? PersonalAccessTokenEntropy
        {
            get { return (string?)this[nameof(PersonalAccessTokenEntropy)]; }
            set { this[nameof(PersonalAccessTokenEntropy)] = value; }
        }

        // ** Settings Management

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool ValidOptions
        {
            get { return (bool)this[nameof(ValidOptions)]; }
            set { this[nameof(ValidOptions)] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool SettingsUpgradeRequired
        {
            get { return (bool)this[nameof(SettingsUpgradeRequired)]; }
            set { this[nameof(SettingsUpgradeRequired)] = value; }
        }

        // ** Encryption and Protection

        public string PersonalAccessTokenPlainText =>
            ProtectionMethods.Unprotect(PersonalAccessTokenProtected);

        public ProtectedInformation PersonalAccessTokenProtected
        {
            get { return new ProtectedInformation(PersonalAccessTokenCipher!, PersonalAccessTokenEntropy!); }
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

            HideStaleDefinitions = existingOptions.HideStaleDefinitions;
            StaleDefinitionDays = existingOptions.StaleDefinitionDays;

            OrderByMostRecent = existingOptions.OrderByMostRecent;

            AzureDevOpsOrganisation = existingOptions.AzureDevOpsOrganisation;
            ProjectName = existingOptions.ProjectName;

            PersonalAccessTokenCipher = existingOptions.PersonalAccessTokenCipher;
            PersonalAccessTokenEntropy = existingOptions.PersonalAccessTokenEntropy;

            ValidOptions = existingOptions.ValidOptions;
        }
    }
}