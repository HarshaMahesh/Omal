using System;
using System.Collections.Generic;

namespace Omal.AppResources
{
    public class Traduzioni
    {
        public Traduzioni()
        {
        }

        string defaultLang = "IT";

        string GetCurvalueLang(Dictionary<string, string> elemento)
        {
            if (elemento == null) return string.Empty;
            if (elemento.ContainsKey(App.CurLang)) return elemento[App.CurLang];
            if (elemento.ContainsKey(defaultLang)) return elemento[defaultLang];
            return "OOOPS";
        }



        public string StrPasswordDimenticata { get { return GetCurvalueLang(_StrPasswordDimenticata); } }
        public string StrUpdateDb { get { return GetCurvalueLang(_StrUpdateDb); }}
        public string StrSedeProduttiva { get { return GetCurvalueLang(_StrSedeProduttiva); } }
        public string StrOrdineAnnullato { get { return GetCurvalueLang(_StrOrdineAnnullato); } }
        public string StrTrovaArticolo { get { return GetCurvalueLang(_StrTrovaArticolo); } }
        public string LogonRichiesto { get { return GetCurvalueLang(_LogonRichiesto); } }
        public string Aggiungi { get { return GetCurvalueLang(_Aggiungi); } }
        public string ClienteRicercato { get { return GetCurvalueLang(_ClienteRicercato); } }
        public string ProdottoRicercato { get { return GetCurvalueLang(_ProdottoRicercato); } }
        public string TitoloImpostazioni { get { return GetCurvalueLang(_TitoloImpostazioni); } }
        public string UltimoAggiornamento { get { return GetCurvalueLang(_UltimoAggiornamento); } }
        public string Aggiorna { get { return GetCurvalueLang(_Aggiorna); } }
        public string TitoloClienti { get { return GetCurvalueLang(_TitoloClienti); } }
        public string TitoloCerca { get { return GetCurvalueLang(_TitoloCerca); } }
        public string ProdottoPicker1 { get { return GetCurvalueLang(_ProdottoPicker1); } }
        public string ProdottoPicker2 { get { return GetCurvalueLang(_ProdottoPicker2); } }
        public string ProdottoPicker3 { get { return GetCurvalueLang(_ProdottoPicker3); } }
        public string TitoloContattiOmal { get { return GetCurvalueLang(_TitoloContattiOmal); } }
        public string TitoloLogin { get { return GetCurvalueLang(_TitoloLogin); } }
        public string TitoloArchivio { get { return GetCurvalueLang(_TitoloArchivio); } }
        public string TitoloCarrello { get { return GetCurvalueLang(_TitoloCarrello); } }
        public string TitoloDettaglioCliente { get { return GetCurvalueLang(_TitoloDettaglioCliente); } }
        public string TitoloProdotti { get { return GetCurvalueLang(_TitoloProdotti); } }
        public string TitoloProdotto { get { return GetCurvalueLang(_TitoloProdotto); } }
        public string TitoloArticoli { get { return GetCurvalueLang(_TitoloArticoli); } }
        public string TitoloAggiornaCatalogo { get { return GetCurvalueLang(_TitoloAggiornaCatalogo); } }
        public string TitoloOrdini { get { return GetCurvalueLang(_TitoloOrdini); } }
        public string BtnAnnulla { get { return GetCurvalueLang(_BtnAnnulla); } }
        public string BtnSalva { get { return GetCurvalueLang(_BtnSalva); } }
        public string BtnInvia { get { return GetCurvalueLang(_BtnInvia); } }
        public string BtnNuovo { get { return GetCurvalueLang(_BtnNuovo); } }

        public string LogonRichiestoOrdini { get { return GetCurvalueLang(_LogonRichiestoOrdini); } }
        public string Cliente { get { return GetCurvalueLang(_Cliente); } }
        public string Note { get { return GetCurvalueLang(_Note); } }
        public string StrTotaleOrdine { get { return GetCurvalueLang(_TotaleOrdine); } }
        public string StrTotaleScontato { get { return GetCurvalueLang(_StrTotaleScontato); } }
        public string StrQta { get { return GetCurvalueLang(_StrQta); } }
        public string LogonTesto { get { return GetCurvalueLang(_LogonTesto); } }

