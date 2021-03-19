using System;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Exceptions;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class MannschaftskampfMapper
    {
        public Mannschaftskampf Map(CompetitionApiModel apiModel)
        {
            Mannschaftskampf result = new Mannschaftskampf
            {
                SaisonId = apiModel.SaisonId,
                WettkampfId = apiModel.CompetitionId,
                HeimMannschaft = apiModel.HomeTeamName,
                GastMannschaft = apiModel.OpponentTeamName,
                Wettkampfstaette = apiModel.Location,
                Kommentar = apiModel.EditorComment,
                Schiedsrichter_Vorname = apiModel.RefereeGivenname,
                Schiedsrichter_Nachname = apiModel.RefereeName,
                IstErgebnisGeprueft = !string.IsNullOrEmpty(apiModel.ControlledAt) && !string.IsNullOrEmpty(apiModel.ControlledBy)
            };
            
            try
            {
                result.Kampfdatum = DateTime.Parse(apiModel.BoutDate);
                result.GeplanterKampfbeginn = TimeSpan.Parse(apiModel.ScaleTime);
                
                result.EchterKampfbeginn = !string.IsNullOrEmpty(apiModel.StartTime) ? TimeSpan.Parse(apiModel.StartTime) : new TimeSpan(0, 0, 0);
                result.EchtesKampfende = !string.IsNullOrEmpty(apiModel.EndTime) ? TimeSpan.Parse(apiModel.EndTime) : new TimeSpan(0,0,0);
                result.AnzahlZuschauer = !string.IsNullOrEmpty(apiModel.Audience) ? int.Parse(apiModel.Audience) : 0;
            }
            catch (Exception ex)
            {
                throw new ApiMappingException("Wettkampfmapping Kampfdaten-Parsing", ex);
            }

            try
            {

                if (!string.IsNullOrEmpty(apiModel.HomePoints))
                {
                    result.HeimPunkte = int.Parse(apiModel.HomePoints);
                    if (!string.IsNullOrEmpty(apiModel.ValidatedHomePoints))
                    {
                        result.HeimPunkte = int.Parse(apiModel.ValidatedHomePoints);
                    }
                }

                if (!string.IsNullOrEmpty(apiModel.OpponentPoints))
                {
                    result.GastPunkte = int.Parse(apiModel.OpponentPoints);
                    if (!string.IsNullOrEmpty(apiModel.ValidatedOpponentPoints))
                    {
                        result.GastPunkte = int.Parse(apiModel.ValidatedOpponentPoints);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApiMappingException("Wettkampfmapping Mannschaftspunkte-Parsing", ex);
            }

            try
            {
                if (!string.IsNullOrEmpty(apiModel.Decision))
                {
                    if (apiModel.Decision.Equals("home", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Sieger = HeimGast.Heim;
                    }
                    else if (apiModel.Decision.Equals("opponent", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Sieger = HeimGast.Gast;
                    }
                    else
                    {
                        result.Sieger = HeimGast.Unbekannt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApiMappingException("Wettkampfmapping Sieger-Parsing", ex);
            }

            return result;
        }
    }
}
