using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.ErgebnisdienstTests
{
    [TestFixture]
    public class GetMannschaftskampfTests
    {
        private IErgebnisdienst _ergebnisdienst;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RdbService rdbService = RdbServiceErsteller.ErstelleService();
            _ergebnisdienst = new Ergebnisdienst(rdbService);
        }


        [Test]
        public void GetMannschaftskaempfe_erwarte_Erfolg()
        {
            List<Mannschaftskampf> wettkampfListe = _ergebnisdienst.GetMannschaftskaempfe("2019", "Oberliga", "Westfalen");

            wettkampfListe.Should().NotBeNull();
            wettkampfListe.Count.Should().BeGreaterThan(0);

            wettkampfListe.FirstOrDefault(li => li.CompetitionId.Equals("011008a")).Should().NotBeNull();
        }

        [Test]
        [TestCase("2019", "011008a")]
        public void GetMannschaftskampf_erwarte_korrekte_Daten(string saisonId, string wettkampfId)
        {
            Tuple<Mannschaftskampf, List<Einzelkampf>> wettkampf = _ergebnisdienst.GetMannschaftskampf(saisonId, wettkampfId);

            wettkampf.Should().NotBeNull();
            wettkampf.Item1.Should().NotBeNull();
            wettkampf.Item2.Should().NotBeNull();

            wettkampf.Item1.CompetitionId.Should().Be(wettkampfId);
            wettkampf.Item1.HeimMannschaft.Should().Be("TV Essen-Dellwig");
            wettkampf.Item1.GastMannschaft.Should().Be("TSG Herdecke");
            wettkampf.Item1.Wettkampfstaette.Should().Be("Gertrud-Bäumer-Realschule, Grünstraße 54, 45326 Essen");
            wettkampf.Item1.Schiedsrichter.Should().Be("Manz, Uwe");
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

        //TODO: GetWettkampf_wennPunkteUndValidatedPunkteUnterschiedlich_erwarte_korrekte_Daten
        //TODO: GetWettkampf_wennNochKeinErgebnis_erwarte_korrekte_Daten
    }
}
