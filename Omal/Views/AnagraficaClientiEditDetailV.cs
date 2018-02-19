using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class AnagraficaClientiEditDetailV : ContentPage
    {
        ViewModels.AnagraficaClientiEditDetailVM viewModel;

        public AnagraficaClientiEditDetailV()
        {
            InitializeComponent();
        }

        public AnagraficaClientiEditDetailV(Models.Cliente curCliente):this()
        {
            BindingContext = viewModel = new ViewModels.AnagraficaClientiEditDetailVM();
            viewModel.CurCliente = curCliente;
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }

    }
}
