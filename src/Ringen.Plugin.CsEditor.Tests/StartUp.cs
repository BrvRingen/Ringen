using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ringen.Core.Services;

namespace Ringen.Plugin.CsEditor.Tests
{
    [SetUpFixture]
    public class StartUp
    {
        [OneTimeSetUp]
        public void Init()
        {
            ServiceBasic.Register(typeof(IRingenService), typeof(RingenService));
            Service.Plugin.InitializeSystem();
        }
    }
}
