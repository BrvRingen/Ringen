using Ringen.Shared;
using System.Configuration;
using Ringen.Schnittstelle.Caching.ConfigSections.ConfigurationElemente;

namespace Ringen.Schnittstelle.Caching.ConfigSections
{
    public sealed class ApiCacheConfigSection : ConfigurationSection
    {
        public static ApiCacheConfigSection Instance = (ApiCacheConfigSection)ConfigurationManager.GetSection($"{GlobaleVariablen.KonfigSectionName}/apiCache");

        [ConfigurationProperty("aktiv", IsRequired = false, DefaultValue = true)]
        public bool IstAktiv
        {
            get { return (bool)this["aktiv"]; }
        }

        [ConfigurationProperty("einzelkampf", IsRequired = true)]
        public CacheConfigurationElement Einzelkampf
        {
            get { return (CacheConfigurationElement)this["einzelkampf"]; }
        }

        [ConfigurationProperty("mannschaftskampf", IsRequired = true)]
        public CacheConfigurationElement Mannschaftskampf
        {
            get { return (CacheConfigurationElement)this["mannschaftskampf"]; }
        }

        [ConfigurationProperty("mannschaftskaempfe", IsRequired = true)]
        public CacheConfigurationElement Mannschaftskaempfe
        {
            get { return (CacheConfigurationElement)this["mannschaftskaempfe"]; }
        }

        [ConfigurationProperty("mannschaftskampfSchema", IsRequired = true)]
        public CacheConfigurationElement MannschaftskampfSchema
        {
            get { return (CacheConfigurationElement)this["mannschaftskampfSchema"]; }
        }

        [ConfigurationProperty("ligaMitPlatzierung", IsRequired = true)]
        public CacheConfigurationElement LigaMitPlatzierung
        {
            get { return (CacheConfigurationElement)this["ligaMitPlatzierung"]; }
        }

        [ConfigurationProperty("ligen", IsRequired = true)]
        public CacheConfigurationElement Ligen
        {
            get { return (CacheConfigurationElement)this["ligen"]; }
        }

        [ConfigurationProperty("saisons", IsRequired = true)]
        public CacheConfigurationElement Saisons
        {
            get { return (CacheConfigurationElement)this["saisons"]; }
        }

        [ConfigurationProperty("saison", IsRequired = true)]
        public CacheConfigurationElement Saison
        {
            get { return (CacheConfigurationElement)this["saison"]; }
        }

        [ConfigurationProperty("kampftage", IsRequired = true)]
        public CacheConfigurationElement Kampftage
        {
            get { return (CacheConfigurationElement)this["kampftage"]; }
        }
        
        [ConfigurationProperty("mannschaften", IsRequired = true)]
        public CacheConfigurationElement Mannschaften
        {
            get { return (CacheConfigurationElement)this["mannschaften"]; }
        }

        [ConfigurationProperty("ringer", IsRequired = true)]
        public CacheConfigurationElement Ringer
        {
            get { return (CacheConfigurationElement)this["ringer"]; }
        }
    }
}
