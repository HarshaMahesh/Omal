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

        }
    }
}
