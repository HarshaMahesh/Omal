using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace Omal.ViewModels
{
    public class DbUpdateVM:BaseVM
    {
        public ICommand UpdateDbCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public DbUpdateVM()
        {
            UpdateDbCommand = new Command(OnUpdateDbCommand);
            CloseCommand = new Command(OnCloseCommand);
        }


        private async void OnCloseCommand(object obj)
        {
            await CurPage.Navigation.PopModalAsync();
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

        public async void OnUpdateDbCommand(object messaggio)
        {
            var start = DateTime.Now;
            IsRunning = true;
            Errore = false;
            ErroreTxt = string.Empty;
            try
            {
                UpdateDbText = string.Format(@"{1} -> {0}{2}", "Inizio", DateTime.Now, Environment.NewLine);
                var attuatori = await DataStore.Attuatori.GetItemsAsync(true);
                ProgressB = 0.1;
                UpdateDbText += string.Format(@"{1} -> Attuatori {0}{2}", attuatori.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var categorie = await DataStore.Categorie.GetItemsAsync(true);
                ProgressB = 0.2;
                UpdateDbText += string.Format(@"{1} -> Categorie {0}{2}", categorie.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                if (App.CurUser != null)
                {
                    var clienti = await DataStore.Clienti.GetItemsAsync(true);
                    UpdateDbText += string.Format(@"{1} -> Clienti {0}{2}", clienti.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                    ProgressB = 0.3;
                }
                var ordini = await DataStore.Ordini.GetItemsAsync(true);
                ProgressB = 0.4;
                UpdateDbText += string.Format(@"{1} -> Ordini {0}{2}", ordini.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var prodotti = await DataStore.Prodotti.GetItemsAsync(true);
                ProgressB = 0.5;
                UpdateDbText += string.Format(@"{1} -> Prodotti {0}{2}", prodotti.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var prodottiGruppiMetadati = await DataStore.ProdottoGruppiMetadati.GetItemsAsync(true);
                ProgressB = 0.6;
                UpdateDbText += string.Format(@"{1} -> Gruppi Prodotti {0}{2}", prodottiGruppiMetadati.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var ProdottoMetadati = await DataStore.ProdottoMetadati.GetItemsAsync(true);
                ProgressB = 0.7;
                UpdateDbText += string.Format(@"{1} -> Prodotti Metadati {0}{2}", ProdottoMetadati.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var Valvole = await DataStore.Valvole.GetItemsAsync(true);
                ProgressB = 1;
                UpdateDbText = string.Format(@"{1} -> Valvole {0}{2}", Valvole.Count(), DateTime.Now, Environment.NewLine);
                App.LastUpdate = start;
                OnPropertyChanged("LastUpdate");
                CloseCommand.Execute(null);
            }
            catch (Exception ex)
            {
                ErroreTxt = ex.Message;
                Errore = true;

            }
            finally
            {
                IsRunning = false;
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
                if (!double.Equals(_ProgressB ,value))
                {
                    _ProgressB = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
