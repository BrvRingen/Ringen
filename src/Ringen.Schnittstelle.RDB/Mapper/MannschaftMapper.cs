using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        public List<Mannschaft> Map(IEnumerable<TeamApiModel> apiModelListe)
        {
            return apiModelListe.Select(apiModel => Map(apiModel)).ToList();
        }
    }
}
