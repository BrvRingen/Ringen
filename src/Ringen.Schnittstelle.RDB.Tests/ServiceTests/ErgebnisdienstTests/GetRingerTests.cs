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

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.ErgebnisdienstTests
{
    [TestFixture]
    public class GetRingerTests
    {
        private IErgebnisdienst _ergebnisdienst;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RdbService rdbService = RdbServiceErsteller.ErstelleService();
            _ergebnisdienst = new Ergebnisdienst(rdbService);
        }

        [Test]
        public void Call_erwarte_Erfolg()
        {
            Ringer ringer = _ergebnisdienst.GetRinger("11358", "2019");
            ringer.Should().NotBeNull();
        }

        [Test]
        public void Pass_11358_erwarte_korrekte_Daten()
        {
            Ringer ringer = _ergebnisdienst.GetRinger("11358", "2019");
            ringer.Vorname.Should().Be("Matin");
            ringer.Nachname.Should().Be("Sakhi");
        }
    }
}
