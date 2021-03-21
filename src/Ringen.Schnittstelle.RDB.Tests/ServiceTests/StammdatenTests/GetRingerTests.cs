using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.StammdatenTests
{
    [TestFixture]
    public class GetRingerTests
    {
        private IApiStammdaten _apiStammdaten;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiStammdaten = new ServiceErsteller().GetService<IApiStammdaten>();
        }

        [Test]
        public void Call_erwarte_Erfolg()
        {
            Ringer ringer = _apiStammdaten.Get_Ringer_Async("1").Result;
            ringer.Should().NotBeNull();
        }

        [Test]
        public void Pass_113581_erwarte_korrekte_Daten()
        {
            Ringer ringer = _apiStammdaten.Get_Ringer_Async("11358").Result;
            ringer.Vorname.Should().Be("M."); //In Testdatenbank anonymisiert (Immer erster Buchstabe des Vornamens mit .)
            ringer.Nachname.Should().Be("Sakhi");
        }

        [Test]
        public void Pass_unbekannt_erwarte_NichtGefundenException()
        {
            Func<Task> act = async () => { await _apiStammdaten.Get_Ringer_Async("99999"); };
            act.Should().Throw<ApiNichtGefundenException>().WithMessage("Ringer mit Startausweisnummer 99999 konnte nicht gefunden werden.");
        }

        [Test]
        public void Pass_1_erwarte_korrekte_Daten()
        {
            Ringer ringer = _apiStammdaten.Get_Ringer_Async("1").Result;

            ringer.Vorname.Should().Be("Oliver");
            ringer.Nachname.Should().Be("Stach");
            ringer.Geburtsdatum.Should().Be(new DateTime(1966,5,20));
            ringer.Geschlecht.Should().Be(Geschlecht.Maennlich);
            ringer.Vereinsnummer.Should().Be("100079");
            ringer.Status.Should().Be("ND");
            ringer.Startausweisnummer.Should().Be("1");
            ringer.Lizenznummer.Should().BeNull();
        }
    }
}
