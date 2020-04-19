using Ringen.Core;
using Ringen.Core.UI;

namespace Ringen.Plugin.CsView
{
    /// <summary>
    /// Interaktionslogik für CsEditor.xaml
    /// </summary>
    public partial class View : RingenTabItem
    {
        private Core.CS.Competition competition;

        public Core.CS.Competition Competition
        {
            get { return competition; }
            set
            {
                competition = value;
                OnPropertyChanged("Competition");
            }
        }

        private Core.CS.Bout bout;

        public Core.CS.Bout Bout
        {
            get { return bout; }
            set
            {
                bout = value;
                OnPropertyChanged("Bout");
            }
        }

        public View()
        {
            InitializeComponent();
            SetData();

            Explorer.SelectedItemChanged += ((object sender, Explorer.SelectedItemChangedEventArgs e) => { SetData(); });
        }

        private void SetData()
        {
            Competition = Explorer.SelectedItem as Core.CS.Competition;
            Bout = Explorer.SelectedItem as Core.CS.Bout;
        }
    }
}
