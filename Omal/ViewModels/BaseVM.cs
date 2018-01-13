using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Omal.Common;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class BaseVM: INotifyPropertyChanged
    {

        public RelayCommand LoginOrLogoutCommand { get; set; }
        public Page CurPage { get; set; }

        public BaseVM()
        {
            LoginOrLogoutCommand = new RelayCommand(OnLoginOrLogoutCommand);
            MessagingCenter.Subscribe<Models.Messages.LoginOrLogoutActionMessage>(this, "LoginOrLogout", sender =>
            {
                LoginOrLogoutCommand.ChangeCanExecute();
                OnPropertyChanged("LoginOrLogOutActionText");
            });
        }

        public string LoginOrLogOutActionText { get { if (App.CurUser == null) return "Login"; else return "Logout"; }}

        private async void OnLoginOrLogoutCommand(object obj)
        {
            if (App.CurUser == null)
            {
                if (Navigation == null) {
                    if (CurPage != null) await CurPage.DisplayAlert("Alert", "Navigation null", "OK");
                    return;
                }
                await Navigation.PushModalAsync(new Views.LoginV());
            } else
            {
                string action=""; 
                if (CurPage != null) 
                {
                    action = await CurPage.DisplayActionSheet("Confermi il logout?", "No", "Si", "Login con altro utente");
                    if (action == "No") return;
                }
                App.CurUser = null;
                if (action == "Login con altro utente") OnLoginOrLogoutCommand(obj);
                MessagingCenter.Send(new Models.Messages.LoginOrLogoutActionMessage(), "LoginOrLogout");
            }
                
        }

        public Services.IOmalDataStore DataStore => DependencyService.Get<Services.IOmalDataStore>() ?? new Services.MockOmalDataStore();
        public INavigation Navigation { get; set; }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
