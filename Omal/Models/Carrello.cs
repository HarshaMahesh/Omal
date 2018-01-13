using System;
namespace Omal.Models
{
    public class Carrello
    {
        public int IdOrdine {   get;set;    }
        public int IdCarrello { get; set; }
        public string Tipologia { get; set; }
        public int IdArticolo { get; set; }
        public string CodiceArticolo { get; set; }
        public string DescrizioneCarrello_It { get; set; }
        public string DescrizioneCarrello_En { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public int Qta { get; set; }
    }
}
