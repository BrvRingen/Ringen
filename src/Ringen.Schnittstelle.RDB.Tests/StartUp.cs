using NUnit.Framework;
using Ringen.Schnittstelle.RDB.DependencyInjection;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Tests
{
    [SetUpFixture]
    class StartUp
    {
        [OneTimeSetUp]
        public void Init()
        {
            if (Ringen.Tests.Shared.StartUp.IstInitialisiert == false)
            {
                Ringen.Tests.Shared.StartUp.Init();
            }

            Ringen.Schnittstelle.RDB.StartUp.Init();
        }
    }
}
