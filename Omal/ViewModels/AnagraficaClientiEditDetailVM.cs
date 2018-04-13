using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using Omal.Models;

namespace Omal.ViewModels
{
    public class AnagraficaClientiEditDetailVM: BaseVM
    {
        public ICommand EditCommand { get; set; }
        public ICommand InsertCommand { get; set; }
        public ICommand EliminaCommand { get; set; }
        public Page PrevDetailPage { get; set; }
        public bool IsModal { get; set; }

        public bool IsInsert
        {
            get
            {
                return CurCliente == null || CurCliente.IDCliente == 0;
            }

        }

        public AnagraficaClientiEditDetailVM()
        {
            EditCommand = new Command(OnEditCommand);
            InsertCommand = new Command(OnInsertCommand);
            EliminaCommand = new Command(OnEliminaCommand);
        }

        private async void OnEliminaCommand(object obj)
        {
            if (obj == null) return;
            Models.Cliente cli = (Models.Cliente)obj;
            var risposta = await CurPage.DisplayAlert(TitoloClienti, StrConfermaEliminazione, StrSi, StrNo);
            if (risposta)
            {
                // verifico che il cliente non abbia un ordine in bozza.
                var ordini = await DataStore.Ordini.GetItemsAsync();
                int tot = ordini.Count(x => x.Stato == Models.Enums.EOrdineStato.bozza && x.IdCliente == cli.IDCliente);
                if (tot > 0)
                {
                    risposta = await CurPage.DisplayAlert(TitoloClienti, AlertClienteInBozza , StrSi, StrNo);
                    if (!risposta) return;
                }
                cli.annullato = 1;
                try
                {
                    var ritorno = await DataStore.Clienti.UpdateItemAsync(cli) as ResponseClienti;
                    if (ritorno.HasError == 1)
                    {
                        throw new Exception(LangIsIT ? ritorno.ErrorDescription : ritorno.ErrorDescription_En);
                    }
                    MessagingCenter.Send(new Models.Messages.ClienteInsertedOrUpdatedMessage() { Cliente = cli }, "");
                    CurPage.DisplayAlert(CurTitle, MsgClienteEliminato, "Ok");

                    if (PrevDetailPage != null) Navigation.RemovePage(PrevDetailPage);
                    PrevDetailPage = null;
                    Navigation.RemovePage(CurPage);
                    Navigation.PushAsync(new Views.AnagraficaClientiV());
                }
                catch (Exception ex)
                {
                    CurPage.DisplayAlert(CurTitle, ex.Message, "Ok");
                }
            }
        }

        private async void OnEditCommand()
        {
            bool errore = false;
            try
            {
                ChecKFields();
                var ritorno = await DataStore.Clienti.UpdateItemAsync(CurCliente);
                if (ritorno.HasError == 1) throw new Exception(App.CurLang == "IT" ? ritorno.ErrorDescription : ritorno.ErrorDescription_En);
            }
            catch (Exception ex)
            {
                errore = true;
                CurPage.DisplayAlert(TitoloClienti, ex.Message, "OK");
            }
            if (errore) return;

            MessagingCenter.Send(new Models.Messages.ClienteInsertedOrUpdatedMessage() { Cliente = CurCliente }, "");
            await CurPage.Navigation.PopAsync();
        }

        private void ChecKFields()
        {
            List<string> errori = new List<string>();
            if (CurCliente == null) throw new ArgumentNullException("Cliente nn valorizzato");
            if (String.IsNullOrWhiteSpace(CurCliente.Cap)) errori.Add(StrCap);
            if (String.IsNullOrWhiteSpace(CurCliente.Citta)) errori.Add(StrCitta);
            if (String.IsNullOrWhiteSpace(CurCliente.Email)) errori.Add(StrEmail);
            if (String.IsNullOrWhiteSpace(CurCliente.Indirizzo)) errori.Add(StrIndirizzo);
            if (String.IsNullOrWhiteSpace(CurCliente.Nazione)) errori.Add(StrNazione);
            if (String.IsNullOrWhiteSpace(CurCliente.Piva)) errori.Add(StrPIva);
            if (String.IsNullOrWhiteSpace(CurCliente.Provincia)) errori.Add(StrProvincia);
            if (String.IsNullOrWhiteSpace(CurCliente.RagioneSociale)) errori.Add(StrRagioneSociale);
            if (String.IsNullOrWhiteSpace(CurCliente.Telefono)) errori.Add(StrTelefono);
            if (errori.Count > 0)
            {
                throw new Exception(string.Format("{0}:{1}{2}", ErrCampiOblCliente, Environment.NewLine, string.Join(Environment.NewLine, errori)));
            }



        }

        private async void OnInsertCommand()
        {
            bool errore = false;
            try
            {
                ChecKFields();
                var ritorno = await DataStore.Clienti.AddItemAsync(CurCliente);
                if (ritorno.HasError == 1) throw new Exception(App.CurLang == "IT" ? ritorno.ErrorDescription : ritorno.ErrorDescription_En);
            }
            catch (Exception ex)
            {
                errore = true;
                CurPage.DisplayAlert(TitoloClienti, ex.Message, "OK");
            }
            if (errore) return;
            MessagingCenter.Send(new Models.Messages.ClienteInsertedOrUpdatedMessage() { Cliente = CurCliente }, "");
            await CurPage.Navigation.PopAsync();
        }

        public string CurTitle
        {
            get {
                if (CurCliente != null && !IsInsert)
                    return CurCliente.RagioneSociale;
                else
                    return StrDettaglioCliente;
            }
        }

        Models.Cliente _CurCliente = null;

        public Models.Cliente CurCliente {
            get
            {
                return _CurCliente;
            }
            set
            {
                if (_CurCliente != value)
                {
                    _CurCliente = value;
                    OnPropertyChanged();
                    OnPropertyChanged("IsInsert");
                    OnPropertyChanged("CurTitle");
                }
            }
            
        }


    }
}
