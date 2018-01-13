﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Omal.Common;

namespace Omal.ViewModels
{
    public class SearchVM: BaseVM
    {

        public RelayCommand SearchWithCategoriesCommand{get;set;}
        public RelayCommand SearchWithProductNameCommand { get; set; }

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

        public SearchVM()
        {
            PropertyChanged += OnLocalPropertyChanged;
            SearchWithCategoriesCommand = new RelayCommand(OnSearchWithCategoriesCommand);
            SearchWithProductNameCommand = new RelayCommand(OnSearchWithProductNameCommand, CanExecuteSearchWithProductNameCommand);
        }

        private bool CanExecuteSearchWithProductNameCommand(object arg)
        {
             return !string.IsNullOrWhiteSpace(ProductNameFilter); 
        }

        private async void OnSearchWithProductNameCommand(object obj)
        {
            await Navigation.PushAsync(new Views.ProductsSearchResutlV(ProductNameFilter));
        }

        private async void OnSearchWithCategoriesCommand()
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
            }
            if (string.Equals(e.PropertyName, "SelectedSecondoLivello", StringComparison.InvariantCultureIgnoreCase))
            {
                terzoLivello = null;
                OnPropertyChanged("TerzoLivello");
            }
                
        }

        ObservableCollection<KeyValuePair<int, string>> primoLivello = null;
        public ObservableCollection<KeyValuePair<int,string>> PrimoLivello
        {
            get
            {
                if (primoLivello == null)
                {
                    if (App.CurLang == "IT")
                        primoLivello = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Categorie.GetItemsAsync(false).Result.Where(x => x.IdPadre == 0).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCategoria, x.Nome)));
                    else
                        primoLivello = new ObservableCollection<KeyValuePair<int, string>>(DataStore.Categorie.GetItemsAsync(false).Result.Where(x => x.IdPadre == 0).OrderBy(x => x.Ordine).Select(x => new KeyValuePair<int, string>(x.IdCategoria, x.NomeEn)));
                //    if (primoLivello != null) SelectedPrimoLivello = primoLivello.FirstOrDefault();
                }
                return primoLivello;
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
                if (secondoLivello == null)
                {
                    if (SelectedPrimoLivello.Key == 0)
                        secondoLivello = new ObservableCollection<KeyValuePair<int, string>>();
                    else
                    {
                        var ritorno = DataStore.Categorie.GetItemsAsync(false).Result.Where(x => x.IdPadre == SelectedPrimoLivello.Key).OrderBy(x => x.Ordine);
                        IEnumerable<KeyValuePair<int, string>> elementi;
                        if (App.CurLang == "IT")
                            elementi = ritorno.Select(x => new KeyValuePair<int, string>(x.IdCategoria, x.Nome));
                        else
                            elementi = ritorno.Select(x => new KeyValuePair<int, string>(x.IdCategoria, x.NomeEn));
                        secondoLivello = new ObservableCollection<KeyValuePair<int, string>>(elementi);
                    }
               //     if (secondoLivello != null) SelectedPrimoLivello = secondoLivello.FirstOrDefault();
                }

                return secondoLivello;
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
                if (terzoLivello == null)
                {
                    if (SelectedSecondoLivello.Key == 0)
                        terzoLivello = new ObservableCollection<KeyValuePair<int, string>>();
                    else
                    {
                        var ritorno = DataStore.Categorie.GetItemsAsync(false).Result.Where(x => x.IdPadre == selectedSecondoLivello.Key).OrderBy(x => x.Ordine);
                        IEnumerable<KeyValuePair<int, string>> elementi;
                        if (App.CurLang == "IT")
                            elementi = ritorno.Select(x => new KeyValuePair<int, string>(x.IdCategoria, x.Nome));
                        else
                            elementi = ritorno.Select(x => new KeyValuePair<int, string>(x.IdCategoria, x.NomeEn));
                        terzoLivello = new ObservableCollection<KeyValuePair<int, string>>(elementi);
                    }
                    //     if (secondoLivello != null) SelectedPrimoLivello = secondoLivello.FirstOrDefault();
                }

                return terzoLivello;
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
