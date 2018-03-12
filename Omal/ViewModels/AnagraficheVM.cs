using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class AnagraficheVM: BaseVM
    {
        public ICommand GoToAnagraficaClientiCommand { get; set; }
        public ICommand GoToAnagraficaOrdiniCommand { get; set; }

        public string CurTitle
        {
            get { return TitoloArchivio; }
        }

        public AnagraficheVM()
        {
            GoToAnagraficaClientiCommand = new Command(OnGoToAnagraficaClientiCommand);
            GoToAnagraficaOrdiniCommand = new Command(OnGoToAnagraficaOrdiniCommand);
        }

        private void OnGoToAnagraficaClientiCommand()
        {
            CurPage.Navigation.PushAsync(new Views.AnagraficaClientiV());
        }

        private void OnGoToAnagraficaOrdiniCommand()
        {
            CurPage.Navigation.PushAsync(new Views.OrdersV());
        }
    }
}
