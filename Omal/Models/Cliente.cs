using System;
namespace Omal.Models
{
    public class Cliente
    {
        public int IdCliente {   get;set;    }
        public string RagioneSociale { get; set; }
        public string PIva { get; set; }
        public string CFiscale { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Città { get; set; }
        public string Provincia { get; set; }
        public string Nazione { get; set; }
        public int IdUtenteReferente { get; set; }
    }
}
