using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using Plugin.Share;
namespace Omal.ViewModels
{
    public class InfoProductVM : BaseVM
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
                if (_GruppoMetadati == null && CurPage != null && !loadGruppoMetadati && _GruppoMetadati == null)
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
            if (_CurProdotto == null) return;
            if (!loadGruppoMetadati)
            {
                loadGruppoMetadati = true;
                var tmpG = await DataStore.ProdottoGruppiMetadati.GetItemsAsync();
                tmpG = tmpG.Where(x => x.idprodotto == CurProdotto.idprodotto).Distinct().OrderBy(x => x.ordine);
                GruppoMetadati = new ObservableCollection<Models.ProdottoGruppiMetadati>(tmpG);
                loadGruppoMetadati = false;
            }
        }

        public InfoProductVM()
        {
            PropertyChanged += OnLocalPropertyChanged;

        }


        public Models.PDF PdfSelected
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {
                    ((Views.InfoProductV)CurPage).OpenB(value.urlFile.Replace(" ","%20"));
                }
            }
        }


        ObservableCollection<Models.GruppoPdf> listaDocumenti = null;
        public ObservableCollection<Models.GruppoPdf> ListaDocumenti
        {
            get
            {
                if (( listaDocumenti == null || listaDocumenti.Count == 0) && !listaDocumentiIsLoading)
                    LoadDocumenti();
                return listaDocumenti;
            }
            set
            {
                listaDocumenti = value;
                OnPropertyChanged();
            }
        }


        public int NumeroDocumenti
        {
            get
            {
                if (listaDocumenti == null) return 0;
                return listaDocumenti.Sum(x => x.Count);
            }
        }


        bool listaDocumentiIsLoading = false;
        async void LoadDocumenti()
        {
            if (_CurProdotto == null) return;
            if (!listaDocumentiIsLoading)
            {
                listaDocumentiIsLoading = true;
                try
                {
                    var tuttiPdf = await DataStore.Pdf.GetItemsAsync(CurProdotto.idprodotto);
                    var elenco = new ObservableCollection<Models.GruppoPdf>();
                    var categorie = tuttiPdf.Select(x => x.categoria).Distinct();
                    foreach (var item in categorie)
                    {
                        List<Models.PDF> documenti;
                        var elemento = new Models.GruppoPdf(item);
                        if (string.IsNullOrWhiteSpace(item))
                        {
                            documenti = tuttiPdf.Where(x => string.IsNullOrWhiteSpace(x.categoria)).ToList();
                        }
                        else
                            documenti = tuttiPdf.Where(x => !string.IsNullOrWhiteSpace(x.categoria) && string.Equals(x.categoria, item, StringComparison.InvariantCultureIgnoreCase)).ToList();
                        if (documenti != null && documenti.Count > 0)
                        {
                            elemento.AddRange(documenti);
                            elenco.Add(elemento);
                        }
                    }
                    ListaDocumenti = new ObservableCollection<Models.GruppoPdf>(elenco.OrderBy(x => x.Categoria));
                }
                finally
                {
                    listaDocumentiIsLoading = false;
                }
            }
        }




        private void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, "CurProdotto", StringComparison.InvariantCultureIgnoreCase))
            {
                if (_CurProdotto != null)
                {
                    var content = ((ContentPage)CurPage).Content;
                    var stack = new StackLayout { Orientation = StackOrientation.Vertical };

                    stack.Children.Add(new Label
                    {
                        Text = string.Equals(App.CurLang, "IT", StringComparison.InvariantCultureIgnoreCase
                                                                      ) ? CurProdotto.nome : CurProdotto.nome_en
                    });

                }
                OnPropertyChanged("ListaDocumenti");
            }
            if (string.Equals(e.PropertyName, "ListaDocumenti", StringComparison.InvariantCultureIgnoreCase))
            {
                OnPropertyChanged("NumeroDocumenti");
            }

        }
    }
}
