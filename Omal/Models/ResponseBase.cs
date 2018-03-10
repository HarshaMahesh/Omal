using System;
namespace Omal.Models
{
    public class ResponseBase
    {
        public ResponseBase()
        {
        }

        public int HasError { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorDescription_En { get; set; }

    }
}
