using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Omal.Common;

namespace Omal.ViewModels
{
    public class SearchVM: BaseVM
    {

        public RelayCommand SearchWithCategoriesCommand{get;set;}
        public RelayCommand SearchWithProductNameCommand { get; set; }

        public string CurTitle { get { return TitoloCerca; }}

        string productNameFilter;
        public string ProductNameFilter
        {
            get
            {
                return productNameFilter;
            }
            set
            {
                productNameFilter = value;
                OnPropertyChanged();
                SearchWithProductNameCommand.ChangeCanExecute();
            }

        }

        public bool Picker2IsVisible
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SelectedPrimoLivello.Value);
            }
        }

        public bool Picker3IsVisible
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SelectedSecondoLivello.Value);
            }
        }

        public SearchVM()
        {
            PropertyChanged += OnLocalPropertyChanged;
            SearchWithCategoriesCommand = new RelayCommand(OnSearchWithCategoriesCommand, CanExecuteSearchWithCategoriesCommand);
            SearchWithProductNameCommand = new RelayCommand(OnSearchWithProductNameCommand, CanExecuteSearchWithProductNameCommand);
        }

        private bool CanExecuteSearchWithCategoriesCommand(object arg)
        {
            return !string.IsNullOrWhiteSpace(SelectedPrimoLivello.Value); 
        }

        private bool CanExecuteSearchWithProductNameCommand(object arg)
        {
            return !string.IsNullOrWhiteSpace(ProductNameFilter);
        }

        private async void OnSearchWithProductNameCommand(object obj)
        {
            await Navigation.PushAsync(new Views.ProductsSearchResutlV(ProductNameFilter));
        }

        private async void OnSearchWithCategoriesCommand(object obj)
        {
            int curCategoria = 0;
            if (SelectedPrimoLivello.Key != 0) curCategoria = SelectedPrimoLivello.Key;
            if (SelectedSecondoLivello.Key != 0) curCategoria = SelectedSecondoLivello.Key;
            if (SelectedTerzoLivello.Key != 0) curCategoria = SelectedTerzoLivello.Key;
            await Navigation.PushAsync(new Views.ProductsSearchResutlV(curCategoria));
        }

        private void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, "SelectedPrimoLivello", StringComparison.InvariantCultureIgnoreCase))
            {
                secondoLivello = null;
                OnPropertyChanged("SecondoLivello");
                OnPropertyChanged("Picker2IsVisible");
                SearchWithCategoriesCommand.ChangeCanExecute();
            }
            if (string.Equals(e.PropertyName, "SelectedSecondoLivello", StringComparison.InvariantCultureIgnoreCase))
            {
                terzoLivello = null;
                OnPropertyChanged("TerzoLivello");
                OnPropertyChanged("Picker3IsVisible");
            }
                
        }

        ObservableCollection<KeyValuePair<int, string>> primoLivello = null;
        public ObservableCollection<KeyValuePair<int,string>> PrimoLivello
        {
            get
            {
                if (primoLivello == null && !loadPrimoLivello)
                {
                    LoadPrimoLivello();
                }
                return primoLivello;
            }
            set
            {
                primoLivello = value;
                OnPropertyChanged();
            }
        }

        bool loadPrimoLivello = false;
        public  async void LoadPrimoLivello()
        {
            //    if (primoLivello != null) SelectedPrimoLivello = primoLivello.FirstOrDefault();
            if (!loadPrimoLivello)
            {
                loadPrimoLivello = true;
                IEnumerable<Models.Categoria> categorieTmp = await DataStore.Categorie.GetItemsAsync(false);
                if (App.CurLang == "IT")
                    PrimoLivello  = new ObservableCollection<KeyValuePair<int, string>>(categorieTmp.Where(x => x.idPadre == 0).OrderBy(x => x.ordine).Select(x => new KeyValuePair<int, string>(x.idCategoria, x.nome)));
                else
                    PrimoLivello = new ObservableCollection<KeyValuePair<int, string>>(categorieTmp.Where(x => x.idPadre == 0).OrderBy(x => x.ordine).Select(x => new KeyValuePair<int, string>(x.idCategoria, x.nome_en)));
                loadPrimoLivello = false;
            }
                
            
        }

        KeyValuePair<int, string> selectedPrimoLivello;
        public KeyValuePair<int,string> SelectedPrimoLivello
        {
            get
            {
                return selectedPrimoLivello;
            }
            set
            {
                if (selectedPrimoLivello.Key != value.Key)
                {
                    selectedPrimoLivello = value;
                    OnPropertyChanged();
                }
            }
        }



        ObservableCollection<KeyValuePair<int, string>> secondoLivello = null;
        public ObservableCollection<KeyValuePair<int, string>> SecondoLivello
        {
            get
            {
                if (secondoLivello == null && !loadSecondoLivello)
                    LoadSecondoLivello();
                return secondoLivello;
            }
            set
            {
                secondoLivello = value;
                OnPropertyChanged();
            }
        }

        bool loadSecondoLivello = false;
        public async void LoadSecondoLivello()
        {
            if (!loadSecondoLivello)
            {
                loadSecondoLivello = true;
                if (SelectedPrimoLivello.Key == 0)
                    SecondoLivello = new ObservableCollection<KeyValuePair<int, string>>();
                else
                {
                    IEnumerable<Models.Categoria> categorieTmp = await DataStore.Categorie.GetItemsAsync(false);
                    var ritorno = await DataStore.Categorie.GetItemsAsync(false);
                    ritorno = ritorno.Where(x => x.idPadre == SelectedPrimoLivello.Key).OrderBy(x => x.ordine);
                    IEnumerable<KeyValuePair<int, string>> elementi;
                    if (App.CurLang == "IT")
                        elementi = ritorno.Select(x => new KeyValuePair<int, string>(x.idCategoria, x.nome));
                    else
                        elementi = ritorno.Select(x => new KeyValuePair<int, string>(x.idCategoria, x.nome_en));
                    SecondoLivello = new ObservableCollection<KeyValuePair<int, string>>(elementi);
                }
                loadSecondoLivello = false;
            }

        }

        KeyValuePair<int, string> selectedSecondoLivello;
        public KeyValuePair<int, string> SelectedSecondoLivello
        {
            get
            {
                return selectedSecondoLivello;
            }
            set
            {
                if (selectedSecondoLivello.Key != value.Key)
                {
                    selectedSecondoLivello = value;
                    OnPropertyChanged();
                }
            }
        }

        ObservableCollection<KeyValuePair<int, string>> terzoLivello = null;
        public ObservableCollection<KeyValuePair<int, string>> TerzoLivello
        {
            get
            {
                if (terzoLivello == null && !loadTerzoLivello)
                    LoadTerzoLivello();
                return terzoLivello;
            }
            set
            {
                terzoLivello = value;
                OnPropertyChanged();
            }
        }

        bool loadTerzoLivello = false;
        private async void LoadTerzoLivello()
        {
            if (!loadTerzoLivello)
            {
                loadTerzoLivello = true;
                if (SelectedSecondoLivello.Key == 0)
                    TerzoLivello = new ObservableCollection<KeyValuePair<int, string>>();
                else
                {
                    var ritorno = await DataStore.Categorie.GetItemsAsync(false);
                    ritorno = ritorno.Where(x => x.idPadre == selectedSecondoLivello.Key).OrderBy(x => x.ordine);
                    IEnumerable<KeyValuePair<int, string>> elementi;
                    if (App.CurLang == "IT")
                        elementi = ritorno.Select(x => new KeyValuePair<int, string>(x.idCategoria, x.nome));
                    else
                        elementi = ritorno.Select(x => new KeyValuePair<int, string>(x.idCategoria, x.nome_en));
                    TerzoLivello = new ObservableCollection<KeyValuePair<int, string>>(elementi);
                }
                loadTerzoLivello = false;
            }

        }

        KeyValuePair<int, string> selectedTerzoLivello;
        public KeyValuePair<int, string> SelectedTerzoLivello
        {
            get
            {
                return selectedTerzoLivello;
            }
            set
            {
                if (selectedTerzoLivello.Key != value.Key)
                {
                    selectedTerzoLivello = value;
                    OnPropertyChanged();
                }
            }
        }


    }
}
