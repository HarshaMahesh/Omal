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

        public string PrezzoTotaleStr
        {
            get
            {
                if (Sconto != 0)
                {
                    return string.Format("€ {1} * {0}  = € {2} - € {3} = € {4}", Qta, PrezzoUnitario.ToString("n2"), (PrezzoUnitario * Qta).ToString("n2"), ((PrezzoUnitario * Qta) * (Sconto / 100)).ToString("n2"), PrezzoTotale.ToString("n2"));
                } else
                    return string.Format("€ {1} * {0}  = € {2}", Qta, PrezzoUnitario.ToString("n2"), (PrezzoUnitario * Qta).ToString("n2"), PrezzoTotale.ToString("n2"));
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
                OnPropertyChanged("PrezzoTotaleStr");
            }
        }
    }
}
