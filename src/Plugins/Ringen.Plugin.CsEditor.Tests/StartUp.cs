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
