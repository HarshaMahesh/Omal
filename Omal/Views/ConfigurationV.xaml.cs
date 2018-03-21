using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class ConfigurationV : ContentPage
    {
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            MessagingCenter.Send<Models.Messages.GotoWelcomeMessage>(new Models.Messages.GotoWelcomeMessage() { ChangeLanguage = true }, "");
            //Navigation.PushAsync(new WelcomeV(true));
        }

        ViewModels.ConfigurationVM Vm;

        public ConfigurationV()
        {
            BindingContext = Vm = new ViewModels.ConfigurationVM();
            Vm.CurPage = this;
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
