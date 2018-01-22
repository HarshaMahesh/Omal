using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Omal.Models
{
    public class Cliente: Base
    {
        public int IdCliente {   get;set;    }

        string ragioneSociale;
        public string RagioneSociale { 
            get { return ragioneSociale; }
            set {
                ragioneSociale = value;
                OnPropertyChanged();
            }
        }
        public string PIva { get; set; }
        public string CFiscale { get; set; }
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
        public string Città
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

        public int IdUtenteReferente { get; set; }
        public string CittaProvinciaNazione
        {
            get
            {
                List<String> elementi = new List<string>();
                if (!string.IsNullOrWhiteSpace(Città)) elementi.Add(Città);
                if (!string.IsNullOrWhiteSpace(Provincia)) elementi.Add(Provincia);
                if (!string.IsNullOrWhiteSpace(Nazione)) elementi.Add(Nazione.ToUpper());
                return String.Join(",", elementi);
            }
        }
    }
}
