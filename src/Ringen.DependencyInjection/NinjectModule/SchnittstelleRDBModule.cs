using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            var aktivesSystem = GlobaleVariablen.AktivesSystem;

            if (aktivesSystem == ErgebnisdienstSystem.RDB)
            {
                Ringen.Schnittstelle.RDB.StartUp.Init();
            }

            Bind<IServiceErsteller>().To<Ringen.Schnittstelle.RDB.Factories.ServiceErsteller>()
                .When(_ => aktivesSystem == ErgebnisdienstSystem.RDB)
                .InSingletonScope();
        }
    }
}
