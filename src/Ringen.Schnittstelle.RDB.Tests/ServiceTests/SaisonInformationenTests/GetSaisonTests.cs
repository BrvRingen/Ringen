using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.SaisonInformationenTests
{
    [TestFixture]
    public class GetSaisonTests
    {
        private IApiSaisonInformationen _apiSaisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiSaisonInformationen = new ServiceErsteller().GetService<IApiSaisonInformationen>();
        }
        
        [Test]
        public void GetSaisons_erwarte_Erfolg()
        {
            List<Saison> saisonListe = _apiSaisonInformationen.Get_Saisons_Async().Result;

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
            Tuple<Saison, List<Leistungsklasse>> saison = _apiSaisonInformationen.Get_Saison_Async(saisonId).Result;

            saison.Should().NotBeNull();
            saison.Item1.SaisonId.Should().Be(saisonId);

            saison.Item2
                .FirstOrDefault(li =>
                    li.Bezeichnung.Equals(erwarteteLeistungsklasse, StringComparison.OrdinalIgnoreCase)).Should()
                .NotBeNull();
        }
    }
}
