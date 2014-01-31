using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json;

namespace BitfinexApi
{
    public class CancelOrderResponse : OrderStatusResponse
    {
        public static CancelOrderResponse FromJSON(string response)
        {
            return JsonConvert.DeserializeObject<CancelOrderResponse>(response);
        }
    }
}
