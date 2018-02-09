using System;
using SQLite;

namespace Omal.Models
{
    public class ProdottoMetadati
    {
        public int idgruppometadato {   get;set;    }
        [PrimaryKey]
        public int idmetadato { get; set; }
        public string immagine_metadati_it { get; set; }
        public string immagine_metadati_en { get; set; }
        public int? ordine { get; set; }
        public string testo_esteso_metadati_it { get; set; }
        public string testo_esteso_metadati_en { get; set; }
        public DateTime dataora_modifica { get; set; }
    }

    public class FkProdottoMetadati
    {
        public int? idgruppometadato { get; set; }
        public int? idmetadato { get; set; }
        public string immagine_metadati_it { get; set; }
        public string immagine_metadati_en { get; set; }
        public int? ordine { get; set; }
        public string testo_esteso_metadati_it { get; set; }
        public string testo_esteso_metadati_en { get; set; }
        public DateTime dataora_modifica { get; set; }
    }
}


