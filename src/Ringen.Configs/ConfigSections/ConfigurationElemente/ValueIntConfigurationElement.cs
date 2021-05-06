using System.Configuration;

namespace Ringen.Configs.ConfigSections.ConfigurationElemente
{
    public sealed class ValueIntConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public int Value
        {
            get { return (int)this["value"]; }
        }
    }
}
