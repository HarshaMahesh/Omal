using System;
using System.ComponentModel;
using Omal.Common;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class WelcomeVM:BaseVM
    {

        public Command LangItCommand { get; set; }
        public Command LangEnCommand { get; set; }
        public Command UpdateDbCommand { get; set; }

        public bool ChangeLanguage { get; set; }

        public WelcomeVM()
        {
            LangItCommand= new Command(OnLangItCommand);
            LangEnCommand = new Command(OnLangEnCommand);
            UpdateDbCommand = new Command(OnUpdateDbCommand);
            PropertyChanged += OnLocalPropertyChanged;
            MessagingCenter.Subscribe<Models.Messages.GotoWelcomeMessage>(this, "", sender =>
            {
                ChangeLanguage = sender.ChangeLanguage;
               Navigation.PopModalAsync();
               

            });

        }

        private void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, "IsRunning", StringComparison.InvariantCultureIgnoreCase)) OnPropertyChanged("IsRunningOrError");
            if (string.Equals(e.PropertyName, "Errore", StringComparison.InvariantCultureIgnoreCase)) OnPropertyChanged("IsRunningOrError");
        }


        private async void OnUpdateDbCommand(object obj)
        {
             UpdateDb();
        }

        private async void OnLangEnCommand(object obj)
        {
            App.CurLang = "EN";
            UpdateDb();
        }

        private void OnLangItCommand(object obj)
        {
            App.CurLang = "IT";
            UpdateDb();
        }



        async void GoToMainPage()
        {
            await Navigation.PushModalAsync(new MainPage());
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
        string _ErroreTxt = String.Empty;
        public string ErroreTxt
        {
            get
            {
                return _ErroreTxt;
            }
            set
            {
                _ErroreTxt = value;
                OnPropertyChanged();
            }
        }

        bool _Errore = false;
        public bool Errore
        {
            get
            {
                return _Errore;
            }
            set
            {
                _Errore = value;
                OnPropertyChanged();
            }
        }

       
        public bool IsRunningOrError
        {
            get
            {
                return Errore || IsRunning;
            }
           
        }



        double _ProgressB;
        public double ProgressB
        {
            get
            {
                return _ProgressB;
            }
            set
            {
                if (!double.Equals(_ProgressB, value))
                {
                    _ProgressB = value;
                    OnPropertyChanged();
                }
            }
        }

        public async void UpdateDb()
        {
            var start = DateTime.Now;
            IsRunning = true;
            Errore = false;
            ErroreTxt = string.Empty;
            try
            {
                var attuatori = await DataStore.Attuatori.GetLastItemsUpdatesAsync();
                ProgressB = 0.1;
                var categorie = await DataStore.Categorie.GetLastItemsUpdatesAsync();
                ProgressB = 0.2;
                if (App.CurUser != null)
                {
                    var clienti = await DataStore.Clienti.GetLastItemsUpdatesAsync();
                    ProgressB = 0.3;
                    var ordini = await DataStore.Ordini.GetLastItemsUpdatesAsync();
                    ProgressB = 0.4;
                }
                var prodotti = await DataStore.Prodotti.GetLastItemsUpdatesAsync();
                ProgressB = 0.5;
                var prodottiGruppiMetadati = await DataStore.ProdottoGruppiMetadati.GetLastItemsUpdatesAsync();
                ProgressB = 0.6;
                var ProdottoMetadati = await DataStore.ProdottoMetadati.GetLastItemsUpdatesAsync();
                ProgressB = 0.7;
                var Valvole = await DataStore.Valvole.GetLastItemsUpdatesAsync();
                ProgressB = 1;
                App.LastUpdate = start;
                OnPropertyChanged("LastUpdate");
            }
            catch (Exception ex)
            {
                ErroreTxt = ex.Message;
                Errore = true;
            }
            finally
            {
                if (!Errore)  GoToMainPage();
                IsRunning = false;
            }

        }

    }
}
