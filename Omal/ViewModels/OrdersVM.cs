using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class OrdersVM: BaseVM
    {
        public OrdersVM()
        {
            PropertyChanged += OnLocalPropertyChanged;
            AddNewCommand = new Command(OnAddNewCommand);
        }

        private void OnAddNewCommand(object obj)
        {
            //throw new NotImplementedException();
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
            if (!ordiniIsLoading)
            {
                ordiniIsLoading = true;
                try
                {
                    var tuttOrdini = await DataStore.Ordini.GetItemsAsync(false);
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
