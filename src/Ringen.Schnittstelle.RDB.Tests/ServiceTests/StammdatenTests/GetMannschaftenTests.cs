using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.StammdatenTests
{
    [TestFixture]
    public class GetMannschaftenTests
    {
        private IStammdaten _stammdaten;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _stammdaten = new ServiceErsteller().GetService<IStammdaten>();
        }

        [Test]
        public void Alle_Mannschaften_von_OM_erwarte_Erfolg()
        {
            List<Mannschaft> mannschaften = _stammdaten.GetMannschaftenAsync().Result;
            mannschaften.Should().NotBeNull();
            mannschaften.Count.Should().BeGreaterThan(0);

            mannschaften
                .FirstOrDefault(li => li.Kurzname.Equals("Aachen EUREGIO Sports", StringComparison.OrdinalIgnoreCase))
                .Should().NotBeNull();
        }
    }
}
