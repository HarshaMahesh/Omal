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
            var start = DateTime.Now;
            IsRunning = true;
            try
            {
                UpdateDbText = string.Format(@"{1} -> {0}{2}", "Inizio", DateTime.Now, Environment.NewLine);
                var attuatori = await DataStore.Attuatori.GetItemsAsync(true);
                UpdateDbText += string.Format(@"{1} -> Attuatori {0}{2}", attuatori.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var categorie = await DataStore.Categorie.GetItemsAsync(true);
                UpdateDbText += string.Format(@"{1} -> Categorie {0}{2}", categorie.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var clienti = await DataStore.Clienti.GetItemsAsync(true);
                UpdateDbText += string.Format(@"{1} -> Clienti {0}{2}", clienti.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var ordini = await DataStore.Ordini.GetItemsAsync(true);
                UpdateDbText += string.Format(@"{1} -> Ordini {0}{2}", ordini.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var prodotti = await DataStore.Prodotti.GetItemsAsync(true);
                UpdateDbText += string.Format(@"{1} -> Prodotti {0}{2}", prodotti.Count(), DateTime.Now.ToShortTimeString(),Environment.NewLine);
                var prodottiGruppiMetadati = await DataStore.ProdottoGruppiMetadati.GetItemsAsync(true);
                UpdateDbText += string.Format(@"{1} -> Gruppi Prodotti {0}{2}", prodottiGruppiMetadati.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var ProdottoMetadati = await DataStore.ProdottoMetadati.GetItemsAsync(true);
                UpdateDbText += string.Format(@"{1} -> Prodotti Metadati {0}{2}", ProdottoMetadati.Count(), DateTime.Now.ToShortTimeString(), Environment.NewLine);
                var Valvole = await DataStore.Valvole.GetItemsAsync(true);
                UpdateDbText = string.Format(@"{1} -> Valvole {0}{2}", Valvole.Count(), DateTime.Now, Environment.NewLine);
                App.LastUpdate = start;
                OnPropertyChanged("LastUpdate");
            }
            catch (Exception ex)
            {
                await CurPage.DisplayAlert("Error", ex.Message,"Ok");
            }
            finally
            {
                IsRunning = false;
            }
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
