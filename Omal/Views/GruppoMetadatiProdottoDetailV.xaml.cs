using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class GruppoMetadatiProdottoDetailV : ContentPage
    {
        ViewModels.GruppoMetadatiProdottoDetailVM viewModel;



        public GruppoMetadatiProdottoDetailV()
        {
            BindingContext = viewModel = new ViewModels.GruppoMetadatiProdottoDetailVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }

        public GruppoMetadatiProdottoDetailV(Models.ProdottoGruppiMetadati curProdottoGruppoMetadati):this()
        {
            viewModel.CurGruppoProdottoMetadati = curProdottoGruppoMetadati;
            InitializeComponent();
        }


    }
}
