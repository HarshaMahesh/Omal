using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class ModificaAccountVM: BaseVM
    {

        public ICommand SaveCommand { get; set; }
        public ICommand EditCommand { get; set; }


        public ModificaAccountVM()
        {
            SaveCommand = new Command(OnSaveCommand);
            EditCommand = new Command(OnEditCommand);
        }

        private async void OnSaveCommand(object obj)
        {
            var ritorno = await DataStore.Utenti.UpdateCurUtente(NomeUtente, EmailBackOffice, Password, PasswordRepeat);
            if (ritorno.HasError != 1)
            {
                App.CurToken.email_per_backoffice = EmailBackOffice;
                App.CurToken.NomeUtente = NomeUtente;
                App.CurUser.NomeUtente = NomeUtente;
                App.CurToken = App.CurToken;
            }
            CurPage.DisplayAlert(TitoloModificaAccount, LangIsIT ? ritorno.ErrorDescription : ritorno.ErrorDescription_En, "ok");
            if (ritorno.HasError != 1)
            {
                CurPage.Navigation.PushAsync(new Views.InformazioniAccountV());
            }
        }
        private async void OnEditCommand(object obj)
        {
            CurPage.Navigation.PushAsync(new Views.ModificaAccountV());
        }

        public string Email
        {
            get;
            set;
        }


        string _NomeUtente;
        public string NomeUtente
        {
            get
            {
                return _NomeUtente;
            }
            set
            {
                if (!string.Equals(value,_NomeUtente, StringComparison.InvariantCultureIgnoreCase))
                {
                    _NomeUtente = value;
                    OnPropertyChanged();
                }
            }
        }

        string _EmailBackOffice;
        public string EmailBackOffice
        {
            get
            {
                return _EmailBackOffice;
            }
            set
            {
                if (!string.Equals(value, _EmailBackOffice, StringComparison.InvariantCultureIgnoreCase))
                {
                    _EmailBackOffice = value;
                    OnPropertyChanged();
                }
            }
        }

        string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (!string.Equals(value, _Password, StringComparison.InvariantCultureIgnoreCase))
                {
                    _Password = value;
                    OnPropertyChanged();
                }
            }
        }

        string _PasswordRepeat;
        public string PasswordRepeat
        {
            get
            {
                return _PasswordRepeat;
            }
            set
            {
                if (!string.Equals(value, _PasswordRepeat, StringComparison.InvariantCultureIgnoreCase))
                {
                    _PasswordRepeat = value;
                    OnPropertyChanged();
                }
            }
        }

     
    }
}
