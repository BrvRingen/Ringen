using System;
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

        public void Uebermittle_Ergebnis(Mannschaftskampf mannschaftskampf, List<Einzelkampf> einzelkaempfe)
        {
            CompetitionPostApiModel apiModel = _mapper.Map(mannschaftskampf, einzelkaempfe);

            List<KeyValuePair<string, string>> validierungsFehler = new List<KeyValuePair<string, string>>();
            bool isValid = Validiere(apiModel, fehlerListe => validierungsFehler = fehlerListe);
            if (!isValid)
            {
                throw new ApiValidierungException(validierungsFehler);
            }

            //TODO: Impl. finalisieren
            _rdbService.Sende_Ergebnis(
                apiModel: apiModel, 
                onAbgeschlossen: httpResult => {

                }
            );
        }

        private bool Validiere(CompetitionPostApiModel apiModel, Action<List<KeyValuePair<string, string>>> onValidierungsFehler)
        {
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(apiModel, context, results, validateAllProperties: true);
            if (!isValid)
            {
                List<KeyValuePair<string, string>> validierungsFehler = new List<KeyValuePair<string, string>>();
                foreach (var validationResult in results)
                {
                    validierungsFehler.AddRange(validationResult.MemberNames.Select(member =>
                        new KeyValuePair<string, string>(member, validationResult.ErrorMessage)));
                }

                onValidierungsFehler(validierungsFehler);
            }

            return isValid;
        }
    }
}
