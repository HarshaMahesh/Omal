using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class InformazioniAccountVM: BaseVM
    {

        public ICommand EditCommand { get; set; }


        public InformazioniAccountVM()
        {
            EditCommand = new Command(OnEditCommand);
        }

        private void OnEditCommand(object obj)
        {
            throw new NotImplementedException();
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
