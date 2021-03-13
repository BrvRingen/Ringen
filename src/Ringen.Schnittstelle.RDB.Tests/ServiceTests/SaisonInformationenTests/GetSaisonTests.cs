using System;
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
    public class GetSaisonTests
    {
        private ISaisonInformationen _saisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RdbService rdbService = RdbServiceErsteller.ErstelleService();
            _saisonInformationen = new SaisonInformationen(rdbService);
        }
        
        [Test]
        public void GetSaisons_erwarte_Erfolg()
        {
            List<Saison> saisonListe = _saisonInformationen.GetSaisons();

            saisonListe.Should().NotBeNull();
            saisonListe.Count.Should().BeGreaterThan(1);

            saisonListe.FirstOrDefault(li => li.SaisonId.Equals("2020")).Should().NotBeNull();
            saisonListe.FirstOrDefault(li => li.SaisonId.Equals("2019")).Should().NotBeNull();
        }

        [Test]
        [TestCase("2020", "Männer")]
        [TestCase("2019", "Männer")]
        public void GetSaison_erwarte_Erfolg(string saisonId, string erwarteteLeistungsklasse)
        {
            Tuple<Saison, List<Leistungsklasse>> saison = _saisonInformationen.GetSaison(saisonId);

            saison.Should().NotBeNull();
            saison.Item1.SaisonId.Should().Be(saisonId);

            saison.Item2
                .FirstOrDefault(li =>
                    li.Bezeichnung.Equals(erwarteteLeistungsklasse, StringComparison.OrdinalIgnoreCase)).Should()
                .NotBeNull();
        }
    }
}
