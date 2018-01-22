using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class AnagraficheV : ContentPage
    {

        ViewModels.AnagraficheVM viewModel; 

        public AnagraficheV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.AnagraficheVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }
    }
}
