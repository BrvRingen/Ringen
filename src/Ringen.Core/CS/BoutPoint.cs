using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core.CS
{
    public class BoutPoint
    {
        public Bout Bout { get; set; }
        public Wrestler? HomeOrOpponent { get; set; }
        public string Value { get; set; }
        public int? Time { get; set; }
        public DateTime? Zeit { get; set; }

        public enum Wrestler { Home, Opponent };

        public BoutPoint(string value, Bout bout, Wrestler? homeOrOpponent = null, DateTime? zeit = null)
        {
            this.Bout = bout;
            this.HomeOrOpponent = homeOrOpponent;
            this.Value = value;
            Time = bout?.Settings.Times[BoutTime.Types.Bout.ToString()].Time;
            Zeit = zeit;
        }

        /// <summary>
        /// Constructor für Abruf aus Homepage.
        /// 
        /// TODO: Übergabe der Zeit eines Punktes aus REST.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Bout"></param>
        /// <param name="HomeOrOpponent"></param>
        /// <param name="Time"></param>
        public BoutPoint(string Value, Bout Bout, Wrestler? HomeOrOpponent, int Time)
        {
            this.Bout = Bout;
            this.HomeOrOpponent = HomeOrOpponent;
            this.Value = Value;
            this.Time = Time;
            Zeit = null;
        }

        public BoutPoint(string value, Bout bout, Wrestler? homeOrOpponent = null) : this(value, bout, homeOrOpponent, DateTime.Now)
        {
        }
    }
}
