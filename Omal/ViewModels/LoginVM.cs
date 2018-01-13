using System;
using Omal.Common;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class LoginVM:BaseVM
    {
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CanceLoginCommand { get; set; }
        string email;
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
        public string Errore { get; set; }

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
            var utente = DataStore.Utenti.Login(Email,Password);
            if (utente == null)
                Errore = "Email o password errata";
            else
            {
                App.CurUser = utente;
                MessagingCenter.Send(new Models.Messages.LoginOrLogoutActionMessage(), "LoginOrLogout");
                await Navigation.PopModalAsync();
            }

        }
    }
}
