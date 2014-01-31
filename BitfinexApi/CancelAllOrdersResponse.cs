using System;
using System.Collections.Generic;

using System.Text;

namespace BitfinexApi
{
    public class CancelAllOrdersResponse
    {
        public string message;
        public CancelAllOrdersResponse(string message)
        {
            this.message = message;
        }
    }
}
