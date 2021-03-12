﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Interfaces
{
    /// <summary>
    /// Saison (2020)
    /// > Klasse (Männer | Schlüler)
    /// > Liga (Oberliga | Bayernliga)
    /// > Tabelle (Nord | Süd)
    /// > Wettkampf (RCA vs Hof)
    /// > Einzelkampf (Müller vs. Hauser)
    /// > Ringer
    /// </summary>
    public interface IErgebnisdienst
    {
        Ringer GetRinger(string startausweisNr, string saisonId);

        Einzelkampf GetEinzelkampf(string saisonId, string wettkampfId, int kampfNr);

        Tuple<Mannschaftskampf, List<Einzelkampf>> GetMannschaftskampf(string saisonId, string wettkampfId);

        List<Mannschaftskampf> GetMannschaftskaempfe(string saisonId, string ligaId, string tableId);

        Tuple<Liga, List<Tabellenplatzierung>> GetLigaMitPlatzierung(string saisonId, string ligaId, string tableId);

        List<Liga> GetLigen(string saisonId);

        Tuple<Saison, Leistungsklasse> GetSaison(string saisonId);

        List<Saison> GetSaisons();

        List<Mannschaft> GetMannschaften(string saisonId, string ligaId, string tableId);
    }
}