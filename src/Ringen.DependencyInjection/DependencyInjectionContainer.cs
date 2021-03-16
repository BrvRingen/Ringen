using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ringen.Schnittstelle.RDB.DependencyInjection;

namespace Ringen.DependencyInjection
{
    public static class DependencyInjectionContainer
    {
        private static IKernel _innerKernel;
        private static readonly object _lock = new object();

        public static IKernel GetKernel()
        {
            return _innerKernel;
        }

        public static void CreateKernel()
        {
            lock (_lock)
            {
                _innerKernel = new StandardKernel(new RDBDiModule());

                try
                {
                    RegisterServices();
                }
                catch
                {
                    _innerKernel.Dispose();
                    throw;
                }
            }
        }

        public static TContract GetService<TContract>() => _innerKernel.Get<TContract>();

        public static object GetService(Type contractType)
        {
            var implementation = _innerKernel.Get(contractType);
            return implementation;
        }


        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices()
        {
        }
    }
}
