using System.Collections.Generic;
using System.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class MannschaftMapper
    {
        public Mannschaft Map(TeamApiModel apiModel)
        {
            var result = new Mannschaft
            {
                TeamId = apiModel.TeamId,
                Kurzname = apiModel.TeamName,
                Vereinsnummer = apiModel.ClubCode,
                Langname = apiModel.ClubName
            };

            return result;
        }

        public Mannschaft Map(ClubApiModel apiModel)
        {
            var result = new Mannschaft
            {
                TeamId = apiModel.Id,
                Kurzname = apiModel.Shortname,
                Vereinsnummer = apiModel.ClubCode,
                Langname = apiModel.Name
            };

            return result;
        }

        public List<Mannschaft> Map(IEnumerable<TeamApiModel> apiModelListe)
        {
            return apiModelListe.Select(apiModel => Map(apiModel)).ToList();
        }

        public List<Mannschaft> Map(IEnumerable<ClubApiModel> apiModelListe)
        {
            return apiModelListe.Select(apiModel => Map(apiModel)).ToList();
        }
    }
}
