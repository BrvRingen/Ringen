using System.Configuration;

namespace Ringen.Shared.ConfigurationElemente
{
    public sealed class CredentialConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("benutzername", IsRequired = true)]
        public string Benutzername
        {
            get { return (string)this["benutzername"]; }
        }

        [ConfigurationProperty("enryptedPasswort", IsRequired = true)]
        public string EnryptedPasswort
        {
            get { return (string)this["enryptedPasswort"]; }
        }
    }
}
