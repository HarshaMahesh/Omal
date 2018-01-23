using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class SearchProductVM:ViewModels.BaseVM
    {
        public ICommand GoToDetailCommand { get; set; }
        public ICommand CercaArticoliCommand { get; set; }
        public ICommand PulisciCommand { get; set; }



        public string CurTitle
        {
            get { return "Prodotto"; }
        }


        public bool ProdottoIsValvola
        {
            get
            {
                if (CurProdotto == null) return false;
                return string.Equals(CurProdotto.Tipologia, "VALVOLA", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public bool Picker1IsVisible
        {
            get
            {
                return CurProdotto != null;
            }
        }

        public bool Picker2IsVisible
        {
            get
            {
                return CurProdotto != null &&  !string.IsNullOrWhiteSpace(selectedPicker1.Key);
            }
        }

        public bool Picker3IsVisible
        {
            get
            {
                return CurProdotto != null && !string.IsNullOrWhiteSpace(selectedPicker2.Key);
            }
        }
        public bool Picker4IsVisible
        {
            get
            {
                return CurProdotto != null && ProdottoIsValvola && !string.IsNullOrWhiteSpace(selectedPicker3.Key);
            }
        }

        public string DescrizionePicker1
        {
            get
            {
                if (ProdottoIsValvola)
                    return "Tipologia di azionamento";
                else
                    return "Misura";
            }
        }

        public string DescrizionePicker2
        {
            get
            {
                if (ProdottoIsValvola)
                    return "Diametro";
                else
                    return "ISO";
            }
        }

        public string DescrizionePicker3
        {
            get
            {
                if (ProdottoIsValvola)
                    return "Pressione";
                else
                    return "Coppia";
            }
        }

        public string DescrizionePicker4
        {
            get
            {
                if (ProdottoIsValvola)
                    return "Materiale";
                else
                    return "";
            }
        }



        Models.Prodotto _CurProdotto = null;
        public Models.Prodotto CurProdotto
        {
            get 
            {
                return _CurProdotto;
            }
            set
            {
                if (_CurProdotto != value)
                {
                    _CurProdotto = value;
                    OnPropertyChanged();
                }
            }
        }


        public SearchProductVM()
        {
            GoToDetailCommand = new Command(OnGoToDetailCommand);
            PulisciCommand = new Command(OnPulisciCommand);
            CercaArticoliCommand = new Command(OnCercaArticoliCommand);
            PropertyChanged += MyPropertyChanged;
        }

        private void OnCercaArticoliCommand(object obj)
        {
            IEnumerable<Models.Base> vettore;
            if (ProdottoIsValvola)
                vettore = ApplicaFiltroValvole(0);
            else
                vettore = ApplicaFiltroAttuatori();
            CurPage.Navigation.PushAsync(new Views.ArticoliSearchResultV(CurProdotto, ProdottoIsValvola, vettore));
        }

        private void OnPulisciCommand(object obj)
        {
            selectedPicker1 = new KeyValuePair<string, string>(string.Empty, string.Empty);
            selectedPicker2 = new KeyValuePair<string, string>(string.Empty, string.Empty);
            selectedPicker3 = new KeyValuePair<string, string>(string.Empty, string.Empty);
            selectedPicker4 = new KeyValuePair<string, string>(string.Empty, string.Empty);
            OnPropertyChanged("SelectedPicker1");
        }

        private void MyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, "CurProdotto", StringComparison.InvariantCultureIgnoreCase))
            {
                OnPropertyChanged("ProdottoIsValvola");
                OnPropertyChanged("DescrizionePicker1");
                OnPropertyChanged("DescrizionePicker2");
                OnPropertyChanged("DescrizionePicker3");
                OnPropertyChanged("DescrizionePicker4");
                OnPropertyChanged("Picker1IsVisible");
                OnPropertyChanged("Picker1");
                OnPropertyChanged("Picker2");
                OnPropertyChanged("Picker3");
                OnPropertyChanged("Picker4");
            }

            if (string.Equals(e.PropertyName, "SelectedPicker1", StringComparison.InvariantCultureIgnoreCase))
            {
                selectedPicker2 = new KeyValuePair<string, string>("","");
                selectedPicker3 = new KeyValuePair<string, string>("", "");
                selectedPicker4 = new KeyValuePair<string, string>("", "");
                OnPropertyChanged("Picker2");
                OnPropertyChanged("Picker3");
                OnPropertyChanged("Picker4");
                OnPropertyChanged("Picker2IsVisible");
                OnPropertyChanged("Picker3IsVisible");
                OnPropertyChanged("Picker4IsVisible");
            }
            if (string.Equals(e.PropertyName, "SelectedPicker2", StringComparison.InvariantCultureIgnoreCase))
            {
                selectedPicker3 = new KeyValuePair<string, string>("", "");
                selectedPicker4 = new KeyValuePair<string, string>("", "");
                OnPropertyChanged("Picker3");
                OnPropertyChanged("Picker4");
                OnPropertyChanged("Picker3IsVisible");
                OnPropertyChanged("Picker4IsVisible");
            }
            if (string.Equals(e.PropertyName, "SelectedPicker3", StringComparison.InvariantCultureIgnoreCase))
            {
                selectedPicker4 = new KeyValuePair<string, string>("", "");
                OnPropertyChanged("Picker4");
                OnPropertyChanged("Picker4IsVisible");
            }
                    
        }

        KeyValuePair<string, string> selectedPicker3;
        public KeyValuePair<string, string> SelectedPicker3
        {
            get
            {
                return selectedPicker3;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value.Key)) return;
                if (!string.Equals(selectedPicker3.Key, value.Key))
                {
                    selectedPicker3 = value;
                    OnPropertyChanged();
                }
            }

        }

        KeyValuePair<string, string> selectedPicker2;
        public KeyValuePair<string, string> SelectedPicker2
        {
            get
            {
                return selectedPicker2;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value.Key)) return;
                if (!string.Equals(selectedPicker2.Key, value.Key, StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPicker2 = value;
                    OnPropertyChanged();
                }
            }

        }

        KeyValuePair<string, string> selectedPicker1;
        public KeyValuePair<string, string> SelectedPicker1
        {
            get
            {
                return selectedPicker1;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value.Key)) return;
                if (!string.Equals(selectedPicker1.Key, value.Key, StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPicker1 = value;
                    OnPropertyChanged();
                }
            }

        }

        KeyValuePair<string, string> selectedPicker4;
        public KeyValuePair<string, string> SelectedPicker4
        {
            get
            {
                return selectedPicker4;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value.Key)) return;
                if (!string.Equals(selectedPicker4.Key,value.Key, StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPicker4 = value;
                    OnPropertyChanged();
                }
            }

        }


        ObservableCollection<KeyValuePair<string, string>> picker1 = null;
        public ObservableCollection<KeyValuePair<string, string>> Picker1
        {
            get
            {
                if (CurProdotto == null) return new ObservableCollection<KeyValuePair<string, string>>();
                if (ProdottoIsValvola)
                    picker1 = new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroValvole(1).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.valore_azionamento, x.valore_azionamento)).Distinct());
                else
                    picker1 = new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroAttuatori().OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.Valore_misura, x.Valore_misura)).Distinct());
                if (picker1.Count() == 1) SelectedPicker1 = picker1.First();
                return picker1;
            }
        }

        ObservableCollection<KeyValuePair<string, string>> picker2 = null;
        public ObservableCollection<KeyValuePair<string, string>> Picker2
        {
            get
            {
                if (CurProdotto == null) return new ObservableCollection<KeyValuePair<string, string>>();
                if (ProdottoIsValvola)
                    picker2 = new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroValvole(2).OrderBy(x=>x.Ordine).Select(x => new KeyValuePair<string, string>(x.valore_dn, x.valore_dn)).Distinct());
                else
                    picker2 = new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroAttuatori().OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.Valore_iso, x.Valore_iso)).Distinct());
                if (picker2.Count() == 1)  SelectedPicker2 = picker2.First();
                return picker2;
            }
        }


        IEnumerable<Models.Valvola> elencoCompletoValvole = null;

        private IEnumerable<Models.Valvola> ApplicaFiltroValvole(int escludiIndice)
        {
            if (elencoCompletoValvole == null) elencoCompletoValvole = DataStore.Valvole.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.IdProdotto);
            var elenco = elencoCompletoValvole;
            if (!String.IsNullOrWhiteSpace(selectedPicker1.Value)) elenco = elenco.Where(x => string.Equals(x.valore_azionamento, selectedPicker1.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.valore_dn, selectedPicker2.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!String.IsNullOrWhiteSpace(selectedPicker3.Value)) elenco = elenco.Where(x => string.Equals(x.valore_pnansi, selectedPicker3.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!String.IsNullOrWhiteSpace(selectedPicker4.Value)) elenco = elenco.Where(x => string.Equals(x.valore_materiale, selectedPicker4.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return elenco;
        }

        private IEnumerable<Models.Attuatore> ApplicaFiltroAttuatori()
        {
            var elenco = DataStore.Attuatori.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.IdProdotto);
            if (!String.IsNullOrWhiteSpace(selectedPicker1.Value)) elenco = elenco.Where(x => string.Equals(x.Valore_misura, selectedPicker1.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.Valore_iso, selectedPicker2.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!String.IsNullOrWhiteSpace(selectedPicker3.Value)) elenco = elenco.Where(x => string.Equals(x.Valore_coppia, selectedPicker3.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return elenco;
        }

        ObservableCollection<KeyValuePair<string, string>> picker3 = null;
        public ObservableCollection<KeyValuePair<string, string>> Picker3
        {
            get
            {
                if (CurProdotto == null) return new ObservableCollection<KeyValuePair<string, string>>();
                if (ProdottoIsValvola)
                    picker3 = new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroValvole(3).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.valore_pnansi, x.valore_pnansi)).Distinct());
                else
                    picker3= new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroAttuatori().OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.Valore_coppia, x.Valore_coppia)).Distinct());
                
                if (picker3.Count() == 1) SelectedPicker3 = picker3.First();
                return picker3;
            }
        }

        ObservableCollection<KeyValuePair<string, string>> picker4 = null;
        public ObservableCollection<KeyValuePair<string, string>> Picker4
        {
            get
            {
                if (CurProdotto == null || !ProdottoIsValvola) return new ObservableCollection<KeyValuePair<string, string>>();
                picker4 = new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroValvole(4).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.valore_materiale, x.valore_materiale)).Distinct());
                if (picker4.Count() == 1) SelectedPicker4 = picker4.First();
                return picker4;
            }
        }



        private void OnGoToDetailCommand()
        {
            CurPage.Navigation.PushAsync(new Views.InfoProductV(CurProdotto));
        }
    }
}
