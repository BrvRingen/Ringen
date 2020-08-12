using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core.CS
{

    public class BoutSettings
    {
        public Bout Bout { get; set; }

        public enum WrestleStyles
        {
            [Description("Freistil")]
            LL,

            [Description("Gr.-röm.")]
            GR
        }
        public enum Results { TÜ, SS, PS, KL }

        private List<string> posPoints;

        public List<string> PosPoints
        {
            get { return posPoints; }
            set { posPoints = value; }
        }
        public List<BoutPoint> PosPointsHome
        {
            get
            {
                var tmp = new List<BoutPoint>();
                foreach (var posPoint in posPoints)
                {
                    tmp.Add(new BoutPoint(posPoint, Bout, BoutPoint.Wrestler.Home));
                }

                return tmp;
            }
        }
        public List<BoutPoint> PosPointsOpponent
        {
            get
            {
                var tmp = new List<BoutPoint>();
                foreach (var posPoint in posPoints)
                {
                    tmp.Add(new BoutPoint(posPoint, Bout, BoutPoint.Wrestler.Opponent));
                }

                return tmp;
            }
        }

        public Dictionary<string, BoutTime> times;
        public Dictionary<string, BoutTime> Times
        {
            get
            {
                if (times == null)
                {
                    times = new Dictionary<string, BoutTime>
                    {
                        { BoutTime.Types.Bout.ToString(), new BoutTime(this, BoutTime.Types.Bout, 360, new List<int>() { 180 }) },
                        { BoutTime.Types.Break.ToString(), new BoutTime(this, BoutTime.Types.Break, 30) },
                        { BoutTime.Types.HomeInjury.ToString(), new BoutTime(this, BoutTime.Types.HomeInjury, 120) },
                        { BoutTime.Types.OpponentInjury.ToString(), new BoutTime(this, BoutTime.Types.OpponentInjury, 120) }//,
                        //{ BoutTime.Types.HomeActivity.ToString(), new BoutTime(30) },
                        //{ BoutTime.Types.OpponentActivity.ToString(), new BoutTime(30) },
                        //{ BoutTime.Types.HomeP.ToString(), new BoutTime() },
                        //{ BoutTime.Types.OpponentP.ToString(), new BoutTime() }
                    };
                }
                return times;
            }
        }

        public BoutSettings(Bout Bout)
        {
            this.Bout = Bout;
            //Aktuelle Regeln nach 2017
            if (Bout.WrestleStyle == WrestleStyles.LL)
                posPoints = new List<string>() { "1", "2", "4", "5", "P", "0", "VZ", "A" };
            else
                posPoints = new List<string>() { "1", "2", "4", "5", "P", "0", "VZ" };


        }
    }
}
