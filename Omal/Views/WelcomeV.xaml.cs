using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class WelcomeV : ContentPage
    {
        void Handle_Clicked_EN(object sender, System.EventArgs e)
        {
            App.CurLang = "EN";
            GoToMainPage();
        }

        async void GoToMainPage()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }

        }

        void Handle_Clicked_IT(object sender, System.EventArgs e)
        {
            App.CurLang = "IT";
            GoToMainPage();
        }

        public WelcomeV()
        {
            InitializeComponent();
        }
    }
}
