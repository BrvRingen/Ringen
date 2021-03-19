using System;
using Ninject;

namespace Ringen.Schnittstelle.RDB.DependencyInjection
{
    internal class RDBNinjectKernel
    {
        private static IKernel _innerKernel;
        private static readonly object _lock = new object();

        public static void CreateKernel()
        {
            lock (_lock)
            {
                _innerKernel = new StandardKernel(new RDBDiModule());
            }
        }

        public static TContract GetService<TContract>() => _innerKernel.Get<TContract>();
    }
}
