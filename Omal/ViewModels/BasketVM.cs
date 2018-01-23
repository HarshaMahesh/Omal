using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;

namespace Omal.ViewModels
{
    public class BasketVM:BaseVM
    {
        public ICommand RemoveFromBasketCommand { get; set; }


        ObservableCollection<Models.Cliente> clienti;
        public ObservableCollection<Models.Cliente> Clienti
        {
            get
            {
                if (clienti == null)
                    clienti = new ObservableCollection<Models.Cliente>(DataStore.Clienti.GetItemsAsync().Result.OrderBy((x=>x.RagioneSociale)));
                return clienti;
            }
        }

        public string NoteOrdine
        {
            get
            {
                return App.CurOrdine== null?string.Empty: App.CurOrdine.Note;
            }
            set
            {
                if (App.CurOrdine != null && !string.Equals(App.CurOrdine.Note, value, StringComparison.InvariantCultureIgnoreCase))
                {
                    App.CurOrdine.Note = value;
                    OnPropertyChanged();
                }

            }
        }

        Models.Cliente selectedCliente;
        public Models.Cliente SelectedCliente
        {
            get
            {
                return selectedCliente;
            }
            set
            {
                if (App.CurOrdine == null) return;
                selectedCliente = value;

                App.CurOrdine.IdCliente = selectedCliente.IdCliente;

                OnPropertyChanged();
            }
        }

        public BasketVM()
        {
                MessagingCenter.Subscribe<Models.Messages.LoginOrLogoutActionMessage>(this, "LoginOrLogout", sender =>
                {
                    ClearItems();
                     OnPropertyChanged("Items");
                });   
                MessagingCenter.Subscribe<Models.Messages.BasketEditedMessage>(this, "", sender =>
                {
                    ClearItems();
                    OnPropertyChanged("Items");
                    OnPropertyChanged("NumeroCarrelli");
                });
            RemoveFromBasketCommand = new Command(OnRemoveFromBasketCommand);
        }

        private void ClearItems()
        {
            if (items == null) return;
            foreach (var item in items)
            {
                item.PropertyChanged -= ItemPropertyChanges;
            }
            items.Clear();
            items = null;
        }

        private void OnRemoveFromBasketCommand(object obj)
        {
            int i = 0;
            i++;
        }


        ObservableCollection<Models.Carrello> items;
        public ObservableCollection<Models.Carrello> Items
        {
            get
            {
                if (items == null)
                {
                    items = new ObservableCollection<Models.Carrello>(DataStore.Carrello);
                    foreach (var item in items)
                    {
                        item.PropertyChanged += ItemPropertyChanges;
                    }
                }

                return items;
            }
        }

        private void ItemPropertyChanges(object sender, PropertyChangedEventArgs e)
        {
            if (App.CurOrdine == null) return;
            App.CurOrdine.Totale = items.Sum(x => x.PrezzoTotale);
            OnPropertyChanged("TotaleOrdine");
        }

        public double TotaleOrdine
        {
            get
            {
                if (App.CurOrdine == null) return 0;
                return App.CurOrdine.Totale;
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
