using System;
namespace Omal.Models
{
    public class Categoria
    {
        public int idCategoria {   get;set;    }
        public int idPadre { get; set; }
        public string codice { get; set; }
        public string nome { get; set; }
        public string nome_en { get; set; }
        public string descrizione { get; set; }
        public string descrizione_en { get; set; }
        public int ordine { get; set; }
        public string tipologia { get; set; }
        public string annullato { get; set; }
        public int visibile_app { get; set; }
        public DateTime dataora_modifica { get; set; }
    }
}
