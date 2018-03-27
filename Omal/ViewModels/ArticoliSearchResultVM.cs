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
                        App.CurOrdine.carrelli.Add(new Models.Carrello() { CodiceArticolo = valvola.codice_articolo, DescrizioneCarrello_En = string.Format("{0}", curProdotto.nome_en), DescrizioneCarrello_It =string.Format("{0}", curProdotto.nome), IdArticolo = valvola.idcodicevalvola, IdOrdine = App.CurOrdine.IdOrdine, PrezzoUnitario = valvola.Prezzo, Qta = Convert.ToInt32(qta), Tipologia = curProdotto.tipologia, IdProdotto = CurProdotto.idprodotto });
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
                        App.CurOrdine.carrelli.Add(new Models.Carrello() { CodiceArticolo = attuatore.codice_articolo, DescrizioneCarrello_En = string.Format("{0}", curProdotto.nome_en), DescrizioneCarrello_It = string.Format("{0}", curProdotto.nome), IdArticolo = attuatore.idcodiceattuatore, IdOrdine = App.CurOrdine.IdOrdine, PrezzoUnitario = attuatore.Prezzo, Qta = Convert.ToInt32(qta), Tipologia = curProdotto.tipologia, IdProdotto = CurProdotto.idprodotto });
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
                ritorno += string.Format(@"<p style=""font-family:Montserrat-Bold;color:#004899;background-color:#EAEAEA;font-size:18px;font-weight: bold"" align='center'>{0}</p>", App.CurLang == "IT"? curProdotto.nome:curProdotto.nome_en);
                ritorno += string.Format("<p ALIGN='CENTER'>{0}</p>", string.Format(MsgRisultatoRicercaArticoli, Articoli.Count()));
                if (prodottoIsValvola)
                    ritorno += HtmlPerValvole();
                else
                    ritorno += HtmlPerAttuatori();
               
                var baseStr = @"<html><head><style type=""text/css"">" +
                @" @font-face {
                    font-family: Montserrat-Regular;"
                    + @"src: url(""file:///android_asset/fonts/Montserrat-Regular.ttf"")}" +
                  
                    @" @font-face {
                    font-family: Montserrat-Bold;"
                    + @"src: url(""file:///android_asset/fonts/Montserrat-Bold.ttf"")}" +
           @"
        body {
                font-family: Montserrat-Regular;
                text-align: justify;
            }
