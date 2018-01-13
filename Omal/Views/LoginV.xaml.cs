using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class LoginV : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public LoginV()
        {
            InitializeComponent();
        }
    }
}
