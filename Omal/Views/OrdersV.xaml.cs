using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class OrdersV : ContentPage
    {
        ViewModels.OrdersVM viewModel;

        public OrdersV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.OrdersVM();
            viewModel.Navigation = Navigation;
            viewModel.CurPage = this;
            NavigationPage.SetBackButtonTitle(this, "");
        }

		protected override void OnAppearing()
		{
            base.OnAppearing();
            ListaOrdini.SelectedItem = null;
		}
	}
}
