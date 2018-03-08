using System;
using System.Collections.Generic;
using Plugin.Share;
using Xamarin.Forms;

namespace Omal.Views
{
    public partial class GruppoMetadatiProdottoDetailV : ContentPage
    {
        void Handle_Navigating(object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            if (e.Url.Contains("local_"))
            {
                var indice = e.Url.IndexOf("local_");
                string url = e.Url.Remove(0, indice + "local_".Length);
                if (!CrossShare.IsSupported) return;
                CrossShare.Current.OpenBrowser(url);
                e.Cancel = true;
            }
        }

        ViewModels.GruppoMetadatiProdottoDetailVM viewModel;



        public GruppoMetadatiProdottoDetailV()
        {
            BindingContext = viewModel = new ViewModels.GruppoMetadatiProdottoDetailVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
            NavigationPage.SetBackButtonTitle(this, "");
        }

        public GruppoMetadatiProdottoDetailV(Models.ProdottoGruppiMetadati curProdottoGruppoMetadati):this()
        {
            viewModel.CurGruppoProdottoMetadati = curProdottoGruppoMetadati;
            InitializeComponent();
        }


    }
}
