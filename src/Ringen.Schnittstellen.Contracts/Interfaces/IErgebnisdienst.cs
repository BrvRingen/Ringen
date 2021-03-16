﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Interfaces
{
    public interface IErgebnisdienst
    {
        void Uebermittle_Ergebnis(string saisonId, string wettkampfId, Tuple<Mannschaftskampf, List<Einzelkampf>> mannschaftskampf);
    }
}
