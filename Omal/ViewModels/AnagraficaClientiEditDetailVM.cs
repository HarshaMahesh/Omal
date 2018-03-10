using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace Omal.ViewModels
{
    public class AnagraficaClientiEditDetailVM: BaseVM
    {
        public ICommand EditCommand { get; set; }
        public ICommand InsertCommand { get; set; }

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
        }

        private async void OnEditCommand()
        {
            
            bool errore = false;
            try
            {
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

        private async void OnInsertCommand()
        {
            /*var maxCliente = DataStore.Clienti.GetItemsAsync().Result.Max(x => x.IDCliente);
            maxCliente += 1;
            CurCliente.IDCliente = maxCliente;*/
            bool errore = false;
            try
            {
                var ritorno = await DataStore.Clienti.AddItemAsync(CurCliente);
                if (ritorno.HasError == 1) throw new Exception(App.CurLang == "IT" ? ritorno.ErrorDescription : ritorno.ErrorDescription_En);
            }
            catch (Exception ex)
            {
                errore = true;
                CurPage.DisplayAlert(TitoloClienti, ex.Message, "OK");
            }
            if (errore) return;


            await CurPage.Navigation.PopAsync();
            MessagingCenter.Send(new Models.Messages.ClienteInsertedOrUpdatedMessage() { Cliente = CurCliente }, "");
        }

        public string CurTitle
        {
            get {
                if (CurCliente != null)
                    return CurCliente.RagioneSociale;
                else
                    return "Dettaglio Cliente";
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
