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
    public class BasketVM : BaseVM
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
                OnPropertyChanged("Clienti");
                OnPropertyChanged("NumeroCarrelli");
                OnPropertyChanged("IsNewOrdine");

            });
            MessagingCenter.Subscribe<Models.Messages.BasketEditedMessage>(this, "", sender =>
            {
                ClearItems();
                OnPropertyChanged("IsNewOrdine");
                OnPropertyChanged("NoteOrdine");
                OnPropertyChanged("SelectedCliente");
                OnPropertyChanged("Items");
                OnPropertyChanged("NumeroCarrelli");
                OnPropertyChanged("TotaleOrdine");
                OnPropertyChanged("ScontoOrdine");
                OnPropertyChanged("ColorePrezzoScontato");
                OnPropertyChanged("ColorePrezzoTotale");
            });
            MessagingCenter.Subscribe<Models.Messages.BasketLoadedMessage>(this, "", sender =>
            {
                ClearItems();
                items = null;
                OnPropertyChanged("IsNewOrdine");
                OnPropertyChanged("NoteOrdine");
                OnPropertyChanged("Items");
                OnPropertyChanged("NumeroCarrelli");
                OnPropertyChanged("TotaleOrdine");
                OnPropertyChanged("TotaleOrdineConSconto");
                OnPropertyChanged("ScontoOrdine");
                OnPropertyChanged("ColorePrezzoScontato");
                OnPropertyChanged("ColorePrezzoTotale");
                if (clienti != null)
                {
                    var cliente = clienti.FirstOrDefault( x =>  x.IDCliente == App.CurOrdine.IdCliente);
                    if (cliente != null)
                        ((Views.BasketV)CurPage).SetClienteIndex(clienti.IndexOf(cliente));
                }


            });

            MessagingCenter.Subscribe<Models.Messages.ClienteInsertedOrUpdatedMessage>(this, "", sender =>
            {
                LoadClienti();
            });


            SalvaCommand = new RelayCommand(OnSalvaCommand, CanSaveCommand);
            InviaCommand = new RelayCommand(OnInviaCommand, CanInviaCommand);
            AnnullaCommand = new RelayCommand(OnAnnullaCommand, CanAnnullaCommand);
            PropertyChanged += LocalPropertyChanged;
        }


        public Color ColorePrezzoScontato
        {
            get
            {
                if (App.CurOrdine == null || App.CurOrdine.Sconto == 0) return Color.Transparent;
                return Color.FromHex("#004899");
            }

        }


        public Color ColorePrezzoTotale
        {
            get
            {
                if (App.CurOrdine == null || App.CurOrdine.Sconto == 0) return Color.FromHex("#004899");
                return Color.FromHex("#5E5B5B");
            }

        }

        private bool CanAnnullaCommand(object arg)
        {
            return App.CurOrdine != null && App.CurOrdine.carrelli.Count > 0 && !IsNewOrdine; 
        }

        private bool CanInviaCommand(object arg)
        {
            return App.CurOrdine != null && App.CurOrdine.IdCliente != 0 && App.CurOrdine.carrelli.Count > 0; ;
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
            return App.CurOrdine != null && App.CurOrdine.IdCliente != 0 && App.CurOrdine.carrelli.Count > 0;
        }

        private async void OnAnnullaCommand(object obj)
        {
            var answer = await CurPage.DisplayAlert(TitoloCarrello, StrSicuroAnnullareCarrello, StrSi, StrNo);
            if (answer)
            {
                App.CurOrdine.Stato = Models.Enums.EOrdineStato.ordineAnnullato;
                DataStore.Ordini.UpdateItemAsync(App.CurOrdine);
                MessagingCenter.Send(new Models.Messages.OrdineNewMessage() { Ordine = App.CurOrdine }, "");
                ClearItems();
                App.CurOrdine = null;
                App.CurOrdine = new Models.Ordine();
                SelectedItem = null;
                ClearItems();
                App.CurOrdine.Totale = 0;
                OnPropertyChanged("Items");
                OnPropertyChanged("TotaleOrdine");
                OnPropertyChanged("TotaleOrdineConSconto");
                OnPropertyChanged("ScontoOrdine");
                OnPropertyChanged("NumeroCarrelli");
                OnPropertyChanged("ColorePrezzoScontato");
                OnPropertyChanged("ColorePrezzoTotale");
                OnPropertyChanged("NoteOrdine");
                SalvaCommand.ChangeCanExecute();
                InviaCommand.ChangeCanExecute();
                AnnullaCommand.ChangeCanExecute();
                ((Views.BasketV)CurPage).SetClienteIndex(-1);
                CurPage.DisplayAlert(TitoloCarrello, StrOrdineAnnullato, "Ok");
            }
        }

        Models.Carrello _SelectedItem = null;
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

        public bool IsNewOrdine
        {
            get
            {
                return App.CurOrdine == null || App.CurOrdine.IdOrdine == 0;
            }
        }


        private async void OnInviaCommand(object obj)
        {
            List<string> destinatari = new List<string>();
            if (!string.IsNullOrWhiteSpace(App.CurToken.email_utente)) destinatari.Add(App.CurToken.email_per_backoffice);
            if (!string.IsNullOrWhiteSpace(App.CurToken.email_utente)) destinatari.Add(App.CurToken.email_utente);

            var answer = await CurPage.DisplayAlert(TitoloCarrello, 
                                                    string.Format(StrSicuroInviareCarrello, string.Join(", ", destinatari )), 

                                                    StrSi, StrNo);
            if (answer)
            {
                try
                {
                    bool isNew = string.IsNullOrWhiteSpace(App.CurOrdine.CodiceOrdine);
                    App.CurOrdine.Stato = Models.Enums.EOrdineStato.ordineInviato;
                    App.CurOrdine.DataFine = DateTime.Now;

                    var ritorno = await DataStore.Ordini.UpdateItemAsync(App.CurOrdine);
                    if (ritorno.HasError == 1) throw new Exception(App.CurLang == "IT" ? ritorno.ErrorDescription : ritorno.ErrorDescription_En);
                    ((Views.BasketV)CurPage).SetClienteIndex(-1);
                    MessagingCenter.Send(new Models.Messages.OrdineNewMessage() { Ordine = App.CurOrdine }, "");
                }
                catch (Exception ex)
                {
                    CurPage.DisplayAlert("Error", ex.Message, "Ok");
                }
                await CurPage.DisplayAlert(TitoloCarrello, string.Format(StrOrdineCompletatoCompletato, App.CurOrdine.IdOrdine), "Ok");
                ClearItems();
                App.CurOrdine = null;
                App.CurOrdine = new Models.Ordine();
                SelectedItem = null;
                ClearItems();
                App.CurOrdine.Totale = 0;
                OnPropertyChanged("Items");
                OnPropertyChanged("TotaleOrdine");
                OnPropertyChanged("TotaleOrdineConSconto");
                OnPropertyChanged("ScontoOrdine");
                OnPropertyChanged("NumeroCarrelli");
                OnPropertyChanged("ColorePrezzoScontato");
                OnPropertyChanged("ColorePrezzoTotale");
                OnPropertyChanged("SelectedCliente");
                OnPropertyChanged("NoteOrdine");
                SalvaCommand.ChangeCanExecute();
                InviaCommand.ChangeCanExecute();
                AnnullaCommand.ChangeCanExecute();

            }
        }

        private async void OnSalvaCommand(object obj)
        {
            var answer = await CurPage.DisplayAlert(TitoloCarrello, 
                                                    string.Format(StrSicuroSalvareCarrello, App.CurToken.email_utente + ", " + App.CurToken.email_per_backoffice)


                                                    , StrSi, StrNo);
            if (answer)
            {
                try
                {
                    bool isNew = string.IsNullOrWhiteSpace(App.CurOrdine.CodiceOrdine);
                    var ritorno = await DataStore.Ordini.UpdateItemAsync(App.CurOrdine);

                    if (ritorno.HasError == 1) throw new Exception(App.CurLang == "IT" ? ritorno.ErrorDescription : ritorno.ErrorDescription_En);
                    MessagingCenter.Send(new Models.Messages.OrdineNewMessage() { Ordine = App.CurOrdine }, "");
                    ((Views.BasketV)CurPage).SetClienteIndex(-1);
                }
                catch (Exception ex)
                {
                    CurPage.DisplayAlert("Error", ex.Message, "Ok");
                }
                await CurPage.DisplayAlert(TitoloCarrello, StrSalvataggioCarrelloCompletato, "Ok");
                ClearItems();
                App.CurOrdine = null;
                App.CurOrdine = new Models.Ordine();
                SelectedItem = null;
                ClearItems();
                App.CurOrdine.Totale = 0;
                OnPropertyChanged("Items");
                OnPropertyChanged("TotaleOrdine");
                OnPropertyChanged("TotaleOrdineConSconto");
                OnPropertyChanged("ScontoOrdine");
                OnPropertyChanged("NumeroCarrelli");
                OnPropertyChanged("ColorePrezzoScontato");
                OnPropertyChanged("ColorePrezzoTotale");
                OnPropertyChanged("SelectedCliente");
                OnPropertyChanged("NoteOrdine");
                SalvaCommand.ChangeCanExecute();
                InviaCommand.ChangeCanExecute();
                AnnullaCommand.ChangeCanExecute();
            }
        }

        ObservableCollection<Models.Cliente> clienti;
        public ObservableCollection<Models.Cliente> Clienti
        {
            get
            {
                if ((clienti == null || clienti.Count() == 0) && !clientiIsLoading)
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
            if (!clientiIsLoading && App.CurUser != null)
            {
                clientiIsLoading = true;
                try
                {
                    var tmpC = await DataStore.Clienti.GetItemsAsync(false);
                    tmpC = tmpC.Where(x => x.annullato == 0 && x.IDUtente == App.CurUser.IdUtente);
                    List<Models.Cliente> clientiTmp = tmpC.ToList();
                    clientiTmp.Insert(0, new Models.Cliente() { RagioneSociale = StrAggiungiNuovoCliente, IDCliente = -1 });
                    Clienti = new ObservableCollection<Models.Cliente>(clientiTmp);
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
                return App.CurOrdine == null ? string.Empty : App.CurOrdine.Note;
            }
            set
            {
                if (App.CurOrdine != null && !string.Equals(App.CurOrdine.Note, value, StringComparison.InvariantCultureIgnoreCase))
                {
                    App.CurOrdine.Note = value;
                    //OnPropertyChanged();
                }

            }
        }

        public double ScontoOrdine
        {
            get
            {
                return App.CurOrdine == null ? 0 : App.CurOrdine.Sconto;
            }
            set
            {
                if (App.CurOrdine != null && App.CurOrdine.Sconto != value)
                {
                    App.CurOrdine.Sconto = value;
                    OnPropertyChanged();
                    RicalcolaTotale();
                }

            }
        }

        Models.Cliente selectedCliente;
        public Models.Cliente SelectedCliente
        {
            get
            {
                if (selectedCliente == null && App.CurOrdine != null && App.CurOrdine.IdCliente != 0)
                    LoadCliente();
                return selectedCliente;
            }
            set
            {
                if (value != null && value.IDCliente == -1)
                {
                    // devo permettere l'aggiunta di un nuovo cliente
                    AggiungiNuovoCliente();
                    return;
                }
                if (App.CurOrdine == null) return;
                selectedCliente = value;
                if (selectedCliente == null)
                {
                    App.CurOrdine.IdCliente = 0;
                }
                else
                    App.CurOrdine.IdCliente = selectedCliente.IDCliente;
                OnPropertyChanged();
            }
        }

        private async void AggiungiNuovoCliente()
        {
            await CurPage.Navigation.PushAsync(new Views.AnagraficaClientiEditDetailV(new Models.Cliente() ,  true));
        }



        private async void LoadCliente()
        {
            if (App.CurOrdine != null && App.CurOrdine.IdCliente != 0)
            {
                selectedCliente = Clienti.FirstOrDefault(x => x.IDCliente == App.CurOrdine.IdCliente);
                OnPropertyChanged("SelectedCliente");
            }

        }

        private void ClearItems()
        {
            if (items == null) return;
            foreach (var item in items)
            {
              /*  if (item != null)
                    item.PropertyChanged -= ItemPropertyChanges;*/
            }

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
                    items = new ObservableCollection<Models.Carrello>(App.CurOrdine.carrelli);
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
            RicalcolaTotale();
            OnPropertyChanged("NumeroCarrelli");
        }

        private void RicalcolaTotale()
        {
            if (App.CurOrdine == null) return;
            App.CurOrdine.Totale = items.Sum(x => x.PrezzoTotaleScontato);
            App.CurOrdine.TotaleConSconto = App.CurOrdine.Totale - (App.CurOrdine.Totale * App.CurOrdine.Sconto / 100);
            OnPropertyChanged("TotaleOrdine");
            OnPropertyChanged("TotaleOrdineConSconto");
            OnPropertyChanged("ColorePrezzoScontato");
            OnPropertyChanged("ColorePrezzoTotale");

        }

        public double TotaleOrdine
        {
            get
            {
                if (App.CurOrdine == null) return 0;
                return App.CurOrdine.Totale;
            }
        }

        public double TotaleOrdineConSconto
        {
            get
            {
                if (App.CurOrdine == null) return 0;
                return App.CurOrdine.TotaleConSconto;
            }
        }



        public string NumeroCarrelli
        {
            get
            {
                var totale = App.CurOrdine.carrelli.Sum(x => x.Qta);
                if (totale == 0) return StrCarrelloVuoto;
                return string.Format(StrCarrelloNrArticoli, totale);
            }
        }
    }
}