.button {
    background-color: #60A5D1;
    border: 1px;
    color: white;
    padding: 14px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin: 2px 2px;
}
.buttongrigio {
    background-color: #EAEAEA;
    border: 1px;
    color: #5E5B5B;
    padding: 14px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin: 2px 2px;
}
input[type=number] {padding:5px; border:0px solid #ccc; 
height: 40px;
font-size: 16px;
background: #EAEAEA;
    border-radius: 10px;
}
input[type=submit] {
    background-color: #60A5D1;
    border: 1px;
    color: white;
    padding: 14px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin: 2px 2px;
border-radius: 18px;
}
.button4 {border-radius: 18px;}
.NoBorder {border: 0px}
.WhiteBackGround {background:#FFFFFF;}


table:not(.nostile) {width: 100% ! important; margin: 0 auto !important;}
table:not(.nostile) td {border: 1px solid #ccc; padding: 5px; }
table:not(.nostile) td {border: 1px solid #ccc; padding: 5px; }

table:not(.nostile) tr:nth-child(odd) td{
           background:#EAEAEA;
}
table:not(.nostile) tr:nth-child(even) td{
            background:#FFFFFF;
}
.metadato-testo table:not(.nostile) {width: 100% ! important; margin: 0 auto !important;}
.metadato-testo table:not(.nostile) td {border: 0px solid #ccc;}
.metadato-testo table:not(.nostile) tr:first-child td {color: #174288; font-weight: bold; text-align: center;}
.metadato-testo table:not(.nostile) .fr-highlighted {color: #174288; font-weight: bold;}
table:not(.nostile) .fr-highlighted {color: #174288; font-weight: bold;}
img.middle {
    vertical-align: middle;
}

.columns {
    float: left;
    width: 20%;
}
.column {
    float: left;
    width: 40%;
}

/* Clear floats after the columns */
.row:after {
    content: "";
    display: table;
    clear: both;
}

.metadato-testo table {width: 100% ! important; margin: 0 auto !important;}
.metadato-testo table td {border: 1px solid #ccc;}
.metadato-testo table tr:first-child td {color: #174288; font-weight: bold; text-align: center;}
.metadato-testo table .fr-highlighted {color: #174288; font-weight: bold;}



    </style>
</head>
<body>" + ritorno +
   @"</body>
   </html>";
                return baseStr;
            }
        }


        string TraduzioneAzionamento(string azionamento)
        {
            try
            {

            if (!string.Equals(App.CurLang, "IT", StringComparison.InvariantCultureIgnoreCase))
            {
                var valori = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Descrizioni>>(CurProdotto.DescrizioniAzionamentiSerialized);
                var valore = valori.FirstOrDefault(x => string.Equals(x.valore, azionamento, StringComparison.InvariantCultureIgnoreCase));
                if (valore != null && (!string.IsNullOrWhiteSpace(valore.valore_en))) return valore.valore_en;
            }
            }
            catch (Exception ex)
            {

            }

            return azionamento;
        }

        string TraduzioneMateriali(string materiale)
        {
            try
            {
            if (!string.Equals(App.CurLang, "IT", StringComparison.InvariantCultureIgnoreCase))
            {
                var valori = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Descrizioni>>(CurProdotto.DescrizioniMaterialiSerialized);
                var valore = valori.FirstOrDefault(x => string.Equals(x.valore, materiale, StringComparison.InvariantCultureIgnoreCase));
                if (valore != null && (!string.IsNullOrWhiteSpace(valore.valore_en))) return valore.valore_en;
            }
            }
            catch (Exception ex)
            {

            }
            return materiale;
        }


        private string HtmlPerAttuatori()
        {
            //var attuatori = (IEnumerable<Models.Attuatore>)Articoli;
            string ritorno = string.Empty;
            foreach (Models.Attuatore attuatore in Articoli)
            {    
                string elemento = @"<center><span style=""color:#004899;font-size: 10px"">{0}</span><br /><b>{1}</b></center>";
                List<string> curAttuatore = new List<string>();
                if (!string.IsNullOrWhiteSpace(attuatore.codice_articolo))
                    curAttuatore.Add(string.Format(@"<p style=""color:#004899;font-size: 18px"" align='center'><b>{0}</b></p>", attuatore.codice_articolo));
                if (!string.IsNullOrWhiteSpace(attuatore.immagine_placeholder) &&  !string.IsNullOrWhiteSpace(attuatore.immagine_placeholder_dt))
                {
                    curAttuatore.Add(string.Format("<P ALIGN='CENTER'><a href='{1}'><img Height='100' src='{0}' /></a>&emsp;<a href='{3}'><img Height='100' src='{2}' /></a></P>", attuatore.immagine_placeholder, string.Format("local_{0}", attuatore.immagine_placeholder),attuatore.immagine_placeholder_dt, string.Format("local_{0}", attuatore.immagine_placeholder_dt)));
                } else
                if (!string.IsNullOrWhiteSpace(attuatore.immagine_placeholder)) 
                    curAttuatore.Add(string.Format("<P ALIGN='CENTER'><a href='{1}'><img Height='100' src='{0}' /></a></P>", attuatore.immagine_placeholder, string.Format("local_{0}", attuatore.immagine_placeholder)));
                curAttuatore.Add("<TABLE>");
                elemento = @"<TR><TD><center><span style=""color:#004899;font-size: 10px"">{0}</span><br /><b>{1}</b></center></TD></TR>";
                if (!string.IsNullOrWhiteSpace(attuatore.valore_misura)) curAttuatore.Add(string.Format(elemento, StrInfoProdottoPicker1Attuatore, attuatore.valore_misura));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_iso)) curAttuatore.Add(string.Format(elemento, StrValoreIso, attuatore.valore_iso));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_coppia)) curAttuatore.Add(string.Format(elemento, StrValoreCoppia, attuatore.valore_coppia));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_coppiabar)) curAttuatore.Add(string.Format(elemento, Strvalore_coppiabar, attuatore.valore_coppiabar));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_voltaggio)) curAttuatore.Add(string.Format(elemento, Strvalore_voltaggio, attuatore.valore_voltaggio));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_pesokgbar)) curAttuatore.Add(string.Format(elemento, Strvalore_pesokgbar, attuatore.valore_pesokgbar));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_pesokg)) curAttuatore.Add(string.Format(elemento, Strvalore_pesokgbar, attuatore.valore_pesokg));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_aria)) curAttuatore.Add(string.Format(elemento, Strvalore_aria, attuatore.valore_aria));
                if (!string.IsNullOrWhiteSpace(attuatore.codice_guarnizioni)) curAttuatore.Add(string.Format(elemento, Strcodice_guarnizioni, attuatore.codice_guarnizioni));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_ch)) curAttuatore.Add(string.Format(elemento, Strvalore_ch, attuatore.valore_ch));
                if (!string.IsNullOrWhiteSpace(attuatore.valore_ngiri)) curAttuatore.Add(string.Format(elemento, Strvalore_ngiri, attuatore.valore_ngiri));
                if (App.CurLang == "IT")
                {
                    if (!string.IsNullOrWhiteSpace(attuatore.note_footer)) curAttuatore.Add(string.Format(elemento, "note_footer", attuatore.note_footer));
                }
                else
                if (App.CurLang == "EN")
                {
                    if (!string.IsNullOrWhiteSpace(attuatore.note_footer_en)) curAttuatore.Add(string.Format(elemento, "note_footer_en", attuatore.note_footer_en));
                }
                curAttuatore.Add("</TABLE>");
                if (IsLoggedIn)
                {
                    if (attuatore.Prezzo>0)
                    curAttuatore.Add(
                        string.Format(
                            "<center><form method='GET'>" +
                            @"<div style='height: 35px;'><p style='line - height:35px;display: table-cell; vertical-align: middle; padding: 10px;' align='center'><span style='color:#004899;font-size: 16px'><b>€{3}</b></span>&emsp;" +
                            @"{2}:<input type='number' name='qta' style=""width: 4em;"" />&emsp;" +
                                "<input type='hidden' name='idprodotto' value='{0}' />" +
                                "<input type='hidden' name='isvalvola' value='0' />" +
                            "<input type='hidden' name='idcodiceattuatore' value='{1}' />" +
                            "<input type='submit' class='button button4' value='{4}' /></p></div></center><br />" +
                            "</form></center>", curProdotto.idprodotto, attuatore.idcodiceattuatore, StrQta, attuatore.Prezzo.ToString("F"),BtnOrdina.ToUpper()));
                    if (!string.IsNullOrWhiteSpace(attuatore.url_3d) || !string.IsNullOrWhiteSpace(attuatore.url_download))
                    {
                        var str = "<table class='nostile' align='center'><tbody><tr>";

                        if (!string.IsNullOrWhiteSpace(attuatore.url_3d) || !string.IsNullOrWhiteSpace(attuatore.url_download))
                            str += "<td class='NoBorder WhiteBackGround' valign='middle'><image alt='3D LOGO' height='60' width='60' src='http://demo.timmagine.com/omal/http/images/rendering_3d.png' /></td>";

                        if (!string.IsNullOrWhiteSpace(attuatore.url_3d))
                            str += string.Format("<td class='NoBorder WhiteBackGround' valign='middle'><a class='button buttongrigio button4' href='{0}'>{1}</a></td>", attuatore.url_3d, BtnMostra3D.ToUpper());
                        if (!string.IsNullOrWhiteSpace(attuatore.url_download))
                            str +=
                                string.Format("<td class='NoBorder WhiteBackGround' valign='middle'><a class='button buttongrigio button4' href='{0}'>{1}</a></td>",
                                              attuatore.url_download + "&email=" + App.CurToken.email_utente, BtnDownload.ToUpper());
                        str += "</tr></tbody></table>";
                        curAttuatore.Add(str);
                    }
                }
                else
                {
                    curAttuatore.Add(string.Format("<br><b>{0}</b>", ErrPerPrezziNecessarioLogin));
                }
                curAttuatore.Add(string.Format(@"<p align='center'><a class='button button4' href=""info_"">{0}</a></p>", BtnMaggioriInfo.ToUpper()));
                curAttuatore.Add("<hr/>");
                ritorno += string.Join("", curAttuatore);
            }
            return ritorno;
        }

        private string HtmlPerValvole()
        {
            //var valvole = (IEnumerable<Models.Valvola>) Articoli;
            string ritorno = string.Empty;
            foreach (Models.Valvola valvola in Articoli)
            {
                string elemento = @"<center><span style=""color:#004899;font-size: 10px"">{0}</span><br /><b>{1}</b></center>";
                List<string> curValvola = new List<string>();
                if (!string.IsNullOrWhiteSpace(valvola.codice_articolo)) 
                    curValvola.Add(string.Format(@"<p style=""color:#004899;font-size: 18px"" align='center'><b>{0}</b>{1}</p>", valvola.codice_articolo,!string.IsNullOrWhiteSpace(valvola.valore_ATEXopzione1)?"&emsp;<img class='middle' src='http://demo.timmagine.com/omal/http/images/atex.png' />":""));
                if (!string.IsNullOrWhiteSpace(valvola.immagine_placeholder) && !String.IsNullOrWhiteSpace(valvola.immagine_placeholder_dt))
                {
                    curValvola.Add(string.Format("<P ALIGN='CENTER'><a href='{1}'><img Height='100' src='{0}' /></a>&emsp;<a href='{3}'><img Height='100' src='{2}' /></a></P>", valvola.immagine_placeholder, string.Format("local_{0}", valvola.immagine_placeholder),valvola.immagine_placeholder_dt, string.Format("local_{0}", valvola.immagine_placeholder_dt)));
                } else
                if (!string.IsNullOrWhiteSpace(valvola.immagine_placeholder)) 
                    curValvola.Add(string.Format("<P ALIGN='CENTER'><a href='{1}'><img Height='100' src='{0}' /></a></P>", valvola.immagine_placeholder, string.Format("local_{0}", valvola.immagine_placeholder)));
                curValvola.Add("<TABLE>");
                elemento = @"<TR><TD><center><span style=""color:#004899;font-size: 10px"">{0}</span><br /><b>{1}</b></center></TD></TR>";
                if (!string.IsNullOrWhiteSpace(valvola.valore_azionamento)) curValvola.Add(string.Format(elemento, StrAzionamento, TraduzioneAzionamento(valvola.valore_azionamento)));
                if (!string.IsNullOrWhiteSpace(valvola.valore_materiale)) curValvola.Add(string.Format(elemento, StrMateriale, TraduzioneMateriali( valvola.valore_materiale)));
                if (!string.IsNullOrWhiteSpace(valvola.valore_dn)) curValvola.Add(string.Format(elemento,StrDn, valvola.valore_dn));
                if (!string.IsNullOrWhiteSpace(valvola.valore_inch)) curValvola.Add(string.Format(elemento, StrInch, valvola.valore_inch));
                if (!string.IsNullOrWhiteSpace(valvola.valore_pnansi)) curValvola.Add(string.Format(elemento, StrPnasi, valvola.valore_pnansi));
                if (!string.IsNullOrWhiteSpace(valvola.codice_attuatore)) curValvola.Add(string.Format(elemento, StrAttuatore, valvola.codice_attuatore));
                if (!string.IsNullOrWhiteSpace(valvola.codice_kit)) curValvola.Add(string.Format(elemento, StrKit, valvola.codice_kit));
                if (!string.IsNullOrWhiteSpace(valvola.codice_valvola)) curValvola.Add(string.Format(elemento, StrValvola, valvola.codice_valvola));
                if (!string.IsNullOrWhiteSpace(valvola.valore_nmm)) curValvola.Add(string.Format(elemento, StrNmm, valvola.valore_nmm));
                if (!string.IsNullOrWhiteSpace(valvola.valore_hmm)) curValvola.Add(string.Format(elemento, StrHmm, valvola.valore_hmm));
                if (!string.IsNullOrWhiteSpace(valvola.valore_pesokg)) curValvola.Add(string.Format(elemento,StrPesoKg, valvola.valore_pesokg));
                if (!string.IsNullOrWhiteSpace(valvola.valore_KITLEVAopzione2)) curValvola.Add(string.Format(elemento, Strvalore_KITLEVAopzione2, valvola.valore_KITLEVAopzione2));
                if (!string.IsNullOrWhiteSpace(valvola.valore_KITGUARNIZIONIopzione3)) curValvola.Add(string.Format(elemento, Strvalore_KITGUARNIZIONIopzione3, valvola.valore_KITGUARNIZIONIopzione3));
                if (!string.IsNullOrWhiteSpace(valvola.valore_LMMopzione4)) curValvola.Add(string.Format(elemento, Strvalore_LMMopzione4, valvola.valore_LMMopzione4));
                if (!string.IsNullOrWhiteSpace(valvola.valore_TENUTAopzione5)) curValvola.Add(string.Format(elemento, Strvalore_TENUTAopzione5, valvola.valore_TENUTAopzione5));
                if (!string.IsNullOrWhiteSpace(valvola.KIT_OTTURATORE_RICAMBIO)) curValvola.Add(string.Format(elemento, StrKIT_OTTURATORE_RICAMBIO, valvola.KIT_OTTURATORE_RICAMBIO));
                if (!string.IsNullOrWhiteSpace(valvola.KIT_TESTA_RICAMBIO)) curValvola.Add(string.Format(elemento, StrKIT_TESTA_RICAMBIO, valvola.KIT_TESTA_RICAMBIO));
                if (!string.IsNullOrWhiteSpace(valvola.P_INTERCETTATA)) curValvola.Add(string.Format(elemento, StrP_INTERCETTATA, valvola.P_INTERCETTATA));
                if (!string.IsNullOrWhiteSpace(valvola.P_COMANDO_BAR_MAX)) curValvola.Add(string.Format(elemento, StrP_COMANDO_BAR_MAX, valvola.P_COMANDO_BAR_MAX));
                if (!string.IsNullOrWhiteSpace(valvola.Kv)) curValvola.Add(string.Format(elemento, StrKv, valvola.Kv));
                if (!string.IsNullOrWhiteSpace(valvola.SH_TESTA_COMANDO)) curValvola.Add(string.Format(elemento, StrSH_TESTA_COMANDO, valvola.SH_TESTA_COMANDO));
                if (!string.IsNullOrWhiteSpace(valvola.passaggio_mm)) curValvola.Add(string.Format(elemento, Strpassaggio_mm, valvola.passaggio_mm));
                if (!string.IsNullOrWhiteSpace(valvola.P_INTERCETTATA_DP_max_bar)) curValvola.Add(string.Format(elemento, StrP_INTERCETTATA_DP_max_bar, valvola.P_INTERCETTATA_DP_max_bar));
                if (!string.IsNullOrWhiteSpace(valvola.codice_RIDUTTORE)) curValvola.Add(string.Format(elemento, Strcodice_RIDUTTORE, valvola.codice_RIDUTTORE));
                if (!string.IsNullOrWhiteSpace(valvola.KIT_VALVOLA_RIDUTTORE)) curValvola.Add(string.Format(elemento, StrKIT_VALVOLA_RIDUTTORE, valvola.KIT_VALVOLA_RIDUTTORE));
                if (!string.IsNullOrWhiteSpace(valvola.codice_KITMONTAGGIO)) curValvola.Add(string.Format(elemento, Strcodice_KITMONTAGGIO, valvola.codice_KITMONTAGGIO));
                if (!string.IsNullOrWhiteSpace(valvola.KIT_RICAMBIO)) curValvola.Add(string.Format(elemento, Strcodice_KIT_RICAMBIO, valvola.KIT_RICAMBIO));

                if (App.CurLang == "IT")
                {
                    if (!string.IsNullOrWhiteSpace(valvola.note_footer)) curValvola.Add(string.Format(elemento, "note_footer", valvola.note_footer));
                } else
                if (App.CurLang == "EN")
                {
                    if (!string.IsNullOrWhiteSpace(valvola.note_footer_en)) curValvola.Add(string.Format(elemento, "note_footer_en", valvola.note_footer_en));
                }   
                curValvola.Add("<TABLE>");
                if (IsLoggedIn)
                {
                    if (valvola.Prezzo > 0)
                    curValvola.Add(
                        string.Format(
                            "<center><form method='GET'>" +
                            @"<div style='height: 35px;'><p style='line - height:35px;display: table-cell; vertical-align: middle; padding: 10px;'><span style='color:#004899;font-size: 16px'><b>€{3}</b></span>&emsp;" +
                            @"{2}:<input type='number' name='qta' style=""width: 4em;"" />&emsp;"+
                                "<input type='hidden' name='idprodotto' value='{0}' />" +
                                "<input type='hidden' name='isvalvola' value='1' />" +
                                "<input type='hidden' name='idcodicevalvola' value='{1}' />" +
                            "<input type='submit' class='button button4' value='{4}' /></p></div></br>" +
                            "</form></center><br />",curProdotto.idprodotto,  valvola.idcodicevalvola, StrQta,valvola.Prezzo.ToString("F"),BtnOrdina.ToUpper() ));
                    if (!string.IsNullOrWhiteSpace(valvola.url_3d) || !string.IsNullOrWhiteSpace(valvola.url_download))
                    {
                        var str = "<div class='row'><div class='columns'><image alt='3D LOGO' height='42' width='42' src='http://demo.timmagine.com/omal/http/images/rendering_3d.png' /></div><div class='column'>";
                        if (!string.IsNullOrWhiteSpace(valvola.url_3d))
                            str += string.Format("<center><a class='button buttongrigio button4' href='{0}'>{1}</a></center>", valvola.url_3d, BtnMostra3D.ToUpper());
                        str += "</div><div class='column'>";
                        if (!string.IsNullOrWhiteSpace(valvola.url_download))
                            str +=
                                string.Format("<center><a class='button buttongrigio button4' href='{0}'>{1}</a></center>",
                                              valvola.url_download + "&email=" + App.CurToken.email_utente, BtnDownload.ToUpper());
                        str += "</div></div>";
                        curValvola.Add(str);
                    }
                } else
                {
                    curValvola.Add( string.Format("<br><center><b>{0}</b></center>", ErrPerPrezziNecessarioLogin));
                }
                curValvola.Add(string.Format(@"<p align='center'><a class='button button4' href=""info_"">{0}</a></p>", BtnMaggioriInfo.ToUpper()));
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
