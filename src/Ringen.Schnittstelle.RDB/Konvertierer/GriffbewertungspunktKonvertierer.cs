using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    internal class GriffbewertungspunktKonvertierer
    {
        public string ToPunkteString(List<Griffbewertungspunkt> griffbewertungspunkte)
        {
            List<string> punkteStrings = new List<string>();

            foreach (var griffbewertungspunkt in griffbewertungspunkte)
            {
                //TODO: Punkte String erstellen
                //"PR62,AR97,1B128,4B171,2B176,2B226,2B237,2B241,2B255"
                punkteStrings.Add("");
            }

            return string.Join(",", punkteStrings);
        }

        public List<Griffbewertungspunkt> Ermittle_Griffbewertungspunkte(string punkteString)
        {
            if (string.IsNullOrEmpty(punkteString))
            {
                return new List<Griffbewertungspunkt>();
            }

            var griffbewertungspunkte = new List<Griffbewertungspunkt>();
            foreach (var punktString in punkteString.Split(','))
            {
                var temp = new Regex(@"(?<value>.*)(?<Wrestler>[R|B])(?<Time>\d*)").Match(punktString.ToUpper());

                var punkt = new Griffbewertungspunkt
                {
                    Fuer = temp.Groups["Wrestler"].Value.ToUpper() == "R" ? HeimGast.Heim : HeimGast.Gast,
                    Typ = GriffbewertungsTyp.Punkt,
                    Zeit = TimeSpan.FromSeconds(int.Parse(temp.Groups["Time"].Value))
                };

                switch (temp.Groups["value"].Value.ToUpper())
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
                            throw new ArgumentException(
                                $"Griffbewertungs-Typ für {temp?.Groups["value"]?.Value} konnte nicht ermittelt werden");
                        }

                        punkt.Typ = GriffbewertungsTyp.Punkt;
                        punkt.Punktzahl = punktzahl;
                        break;
                }

                griffbewertungspunkte.Add(punkt);
            }

            return griffbewertungspunkte;
        }
    }
}
