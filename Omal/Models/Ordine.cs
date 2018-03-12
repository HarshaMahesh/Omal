using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace Omal.Models
{
    public class Ordine:Base
    {
        public Ordine()
        {
            Stato = Enums.EOrdineStato.bozza;
            carrelli = new List<Carrello>();
            CodiceOrdine = Guid.NewGuid().ToString("N");
            DataInizio = DateTime.Now;
        }

        public int IdOrdine {   get;set;    }
        public int IdCliente { get; set; }
        [PrimaryKey]
        public string CodiceOrdine { get; set; }
        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string Note { get; set; }
        double totale = 0;
        public double Totale 
        { 
            get { return totale; }
            set 
            {
                totale = value;
                OnPropertyChanged();
            }
        }
        double totaleConSconto = 0;
        public double TotaleConSconto
        {
            get { return totaleConSconto; }
            set
            {
                totaleConSconto = value;
                OnPropertyChanged();
            }
        }
        double sconto;
        public double Sconto
        {
            get { return sconto; }
            set
            {
                sconto = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DataEliminazione { get; set; }

        public Enums.EOrdineStato Stato { get; set; }

        [Ignore]
        public string ClienteRagSoc { get; set; } 
        List<Carrello> _carrelli = null;
        [Ignore]
        public List<Carrello> carrelli 
        { 
            get
            {
                if ((_carrelli == null) && (!string.IsNullOrWhiteSpace(jsonCarrelliSerialized))) _carrelli = JsonConvert.DeserializeObject<List<Carrello>>(jsonCarrelliSerialized);
                return _carrelli;
            }

            set
            {
                _carrelli = value;
                jsonCarrelliSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(_carrelli);
            }
        }
        public string jsonCarrelliSerialized { get; set; }

    }
}
