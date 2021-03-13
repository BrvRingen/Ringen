using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.SaisonInformationenTests
{
    [TestFixture]
    public class GetMannschaftskampfSchema
    {
        private ISaisonInformationen _saisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var rdbService = RdbServiceErsteller.ErstelleService();
            _saisonInformationen = new SaisonInformationen(rdbService);
        }
    }
}
