using System;
namespace Omal.Models
{
    public class ProdottoMetadati
    {
        public int IdGruppoMetadato {   get;set;    }
        public int IdMetadato { get; set; }
        public string ImmagineMetadatiIt { get; set; }
        public string ImmagineMetadatiEn { get; set; }
        public int Ordine { get; set; }
        public string TestoEstesoMetadatiIt { get; set; }
        public string TestoEstesoMetadatiEn { get; set; }
    }
}