        public string StrRagioneSociale { get { return GetCurvalueLang(_StrRagioneSociale); } }
        public string StrPIva { get { return GetCurvalueLang(_StrPIva); } }
        public string StrSconto { get { return GetCurvalueLang(_StrSconto); } }
        public string StrCFiscale { get { return GetCurvalueLang(_StrCFiscale); } }
        public string StrPosizione { get { return GetCurvalueLang(_StrPosizione); } }
        public string StrIndirizzo { get { return GetCurvalueLang(_StrIndirizzo); } }
        public string StrCitta { get { return GetCurvalueLang(_StrCitta); } }
        public string StrCap { get { return GetCurvalueLang(_StrCap); } }
        public string StrProvincia { get { return GetCurvalueLang(_StrProvincia); } }
        public string StrNazione { get { return GetCurvalueLang(_StrNazione); } }
        public string StrContatti { get { return GetCurvalueLang(_StrContatti); } }
        public string StrEmail { get { return GetCurvalueLang(_StrEmail); } }
        public string StrTelefono { get { return GetCurvalueLang(_StrTelefono); } }
        public string StrFax { get { return GetCurvalueLang(_StrFax); } }
        public string StrElimina { get {return GetCurvalueLang(_StrElimina); } }
        public string StrTotaleOrdineSconto { get { return GetCurvalueLang(_StrTotaleOrdineSconto); }}
        public string StrInfoProdottoPicker1Valvola { get { return GetCurvalueLang(_StrInfoProdottoPicker1Valvola); } }
        public string StrInfoProdottoPicker2Valvola { get { return GetCurvalueLang(_StrInfoProdottoPicker2Valvola); } }
        public string StrInfoProdottoPicker3Valvola { get { return GetCurvalueLang(_StrInfoProdottoPicker3Valvola); } }
        public string StrInfoProdottoPicker4Valvola { get { return GetCurvalueLang(_StrInfoProdottoPicker4Valvola); } }
        public string StrInfoProdottoPicker1Attuatore { get { return GetCurvalueLang(_StrInfoProdottoPicker1Attuatore); } }
        public string StrInfoProdottoPicker2Attuatore { get { return GetCurvalueLang(_StrInfoProdottoPicker2Attuatore); } }
        public string StrInfoProdottoPicker3Attuatore { get { return GetCurvalueLang(_StrInfoProdottoPicker3Attuatore); } }
        public string StrBtnPulisci { get { return GetCurvalueLang(_StrBtnPulisci); } }
        public string LoginButton {get {return GetCurvalueLang(_LoginButton);}}
        public string NavigationBarLoginButton { get { return GetCurvalueLang(_NavigationBarLoginButton); } }
        public string BtnClienti { get { return GetCurvalueLang(_BtnClienti); } }
        public string BtnOrdini { get { return GetCurvalueLang(_BtnOrdini); } }
        public string BtnCerca { get { return GetCurvalueLang(_BtnCerca); } }
        public string BtnMaggioriInfo { get { return GetCurvalueLang(_BtnMaggioriInfo); } }
        public string BtnTrova { get { return GetCurvalueLang(_BtnTrova); } }
        public string BtnOrdina { get { return GetCurvalueLang(_BtnOrdina); } }
        public string BtnRiprova { get { return GetCurvalueLang(_BtnRiprova); } }
        public string BtnMostra3D{ get { return GetCurvalueLang(_BtnMostra3D); } }
        public string BtnDownload { get { return GetCurvalueLang(_BtnDownload); } }
        public string BtnAggiungi { get { return GetCurvalueLang(_BtnAggiungi); } }
        public string ErrNumeroCarrello { get { return GetCurvalueLang(_ErrNumeroCarrello); } }
        public string ErrQtaMaggiore0 { get { return GetCurvalueLang(_ErrQtaMaggiore0); } }

        public string ErrPerPrezziNecessarioLogin { get { return GetCurvalueLang(_ErrPerPrezziNecessarioLogin); } }
        public string MsgArticoliAggiuntiAlCarrello { get { return GetCurvalueLang(_MsgArticoliAggiuntiAlCarrello); } }
        public string MsgRisultatoRicercaArticoli { get { return GetCurvalueLang(_MsgRisultatoRicercaArticoli); } }

        public string StrAzionamento { get { return GetCurvalueLang(_StrAzionamento); } }
        public string StrMateriale { get { return GetCurvalueLang(_StrMateriale); } }
        public string StrDn { get { return GetCurvalueLang(_StrDn); } }
        public string StrInch { get { return GetCurvalueLang(_StrInch); } }
        public string StrPnasi { get { return GetCurvalueLang(_StrPnasi); } }
        public string StrAttuatore { get { return GetCurvalueLang(_StrAttuatore); } }
        public string StrKit { get { return GetCurvalueLang(_StrKit); } }
        public string StrValvola { get { return GetCurvalueLang(_StrValvola); } }
        public string StrNmm { get { return GetCurvalueLang(_StrNmm); } }
        public string StrHmm { get { return GetCurvalueLang(_StrHmm); } }
        public string StrPesoKg { get { return GetCurvalueLang(_StrPesoKg); } }

        public string StrValoreIso { get { return GetCurvalueLang(_StrValoreIso); } }
        public string StrValoreCoppia { get { return GetCurvalueLang(_StrValoreCoppia); } }
        public string StrUltimoAggiornamento { get { return GetCurvalueLang(_StrUltimoAggiornamento); } }
        public string StrCarrelloVuoto { get { return GetCurvalueLang(_StrCarrelloVuoto); } }
        public string StrCarrelloNrArticoli { get { return GetCurvalueLang(_StrCarrelloNrArticoli); } }
        public string StrLingua { get { return GetCurvalueLang(_StrLingua); } }
        public string StrVersioneItaliana { get { return "Versione Italiana"; } }
        public string StrVersioneInglese { get { return "English Version"; } }
        public string StrNessunContatto { get { return GetCurvalueLang(_StrNessunContatto); } }
        public string StrTrovatiNrContatti { get { return GetCurvalueLang(_StrTrovatiNrContatti); } }
        public string StrTrovatoUnContatto { get { return GetCurvalueLang(_StrTrovatoUnContatto); } }

        public string StrNessunProdottoTrovato { get { return GetCurvalueLang(_StrNessunProdottoTrovato); } }
        public string StrTrovatounSoloProdotto { get { return GetCurvalueLang(_StrTrovatounSoloProdotto); } }
        public string StrTrovatiNrProdotti { get { return GetCurvalueLang(_StrTrovatiNrProdotti); } }
        public string SrtConnessioneAssente { get { return GetCurvalueLang(_SrtConnessioneAssente); } }




