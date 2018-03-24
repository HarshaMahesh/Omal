using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class BasketV : ContentPage
    {
        ViewModels.BasketVM viewModel;

        public BasketV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.BasketVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
            NavigationPage.SetBackButtonTitle(this, "");

        }

        public void SetClienteIndex(int index)
        {
            ComboClienti.SelectedIndex = index;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
	}
}
