using System;
using System.Collections.Generic;
using Plugin.Share;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class DocumentiDownloadV : ContentPage
    {
        ViewModels.DocumentiDownloadVM viewModel;
        public DocumentiDownloadV()
        {
            BindingContext = viewModel = new ViewModels.DocumentiDownloadVM();
            InitializeComponent();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
            NavigationPage.SetBackButtonTitle(this, "");
        }

        public DocumentiDownloadV(Models.Prodotto curProdotto):this()
        {
            viewModel.CurProdotto = curProdotto;

        }


		protected override void OnAppearing()
		{
            base.OnAppearing();
            ElencoPdf.SelectedItem = null;
		}

        public void OpenB(string url)
        {
            if (!CrossShare.IsSupported) return;
            CrossShare.Current.OpenBrowser(url);

        }
	}
}
