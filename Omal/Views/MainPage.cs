using System;

using Xamarin.Forms;

namespace Omal
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page SearchPage, OrdersPage, BasketPage, InfoProductPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    
                    SearchPage = new NavigationPage(new Views.SearchV()){Title = "Cerca"};
                    OrdersPage = new NavigationPage(new Views.OrdersV()) { Title = "Ordini" };
                    BasketPage = new NavigationPage(new Views.BasketV()) { Title = "Carrello" };
//                    InfoProductPage = new NavigationPage(new Views.InfoProductV()) { Title = "Informazioni Prodotto" };
                    SearchPage.Icon = "Search.png";
                    OrdersPage.Icon = "OrdersPage.png";
                    BasketPage.Icon = "BasketPage.png";
  //                  InfoProductPage.Icon = "Info.png";
                    break;
                default:
                    SearchPage =new Views.SearchV() { Title = "Cerca" };
                    OrdersPage = new Views.OrdersV() { Title = "Ordini" };
                    BasketPage = new Views.BasketV() { Title = "Carrello" };
              //      InfoProductPage = new Views.InfoProductV() { Title = "Informazioni Prodotto" };
                    break;
            }

            Children.Add(SearchPage);
            Children.Add(OrdersPage);
            Children.Add(BasketPage);
       //     Children.Add(InfoProductPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
