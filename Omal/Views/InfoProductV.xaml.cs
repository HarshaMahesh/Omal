using System;
using System.Collections.Generic;
using Plugin.Share;
using Xamarin.Forms;

namespace Omal.Views
{
    public partial class InfoProductV : ContentPage
    {
        ViewModels.InfoProductVM viewModel;

        public InfoProductV()
        {
            BindingContext = viewModel = new ViewModels.InfoProductVM();
            InitializeComponent();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
            NavigationPage.SetBackButtonTitle(this, "");
        }

        public InfoProductV(Models.Prodotto curProdotto) : this()
        {
            viewModel.CurProdotto = curProdotto;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CurGruppo.SelectedItem = null;
            ElencoPdf.SelectedItem = null;
        }

        public void OpenB(string url)
        {
            if (!CrossShare.IsSupported) return;
            CrossShare.Current.OpenBrowser(url);

        }

    }
}
