using System.Configuration;

namespace Ringen.Schnittstelle.Caching.ConfigSections.ConfigurationElemente
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
