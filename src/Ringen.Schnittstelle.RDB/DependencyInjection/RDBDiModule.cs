using Ninject.Modules;
using Ringen.Schnittstelle.RDB.ConfigSections;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Konvertierer;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstelle.RDB.Models;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;

namespace Ringen.Schnittstelle.RDB.DependencyInjection
{
    internal class RDBDiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<RdbSystemSettings>().ToMethod(_ => new RdbSystemSettings(RdbConfigSection.Instance)).InSingletonScope();
            Bind<RdbService>().ToProvider<RdbServiceProvider>();


            Bind<IMannschaftskaempfe>().To<Mannschaftskaempfe>().InSingletonScope();
            Bind<ISaisonInformationen>().To<SaisonInformationen>().InSingletonScope();
            Bind<IErgebnisdienst>().To<Ergebnisdienst>().InSingletonScope();
            Bind<IStammdaten>().To<Stammdaten>().InSingletonScope();

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
