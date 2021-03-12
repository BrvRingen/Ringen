using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.DependencyInjection;

namespace Ringen.Tests.Shared
{
    public class StartUp
    {

        public static bool IstInitialisiert = false;
        private static object _lock = new object();

        public static void Init()
        {
            lock (_lock)
            {
                if (IstInitialisiert == false)
                {
                    DependencyInjectionContainer.CreateKernel();
                    IstInitialisiert = true;
                }
            }
        }
    }
}
