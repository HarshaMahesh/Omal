using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                }

            }
        }

        public ICommand AddNewCommand { get; set; }

        public AnagraficaClientiVM()
        {
            AddNewCommand = new Command(OnAddNewCommand);
            MessagingCenter.Subscribe<Models.Messages.ClienteInsertedOrUpdatedMessage>(this, "", sender =>
            {
                clienti = null;
                tuttiClienti = null;
                OnPropertyChanged("Clienti");

            });

        }

        private void OnAddNewCommand()
        {
            CurPage.Navigation.PushAsync(new Views.AnagraficaClientiEditDetailV(new Models.Cliente(){}));
        }

        Models.Cliente _ClienteSelected;
        public Models.Cliente ClienteSelected
        {
            get
            {
                return _ClienteSelected;
            }

            set
            {
                if (_ClienteSelected != value)
                {
                    _ClienteSelected = value;
                    if (_ClienteSelected != null) CurPage.Navigation.PushAsync(new Views.AnagraficaClientiDetailV(_ClienteSelected));
                }
            }
        }

        public string NumeroContatti
        {
            get
            {
                if (Clienti == null)  return "Nessun Contatto";
                var ritorno =  Clienti.Select(x => x.Count).Sum();
                if (ritorno == 0) return "Nessun Contatto";
                if (ritorno == 1) return "Trovato un contatto";
                return string.Format("Trovati {0} contatti", ritorno);
            }
        }


        List<Models.Cliente> tuttiClienti = null;

        ObservableCollection<Models.GruppoClienti> clienti = null;
        public ObservableCollection<Models.GruppoClienti> Clienti
        {
            get
            {
                if (clienti == null)
                {
                    var elenco = new ObservableCollection<Models.GruppoClienti>();
                    if (tuttiClienti == null) tuttiClienti = DataStore.Clienti.GetItemsAsync().Result.ToList();
                    var elencoClienti = tuttiClienti;
                    var iniziali =elencoClienti.Where(x => !string.IsNullOrWhiteSpace(x.RagioneSociale)).Select(x => x.RagioneSociale.Substring(0, 1)).Distinct();
                    foreach (var item in iniziali)
                    {
                        var elemento = new Models.GruppoClienti(item, item);
                        var sclienti = elencoClienti.Where(x => !string.IsNullOrWhiteSpace(x.RagioneSociale) && x.RagioneSociale.StartsWith(item, StringComparison.InvariantCultureIgnoreCase)).ToList();
                        if (!string.IsNullOrWhiteSpace(SearchText)) sclienti = sclienti.Where(x => x.RagioneSociale.Contains(SearchText)).ToList();
                        if (sclienti != null && sclienti.Count > 0)
                        {
                            elemento.AddRange(sclienti);
                            elenco.Add(elemento);
                        }
                    }
                    clienti = new ObservableCollection<Models.GruppoClienti>(elenco.OrderBy(x => x.Titolo));
                    OnPropertyChanged("NumeroContatti");
                }
                return clienti;
            }
        }

    }
}
