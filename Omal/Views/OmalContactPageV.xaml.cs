using System;
using System.Collections.Generic;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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

           /* Telefono.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    Device.OpenUri(new Uri("tel:0308900145"));
                })
            });*/
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

                    var results = CrossPermissions.Current.RequestPermissionsAsync(Permission.Location).Result;

                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }
            }
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
