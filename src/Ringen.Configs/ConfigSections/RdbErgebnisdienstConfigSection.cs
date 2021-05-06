using Ringen.Configs.ConfigSections.ConfigurationElemente;
using System.Configuration;

namespace Ringen.Configs.ConfigSections
{
    public sealed class RdbErgebnisdienstConfigSection : ConfigurationSection
    {
        public static RdbErgebnisdienstConfigSection Instance = (RdbErgebnisdienstConfigSection)ConfigurationManager.GetSection($"{GlobaleVariablen.KonfigSectionName}/rdbErgebnisdienst");
        
        [ConfigurationProperty("credentials", IsRequired = false)]
        public CredentialConfigurationElement Credentials
        {
            get { return (CredentialConfigurationElement)this["credentials"]; }
        }

        [ConfigurationProperty("api", IsRequired = true)]
        public RdbServerConfigurationElement Api
        {
            get { return (RdbServerConfigurationElement)this["api"]; }
        }
    }
}
