using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.RDB.Services
{
    internal class ApiStammdaten : IApiStammdaten
    {
        private RdbService _rdbService;

        public ApiStammdaten(RdbService rdbService)
        {
            _rdbService = rdbService;
        }

        public async Task<Ringer> Get_Ringer_Async(string startausweisNr)
        {
            RingerMapper mapper = new RingerMapper();

            JObject response = await _rdbService.Get_CompetitionSystem_Async(
                "getSaisonWrestler",
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("passcode", startausweisNr),
                });

            WrestlerApiModel apiModel = response["wrestler"].ToObject<WrestlerApiModel>();

            if (apiModel == null)
            {
                throw new ApiNichtGefundenException($"Ringer mit Startausweisnummer {startausweisNr} konnte nicht gefunden werden.");
            }

            return mapper.Map(apiModel);
        }

        public async Task<List<Mannschaft>> Get_Mannschaften_Async()
        {
            MannschaftMapper mapper = new MannschaftMapper();

            JObject response = await _rdbService.Get_Organisationsmanager_Async("getAuthClubList");

            IEnumerable<ClubApiModel> apiModelListe = response["clubMap"].Select(elem => elem.FirstOrDefault().ToObject<ClubApiModel>());

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
        }
    }
}
