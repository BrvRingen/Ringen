using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Schnittstelle.RDB.Konvertierer
{
    public class GriffbewertungsTypKonvertierer : KonvertiererBase<GriffbewertungsTyp>
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
