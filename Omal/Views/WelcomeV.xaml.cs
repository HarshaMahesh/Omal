using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class WelcomeV : ContentPage
    {
        async void Handle_Clicked_EN(object sender, System.EventArgs e)
        {
            App.CurLang = "EN";
            await Navigation.PushModalAsync(new MainPage());
        }

        async void Handle_Clicked_IT(object sender, System.EventArgs e)
        {
            App.CurLang = "IT";
            await Navigation.PushModalAsync(new MainPage());

        }

        public WelcomeV()
        {
            InitializeComponent();
        }
    }
}
