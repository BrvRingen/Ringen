using System.Diagnostics;
using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Ringen.Plugin.CsEditor.Reporting;

namespace Ringen.Plugin.CsEditor.Tests
{
    [TestFixture]
    public class ProtocolPdfGenerierungTests
    {
        [Test]
        public void TestePdfExport()
        {
            string daten = File.ReadAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}\\TestDaten\\2019_Landesliga_RCA-Bayreuth_vs_ASV-Hof.json");
            JObject competitionData = JObject.Parse(daten);


            var testCompetition = new Ringen.Core.CS.Competition((JObject) competitionData, null);

            string filename = "Test.pdf";

            IReport bericht = new ReportPdf();
            bericht.Export(filename, testCompetition);
            Process.Start(filename);//Öffne PDF

        }


        [Test]
        public void Test2()
        {
            var x = 1 + 1;
            Assert.That(x, Is.EqualTo(2));
        }
    }
}
