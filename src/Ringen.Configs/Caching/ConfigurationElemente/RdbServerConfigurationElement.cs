using System.Configuration;
using Ringen.Shared.ConfigurationElemente;

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
        }

        [ConfigurationProperty("jsonReaderService", IsRequired = true)]
        public KeyValueStringConfigurationElement JsonReaderService
        {
            get { return (KeyValueStringConfigurationElement)this["jsonReaderService"]; }
        }

        [ConfigurationProperty("taskCompetitionSystem", IsRequired = true)]
        public KeyValueStringConfigurationElement TaskCompetitionSystem
        {
            get { return (KeyValueStringConfigurationElement)this["taskCompetitionSystem"]; }
        }

        [ConfigurationProperty("taskOrganisationsmanager", IsRequired = true)]
        public KeyValueStringConfigurationElement TaskOrganisationsmanager
        {
            get { return (KeyValueStringConfigurationElement)this["taskOrganisationsmanager"]; }
        }
    }
}
