using System.Configuration;

namespace Ringen.Configs.ConfigSections.ConfigurationElemente
{
    public class CacheConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("cacheTage")]
        public int CacheTage
        {
            get { return (int)this["cacheTage"]; }
        }
    }
}
