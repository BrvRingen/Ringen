using System.Configuration;

namespace Ringen.Configs.ConfigSections.ConfigurationElemente
{
    public sealed class ValueStringConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)this["value"]; }
        }
    }
}
