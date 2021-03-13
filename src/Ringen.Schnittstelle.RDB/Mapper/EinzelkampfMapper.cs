using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class EinzelkampfMapper
    {
        public List<Einzelkampf> Map(JToken[] kaempfeJArray)
        {
            return kaempfeJArray.Select(kampfJToken => Map(kampfJToken)).ToList();
        }

        public Einzelkampf Map(JToken kampfJToken)
        {
            var apiModel = kampfJToken.ToObject<BoutApiModel>();
            var annotationApiModelListe = kampfJToken["annotation"]["1"].Select(li => li.FirstOrDefault().ToObject<AnnotationApiModel>()).ToList();
            apiModel.Annotations = annotationApiModelListe.ToList();

            return Map(apiModel);
        }

        public EinzelkampfSchema Map(BoutSchemaApiModel apiModel)
        {
            var result = new EinzelkampfSchema
            {
                KampfNr = int.Parse(apiModel.Order),
                Gewichtsklasse = apiModel.WeightClass,
                Stilart = ErmittleStilart(apiModel.Style)
            };

            return result;
        }

        public Einzelkampf Map(BoutApiModel apiModel)
        {
            Einzelkampf result = new Einzelkampf
            {
                KampfNr = int.Parse(apiModel.Order),
                Gewichtsklasse = apiModel.WeightClass,
                Stilart = ErmittleStilart(apiModel.Style),
                HeimRinger = new Ringer(apiModel.HomeWrestlerGivenname, apiModel.HomeWrestlerName, apiModel.HomeWrestlerStatus, apiModel.HomeWrestlerPassCode, apiModel.HomeWrestlerSaisonLicenceId),
                GastRinger = new Ringer(apiModel.OpponentWrestlerGivenname, apiModel.OpponentWrestlerName, apiModel.OpponentWrestlerStatus, apiModel.OpponentWrestlerPassCode, apiModel.OpponentWrestlerSaisonLicenceId),
                HeimMannschaftswertung = int.Parse(apiModel.HomeWrestlerPoints),
                GastMannschaftswertung = int.Parse(apiModel.OpponentWrestlerPoints),
                RundenErgebnisse = ErmittleRundenErgebnisse(apiModel),
                Siegart = ErmittleSiegart(apiModel.Result),
                Kampfdauer = TimeSpan.FromSeconds(Convert.ToDouble(apiModel.Annotations.FirstOrDefault(li => li.Type.Equals("duration", StringComparison.OrdinalIgnoreCase)).Value)),
                Wertungspunkte = null,
                Kommentar = apiModel.Annotations.FirstOrDefault(li => li.Type.Equals("comment", StringComparison.OrdinalIgnoreCase)).Value
            };

            var punkteString = apiModel.Annotations.FirstOrDefault(li => li.Type.Equals("points", StringComparison.OrdinalIgnoreCase)).Value;

            var griffbewertungspunkte = new List<Griffbewertungspunkt>();
            foreach (var punktString in punkteString.Split(','))
            {
                var temp = new Regex(@"(?<value>.*)(?<Wrestler>[R|B])(?<Time>\d*)").Match(punktString.ToUpper());

                var punkt = new Griffbewertungspunkt
                {
                    Fuer = temp.Groups["Wrestler"].Value == "R" ? HeimGast.Heim : HeimGast.Gast,
                    Typ = GriffbewertungsTyp.Punkt,
                    Zeit = TimeSpan.FromSeconds(int.Parse(temp.Groups["Time"].Value)),
                    Punktzahl = 0
                };

                switch (temp.Groups["value"].Value)
                {
                    case "P":
                        punkt.Typ = GriffbewertungsTyp.Passiv;
                        punkt.Punktzahl = 0;
                        break;

                    case "A":
                        punkt.Typ = GriffbewertungsTyp.Aktivitaetszeit;
                        punkt.Punktzahl = 0;
                        break;

                    case "V":
                        punkt.Typ = GriffbewertungsTyp.Verwarnung;
                        punkt.Punktzahl = 0;
                        break;

                    default:
                        int punktzahl = 0;
                        if (!int.TryParse(temp.Groups["value"].Value, out punktzahl))
                        {
                            throw new ArgumentException($"Griffbewertungs-Typ für {temp?.Groups["value"]?.Value} konnte nicht ermittelt werden");
                        }
                        punkt.Typ = GriffbewertungsTyp.Punkt;
                        punkt.Punktzahl = punktzahl;
                        break;
                }

                griffbewertungspunkte.Add(punkt);
            }
            result.Wertungspunkte = griffbewertungspunkte;

            return result;
        }

        private Siegart ErmittleSiegart(string apiModelResult)
        {
            switch (apiModelResult)
            {
                case "TÜ":
                    return Siegart.TechnischUeberlegen;
                case "SS":
                    return Siegart.Schultersieg;
                case "PS":
                    return Siegart.Punktsieg;
                case "KL":
                    return Siegart.Kampflos;
                case "ÜG":
                    return Siegart.Uebergewicht;

                default:
                    throw new ArgumentException($"Siegart \"{apiModelResult}\" konnte nicht interpretiert werden");
                    break;
            }

            throw new ArgumentException($"Siegart \"{apiModelResult}\" konnte nicht interpretiert werden");
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

        private Stilart ErmittleStilart(string apiModelStyle)
        {
            if (apiModelStyle.Equals("LL", StringComparison.OrdinalIgnoreCase))
            {
                return Stilart.Freistil;
            } 
            else if (apiModelStyle.Equals("GR", StringComparison.OrdinalIgnoreCase))
            {
                return Stilart.GriechischRoemisch;
            }

            throw new ArgumentException($"Stilart \"{apiModelStyle}\" konnte nicht interpretiert werden");
        }

        public List<Einzelkampf> Map(IEnumerable<BoutApiModel> apiModelListe)
        {
            return apiModelListe.Select(apiModel => Map(apiModel)).ToList();
        }
    }
}
