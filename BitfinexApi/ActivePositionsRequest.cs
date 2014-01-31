using System;
using System.Collections.Generic;
using System.Text;

namespace BitfinexApi
{
    public class ActivePositionsRequest:GenericRequest
    {
        public ActivePositionsRequest(string nonce)
        {
            this.nonce = nonce;
            this.request = "/v1/positions";
        }
    }
}
