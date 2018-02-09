using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class ConfigurationV : ContentPage
    {
        ViewModels.ConfigurationVM Vm;

        public ConfigurationV()
        {
            BindingContext = Vm = new ViewModels.ConfigurationVM();
            Vm.CurPage = this;
            InitializeComponent();
        }
    }
}
