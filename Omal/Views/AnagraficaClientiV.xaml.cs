using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class AnagraficaClientiV : ContentPage
    {
        ViewModels.AnagraficaClientiVM viewModel;

        public AnagraficaClientiV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.AnagraficaClientiVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
           
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}
