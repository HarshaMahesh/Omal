using System;
namespace Omal.Models
{
    public class Ordine:Base
    {
        public int IdOrdine {   get;set;    }
        public int IdCliente { get; set; }
        public string CodiceOrdine { get; set; }
        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string Note { get; set; }
        double totale;
        public double Totale 
        { 
            get { return totale; }
            set 
            {
                totale = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DataEliminazione { get; set; }
    }
}
