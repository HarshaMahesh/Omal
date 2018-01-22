using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class AnagraficaClientiDetailVM: BaseVM
    {
        public ICommand EditCommand { get; set; }

        public AnagraficaClientiDetailVM()
        {
            EditCommand = new Command(OnEditCommand);
            MessagingCenter.Subscribe<Models.Messages.ClienteInsertedOrUpdatedMessage>(this, "", sender =>
            {
                _CurCliente = null;
                CurCliente = sender.Cliente;
            });
        }

        private void OnEditCommand()
        {
            CurPage.Navigation.PushAsync(new Views.AnagraficaClientiEditDetailV(CurCliente));
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
                    OnPropertyChanged("CurTitle");
                }
            }
            
        }


    }
}
