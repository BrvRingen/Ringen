using Ninject.Modules;
using Ringen.Schnittstellen.Contracts.Factories;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Core.DependencyInjection.Module
{
    class SchittstelleErgebnisdienstModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApiMannschaftskaempfe>().ToMethod(x => DependencyInjectionContainer.GetService<IServiceErsteller>()
                .GetService<IApiMannschaftskaempfe>()).InSingletonScope();
            Bind<IApiSaisonInformationen>().ToMethod(x => DependencyInjectionContainer.GetService<IServiceErsteller>().GetService<IApiSaisonInformationen>()).InSingletonScope();
            Bind<IApiErgebnisdienst>().ToMethod(x => DependencyInjectionContainer.GetService<IServiceErsteller>().GetService<IApiErgebnisdienst>()).InSingletonScope();
            Bind<IApiStammdaten>().ToMethod(x => DependencyInjectionContainer.GetService<IServiceErsteller>().GetService<IApiStammdaten>()).InSingletonScope();
        }
    }
}
