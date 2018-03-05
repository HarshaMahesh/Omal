using System;
using Omal.Common;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class LoginVM:BaseVM
    {
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CanceLoginCommand { get; set; }

        public string CurTitle { get { return TitoloLogin; } }


        string email;
        public string Email { 
            get
            {
                if (string.IsNullOrWhiteSpace(email) && Application.Current.Properties.ContainsKey("Email"))
                    email = Application.Current.Properties["Email"].ToString();
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
            var token = await DataStore.Utenti.Login(Email,Password);
            if (token == null)
                Errore = "Email o password errata";
            else
            {
                App.CurToken = token;
                App.CurUser = new Models.Utente { Email = token.email_utente, IdUtente = token.IDUtente, NomeUtente = token.NomeUtente, Password = Password };
                Application.Current.Properties["Email"] = Email;
                MessagingCenter.Send(new Models.Messages.LoginOrLogoutActionMessage(), "LoginOrLogout");
                await Navigation.PopAsync();
            }

        }
    }
}
