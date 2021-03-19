using System.Collections.Generic;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    internal class GriffbewertungsTypKonvertierer : KonvertiererBase<GriffbewertungsTyp>
    {
        protected override Dictionary<string, GriffbewertungsTyp> MappingDictionary { get; } = new Dictionary<string, GriffbewertungsTyp>()
        {
            {"V", GriffbewertungsTyp.Verwarnung },
            {"0", GriffbewertungsTyp.Verwarnung },
            {"O", GriffbewertungsTyp.Verwarnung },

            {"P", GriffbewertungsTyp.Passiv },
            {"A", GriffbewertungsTyp.Aktivitaetszeit },
        };
    }
}
