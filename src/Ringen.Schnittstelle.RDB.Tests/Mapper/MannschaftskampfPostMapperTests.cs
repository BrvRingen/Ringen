using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Ringen.DependencyInjection;
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
            _mannschaftskaempfe = DependencyInjectionContainer.GetService<IMannschaftskaempfe>();
            _mapper = DependencyInjectionContainer.GetService<MannschaftskampfPostMapper>();
        }

        [Test]
        public void JsonString_2019_011008a_erwarte_korrektenJsonString()
        {
            Tuple<Mannschaftskampf, List<Einzelkampf>> wettkampf = _mannschaftskaempfe.GetMannschaftskampf("2019", "011008a");
            wettkampf.Item1.EchterKampfbeginn = new TimeSpan(19,30,0);
            wettkampf.Item1.EchtesKampfende = new TimeSpan(21, 23, 0);

            var postApiModel = _mapper.Map(wettkampf.Item1, wettkampf.Item2);

            var jsonStringGeneriert = JsonConvert.SerializeObject(postApiModel, Formatting.Indented);
#if DEBUG
            string istPfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "POST_2019_011008a_IST.json");
            File.WriteAllText(istPfad, jsonStringGeneriert);
#endif

            string erwartetPfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "POST_2019_011008a.json");
            string erwartet = File.ReadAllText(erwartetPfad);

            jsonStringGeneriert.Should().Be(erwartet);
        }
    }
}
