using System;
using System.Collections.Generic;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace Omal.Views
{
    public partial class WelcomeV : CustomControls.CualevaGradientContentPage
    {
      

      
       
        ViewModels.WelcomeVM viewModel;

        public WelcomeV()
        {
            InitializeComponent();
            StartColor = Color.FromHex("#005FAA");
            EndColor = Color.FromHex("#7FBADE");
            BindingContext = viewModel = new ViewModels.WelcomeVM();
            viewModel.Navigation = Navigation;
            viewModel.CurPage = this;
            NavigationPage.SetBackButtonTitle(this, "");
        }



        public WelcomeV(bool changeLanguage):this()
        {
            viewModel.ChangeLanguage = changeLanguage;
        }

		protected override void OnAppearing()
		{
            base.OnAppearing();
            if (!string.IsNullOrWhiteSpace(App.CurLang) && !viewModel.ChangeLanguage) viewModel.UpdateDb();
		}



	}
}
