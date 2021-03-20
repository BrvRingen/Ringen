using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.StammdatenTests
{
    [TestFixture]
    public class GetMannschaftenTests
    {
        private IApiStammdaten _apiStammdaten;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiStammdaten = new ServiceErsteller().GetService<IApiStammdaten>();
        }

        [Test]
        public void Alle_Mannschaften_von_OM_erwarte_Erfolg()
        {
            List<Mannschaft> mannschaften = _apiStammdaten.Get_Mannschaften_Async().Result;
            mannschaften.Should().NotBeNull();
            mannschaften.Count.Should().BeGreaterThan(0);

            mannschaften
                .FirstOrDefault(li => li.Kurzname.Equals("Aachen EUREGIO Sports", StringComparison.OrdinalIgnoreCase))
                .Should().NotBeNull();
        }
    }
}
