using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class AnagraficaClientiDetailV : ContentPage
    {
        ViewModels.AnagraficaClientiDetailVM viewModel;

        public AnagraficaClientiDetailV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.AnagraficaClientiDetailVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
            NavigationPage.SetBackButtonTitle(this, "");
        }

        public AnagraficaClientiDetailV(Models.Cliente curCliente):this()
        {
            viewModel.CurCliente = curCliente;
        }

    }
}
