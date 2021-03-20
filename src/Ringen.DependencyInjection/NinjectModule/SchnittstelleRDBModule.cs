using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstelle.Caching;
using Ringen.Schnittstelle.Caching.ConfigSections;
using Ringen.Schnittstelle.Caching.Factories;
using Ringen.Schnittstelle.Caching.Models;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Factories;
using Ringen.Schnittstellen.Contracts.Models.Enums;
using Ringen.Shared;

namespace Ringen.DependencyInjection.NinjectModule
{
    class SchnittstelleRDBModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            var aktivesSystem = GlobaleVariablen.AktiveApiSchnittstelle;

            if (aktivesSystem == ErgebnisdienstSystem.RDB)
            {
                Ringen.Schnittstelle.RDB.StartUp.Init();
            }

            var istApiCache = ApiCacheConfigSection.Instance.IstAktiv;

            Bind<IServiceErsteller>().To<Ringen.Schnittstelle.RDB.Factories.ServiceErsteller>()
                .When(_ => aktivesSystem == ErgebnisdienstSystem.RDB && !istApiCache)
                .InSingletonScope();

            Bind<IServiceErsteller>().ToMethod(x => new ServiceErstellerMitCache(new ServiceErsteller(), new CacheZeiten(ApiCacheConfigSection.Instance)))
                .When(_ => aktivesSystem == ErgebnisdienstSystem.RDB && istApiCache)
                .InSingletonScope();
        }
    }
}
