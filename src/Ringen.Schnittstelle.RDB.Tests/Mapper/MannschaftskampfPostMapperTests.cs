using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Tests.Mapper
{
    [TestFixture]
    public class MannschaftskampfPostMapperTests
    {
        private IApiMannschaftskaempfe _apiMannschaftskaempfe;
        private MannschaftskampfPostMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiMannschaftskaempfe = new ServiceErsteller().GetService<IApiMannschaftskaempfe>();
            _mapper = new ServiceErsteller().GetService<MannschaftskampfPostMapper>();
        }

        [Test]
        public void JsonString_2019_011008a_erwarte_korrektenJsonString()
        {
            Tuple<Mannschaftskampf, List<Einzelkampf>> wettkampf = _apiMannschaftskaempfe.Get_Mannschaftskampf_Async("2019", "011008a").Result;
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
