using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Omal.Models
{
    public class Base: INotifyPropertyChanged
    {
        public Base()
        {
        }

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
