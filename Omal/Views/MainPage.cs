using System;
using Naxam.Controls.Forms;
using Xamarin.Forms;

namespace Omal
{
    public class MainPage : BottomTabbedPage
    {
        public MainPage()
        {
            Page SearchPage, AnagrafichePage, BasketPage, ContactOmalPage = null, ImpostazioniPage = null;
            SearchPage = new NavigationPage(new Views.SearchV()) { Title = "Cerca", Icon="Cerca.png" };
            AnagrafichePage = new NavigationPage(new Views.AnagraficheV()) { Title = "Anagrafiche", Icon = "Archivio.png"  };
            BasketPage = new NavigationPage(new Views.BasketV()) { Title = "Carrello", Icon ="Ordini.png" };
            ContactOmalPage = new NavigationPage(new Views.OmalContactPageV()) { Title = "Contatti Omal", Icon="Omal.png" };
            ImpostazioniPage = new NavigationPage(new Views.OmalContactPageV()) { Title = "Impostazioni", Icon = "Impostazioni.png" };
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
            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
