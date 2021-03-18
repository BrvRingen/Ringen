﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstelle.RDB.Mapper;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Interfaces;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Shared.Helpers;

namespace Ringen.Schnittstelle.RDB.Services
{
    public class Ergebnisdienst : IErgebnisdienst
    {
        private RdbService _rdbService;
        private MannschaftskampfPostMapper _mapper;

        public Ergebnisdienst(RdbService rdbService, MannschaftskampfPostMapper mapper)
        {
            _rdbService = rdbService;
            _mapper = mapper;
        }

        public async Task UebermittleErgebnisAsync(Mannschaftskampf mannschaftskampf, List<Einzelkampf> einzelkaempfe)
        {
            CompetitionPostApiModel apiModel = _mapper.Map(mannschaftskampf, einzelkaempfe);

            List<ValidationResult> validationResults=new List<ValidationResult>();
            bool isValid = ValidationHelper.IsValidate(apiModel, fehlerListe => validationResults = fehlerListe);
            if (!isValid)
            {
                List<KeyValuePair<string, string>> validierungsFehler = new List<KeyValuePair<string, string>>();
                foreach (var validationResult in validationResults)
                {
                    validierungsFehler.AddRange(validationResult.MemberNames.Select(member =>
                        new KeyValuePair<string, string>(member, validationResult.ErrorMessage)));
                }

                throw new ApiValidierungException(validierungsFehler);
            }

            //TODO: Impl. finalisieren
            var httpResponse = await _rdbService.Sende_Ergebnis_Async(apiModel: apiModel);
        }
    }
}
