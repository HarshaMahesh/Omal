using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Omal.ViewModels
{
    public class ConfigurationVM : BaseVM
    {
        public ICommand UpdateDbCommand { get; set; }

        public ConfigurationVM()
        {
            UpdateDbCommand = new Command(OnUpdateDbCommand);
        }

        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        string _UpdateDbText = String.Empty;
        public string UpdateDbText
        {
            get
            {
                return _UpdateDbText;
            }
            set
            {
                _UpdateDbText = value;
                OnPropertyChanged();
            }
        }

        public async void OnUpdateDbCommand(object messaggio)
        {
            await CurPage.Navigation.PushModalAsync(new NavigationPage(new Views.DbUpdateV(true)));
            OnPropertyChanged("LastUpdate");
        }

        public string CurTitle
        {
            get { return "Configurazione"; }
        }

        public string LastUpdate
        {
            get
            {
                var val = App.LastUpdate;
                if (!val.HasValue) return "Mai";
                var giorni = Convert.ToInt32((DateTime.Now - val.Value).TotalDays);
                if (giorni > 1) return string.Format("{0} giorni fa.", giorni);
                return "Ora";
            }

        }


    }
}
