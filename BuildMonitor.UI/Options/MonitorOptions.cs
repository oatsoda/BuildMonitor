using System.Configuration;
using System.Net;
using BuildMonitor.Core;
using BuildMonitor.UI.Protection;

namespace BuildMonitor.UI.Options
{

    internal class MonitorOptions : ApplicationSettingsBase, IMonitorOptions
    {
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
        [DefaultSettingValue("30")]
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
            
        }

        public MonitorOptions(IMonitorOptions existingOptions)
        {
            TfsApiUrl = existingOptions.TfsApiUrl;
            ProjectName = existingOptions.ProjectName;
            IntervalSeconds = existingOptions.IntervalSeconds;
            RefreshDefintions = existingOptions.RefreshDefintions;
            RefreshDefinitionIntervalSeconds = existingOptions.RefreshDefinitionIntervalSeconds;
            UseCredentials = existingOptions.UseCredentials;
            //Credential
            Username = existingOptions.Username;
            UsernameEntropy = existingOptions.UsernameEntropy;
            Password = existingOptions.Password;
            PasswordEntropy = existingOptions.PasswordEntropy;
        }
    }
}