using System;
using System.Collections.Generic;
using System.Linq;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class KampftagMapper
    {
        public Kampftag Map(BoutdayApiModel apiModel)
        {
            var result = new Kampftag()
            {
                SaisonId = apiModel.SaisonId,
                Datum = DateTime.Parse(apiModel.BoutDate),
                KampftagNummer = int.Parse(apiModel.OrgBoutday)
            };

            return result;
        }


        public List<Kampftag> Map(IEnumerable<BoutdayApiModel> apiModelListe)
        {
            return apiModelListe.Select(apiModel => Map(apiModel)).ToList();
        }
    }
}
