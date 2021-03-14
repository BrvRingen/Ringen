using System;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.StammdatenTests
{
    [TestFixture]
    public class GetRingerTests
    {
        private IStammdaten _stammdaten;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RdbService rdbService = RdbServiceErsteller.ErstelleService();
            _stammdaten = new Stammdaten(rdbService);
        }

        [Test]
        public void Call_erwarte_Erfolg()
        {
            Ringer ringer = _stammdaten.GetRinger("1");
            ringer.Should().NotBeNull();
        }

        [Test]
        public void Pass_113581_erwarte_korrekte_Daten()
        {
            Ringer ringer = _stammdaten.GetRinger("11358");
            ringer.Vorname.Should().Be("Matin");
            ringer.Nachname.Should().Be("Sakhi");
        }

        [Test]
        public void Pass_unbekannt_erwarte_NichtGefundenException()
        {
            Action act = () => _stammdaten.GetRinger("99999");

            act.Should().Throw<ApiNichtGefundenException>().WithMessage("Ringer mit Startausweisnummer 99999 konnte nicht gefunden werden.");
        }

        [Test]
        public void Pass_1_erwarte_korrekte_Daten()
        {
            Ringer ringer = _stammdaten.GetRinger("1");

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
