using Newtonsoft.Json.Linq;
using Ringen.Core.UI;
using System.Collections.Generic;
using System.Timers;

namespace Ringen.Core.CS
{
    public class Bout : ExtendedNotifyPropertyChangedUserControl, IExplorerItem
    {
        private JObject Data;

        public Bout(JObject Data)
        {
            this.Data = Data;


        }

        public string Value
        {
            get
            {
                return $"{Data["homeWrestlerName"].ToString()} - {Data["opponentWrestlerName"].ToString()}"; ;
            }
        }

        private Timer myTimer;
        public Timer MyTimer
        {
            get
            {
                if(myTimer == null)
                {
                    myTimer = new Timer();
                    myTimer.Interval = 1000;
                    myTimer.Elapsed += (object sender, ElapsedEventArgs e) =>
                    {
                        ZeitRunde1++;
                    };
                }
                return myTimer;
            }
        }

        private int zeitRunde1 = 0;
        public int ZeitRunde1
        {
            get
            {
                return zeitRunde1;
            }
            set
            {
                zeitRunde1 = value;
                base.OnPropertyChanged();
            }
        }





        public List<bool> Children { get; set; }
    }
}
