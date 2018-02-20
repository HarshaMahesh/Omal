using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class DbUpdateV : ContentPage
    {
        ViewModels.DbUpdateVM viewModel;
        public bool FirstOpen = false;

        public DbUpdateV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.DbUpdateVM();
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
        }

        public Page MainPage { get; set; }

        public DbUpdateV(bool firstOpen):this()
        {
            FirstOpen = firstOpen; 
        }

        protected  async override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnUpdateDbCommand(string.Empty);
        }
    }
}
