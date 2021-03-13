using System.Configuration;

namespace Ringen.Schnittstelle.RDB.ConfigSections.ConfigurationElemente
{
    public class RdbServerConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// http://test.rdb.ringen-nrw.de
        /// </summary>
        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["host"]; }
            set { this["host"] = value; }
        }
    }
}
