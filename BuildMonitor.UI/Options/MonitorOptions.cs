using System.Configuration;
using System.Net;
using BuildMonitor.Core;
using BuildMonitor.UI.Properties;
using BuildMonitor.UI.Protection;

namespace BuildMonitor.UI.Options
{

    internal sealed class MonitorOptions : ApplicationSettingsBase, IMonitorOptions
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
        [DefaultSettingValue("14")]
        public int StaleDefinitionDays
        {
            get { return (int)this["StaleDefinitionDays"]; }
            set { this["StaleDefinitionDays"] = value; }
        }

        [UserScopedSetting]
        public string TfsApiUrl
        {
            get { return (string)this["TfsApiUrl"]; }
            set { this["TfsApiUrl"] = value; }
        }

        [UserScopedSetting]
        public string ProjectName
        {
            get { return (string)this["ProjectName"]; }
            set { this["ProjectName"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool UseCredentials
        {
            get { return (bool)this["UseCredentials"]; }
            set { this["UseCredentials"] = value; }
        }

        [UserScopedSetting]
        public string Username
        {
            get { return (string)this["Username"]; }
            set { this["Username"] = value; }
        }

        [UserScopedSetting]
        public string UsernameEntropy
        {
            get { return (string)this["UsernameEntropy"]; }
            set { this["UsernameEntropy"] = value; }
        }

        [UserScopedSetting]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }

        [UserScopedSetting]
        public string PasswordEntropy
        {
            get { return (string)this["PasswordEntropy"]; }
            set { this["PasswordEntropy"] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool ValidOptions
        {
            get { return (bool)this["ValidOptions"]; }
            set { this["ValidOptions"] = value; }
        }


        public NetworkCredential Credential
        {
            get 
            { 
                return new NetworkCredential(
                    ProtectionMethods.Unprotect(UsernameProtected),
                    ProtectionMethods.Unprotect(PasswordProtected)
                    ); 
            }
        }

        public ProtectedInformation UsernameProtected
        {
            get { return new ProtectedInformation(Username, UsernameEntropy); }
            set 
            {
                Username = value.DataHash;
                UsernameEntropy = value.DataEntropy;
            }
        }

        public ProtectedInformation PasswordProtected
        {
            get { return new ProtectedInformation(Password, PasswordEntropy); }
            set
            {
                Password = value.DataHash;
                PasswordEntropy = value.DataEntropy;
            }
        }

        public MonitorOptions()
        {
            if (!Settings.Default.UpgradeSettings) 
                return;

            Upgrade();
            Settings.Default.UpgradeSettings = false;
        }

        public MonitorOptions(IMonitorOptions existingOptions)
        {
            IncludeRunningBuilds = existingOptions.IncludeRunningBuilds;
            IntervalSeconds = existingOptions.IntervalSeconds;
            RefreshDefintions = existingOptions.RefreshDefintions;
            RefreshDefinitionIntervalSeconds = existingOptions.RefreshDefinitionIntervalSeconds;

            TfsApiUrl = existingOptions.TfsApiUrl;
            ProjectName = existingOptions.ProjectName;

            UseCredentials = existingOptions.UseCredentials;
            //Credential
            Username = existingOptions.Username;
            UsernameEntropy = existingOptions.UsernameEntropy;
            Password = existingOptions.Password;
            PasswordEntropy = existingOptions.PasswordEntropy;
            ValidOptions = existingOptions.ValidOptions;
        }
    }
}