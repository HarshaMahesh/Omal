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
            get { return TitoloProdotto; }
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
                return CurProdotto != null && picker1!= null && picker1.Count>0;
            }
        }

        public bool Picker2IsVisible
        {
            get
            {
                return CurProdotto != null &&  !string.IsNullOrWhiteSpace(selectedPicker1.Key) && (picker2 != null) && (picker2.Count > 0);
            }
        }

        public bool Picker3IsVisible
        {
            get
            {
                return CurProdotto != null && ProdottoIsValvola && !string.IsNullOrWhiteSpace(selectedPicker2.Key)&& (picker3 != null) && (picker3.Count > 0);
            }
        }
        public bool Picker4IsVisible
        {
            get
            {
                return CurProdotto != null && ProdottoIsValvola && !string.IsNullOrWhiteSpace(selectedPicker3.Key) && (picker4 != null) && (picker4.Count > 0);
            }
        }

        public string DescrizionePicker1
        {
            get
            {
                if (ProdottoIsValvola)
                    return StrAzionamento;
                else
                    return StrInfoProdottoPicker1Attuatore;
            }
        }

        public string DescrizionePicker2
        {
            get
            {
                if (ProdottoIsValvola)
                    return StrMateriale;
                else
                    return StrInfoProdottoPicker2Attuatore;
            }
        }

        public string DescrizionePicker3
        {
            get
            {
                if (ProdottoIsValvola)
                    return StrPnasi;
                else
                    return "";
            }
        }

        public string DescrizionePicker4
        {
            get
            {
                if (ProdottoIsValvola)
                    return StrDn;
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

        bool isRunning = false;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {

                if (isRunning != value)
                {
                    isRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        private async void OnCercaArticoliCommand(object obj)
        {
            List<Models.Base> vettore;
            if (ProdottoIsValvola)
            {
                var elenco = elencoCompletoValvole;
                if (!String.IsNullOrWhiteSpace(selectedPicker1.Value)) elenco = elenco.Where(x => string.Equals(x.valore_azionamento, selectedPicker1.Key, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.valore_materiale, selectedPicker2.Key, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker3.Value)) elenco = elenco.Where(x => string.Equals(x.valore_pnansi, selectedPicker3.Key, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker4.Value)) elenco = elenco.Where(x => string.Equals(x.valore_dn, selectedPicker4.Key, StringComparison.InvariantCultureIgnoreCase)).ToList();
                vettore = new List<Models.Base>(elenco);
            }
            else
            {
                var elenco = elencoCompletoAttuatori;
                if (!String.IsNullOrWhiteSpace(selectedPicker1.Value)) elenco = elenco.Where(x => string.Equals(x.valore_misura, selectedPicker1.Key, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.valore_iso, selectedPicker2.Key, StringComparison.InvariantCultureIgnoreCase)).ToList();

                vettore = new List<Models.Base>(elenco);
            }
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
            OnPropertyChanged("SelectedPicker4");
            OnPropertyChanged("SelectedPicker3");
            OnPropertyChanged("SelectedPicker2");
            OnPropertyChanged("SelectedPicker1");
        }

        private void MyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, "Picker1", StringComparison.InvariantCultureIgnoreCase))
            {
                OnPropertyChanged("Picker1IsVisible");
                OnPropertyChanged("TrovaIsVisible");
            }
            if (string.Equals(e.PropertyName, "Picker2", StringComparison.InvariantCultureIgnoreCase))
            {
                OnPropertyChanged("Picker2IsVisible");
            }
            if (string.Equals(e.PropertyName, "Picker3", StringComparison.InvariantCultureIgnoreCase))
            {
                OnPropertyChanged("Picker3IsVisible");
            }
            if (string.Equals(e.PropertyName, "Picker4", StringComparison.InvariantCultureIgnoreCase))
            {
                OnPropertyChanged("Picker4IsVisible");
            }
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
                    if (picker1.Count() == 0 && !loadAttuatore[0] && isFirstTry[0])
                        LoadAttuatori(1);
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
                    if (picker2.Count() == 0 && !loadAttuatore[1] && isFirstTry[1] && !string.IsNullOrWhiteSpace(SelectedPicker1.Value))
                        LoadAttuatori(2);
                
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
                IsRunning = true;
                try
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
                    if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.valore_materiale, selectedPicker2.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    if (!String.IsNullOrWhiteSpace(selectedPicker3.Value)) elenco = elenco.Where(x => string.Equals(x.valore_pnansi, selectedPicker3.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    if (!String.IsNullOrWhiteSpace(selectedPicker4.Value)) elenco = elenco.Where(x => string.Equals(x.valore_dn, selectedPicker4.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    switch (indice)
                    {
                        case 1:
                            Picker1 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_azionamento, RemoveHtmlChar(x.valore_azionamento))).Distinct());
                            break;
                        case 2:

                            Picker2 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_materiale, RemoveHtmlChar(x.valore_materiale))).Distinct());
                            if (Picker2.Count() == 1) SelectedPicker2 = Picker2.First();
                            break;
                        case 3:
                            Picker3 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_pnansi, RemoveHtmlChar(x.valore_pnansi))).Distinct());
                            break;
                        case 4:
                            Picker4 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_dn, RemoveHtmlChar(x.valore_dn))).Distinct());
                            break;
                        default:
                            break;
                    }
                    loadvalvola = false;
                    loadValvole[indice - 1] = false;
                }
                finally
                {
                    IsRunning = false;
                }

            }
        }


        private string RemoveHtmlChar(string valore)
        {
            if (string.IsNullOrWhiteSpace(valore)) return valore;
            return valore.Replace("&quot;", @"""");
        }

        bool[] loadAttuatore = new bool[] { false, false, false};
        bool loadattuatore = false;
        async void LoadAttuatori(int indice)
        {
            if (!loadattuatore && !loadAttuatore[indice - 1])
            {
                IsRunning = true;
                try
                {
                    isFirstTry[indice - 1] = false;
                    loadattuatore = true;
                    loadAttuatore[indice - 1] = true;
                    if (elencoCompletoAttuatori == null)
                    {
                        elencoCompletoAttuatori = await DataStore.Attuatori.GetItemsAsync(false);
                        elencoCompletoAttuatori = elencoCompletoAttuatori.Where(x => x.idprodotto == CurProdotto.idprodotto);
                    }
                    var elenco = elencoCompletoAttuatori;
                    if (!String.IsNullOrWhiteSpace(selectedPicker1.Value)) elenco = elenco.Where(x => string.Equals(x.valore_misura, selectedPicker1.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    if (!String.IsNullOrWhiteSpace(selectedPicker2.Value)) elenco = elenco.Where(x => string.Equals(x.valore_iso, selectedPicker2.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    switch (indice)
                    {
                        case 1:
                            Picker1 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_misura, RemoveHtmlChar(x.valore_misura))).Distinct());
                            break;
                        case 2:
                            Picker2 = new ObservableCollection<KeyValuePair<string, string>>(elenco.OrderBy(x => x.ordine).Select(x => new KeyValuePair<string, string>(x.valore_iso, RemoveHtmlChar(x.valore_iso))).Distinct());
                            if (Picker2.Count() == 1) SelectedPicker2 = Picker2.First();
                            break;
                        default:
                            break;
                    }
                    loadattuatore = false;
                    loadAttuatore[indice - 1] = false;
                }
                finally
                {
                    IsRunning = false;
                }

            }
        }

        IEnumerable<Models.Valvola> elencoCompletoValvole = null;
        IEnumerable<Models.Attuatore> elencoCompletoAttuatori = null;



        ObservableCollection<KeyValuePair<string, string>> picker3 = new ObservableCollection<KeyValuePair<string, string>>();
        public ObservableCollection<KeyValuePair<string, string>> Picker3
        {
            get
            {
                if (CurProdotto == null) return new ObservableCollection<KeyValuePair<string, string>>();
                if (ProdottoIsValvola)
                {
                    if (picker3.Count() == 0 && !loadValvole[2] && isFirstTry[2] && !string.IsNullOrWhiteSpace(SelectedPicker2.Value))
                        LoadValvole(3);
                }
                else
                    if (picker3.Count() == 0 && !loadAttuatore[2] && isFirstTry[2] && !string.IsNullOrWhiteSpace(SelectedPicker2.Value))
                       LoadAttuatori(3);
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
