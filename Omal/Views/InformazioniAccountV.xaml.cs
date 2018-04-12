using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Omal.Views
{
    public partial class InformazioniAccountV : ContentPage
    {

        ViewModels.ModificaAccountVM viewModel;

        public InformazioniAccountV()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel = new ViewModels.ModificaAccountVM();
            viewModel.NomeUtente = App.CurToken.NomeUtente;
            viewModel.EmailBackOffice = App.CurToken.email_per_backoffice;
            viewModel.Email = App.CurToken.email_utente;
            viewModel.CurPage = this;
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}
