using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Omal.Persistence;

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

        public string CurLang
        {
            get
            {
                return App.CurLang;
            }
        }

        public string LastUpdate
        {
            get
            {
                string giorno = "Mai";
                var val = App.LastUpdate;
                if (val.HasValue) giorno = val.Value.ToString("d MMMM yyyy");
                var _connection = DependencyService.Get<ISQLiteDb>();
                var dimensione = _connection.GetDBSize();
                if (!val.HasValue) return "Mai";
                return string.Format(StrUltimoAggiornamento, giorno, dimensione.ToString("n2"));
            }

        }




    }
}
