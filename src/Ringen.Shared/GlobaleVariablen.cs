using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Shared
{
    public class GlobaleVariablen
    {
        public const string KonfigSectionName = "ringen";

        //TODO: Info Konfigdatei auslagern
        public static ErgebnisdienstSystem AktiveApiSchnittstelle = ErgebnisdienstSystem.RDB;
    }
}
