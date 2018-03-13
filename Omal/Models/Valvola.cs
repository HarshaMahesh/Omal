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
        public string immagine_placeholder_dt { get; set; }
        public int annullato { get; set; }
        public DateTime dataora_modifica { get; set; }
        public string codice_KITMONTAGGIO { get; set; }
        public string valore_ATEXopzione1 { get; set; }
        public string valore_KITLEVAopzione2 { get; set; }
        public string valore_KITGUARNIZIONIopzione3 { get; set; }
        public string valore_LMMopzione4 { get; set; }
        public string valore_TENUTAopzione5 { get; set; }
        public string KIT_OTTURATORE_RICAMBIO { get; set; }
        public string KIT_TESTA_RICAMBIO { get; set; }
        public string P_INTERCETTATA { get; set; }
        public string P_COMANDO_BAR_MIN { get; set; }
        public string P_COMANDO_BAR_MAX { get; set; }
        public string Kv { get; set; }
        public string SH_TESTA_COMANDO { get; set; }
        public string passaggio_mm { get; set; }
        public string P_INTERCETTATA_DP_max_bar { get; set; }

        public string codice_RIDUTTORE { get; set; }
        public string KIT_VALVOLA_RIDUTTORE { get; set; }

        public string KIT_RICAMBIO { get; set; }
    }
}
