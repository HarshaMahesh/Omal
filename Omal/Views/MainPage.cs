using System;
using Naxam.Controls.Forms;
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
