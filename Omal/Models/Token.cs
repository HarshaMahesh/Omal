using System;
namespace Omal.Models
{
    public class Token
    {
        public Token()
        {
        }

        public int idtoken { get; set; }
        public string token { get; set; }
        public DateTime dataora_inserimento { get; set; }
        public DateTime dataora_scadenza { get; set; }
        public int IDUtente { get; set; }
        public DateTime dataora_server { get; set; }
        public string email_utente { get; set; }
        public string NomeUtente { get; set; }
    }
}
