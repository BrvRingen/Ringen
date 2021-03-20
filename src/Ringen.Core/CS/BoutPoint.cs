using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.ViewModels;

namespace Ringen.Core.CS
{
    public class BoutPoint
    {
        public EinzelkampfViewModel EinzelkampfViewModel { get; set; }
        public Wrestler? HomeOrOpponent { get; set; }
        public string Value { get; set; }
        public int? Time { get; set; }
        public DateTime? Zeit { get; set; }

        public enum Wrestler { Home, Opponent };

        public BoutPoint(string value, EinzelkampfViewModel einzelkampfViewModel, Wrestler? homeOrOpponent = null, DateTime? zeit = null)
        {
            this.EinzelkampfViewModel = einzelkampfViewModel;
            this.HomeOrOpponent = homeOrOpponent;
            this.Value = value;
            Time = einzelkampfViewModel?.Settings.Times[BoutTime.Types.Bout.ToString()].Time;
            Zeit = zeit;
        }

        /// <summary>
        /// Constructor für Abruf aus Homepage.
        /// 
        /// TODO: Übergabe der Zeit eines Punktes aus REST.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="einzelkampfViewModel"></param>
        /// <param name="HomeOrOpponent"></param>
        /// <param name="Time"></param>
        public BoutPoint(string Value, EinzelkampfViewModel einzelkampfViewModel, Wrestler? HomeOrOpponent, int Time)
        {
            this.EinzelkampfViewModel = einzelkampfViewModel;
            this.HomeOrOpponent = HomeOrOpponent;
            this.Value = Value;
            this.Time = Time;
            Zeit = null;
        }

        public BoutPoint(string value, EinzelkampfViewModel einzelkampfViewModel, Wrestler? homeOrOpponent = null) : this(value, einzelkampfViewModel, homeOrOpponent, DateTime.Now)
        {
        }
    }
}
