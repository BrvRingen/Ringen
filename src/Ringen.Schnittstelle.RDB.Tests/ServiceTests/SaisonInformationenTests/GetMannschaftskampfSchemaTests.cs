using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Ringen.Schnittstelle.RDB.Factories;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Tests.ServiceTests.SaisonInformationenTests
{
    [TestFixture]
    public class GetMannschaftskampfSchemaTests
    {
        private IApiSaisonInformationen _apiSaisonInformationen;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiSaisonInformationen = new ServiceErsteller().GetService<IApiSaisonInformationen>();
        }

        [Test]
        public void Call_erwarte_Erfolg()
        {
            List<EinzelkampfSchema> kampfSchema = _apiSaisonInformationen.Get_MannschaftskampfSchema_Async("2019", "011008a").Result;
            kampfSchema.Should().NotBeNull();
            kampfSchema.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Abgeschlossene_Saison_erwarte_korrekte_Ergebnisse()
        {
            List<EinzelkampfSchema> kampfSchema = _apiSaisonInformationen.Get_MannschaftskampfSchema_Async("2019", "011008a").Result;
            kampfSchema.Should().NotBeNull();
            kampfSchema.Count.Should().BeGreaterThan(0);

            Validiere_Einfachrunde(kampfSchema);
        }

        private void Validiere_Einfachrunde(List<EinzelkampfSchema> kampfSchema)
        {
            for (int i = 1; i <= 10; i++)
            {
                kampfSchema[i - 1].KampfNr.Should().Be(i);
            }

            kampfSchema[0].Gewichtsklasse.Should().Be("57");
            kampfSchema[0].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[1].Gewichtsklasse.Should().Be("61");
            kampfSchema[1].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[2].Gewichtsklasse.Should().Be("66");
            kampfSchema[2].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[3].Gewichtsklasse.Should().Be("71");
            kampfSchema[3].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[4].Gewichtsklasse.Should().Be("75 A");
            kampfSchema[4].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[5].Gewichtsklasse.Should().Be("75 B");
            kampfSchema[5].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[6].Gewichtsklasse.Should().Be("80");
            kampfSchema[6].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[7].Gewichtsklasse.Should().Be("86");
            kampfSchema[7].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[8].Gewichtsklasse.Should().Be("98");
            kampfSchema[8].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[9].Gewichtsklasse.Should().Be("130");
            kampfSchema[9].Stilart.Should().Be(Stilart.GriechischRoemisch);
        }

        [Test]
        public void Abgeschlossene_Saison_Doppelrunde_erwarte_korrekte_Ergebnisse()
        {
            List<EinzelkampfSchema> kampfSchema = _apiSaisonInformationen.Get_MannschaftskampfSchema_Async("2019", "006028e").Result;
            kampfSchema.Should().NotBeNull();
            kampfSchema.Count.Should().BeGreaterThan(0);

            Validiere_Doppelrunde(kampfSchema);
        }

        [Test]
        public void Offene_Saison_erwarte_korrekte_Ergebnisse()
        {
            List<EinzelkampfSchema> kampfSchema = _apiSaisonInformationen.Get_MannschaftskampfSchema_Async("2020", "013003b").Result;
            kampfSchema.Should().NotBeNull();
            kampfSchema.Count.Should().BeGreaterThan(0);

            Validiere_Einfachrunde(kampfSchema);
        }

        [Ignore("Noch keine Testdaten im Testsystem vorhanden")]
        [Test]
        public void Offene_Saison_Doppelrunde_erwarte_korrekte_Ergebnisse()
        {
            List<EinzelkampfSchema> kampfSchema = _apiSaisonInformationen.Get_MannschaftskampfSchema_Async("2020", "").Result; //TODO: cid sobald in Testsystem vorhanden
            kampfSchema.Should().NotBeNull();
            kampfSchema.Count.Should().BeGreaterThan(0);

            Validiere_Doppelrunde(kampfSchema);
        }

        private void Validiere_Doppelrunde(List<EinzelkampfSchema> kampfSchema)
        {
            for (int i = 1; i <= 14; i++)
            {
                kampfSchema[i - 1].KampfNr.Should().Be(i);
            }

            kampfSchema[0].Gewichtsklasse.Should().Be("57 A");
            kampfSchema[0].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[1].Gewichtsklasse.Should().Be("61 A");
            kampfSchema[1].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[2].Gewichtsklasse.Should().Be("66 A");
            kampfSchema[2].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[3].Gewichtsklasse.Should().Be("75 A");
            kampfSchema[3].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[4].Gewichtsklasse.Should().Be("86 A");
            kampfSchema[4].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[5].Gewichtsklasse.Should().Be("98 A");
            kampfSchema[5].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[6].Gewichtsklasse.Should().Be("130 A");
            kampfSchema[6].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[7].Gewichtsklasse.Should().Be("57 B");
            kampfSchema[7].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[8].Gewichtsklasse.Should().Be("61 B");
            kampfSchema[8].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[9].Gewichtsklasse.Should().Be("66 B");
            kampfSchema[9].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[10].Gewichtsklasse.Should().Be("75 B");
            kampfSchema[10].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[11].Gewichtsklasse.Should().Be("86 B");
            kampfSchema[11].Stilart.Should().Be(Stilart.GriechischRoemisch);

            kampfSchema[12].Gewichtsklasse.Should().Be("98 B");
            kampfSchema[12].Stilart.Should().Be(Stilart.Freistil);

            kampfSchema[13].Gewichtsklasse.Should().Be("130 B");
            kampfSchema[13].Stilart.Should().Be(Stilart.GriechischRoemisch);
        }
    }
}
