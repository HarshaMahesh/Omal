using System;
namespace Omal.Models
{
    public class Categoria
    {
        public int IdCategoria {   get;set;    }
        public int IdPadre { get; set; }
        public string Codice { get; set; }
        public string Nome { get; set; }
        public string NomeEn { get; set; }
        public string Descrizione { get; set; }
        public string DescrizioneEn { get; set; }
        public int Ordine { get; set; }
        public string Tipologia { get; set; }
        public bool VisibileApp { get; set; }
    }
}
