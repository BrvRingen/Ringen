using System;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class LigaMapper
    {
        public Liga Map(LigaApiModel apiModel)
        {
            var result = new Liga
            {
                SaisonId = apiModel.SaisonId,
                TabellenId = apiModel.TableId,
                LigaId = apiModel.LigaId,
                Bezeichnung = $"{apiModel.LigaId}{(!string.IsNullOrEmpty(apiModel.TableId) ? $" {apiModel.TableId}" : string.Empty)} {apiModel.SaisonId}"
            };

            switch (apiModel.Type.ToLower())
            {
                case "utable":
                    result.Austragungsmodus = Austragungsmodus.HinRueckRunde;
                    break;

                case "wtable":
                    result.Austragungsmodus = Austragungsmodus.Doppelrunde;
                    break;

                case "amorphous":
                    result.Austragungsmodus = Austragungsmodus.KOSystem;
                    break;

                case "notable":
                    result.Austragungsmodus = Austragungsmodus.NoTable;
                    break;

                default:
                    throw new ArgumentException($"Austragungsmodus {apiModel.Type} konnte nicht ermittelt werden");
            }

            return result;
        }
    }
}
