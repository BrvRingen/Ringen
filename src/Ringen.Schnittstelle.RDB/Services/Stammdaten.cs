using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Services
{
    public class Stammdaten : IStammdaten
    {
        private RdbService _rdbService;

        public Stammdaten(RdbService rdbService)
        {
            _rdbService = rdbService;
        }

        public Ringer GetRinger(string startausweisNr)
        {
            RingerMapper mapper = new RingerMapper();

            JObject response = _rdbService.GetCompetitionSystem(
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

        public List<Mannschaft> GetMannschaften()
        {
            MannschaftMapper mapper = new MannschaftMapper();

            JObject response = _rdbService.GetOrganisationsmanager("getAuthClubList");

            IEnumerable<ClubApiModel> apiModelListe = response["clubMap"].Select(elem => elem.FirstOrDefault().ToObject<ClubApiModel>());

            return apiModelListe.Select(apiModel => mapper.Map(apiModel)).ToList();
        }
    }
}
