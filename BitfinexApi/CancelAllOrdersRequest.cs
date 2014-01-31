using System;
using System.Collections.Generic;

using System.Text;

namespace BitfinexApi
{
    public class CancelAllOrdersRequest:GenericRequest
    {
        public CancelAllOrdersRequest(string nonce)
        {
            this.nonce = nonce;
            this.request = "/v1/order/cancel/all";
        }
    }
}
