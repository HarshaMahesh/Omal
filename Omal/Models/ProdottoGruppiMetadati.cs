using System;
using SQLite;

namespace Omal.Models
{
    public class ProdottoGruppiMetadati
    {
        [PrimaryKey]
        public int idgruppometadato {   get;set;    }
        public int idprodotto { get; set; }
        public string gruppo_metadati_it { get; set; }
        public string gruppo_metadati_en { get; set; }
        public int? ordine { get; set; }
        public DateTime dataora_modifica { get; set; }
    }

    public class FkProdottoGruppiMetadati
    {
        public int? idgruppometadato { get; set; }
        public int? idprodotto { get; set; }
        public string gruppo_metadati_it { get; set; }
        public string gruppo_metadati_en { get; set; }
        public int? ordine { get; set; }
        public DateTime dataora_modifica { get; set; }
    }
}
