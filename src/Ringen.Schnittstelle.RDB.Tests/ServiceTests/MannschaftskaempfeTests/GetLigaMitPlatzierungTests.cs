﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.MannschaftskaempfeTests
{
    [TestFixture]
    public class GetLigaMitPlatzierungTests
    {
        private IMannschaftskaempfe _mannschaftskaempfe;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mannschaftskaempfe = new ServiceErsteller().GetService<IMannschaftskaempfe>();
        }

        [Test]
        public void Call_erwarte_Erfolg()
        {
            Tuple<Liga, List<Tabellenplatzierung>> ligaTuple = _mannschaftskaempfe.GetLigaMitPlatzierungAsync("2019", "Oberliga", "Westfalen").Result;
            ligaTuple.Should().NotBeNull();
        }

        [Test]
        public void Abgeschlossene_Saison_erwarte_korrekte_Platzierungen()
        {
            Tuple<Liga, List<Tabellenplatzierung>> ligaTuple = _mannschaftskaempfe.GetLigaMitPlatzierungAsync("2019", "Oberliga", "Westfalen").Result;

            ligaTuple.Item1.Bezeichnung.Should().Be("Oberliga Westfalen 2019");
            ligaTuple.Item2.FirstOrDefault(li => li.Tabellenplatz == 1).TeamId.Should().Be("KSV Witten 07 II");
            ligaTuple.Item2.FirstOrDefault(li => li.Tabellenplatz == 6).TeamId.Should().Be("KSV Hohenlimburg");
        }

        [Test]
        public void Offene_Saison_erwarte_leere_Platzierungen()
        {
            Tuple<Liga, List<Tabellenplatzierung>> ligaTuple = _mannschaftskaempfe.GetLigaMitPlatzierungAsync("2020", "Oberliga", "").Result;

            ligaTuple.Item1.Bezeichnung.Should().Be("Oberliga 2020");
            ligaTuple.Item2.ForEach(li => li.Tabellenplatz.Should().Be(0));
        }
    }
}
