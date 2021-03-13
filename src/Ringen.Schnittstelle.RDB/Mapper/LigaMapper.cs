using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class LigaMapper
    {
        public Liga Map(LigaApiModel apiModel)
        {
            var result = new Liga
            {
                TabellenId = apiModel.TableId,
                LigaId = apiModel.LigaId,
                Bezeichnung = $"{apiModel.LigaId}{(!string.IsNullOrEmpty(apiModel.TableId) ? $" {apiModel.TableId}" : string.Empty)} {apiModel.SaisonId}"
            };

            return result;
        }
    }
}
