using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Omal.ViewModels
{
    public class ProductSearchResultVM: BaseVM
    {
        public string CurTitle { get { return "Prodotti"; } }


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
        async void LoadProdotti()
        {
            if (_prodotti == null && !prodottiLoading)
            {
                prodottiLoading = true;
                var tmpProdotti = await DataStore.Prodotti.GetItemsAsync();
                if (!string.IsNullOrWhiteSpace(ProductFilter))
                {
                    if (App.CurLang == "IT")
                        tmpProdotti = tmpProdotti.Where(x => x.nome.Contains(ProductFilter));
                    else
                        tmpProdotti = tmpProdotti.Where(x => x.nome_en.Contains(ProductFilter));
                }
                else
                if (IdCategoria.HasValue)
                {
                    var categorieFiglie = GetCategorieFiglie(IdCategoria.Value);
                    tmpProdotti =tmpProdotti.Where(x => categorieFiglie.Contains(x.idcategoria));
                }
                else
                    tmpProdotti = new ObservableCollection<Models.Prodotto>();
                foreach (var prodotto in tmpProdotti)
                {
                    if (!Uri.IsWellFormedUriString(prodotto.immagine_placeholder, UriKind.Absolute)) prodotto.immagine_placeholder = "http://wordpress.docsmarshal.it/wp-content/uploads/2018/02/Logo_DM_Docsmarshal.png"; 
                }
                Prodotti = new ObservableCollection<Models.Prodotto>(tmpProdotti);
                prodottiLoading = false;
            }
        }

        public string NumeroProdotti { 
            get
            {
                if (Prodotti == null || Prodotti.Count() == 0) return "Nessun prodotto trovato";
                if (Prodotti.Count() == 1) return "Trovato un solo prodotto";
                return string.Format("Trovati {0} prodotti", Prodotti.Count());

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
