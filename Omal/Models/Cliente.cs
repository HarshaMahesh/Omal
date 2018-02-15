using System;
using System.Collections.Generic;
using System.ComponentModel;
using SQLite;

namespace Omal.Models
{
    public class Cliente: Base
    {
        [PrimaryKey]
        public int IDCliente {   get;set;    }
        public int IDUtente { get;  set;}
        public string societapersona { get; set; }

        string ragioneSociale;
        public string RagioneSociale { 
            get { return ragioneSociale; }
            set {
                ragioneSociale = value;
                OnPropertyChanged();
            }
        }

        string cognomeNome;
        public string CognomeNome
        {
            get { return cognomeNome; }
            set
            {
                cognomeNome = value;
                OnPropertyChanged();
            }
        }
        public string Piva { get; set; }
        public string CodiceFiscale { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Indirizzo { get; set; }

        string cap;
        public string Cap
        {
            get { return cap; }
            set
            {
                cap = value;
                OnPropertyChanged();
            }
        }

        string città;
        public string Citta
        {
            get { return città; }
            set
            {
                città = value;
                OnPropertyChanged();
            }
        }

        string provincia;
        public string Provincia
        {
            get { return provincia; }
            set
            {
                provincia = value;
                OnPropertyChanged();
            }
        }

        string nazione;
        public string Nazione
        {
            get { return nazione; }
            set
            {
                nazione = value;
                OnPropertyChanged();
            }
        }

        public DateTime dataora_modifica { get; set; }
        public int annullato { get; set; }
        public int IdUtenteReferente { get; set; }
        public string CittaProvinciaNazione
        {
            get
            {
                List<String> elementi = new List<string>();
                if (!string.IsNullOrWhiteSpace(città)) elementi.Add(città);
                if (!string.IsNullOrWhiteSpace(Provincia)) elementi.Add(Provincia);
                if (!string.IsNullOrWhiteSpace(Nazione)) elementi.Add(Nazione.ToUpper());
                return String.Join(",", elementi);
            }
        }
    }
}


















