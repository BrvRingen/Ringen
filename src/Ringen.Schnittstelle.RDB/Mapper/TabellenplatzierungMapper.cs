using System;
using System.Collections.Generic;
using System.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class TabellenplatzierungMapper
    {
        public Tabellenplatzierung Map(PlaceApiModel apiModel)
        {
            var result = new Tabellenplatzierung
            {
                TeamId = apiModel.TeamId,
            };

            try
            {
                result.Tabellenplatz = int.Parse(apiModel.Place);
                result.AnzahlSiege = int.Parse(apiModel.WonMatch);
                result.AnzahlNiederlagen = int.Parse(apiModel.LostMatch);
                result.AnzahlUnentschieden = int.Parse(apiModel.TieMatch);
                result.GesamtErkaempfteGriffbewertungspunkte = int.Parse(apiModel.WonBPoints);
                result.GesamtAbgegebeneGriffbewertungspunkte = int.Parse(apiModel.LostBPoints);
                result.PlusPunkte = int.Parse(apiModel.WonTPoints);
                result.MinusPunkte = int.Parse(apiModel.LostTPoints);
            }
            catch (Exception ex)
            {
                throw new ApiMappingException("Tabellenplatzierungmapping Punkte-Parsing", ex);
            }

            return result;
        }
        
        public List<Tabellenplatzierung> Map(IEnumerable<PlaceApiModel> apiModelListe)
        {
            return apiModelListe.Select(apiModel => Map(apiModel)).ToList();
        }
    }
}
