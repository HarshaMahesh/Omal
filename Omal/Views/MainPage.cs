﻿using System;
using Naxam.Controls.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace Omal
{
    public class MainPage : BottomTabbedPage
    {
        Page SearchPage, AnagrafichePage, BasketPage, ContactOmalPage = null, ImpostazioniPage = null, ConfigurazioniPage;
        public MainPage()
        {
            
            SearchPage = new NavigationPage(new Views.SearchV()) { Title = "Cerca", Icon="Cerca.png" };
            AnagrafichePage = new NavigationPage(new Views.AnagraficheV()) { Title = "Archivio", Icon = "Archivio.png"  };
            BasketPage = new NavigationPage(new Views.BasketV()) { Title = "Carrello", Icon ="Ordini.png" };
            ContactOmalPage = new NavigationPage(new Views.OmalContactPageV()) { Title = "Contatti Omal", Icon="Omal.png" };
            ConfigurazioniPage = new Views.ConfigurationV();
            ImpostazioniPage = new NavigationPage(ConfigurazioniPage) { Title = "Impostazioni", Icon = "Impostazioni.png" };
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
            }

            Children.Add(SearchPage);
            Children.Add(AnagrafichePage);
            Children.Add(BasketPage);
            Children.Add(ContactOmalPage);
            Children.Add(ImpostazioniPage);
            if (!App.LastUpdate.HasValue)
            {
                requireUpdate = true;
                CurrentPage = ImpostazioniPage;

            } else
            Title = Children[0].Title;
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

        bool requireUpdate = false;

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
            if (requireUpdate)
            {
                requireUpdate = false;
                var Vm = (ViewModels.ConfigurationVM) ConfigurazioniPage.BindingContext;
                Vm.OnUpdateDbCommand("Il sistema effettuerà l'importazione delle informazioni in loco prima di procedeere con l'utilizzo della App");
            }
        }
    }
}
