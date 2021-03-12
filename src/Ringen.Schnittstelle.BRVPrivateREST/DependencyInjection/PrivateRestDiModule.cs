using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ringen.Schnittstellen.Contracts.Models.Enums;
using Ringen.Shared;

namespace Ringen.Schnittstelle.BRVPrivateREST.DependencyInjection
{
    public class PrivateRestDiModule : NinjectModule
    {
        public override void Load()
        {
            ErgebnisdienstSystem system = GlobaleVariablen.AktivesSystem;
        }
    }
}
