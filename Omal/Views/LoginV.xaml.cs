using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class LoginV : ContentPage
    {

        ViewModels.LoginVM viewModel;

        public LoginV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.LoginVM();
            viewModel.Navigation = Navigation;
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}
