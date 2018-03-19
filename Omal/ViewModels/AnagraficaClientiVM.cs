﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class AnagraficaClientiVM: BaseVM
    {

        string _SearchText;
        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                if (!string.Equals(value,_SearchText))
                {
                    _SearchText = value;
                    OnPropertyChanged();
                    clienti = null;
                    OnPropertyChanged("Clienti");
                    OnPropertyChanged("NumeroContatti");
                }

            }
        }

        public ICommand AddNewCommand { get; set; }
        public ICommand EliminaCommand { get; set; }


        public AnagraficaClientiVM()
        {
            PropertyChanged += OnLocalPropertyChanged;
            AddNewCommand = new Command(OnAddNewCommand);
            EliminaCommand = new Command(OnEliminaCommand);
            MessagingCenter.Subscribe<Models.Messages.ClienteInsertedOrUpdatedMessage>(this, "", sender =>
            {
                clienti = null;
                tuttiClienti = null;
                OnPropertyChanged("Clienti");

            });
        }

        private async void OnEliminaCommand(object obj)
        {
            if (obj == null) return;
            Models.Cliente cli = (Models.Cliente)obj;
            var risposta =await CurPage.DisplayAlert(TitoloClienti, StrConfermaEliminazione, StrSi, StrNo);
            if (risposta)
            {
                cli.annullato = 1;
                try
                {
                    DataStore.Clienti.UpdateItemAsync(cli);
                    clienti = null;
                    OnPropertyChanged("Clienti");

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void OnLocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, "Items", StringComparison.InvariantCultureIgnoreCase) ||
                (string.Equals(e.PropertyName, "Clienti", StringComparison.InvariantCultureIgnoreCase)
                ))
            {
                OnPropertyChanged("NumeroContatti");
            }
        }

        private void OnAddNewCommand()
        {
            CurPage.Navigation.PushAsync(new Views.AnagraficaClientiEditDetailV(new Models.Cliente(){}));
        }

        Models.Cliente _ClienteSelected = null;
        public Models.Cliente ClienteSelected
        {
            get
            {
                return _ClienteSelected;
            }

            set
            {
                OnPropertyChanged();
                if (value != null)
                {
                    CurPage.Navigation.PushAsync(new Views.AnagraficaClientiDetailV(value));
                }

            }
        }

        public string NumeroContatti
        {
            get
            {
                if (clienti == null)  return StrNessunContatto;
                var ritorno =  clienti.Select(x => x.Count).Sum();
                if (ritorno == 0) return StrNessunContatto;
                if (ritorno == 1) return StrTrovatoUnContatto;
                return string.Format(StrTrovatiNrContatti, ritorno);
            }
        }


        List<Models.Cliente> tuttiClienti = null;

        ObservableCollection<Models.GruppoClienti> clienti = null;
        public ObservableCollection<Models.GruppoClienti> Clienti
        {
            get
            {
                if ((clienti == null || clienti.Count == 0) && !clientiIsLoading)
                    LoadClienti();
                return clienti;
            }
            set
            {
                clienti = value;
                OnPropertyChanged();
            }
        }

        bool clientiIsLoading = false;
        async void LoadClienti()
        {
            if (!clientiIsLoading)
            {
                clientiIsLoading = true;
                try
                {
                    var tuttiClienti = await DataStore.Clienti.GetItemsAsync(false);
                    tuttiClienti = tuttiClienti.Where(x => x.annullato == 0);
                    var elenco = new ObservableCollection<Models.GruppoClienti>();
                    var elencoClienti = tuttiClienti.Where(x => x.annullato == 0 && x.IDUtente == App.CurUser.IdUtente && !string.IsNullOrWhiteSpace(x.RagioneSociale));
                    var iniziali = elencoClienti.Where(x => x.annullato == 0 && x.IDUtente == App.CurUser.IdUtente && !string.IsNullOrWhiteSpace(x.RagioneSociale)).Select(x => x.RagioneSociale.ToUpper().Substring(0, 1)).Distinct().ToList();
                    iniziali.Add(" ");
                    foreach (var item in iniziali)
                    {
                        List<Models.Cliente> sclienti;
                        var elemento = new Models.GruppoClienti(item, item);
                        if (string.IsNullOrWhiteSpace(item)) 
                        {
                            sclienti = elencoClienti.Where(x => string.IsNullOrWhiteSpace(x.RagioneSociale)).ToList();                        
                        }
                        else
                            sclienti= elencoClienti.Where(x => !string.IsNullOrWhiteSpace(x.RagioneSociale) && x.RagioneSociale.StartsWith(item, StringComparison.InvariantCultureIgnoreCase)).ToList();
                        if (!string.IsNullOrWhiteSpace(SearchText)) sclienti = sclienti.Where(x => x.RagioneSociale.Contains(SearchText)).ToList();
                        if (sclienti != null && sclienti.Count > 0)
                        {
                            elemento.AddRange(sclienti);
                            elenco.Add(elemento);
                        }
                    }
                    clienti = new ObservableCollection<Models.GruppoClienti>(elenco.OrderBy(x => x.Titolo));
                }
                finally
                {
                    clientiIsLoading = false;
                }
            }
        }

    }
}
