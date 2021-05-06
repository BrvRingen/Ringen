using System.Configuration;
using Ringen.Schnittstelle.RDB.ConfigSections.ConfigurationElemente;
using Ringen.Shared;
using Ringen.Shared.ConfigurationElemente;

namespace Ringen.Schnittstelle.RDB.ConfigSections
{
    public sealed class RdbConfigSection : ConfigurationSection
    {
        public static RdbConfigSection Instance = (RdbConfigSection)ConfigurationManager.GetSection($"{GlobaleVariablen.KonfigSectionName}/rdbErgebnisdienst");
        
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
