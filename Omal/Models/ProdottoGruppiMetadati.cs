using System;
namespace Omal.Models
{
    public class ProdottoGruppiMetadati
    {
        public int IdGruppoMetadato {   get;set;    }
        public int IdProdotto { get; set; }
        public string GruppoMetadatiIt { get; set; }
        public string GruppoMetadatiEn { get; set; }
        public int Ordine { get; set; }
    }
}
