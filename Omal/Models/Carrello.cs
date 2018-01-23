using System;
using System.ComponentModel;

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
        double sconto;
        public double Sconto { 
            get
            {
                return sconto;
            }
            set
            {
                sconto = value;
                OnPropertyChanged();
            }
        }
        public double PrezzoTotale 
        { 
            get
            {
                return (PrezzoUnitario * Qta) - (PrezzoUnitario * Qta) * (Sconto/100);
            }
        }
        public int IdProdotto { get; set; }
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


        public Carrello()
        {
            PropertyChanged += LocalPropertyChanged;
        }

        private void LocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if 
                ( string.Equals("Qta", e.PropertyName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals("Sconto", e.PropertyName, StringComparison.InvariantCultureIgnoreCase))
            {
                OnPropertyChanged("PrezzoTotale");
            }
        }
    }
}
