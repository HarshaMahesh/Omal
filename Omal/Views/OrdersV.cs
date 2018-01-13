using System;

using Xamarin.Forms;

namespace Omal.Views
{
    public class OrdersV : ContentPage
    {
        public OrdersV()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

