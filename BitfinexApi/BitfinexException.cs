using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace BitfinexApi
{
    public class BitfinexException:WebException
    {

        public BitfinexException(WebException ex, string bitfinexMessage):
            base(bitfinexMessage,ex)
        {
        }
    }
}
