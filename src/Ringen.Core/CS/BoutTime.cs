using System.Collections.Generic;
using System.Timers;
using Ringen.Core.Messaging;
using Ringen.Core.UI;
using Ringen.Core.ViewModels.Enums;

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

        

        private GriffbewertungsTypViewModel _mGriffbewertungsTypViewModel;

        public GriffbewertungsTypViewModel GriffbewertungsTypViewModel
        {
            get { return Get(_mGriffbewertungsTypViewModel); }
            set { Set(ref _mGriffbewertungsTypViewModel, value); }
        }

        private BoutSettings BoutSettings;

        public int Max { get; set; }
        public List<int> Pauses { get; set; }

        public BoutTime(BoutSettings BoutSettings, GriffbewertungsTypViewModel griffbewertungsTypViewModel, int Max, List<int> Pauses = null)
        {
            this.BoutSettings = BoutSettings;
            this.GriffbewertungsTypViewModel = griffbewertungsTypViewModel;
            Mode = Modes.Paused;
            this.Max = Max;
            this.Pauses = Pauses;
        }

        public void Start()
        {
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Timer '{GriffbewertungsTypViewModel.ToString()}' wurde gestartet."));
            Timer.Start();
            Mode = Modes.Running;

            if (GriffbewertungsTypViewModel == GriffbewertungsTypViewModel.HomeInjury || GriffbewertungsTypViewModel == GriffbewertungsTypViewModel.OpponentInjury) BoutSettings.Times[GriffbewertungsTypViewModel.Bout.ToString()].Stop();
        }
        public void Stop()
        {
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Timer '{GriffbewertungsTypViewModel.ToString()}' wurde gestoppt."));
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

                    if (GriffbewertungsTypViewModel == GriffbewertungsTypViewModel.Bout) BoutSettings.Times[GriffbewertungsTypViewModel.Break.ToString()].Start();
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