        public string StrSicuroAnnullareCarrello { get { return GetCurvalueLang(_StrSicuroAnnullareCarrello); } }
        public string StrOrdineBozza { get { return GetCurvalueLang(_StrOrdineBozza); } }
        public string StrOrdineStatoAnnullato { get { return GetCurvalueLang(_StrOrdineStatoAnnullato); } }
        public string StrOrdineEvaso { get { return GetCurvalueLang(_StrOrdineEvaso); } }
        public string StrOrdineInviato { get { return GetCurvalueLang(_StrOrdineInviato); } }
        public string StrSicuroInviareCarrello { get { return GetCurvalueLang(_StrSicuroInviareCarrello); } }
        public string StrSicuroSalvareCarrello { get { return GetCurvalueLang(_StrSicuroSalvareCarrello); } }
        public string StrSalvataggioCarrelloCompletato { get { return GetCurvalueLang(_StrSalvataggioCarrelloCompletato); } }
        public string StrSi { get { return GetCurvalueLang(_StrSi); } }
        public string StrNo { get { return GetCurvalueLang(_StrNo); } }
        public string StrCaricaOrdineSelezionato { get { return GetCurvalueLang(_StrCaricaOrdineSelezionato); } }
        public string StrOrdineCaricato { get { return GetCurvalueLang(_StrOrdineCaricato); } }
        public string StrOrdineCreato { get { return GetCurvalueLang(_StrOrdineCreato); } }
        public string StrConfermaEliminazione { get { return GetCurvalueLang(_StrConfermaEliminazione); } }
        public string Strvalore_KITLEVAopzione2 { get { return GetCurvalueLang(_Strvalore_KITLEVAopzione2); } }
        public string Strvalore_KITGUARNIZIONIopzione3 { get { return GetCurvalueLang(_Strvalore_KITGUARNIZIONIopzione3); } }
        public string Strvalore_LMMopzione4 { get { return GetCurvalueLang(_Strvalore_LMMopzione4); } }
        public string Strvalore_TENUTAopzione5 { get { return GetCurvalueLang(_Strvalore_TENUTAopzione5); } }
        public string StrKIT_OTTURATORE_RICAMBIO { get { return GetCurvalueLang(_StrKIT_OTTURATORE_RICAMBIO); } }
        public string StrKIT_TESTA_RICAMBIO { get { return GetCurvalueLang(_StrKIT_TESTA_RICAMBIO); } }
        public string StrP_INTERCETTATA { get { return GetCurvalueLang(_StrP_INTERCETTATA); } }
        public string StrP_COMANDO_BAR_MIN { get { return GetCurvalueLang(_StrP_COMANDO_BAR_MIN); } }
        public string StrP_COMANDO_BAR_MAX { get { return GetCurvalueLang(_StrP_COMANDO_BAR_MAX); } }
        public string StrKv { get { return GetCurvalueLang(_StrKv); } }
        public string StrSH_TESTA_COMANDO { get { return GetCurvalueLang(_StrSH_TESTA_COMANDO); } }
        public string Strpassaggio_mm { get { return GetCurvalueLang(_Strpassaggio_mm); } }
        public string StrP_INTERCETTATA_DP_max_bar { get { return GetCurvalueLang(_Strpassaggio_mm); } }
        public string Strvalore_coppiabar { get { return GetCurvalueLang(_Strvalore_coppiabar); } }
        public string Strvalore_voltaggio { get { return GetCurvalueLang(_Strvalore_voltaggio); } }
        public string Strvalore_pesokgbar { get { return GetCurvalueLang(_Strvalore_pesokgbar); } }
        public string Strvalore_aria { get { return GetCurvalueLang(_Strvalore_aria); } }
        public string Strcodice_guarnizioni { get { return GetCurvalueLang(_Strcodice_guarnizioni); } }
        public string Strvalore_ch { get { return GetCurvalueLang(_Strvalore_ch); } }
        public string Strvalore_ngiri { get { return GetCurvalueLang(_Strvalore_ngiri); } }
        public string Strcodice_RIDUTTORE { get { return GetCurvalueLang(_Strcodice_RIDUTTORE); } }
        public string StrKIT_VALVOLA_RIDUTTORE { get { return GetCurvalueLang(_StrKIT_VALVOLA_RIDUTTORE); } }
        public string Strcodice_KITMONTAGGIO { get { return GetCurvalueLang(_Strcodice_KITMONTAGGIO); } }
        public string Strcodice_KIT_RICAMBIO { get { return GetCurvalueLang(_Strcodice_KIT_RICAMBIO); } }
        public string StrOrdineCompletatoCompletato { get { return GetCurvalueLang(_StrOrdineCompletatoCompletato); } }
        public string ErrCampiOblCliente { get { return GetCurvalueLang(_ErrCampiOblCliente); } }
        public string MsgClienteEliminato { get { return GetCurvalueLang(_MsgClienteEliminato); } }
        public string StrPerdiModificheCarrello { get { return GetCurvalueLang(_StrPerdiModificheCarrello); } }
        public string StrConfermiLogout { get { return GetCurvalueLang(_StrConfermiLogout); } }
        public string BtnInsert { get { return GetCurvalueLang(_BtnInsert); } }
        public string BtnUpdate { get { return GetCurvalueLang(_BtnUpdate); } }
        public string BtnEdit { get { return GetCurvalueLang(_BtnEdit); } }
        public string StrSelezionaMisura { get { return GetCurvalueLang(_StrSelezionaMisura); } }
        public string AlertClienteInBozza { get { return GetCurvalueLang(_AlertClienteInBozza); } }
        public string BtnDocumenti { get { return GetCurvalueLang(_BtnDocumenti); } }
        public string TitoloDocumenti { get { return GetCurvalueLang(_TitoloDocumenti); } }
        public string StrEmailVuota { get { return GetCurvalueLang(_StrEmailVuota); } }
        public string StrConfermaInvioMailRecuperoPassword { get { return GetCurvalueLang(_StrConfermaInvioMailRecuperoPassword); } }

        public string OrdineClienteEliminato { get { return GetCurvalueLang(_OrdineClienteEliminato); } }



        Dictionary<string, string> _StrConfermaInvioMailRecuperoPassword = new Dictionary<string, string>
        {
            {"IT",  "Abbiamo inviato una email al tuo indirizzo.Controlla la posta per terminare la procedura di recupero"},
            {"EN",  "We have sent an email to your address. Check your mailbox to complete the recovery process"}
        };

        Dictionary<string, string> _StrEmailVuota = new Dictionary<string, string>
        {
            {"IT",  "Campo email non valorizzato"},
            {"EN",  "Field email cannot be empty"}
        };
        Dictionary<string, string> _StrPasswordDimenticata = new Dictionary<string, string>
        {
            {"IT",  "Password dimenticata?"},
            {"EN",  "Forgot password?"}
        };
        Dictionary<string, string> _TitoloDocumenti = new Dictionary<string, string>
        {
            {"IT",  "Documenti"},
            {"EN",  "Documents"}
        };
        Dictionary<string, string> _OrdineClienteEliminato = new Dictionary<string, string>
        {
            {"IT",  "ATTENZIONE Stai recuperando un ordine intestato ad un cliente eliminato. Per intestarlo ad un nuovo cliente e completarlo vai nella sezione dedicata"},
            {"EN",  "Attention: you are recovering an order for a deleted customer. To make it to a new customer and complete it, go to the order page"}
        };

