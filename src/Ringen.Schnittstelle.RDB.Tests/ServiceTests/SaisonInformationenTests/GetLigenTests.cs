using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.DependencyInjection;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.SaisonInformationenTests
{
    [TestFixture]
    public class GetLigenTests
    {
        private ISaisonInformationen _saisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _saisonInformationen = DependencyInjectionContainer.GetService<ISaisonInformationen>();
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
            
            Liga oberliga =
                ligen.FirstOrDefault(li => li.LigaId.Equals("Oberliga"));
            oberliga.LigaId.Should().Be("Oberliga");
            oberliga.Bezeichnung.Should().Be("Oberliga 2020");
            oberliga.Austragungsmodus.Should().Be(Austragungsmodus.HinRueckRunde);

            Liga landesliga =
                ligen.FirstOrDefault(li => li.LigaId.Equals("Landesliga"));
            landesliga.LigaId.Should().Be("Landesliga");
            landesliga.Bezeichnung.Should().Be("Landesliga 2020");
            landesliga.Austragungsmodus.Should().Be(Austragungsmodus.HinRueckRunde);

            Liga bezirksliga =
                ligen.FirstOrDefault(li => li.LigaId.Equals("Bezirksliga"));
            bezirksliga.LigaId.Should().Be("Bezirksliga");
            bezirksliga.Bezeichnung.Should().Be("Bezirksliga 2020");
            bezirksliga.Austragungsmodus.Should().Be(Austragungsmodus.Doppelrunde);
        }
    }
}
