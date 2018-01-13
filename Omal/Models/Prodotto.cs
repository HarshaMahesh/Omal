using System;
namespace Omal.Models
{
    public class Prodotto
    {
        public int IdProdotto {   get;set;    }
        public int IdCategoria { get; set; }
        public string Codice { get; set; }
        public string Nome { get; set; }
        public string NomeEn { get; set; }
        public string Descrizione { get; set; }
        public string DescrizioneEn { get; set; }
        public string Tipologia { get; set; }
        public int Ordine { get; set; }
        public string UrlSeo { get; set; }
        public string UrlSepEn { get; set; }
        public bool VisibileApp { get; set; }
        public string ImmaginePlaceHolder { get; set; }
    }
}
