using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class RingerMapper
    {
        public Ringer Map(WrestlerApiModel apiModel)
        {
            Ringer result = new Ringer()
            {
                //TODO: Ringer Mapping füllen, sobald API klar
            };
            return result;
        }
    }
}
