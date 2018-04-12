using System;
using System.Collections.Specialized;
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
            var tmpTraduzioni = new AppResources.Traduzioni();
            SearchPage = new CustomControls.CualevaNavigationPage(new Views.SearchV()) { Title = tmpTraduzioni.TitoloCerca, Icon="Cerca.png"  };
            AnagrafichePage = new CustomControls.CualevaNavigationPage(new Views.AnagraficheV()) { Title = tmpTraduzioni.TitoloArchivio, Icon = "Archivio.png"  };
            BasketPage = new CustomControls.CualevaNavigationPage(new Views.BasketV()) { Title = tmpTraduzioni.TitoloCarrello, Icon ="Ordini.png" };
            ContactOmalPage = new CustomControls.CualevaNavigationPage(new Views.OmalContactPageV()) { Title = tmpTraduzioni.TitoloContattiOmal, Icon="Omal.png" };
            ConfigurazioniPage = new Views.ConfigurationV();
            ImpostazioniPage = new CustomControls.CualevaNavigationPage(ConfigurazioniPage) { Title = tmpTraduzioni.TitoloImpostazioni, Icon = "Impostazioni.png" };
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

            MessagingCenter.Subscribe<Models.Messages.ChangeTabbedPageMessage>(this, "", sender =>
            {
                switch (sender.SetPage)
                {
                    case Models.Enums.EPages.Carrello:
                        CurrentPage = BasketPage;
                        break;
                    case Models.Enums.EPages.Search:
                        CurrentPage = SearchPage;
                        break;
                               
                    default:
                        break;
                }
            });
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
            try
            {
                base.OnCurrentPageChanged();
                Title = CurrentPage?.Title ?? string.Empty;
                if (CurrentPage == ImpostazioniPage)
                {
                    ImpostazioniPage.Navigation.PopToRootAsync();
                }
                if (requireUpdate)
                {
                    requireUpdate = false;
                    var Vm = (ViewModels.ConfigurationVM)ConfigurazioniPage.BindingContext;
                    Vm.OnUpdateDbCommand("Il sistema effettuerà l'importazione delle informazioni in loco prima di procedeere con l'utilizzo della App");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
