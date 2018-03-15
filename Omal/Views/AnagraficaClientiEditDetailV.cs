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
            NavigationPage.SetBackButtonTitle(this, "");
        }

        public AnagraficaClientiEditDetailV(Models.Cliente curCliente):this()
        {
            BindingContext = viewModel = new ViewModels.AnagraficaClientiEditDetailVM();
            viewModel.CurCliente = curCliente;
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }

        public AnagraficaClientiEditDetailV(Models.Cliente curCliente, Page ClientiDetailV) : this(curCliente)
        {
            BindingContext = viewModel = new ViewModels.AnagraficaClientiEditDetailVM();
            viewModel.CurCliente = curCliente;
            viewModel.PrevDetailPage = ClientiDetailV;
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }

    }
}
