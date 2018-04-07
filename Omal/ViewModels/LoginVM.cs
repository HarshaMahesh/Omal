using System;
using Omal.Common;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class LoginVM:BaseVM
    {
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CanceLoginCommand { get; set; }
        public RelayCommand PswDimenticataCommand { get; set; }


        public string CurTitle { get { return TitoloLogin; } }
        bool isRunning = false;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {

                if (isRunning != value)
                {
                    isRunning = value;
                    OnPropertyChanged();
                }
            }
        }


        string email = Application.Current.Properties.ContainsKey("Email")?Application.Current.Properties["Email"].ToString():"";
        public string Email { 
            get
            {
                return email;
            }
            set
            {
                if (string.Equals(email, value, StringComparison.InvariantCultureIgnoreCase)) return;
                email = value;
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }
        string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (string.Equals(password, value, StringComparison.InvariantCultureIgnoreCase)) return;
                password = value;
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();

            }
        }

        string errore = string.Empty;
        public string Errore { 
            get
            {
                return errore;
            }
            set
            {
                errore = value;
                OnPropertyChanged();
            }
        
        }

        public LoginVM()
        {
            LoginCommand = new RelayCommand(OnLoginCommand, CanExecuteLoginCommand);
            CanceLoginCommand = new RelayCommand(OnCanceLoginCommand);
            PswDimenticataCommand = new RelayCommand(OnPswDimenticataCommand);
        }

        private async void OnPswDimenticataCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                await CurPage.DisplayAlert(TitoloLogin, StrEmailVuota, "Ok");
                return;
            }
            var ritorno = await DataStore.Utenti.RecoverPassword(Email);
            if (ritorno.HasError == 1)
            {
                await CurPage.DisplayAlert(TitoloLogin, LangIsIT ? ritorno.ErrorDescription : ritorno.ErrorDescription_En, "Ok");
            }
            else
            {
                await CurPage.DisplayAlert(TitoloLogin, StrConfermaInvioMailRecuperoPassword, "Ok");
                CanceLoginCommand.Execute(null);
            }
        }

        private bool CanExecuteLoginCommand(object arg)
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        }

        private async void OnCanceLoginCommand(object obj)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnLoginCommand(object obj)
        {
            IsRunning = true;
            try
            {
                var token = await DataStore.Utenti.Login(Email, Password);
                if (token == null)
                    throw new Exception("Email o password errata");
                else
                {
                    App.CurToken = token;
                    App.CurUser = new Models.Utente { Email = token.email_utente, IdUtente = token.IDUtente, NomeUtente = token.NomeUtente };
                    Application.Current.Properties["Email"] = Email;
                    await DataStore.Ordini.GetItemsAsync(true);
                    await DataStore.Clienti.GetItemsAsync(true);
                    MessagingCenter.Send(new Models.Messages.LoginOrLogoutActionMessage(), "LoginOrLogout");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Errore = ex.Message;
            }
            finally
            {
                IsRunning = false;
            }
        }
    }
}
