using System.Collections.Generic;
using System.Timers;
using Ringen.Core.DependencyInjection;
using Ringen.Core.Messaging;
using Ringen.Core.UI;
using Ringen.Core.ViewModels;
using Ringen.Core.ViewModels.Enums;
using Ringen.Schnittstellen.Contracts.Models.Enums;

namespace Ringen.Core.CS
{
    public class BoutTime : ExtendedNotifyPropertyChanged
    {
        public enum Modes { Idle, Running, Paused, Finished }

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
            Mode = Modes.Idle;
            this.Max = Max;
            this.Pauses = Pauses;
        }

        public void Start()
        {
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Timer '{GriffbewertungsTypViewModel}' wurde gestartet."));
            Timer.Start();
            Mode = Modes.Running;

            if (GriffbewertungsTypViewModel is GriffbewertungsTypViewModel.HeimInjury or GriffbewertungsTypViewModel.GastInjury)
                BoutSettings.Times[GriffbewertungsTypViewModel.Bout.ToString()].Stop();

            //TODO: Wäre wohl besser über Messageing realisierbar...
            if (GriffbewertungsTypViewModel == GriffbewertungsTypViewModel.Bout)
            {
                if(BoutSettings.Times["HeimActivity"].Mode == Modes.Paused)
                    BoutSettings.Times["HeimActivity"].Start();
                else if (BoutSettings.Times["GastActivity"].Mode == Modes.Paused)
                    BoutSettings.Times["GastActivity"].Stop();
            }
        }
        public void Stop()
        {
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Timer '{GriffbewertungsTypViewModel}' wurde gestoppt."));
            Timer.Stop();
            Mode = Modes.Paused;

            //TODO: Wäre wohl besser über Messageing realisierbar...
            if (GriffbewertungsTypViewModel == GriffbewertungsTypViewModel.Bout) {
                if (BoutSettings.Times["HeimActivity"].Mode == Modes.Running)
                    BoutSettings.Times["HeimActivity"].Stop();
                if (BoutSettings.Times["GastActivity"].Mode == Modes.Running)
                    BoutSettings.Times["GastActivity"].Stop();
            }
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

                    if (GriffbewertungsTypViewModel == GriffbewertungsTypViewModel.Bout)
                        BoutSettings.Times[GriffbewertungsTypViewModel.Break.ToString()].Start();
                }
                if (Time == Max)
                {
                    Timer.Stop();
                    Mode = Modes.Finished;

                    //TODO: Bin mir nicht sicher ob das hier vernünftig hingehört. Evtl. könnte man das über Messageing oder Events besser lösen....
                    //TODO: Timer wird aktuell bei einem Punkt nicht gestoppt.
                    if (GriffbewertungsTypViewModel is GriffbewertungsTypViewModel.HeimActivity or GriffbewertungsTypViewModel.GastActivity)
                    {
                        var  explorerStates = DependencyInjectionContainer.GetService<ExplorerStates>();
                        var Punkt = new Schnittstellen.Contracts.Models.Griffbewertungspunkt()
                        {
                            Fuer = GriffbewertungsTypViewModel == GriffbewertungsTypViewModel.HeimActivity ? HeimGast.Gast : HeimGast.Heim,
                            Punktzahl = 1,
                            Zeit = System.TimeSpan.FromSeconds(BoutSettings.Times["Bout"].Time)
                        };
                        RunInUI.Run(() =>
                        {
                            explorerStates.Einzelkampf.Wertungspunkte.Add(Punkt);
                        });
                    }
                }

                base.OnPropertyChanged();
            }
        }
    }
}