        Dictionary<string, string> _BtnDocumenti = new Dictionary<string, string>
        {
            {"IT",  "Documenti"},
            {"EN",  "Documents"}
        };

        Dictionary<string, string> _AlertClienteInBozza = new Dictionary<string, string>
        {
            {"IT",  "ATTENZIONE Nel tuo archivio c’è una bozza ordine intestata a questo cliente. Sei sicuro di eliminarlo?"},
            {"EN",  "Attention: in your archive there is a draft order registered to this customer. Are you sure to delete it?"}
        };
        Dictionary<string, string> _LogonRichiestoOrdini = new Dictionary<string, string>
        {
            {"IT",  "Effettua il login e naviga il catalogo per creare il tuo prossimo ordine"},
            {"EN",  "Login and browse the catalog, to create your next order"}
        };
        Dictionary<string, string> _StrSelezionaMisura = new Dictionary<string, string>
        {
            {"IT",  "Seleziona misura"},
            {"EN",  "Size select"}
        };
        Dictionary<string, string> _SrtConnessioneAssente = new Dictionary<string, string>
        {
            {"IT",  "Connessione assente"},
            {"EN",  "No Connecttion.."}
        };
        Dictionary<string, string> _StrUpdateDb = new Dictionary<string, string>
        {
            {"IT",  "Aggiornamento catalogo in corso..."},
            {"EN",  "Updating catalogue in progress..."}
        };

        Dictionary<string, string> _BtnNuovo = new Dictionary<string, string>
        {
            {"IT",  "Nuovo"},
            {"EN",  "New"}
        };

        Dictionary<string, string> _StrOrdineStatoAnnullato = new Dictionary<string, string>
        {
            {"IT",  "Annullato"},
            {"EN",  "Cancelled"}
        };

        Dictionary<string, string> _StrSedeProduttiva = new Dictionary<string, string>
        {
            {"IT",  "Sede produttiva"},
            {"EN",  "Production site"}
        };

        Dictionary<string, string> _BtnAnnulla = new Dictionary<string, string>
        {
            {"IT",  "Annulla"},
            {"EN",  "Cancel"}
        };

        Dictionary<string, string> _BtnSalva = new Dictionary<string, string>
        {
            {"IT",  "Salva"},
            {"EN",  "Save"}
        };

        Dictionary<string, string> _BtnInvia = new Dictionary<string, string>
        {
            {"IT",  "Invia"},
            {"EN",  "Send"}
        };
        Dictionary<string, string> _StrOrdineEvaso = new Dictionary<string, string>
        {
            {"IT",  "Evaso"},
            {"EN",  "escaped"}
        };
        Dictionary<string, string> _StrOrdineInviato = new Dictionary<string, string>
        {
            {"IT",  "Inviato"},
            {"EN",  "Sended"}
        };

        Dictionary<string, string> _StrOrdineBozza = new Dictionary<string, string>
        {
            {"IT",  "Bozza"},
            {"EN",  "Draft"}
        };
        Dictionary<string, string> _BtnEdit = new Dictionary<string, string>
        {
            {"IT",  "Modifica"},
            {"EN",  "Edit"}
        };
        Dictionary<string, string> _BtnUpdate = new Dictionary<string, string>
        {
            {"IT",  "Aggiorna"},
            {"EN",  "Update"}
        };
        Dictionary<string, string> _BtnInsert = new Dictionary<string, string>
        {
            {"IT",  "Inserisci"},
            {"EN",  "Insert"}
        };
        Dictionary<string, string> _StrConfermiLogout = new Dictionary<string, string>
        {
            {"IT",  "Confermi il logout?"},
            {"EN",  "Do you want to logout?"}
        };

        Dictionary<string, string> _StrOrdineAnnullato = new Dictionary<string, string>
        {
            {"IT",  "ORDINE ANNULLATO. Per consultarlo vai nella sezione cestino."},
            {"EN",  "ORDER DELETED. To see it, go to the trash bin."}
        };
        Dictionary<string, string> _StrOrdineCreato = new Dictionary<string, string>
        {
            {"IT",  "Nuovo ordine creato con successo, è ora possibile procedere con la ricerca dei prodotti da aggiungere al carrello."},
            {"EN",  "New order was created with success. Now you can process to search new items to add to the basket."}
        };
        Dictionary<string, string> _StrPerdiModificheCarrello = new Dictionary<string, string>
        {
            {"IT",  "Risultano modifiche non salvate nell'attuale carrello, procedendo le modifiche verranno perse. Continuare?"},
            {"EN",  "There are unsaved items in the basket. Do you want to lost your update?"}
        };
        Dictionary<string, string> _MsgClienteEliminato = new Dictionary<string, string>
        {
            {"IT",  "CLIENTE ELIMINATO. L'anagrafica non compartirà più nell'elenco clienti dell'archivio"},
            {"EN",  "CUSTOMER DELETED"}
        };

