using System;
using SQLite;

namespace Omal.Models
{
    public class Valvola:Base
    {
        public Valvola()
        {
        }

        [PrimaryKey]
        public int idcodicevalvola { get; set; }
        public int idprodotto { get; set; }
        public string valore_dn { get; set; }
        public string valore_inch { get; set; }
        public string valore_pnansi { get; set; }
        public string codice_articolo { get; set; }
        public string codice_attuatore { get; set; }
        public string codice_kit { get; set; }
        public string codice_valvola { get; set; }
        public string valore_nmm { get; set; }
        public string valore_hmm { get; set; }
        public string valore_pesokg { get; set; }
        public double Prezzo { get; set; }
        public int ordine { get; set; }
        public int giacenza { get; set; }
        public string valore_azionamento { get; set; }
        public string valore_materiale { get; set; }
        public string note_footer { get; set; }
        public string note_footer_en { get; set; }
        public string url_3d { get; set; }
        public string url_download { get; set; }
        public string immagine_placeholder { get; set; }
        public int annullato { get; set; }
        public DateTime dataora_modifica { get; set; }
    }
}
