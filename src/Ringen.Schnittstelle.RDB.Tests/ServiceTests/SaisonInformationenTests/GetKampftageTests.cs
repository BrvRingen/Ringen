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
    public class GetKampftageTests
    {
        private IApiSaisonInformationen _apiSaisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiSaisonInformationen = new ServiceErsteller().GetService<IApiSaisonInformationen>();
        }

        [Test]
        public void Call_erwarte_Erfolg()
        {
            List<Kampftag> kampftage = _apiSaisonInformationen.Get_Kampftage_Async("2019").Result;
            kampftage.Should().NotBeNull();
            kampftage.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Abgeschlossene_Saison_erwarte_korrekte_Ligen()
        {
            List<Kampftag> kampftage = _apiSaisonInformationen.Get_Kampftage_Async("2019").Result;

            DateTime datum = new DateTime(2019, 8, 31);
            for (int i = 1; i <= 17; i++)
            {
                kampftage.FirstOrDefault(li => li.KampftagNummer == i).Datum.Should().Be(datum);
                datum = datum.AddDays(7);
            }
        }

        [Test]
        public void Offene_Saison_erwarte_korrekte_Ligen()
        {
            List<Kampftag> kampftage = _apiSaisonInformationen.Get_Kampftage_Async("2020").Result;

            DateTime datum = new DateTime(2020, 9, 5);
            for (int i = 1; i <= 18; i++)
            {
                kampftage.FirstOrDefault(li => li.KampftagNummer == i).Datum.Should().Be(datum);
                datum = datum.AddDays(7);
            }
            
        }
    }
}
