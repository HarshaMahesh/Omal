using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Omal.ViewModels
{
    public class ProductSearchResultVM: BaseVM
    {
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
                if (_prodotti == null)
                {
                    _prodotti = new ObservableCollection<Models.Prodotto>(DataStore.Prodotti.GetItemsAsync().Result);
                    if (!string.IsNullOrWhiteSpace(ProductFilter))
                        _prodotti = new ObservableCollection<Models.Prodotto>(_prodotti.Where(x => x.Nome.Contains(ProductFilter)));
                    else
                        if (IdCategoria.HasValue)
                    {
                        var categorieFiglie = GetCategorieFiglie(IdCategoria.Value);
                        _prodotti =new ObservableCollection<Models.Prodotto>( _prodotti.Where(x => categorieFiglie.Contains(x.IdCategoria)));
                    }
                    else
                        _prodotti = new  ObservableCollection<Models.Prodotto>();
                    OnPropertyChanged();
                    OnPropertyChanged("NumeroProdotti");
                }
                return _prodotti;
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
            var figlie= DataStore.Categorie.GetItemsAsync().Result.Where(x => x.IdPadre == categoria);
            foreach (var figlia in figlie)
            {
                var locRit = GetCategorieFiglie(figlia.IdCategoria);
                ritorno.AddRange(locRit);
            }
            return ritorno.Distinct().ToList();
        }
    }
}
