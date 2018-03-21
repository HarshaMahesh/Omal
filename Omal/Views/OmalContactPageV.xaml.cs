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
        ViewModels.OmalContactPageVM viewModel;

        public OmalContactPageV()
        {
            InitializeComponent();
            MyMap.IsVisible = false;
            MyMap2.IsVisible = false;
            Sito.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    CrossShare.Current.OpenBrowser("http://www.omal.it");
                })
            });
            Email.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    Device.OpenUri(new Uri("mailto:info@omal.it"));
                })
            });
            Tel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    Device.OpenUri(new Uri("tel:+390308900145"));
                })
            });
           
            NavigationPage.SetBackButtonTitle(this, "");
            BindingContext = viewModel = new ViewModels.OmalContactPageVM();
            viewModel.Navigation = Navigation;
            viewModel.CurPage = this;
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
            try
            {
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

                var address = "Italia, Rodengo Saiano, Via Ponte Nuovo, 11";
                Pin p = new Pin() { Position = new Position(45.598083, 10.090149), Address = address, Label = "OMAL Spa", Type = PinType.Place };
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(p.Position, Distance.FromMeters(200)));
                MyMap.Pins.Add(p);

                var address2 = "Italia, Rodengo Saiano, Via Ponte Nuovo, 11";
                Pin p2 = new Pin() { Position = new Position(45.597212, 10.088550), Address = address2, Label = "OMAL Spa", Type = PinType.Place };
                MyMap2.MoveToRegion(MapSpan.FromCenterAndRadius(p2.Position, Distance.FromMeters(200)));
                MyMap2.Pins.Add(p2);


                /*geoCoder = new Geocoder();

                var approximateLocations = await geoCoder. GetPositionsForAddressAsync(address);
                foreach (var position in approximateLocations)
                {
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(200)));
                    MyMap.Pins.Add(new Pin() { Type = PinType.Place, Address = address, Position = position, Label = "OMAL Spa" });
                }
                address = "Italia, Passirano, Via Brognolo, 12";
                approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
                foreach (var position in approximateLocations)
                {
                    MyMap2.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(200)));
                    MyMap2.Pins.Add(new Pin() { Type = PinType.Place, Address = address, Position = position, Label = "OMAL Spa" });
                }
                */
            }
            catch (Exception ex)
            {
                await DisplayAlert(viewModel.TitoloContattiOmal, ex.Message, "ok");
            }
        }
    }
}
