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

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.SaisonInformationenTests
{
    [TestFixture]
    public class GetSaison
    {
        private ISaisonInformationen _saisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var rdbService = RdbServiceErsteller.ErstelleService();
            _saisonInformationen = new SaisonInformationen(rdbService);
        }


        [Test]
        public void GetSaison_erwarte_Erfolg()
        {
            var saisonListe = _saisonInformationen.GetSaisons();

            saisonListe.Should().NotBeNull();
            saisonListe.Count.Should().BeGreaterThan(0);

            saisonListe.FirstOrDefault(li => li.SaisonId.Equals("2020")).Should().NotBeNull();
        }
    }
}
