using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class OrdersVM : BaseVM
    {
        public OrdersVM()
        {
            PropertyChanged += OnLocalPropertyChanged;
            AddNewCommand = new Command(OnAddNewCommand);
            MessagingCenter.Subscribe<Models.Messages.OrdineNewMessage>(this, "", sender =>
            {
                ordini = null;
                LoadOrdini();
            });
            MessagingCenter.Subscribe<Models.Messages.ClienteInsertedOrUpdatedMessage>(this, "", sender =>
            {
                ordini = null;
                LoadOrdini();
            });

        }

        private async void OnAddNewCommand(object obj)
        {
            if (App.CurOrdine != null && App.CurOrdine.carrelli != null && App.CurOrdine.carrelli.Count != 0)
            {
                var risposta = await CurPage.DisplayAlert(TitoloOrdini, StrPerdiModificheCarrello, StrSi, StrNo);
                if (!risposta) return;
                App.CurOrdine = new Models.Ordine();
                MessagingCenter.Send<Models.Messages.BasketLoadedMessage>(new Models.Messages.BasketLoadedMessage() { Ordine = App.CurOrdine }, "");
                CurPage.DisplayAlert(TitoloOrdini, StrOrdineCreato, "ok");
                MessagingCenter.Send(new Models.Messages.ChangeTabbedPageMessage() { SetPage = Models.Enums.EPages.Search }, "");

            }
        }

        private void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        string _SearchText;
        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                if (!string.Equals(value, _SearchText))
                {
                    _SearchText = value;
                    OnPropertyChanged();
                    ordini = null;
                    OnPropertyChanged("Ordini");
                    OnPropertyChanged("NumeroContatti");
                }

            }
        }

        public Models.Ordine OrdineSelected
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {
                    loadCurOrdine(value);
                }
            }
        }

        public async void loadCurOrdine(Models.Ordine curOrdine)
        {
            if (curOrdine == null || curOrdine.Stato == Models.Enums.EOrdineStato.ordineAnnullato || curOrdine.Stato == Models.Enums.EOrdineStato.ordineEvaso || curOrdine.Stato == Models.Enums.EOrdineStato.ordineInviato) return;
            var risposta = await CurPage.DisplayAlert(TitoloOrdini, StrCaricaOrdineSelezionato, StrSi, StrNo);
            if (risposta)
            {
                App.CurOrdine = curOrdine;
                MessagingCenter.Send<Models.Messages.BasketLoadedMessage>(new Models.Messages.BasketLoadedMessage() { Ordine = curOrdine }, "");
                CurPage.DisplayAlert(TitoloOrdini, StrOrdineCaricato, "ok");
                MessagingCenter.Send(new Models.Messages.ChangeTabbedPageMessage() { SetPage =  Models.Enums.EPages.Carrello }, "");
            }
        }

        public System.Windows.Input.ICommand AddNewCommand { get; set; }

        ObservableCollection<Models.Ordine> ordini = null;
        public ObservableCollection<Models.Ordine> Ordini
        {
            get
            {
                if ((ordini == null || ordini.Count == 0) && !ordiniIsLoading)
                    LoadOrdini();
                return ordini;
            }
            set
            {
                ordini = value;
                OnPropertyChanged();
            }
        }

        bool ordiniIsLoading = false;
        async void LoadOrdini()
        {
            if (!ordiniIsLoading && App.CurUser != null)
            {
                ordiniIsLoading = true;
                try
                {
                    var tuttOrdini = await DataStore.Ordini.GetItemsAsync(false);
                    tuttOrdini = tuttOrdini.Where(x => x.IDUtente == App.CurUser.IdUtente);
                    if  (!string.IsNullOrWhiteSpace(SearchText)) 
                        tuttOrdini = tuttOrdini.Where(x => x.ClienteRagSoc.ToUpper().Contains(SearchText.ToUpper())).ToList();
                    tuttOrdini = tuttOrdini.OrderByDescending(x => x.DataInizio);
                    Ordini = new ObservableCollection<Models.Ordine>(tuttOrdini);
                }
                finally
                {
                    ordiniIsLoading = false;
                }
            }
        }

    }
}
