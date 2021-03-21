using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.Properties;
using Ringen.Core.ViewModels;
using Ringen.Core.ViewModels.Enums;

namespace Ringen.Core.CS
{
    public class BoutPoint
    {
        public HeimGastViewModel? HomeOrOpponent { get; set; }
        public string Value { get; set; }
        public int? Time { get; set; }
        public DateTime? Zeit { get; set; }


        public BoutPoint(string value, HeimGastViewModel? homeOrOpponent = null, DateTime? zeit = null)
        {
            this.HomeOrOpponent = homeOrOpponent;
            this.Value = value;
            //Time = Settings.Times[Types.Bout.ToString()].Time;
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
        public BoutPoint(string Value, HeimGastViewModel? HomeOrOpponent, int Time)
        {
            this.HomeOrOpponent = HomeOrOpponent;
            this.Value = Value;
            this.Time = Time;
            Zeit = null;
        }

        public BoutPoint(string value, HeimGastViewModel? homeOrOpponent = null) : this(value, homeOrOpponent, DateTime.Now)
        {
        }
    }
}
