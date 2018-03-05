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

        string GetCurvalueLang(Dictionary<string,string> elemento)
        {
            if (elemento == null) return string.Empty;
            if (elemento.ContainsKey(App.CurLang)) return elemento[App.CurLang];
            if (elemento.ContainsKey(defaultLang)) return elemento[defaultLang];
            return "OOOPS";
        }


        public string StrTrovaArticolo { get { return GetCurvalueLang(_StrTrovaArticolo); } }

        public string LogonRichiesto { get  { return GetCurvalueLang(_LogonRichiesto); } }
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



        public string Cliente { get { return GetCurvalueLang(_Cliente); } }
        public string Note { get { return GetCurvalueLang(_Note); } }
        public string StrTotaleOrdine { get { return GetCurvalueLang(_TotaleOrdine); } }
        public string StrSconto { get { return GetCurvalueLang(_StrSconto); } }
        public string StrQta { get { return GetCurvalueLang(_StrQta); } }
        public string LogonTesto { get { return GetCurvalueLang(_LogonTesto); } }

        public string StrRagioneSociale { get { return GetCurvalueLang(_StrRagioneSociale); } }
        public string StrPIva { get { return GetCurvalueLang(_StrPIva); } }
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
            {"EN", "Elimina"}
        };

        Dictionary<string, string> _TitoloLogin = new Dictionary<string, string>
        {
            {"IT", "Accessi"},
            {"EN", "Login"}
        };


        Dictionary<string, string> _StrInfoProdottoPicker1Attuatore = new Dictionary<string, string>
        {
            {"IT", "Misura"},
            {"EN", "Misura"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker2Attuatore = new Dictionary<string, string>
        {
            {"IT", "ISO"},
            {"EN", "ISO"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker3Attuatore = new Dictionary<string, string>
        {
            {"IT", "Coppia"},
            {"EN", "Coppia"}
        };

        Dictionary<string, string> _StrInfoProdottoPicker1Valvola = new Dictionary<string, string>
        {
            {"IT", "Azionamenti"},
            {"EN", "Azionamenti"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker2Valvola = new Dictionary<string, string>
        {
            {"IT", "Misure DN"},
            {"EN", "Misure DN"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker3Valvola = new Dictionary<string, string>
        {
            {"IT", "Pressione nominale"},
            {"EN", "Pressione nominale"}
        };
        Dictionary<string, string> _StrInfoProdottoPicker4Valvola = new Dictionary<string, string>
        {
            {"IT", "Varianti"},
            {"EN", "Varianti"}
        };

        Dictionary<string, string> _StrTrovaArticolo = new Dictionary<string, string>
        {
            {"IT", "Trova Articolo"},
            {"EN", "Trova Articolo"}
        };
        Dictionary<string, string> _TitoloDettaglioCliente = new Dictionary<string, string>
        {
            {"IT", "Dettaglio Cliente"},
            {"EN", "Dettaglio Cliente"}
        };
        Dictionary<string, string> _StrRagioneSociale = new Dictionary<string, string>
        {
            {"IT", "Ragione Sociale"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrPIva = new Dictionary<string, string>
        {
            {"IT", "P.Iva"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrCFiscale = new Dictionary<string, string>
        {
            {"IT", "C.Fiscale"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrPosizione = new Dictionary<string, string>
        {
            {"IT", "Posizione"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrIndirizzo = new Dictionary<string, string>
        {
            {"IT", "Indirizzo"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrCitta = new Dictionary<string, string>
        {
            {"IT", "Città"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrCap = new Dictionary<string, string>
        {
            {"IT", "Cap"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrProvincia = new Dictionary<string, string>
        {
            {"IT", "Provincia"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrContatti = new Dictionary<string, string>
        {
            {"IT", "Contatti"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrEmail = new Dictionary<string, string>
        {
            {"IT", "Email"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrTelefono = new Dictionary<string, string>
        {
            {"IT", "Telefono"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrFax = new Dictionary<string, string>
        {
            {"IT", "Fax"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrNazione = new Dictionary<string, string>
        {
            {"IT", "Nazione"},
            {"EN", "Sconto (%)"}
        };
        Dictionary<string, string> _StrSconto = new Dictionary<string, string>
        {
            {"IT", "Sconto"},
            {"EN", "Sconto (%)"}
        };



        Dictionary<string, string> _StrQta = new Dictionary<string, string>
        {
            {"IT", "Qta"},
            {"EN", "Qty"}
        };

        Dictionary<string, string> _TotaleOrdine = new Dictionary<string, string>
        {
            {"IT", "Totale Ordine"},
            {"EN", "Totale Ordine"}
        };

        Dictionary<string, string> _ProdottoPicker1 = new Dictionary<string, string>
        {
            {"IT", "Naviga il catalogo"},
            {"EN", "Naviga il catalogo"}
        };

        Dictionary<string, string> _ProdottoPicker2 = new Dictionary<string, string>
        {
            {"IT", "Scegli una serie"},
            {"EN", "Scegli una serie"}
        };
        Dictionary<string, string> _ProdottoPicker3 = new Dictionary<string, string>
        {
            {"IT", "Seleziona una famiglia"},
            {"EN", "Seleziona una famiglia"}
        };

        Dictionary<string, string> _LogonRichiesto = new Dictionary<string, string>
        {
            {"IT", "Effettua il login per creare e consultare l'elenco clienti e ordini"},
            {"EN", "Effettua il login per creare e consultare l'elenco clienti e ordini"}
        };

        Dictionary<string, string> _LogonTesto = new Dictionary<string, string>
        {
            {"IT", "Effettua il login per accedere all'elenco clienti e ordini, vedere prezzi e rendering 3D"},
            {"EN", "Effettua il login per accedere all'elenco clienti e ordini, vedere prezzi e rendering 3D"}
        };



        Dictionary<string, string> _TitoloContattiOmal = new Dictionary<string, string>
        {
            {"IT", "Contatti Omal"},
            {"EN", "Contatti Omal"}
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
            {"EN", "Clients"}
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
            {"EN", "Find..."}
        };

        Dictionary<string, string> _ProdottoRicercato = new Dictionary<string, string>
        {
            {"IT", "Inserisci il prodotto ricercato"},
            {"EN", "Find..."}
        };

    }
}
