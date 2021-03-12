using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstelle.RDB.ApiModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstelle.RDB.Mapper
{
    internal class LeistungsklasseMapper
    {
        public Leistungsklasse Map(SystemApiModel apiModel)
        {
            var result = new Leistungsklasse
            {
                SystemId = apiModel.SystemId,
                Bezeichnung = apiModel.Display
            };

            return result;
        }
    }
}
