using Ringen.Schnittstelle.Caching.ConfigSections;

namespace Ringen.Schnittstelle.Caching.Models
{
    public class CacheZeiten
    {
        public int EinzelkampfInTagen { get; set; }
        public int MannschaftskampfInTagen { get; set; }

        public int MannschaftskaempfeInTagen { get; set; }

        public int LigaMitPlatzierungInTagen { get; set; }

        public int MannschaftenInTagen { get; set; }

        public int RingerInTagen { get; set; }

        public int MannschaftskampfSchemaInTagen { get; set; }

        public int LigenInTagen { get; set; }

        public int SaisonsInTagen { get; set; }

        public int SaisonInTagen { get; set; }

        public int KampftageInTagen { get; set; }
        
        public CacheZeiten()
        {
            
        }

        public CacheZeiten(ApiCacheConfigSection configSection)
        {
            EinzelkampfInTagen = configSection.Einzelkampf.CacheTage;
            MannschaftskampfInTagen = configSection.Mannschaftskampf.CacheTage;
            MannschaftskaempfeInTagen = configSection.Mannschaftskaempfe.CacheTage;
            LigaMitPlatzierungInTagen = configSection.LigaMitPlatzierung.CacheTage;
            MannschaftenInTagen = configSection.Mannschaften.CacheTage;
            RingerInTagen = configSection.Ringer.CacheTage;

            MannschaftskampfSchemaInTagen = configSection.MannschaftskampfSchema.CacheTage;
            LigenInTagen = configSection.Ligen.CacheTage;
            SaisonsInTagen = configSection.Saisons.CacheTage;
            SaisonInTagen = configSection.Saison.CacheTage;
            KampftageInTagen = configSection.Kampftage.CacheTage;
        }
    }
}
