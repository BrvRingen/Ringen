using System;
using Ringen.Schnittstelle.RDB.ConfigSections;
using Ringen.Schnittstelle.RDB.DependencyInjection;
using Ringen.Schnittstelle.RDB.Models;
using Ringen.Schnittstellen.Contracts.Factories;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Factories
{
    public class ServiceErsteller : IServiceErsteller
    {
        public T GetService<T>()
        {
            return RDBNinjectKernel.GetService<T>();
        }
    }
}
