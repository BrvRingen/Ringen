using System.Collections.Generic;
using Ringen.Core.ViewModels.Enums;

namespace Ringen.Core.CS
{
    public class BoutSettings
    {


        private List<string> posPoints;

        public List<string> PosPoints
        {
            get { return posPoints; }
            set { posPoints = value; }
        }
        public List<Schnittstellen.Contracts.Models.Griffbewertungspunkt> PosPointsHome
        {
            get
            {
                var tmp = new List<Schnittstellen.Contracts.Models.Griffbewertungspunkt>();
                foreach (var posPoint in posPoints)
                {
                    tmp.Add(new Schnittstellen.Contracts.Models.Griffbewertungspunkt()
                        {
                            Fuer = Schnittstellen.Contracts.Models.Enums.HeimGast.Heim,
                            Punktzahl = int.TryParse(posPoint, out int Point) ? Point : 0,
                        }
                    );
                }

                return tmp;
            }
        }
        public List<Schnittstellen.Contracts.Models.Griffbewertungspunkt> PosPointsOpponent
        {
            get
            {
                var tmp = new List<Schnittstellen.Contracts.Models.Griffbewertungspunkt>();
                foreach (var posPoint in posPoints)
                {
                    tmp.Add(new Schnittstellen.Contracts.Models.Griffbewertungspunkt()
                        {
                            Fuer = Schnittstellen.Contracts.Models.Enums.HeimGast.Gast,
                            Punktzahl = int.TryParse(posPoint, out int Point) ? Point : 0,
                        }
                    );
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
                        { GriffbewertungsTypViewModel.Bout.ToString(), new BoutTime(this, GriffbewertungsTypViewModel.Bout, 360, new List<int>() { 180 }) },
                        { GriffbewertungsTypViewModel.Break.ToString(), new BoutTime(this, GriffbewertungsTypViewModel.Break, 30) },
                        { GriffbewertungsTypViewModel.HomeInjury.ToString(), new BoutTime(this, GriffbewertungsTypViewModel.HomeInjury, 120) },
                        { GriffbewertungsTypViewModel.OpponentInjury.ToString(), new BoutTime(this, GriffbewertungsTypViewModel.OpponentInjury, 120) }//,
                        //{ Types.HomeActivity.ToString(), new BoutTime(30) },
                        //{ Types.OpponentActivity.ToString(), new BoutTime(30) },
                        //{ Types.HomeP.ToString(), new BoutTime() },
                        //{ Types.OpponentP.ToString(), new BoutTime() }
                    };
                }
                return times;
            }
        }

        public BoutSettings(StilartViewModel stilartViewModel)
        {
            //Aktuelle Regeln nach 2017
            if (stilartViewModel == StilartViewModel.LL)
                posPoints = new List<string>() { "1", "2", "4", "5", "P", "0", "VZ", "A" };
            else
                posPoints = new List<string>() { "1", "2", "4", "5", "P", "0", "VZ" };


        }
    }
}
