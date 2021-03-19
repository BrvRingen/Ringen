using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Shared
{
    public class GlobaleVariablen
    {
        public const string KonfigSectionName = "ringen";

        //TODO: Info Konfigdatei auslagern
        public static ErgebnisdienstSystem AktivesSystem = ErgebnisdienstSystem.RDB;

        //TODO: Info Konfigdatei auslagern
        public static bool IstApiCaching = true;
    }
}
