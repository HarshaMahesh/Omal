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
            BindingContext = viewModel = new ViewModels.AnagraficaClientiEditDetailVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }

        public AnagraficaClientiEditDetailV(Models.Cliente curCliente):this()
        {
            viewModel.CurCliente = curCliente;
        }

    }
}
