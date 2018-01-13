using System;
namespace Omal.Models
{
    public class Ordine
    {
        public int IdOrdine {   get;set;    }
        public int IdCliente { get; set; }
        public string CodiceOrdine { get; set; }
        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string Note { get; set; }
        public Decimal Totale { get; set; }
        public DateTime? DataEliminazione { get; set; }
    }
}
