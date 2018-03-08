using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class ProductsSearchResutlV : ContentPage
    {
        ViewModels.ProductSearchResultVM viewModel;

        public ProductsSearchResutlV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.ProductSearchResultVM();
            viewModel.Navigation = Navigation;
            viewModel.CurPage = this;
            NavigationPage.SetBackButtonTitle(this, "");
        }

        public ProductsSearchResutlV(int idCategoria):this()
        {
            viewModel.IdCategoria = idCategoria;

        }

        public ProductsSearchResutlV(string productNameFilter) : this()
        {
            viewModel.ProductFilter = productNameFilter;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ElencoProdotti.SelectedItem = null;
        }
    }
}
