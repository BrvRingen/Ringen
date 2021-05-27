using Ninject.Modules;
using Ringen.Core.Services.ErgebnisdienstApi;

namespace Ringen.Core.DependencyInjection.Module
{
    public class RingenCoreDiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<SaisonService>().ToSelf().InSingletonScope();
            Bind<MannschaftskaempfeService>().ToSelf().InSingletonScope();
        }
    }
}
