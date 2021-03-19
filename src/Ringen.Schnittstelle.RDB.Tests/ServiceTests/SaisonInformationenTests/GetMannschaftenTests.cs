﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.SaisonInformationenTests
{
    [TestFixture]
    public class GetMannschaftenTests
    {
        private ISaisonInformationen _saisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _saisonInformationen = new ServiceErsteller().GetService<ISaisonInformationen>();
        }

        [Test]
        public void Offene_Saison_Oberliga_erwarte_Mannschaft()
        {
            List<Mannschaft> mannschaften = _saisonInformationen.GetMannschaftenAsync("2020", "Oberliga", "").Result;
            mannschaften.Should().NotBeNull();
            mannschaften.Count.Should().BeGreaterThan(0);

            mannschaften
                .FirstOrDefault(li => li.Kurzname.Equals("AC Mülheim am Rhein", StringComparison.OrdinalIgnoreCase))
                .Should().NotBeNull();
        }

        [Test]
        public void Abgeschlossene_Saison_Oberliga_erwarte_Mannschaft()
        {
            List<Mannschaft> mannschaften = _saisonInformationen.GetMannschaftenAsync("2019", "Oberliga", "Westfalen").Result;
            mannschaften.Should().NotBeNull();
            mannschaften.Count.Should().BeGreaterThan(0);

            mannschaften
                .FirstOrDefault(li => li.Kurzname.Equals("KSV Hohenlimburg", StringComparison.OrdinalIgnoreCase))
                .Should().NotBeNull();
        }

        [Test]
        public void Abgeschlossene_Saison_Bezirksliga_erwarte_Mannschaft()
        {
            List<Mannschaft> mannschaften = _saisonInformationen.GetMannschaftenAsync("2019", "Bezirksliga", "Westfalen").Result;
            mannschaften.Should().NotBeNull();
            mannschaften.Count.Should().BeGreaterThan(0);

            mannschaften
                .FirstOrDefault(li => li.Kurzname.Equals("AC Hörde 04", StringComparison.OrdinalIgnoreCase))
                .Should().NotBeNull();
        }
    }
}
