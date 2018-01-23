using System;
namespace Omal.Models
{
    public class Carrello: Base
    {
        public int IdOrdine {   get;set;    }
        public int IdCarrello { get; set; }
        public string Tipologia { get; set; }
        public int IdArticolo { get; set; }
        public string CodiceArticolo { get; set; }
        public string DescrizioneCarrello_It { get; set; }
        public string DescrizioneCarrello_En { get; set; }
        public double PrezzoUnitario { get; set; }
        int qta;
        public int Qta {
            get
            { return qta; }
            set
            {
                if (qta != value)
                {
                    qta = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
