using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Omal.Common;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class BaseVM: AppResources.Traduzioni, INotifyPropertyChanged
    {
        public virtual string LoggedUserName {
            get
            {
                if (App.CurUser == null) return string.Empty;
                return App.CurUser.NomeUtente;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return App.CurUser != null;
            }
        }

        public RelayCommand LoginOrLogoutCommand { get; set; }

        public BaseVM()
        {
            LoginOrLogoutCommand = new RelayCommand(OnLoginOrLogoutCommand);
            MessagingCenter.Subscribe<Models.Messages.LoginOrLogoutActionMessage>(this, "LoginOrLogout", sender =>
            {
                LoginOrLogoutCommand.ChangeCanExecute();
                OnPropertyChanged("LoginOrLogOutActionText");
                OnPropertyChanged("LoggedUserName");
                OnPropertyChanged("IsLoggedIn");
            });
        }

        public string LoginOrLogOutActionText { get { if (App.CurUser == null) return "Login"; else return App.CurUser.NomeUtente; }}

        private async void OnLoginOrLogoutCommand(object obj)
        {
            if (App.CurUser == null)
            {
                if (Navigation == null) {
                    if (CurPage != null) await CurPage.DisplayAlert("Alert", "Navigation null", "OK");
                    return;
                }
                await Navigation.PushAsync(new Views.LoginV());
            } else
            {
                bool action; 
                if (CurPage != null) 
                {
                    action = await CurPage. DisplayAlert("logout",StrConfermiLogout, StrSi, StrNo);
                    if (!action) return;
                }
                App.CurUser = null;
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

        public Page CurPage { get; set; }

        public bool LangIsIT
        {
            get
            {
                return !String.IsNullOrWhiteSpace(App.CurLang) && string.Equals(App.CurLang.ToUpper(), "IT");
            }
        }
    }
}
