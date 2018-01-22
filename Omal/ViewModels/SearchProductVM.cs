using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class SearchProductVM:ViewModels.BaseVM
    {
        public ICommand GoToDetailCommand { get; set; }

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
            PropertyChanged += MyPropertyChanged;
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
                OnPropertyChanged("Picker1");
                OnPropertyChanged("Picker2");
                OnPropertyChanged("Picker3");
                OnPropertyChanged("Picker4");
            }
        }

        ObservableCollection<KeyValuePair<int, string>> picker1 = null;
        public ObservableCollection<KeyValuePair<int, string>> Picker1
        {
            get
            {
                if (picker1 == null || picker1.Count()==0 )
                {
                    if (CurProdotto == null) return new ObservableCollection<KeyValuePair<int, string>>();
                    if (ProdottoIsValvola)
                        picker1 = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Valvole.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.IdProdotto).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCodiceValvola, x.valore_azionamento)));
                    else
                        picker1 = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Attuatori.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.IdProdotto).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCodiceAttuatore, x.Valore_misura)));
                }
                return picker1;
            }
        }

        ObservableCollection<KeyValuePair<int, string>> picker2 = null;
        public ObservableCollection<KeyValuePair<int, string>> Picker2
        {
            get
            {
                if (picker2 == null ||picker2.Count() == 0 )
                {
                    if (CurProdotto == null) return new ObservableCollection<KeyValuePair<int, string>>();
                    if (ProdottoIsValvola)
                        picker2 = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Valvole.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.IdProdotto).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCodiceValvola, x.valore_dn)));
                    else
                        picker2 = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Attuatori.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.IdProdotto).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCodiceAttuatore, x.Valore_iso)));
                }
                return picker2;
            }
        }

        ObservableCollection<KeyValuePair<int, string>> picker3 = null;
        public ObservableCollection<KeyValuePair<int, string>> Picker3
        {
            get
            {
                if (picker3 == null || picker3.Count() == 0)
                {
                    if (CurProdotto == null) return new ObservableCollection<KeyValuePair<int, string>>();
                    if (ProdottoIsValvola)
                        picker3 = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Valvole.GetItemsAsync(false).Result.Where(x=>x.IdProdotto == CurProdotto.IdProdotto).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCodiceValvola, x.valore_pnansi)));
                    else
                        picker3 = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Attuatori.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.IdProdotto).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCodiceAttuatore, x.Valore_coppia)));
                }
                return picker3;
            }
        }

        ObservableCollection<KeyValuePair<int, string>> picker4 = null;
        public ObservableCollection<KeyValuePair<int, string>> Picker4
        {
            get
            {
                if (picker4 == null || picker4.Count() == 0)
                {
                    if (CurProdotto == null || !ProdottoIsValvola) return new ObservableCollection<KeyValuePair<int, string>>();
                    picker4 = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Valvole.GetItemsAsync(false).Result.Where(x => x.IdProdotto == CurProdotto.IdProdotto).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCodiceValvola, x.valore_pnansi)));
                    
                }
                return picker4;
            }
        }



        private void OnGoToDetailCommand()
        {
            CurPage.Navigation.PushAsync(new Views.InfoProductV(CurProdotto));
        }
    }
}
