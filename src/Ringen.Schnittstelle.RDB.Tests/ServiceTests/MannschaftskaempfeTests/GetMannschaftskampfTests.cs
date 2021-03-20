using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.MannschaftskaempfeTests
{
    [TestFixture]
    public class GetMannschaftskampfTests
    {
        private IApiMannschaftskaempfe _apiMannschaftskaempfe;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiMannschaftskaempfe = new ServiceErsteller().GetService<IApiMannschaftskaempfe>();
        }


        [Test]
        public void GetMannschaftskaempfe_erwarte_Erfolg()
        {
            List<Mannschaftskampf> wettkampfListe = _apiMannschaftskaempfe.Get_Mannschaftskaempfe_Async("2019", "Oberliga", "Westfalen").Result;

            wettkampfListe.Should().NotBeNull();
            wettkampfListe.Count.Should().BeGreaterThan(0);

            wettkampfListe.FirstOrDefault(li => li.WettkampfId.Equals("011008a")).Should().NotBeNull();
        }

        [Test]
        public void Abgeschlossene_Saison_erwarte_korrekte_Daten()
        {
            Tuple<Mannschaftskampf, List<Einzelkampf>> wettkampf = _apiMannschaftskaempfe.Get_Mannschaftskampf_Async("2019", "011008a").Result;

            wettkampf.Should().NotBeNull();
            wettkampf.Item1.Should().NotBeNull();
            wettkampf.Item2.Should().NotBeNull();

            wettkampf.Item1.WettkampfId.Should().Be("011008a");
            wettkampf.Item1.HeimMannschaft.Should().Be("TV Essen-Dellwig");
            wettkampf.Item1.GastMannschaft.Should().Be("TSG Herdecke");
            wettkampf.Item1.Wettkampfstaette.Should().Be("Gertrud-Bäumer-Realschule, Grünstraße 54, 45326 Essen");
            wettkampf.Item1.Schiedsrichter_Nachname.Should().Be("Manz");
            wettkampf.Item1.Schiedsrichter_Vorname.Should().Be("Uwe");
            wettkampf.Item1.IstErgebnisGeprueft.Should().BeTrue();
            wettkampf.Item1.Kampfdatum.Should().Be(new DateTime(2019, 8, 31));
            wettkampf.Item1.GeplanterKampfbeginn.Should().Be(new TimeSpan(19, 0, 0));
            wettkampf.Item1.EchterKampfbeginn.Should().Be(new TimeSpan(0, 0, 0));
            wettkampf.Item1.EchtesKampfende.Should().Be(new TimeSpan(0, 0, 0));
            wettkampf.Item1.AnzahlZuschauer.Should().Be(50);
            wettkampf.Item1.HeimPunkte.Should().Be(25);
            wettkampf.Item1.GastPunkte.Should().Be(15);
            wettkampf.Item1.Sieger.Should().Be(HeimGast.Heim);
        }

        [Test]
        public void Offene_Saison_erwarte_korrekte_Daten()
        {
            Tuple<Mannschaftskampf, List<Einzelkampf>> wettkampf = _apiMannschaftskaempfe.Get_Mannschaftskampf_Async("2020", "047012b").Result;

            wettkampf.Should().NotBeNull();
            wettkampf.Item1.Should().NotBeNull();
            wettkampf.Item2.Count.Should().Be(0); //Keine Einzelkämpfe, da Kampf noch nicht stattgefunden

            wettkampf.Item1.WettkampfId.Should().Be("047012b");
            wettkampf.Item1.HeimMannschaft.Should().Be("AC Mülheim am Rhein II");
            wettkampf.Item1.GastMannschaft.Should().Be("KSK Konkordia Neuss II");
            wettkampf.Item1.Wettkampfstaette.Should().Be("Sporthalle, Bergischer Ring 40, 51063 Köln");
            wettkampf.Item1.Schiedsrichter_Nachname.Should().Be("");
            wettkampf.Item1.Schiedsrichter_Vorname.Should().Be("");
            wettkampf.Item1.IstErgebnisGeprueft.Should().BeFalse();
            wettkampf.Item1.Kampfdatum.Should().Be(new DateTime(2020, 9, 5));
            wettkampf.Item1.GeplanterKampfbeginn.Should().Be(new TimeSpan(19, 0, 0));
            wettkampf.Item1.EchterKampfbeginn.Should().Be(new TimeSpan(0, 0, 0));
            wettkampf.Item1.EchtesKampfende.Should().Be(new TimeSpan(0, 0, 0));
            wettkampf.Item1.AnzahlZuschauer.Should().Be(0);
            wettkampf.Item1.HeimPunkte.Should().Be(0);
            wettkampf.Item1.GastPunkte.Should().Be(0);
            wettkampf.Item1.Sieger.Should().Be(HeimGast.Unbekannt);
        }
    }
}
