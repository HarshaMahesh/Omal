using System;
using System.ComponentModel;
using SQLite;
using Xamarin.Forms;

namespace Omal.Models
{
    public class Carrello : Base
    {
        public int IdOrdine { get; set; }
        public int IdCarrello { get; set; }
        public string Tipologia { get; set; }
        public int IdArticolo { get; set; }
        public string CodiceArticolo { get; set; }
        public string DescrizioneCarrello_It { get; set; }
        public string DescrizioneCarrello_En { get; set; }
        public double PrezzoUnitario { get; set; }
        double sconto;
        public double Sconto
        {
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

        int qta;
        public int Qta
        {
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

        [Ignore]
        public Color ColorePrezzoScontato
        {
            get
            {
                if (Sconto == 0) return Color.Transparent;
                return Color.FromHex("#004899");
            }

        }
        [Ignore]
        public Color ColorePrezzoTotale
        {
            get
            {
                if (Sconto == 0) return Color.FromHex("#004899");
                return Color.FromHex("#EAEAEA");
            }

        }


        [Ignore]
        public double PrezzoUnitarioScontato => PrezzoUnitario - ScontoUnitario;
        [Ignore]
        public double ScontoUnitario => (PrezzoUnitario * Sconto)/100;

        [Ignore]
        public double ScontoTotale
        {
            get
            {
                return ScontoUnitario * qta;
            }
        }

        [Ignore]
        public double PrezzoTotale
        {
            get
            {
                return (PrezzoUnitario * Qta);
            }
        }

        [Ignore]
        public double PrezzoTotaleScontato
        {
            get
            {
                return (PrezzoUnitario * Qta) - ScontoTotale;
            }
        }

        [Ignore]
        public string QtaStr
        {
            get
            {
                return App.Traduzioni.StrQta;
            }
        }

        [Ignore]
        public string ScontoStr
        {
            get
            {
                return App.Traduzioni.StrSconto;
            }
        }

        [Ignore]
        public string PrezzoTotaleStr
        {
            get
            {
                if (Sconto != 0)
                {
                    return string.Format("€ {1} * {0}  = € {2} - € {3} = € {4}", Qta, PrezzoUnitario.ToString("n2"), PrezzoTotale.ToString("n2"), ScontoTotale.ToString("n2"), PrezzoTotaleScontato.ToString("n2"));
                }
                else
                    return string.Format("€ {1} * {0}  = € {2}", Qta, PrezzoUnitario.ToString("n2"), PrezzoTotale.ToString("n2"));
            }
        }

        public int IdProdotto { get; set; }
       


        public Carrello()
        {
            PropertyChanged += LocalPropertyChanged;
        }

        private void LocalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if
                (string.Equals("Qta", e.PropertyName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals("Sconto", e.PropertyName, StringComparison.InvariantCultureIgnoreCase))
            {
                OnPropertyChanged("ColorePrezzoTotale");
                OnPropertyChanged("ColorePrezzoScontato");
                OnPropertyChanged("PrezzoUnitarioScontato");
                OnPropertyChanged("ScontoUnitario");
                OnPropertyChanged("PrezzoTotaleScontato");
                OnPropertyChanged("PrezzoTotale");
                OnPropertyChanged("PrezzoTotaleStr");
            }
        }
    }
}
