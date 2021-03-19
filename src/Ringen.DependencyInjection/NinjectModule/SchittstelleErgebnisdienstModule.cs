using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Factories;
using Ringen.Schnittstellen.Contracts.Interfaces;

namespace Ringen.DependencyInjection.NinjectModule
{
    class SchittstelleErgebnisdienstModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IMannschaftskaempfe>().ToMethod(x => DependencyInjectionContainer.GetService<IServiceErsteller>().GetService<IMannschaftskaempfe>()).InSingletonScope();
            Bind<ISaisonInformationen>().ToMethod(x => DependencyInjectionContainer.GetService<IServiceErsteller>().GetService<ISaisonInformationen>()).InSingletonScope();
            Bind<IErgebnisdienst>().ToMethod(x => DependencyInjectionContainer.GetService<IServiceErsteller>().GetService<IErgebnisdienst>()).InSingletonScope();
            Bind<IStammdaten>().ToMethod(x => DependencyInjectionContainer.GetService<IServiceErsteller>().GetService<IStammdaten>()).InSingletonScope();
        }
    }
}
