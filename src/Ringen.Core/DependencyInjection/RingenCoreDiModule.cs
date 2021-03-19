using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ringen.Core.Mapper;
using Ringen.Core.Services;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Factories;

namespace Ringen.Core.DependencyInjection
{
    public class RingenCoreDiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<SaisonService>().ToSelf().InSingletonScope();
            Bind<MannschaftskaempfeService>().ToSelf().InSingletonScope();


            Bind<MannschaftskaempfeViewModel>().ToSelf();
        }
    }
}
