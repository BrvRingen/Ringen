using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class SaisonMapper
    {
        public Saison Map(SaisonApiModel apiModel)
        {
            var result = new Saison
            {
                SaisonId = apiModel.SaisonId,
                Bezeichnung = apiModel.Description,
                Ligenleiter = apiModel.ControllerName
            };

            return result;
        }
    }
}
