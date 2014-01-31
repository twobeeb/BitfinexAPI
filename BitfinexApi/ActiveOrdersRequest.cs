using System;
using System.Collections.Generic;

using System.Text;

namespace BitfinexApi
{
    public class ActiveOrdersRequest:GenericRequest
    {
        public ActiveOrdersRequest(string nonce)
        {
            this.nonce = nonce;
            this.request = "/v1/orders";
        }
    }
}
