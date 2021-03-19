using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    internal class GriffbewertungspunktKonvertierer
    {
        private HeimGastKonvertierer _heimGastKonvertierer;
        private GriffbewertungsTypKonvertierer _griffbewertungsTypKonvertierer;

        public GriffbewertungspunktKonvertierer(HeimGastKonvertierer heimGastKonvertierer, GriffbewertungsTypKonvertierer griffbewertungsTypKonvertierer)
        {
            _heimGastKonvertierer = heimGastKonvertierer;
            _griffbewertungsTypKonvertierer = griffbewertungsTypKonvertierer;
        }

        public string ToApiString(List<Griffbewertungspunkt> griffbewertungspunkte)
        {
            List<string> punkteStrings = new List<string>();

            foreach (var griffbewertungspunkt in griffbewertungspunkte)
            {
                /*
                 * Grammatik
                 * <<point-data>> ::= <token> (WS <token>)::* ;
                    <token> ::= (<score> | <pause>) ; 
                    <score> ::= <color><grade><timeInSec> ; 
                    <color> ::= („R“|“B“) ; 
                    <grade> ::= <points>|<decision> ; 
                    <poin [0-9]{1,3} ;ts> ::= [1-5] ;
                    <decision> ::= [APV0O] ; 
                    <timeInSec> ::=
                    <pause> ::= „#“ ;;
                 */
                string grade = _griffbewertungsTypKonvertierer.ToApiString(griffbewertungspunkt.Typ);
                string point = griffbewertungspunkt.Punktzahl.ToString();
                if (griffbewertungspunkt.Typ.Equals(GriffbewertungsTyp.Punkt))
                {
                    grade = point;
                }

                string color = _heimGastKonvertierer.ToApiString(griffbewertungspunkt.Fuer);
                string timeInSec = griffbewertungspunkt.Zeit.TotalSeconds.ToString();

                string token = $"{grade}{color}{timeInSec}";

                punkteStrings.Add(token);
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
                    Fuer = _heimGastKonvertierer.ToEnum(temp.Groups["Wrestler"].Value),
                    Zeit = TimeSpan.FromSeconds(int.Parse(temp.Groups["Time"].Value)),
                    Typ = _griffbewertungsTypKonvertierer.ToEnum(temp.Groups["value"].Value),
                    Punktzahl = 0
                };

                if (punkt.Typ.Equals(GriffbewertungsTyp.Punkt))
                {
                    int punktzahl = 0;
                    if (!int.TryParse(temp.Groups["value"].Value, out punktzahl))
                    {
                        throw new ArgumentException($"Griffbewertungs-Typ für {temp?.Groups["value"]?.Value} konnte nicht ermittelt werden");
                    }
                    punkt.Punktzahl = punktzahl;
                }

                griffbewertungspunkte.Add(punkt);
            }

            return griffbewertungspunkte;
        }
    }
}
