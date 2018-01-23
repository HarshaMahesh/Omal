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
                if (parametri["isvalvola"].ToString() == "1")
                {
                    // Sto ordinando una valvola
                    var idcodicevalvola = parametri["idcodicevalvola"].ToString();
                    var valvola = ((IEnumerable<Models.Valvola>)Articoli).FirstOrDefault(x=>x.IdCodiceValvola == Convert.ToInt32(idcodicevalvola));
                    if (valvola == null) throw new KeyNotFoundException("IdCodiceValvola non trovato");
                    DataStore.Carrello.Add(new Models.Carrello() { CodiceArticolo = valvola.Codice_Articolo, DescrizioneCarrello_En = curProdotto.DescrizioneEn, DescrizioneCarrello_It = curProdotto.Descrizione, IdArticolo = valvola.IdCodiceValvola, IdOrdine = App.CurOrdine.IdOrdine, PrezzoUnitario = valvola.Prezzo, Qta = Convert.ToInt32(qta), Tipologia = curProdotto.Tipologia });
                }
                else
                {
                    // sto ordinando un attuatore
                    var idcodiceattuatore = parametri["idcodiceattuatore"].ToString();
                    var attuatore = ((IEnumerable<Models.Attuatore>)Articoli).FirstOrDefault(x => x.IdCodiceAttuatore == Convert.ToInt32(idcodiceattuatore));
                    if (attuatore == null) throw new KeyNotFoundException("IdCodiceAttuatore non trovato");
                    DataStore.Carrello.Add(new Models.Carrello() { CodiceArticolo = attuatore.Codice_Articolo, DescrizioneCarrello_En = curProdotto.DescrizioneEn, DescrizioneCarrello_It = curProdotto.Descrizione, IdArticolo = attuatore.IdCodiceAttuatore, IdOrdine = App.CurOrdine.IdOrdine, PrezzoUnitario = attuatore.Prezzo, Qta = Convert.ToInt32(qta), Tipologia = curProdotto.Tipologia });
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
                ritorno += string.Format("<p><b>{0}</b></p>", App.CurLang == "IT"? curProdotto.Nome:curProdotto.NomeEn);
                ritorno += string.Format("<p ALIGN='CENTER'>Trovati {0} articoli</p>", Articoli.Count());
                if (prodottoIsValvola)
                    ritorno += HtmlPerValvole();
                else
                    ritorno += HtmlPerAttuatori();
                if (!IsLoggedIn)
                {
                    ritorno += "<br><b>Per vedere i prezzi è necessario effettuare il login</b>";
                }
                return ritorno;
            }
        }


        private string HtmlPerAttuatori()
        {
            var attuatori = (IEnumerable<Models.Attuatore>)Articoli;
            string ritorno = string.Empty;
            string elemento = "<b>{0}</b><br />{1}";
            foreach (var attuatore in attuatori)
            {                
                List<string> curAttuatore = new List<string>();
                if (!string.IsNullOrWhiteSpace(attuatore.immagine_placeholder)) curAttuatore.Add(string.Format("<p ALIGN='CENTER'><img src='{0}' /></p>", attuatore.immagine_placeholder));
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
                    curAttuatore.Add(string.Format("<br /><p>{0}</p><p>€ {1}</p>", "Prezzo", attuatore.Prezzo.ToString("c2")));
                    curAttuatore.Add(
                        string.Format(
                            "<form method='GET'>" +
                            "<input type='number' name='qta' />" +
                                "<input type='hidden' name='idprodotto' value='{0}' />" +
                                "<input type='hidden' name='isvalvola' value='0' />" +
                                "<input type='hidden' name='idcodiceattuatore' value='{1}' />" +
                                "<input type='submit' value='Ordina' />" +
                            "</form>", curProdotto.IdProdotto, attuatore.IdCodiceAttuatore));
                }
                curAttuatore.Add("<p><a href=''>Mostra 3D</a>&emsp;<a href=''>Invia 3D</a></p>");
                curAttuatore.Add("<hr/>");
                ritorno += string.Join("<br />", curAttuatore);
            }
            return ritorno;
        }

        private string HtmlPerValvole()
        {
            var valvole = (IEnumerable<Models.Valvola>) Articoli;
            string ritorno = string.Empty;
            string elemento = "<b>{0}</b><br />{1}";
            foreach (var valvola in valvole)
            {
                
                List<string> curValvola = new List<string>();
                if (!string.IsNullOrWhiteSpace(valvola.immagine_placeholder)) curValvola.Add(string.Format("<p ALIGN='CENTER'><img src='{0}' /></p>", valvola.immagine_placeholder));
                if (!string.IsNullOrWhiteSpace(valvola.valore_azionamento)) curValvola.Add(string.Format(elemento, "valore_azionamento", valvola.valore_azionamento));
                if (!string.IsNullOrWhiteSpace(valvola.valore_materiale)) curValvola.Add(string.Format(elemento, "valore_materiale", valvola.valore_materiale));
                if (!string.IsNullOrWhiteSpace(valvola.valore_dn)) curValvola.Add(string.Format(elemento,"valore_dn", valvola.valore_dn));
                if (!string.IsNullOrWhiteSpace(valvola.valore_inch)) curValvola.Add(string.Format(elemento, "valore_inch", valvola.valore_inch));
                if (!string.IsNullOrWhiteSpace(valvola.valore_pnansi)) curValvola.Add(string.Format(elemento, "valore_pnansi", valvola.valore_pnansi));
                 if (!string.IsNullOrWhiteSpace(valvola.Codice_Articolo)) curValvola.Add(string.Format(elemento, "codice_articolo", valvola.Codice_Articolo));
                 if (!string.IsNullOrWhiteSpace(valvola.Codice_Attuatore)) curValvola.Add(string.Format(elemento, "Codice_Attuatore", valvola.Codice_Attuatore));
                 if (!string.IsNullOrWhiteSpace(valvola.Codice_Kit)) curValvola.Add(string.Format(elemento, "codice_kit", valvola.Codice_Kit));
                 if (!string.IsNullOrWhiteSpace(valvola.Codice_Valvola)) curValvola.Add(string.Format(elemento, "Codice_Valvola", valvola.Codice_Valvola));
                 if (!string.IsNullOrWhiteSpace(valvola.Valore_nmm)) curValvola.Add(string.Format(elemento, "valore_nmm", valvola.Valore_nmm));
                 if (!string.IsNullOrWhiteSpace(valvola.Valore_hmm)) curValvola.Add(string.Format(elemento, "valore_hmm", valvola.Valore_hmm));
                 if (!string.IsNullOrWhiteSpace(valvola.Valore_pesokg)) curValvola.Add(string.Format(elemento, "valore_pesokg", valvola.Valore_pesokg));
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
                    curValvola.Add(string.Format("<br /><p>{0}</p><p>€ {1}</p>", "Prezzo", valvola.Prezzo.ToString("c2")));
                    curValvola.Add(
                        string.Format(
                            "<form method='GET'>" +
                                "<input type='number' name='qta' />"+
                                "<input type='hidden' name='idprodotto' value='{0}' />" +
                                "<input type='hidden' name='isvalvola' value='1' />" +
                                "<input type='hidden' name='idcodicevalvola' value='{1}' />" +
                                "<input type='submit' value='Ordina' />" +
                                "</form>",curProdotto.IdProdotto,  valvola.IdCodiceValvola));
                }
                curValvola.Add("<p><a href=''>Mostra 3D</a>&emsp;<a href=''>Invia 3D</a></p>");
                curValvola.Add("<hr/>");
                ritorno += string.Join("<br />", curValvola);
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
