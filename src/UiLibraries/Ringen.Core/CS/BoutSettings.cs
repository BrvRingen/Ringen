using System.Collections.Generic;
using Ringen.Core.ViewModels.Enums;
using Ringen.Schnittstellen.Contracts.Models;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Core.CS
{
    public class BoutSettings
    {


        private List<Griffbewertungspunkt> posPoints;

        public List<Griffbewertungspunkt> PosPoints
        {
            get { return posPoints; }
            set { posPoints = value; }
        }
        public List<Griffbewertungspunkt> PosPointsHeim
        {
            get
            {
                var tmp = new List<Griffbewertungspunkt>();
                PosPoints.ForEach((PosPoint) =>
                {
                    var PosPointHome = (Griffbewertungspunkt)PosPoint.Clone();
                    PosPointHome.Fuer = HeimGast.Heim;
                    tmp.Add(PosPointHome);
                });
                return tmp;
            }
        }
        public List<Griffbewertungspunkt> PosPointsGast
        {
            get
            {
                var tmp = new List<Griffbewertungspunkt>();
                PosPoints.ForEach((PosPoint) =>
                {
                    var PosPointHome = (Griffbewertungspunkt)PosPoint.Clone();
                    PosPointHome.Fuer = HeimGast.Gast;
                    tmp.Add(PosPointHome);
                });
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
                        { GriffbewertungsTypViewModel.HeimInjury.ToString(), new BoutTime(this, GriffbewertungsTypViewModel.HeimInjury, 120) },
                        { GriffbewertungsTypViewModel.GastInjury.ToString(), new BoutTime(this, GriffbewertungsTypViewModel.GastInjury, 120) },
                        { GriffbewertungsTypViewModel.HeimActivity.ToString(), new BoutTime(this, GriffbewertungsTypViewModel.HeimActivity, 30) },
                        { GriffbewertungsTypViewModel.GastActivity.ToString(), new BoutTime(this, GriffbewertungsTypViewModel.GastActivity, 30) },
                    };
                }
                return times;
            }
        }

        public BoutSettings(StilartViewModel stilartViewModel)
        {
            //Aktuelle Regeln nach 2017
            PosPoints = new List<Griffbewertungspunkt>() {
                        new Griffbewertungspunkt() { Punktzahl = 1, Typ = GriffbewertungsTyp.Punkt },
                        new Griffbewertungspunkt() { Punktzahl = 2, Typ = GriffbewertungsTyp.Punkt },
                        new Griffbewertungspunkt() { Punktzahl = 4, Typ = GriffbewertungsTyp.Punkt },
                        new Griffbewertungspunkt() { Punktzahl = 5, Typ = GriffbewertungsTyp.Punkt },
                        new Griffbewertungspunkt() { Typ = GriffbewertungsTyp.Passiv },
                        new Griffbewertungspunkt() { Punktzahl = 0, Typ = GriffbewertungsTyp.Punkt },
                        new Griffbewertungspunkt() { Typ = GriffbewertungsTyp.Verwarnung },
                     };

            if (stilartViewModel == StilartViewModel.LL)
                PosPoints.Add(new Griffbewertungspunkt() { Typ = GriffbewertungsTyp.Aktivitaetszeit });
        }
    }
}
