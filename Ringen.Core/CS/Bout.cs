using Newtonsoft.Json.Linq;
using Ringen.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

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

        private DispatcherTimer myDispatcherTimer;
        public DispatcherTimer MyDispatcherTimer
        {
            get
            {
                if(myDispatcherTimer == null)
                {
                    myDispatcherTimer = new DispatcherTimer();
                    myDispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
                    myDispatcherTimer.Tick += (object sender, EventArgs e) => {
                        ZeitRunde1++;
                    };
                }
                return myDispatcherTimer;
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
