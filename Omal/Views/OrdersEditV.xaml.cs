using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class OrdersEditV : ContentPage
    {
        ViewModels.OrdersEditVM viewModel;

        public OrdersEditV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.OrdersEditVM();
            viewModel.Navigation = Navigation;
            viewModel.CurPage = this;
        }


    }
}
