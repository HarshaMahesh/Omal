using System;
namespace Omal.Models.Messages
{
    public class ClienteInsertedOrUpdatedMessage
    {
        public ClienteInsertedOrUpdatedMessage()
        {
        }

        public Models.Cliente Cliente { get; set; }
    }
}
