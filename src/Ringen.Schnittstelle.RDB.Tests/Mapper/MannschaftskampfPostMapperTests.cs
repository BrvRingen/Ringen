using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstelle.RDB.Services;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Tests.Mapper
{
    [TestFixture]
    public class MannschaftskampfPostMapperTests
    {
        private IMannschaftskaempfe _mannschaftskaempfe;
        private MannschaftskampfPostMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RdbService rdbService = RdbServiceErsteller.ErstelleService();
            _mannschaftskaempfe = new Mannschaftskaempfe(rdbService);
            _mapper = new MannschaftskampfPostMapper();
        }

        [Test]
        public void Test()
        {
            Tuple<Mannschaftskampf, List<Einzelkampf>> wettkampf = _mannschaftskaempfe.GetMannschaftskampf("2019", "011008a");
            wettkampf.Item1.EchterKampfbeginn = new TimeSpan(19,30,0);
            wettkampf.Item1.EchtesKampfende = new TimeSpan(21, 23, 0);

            var postApiModel = _mapper.Map(wettkampf);

            var jsonString = JsonConvert.SerializeObject(postApiModel, Formatting.Indented);
        }
    }
}
