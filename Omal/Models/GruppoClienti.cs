using System;
using System.Collections.Generic;

namespace Omal.Models
{
    public class GruppoClienti: List<Cliente>
    {
        public string Titolo { get; set; }
        public string TitoloCorto { get; set; }

        public GruppoClienti(string titolo, string titoloCorto)
        {
            Titolo = titolo;
            TitoloCorto = titoloCorto;
        }
    }
}
