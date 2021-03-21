using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.Konvertierer;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class EinzelkampfMapper
    {
        private StilartKonvertierer _stilartKonvertierer;
        private SiegartKonvertierer _siegartKonvertierer;
        private GriffbewertungspunktKonvertierer _griffbewertungspunktKonvertierer;

        public EinzelkampfMapper(StilartKonvertierer stilartKonvertierer, SiegartKonvertierer siegartKonvertierer, GriffbewertungspunktKonvertierer griffbewertungspunktKonvertierer)
        {
            _stilartKonvertierer = stilartKonvertierer;
            _siegartKonvertierer = siegartKonvertierer;
            _griffbewertungspunktKonvertierer = griffbewertungspunktKonvertierer;
        }

        public List<Einzelkampf> Map(JToken[] kaempfeJArray)
        {
            return kaempfeJArray.Select(kampfJToken => Map(kampfJToken)).ToList();
        }

        public Einzelkampf Map(JToken kampfJToken)
        {
            BoutApiModel apiModel = kampfJToken.ToObject<BoutApiModel>();

            if (kampfJToken["annotation"] != null)
            {
                var annotationApiModelListe = kampfJToken["annotation"]["1"].Select(li => li.FirstOrDefault().ToObject<AnnotationApiModel>()).ToList();
                apiModel.Annotations = annotationApiModelListe.ToList();
            }

            return Map(apiModel);
        }

        public EinzelkampfSchema Map(BoutSchemaApiModel apiModel)
        {
            var result = new EinzelkampfSchema
            {
                KampfNr = int.Parse(apiModel.Order),
                Gewichtsklasse = apiModel.WeightClass,
                Stilart = _stilartKonvertierer.ToEnum(apiModel.Style)
            };

            return result;
        }

        public Einzelkampf Map(BoutApiModel apiModel)
        {
            Einzelkampf result = new Einzelkampf
            {
                KampfNr = int.Parse(apiModel.Order),
                Gewichtsklasse = apiModel.WeightClass,
                Stilart = _stilartKonvertierer.ToEnum(apiModel.Style),
                HeimRinger = GetRinger(HeimGast.Heim, apiModel),
                GastRinger = GetRinger(HeimGast.Gast, apiModel),
                HeimMannschaftswertung = int.Parse(apiModel.HomeWrestlerPoints),
                GastMannschaftswertung = int.Parse(apiModel.OpponentWrestlerPoints),
                RundenErgebnisse = ErmittleRundenErgebnisse(apiModel),
                Siegart = _siegartKonvertierer.ToEnum(apiModel.Result),
                Kampfdauer = TimeSpan.FromSeconds(Convert.ToDouble(GetAnnotationValue(apiModel.Annotations, "duration"))),
                Kommentar = GetAnnotationValue(apiModel.Annotations, "comment")
            };

            var punkteString = GetAnnotationValue(apiModel.Annotations, "points");
            result.Wertungspunkte = _griffbewertungspunktKonvertierer.Ermittle_Griffbewertungspunkte(punkteString);

            return result;
        }

        private string GetAnnotationValue(List<AnnotationApiModel> annotationApiModelListe, string type)
        {
            var annotationApiModel = annotationApiModelListe?.FirstOrDefault(li => li.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            return annotationApiModel?.Value;
        }

        private Ringer GetRinger(HeimGast heimGast, BoutApiModel apiModel)
        {
            switch (heimGast)
            {
                case HeimGast.Unbekannt:
                    break;
                case HeimGast.Heim:
                    return new Ringer
                    {
                        Vorname = apiModel.HomeWrestlerGivenname,
                        Nachname = apiModel.HomeWrestlerName,
                        Status = apiModel.HomeWrestlerStatus,
                        Startausweisnummer = apiModel.HomeWrestlerPassCode,
                        Lizenznummer = apiModel.HomeWrestlerSaisonLicenceId
                    };
                case HeimGast.Gast:
                    return new Ringer
                    {
                        Vorname = apiModel.OpponentWrestlerGivenname,
                        Nachname = apiModel.OpponentWrestlerName,
                        Status = apiModel.OpponentWrestlerStatus,
                        Startausweisnummer = apiModel.OpponentWrestlerPassCode,
                        Lizenznummer = apiModel.OpponentWrestlerSaisonLicenceId
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(heimGast), heimGast, null);
            }

            throw new ArgumentException($"Ringer \"{heimGast}\" konnte nicht interpretiert werden");
        }


        private List<KeyValuePair<int, string>> ErmittleRundenErgebnisse(BoutApiModel apiModel)
        {
            var rundenProps = apiModel.GetType().GetProperties().Where(li => li.Name.StartsWith("Round", StringComparison.OrdinalIgnoreCase));

            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();
            foreach (var runde in rundenProps)
            {
                int nummer = int.Parse(runde.Name.Replace("Round", string.Empty));
                string wertung = runde.GetValue(apiModel).ToString().Trim();
                if (!string.IsNullOrEmpty(wertung))
                {
                    result.Add(new KeyValuePair<int, string>(nummer, wertung));
                }
            }

            return result;
        }
        
        public List<Einzelkampf> Map(IEnumerable<BoutApiModel> apiModelListe)
        {
            return apiModelListe.Select(apiModel => Map(apiModel)).ToList();
        }
    }
}
