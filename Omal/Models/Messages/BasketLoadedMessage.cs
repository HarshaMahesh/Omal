using System;
namespace Omal.Models.Messages
{
    public class BasketLoadedMessage
    {
        public BasketLoadedMessage()
        {
        }

        public Models.Ordine Ordine { get; set; }
    }
}
