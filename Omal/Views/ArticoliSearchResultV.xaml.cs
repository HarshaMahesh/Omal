using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class ArticoliSearchResultV : ContentPage
    {
        void Handle_Navigating(object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            viewModel.Navigating(e.Url);
            e.Cancel = true;
        }


        void Handle_Navigated(object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
        }

        ViewModels.ArticoliSearchResultVM viewModel; 

        public ArticoliSearchResultV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.ArticoliSearchResultVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }

        public ArticoliSearchResultV (Models.Prodotto prodotto, bool prodottoIsValvola, IEnumerable<Models.Base> articoli):this()
        {
            viewModel.prodottoIsValvola = prodottoIsValvola;
            viewModel.Articoli = articoli;
            viewModel.CurProdotto = prodotto;
        }
    }
}