        Dictionary<string, string> _ErrCampiOblCliente = new Dictionary<string, string>
        {
            {"IT",  "Risultano non compilatio i seguenti campi:"},
            {"EN",  "Theese fields are empty, but are required:"}
        };
        Dictionary<string, string> _StrOrdineCompletatoCompletato = new Dictionary<string, string>
        {
            {"IT",  "ORDINE INVIATO nr.{0}. Per consultarlo, controlla la tua casella di posta o vai nella sezione archivio ordini."},
            {"EN",  "ORDINE INVIATO nr.{0}. Per consultarlo, controlla la tua casella di posta o vai nella sezione archivio ordini."}
        };
        Dictionary<string, string> _Strcodice_KIT_RICAMBIO = new Dictionary<string, string>
        {
            {"IT",  "Kit ricambio"},
            {"EN",  "Spare part kit"}
        };
        Dictionary<string, string> _Strcodice_KITMONTAGGIO = new Dictionary<string, string>
        {
            {"IT",  "Kit montaggio"},
            {"EN",  "Mounting kit"}
        };
        Dictionary<string, string> _StrKIT_VALVOLA_RIDUTTORE = new Dictionary<string, string>
        {
            {"IT",  "Kit valvola-riduttore"},
            {"EN",  "Kit valve-gear box"}
        };
        Dictionary<string, string> _Strcodice_RIDUTTORE = new Dictionary<string, string>
        {
            {"IT",  "Riduttore"},
            {"EN",  "Gear Box"}
        };

        Dictionary<string, string> _Strvalore_ngiri = new Dictionary<string, string>
        {
            {"IT",  "N° Giri"},
            {"EN",  "N° of turns"}
        };

        Dictionary<string, string> _Strvalore_ch = new Dictionary<string, string>
        {
            {"IT",  "Ch"},
            {"EN",  "Ch"}
        };
        Dictionary<string, string> _Strcodice_guarnizioni = new Dictionary<string, string>
        {
            {"IT",  "Guarnizione di ricambio"},
            {"EN",  "Spare seal"}
        };
        Dictionary<string, string> _Strvalore_aria = new Dictionary<string, string>
        {
            {"IT",  "Volume aria [dm3/cycle]"},
            {"EN",  "Air volume [dm3/cycle]"}
        };

        Dictionary<string, string> _Strvalore_pesokgbar = new Dictionary<string, string>
        {
            {"IT",  "Peso a 2,8 bar [kg]"},
            {"EN",  "Weight 2,8 bat [kg]"}
        };
        Dictionary<string, string> _Strvalore_voltaggio = new Dictionary<string, string>
        {
            {"IT",  "Voltaggio"},
            {"EN",  "Voltague"}
        };
        Dictionary<string, string> _Strvalore_coppiabar = new Dictionary<string, string>
        {
            {"IT",  "Coppia a 5,6 bar [Nm]"},
            {"EN",  "Torque 5,6 bar [Nm]"}
        };
        Dictionary<string, string> _StrP_INTERCETTATA_DP_max_bar = new Dictionary<string, string>
        {
            {"IT",  "P intercettata [P max. bar]"},
            {"EN",  "P operating [P max. bar]"}
        };
        Dictionary<string, string> _Strpassaggio_mm = new Dictionary<string, string>
        {
            {"IT",  "Passaggio [mm]"},
            {"EN",  "Bore [mm]"}
        };
        Dictionary<string, string> _StrSH_TESTA_COMANDO = new Dictionary<string, string>
        {
            {"IT",  "Testa Comando"},
            {"EN",  "Control head"}
        };
        Dictionary<string, string> _StrKv = new Dictionary<string, string>
        {
            {"IT",  "Kv [m3/h]"},
            {"EN",  "Kv [m3/h]"}
        };
        Dictionary<string, string> _StrP_COMANDO_BAR_MAX = new Dictionary<string, string>
        {
            {"IT",  "P comando bar [max]"},
            {"EN",  "P control bar [max]"}
        };
        Dictionary<string, string> _StrP_COMANDO_BAR_MIN = new Dictionary<string, string>
        {
            {"IT",  "P comando bar [min]"},
            {"EN",  "P control bar [min]"}
        };
        Dictionary<string, string> _StrP_INTERCETTATA = new Dictionary<string, string>
        {
            {"IT",  "P intercettata [P max. bar]"},
            {"EN",  "P operating [P max. bar]"}
        };
        Dictionary<string, string> _StrKIT_TESTA_RICAMBIO = new Dictionary<string, string>
        {
            {"IT",  "Kit testa di ricambio"},
            {"EN",  "Head spare kit"}
        };
        Dictionary<string, string> _StrKIT_OTTURATORE_RICAMBIO = new Dictionary<string, string>
        {
            {"IT",  "Kit Otturatore di ricambio"},
            {"EN",  "Plug spare kit"}
        };
        Dictionary<string, string> _Strvalore_TENUTAopzione5 = new Dictionary<string, string>
        {
            {"IT",  "Guarnizione"},
            {"EN",  "Liner"}
        };
        Dictionary<string, string> _Strvalore_LMMopzione4 = new Dictionary<string, string>
        {
            {"IT",  "L [mm]"},
            {"EN",  "L [mm]"}
        };
        Dictionary<string, string> _Strvalore_KITGUARNIZIONIopzione3 = new Dictionary<string, string>
        {
            {"IT",  "Kit guarnizioni"},
            {"EN",  "Seals kit"}
        };
        Dictionary<string, string> _Strvalore_KITLEVAopzione2 = new Dictionary<string, string>
        {
            {"IT",  "Kit Leva"},
            {"EN",  "Lever kit"}
        };
        Dictionary<string, string> _StrConfermaEliminazione = new Dictionary<string, string>
        {
            {"IT",  "Conferma la volontà di eliminare l'elemento selezionato?"},
            {"EN",  "Do you want to delete selected item?"}
        };
        Dictionary<string, string> _StrElimina = new Dictionary<string, string>
        {
            {"IT",  "Elimina"},
            {"EN",  "Delete"}
        };


        Dictionary<string, string> _StrOrdineCaricato = new Dictionary<string, string>
        {
            {"IT",  "ORDINE RECUPERATO. Per completarlo vai nella sezione dedicata"},
            {"EN",  "ORDER RECOVERED. To complete it, go to the order page"}
        };

