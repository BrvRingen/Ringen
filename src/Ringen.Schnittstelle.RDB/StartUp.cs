using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstelle.RDB.DependencyInjection;

namespace Ringen.Schnittstelle.RDB
{
    public class StartUp
    {
        public static void Init()
        {
            RDBNinjectKernel.CreateKernel();
        }
    }
}
