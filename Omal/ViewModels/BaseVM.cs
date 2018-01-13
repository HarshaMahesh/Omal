using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class BaseVM: INotifyPropertyChanged
    {
        public BaseVM()
        {
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
