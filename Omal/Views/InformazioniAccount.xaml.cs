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
            BindingContext = viewModel = new ViewModels.InformazioniAccountVM();
            viewModel.NomeUtente = App.CurUser.NomeUtente;
            viewModel.EmailBackOffice = App.CurToken.email_per_backoffice;
            viewModel.Email = App.CurToken.email_utente;
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
            NavigationPage.SetBackButtonTitle(this, "");
        }

        ViewModels.InformazioniAccountVM viewModel;

       


    }
}
