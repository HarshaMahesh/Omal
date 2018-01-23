using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;

namespace Omal.ViewModels
{
    public class BasketVM:BaseVM
    {
        public ICommand RemoveFromBasketCommand { get; set; }

        public BasketVM()
        {
                MessagingCenter.Subscribe<Models.Messages.LoginOrLogoutActionMessage>(this, "LoginOrLogout", sender =>
                {
                     OnPropertyChanged("Items");
                });   
                MessagingCenter.Subscribe<Models.Messages.BasketEditedMessage>(this, "", sender =>
                {
                     OnPropertyChanged("Items");
                    OnPropertyChanged("NumeroCarrelli");
                });
            RemoveFromBasketCommand = new Command(OnRemoveFromBasketCommand);
        }

        private void OnRemoveFromBasketCommand(object obj)
        {
            int i = 0;
            i++;
        }

        public ObservableCollection<Models.Carrello> Items
        {
            get
            {
                return new ObservableCollection<Models.Carrello>(DataStore.Carrello);
            }
        }


        public string NumeroCarrelli
        {
            get
            {
                var totale = DataStore.Carrello.Sum(x=>x.Qta);
                if (totale == 0) return "Carrello vuoto";
                if (totale == 1) return "1 articolo nel carrello";
                return string.Format("{0} articoli nel carrello",totale);
            }
        }
    }
}
