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
    public class GetMannschaftenTests
    {
        private IApiSaisonInformationen _apiSaisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiSaisonInformationen = new ServiceErsteller().GetService<IApiSaisonInformationen>();
        }

        [Test]
        public void Offene_Saison_Oberliga_erwarte_Mannschaft()
        {
            List<Mannschaft> mannschaften = _apiSaisonInformationen.Get_Mannschaften_Async("2020", "Oberliga", "").Result;
            mannschaften.Should().NotBeNull();
            mannschaften.Count.Should().BeGreaterThan(0);

            mannschaften
                .FirstOrDefault(li => li.Kurzname.Equals("AC Mülheim am Rhein", StringComparison.OrdinalIgnoreCase))
                .Should().NotBeNull();
        }

        [Test]
        public void Abgeschlossene_Saison_Oberliga_erwarte_Mannschaft()
        {
            List<Mannschaft> mannschaften = _apiSaisonInformationen.Get_Mannschaften_Async("2019", "Oberliga", "Westfalen").Result;
            mannschaften.Should().NotBeNull();
            mannschaften.Count.Should().BeGreaterThan(0);

            mannschaften
                .FirstOrDefault(li => li.Kurzname.Equals("KSV Hohenlimburg", StringComparison.OrdinalIgnoreCase))
                .Should().NotBeNull();
        }

        [Test]
        public void Abgeschlossene_Saison_Bezirksliga_erwarte_Mannschaft()
        {
            List<Mannschaft> mannschaften = _apiSaisonInformationen.Get_Mannschaften_Async("2019", "Bezirksliga", "Westfalen").Result;
            mannschaften.Should().NotBeNull();
            mannschaften.Count.Should().BeGreaterThan(0);

            mannschaften
                .FirstOrDefault(li => li.Kurzname.Equals("AC Hörde 04", StringComparison.OrdinalIgnoreCase))
                .Should().NotBeNull();
        }
    }
}
