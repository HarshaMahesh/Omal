﻿using System;
namespace Omal.Models
{
    public class Utente
    {
        public int IdUtente {   get;set;    }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
    }
}
