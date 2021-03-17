using System.Net;
using Ninject.Modules;
using Ringen.Schnittstelle.RDB.ConfigSections;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Konvertierer;
using Ringen.Schnittstelle.RDB.Mapper;
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
            RdbServiceErsteller.Init(new RdbSystemSettings(RdbConfigSection.Instance));

            Bind<RdbService>().ToMethod(x => RdbServiceErsteller.ErstelleService())
                .When(_ => system.Equals(ErgebnisdienstSystem.RDB))
                .InSingletonScope();

            Bind<IMannschaftskaempfe>().To<Mannschaftskaempfe>()
                .When(_ => system.Equals(ErgebnisdienstSystem.RDB))
                .InSingletonScope();

            Bind<ISaisonInformationen>().To<SaisonInformationen>()
                .When(_ => system.Equals(ErgebnisdienstSystem.RDB))
                .InSingletonScope();

            Bind<GriffbewertungspunktKonvertierer>().ToSelf().InSingletonScope();
            Bind<GriffbewertungsTypKonvertierer>().ToSelf().InSingletonScope();
            Bind<HeimGastKonvertierer>().ToSelf().InSingletonScope();
            Bind<SiegartKonvertierer>().ToSelf().InSingletonScope();
            Bind<StilartKonvertierer>().ToSelf().InSingletonScope();

            Bind<MannschaftskampfPostMapper>().ToSelf().InSingletonScope();
            Bind<EinzelkampfMapper>().ToSelf().InSingletonScope();
        }
    }
}
