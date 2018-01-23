using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class BasketV : ContentPage
    {
        ViewModels.BasketVM viewModel;

        public BasketV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.BasketVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }
    }
}
