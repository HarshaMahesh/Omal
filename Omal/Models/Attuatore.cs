using System;
using SQLite;

namespace Omal.Models
{
    public class Attuatore:Base
    {
        

        public Attuatore()
        {
        }
        [PrimaryKey]
        public int idcodiceattuatore { get; set; }
        public int idprodotto { get; set; }
        public string codice_articolo { get; set; }
        public int giacenza { get; set; }
        public double Prezzo { get; set; }
        public string valore_misura { get; set; }
        public string valore_iso { get; set; }
        public string valore_coppia { get; set; }
        public int ordine { get; set; }
        public string note_footer { get; set; }
        public string note_footer_en { get; set; }
        public string url_3d { get; set; }
        public string url_download { get; set; }
        public string immagine_placeholder { get; set; }
        public string immagine_placeholder_dt { get; set; }
        public int annullato { get; set; }
        public DateTime dataora_modifica { get; set; }

        public string valore_coppiabar { get; set; }
        public string valore_voltaggio { get; set; }
        public string valore_pesokg { get; set; }
        public string valore_pesokgbar { get; set; }
        public string valore_aria { get; set; }
        public string codice_guarnizioni { get; set; }
        public string valore_ch { get; set; }
        public string valore_ngiri { get; set; }

    }
}
