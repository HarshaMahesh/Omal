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

            Links.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    CrossShare.Current.OpenBrowser("http://www.omal.it");
                })
            });
        }



        protected async override void OnAppearing()
        {
            base.OnAppearing();
          

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
