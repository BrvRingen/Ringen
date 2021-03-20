using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.MannschaftskaempfeTests
{
    [TestFixture]
    public class GetEinzelkampfTests
    {
        private IApiMannschaftskaempfe _apiMannschaftskaempfe;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiMannschaftskaempfe = new ServiceErsteller().GetService<IApiMannschaftskaempfe>();
        }



        [Test]
        public void Call_erwarte_Erfolg()
        {
            Einzelkampf einzelkampf = _apiMannschaftskaempfe.Get_Einzelkampf_Async("2019", "011008a", 1).Result;
            einzelkampf.Should().NotBeNull();
        }

        [Test]
        public void Abgeschlossene_Saison_erwarte_korrekte_Platzierungen()
        {
            Einzelkampf einzelkampf = _apiMannschaftskaempfe.Get_Einzelkampf_Async("2019", "011008a", 1).Result;

            einzelkampf.KampfNr.Should().Be(1);
            einzelkampf.Gewichtsklasse.Should().Be("57");
            einzelkampf.Stilart.Should().Be(Stilart.Freistil);

            einzelkampf.HeimRinger.Vorname.Should().Be("Matin");
            einzelkampf.HeimRinger.Nachname.Should().Be("Sakhi");
            einzelkampf.HeimRinger.Startausweisnummer.Should().Be("11358");
            einzelkampf.HeimRinger.Lizenznummer.Should().Be("0");
            einzelkampf.HeimRinger.Status.Should().Be("");

            einzelkampf.GastRinger.Vorname.Should().Be("Szabolcz");
            einzelkampf.GastRinger.Nachname.Should().Be("Lakatos");
            einzelkampf.GastRinger.Startausweisnummer.Should().Be("11351");
            einzelkampf.GastRinger.Lizenznummer.Should().Be("0");
            einzelkampf.GastRinger.Status.Should().Be("");

            einzelkampf.HeimMannschaftswertung.Should().Be(0);
            einzelkampf.GastMannschaftswertung.Should().Be(4);
            einzelkampf.RundenErgebnisse.First().Should().Be(new KeyValuePair<int, string>(1, "0:15"));
            einzelkampf.Siegart.Should().Be(Siegart.TechnischUeberlegen);
            einzelkampf.Kampfdauer.Should().Be(new TimeSpan(0, 4, 23));
            einzelkampf.Kommentar.Should().Be("");

            //"PR62,AR97,1B128,4B171,2B176,2B226,2B237,2B241,2B255"
            List<Griffbewertungspunkt> sollPunkte = new List<Griffbewertungspunkt>();
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Heim, GriffbewertungsTyp.Passiv, new TimeSpan(0, 1, 2)));
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Heim, GriffbewertungsTyp.Aktivitaetszeit, new TimeSpan(0, 1, 37)));
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Gast, GriffbewertungsTyp.Punkt, new TimeSpan(0, 2, 8), 1));
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Gast, GriffbewertungsTyp.Punkt, new TimeSpan(0, 2, 51),4));
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Gast, GriffbewertungsTyp.Punkt, new TimeSpan(0, 2, 56),2));
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Gast, GriffbewertungsTyp.Punkt, new TimeSpan(0, 3, 46),2));
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Gast, GriffbewertungsTyp.Punkt, new TimeSpan(0, 3, 57),2));
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Gast, GriffbewertungsTyp.Punkt, new TimeSpan(0, 4, 1),2));
            sollPunkte.Add(new Griffbewertungspunkt(HeimGast.Gast, GriffbewertungsTyp.Punkt, new TimeSpan(0, 4, 15), 2));

            einzelkampf.Wertungspunkte.Should().BeEquivalentTo(sollPunkte);
        }

        [Test]
        public void Offene_Saison_erwarte_leere_Platzierungen()
        {
            Func<Task> act = async () => { await _apiMannschaftskaempfe.Get_Einzelkampf_Async("2020", "013003b", 1); };

            act.Should().Throw<ApiNichtGefundenException>()
                .WithMessage("Es sind keine Kämpfe für Saison 2020 und Wettkampf 013003b (AC Ückerath 1961 vs. KSK Konkordia Neuss am 2020-09-05) vorhanden.");
        }
    }
}
