using System;

using Xamarin.Forms;

namespace Omal
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page SearchPage, AnagrafichePage, BasketPage, InfoProductPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    
                    SearchPage = new NavigationPage(new Views.SearchV()){Title = "Cerca"};
                    AnagrafichePage = new NavigationPage(new Views.AnagraficheV()) { Title = "Anagrafiche" };
                    BasketPage = new NavigationPage(new Views.BasketV()) { Title = "Carrello" };
//                    InfoProductPage = new NavigationPage(new Views.InfoProductV()) { Title = "Informazioni Prodotto" };
                    SearchPage.Icon = "Search.png";
                    AnagrafichePage.Icon = "OrdersPage.png";
                    BasketPage.Icon = "BasketPage.png";
  //                  InfoProductPage.Icon = "Info.png";
                    break;
                default:
                    SearchPage = new NavigationPage(new Views.SearchV()) { Title = "Cerca" };
                    AnagrafichePage = new NavigationPage(new Views.AnagraficheV()) { Title = "Anagrafiche" };
                    BasketPage = new Views.BasketV() { Title = "Carrello" };
              //    InfoProductPage = new Views.InfoProductV() { Title = "Informazioni Prodotto" };
                    break;
            }

            Children.Add(SearchPage);
            Children.Add(AnagrafichePage);
            Children.Add(BasketPage);
       //     Children.Add(InfoProductPage);
            NavigationPage.SetHasNavigationBar(this, false);
            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
