using System;
using System.Collections.Generic;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                         await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                    else
                        return;
                }
            }
        }
    }
}
