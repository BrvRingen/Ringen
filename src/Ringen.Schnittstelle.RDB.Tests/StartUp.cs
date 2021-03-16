using System.Net;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;
using Ringen.Shared;

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

            GlobaleVariablen.AktivesSystem = ErgebnisdienstSystem.RDB;
        }
    }
}