        Dictionary<string, string> _StrCaricaOrdineSelezionato = new Dictionary<string, string>
        {
            {"IT",  "RECUPERA ORDINE. Desideri recuperare l'ordine selezionato?"},
            {"EN",  "RECOVER ORDER. Do you want to recover this order?"}
        };
        Dictionary<string, string> _TitoloOrdini = new Dictionary<string, string>
        {
            {"IT",  "Ordini"},
            {"EN",  "Orders"}
        };
        Dictionary<string, string> _StrSalvataggioCarrelloCompletato = new Dictionary<string, string>
        {
            {"IT",  "BOZZA SALVATA. Per recuperarla vai nella sezione archivio ordini."},
            {"EN",  "DRAFT SAVED. To recover it, go to the orders archive page."}
        };


        Dictionary<string, string> _StrSicuroInviareCarrello = new Dictionary<string, string>
        {
            {"IT",  "INVIO ORDINE. Confermi l'invio dell'ordine ai seguenti destinatari {0} ?"},
            {"EN",  "SEND ORDER. Do you confirm the order sending to the following recipients {0}?"}
        };

        Dictionary<string, string> _StrSicuroSalvareCarrello = new Dictionary<string, string>
        {
            {"IT",  "SALVA BOZZA. Desideri salvare in bozza l’ordine corrente?"},
            {"EN",  "SAVE DRAFT. Do you want to save this order as draft?"}
        };
        Dictionary<string, string> _StrNo = new Dictionary<string, string>
        {
            {"IT",  "No"},
            {"EN",  "No"}
        };
        Dictionary<string, string> _StrSi = new Dictionary<string, string>
        {
            {"IT",  "Si"},
            {"EN",  "Yes"}
        };

        Dictionary<string, string> _StrSicuroAnnullareCarrello = new Dictionary<string, string>
        {
            {"IT",  "ANNULLA ORDINE. Sei sicuro di voler eliminare l'ordine corrente?"},
            {"EN",  "DELETE ORDER. Are you sure to delete this order?"}
        };
        Dictionary<string, string> _StrNessunContatto = new Dictionary<string, string>
        {
            {"IT",  "Nessun contatto"},
            {"EN",  "No contact found"}
        };

        Dictionary<string, string> _StrTrovatiNrContatti = new Dictionary<string, string>
        {
            {"IT",  "Trovati {0} contatti"},
            {"EN",  "{0} contacts found"}
        };

        Dictionary<string, string> _StrTrovatoUnContatto = new Dictionary<string, string>
        {
            {"IT",  "Trovato 1 contatto"},
            {"EN",  "1 contact found"}
        };



        Dictionary<string, string> _StrNessunProdottoTrovato = new Dictionary<string, string>
        {
            {"IT",  "Nessun prodotto"},
            {"EN",  "no product found"}
        };
        Dictionary<string, string> _StrTrovatounSoloProdotto = new Dictionary<string, string>
        {
            {"IT",  "Trovato 1 prodotto"},
            {"EN",  "1 product found"}
        };
        Dictionary<string, string> _StrTrovatiNrProdotti = new Dictionary<string, string>
        {
            {"IT",  "Trovati {0} prodotti"},
            {"EN",  "{0} products found"}
        };




        Dictionary<string, string> _BtnAggiungi = new Dictionary<string, string>
        {
            {"IT",  "Aggiungi"},
            {"EN",  "Add"}
        };

        Dictionary<string, string> _StrTotaleOrdineSconto = new Dictionary<string, string>
        {
            {"IT",  "Totale Scontato"},
            {"EN",  "Discounted Total"}
        };

        Dictionary<string, string> _StrLingua = new Dictionary<string, string>
        {
            {"IT",  "Lingua"},
            {"EN",  "Languages"}
        };
        Dictionary<string, string> _StrUltimoAggiornamento = new Dictionary<string, string>
        {
            {"IT",  "ultimo aggiornamento {0}. {1} Kb"},
            {"EN",  "last update {0}. {1} Kb"}
        };
        Dictionary<string, string> _BtnRiprova = new Dictionary<string, string>
        {
            {"IT",  "Riprova"},
            {"EN",  "Retry"}
        };
        Dictionary<string, string> _TitoloAggiornaCatalogo = new Dictionary<string, string>
        {
            {"IT",  "Aggiorna Catalogo"},
            {"EN",  "Update Catalogue"}
        };
        Dictionary<string, string> _StrCarrelloVuoto = new Dictionary<string, string>
        {
            {"IT",  "Carrello vuoto"},
            {"EN",  "Empty cart"}
        };
        Dictionary<string, string> _StrCarrelloNrArticoli = new Dictionary<string, string>
        {
            {"IT",  "{0} articoli nel carrello"},
            {"EN",  "{0} items in chart"}
        };

        Dictionary<string, string> _StrValoreIso = new Dictionary<string, string>
        {
            {"IT",  "ISO"},
            {"EN",  "ISO"}
        };
        Dictionary<string, string> _StrValoreCoppia = new Dictionary<string, string>
        {
            {"IT",  "Coppia [Nm]"},
            {"EN",  "Torque [Nm]"}
        };

        Dictionary<string, string> _StrAzionamento = new Dictionary<string, string>
        {
            {"IT",  "Azionamento"},
            {"EN",  "Drive"}
        };

        Dictionary<string, string> _StrMateriale = new Dictionary<string, string>
        {
            {"IT",  "Variante"},
            {"EN",  "Variant"}
        };
        Dictionary<string, string> _StrDn = new Dictionary<string, string>
        {
            {"IT",  "Misura"},
            {"EN",  "Size"}
        };
        Dictionary<string, string> _StrInch = new Dictionary<string, string>
        {
            {"IT",  "Misura"},
            {"EN",  "Size"}
        };
        Dictionary<string, string> _StrPnasi = new Dictionary<string, string>
        {
            {"IT",  "Pressione nominale"},
            {"EN",  "Nominal pressure"}
        };
        Dictionary<string, string> _StrAttuatore = new Dictionary<string, string>
        {
            {"IT",  "Attuatore"},
            {"EN",  "Actuator"}
        };
        Dictionary<string, string> _StrKit = new Dictionary<string, string>
        {
            {"IT",  "Kit di montaggio"},
            {"EN",  "Mounting kit"}
        };
        Dictionary<string, string> _StrValvola = new Dictionary<string, string>
        {
            {"IT",  "Valvola"},
            {"EN",  "Valve"}
        };
        Dictionary<string, string> _StrNmm = new Dictionary<string, string>
        {
            {"IT",  "N [mm]"},
            {"EN",  "N [mm]"}
        };
        Dictionary<string, string> _StrHmm = new Dictionary<string, string>
        {
            {"IT",  "H [mm]"},
            {"EN",  "H [mm]"}
        };
        Dictionary<string, string> _StrPesoKg = new Dictionary<string, string>
        {
                {"IT",  "Peso [kg]"},
                {"EN",  "Weight [kg]"}
        };





