using System;
using System.Collections.Generic;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Share;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Omal.Views
{
    public partial class OmalContactPageV : ContentPage
    {
        Geocoder geoCoder;

        public OmalContactPageV()
        {
            InitializeComponent();
            MyMap.IsVisible = false;
            MyMap2.IsVisible = false;
            Links.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    CrossShare.Current.OpenBrowser("http://www.omal.it");
                })
            });
            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            bool mostra = true;
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
                mostra = status == PermissionStatus.Granted;
            }
            MyMap.IsVisible = mostra;
            MyMap2.IsVisible = mostra;
           
                geoCoder = new Geocoder();
                var address = "Italia, Rodengo Saiano, Via Ponte Nuovo, 11";
                var approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
                foreach (var position in approximateLocations)
                {
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(200)));
                    MyMap.Pins.Add(new Pin() { Type = PinType.Place, Address = address, Position = position, Label = "Omal Spa" });
                }
                address = "Italia, Passirano, Via Brognolo, 12";
                approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
                foreach (var position in approximateLocations)
                {
                    MyMap2.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(200)));
                    MyMap2.Pins.Add(new Pin() { Type = PinType.Place, Address = address, Position = position, Label = "Omal Spa" });
                }
        }
    }
}
