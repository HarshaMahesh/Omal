using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using Omal.Common;

namespace Omal.ViewModels
{
    public class BasketVM:BaseVM
    {
        public RelayCommand SalvaCommand { get; set; }
        public RelayCommand InviaCommand { get; set; }
        public RelayCommand AnnullaCommand { get; set; }

        public BasketVM()
        {
            MessagingCenter.Subscribe<Models.Messages.LoginOrLogoutActionMessage>(this, "LoginOrLogout", sender =>
            {
                ClearItems();
                OnPropertyChanged("Items");
                OnPropertyChanged("NumeroCarrelli");

            });
            MessagingCenter.Subscribe<Models.Messages.BasketEditedMessage>(this, "", sender =>
            {
                ClearItems();
                OnPropertyChanged("Items");
                OnPropertyChanged("NumeroCarrelli");
            });
            MessagingCenter.Subscribe<Models.Messages.ClienteInsertedOrUpdatedMessage>(this, "", sender =>
            {
                LoadClienti();
            });
            SalvaCommand = new RelayCommand(OnSalvaCommand,CanSaveCommand);
            InviaCommand = new RelayCommand(OnInviaCommand, CanInviaCommand);
            AnnullaCommand = new RelayCommand(OnAnnullaCommand, CanAnnullaCommand);
            PropertyChanged += LocalPropertyChanged;
        }

        private bool CanAnnullaCommand(object arg)
        {
            return App.CurOrdine != null && DataStore.Carrello.Count > 0;
        }

        private bool CanInviaCommand(object arg)
        {
            return App.CurOrdine != null && App.CurOrdine.IdCliente != 0 && DataStore.Carrello.Count > 0;
        }

        private void LocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals("SelectedCliente", e.PropertyName, StringComparison.InvariantCultureIgnoreCase))
            {
                SalvaCommand.ChangeCanExecute();
                InviaCommand.ChangeCanExecute();
            }
        }

        private bool CanSaveCommand(object arg)
        {
            return App.CurOrdine != null && App.CurOrdine.IdCliente != 0 && DataStore.Carrello.Count > 0;
        }

        private async void OnAnnullaCommand(object obj)
        {
            var answer = await CurPage.DisplayAlert("Question?", "Sei sicuro di voler anullare l'ordine corrente?", "Si", "No");
            if (answer)
            {
                DataStore.Carrello.Clear();
                ClearItems();
                App.CurOrdine.Totale = 0;
                OnPropertyChanged("Items");
                OnPropertyChanged("TotaleOrdine");
                OnPropertyChanged("NumeroCarrelli");
                SalvaCommand.ChangeCanExecute();
                InviaCommand.ChangeCanExecute();
                AnnullaCommand.ChangeCanExecute();
            }
        }

        Models.Carrello _SelectedItem;
        public Models.Carrello SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (value != null)
                    OnPropertyChanged();
            }
        }



        private void OnInviaCommand(object obj)
        {
            CurPage.DisplayAlert("Invio Ordine","Invio da implemetare", "Ok");
        }

        private void OnSalvaCommand(object obj)
        {
            CurPage.DisplayAlert("Salva Ordine", "Salvataggio da implemetare", "Ok");
        }

        ObservableCollection<Models.Cliente> clienti;
        public ObservableCollection<Models.Cliente> Clienti
        {
            get
            {
                if ((clienti == null || clienti.Count() == 0  ) && !clientiIsLoading)
                    LoadClienti();
                return clienti;
            }
            set
            {
                clienti = value;
                OnPropertyChanged();
            }
        }

        bool clientiIsLoading = false;
        async void LoadClienti()
        {
            if (!clientiIsLoading)
            {
                clientiIsLoading = true;
                try
                {
                    var tmpC = await DataStore.Clienti.GetItemsAsync(false);
                    tmpC = tmpC.Where(x => x.annullato == 0);
                    Clienti = new ObservableCollection<Models.Cliente>(tmpC);
                }
                finally
                {
                    clientiIsLoading = false;
                }
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
                App.CurOrdine.IdCliente = selectedCliente.IDCliente;
                OnPropertyChanged();
            }
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
                    InviaCommand.ChangeCanExecute();
                    AnnullaCommand.ChangeCanExecute();
                    SalvaCommand.ChangeCanExecute();
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
            OnPropertyChanged("NumeroCarrelli");
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
                if (totale == 0) return StrCarrelloVuoto;
                return string.Format(StrCarrelloNrArticoli,totale);
            }
        }
    }
}
