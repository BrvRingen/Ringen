using System.Collections.Generic;
using System.Net;
using Ringen.Schnittstelle.RDB.ConfigSections;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Shared.Helpers;

namespace Ringen.Schnittstelle.RDB.Models
{
    internal class RdbSystemSettings : IApiSettings
    {
        public string BaseUrl { get; }

        public NetworkCredential Credentials { get; }

        public KeyValuePair<string, string> JsonReaderService { get; set; } =
            new KeyValuePair<string, string>("sv", "json");

        public KeyValuePair<string, string> TaskCompetitionSystem { get; set; } =
            new KeyValuePair<string, string>("tk", "jr:cs");

        public KeyValuePair<string, string> TaskOrganisationsmanager { get; set; } =
            new KeyValuePair<string, string>("tk", "jr:om");

        public RdbSystemSettings(string baseUrl, NetworkCredential credentials)
        {
            BaseUrl = baseUrl;
            Credentials = credentials;
        }

        public RdbSystemSettings(string baseUrl, NetworkCredential credentials, 
            KeyValuePair<string, string> jsonReaderService, 
            KeyValuePair<string, string> taskCompetitionSystem, 
            KeyValuePair<string, string> taskOrganisationsmanager)
        {
            BaseUrl = baseUrl;
            Credentials = credentials;
            JsonReaderService = jsonReaderService;
            TaskCompetitionSystem = taskCompetitionSystem;
            TaskOrganisationsmanager = taskOrganisationsmanager;
        }

        public RdbSystemSettings(RdbConfigSection configSection)
        {
            Credentials = new NetworkCredential(configSection.Credentials.Benutzername, PasswordHelper.DecryptString(configSection.Credentials.EnryptedPasswort));
            BaseUrl = configSection.Api.Host;
            JsonReaderService = new KeyValuePair<string, string>(configSection.Api.JsonReaderService.Key, configSection.Api.JsonReaderService.Value);
            TaskCompetitionSystem = new KeyValuePair<string, string>(configSection.Api.TaskCompetitionSystem.Key, configSection.Api.TaskCompetitionSystem.Value);
            TaskOrganisationsmanager = new KeyValuePair<string, string>(configSection.Api.TaskOrganisationsmanager.Key, configSection.Api.TaskOrganisationsmanager.Value);
        }
    }
}
