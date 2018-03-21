using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


namespace Omal.ViewModels
{
    public class ProductSearchResultVM: BaseVM
    {
        public string CurTitle { get { return TitoloProdotti; } }


        Models.Prodotto _SelectedProduct = null;
        public Models.Prodotto SelectedProduct
        {
            get
            {
                return _SelectedProduct;
            }
            set
            {
                if (value != null)
                { 
                    CurPage.Navigation.PushAsync(new Views.SearchProductV(value));
                }
            }
        }

        public ProductSearchResultVM()
        {
            PropertyChanged += OnLocalPropertyChanged;
        }

        private void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, "prodotti", StringComparison.InvariantCultureIgnoreCase)) OnPropertyChanged("NumeroProdotti");
        }

        string productFilter;
        public string ProductFilter{ 
            get{
                return productFilter;
            } 
            set{
                if (!string.Equals(productFilter, value,  StringComparison.InvariantCultureIgnoreCase))
                {
                    productFilter = value;
                    OnPropertyChanged();
                    _prodotti = null;
                    OnPropertyChanged("Prodotti");
                }
            } 
        }


        int? idCategoria;
        public int? IdCategoria { 
            get
            {
                return idCategoria;
            }
            set
            {
                if (idCategoria != value)
                {
                    idCategoria = value;
                    _prodotti = null;
                    OnPropertyChanged();
                    OnPropertyChanged("Prodotti");
                }
            }
        }



        ObservableCollection<Models.Prodotto> _prodotti = null;
        public  ObservableCollection<Models.Prodotto> Prodotti
        {
            get
            {
                if (_prodotti == null && !prodottiLoading)
                    LoadProdotti();
                return _prodotti;
            }
            set
            {
                _prodotti = value;
                OnPropertyChanged();
            }
        }

        bool prodottiLoading = false;

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


        async void LoadProdotti()
        {
            if (_prodotti == null && !prodottiLoading)
            {
                IsRunning = true;
                prodottiLoading = true;
                try
                {
                var originalProdotti = await DataStore.Prodotti.GetItemsAsync();
                var tmpProdotti = new List<Models.Prodotto>(originalProdotti);
                if (!string.IsNullOrWhiteSpace(ProductFilter))
                {
                    if (App.CurLang == "IT")
                        tmpProdotti = tmpProdotti.Where(x => x.nome.ToLower().Contains(ProductFilter.ToLower())).ToList();
                    else
                        tmpProdotti = tmpProdotti.Where(x => x.nome_en.ToLower().Contains(ProductFilter.ToLower())).ToList();
                    if (tmpProdotti == null || tmpProdotti.Count() == 0)
                    {
                        // nn ho prodotti, quindi cerco eventuali codici articoli che siano coerenti con la stringa.
                        var valvole = await DataStore.Valvole.GetItemsAsync();
                        var prodotti = valvole.Where(x => x.codice_articolo.ToLower().Contains(productFilter.ToLower())).Select(x => x.idprodotto).Distinct().ToList();
                        // cerco degli attuatori
                        var attuatori = await DataStore.Attuatori.GetItemsAsync();
                        var prodotti2 = attuatori.Where(x => x.codice_articolo.ToLower().Contains(productFilter.ToLower())).Select(x => x.idprodotto).Distinct().ToList();
                        prodotti.AddRange(prodotti2);
                        prodotti = prodotti.Distinct().ToList();
                        tmpProdotti = originalProdotti.Where(x => prodotti.Count(y => y == x.idprodotto) > 0).ToList();
                    }
                }
                else
                if (IdCategoria.HasValue)
                {
                    var categorieFiglie = GetCategorieFiglie(IdCategoria.Value);
                    tmpProdotti = tmpProdotti.Where(x => categorieFiglie.Contains(x.idcategoria)).ToList();
                }
                else
                        tmpProdotti = new List<Models.Prodotto>();
                foreach (var prodotto in tmpProdotti)
                {
                    if (!Uri.IsWellFormedUriString(prodotto.immagine_placeholder, UriKind.Absolute)) prodotto.immagine_placeholder = "http://wordpress.docsmarshal.it/wp-content/uploads/2018/02/Logo_DM_Docsmarshal.png"; 
                }
                Prodotti = new ObservableCollection<Models.Prodotto>(tmpProdotti);
                
                }
                finally
                {
                    prodottiLoading = false;
                    IsRunning = false;
                }

            }
        }

        public string NumeroProdotti 
        { 
            get
            {
                if (_prodotti == null || _prodotti.Count() == 0) return StrNessunProdottoTrovato;
                if (_prodotti.Count() == 1) return StrTrovatounSoloProdotto;
                return string.Format(StrTrovatiNrProdotti, _prodotti.Count());

            }
        }


        public List<int> GetCategorieFiglie(int categoria)
        {
            List<int> ritorno = new List<int>();
            ritorno.Add(categoria);
            var figlie= DataStore.Categorie.GetItemsAsync().Result.Where(x => x.idPadre == categoria);
            foreach (var figlia in figlie)
            {
                var locRit = GetCategorieFiglie(figlia.idCategoria);
                ritorno.AddRange(locRit);
            }
            return ritorno.Distinct().ToList();
        }
    }
}
