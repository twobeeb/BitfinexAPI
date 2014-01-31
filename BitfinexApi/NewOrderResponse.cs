using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BitfinexApi
{
    public class NewOrderResponse : OrderStatusResponse
    {
        public string order_id;
        
        public static NewOrderResponse FromJSON(string response)
        {
            NewOrderResponse resp = JsonConvert.DeserializeObject<NewOrderResponse>(response);
            return resp;
        }
    }
}
