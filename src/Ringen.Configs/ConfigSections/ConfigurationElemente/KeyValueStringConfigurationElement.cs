using System.Configuration;

namespace Ringen.Configs.ConfigSections.ConfigurationElemente
{
    public sealed class KeyValueStringConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("key")]
        public string Key
        {
            get { return (string)this["key"]; }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)this["value"]; }
        }
    }
}
