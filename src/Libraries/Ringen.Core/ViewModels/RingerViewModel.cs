using System;
using System.Threading.Tasks;
using Ringen.Core.DependencyInjection;
using Ringen.Core.Services.ErgebnisdienstApi;
using Ringen.Core.UI;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class RingerViewModel : ExtendedNotifyPropertyChanged
    {
        public RingerViewModel(string vorname, string nachname, string status, string startausweisnummer, DateTime geburtsdatum)
        {
            _vorname = vorname;
            _nachname = nachname;
            _status = status;
            _startausweisnummer = startausweisnummer;
            _geburtsdatum = geburtsdatum;
        }

        private string _vorname;
        public string Vorname
        {
            get => _vorname;
            set => Set(ref _vorname, value);
        }

        private string _nachname;
        public string Nachname
        {
            get => _nachname;
            set
            {
                Set(ref _nachname, value);
            }
        }

        private string _status;
        public string? Status { get; set; }

        private string _startausweisnummer;
        public string Startausweisnummer
        {
            get
            {
                return _startausweisnummer;
            }
            set
            {
                Setze_Daten_aus_Startausweisnummer(value);
                Set(ref _startausweisnummer, value);
            }
        }

        private DateTime _geburtsdatum;
        public DateTime Geburtsdatum { get; set; }

        public async Task Setze_Daten_aus_Startausweisnummer(string startausweisnummer)
        {
            var ringer = await DependencyInjectionContainer.GetService<StammdatenService>().Get_und_Map_Ringer_Async(startausweisnummer);
            Vorname = ringer.Vorname;
            Nachname = ringer.Nachname;
            Status = ringer.Status;
            Geburtsdatum = ringer.Geburtsdatum;
        }
    }
}
