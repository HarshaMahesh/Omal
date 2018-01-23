using System;
namespace Omal.Models
{
    public class Valvola:Base
    {
        public Valvola()
        {
        }

        public int IdCodiceValvola { get; set; }
        public int IdProdotto { get; set; }
        public string valore_dn { get; set; }
        public string valore_inch { get; set; }
        public string valore_pnansi { get; set; }
        public string Codice_Articolo { get; set; }
        public string Codice_Attuatore { get; set; }
        public string Codice_Kit { get; set; }
        public string Codice_Valvola { get; set; }
        public string Valore_nmm { get; set; }
        public string Valore_hmm { get; set; }
        public string Valore_pesokg { get; set; }
        public double Prezzo { get; set; }
        public int Ordine { get; set; }
        public int Giacenza { get; set; }
        public string valore_azionamento { get; set; }
        public string valore_materiale { get; set; }
        public string note_footer { get; set; }
        public string note_footer_en { get; set; }
        public string url3d { get; set; }
        public string urlDownload { get; set; }
        public string immagine_placeholder { get; set; }



    }
}
