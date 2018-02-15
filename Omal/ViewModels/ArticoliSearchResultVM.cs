using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Omal.ViewModels
{
    public class ArticoliSearchResultVM: BaseVM
    {
        public bool prodottoIsValvola { get; set; }
        public IEnumerable<Models.Base> Articoli { get; set; }

        internal void Navigating(string url)
        {
            var parametri = GetParams(url);
            if (parametri.ContainsKey("isvalvola"))
            {
                if (App.CurOrdine == null)
                {
                    App.CurOrdine = new Models.Ordine();
                    // recupero il codice dell'ordine più alto 
                    App.CurOrdine.DataInizio = DateTime.Now;
                }
                var codiceProdotto = parametri["idprodotto"].ToString();
                var qta = parametri["qta"].ToString();
                int IQta;
                if (!int.TryParse(qta,out IQta))
                {
                    CurPage.DisplayAlert("Carrello", "Qta nn valida. Numero intero richiesto.", "OK");
                    return;
                }
                if (IQta < 0)
                {
                    CurPage.DisplayAlert("Carrello", "Qta nn valida. Il numero deve essere maggiore di 0", "OK");
                    return; 
                }
                if (parametri["isvalvola"].ToString() == "1")
                {
                    // Sto ordinando una valvola
                    var idcodicevalvola = parametri["idcodicevalvola"].ToString();
                    int IntIdCodiceValvola = Convert.ToInt32(idcodicevalvola);
                    var valvole = Articoli.Select(x => (Models.Valvola)x).ToList();
                    var valvola = valvole.FirstOrDefault(x => x.idcodicevalvola == IntIdCodiceValvola);
                    //var v = (Articoli.FirstOrDefault(x => x.idcodicevalvola == IntIdCodiceValvola);
                    if (valvola == null) throw new KeyNotFoundException("IdCodiceValvola non trovato");
                    var elementoCarrello = DataStore.Carrello.FirstOrDefault(x => x.IdArticolo == valvola.idcodicevalvola && x.Tipologia == CurProdotto.tipologia && x.IdProdotto == CurProdotto.idprodotto);
                    if (elementoCarrello == null)
                        DataStore.Carrello.Add(new Models.Carrello() { CodiceArticolo = valvola.codice_articolo, DescrizioneCarrello_En = curProdotto.descrizione_en, DescrizioneCarrello_It = curProdotto.descrizione, IdArticolo = valvola.idcodicevalvola, IdOrdine = App.CurOrdine.IdOrdine, PrezzoUnitario = valvola.Prezzo, Qta = Convert.ToInt32(qta), Tipologia = curProdotto.tipologia, IdProdotto = CurProdotto.idprodotto });
                    else
                        elementoCarrello.Qta += IQta;                                                                              
                }
                else
                {
                    // sto ordinando un attuatore
                    var idcodiceattuatore = parametri["idcodiceattuatore"].ToString();
                    var attuatore = ((IEnumerable<Models.Attuatore>)Articoli).FirstOrDefault(x => x.IdCodiceAttuatore == Convert.ToInt32(idcodiceattuatore));
                    if (attuatore == null) throw new KeyNotFoundException("IdCodiceAttuatore non trovato");
                    var elementoCarrello = DataStore.Carrello.FirstOrDefault(x => x.IdArticolo == attuatore.IdCodiceAttuatore && x.Tipologia == CurProdotto.tipologia && x.IdProdotto == CurProdotto.idprodotto);
                    if (elementoCarrello == null)
                        DataStore.Carrello.Add(new Models.Carrello() { CodiceArticolo = attuatore.Codice_Articolo, DescrizioneCarrello_En = curProdotto.descrizione_en, DescrizioneCarrello_It = curProdotto.descrizione, IdArticolo = attuatore.IdCodiceAttuatore, IdOrdine = App.CurOrdine.IdOrdine, PrezzoUnitario = attuatore.Prezzo, Qta = IQta, Tipologia = curProdotto.tipologia, IdProdotto = CurProdotto.idprodotto });
                    else
                        elementoCarrello.Qta += IQta;
                }
                MessagingCenter.Send<Models.Messages.BasketEditedMessage>(new Models.Messages.BasketEditedMessage(){ },"");
                CurPage.DisplayAlert("Carrello", "Articoli aggiunti al carrello", "OK");
            }
        }

        static Dictionary<string, string> GetParams(string url)
        {
            var matches = Regex.Matches(url, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
            return matches.Cast<Match>().ToDictionary(
                m => Uri.UnescapeDataString(m.Groups[2].Value),
                m => Uri.UnescapeDataString(m.Groups[3].Value)
            );
        }

        Models.Prodotto curProdotto;
        public Models.Prodotto CurProdotto
        {
            get
            {
                return curProdotto;
            }
            set
            {
                curProdotto = value;
                OnPropertyChanged("ContentHtml");
            }
        }

        public IEnumerable<Models.Valvola> Valvole { get; set; }
        public IEnumerable<Models.Attuatore> Attuatori { get; set; }

        public string ContentHtml
        {
            get
            {
                if (curProdotto == null) return string.Empty;
                string ritorno = string.Empty;
                if (!IsLoggedIn)
                {
                    ritorno += "<br><b>Per vedere i prezzi è necessario effettuare il login</b>";
                }
                ritorno += string.Format("<b>{0}</b>", App.CurLang == "IT"? curProdotto.nome:curProdotto.nome_en);
                ritorno += string.Format("<p ALIGN='CENTER'>Trovati {0} articoli</p>", Articoli.Count());
                if (prodottoIsValvola)
                    ritorno += HtmlPerValvole();
                else
                    ritorno += HtmlPerAttuatori();
               
                return ritorno;
            }
        }


        private string HtmlPerAttuatori()
        {
            var attuatori = (IEnumerable<Models.Attuatore>)Articoli;
            string ritorno = string.Empty;
            string elemento = "{0}<br /><b>{1}</b><br />";
            foreach (var attuatore in attuatori)
            {                
                List<string> curAttuatore = new List<string>();
                if (!string.IsNullOrWhiteSpace(attuatore.immagine_placeholder)) curAttuatore.Add(string.Format("<center><img src='{0}' /></center>", attuatore.immagine_placeholder));
                if (!string.IsNullOrWhiteSpace(attuatore.Valore_iso)) curAttuatore.Add(string.Format(elemento, "Valore_iso", attuatore.Valore_iso));
                if (!string.IsNullOrWhiteSpace(attuatore.Valore_coppia)) curAttuatore.Add(string.Format(elemento, "Valore_coppia", attuatore.Valore_coppia));
                if (App.CurLang == "IT")
                {
                    if (!string.IsNullOrWhiteSpace(attuatore.note_footer)) curAttuatore.Add(string.Format(elemento, "note_footer", attuatore.note_footer));
                }
                else
                if (App.CurLang == "EN")
                {
                    if (!string.IsNullOrWhiteSpace(attuatore.note_footer_en)) curAttuatore.Add(string.Format(elemento, "note_footer_en", attuatore.note_footer_en));
                }
                if (IsLoggedIn)
                {
                    curAttuatore.Add(
                        string.Format(
                            "<form method='GET'>" +
                            "{2}&emsp;€ {3}&emsp;" +
                            "<input placeholder='Qta' type='number' name='qta' />" +
                                "<input type='hidden' name='idprodotto' value='{0}' />" +
                                "<input type='hidden' name='isvalvola' value='0' />" +
                                "<input type='hidden' name='idcodiceattuatore' value='{1}' />" +
                                "<input type='submit' value='Ordina' />" +
                            "</form>", curProdotto.idprodotto, attuatore.IdCodiceAttuatore,"Prezzo",attuatore.Prezzo.ToString("F")));
                }
                curAttuatore.Add(string.Format("<a href='{0}'>Mostra 3D</a>&emsp;<a href=''>Invia 3D</a>",attuatore.url3d));
                curAttuatore.Add("<hr/>");
                ritorno += string.Join("", curAttuatore);
            }
            return ritorno;
        }

        private string HtmlPerValvole()
        {
            //var valvole = (IEnumerable<Models.Valvola>) Articoli;
            string ritorno = string.Empty;
            string elemento = "{0}<br /><b>{1}</b><br />";
            foreach (Models.Valvola valvola in Articoli)
            {
                
                List<string> curValvola = new List<string>();
                if (!string.IsNullOrWhiteSpace(valvola.immagine_placeholder)) curValvola.Add(string.Format(@"<center><img src=""{0}"" /></center>", valvola.immagine_placeholder));
                if (!string.IsNullOrWhiteSpace(valvola.valore_azionamento)) curValvola.Add(string.Format(elemento, "valore_azionamento", valvola.valore_azionamento));
                if (!string.IsNullOrWhiteSpace(valvola.valore_materiale)) curValvola.Add(string.Format(elemento, "valore_materiale", valvola.valore_materiale));
                if (!string.IsNullOrWhiteSpace(valvola.valore_dn)) curValvola.Add(string.Format(elemento,"valore_dn", valvola.valore_dn));
                if (!string.IsNullOrWhiteSpace(valvola.valore_inch)) curValvola.Add(string.Format(elemento, "valore_inch", valvola.valore_inch));
                if (!string.IsNullOrWhiteSpace(valvola.valore_pnansi)) curValvola.Add(string.Format(elemento, "valore_pnansi", valvola.valore_pnansi));
                 if (!string.IsNullOrWhiteSpace(valvola.codice_articolo)) curValvola.Add(string.Format(elemento, "codice_articolo", valvola.codice_articolo));
                 if (!string.IsNullOrWhiteSpace(valvola.codice_attuatore)) curValvola.Add(string.Format(elemento, "Codice_Attuatore", valvola.codice_attuatore));
                 if (!string.IsNullOrWhiteSpace(valvola.codice_kit)) curValvola.Add(string.Format(elemento, "codice_kit", valvola.codice_kit));
                 if (!string.IsNullOrWhiteSpace(valvola.codice_valvola)) curValvola.Add(string.Format(elemento, "Codice_Valvola", valvola.codice_valvola));
                 if (!string.IsNullOrWhiteSpace(valvola.valore_nmm)) curValvola.Add(string.Format(elemento, "valore_nmm", valvola.valore_nmm));
                 if (!string.IsNullOrWhiteSpace(valvola.valore_hmm)) curValvola.Add(string.Format(elemento, "valore_hmm", valvola.valore_hmm));
                 if (!string.IsNullOrWhiteSpace(valvola.valore_pesokg)) curValvola.Add(string.Format(elemento, "valore_pesokg", valvola.valore_pesokg));
                if (App.CurLang == "IT")
                {
                    if (!string.IsNullOrWhiteSpace(valvola.note_footer)) curValvola.Add(string.Format(elemento, "note_footer", valvola.note_footer));
                } else
                if (App.CurLang == "EN")
                {
                    if (!string.IsNullOrWhiteSpace(valvola.note_footer_en)) curValvola.Add(string.Format(elemento, "note_footer_en", valvola.note_footer_en));
                }   
                if (IsLoggedIn)
                {
                    curValvola.Add(
                        string.Format(
                            "<form method='GET'>" +
                            "{2}&emsp;€ {3}&emsp;{4}&emsp;" +
                                "<input type='number' name='qta' />"+
                                "<input type='hidden' name='idprodotto' value='{0}' />" +
                                "<input type='hidden' name='isvalvola' value='1' />" +
                                "<input type='hidden' name='idcodicevalvola' value='{1}' />" +
                                "<input type='submit' value='Ordina' />" +
                            "</form>",curProdotto.idprodotto,  valvola.idcodicevalvola, "Prezzo",valvola.Prezzo.ToString("F") ,"Qta"));
                }
                if (!string.IsNullOrWhiteSpace(valvola.url_3d)) curValvola.Add(string.Format("<a href='{0}'>Mostra 3D</a>", valvola.url_3d));
                if (!string.IsNullOrWhiteSpace(valvola.url_download)) curValvola.Add(string.Format("<a href='{0}'>Download</a>", valvola.url_download));
                curValvola.Add("<hr/>");
                ritorno += string.Join("", curValvola);
            }
            return ritorno;

        }

        public ArticoliSearchResultVM()
        {
            MessagingCenter.Subscribe<Models.Messages.LoginOrLogoutActionMessage>(this, "LoginOrLogout", sender =>
            {
                OnPropertyChanged("ContentHtml");

            });
        }
    }
}
