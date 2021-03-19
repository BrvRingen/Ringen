using Ninject.Modules;
using Ringen.Schnittstelle.RDB.ConfigSections;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Konvertierer;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstelle.RDB.Models;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.DependencyInjection
{
    internal class RDBDiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<RdbSystemSettings>().ToMethod(_ => new RdbSystemSettings(RdbConfigSection.Instance)).InSingletonScope();
            Bind<RdbService>().ToProvider<RdbServiceProvider>();


            Bind<IApiMannschaftskaempfe>().To<ApiMannschaftskaempfe>().InSingletonScope();
            Bind<IApiSaisonInformationen>().To<ApiSaisonInformationen>().InSingletonScope();
            Bind<IApiErgebnisdienst>().To<ApiErgebnisdienst>().InSingletonScope();
            Bind<IApiStammdaten>().To<ApiStammdaten>().InSingletonScope();

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