        Dictionary<string, string> _BtnMostra3D = new Dictionary<string, string>
        {
            {"IT",  "Mostra 3D"},
            {"EN",  "Show 3D"}
        };
        Dictionary<string, string> _BtnDownload = new Dictionary<string, string>
        {
            {"IT",  "INVIA 3D"},
            {"EN",  "SEND 3D"}
        };
        Dictionary<string, string> _BtnOrdina = new Dictionary<string, string>
        {
            {"IT",  "Ordina"},
            {"EN",  "Order"}
        };
        Dictionary<string, string> _MsgRisultatoRicercaArticoli = new Dictionary<string, string>
        {
            {"IT",  "Risultato Ricerca: {0} articoli"},
            {"EN",  "Search result: {0} items"}
        };
        Dictionary<string, string> _ErrPerPrezziNecessarioLogin = new Dictionary<string, string>
        {
            {"IT",  "Effettua il login per vedere prezzi e rendering 3D"},
            {"EN",  "Login to see prices and 3D rendering"}
        };
        Dictionary<string, string> _TitoloArticoli = new Dictionary<string, string>
        {
            {"IT",  "Articoli"},
            {"EN",  "Items"}
        };
        Dictionary<string, string> _MsgArticoliAggiuntiAlCarrello = new Dictionary<string, string>
        {
            {"IT",  "AGGIUNTO. Articolo aggiunto all'ordine. Per completarlo vai nella sezione dedicata"},
            {"EN",  "ADDED. Items added to the order. To complete it, go to the order page"}
        };

        Dictionary<string, string> _ErrQtaMaggiore0 = new Dictionary<string, string>
        {
            {"IT",  "Qtà non valida. Il numero deve essere maggiore di 0"},
            {"EN",  "Qty invalid. Number must be greater than 0"}
        };
        Dictionary<string, string> _TitoloProdotto = new Dictionary<string, string>
        {
            {"IT",  "Prodotto"},
            {"EN",  "Product"}
        };

        Dictionary<string, string> _ErrNumeroCarrello = new Dictionary<string, string>
        {
            {"IT",  "Qtà non valida. Numero intero richiesto."},
            {"EN",  "Qty invalid. Required integer value."}
        };
        Dictionary<string, string> _BtnTrova = new Dictionary<string, string>
        {
            {"IT", "Trova"},
            {"EN", "Find"}
        };
        Dictionary<string, string> _BtnMaggioriInfo = new Dictionary<string, string>
        {
            {"IT", "Maggiori Info"},
            {"EN", "More Info"}
        };

        Dictionary<string, string> _TitoloProdotti = new Dictionary<string, string>
        {
            {"IT", "Risultati"},
            {"EN", "Search Results"}
        };

        Dictionary<string, string> _BtnCerca = new Dictionary<string, string>
        {
            {"IT", "Cerca"},
            {"EN", "Search"}
        };

        Dictionary<string, string> _BtnClienti = new Dictionary<string, string>
        {
            {"IT", "Clienti"},
            {"EN", "Customers"}
        };

        Dictionary<string, string> _BtnOrdini = new Dictionary<string, string>
        {
            {"IT", "Ordini"},
            {"EN", "Orders"}
        };

        Dictionary<string, string> _TitoloCarrello = new Dictionary<string, string>
        {
            {"IT", "Ordini"},
            {"EN", "Orders"}
        };

        Dictionary<string, string> _TitoloArchivio = new Dictionary<string, string>
        {
            {"IT", "Archivio"},
            {"EN", "Archive"}
        };
        Dictionary<string, string> _NavigationBarLoginButton = new Dictionary<string, string>
        {
            {"IT", "Login"},
            {"EN", "Login"}
        };
        Dictionary<string, string> _LoginButton = new Dictionary<string, string>
        {
            {"IT", "Login"},
            {"EN", "Login"}
        };
        Dictionary<string, string> _StrBtnPulisci = new Dictionary<string, string>
        {
            {"IT", "Elimina"},
            {"EN", "Delete"}
        };

        Dictionary<string, string> _TitoloLogin = new Dictionary<string, string>
        {
            {"IT", "Accessi"},
            {"EN", "Login"}
        };


