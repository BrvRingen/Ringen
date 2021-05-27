using Ninject;
using Ringen.Core.DependencyInjection.Module;

namespace Ringen.Core.DependencyInjection
{
    public static class DependencyInjectionContainer
    {
        private static IKernel _innerKernel;
        private static readonly object _lock = new object();

        public static void CreateKernel()
        {
            lock (_lock)
            {
                _innerKernel = new StandardKernel(new SchnittstelleRDBModule(), new SchittstelleErgebnisdienstModule(), new RingenCoreDiModule());
            }
        }

        public static TContract GetService<TContract>() => _innerKernel.Get<TContract>();
    }
}
