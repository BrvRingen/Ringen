﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.DependencyInjection;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.SaisonInformationenTests
{
    [TestFixture]
    public class GetKampftageTests
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
            List<Kampftag> kampftage = _saisonInformationen.GetKampftageAsync("2019").Result;
            kampftage.Should().NotBeNull();
            kampftage.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Abgeschlossene_Saison_erwarte_korrekte_Ligen()
        {
            List<Kampftag> kampftage = _saisonInformationen.GetKampftageAsync("2019").Result;

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
            List<Kampftag> kampftage = _saisonInformationen.GetKampftageAsync("2020").Result;

            DateTime datum = new DateTime(2020, 9, 5);
            for (int i = 1; i <= 18; i++)
            {
                kampftage.FirstOrDefault(li => li.KampftagNummer == i).Datum.Should().Be(datum);
                datum = datum.AddDays(7);
            }
            
        }
    }
}