        Dictionary<string, string> _StrInfoProdottoPicker1Attuatore = new Dictionary<string, string>
        {
            {"IT", "Misura"},
            {"EN", "Measure"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker2Attuatore = new Dictionary<string, string>
        {
            {"IT", "ISO"},
            {"EN", "ISO"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker3Attuatore = new Dictionary<string, string>
        {
            {"IT", "Coppia"},
            {"EN", "Torque"}
        };

        Dictionary<string, string> _StrInfoProdottoPicker1Valvola = new Dictionary<string, string>
        {
            {"IT", "Azionamenti"},
            {"EN", "Drives"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker2Valvola = new Dictionary<string, string>
        {
            {"IT", "Misure DN"},
            {"EN", "DN Measures"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker3Valvola = new Dictionary<string, string>
        {
            {"IT", "Pressione nominale"},
            {"EN", "Nominal pressure"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker4Valvola = new Dictionary<string, string>
        {
            {"IT", "Varianti"},
            {"EN", "Variants"}
        };

        Dictionary<string, string> _StrTrovaArticolo = new Dictionary<string, string>
        {
            {"IT", "Configura azionamento"},
            {"EN", "Drives set up"}
        };
        Dictionary<string, string> _TitoloDettaglioCliente = new Dictionary<string, string>
        {
            {"IT", "Dettaglio Cliente"},
            {"EN", "Customer detail"}
        };
        Dictionary<string, string> _StrRagioneSociale = new Dictionary<string, string>
        {
            {"IT", "Ragione Sociale"},
            {"EN", "Business name"}
        };
        Dictionary<string, string> _StrPIva = new Dictionary<string, string>
        {
            {"IT", "P.Iva"},
            {"EN", "Vat nr."}
        };
        Dictionary<string, string> _StrCFiscale = new Dictionary<string, string>
        {
            {"IT", "C.Fiscale"},
            {"EN", "Nr. ID"}
        };
        Dictionary<string, string> _StrPosizione = new Dictionary<string, string>
        {
            {"IT", "Posizione"},
            {"EN", "Position"}
        };
        Dictionary<string, string> _StrIndirizzo = new Dictionary<string, string>
        {
            {"IT", "Indirizzo"},
            {"EN", "Address"}
        };
        Dictionary<string, string> _StrCitta = new Dictionary<string, string>
        {
            {"IT", "Città"},
            {"EN", "City"}
        };
        Dictionary<string, string> _StrCap = new Dictionary<string, string>
        {
            {"IT", "Cap"},
            {"EN", "Postal Code"}
        };
        Dictionary<string, string> _StrProvincia = new Dictionary<string, string>
        {
            {"IT", "Provincia"},
            {"EN", "Region"}
        };
        Dictionary<string, string> _StrContatti = new Dictionary<string, string>
        {
            {"IT", "Contatti"},
            {"EN", "Contacts"}
        };
        Dictionary<string, string> _StrEmail = new Dictionary<string, string>
        {
            {"IT", "Email"},
            {"EN", "Email"}
        };
        Dictionary<string, string> _StrTelefono = new Dictionary<string, string>
        {
            {"IT", "Telefono"},
            {"EN", "Telephone"}
        };
        Dictionary<string, string> _StrFax = new Dictionary<string, string>
        {
            {"IT", "Fax"},
            {"EN", "Fax"}
        };
        Dictionary<string, string> _StrNazione = new Dictionary<string, string>
        {
            {"IT", "Nazione"},
            {"EN", "Nation"}
        };
        Dictionary<string, string> _StrSconto = new Dictionary<string, string>
        {
            {"IT", "Sconto"},
            {"EN", "Discount"}
        };




        Dictionary<string, string> _StrQta = new Dictionary<string, string>
        {
            {"IT", "Qtà"},
            {"EN", "Qty"}
        };



        Dictionary<string, string> _StrTotaleScontato = new Dictionary<string, string>
        {
            {"IT", "Totale Scontato"},
            {"EN", "Totale Scontato"}
        };

        Dictionary<string, string> _TotaleOrdine = new Dictionary<string, string>
        {
            {"IT", "Totale Ordine"},
            {"EN", "Total Order"}
        };

        Dictionary<string, string> _ProdottoPicker1 = new Dictionary<string, string>
        {
            {"IT", "Naviga il catalogo"},
            {"EN", "Browse the catalog"}
        };

        Dictionary<string, string> _ProdottoPicker2 = new Dictionary<string, string>
        {
            {"IT", "Scegli una serie"},
            {"EN", "Choose a series"}
        };
        Dictionary<string, string> _ProdottoPicker3 = new Dictionary<string, string>
        {
            {"IT", "Seleziona una famiglia"},
            {"EN", "Select a family"}
        };

        Dictionary<string, string> _LogonRichiesto = new Dictionary<string, string>
        {
            {"IT", "Effettua il login per creare e consultare l'elenco clienti e ordini"},
            {"EN", "Login to create and view the customer and order list"}
        };

        Dictionary<string, string> _LogonTesto = new Dictionary<string, string>
        {
            {"IT", "Effettua il login per accedere all'elenco clienti e ordini, vedere prezzi e rendering 3D"},
            {"EN", "Login to access the customer list and orders, see prices and 3D rendering"}
        };



        Dictionary<string, string> _TitoloContattiOmal = new Dictionary<string, string>
        {
            {"IT", "Contatti OMAL"},
            {"EN", "OMAL Contacts"}
        };

        Dictionary<string, string> _UltimoAggiornamento = new Dictionary<string, string>
        {
            {"IT", "Ultimo aggiornamento"},
            {"EN", "Last Update"}
        };

        Dictionary<string, string> _TitoloImpostazioni = new Dictionary<string, string>
        {
            {"IT", "Impostazioni"},
            {"EN", "Settings"}
        };
        Dictionary<string, string> _TitoloClienti = new Dictionary<string, string>
        {
            {"IT", "Clienti"},
            {"EN", "Customers"}
        };

        Dictionary<string, string> _Note = new Dictionary<string, string>
        {
            {"IT", "Note"},
            {"EN", "Notes"}
        };

        Dictionary<string, string> _TitoloCerca = new Dictionary<string, string>
        {
            {"IT", "Cerca"},
            {"EN", "Search"}
        };

        Dictionary<string, string> _Cliente = new Dictionary<string, string>
        {
            {"IT", "Cliente"},
            {"EN", "Customer"}
        };




        Dictionary<string, string> _Aggiungi = new Dictionary<string, string>
        {
            {"IT", "Aggiungi"},
            {"EN", "Add"}
        };

        Dictionary<string, string> _Aggiorna = new Dictionary<string, string>
        {
            {"IT", "Aggiorna"},
            {"EN", "Update"}
        };

        Dictionary<string, string> _ClienteRicercato = new Dictionary<string, string>
        {
            {"IT", "Inserisci il cliente ricercato"},
            {"EN", "Enter the requested client"}
        };

        Dictionary<string, string> _ProdottoRicercato = new Dictionary<string, string>
        {
            {"IT", "Cerca per nome o codice prodotto"},
            {"EN", "Search product name or code"}
        };

    }
}
