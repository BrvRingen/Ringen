using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Ringen.Core.Messaging;
using Ringen.Core.UI;

namespace Ringen.Core.CS
{

    public class BoutTime : ExtendedNotifyPropertyChanged
    {
        public enum Modes { Running, Paused, Finished }

        private Modes m_Mode;

        public Modes Mode
        {
            get { return Get(m_Mode); }
            set { Set(ref m_Mode, value); }
        }

        public enum Types { Bout, Break, HomeInjury, OpponentInjury, HomeActivity, OpponentActivity, HomeP, OpponentP }

        private Types m_Type;

        public Types Type
        {
            get { return Get(m_Type); }
            set { Set(ref m_Type, value); }
        }

        private BoutSettings BoutSettings;

        public int Max { get; set; }
        public List<int> Pauses { get; set; }

        public BoutTime(BoutSettings BoutSettings, Types Type, int Max, List<int> Pauses = null)
        {
            this.BoutSettings = BoutSettings;
            this.Type = Type;
            Mode = Modes.Paused;
            this.Max = Max;
            this.Pauses = Pauses;
        }

        public void Start()
        {
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Timer '{Type.ToString()}' wurde gestartet."));
            Timer.Start();
            Mode = Modes.Running;

            if (Type == Types.HomeInjury || Type == Types.OpponentInjury) BoutSettings.Times[Types.Bout.ToString()].Stop();
        }
        public void Stop()
        {
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Timer '{Type.ToString()}' wurde gestoppt."));
            Timer.Stop();
            Mode = Modes.Paused;
        }

        private Timer timer;
        private Timer Timer
        {
            get
            {
                if (timer == null)
                {
                    Mode = Modes.Paused;
                    timer = new Timer
                    {
                        Interval = 1000
                    };
                    timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                    {
                        Time++;
                    };
                }
                return timer;
            }
        }

        private int time = 0;
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;

                if (Pauses != null && Pauses.Contains(Time))
                {
                    Timer.Stop();
                    Mode = Modes.Paused;

                    if (Type == Types.Bout) BoutSettings.Times[Types.Break.ToString()].Start();
                }
                if (Time == Max)
                {
                    Timer.Stop();
                    Mode = Modes.Finished;
                }

                base.OnPropertyChanged();
            }
        }
    }
}
