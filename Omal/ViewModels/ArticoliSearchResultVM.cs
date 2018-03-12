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
                }
                var codiceProdotto = parametri["idprodotto"].ToString();
                var qta = parametri["qta"].ToString();
                int IQta;
                if (!int.TryParse(qta,out IQta))
                {
                    CurPage.DisplayAlert(TitoloCarrello, ErrNumeroCarrello, "OK");
                    return;
                }
                if (IQta < 0)
                {
                    CurPage.DisplayAlert(TitoloCarrello, ErrQtaMaggiore0, "OK");
                    return; 
                }
                if (parametri["isvalvola"].ToString() == "1")
                {
                    // Sto ordinando una valvola
                    var idcodicevalvola = parametri["idcodicevalvola"].ToString();
                    int IntIdCodiceValvola = Convert.ToInt32(idcodicevalvola);
                    var valvole = Articoli.Select(x => (Models.Valvola)x).ToList();
                    var valvola = valvole.FirstOrDefault(x => x.idcodicevalvola == IntIdCodiceValvola);

                    if (valvola == null) throw new KeyNotFoundException("IdCodiceValvola non trovato");
                    var elementoCarrello = App.CurOrdine.carrelli.FirstOrDefault(x => x.IdArticolo == valvola.idcodicevalvola && x.Tipologia == CurProdotto.tipologia && x.IdProdotto == CurProdotto.idprodotto);
                    if (elementoCarrello == null)
                        App.CurOrdine.carrelli.Add(new Models.Carrello() { CodiceArticolo = valvola.codice_articolo, DescrizioneCarrello_En = curProdotto.descrizione_en, DescrizioneCarrello_It = curProdotto.descrizione, IdArticolo = valvola.idcodicevalvola, IdOrdine = App.CurOrdine.IdOrdine, PrezzoUnitario = valvola.Prezzo, Qta = Convert.ToInt32(qta), Tipologia = curProdotto.tipologia, IdProdotto = CurProdotto.idprodotto });
                    else
                        elementoCarrello.Qta += IQta;                                                                              
                }
                else
                {
                    // Sto ordinando una valvola
                    var idcodiceattuatore = parametri["idcodiceattuatore"].ToString();
                    int IntIdCodiceAttuatore = Convert.ToInt32(idcodiceattuatore);
                    var attuatori = Articoli.Select(x => (Models.Attuatore)x).ToList();
                    var attuatore = attuatori.FirstOrDefault(x => x.idcodiceattuatore == IntIdCodiceAttuatore);

                    if (attuatore == null) throw new KeyNotFoundException("IdCodiceAttuatore non trovato");
                    var elementoCarrello = App.CurOrdine.carrelli.FirstOrDefault(x => x.IdArticolo == attuatore.idcodiceattuatore && x.Tipologia == CurProdotto.tipologia && x.IdProdotto == CurProdotto.idprodotto);
                    if (elementoCarrello == null)
                        App.CurOrdine.carrelli.Add(new Models.Carrello() { CodiceArticolo = attuatore.codice_articolo, DescrizioneCarrello_En = curProdotto.descrizione_en, DescrizioneCarrello_It = curProdotto.descrizione, IdArticolo = attuatore.idcodiceattuatore, IdOrdine = App.CurOrdine.IdOrdine, PrezzoUnitario = attuatore.Prezzo, Qta = Convert.ToInt32(qta), Tipologia = curProdotto.tipologia, IdProdotto = CurProdotto.idprodotto });
                    else
                        elementoCarrello.Qta += IQta;     
                }
                MessagingCenter.Send<Models.Messages.BasketEditedMessage>(new Models.Messages.BasketEditedMessage(){ },"");
                CurPage.DisplayAlert(TitoloCarrello, MsgArticoliAggiuntiAlCarrello, "OK");
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
                ritorno += string.Format(@"<p style=""color:#004899;background-color:#EAEAEA"" align='center'><b>{0}</b></p>", App.CurLang == "IT"? curProdotto.nome:curProdotto.nome_en);
                ritorno += string.Format("<p ALIGN='CENTER'>{0}</p>", string.Format(MsgRisultatoRicercaArticoli, Articoli.Count()));
                if (prodottoIsValvola)
                    ritorno += HtmlPerValvole();
                else
                    ritorno += HtmlPerAttuatori();
               
                var baseStr = @"<html><head><style type=""text/css"">" +
                @" @font-face {
                    font-family: Montserrat-Regular;"
                    + @"src: url(""file:///android_asset/fonts/Montserrat-Regular.ttf"")" +
            
           @"}
        body {
                font-family: Montserrat-Regular;
                text - align: justify;
            }
