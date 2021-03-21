using System.Collections.Generic;
using System.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.ApiModels.Post;
using Ringen.Schnittstelle.RDB.Konvertierer;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class MannschaftskampfPostMapper
    {
        private StilartKonvertierer _stilartKonvertierer;
        private SiegartKonvertierer _siegartKonvertierer;
        private GriffbewertungspunktKonvertierer _griffbewertungspunktKonvertierer;

        public MannschaftskampfPostMapper(StilartKonvertierer stilartKonvertierer, SiegartKonvertierer siegartKonvertierer, GriffbewertungspunktKonvertierer griffbewertungspunktKonvertierer)
        {
            _stilartKonvertierer = stilartKonvertierer;
            _siegartKonvertierer = siegartKonvertierer;
            _griffbewertungspunktKonvertierer = griffbewertungspunktKonvertierer;
        }

        public CompetitionPostApiModel Map(Mannschaftskampf mannschaftskampf, List<Einzelkampf> einzelkaempfe)
        {
            CompetitionPostApiModel apiModel = new CompetitionPostApiModel
            {
                SaisonId = mannschaftskampf.SaisonId,
                CompetitionId = mannschaftskampf.WettkampfId,
                HomePoints = mannschaftskampf.HeimPunkte.ToString(),
                OpponentPoints = mannschaftskampf.GastPunkte.ToString(),
                Audience = mannschaftskampf.AnzahlZuschauer.ToString(),
                EditorComment = mannschaftskampf.Kommentar,
                RefereeName = mannschaftskampf.Schiedsrichter_Nachname,
                RefereeGivenname = mannschaftskampf.Schiedsrichter_Vorname,
                StartTime = mannschaftskampf.EchterKampfbeginn.ToString(@"hh\:mm\:ss"),
                EndTime = mannschaftskampf.EchtesKampfende.ToString(@"hh\:mm\:ss"),
                BoutList = MapEinzelkaempfe(einzelkaempfe)
            };
            
            return apiModel;
        }

        public List<BoutPostApiModel> MapEinzelkaempfe(List<Einzelkampf> einzelkaempfe)
        {
            return einzelkaempfe.Select(kampf => MapEinzelkaempf(kampf)).ToList();
        }

        public BoutPostApiModel MapEinzelkaempf(Einzelkampf einzelkampf)
        {
            BoutPostApiModel apiModel = new BoutPostApiModel
            {
                WeightClass = einzelkampf.Gewichtsklasse.Trim(),
                Style = _stilartKonvertierer.ToApiString(einzelkampf.Stilart),

                HomeWrestlerName = einzelkampf.HeimRinger.Nachname,
                HomeWrestlerGivenname = einzelkampf.HeimRinger.Vorname,
                HomeWrestlerRating = einzelkampf.HeimRinger.Status,
                HomeWrestlerPassCode = einzelkampf.HeimRinger.Startausweisnummer,
                HomeWrestlerPoints = einzelkampf.HeimMannschaftswertung.ToString(),

                OpponentWrestlerName = einzelkampf.GastRinger.Nachname,
                OpponentWrestlerGivenname = einzelkampf.GastRinger.Vorname,
                OpponentWrestlerRating = einzelkampf.GastRinger.Status,
                OpponentWrestlerPassCode = einzelkampf.GastRinger.Startausweisnummer,
                OpponentWrestlerPoints = einzelkampf.GastMannschaftswertung.ToString(),

                Result = _siegartKonvertierer.ToApiString(einzelkampf.Siegart),
                Round1 = einzelkampf.RundenErgebnisse.FirstOrDefault().Value.Trim(), //TODO: ggf. andere Runden-Mappings mit integrieren
                Round2 = string.Empty,
                Round3 = string.Empty,
                Round4 = string.Empty,
                Round5 = string.Empty,

                Annotations = new AnnotationsPostApiModel
                {
                    Points = new RoundValuePostApiModel(_griffbewertungspunktKonvertierer.ToApiString(einzelkampf.Wertungspunkte)),
                    Duration = new RoundValuePostApiModel(einzelkampf.Kampfdauer.TotalSeconds.ToString())
                }
            };

            return apiModel;
        }
    }
}
