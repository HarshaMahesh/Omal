using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class InfoProductV : ContentPage
    {
        ViewModels.InfoProductVM viewModel;

        public InfoProductV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.InfoProductVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;

        }

        public InfoProductV(Models.Prodotto curProdotto) : this()
        {
            viewModel.CurProdotto = curProdotto;
        }
    }
}