.button {
    background-color: #60A5D1;
    border: 1px;
    color: white;
    padding: 5px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin: 2px 2px;
}

.button4 {border-radius: 12px;}
    </style>
</head>
<body>" + ritorno +
   @"</body>
   </html>";
                return baseStr;
            }
        }


        private string HtmlPerAttuatori()
        {
            //var attuatori = (IEnumerable<Models.Attuatore>)Articoli;
            string ritorno = string.Empty;
            string elemento = @"<center><span style=""color:#004899;font-size: 8px"">{0}</span><br /><b>{1}</b></center>";
            foreach (Models.Attuatore attuatore in Articoli)
            {                
                List<string> curAttuatore = new List<string>();
                if (!string.IsNullOrWhiteSpace(attuatore.immagine_placeholder)) 
                    curAttuatore.Add(string.Format("<P ALIGN='CENTER'><a href='{1}'><img Height='100' src='{0}' /></a></P>", attuatore.immagine_placeholder, string.Format("local_{0}", attuatore.immagine_placeholder)));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_iso)) curAttuatore.Add(string.Format(elemento, StrValoreIso, attuatore.valore_iso));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_coppia)) curAttuatore.Add(string.Format(elemento, StrValoreCoppia, attuatore.valore_coppia));
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
                            "<center><form method='GET'>" +
                            @"<div style='height: 35px;'><p style='line - height:35px;display: table-cell; vertical-align: middle; padding: 10px;' align='center'><span style='color:#004899;font-size: 16px'><b>€{3}</b></span>&emsp;" +
                            @"{2}:<input type='number' name='qta' style=""width: 4em;"" />&emsp;" +
                                "<input type='hidden' name='idprodotto' value='{0}' />" +
                                "<input type='hidden' name='isvalvola' value='0' />" +
                            "<input type='hidden' name='idcodiceattuatore' value='{1}' />" +
                            "<input type='submit' class='button button4' value='{4}' /></p></div></center>" +
                            "</form>", curProdotto.idprodotto, attuatore.idcodiceattuatore, StrQta, attuatore.Prezzo.ToString("F"),BtnOrdina.ToUpper()));
                    if (!string.IsNullOrWhiteSpace(attuatore.url_3d)) curAttuatore.Add(string.Format("<a class='button button4' href='{0}'>{1}</a>", attuatore.url_3d, BtnMostra3D.ToUpper()));
                    if (!string.IsNullOrWhiteSpace(attuatore.url_download)) curAttuatore.Add(string.Format("<a class='button button4' href='{0}'>{1}</a>", attuatore.url_download, BtnDownload.ToUpper()));
                }
                else
                {
                    curAttuatore.Add(string.Format("<br><b>{0}</b>", ErrPerPrezziNecessarioLogin));
                }

                curAttuatore.Add("<hr/>");
                ritorno += string.Join("", curAttuatore);
            }
            return ritorno;
        }

        private string HtmlPerValvole()
        {
            //var valvole = (IEnumerable<Models.Valvola>) Articoli;
            string ritorno = string.Empty;
            string elemento = @"<center><span style=""color:#004899;font-size: 8px"">{0}</span><br /><b>{1}</b></center>";
            foreach (Models.Valvola valvola in Articoli)
            {
                
                List<string> curValvola = new List<string>();
                if (!string.IsNullOrWhiteSpace(valvola.codice_articolo)) 
                    curValvola.Add(string.Format(@"<p style=""color:#004899;font-size: 18px"" align='center'><b>{0}</b></p>", valvola.codice_articolo));
                if (!string.IsNullOrWhiteSpace(valvola.immagine_placeholder)) 
                    curValvola.Add(string.Format("<P ALIGN='CENTER'><a href='{1}'><img Height='100' src='{0}' /></a></P>", valvola.immagine_placeholder, string.Format("local_{0}", valvola.immagine_placeholder)));
                if (!string.IsNullOrWhiteSpace(valvola.valore_azionamento)) curValvola.Add(string.Format(elemento, StrAzionamento, valvola.valore_azionamento));
                if (!string.IsNullOrWhiteSpace(valvola.valore_materiale)) curValvola.Add(string.Format(elemento, StrMateriale, valvola.valore_materiale));
                if (!string.IsNullOrWhiteSpace(valvola.valore_dn)) curValvola.Add(string.Format(elemento,StrDn, valvola.valore_dn));
                if (!string.IsNullOrWhiteSpace(valvola.valore_inch)) curValvola.Add(string.Format(elemento, StrInch, valvola.valore_inch));
                if (!string.IsNullOrWhiteSpace(valvola.valore_pnansi)) curValvola.Add(string.Format(elemento, StrPnasi, valvola.valore_pnansi));
                if (!string.IsNullOrWhiteSpace(valvola.codice_attuatore)) curValvola.Add(string.Format(elemento, StrAttuatore, valvola.codice_attuatore));
                if (!string.IsNullOrWhiteSpace(valvola.codice_kit)) curValvola.Add(string.Format(elemento, StrKit, valvola.codice_kit));
                if (!string.IsNullOrWhiteSpace(valvola.codice_valvola)) curValvola.Add(string.Format(elemento, StrValvola, valvola.codice_valvola));
                if (!string.IsNullOrWhiteSpace(valvola.valore_nmm)) curValvola.Add(string.Format(elemento, StrNmm, valvola.valore_nmm));
                if (!string.IsNullOrWhiteSpace(valvola.valore_hmm)) curValvola.Add(string.Format(elemento, StrHmm, valvola.valore_hmm));
                if (!string.IsNullOrWhiteSpace(valvola.valore_pesokg)) curValvola.Add(string.Format(elemento,StrPesoKg, valvola.valore_pesokg));
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
                            "<center><form method='GET'>" +
                            @"<div style='height: 35px;'><p style='line - height:35px;display: table-cell; vertical-align: middle; padding: 10px;'><span style='color:#004899;font-size: 16px'><b>€{3}</b></span>&emsp;" +
                            @"{2}:<input type='number' name='qta' style=""width: 4em;"" />&emsp;"+
                                "<input type='hidden' name='idprodotto' value='{0}' />" +
                                "<input type='hidden' name='isvalvola' value='1' />" +
                                "<input type='hidden' name='idcodicevalvola' value='{1}' />" +
                            "<input type='submit' class='button button4' value='{4}' /></p></div>" +
                            "</form></center>",curProdotto.idprodotto,  valvola.idcodicevalvola, StrQta,valvola.Prezzo.ToString("F"),BtnOrdina.ToUpper() ));
                    if (!string.IsNullOrWhiteSpace(valvola.url_3d)) curValvola.Add(string.Format("<a class='button button4' href='{0}'>{1}</a>", valvola.url_3d, BtnMostra3D.ToUpper()));
                    if (!string.IsNullOrWhiteSpace(valvola.url_download)) curValvola.Add(string.Format("<a class='button button4' href='{0}'>{1}</a>", valvola.url_download, BtnDownload.ToUpper()));
                } else
                {
                    curValvola.Add( string.Format("<br><b>{0}</b>", ErrPerPrezziNecessarioLogin));
                }
                curValvola.Add("<hr/>");
                ritorno += string.Format("<p align='center'>{0}</p>", string.Join("", curValvola));
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
