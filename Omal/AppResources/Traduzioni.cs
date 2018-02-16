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
        public string TitoloImpostazioni { get { return GetCurvalueLang(_TitoloImpostazioni); } }

        Dictionary<string, string> _LogonRichiesto = new Dictionary<string, string>
        {
            {"IT", "Effettua il login per creare e consultare l'elenco clienti e ordini"},
            {"EN", "Effettua il login per creare e consultare l'elenco clienti e ordini"}
        };

        Dictionary<string, string> _TitoloImpostazioni = new Dictionary<string, string>
        {
            {"IT", "Impostazioni"},
            {"EN", "Settings"}
        };

        Dictionary<string, string> _Aggiungi = new Dictionary<string, string>
        {
            {"IT", "Aggiungi"},
            {"EN", "Add"}
        };

        Dictionary<string, string> _ClienteRicercato = new Dictionary<string, string>
        {
            {"IT", "Inserisci il cliente ricercato"},
            {"EN", "Find..."}
        };

    }
}
