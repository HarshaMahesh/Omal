using System;
namespace Omal.Models
{
    public class ResponseOrdini: ResponseBase
    {
        public ResponseOrdini()
        {
        }
        public int? IDOrdine { get; set; }
        public string ordine
        {
            get;
            set;
        }
    }
}
