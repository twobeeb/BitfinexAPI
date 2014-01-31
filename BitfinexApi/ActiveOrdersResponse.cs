using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json;

namespace BitfinexApi
{
    public class ActiveOrdersResponse
    {
        public List<OrderStatusResponse> orders;

        public static ActiveOrdersResponse FromJSON(string response)
        {
            List<OrderStatusResponse> orders = JsonConvert.DeserializeObject<List<OrderStatusResponse>>(response);
            return new ActiveOrdersResponse(orders);
        }
        private ActiveOrdersResponse(List<OrderStatusResponse> orders)
        {
            this.orders = orders;
        }
    }
}
