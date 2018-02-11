using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Omal.ViewModels
{
    public class InfoProductVM: BaseVM
    {
        

        public string CurTitle
        {
            get { return "Info"; }
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
                    OnPropertyChanged("GruppoMetadati");
                }
            }
        }

        Models.ProdottoGruppiMetadati _CurGruppoProdotto;
        public Models.ProdottoGruppiMetadati CurGruppoProdotto
        {
            get
            {
                return _CurGruppoProdotto;
            }
            set
            {
                if (_CurGruppoProdotto != value)
                {
                    _CurGruppoProdotto = value;
                    if (_CurGruppoProdotto != null)
                        CurPage.Navigation.PushAsync(new Views.GruppoMetadatiProdottoDetailV(_CurGruppoProdotto));
                }

            }
        }

        ObservableCollection<Models.ProdottoGruppiMetadati> _GruppoMetadati = null;
        public ObservableCollection<Models.ProdottoGruppiMetadati> GruppoMetadati
        {
            get
            {
                if (_GruppoMetadati == null && CurPage != null && !loadGruppoMetadati && _GruppoMetadati==null)
                    LoadGruppoMetadati();
                return _GruppoMetadati;
            }
            set
            {
                _GruppoMetadati = value;
                OnPropertyChanged();
            }
        }

        bool loadGruppoMetadati = false;
        async void LoadGruppoMetadati()
        {
            if (!loadGruppoMetadati)
            {
                loadGruppoMetadati = true;
                var tmpG = await DataStore.ProdottoGruppiMetadati.GetItemsAsync();
                tmpG = tmpG.Where(x => x.idprodotto == CurProdotto.idprodotto).OrderBy(x => x.ordine);
                GruppoMetadati = new ObservableCollection<Models.ProdottoGruppiMetadati>(tmpG);
                loadGruppoMetadati = false;
            }
        }

        public InfoProductVM()
        {
            PropertyChanged += OnLocalPropertyChanged;
         
        }

       
        private void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, "CurProdotto", StringComparison.InvariantCultureIgnoreCase))   
            {
                if (_CurProdotto != null)
                {
                    var content = ((ContentPage)CurPage).Content;
                    var stack = new StackLayout { Orientation = StackOrientation.Vertical };

                    stack.Children.Add(new Label { Text =string.Equals(App.CurLang,"IT", StringComparison.InvariantCultureIgnoreCase
                                                                      )?CurProdotto.nome:CurProdotto.nome_en });

                }
            }
        }
    }
}
