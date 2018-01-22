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
                return CurCliente == null || CurCliente.IdCliente == 0;
            }

        }

        public AnagraficaClientiEditDetailVM()
        {
            EditCommand = new Command(OnEditCommand);
            InsertCommand = new Command(OnInsertCommand);
        }

        private async void OnEditCommand()
        {
            await DataStore.Clienti.UpdateItemAsync(CurCliente);
            MessagingCenter.Send(new Models.Messages.ClienteInsertedOrUpdatedMessage() { Cliente = CurCliente }, "");
            await CurPage.Navigation.PopAsync();
        }

        private async void OnInsertCommand()
        {
            var maxCliente = DataStore.Clienti.GetItemsAsync().Result.Max(x => x.IdCliente);
            maxCliente += 1;
            CurCliente.IdCliente = maxCliente;
            await DataStore.Clienti.AddItemAsync(CurCliente);
            MessagingCenter.Send(new Models.Messages.ClienteInsertedOrUpdatedMessage() { Cliente = CurCliente}, "");
            await CurPage.Navigation.PopAsync();

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
