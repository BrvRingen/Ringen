using Ninject.Modules;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.ViewModels;

namespace Ringen.Core.DependencyInjection.Module
{
    public class RingenCoreDiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<SaisonService>().ToSelf().InSingletonScope();
            Bind<MannschaftskaempfeService>().ToSelf().InSingletonScope();
            Bind<StammdatenService>().ToSelf().InSingletonScope();

            Bind<ExplorerStates>().ToSelf().InSingletonScope();
        }
    }
}
