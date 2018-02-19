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
        public string TitoloDettaglioCliente { get { return GetCurvalueLang(_TitoloDettaglioCliente); } }


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
