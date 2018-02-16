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

        Dictionary<string, string> _TitoloCerca = new Dictionary<string, string>
        {
            {"IT", "Cerca"},
            {"EN", "Search"}
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
