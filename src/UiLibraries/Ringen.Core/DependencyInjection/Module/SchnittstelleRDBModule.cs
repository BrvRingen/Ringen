using System.Collections.Generic;
using System.Net;
using Ninject.Modules;
using Ringen.Configs;
using Ringen.Configs.ConfigSections;
using Ringen.CrossCutting.Helpers;
using Ringen.Schnittstellen.Caching.Factories;
using Ringen.Schnittstellen.Caching.Models;
using Ringen.Schnittstellen.Contracts.Factories;
using Ringen.Schnittstellen.RDB.Factories;
using Ringen.Schnittstellen.RDB.Models;

namespace Ringen.Core.DependencyInjection.Module
{
    class SchnittstelleRDBModule : NinjectModule
    {
        public override void Load()
        {
            var aktivesSystem = Ringen.Configs.GlobaleVariablen.AktiveApiSchnittstelle;
            if (aktivesSystem != ErgebnisdienstSystem.RDB)
            {
                return;
            }

            Ringen.Schnittstellen.RDB.StartUp.Init(GetRdbSystemSettings(RdbErgebnisdienstConfigSection.Instance));

            var istApiCache = ApiCacheConfigSection.Instance.IstAktiv;

            Bind<IServiceErsteller>().To<ServiceErsteller>()
                .When(_ => !istApiCache)
                .InSingletonScope();

            Bind<IServiceErsteller>().ToMethod(x => new ServiceErstellerMitCache(new ServiceErsteller(), GetCacheZeiten(ApiCacheConfigSection.Instance)))
                .When(_ => istApiCache)
                .InSingletonScope();
        }

        public RdbSystemSettings GetRdbSystemSettings(RdbErgebnisdienstConfigSection configSection)
        {
            var credentials = new NetworkCredential(configSection.Credentials.Benutzername, PasswordHelper.DecryptString(configSection.Credentials.EnryptedPasswort));
            var baseUrl = configSection.Api.Host;

            RdbSystemSettings result = new RdbSystemSettings(baseUrl, credentials);

            result.JsonReaderService = new KeyValuePair<string, string>(configSection.Api.JsonReaderService.Key, configSection.Api.JsonReaderService.Value);
            result.TaskCompetitionSystem = new KeyValuePair<string, string>(configSection.Api.TaskCompetitionSystem.Key, configSection.Api.TaskCompetitionSystem.Value);
            result.TaskOrganisationsmanager = new KeyValuePair<string, string>(configSection.Api.TaskOrganisationsmanager.Key, configSection.Api.TaskOrganisationsmanager.Value);

            return result;
        }

        public CacheZeiten GetCacheZeiten(ApiCacheConfigSection configSection)
        {
            CacheZeiten result = new CacheZeiten();

            result.EinzelkampfInTagen = configSection.Einzelkampf.CacheTage;
            result.MannschaftskampfInTagen = configSection.Mannschaftskampf.CacheTage;
            result.MannschaftskaempfeInTagen = configSection.Mannschaftskaempfe.CacheTage;
            result.LigaMitPlatzierungInTagen = configSection.LigaMitPlatzierung.CacheTage;
            result.MannschaftenInTagen = configSection.Mannschaften.CacheTage;
            result.RingerInTagen = configSection.Ringer.CacheTage;

            result.MannschaftskampfSchemaInTagen = configSection.MannschaftskampfSchema.CacheTage;
            result.LigenInTagen = configSection.Ligen.CacheTage;
            result.SaisonsInTagen = configSection.Saisons.CacheTage;
            result.SaisonInTagen = configSection.Saison.CacheTage;
            result.KampftageInTagen = configSection.Kampftage.CacheTage;

            return result;
        }
    }
}
