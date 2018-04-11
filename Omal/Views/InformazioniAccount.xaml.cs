using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class InformazioniAccount : ContentPage
    {
        public InformazioniAccount()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel = new ViewModels.InformazioniAccountVM();
            viewModel.NomeUtente = App.CurToken.NomeUtente;
            viewModel.EmailBackOffice = App.CurToken.email_per_backoffice;
            viewModel.Email = App.CurToken.email_utente;
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

        }

        ViewModels.InformazioniAccountVM viewModel;

       


    }
}
