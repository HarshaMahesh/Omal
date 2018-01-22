using System;
namespace Omal.Models
{
    public class Attuatore
    {
        public Attuatore()
        {
        }

        public int IdCodiceAttuatore { get; set; }
        public int IdProdotto { get; set; }
        public string Codice_Articolo { get; set; }
        public int Giacenza { get; set; }
        public double Prezzo { get; set; }
        public string Valore_misura { get; set; }
        public string Valore_iso { get; set; }
        public string Valore_coppia { get; set; }
        public int Ordine { get; set; }
        public string note_footer { get; set; }
        public string note_footer_en { get; set; }
        public string url3d { get; set; }
        public string urlDownload { get; set; }
        public string immagine_placeholder { get; set; }
    }
}
