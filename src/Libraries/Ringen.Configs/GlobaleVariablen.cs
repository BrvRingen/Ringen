using System;
using System.Collections.Generic;
using System.Text;

namespace Ringen.Configs
{
    public enum ErgebnisdienstSystem
    {
        RDB
    }

    public class GlobaleVariablen
    {
        public const string KonfigSectionName = "ringen";

        //TODO: Info Konfigdatei auslagern
        public static ErgebnisdienstSystem AktiveApiSchnittstelle = ErgebnisdienstSystem.RDB;
    }
}
