using System;
using System.Collections.Generic;

using System.Text;

namespace BitfinexApi
{
    public class BalancesRequest:GenericRequest
    {
        public BalancesRequest(string nonce)
        {
            this.nonce = nonce;
            this.request = "/v1/balances";
        }
    }
}
