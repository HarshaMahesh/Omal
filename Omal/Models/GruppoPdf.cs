using System;
using System.Collections.Generic;

namespace Omal.Models
{
    public class GruppoPdf: List<PDF>
    {
        public string Categoria { get; set; }

        public GruppoPdf(string categoria)
        {
            Categoria = categoria;
        }
    }
}
