using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.SaisonInformationenTests
{
    [TestFixture]
    public class GetLigenTests
    {
        private ISaisonInformationen _saisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RdbService rdbService = RdbServiceErsteller.ErstelleService();
            _saisonInformationen = new SaisonInformationen(rdbService);
        }

        [Test]
        public void Call_erwarte_Erfolg()
        {
            List<Liga> ligen = _saisonInformationen.GetLigen("2019");
            ligen.Should().NotBeNull();
            ligen.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Abgeschlossene_Saison_erwarte_korrekte_Ligen()
        {
            List<Liga> ligen = _saisonInformationen.GetLigen("2019");


            ligen.Count.Should().Be(15);

            Liga oberliga =
                ligen.FirstOrDefault(li => li.LigaId.Equals("Oberliga") && li.TabellenId.Equals("Westfalen"));
            oberliga.LigaId.Should().Be("Oberliga");
            oberliga.Bezeichnung.Should().Be("Oberliga Westfalen 2019");

            Liga bezirksliga =
                ligen.FirstOrDefault(li => li.LigaId.Equals("Bezirksliga") && li.TabellenId.Equals("Westfalen"));
            bezirksliga.LigaId.Should().Be("Bezirksliga");
            bezirksliga.Bezeichnung.Should().Be("Bezirksliga Westfalen 2019");
        }

        [Test]
        public void Offene_Saison_erwarte_korrekte_Ligen()
        {
            List<Liga> ligen = _saisonInformationen.GetLigen("2020");

            ligen.Count.Should().Be(1);
            ligen[0].LigaId.Should().Be("Oberliga");
            ligen[0].Bezeichnung.Should().Be("Oberliga 2020");
        }
    }
}
