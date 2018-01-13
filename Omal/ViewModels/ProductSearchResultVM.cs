using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Omal.ViewModels
{
    public class ProductSearchResultVM: BaseVM
    {
        public ProductSearchResultVM()
        {
            _elencoProdotti = null;
        }
        public string ProductFilter{ get; set; }


        List<Models.Prodotto> _elencoProdotti { get; set; }
        public ObservableCollection<Models.Prodotto> ElencoProdotti
        {
            get{
                throw new NotImplementedException();

            }

        }
    }
}
