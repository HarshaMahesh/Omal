using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class SearchV : ContentPage
    {
        async void  Login_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LoginV());
        }

        ViewModels.SearchVM viewModel;

        public SearchV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.SearchVM();
            viewModel.Navigation = Navigation;
            viewModel.CurPage = this;
            NavigationPage.SetBackButtonTitle(this, "");
        }

        async void OnCompletedCodiceProdotto(object sender, System.EventArgs e)
        {
            viewModel.SearchWithProductCodeCommand.Execute(null);
        }

        async void OnCompletedNameProdotto(object sender, System.EventArgs e)
        {
            viewModel.SearchWithProductNameCommand.Execute(null);
        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
