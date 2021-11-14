using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.ViewModels;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.Mapper
{
    public class RingerViewModelMapper
    {

        public RingerViewModel Map(Ringer ringer)
        {
            return new RingerViewModel(ringer.Vorname, ringer.Nachname, ringer.Status, ringer.Startausweisnummer, ringer.Geburtsdatum);

        }
    }
}
