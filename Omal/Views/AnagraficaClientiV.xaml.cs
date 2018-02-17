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
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    sb.IsVisible = true;
                    break;
                case Device.Android:
                    esb.IsVisible = true;
                    break;

            }
        }
    }
}
