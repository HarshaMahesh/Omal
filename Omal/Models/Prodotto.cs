using System;
using System.Collections.Generic;
using SQLite;

namespace Omal.Models
{
    public class Prodotto
    {
        [PrimaryKey]
        public int idprodotto {   get;set;    }
        public int idcategoria { get; set; }
        public string codice { get; set; }
        public string nome { get; set; }
        public string nome_en { get; set; }
        public string descrizione { get; set; }
        public string descrizione_en { get; set; }
        public string tipologia { get; set; }
        public int ordine { get; set; }
        public int visibile_app { get; set; }
        public string immagine_placeholder { get; set; }
        public DateTime dataora_modifica { get; set; }
        public int annullato { get; set; }
        [Ignore]
        public List<Descrizioni> descrizioniAzionamenti { get; set; }
        [Ignore]
        public List<Descrizioni> descrizioniMateriali { get; set; }

        public string DescrizioniAzionamentiSerialized { get; set; }
        public string DescrizioniMaterialiSerialized { get; set; }

    }
}

