using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class SearchProductVM:ViewModels.BaseVM
    {
        public ICommand GoToDetailCommand { get; set; }

        public string CurTitle
        {
            get { return "Prodotto"; }
        }


        Models.Prodotto _CurProdotto = null;
        public Models.Prodotto CurProdotto
        {
            get 
            {
                return _CurProdotto;
            }
            set
            {
                if (_CurProdotto != value)
                {
                    _CurProdotto = value;
                    OnPropertyChanged();
                }
            }
        }

        public SearchProductVM()
        {
            GoToDetailCommand = new Command(OnGoToDetailCommand);
        }

        private void OnGoToDetailCommand()
        {
            CurPage.Navigation.PushAsync(new Views.InfoProductV(CurProdotto));
        }
    }
}
