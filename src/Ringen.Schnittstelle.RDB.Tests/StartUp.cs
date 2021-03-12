using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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

            RdbServiceErsteller.Init(new RdbSystemSettings("http://test.rdb.ringen-nrw.de/index.php", new NetworkCredential("", "")));
            
            GlobaleVariablen.AktivesSystem = ErgebnisdienstSystem.RDB;
        }
    }
}
