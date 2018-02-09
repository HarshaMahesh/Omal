using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
                return string.Equals(CurProdotto.tipologia, "VALVOLA", StringComparison.InvariantCultureIgnoreCase);
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

        private async void OnCercaArticoliCommand(object obj)
        {
            List<Models.Base> vettore;
            if (ProdottoIsValvola)
            {
                var elenco = elencoCompletoValvole;
                if (!String.IsNullOrWhiteSpace(selectedPicker1.Value)) elenco = elenco.Where(x => string.Equals(x.valore_azionamento, selectedPicker1.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.valore_dn, selectedPicker2.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker3.Value)) elenco = elenco.Where(x => string.Equals(x.valore_pnansi, selectedPicker3.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker4.Value)) elenco = elenco.Where(x => string.Equals(x.valore_materiale, selectedPicker4.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                vettore = new List<Models.Base>(elenco);
            }
            else
                vettore = new List<Models.Base>(ApplicaFiltroAttuatori());
            await CurPage.Navigation.PushAsync(new Views.ArticoliSearchResultV(CurProdotto, ProdottoIsValvola, vettore));
        }


        private void OnPulisciCommand(object obj)
        {
            selectedPicker1 = new KeyValuePair<string, string>(string.Empty, string.Empty);
            selectedPicker2 = new KeyValuePair<string, string>(string.Empty, string.Empty);
            selectedPicker3 = new KeyValuePair<string, string>(string.Empty, string.Empty);
            selectedPicker4 = new KeyValuePair<string, string>(string.Empty, string.Empty);
            isFirstTry[0] = true;
            isFirstTry[1] = true;
            isFirstTry[2] = true;
            isFirstTry[3] = true;
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
                OnPropertyChanged("Picker2IsVisible");
                OnPropertyChanged("Picker3IsVisible");
                OnPropertyChanged("Picker4IsVisible");
            }
            if (string.Equals(e.PropertyName, "SelectedPicker2", StringComparison.InvariantCultureIgnoreCase))
            {
                selectedPicker3 = new KeyValuePair<string, string>("", "");
                selectedPicker4 = new KeyValuePair<string, string>("", "");
                OnPropertyChanged("Picker3");
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
                    isFirstTry[3] = true;
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
                    isFirstTry[2] = true;
                    isFirstTry[3] = true;
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
                    isFirstTry[1] = true;
                    isFirstTry[2] = true;
                    isFirstTry[3] = true;
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


        ObservableCollection<KeyValuePair<string, string>> picker1 = new ObservableCollection<KeyValuePair<string, string>>();
        public ObservableCollection<KeyValuePair<string, string>> Picker1
        {
            get
            {
                if (CurProdotto == null) return new ObservableCollection<KeyValuePair<string, string>>();
                if (ProdottoIsValvola)
                {
                    if (picker1.Count()  == 0 && !loadValvole[0] && isFirstTry[0]) 
                        LoadValvole(1);
                }
                else
                    picker1 = new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroAttuatori().OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.Valore_misura, x.Valore_misura)).Distinct());
                if (picker1.Count() == 1) SelectedPicker1 = picker1.First();
                return picker1;
            }
            set
            {
                picker1 = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<KeyValuePair<string, string>> picker2 = new ObservableCollection<KeyValuePair<string, string>>();
        public ObservableCollection<KeyValuePair<string, string>> Picker2
        {
            get
            {
                if (CurProdotto == null) return new ObservableCollection<KeyValuePair<string, string>>();
                if (ProdottoIsValvola)
                    {
                    if (picker2.Count() == 0 && !loadValvole[1] && isFirstTry[1] && !string.IsNullOrWhiteSpace(SelectedPicker1.Value)) 
                        LoadValvole(2);
                    }
                else
                    picker2 = new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroAttuatori().OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.Valore_iso, x.Valore_iso)).Distinct());
                
                return picker2;
            }
            set
            {
                picker2 = value;
                OnPropertyChanged();
            }
        }

        bool[] loadValvole = new bool[] { false, false, false, false };
        bool[] isFirstTry = new bool[] { true, true, true, true };
        bool loadvalvola = false;
        async void LoadValvole(int indice)
        {
            if (!loadvalvola && !loadValvole[indice-1])
            {
                isFirstTry[indice - 1] = false;
                loadvalvola = true;
                loadValvole[indice - 1] = true;
                if (elencoCompletoValvole == null)
                {
                    elencoCompletoValvole = await DataStore.Valvole.GetItemsAsync(false);
                    elencoCompletoValvole = elencoCompletoValvole.Where(x => x.idprodotto == CurProdotto.idprodotto);
                }
                var elenco = elencoCompletoValvole;
                if (!String.IsNullOrWhiteSpace(selectedPicker1.Value)) elenco = elenco.Where(x => string.Equals(x.valore_azionamento, selectedPicker1.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.valore_dn, selectedPicker2.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker3.Value)) elenco = elenco.Where(x => string.Equals(x.valore_pnansi, selectedPicker3.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker4.Value)) elenco = elenco.Where(x => string.Equals(x.valore_materiale, selectedPicker4.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                switch (indice)
                {
                    case 1:
                        Picker1 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_azionamento, x.valore_azionamento)).Distinct());
                        break;
                    case 2:
                        Picker2 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_dn, x.valore_dn)).Distinct());
                        if (Picker2.Count() == 1) SelectedPicker2 = Picker2.First();
                        break;
                    case 3:
                        Picker3 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_pnansi, x.valore_pnansi)).Distinct());
                        break;
                    case 4:
                        Picker4 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_materiale, x.valore_materiale)).Distinct());
                        break;
                    default:
                        break;
                }
                loadvalvola = false;
                loadValvole[indice -1] = false;
            }
        }

        IEnumerable<Models.Valvola> elencoCompletoValvole = null;



        private IEnumerable<Models.Attuatore> ApplicaFiltroAttuatori()
        {
            var elenco = DataStore.Attuatori.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.idprodotto);
            if (!String.IsNullOrWhiteSpace(selectedPicker1.Value)) elenco = elenco.Where(x => string.Equals(x.Valore_misura, selectedPicker1.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.Valore_iso, selectedPicker2.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!String.IsNullOrWhiteSpace(selectedPicker3.Value)) elenco = elenco.Where(x => string.Equals(x.Valore_coppia, selectedPicker3.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return elenco;
        }

        ObservableCollection<KeyValuePair<string, string>> picker3 = new ObservableCollection<KeyValuePair<string, string>>();
        public ObservableCollection<KeyValuePair<string, string>> Picker3
        {
            get
            {
                if (CurProdotto == null) return new ObservableCollection<KeyValuePair<string, string>>();
                if (ProdottoIsValvola)
                {
                    if (picker3.Count() == 0 && !loadValvole[2] && isFirstTry[2]  && !string.IsNullOrWhiteSpace(SelectedPicker2.Value))
                        LoadValvole(3);
                }
                else
                    picker3= new ObservableCollection<KeyValuePair<string, string>>(ApplicaFiltroAttuatori().OrderBy(x => x.Ordine).Select(x => new KeyValuePair<string, string>(x.Valore_coppia, x.Valore_coppia)).Distinct());
                
                if (picker3.Count() == 1) SelectedPicker3 = picker3.First();
                return picker3;
            }
            set
            {
                picker3 = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<KeyValuePair<string, string>> picker4 = new ObservableCollection<KeyValuePair<string, string>>();
        public ObservableCollection<KeyValuePair<string, string>> Picker4
        {
            get
            {
                if (CurProdotto == null || !ProdottoIsValvola) return new ObservableCollection<KeyValuePair<string, string>>();
                if (ProdottoIsValvola)
                {
                    if (picker4.Count() == 0 && !loadValvole[3] && isFirstTry[3] && !string.IsNullOrWhiteSpace(SelectedPicker3.Value)) 
                        LoadValvole(4);
                }
                return picker4;
            }
            set
            {
                picker4 = value;
                OnPropertyChanged();
            }
        }



        private void OnGoToDetailCommand()
        {
            CurPage.Navigation.PushAsync(new Views.InfoProductV(CurProdotto));
        }
    }
}
