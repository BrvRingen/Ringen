using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ringen.DependencyInjection.NinjectModule;
using Ringen.Schnittstelle.RDB.DependencyInjection;
using Ringen.Schnittstellen.Contracts.Factories;

namespace Ringen.DependencyInjection
{
    public static class DependencyInjectionContainer
    {
        private static IKernel _innerKernel;
        private static readonly object _lock = new object();

        public static void CreateKernel()
        {
            lock (_lock)
            {
                _innerKernel = new StandardKernel(new SchnittstelleRDBModule(), new SchittstelleErgebnisdienstModule());
            }
        }

        public static TContract GetService<TContract>() => _innerKernel.Get<TContract>();
    }
}
