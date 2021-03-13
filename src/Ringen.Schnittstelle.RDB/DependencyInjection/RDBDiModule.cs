using System.Net;
using Ninject.Modules;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Models;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models.Enums;
using Ringen.Shared;

namespace Ringen.Schnittstelle.RDB.DependencyInjection
{
    public class RDBDiModule : NinjectModule
    {
        public override void Load()
        {
            ErgebnisdienstSystem system = GlobaleVariablen.AktivesSystem; //TODO: Aus Konfig auslesen

            //TODO: In Konfig auslagern
            RdbServiceErsteller.Init(new RdbSystemSettings("http://test.rdb.ringen-nrw.de/index.php", new NetworkCredential("", "")));

            Bind<RdbService>().ToMethod(x => RdbServiceErsteller.ErstelleService())
                .When(_ => system.Equals(ErgebnisdienstSystem.RDB))
                .InSingletonScope();

            Bind<IErgebnisdienst>().To<Ergebnisdienst>()
                .When(_ => system.Equals(ErgebnisdienstSystem.RDB))
                .InSingletonScope();

            Bind<ISaisonInformationen>().To<SaisonInformationen>()
                .When(_ => system.Equals(ErgebnisdienstSystem.RDB))
                .InSingletonScope();
        }
    }
}
