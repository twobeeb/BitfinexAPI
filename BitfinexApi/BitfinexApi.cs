using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace BitfinexApi
{
    public class BitfinexApiV1
    {
        private DateTime epoch = new DateTime(1970, 1, 1);

        private HMACSHA384 hashMaker; 
        private string Key;
        private int nonce = 0;
        private string Nonce
        {
            get
            {
                if (nonce == 0)
                {
                    nonce = (int)(DateTime.UtcNow - epoch).TotalSeconds;
                }
                return (nonce++).ToString();
            }
        }
        public BitfinexApiV1(string key, string secret)
        {
            hashMaker = new HMACSHA384(Encoding.UTF8.GetBytes(secret));
            this.Key = key;
        }

        private String GetHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(String.Format("{0:x2}", b));
            }
            return sb.ToString();
        }
        public BalancesResponse GetBalances()
        {
            BalancesRequest req = new BalancesRequest(Nonce);
            string response = SendRequest(req,"GET");
            BalancesResponse resp = BalancesResponse.FromJSON(response);

            return resp;
        }
        public CancelOrderResponse CancelOrder(int order_id)
        {
            CancelOrderRequest req = new CancelOrderRequest(Nonce, order_id);
            string response = SendRequest(req,"POST");
            CancelOrderResponse resp = CancelOrderResponse.FromJSON(response);
            return resp;
        }
        public CancelAllOrdersResponse CancelAllOrders()
        {
            CancelAllOrdersRequest req = new CancelAllOrdersRequest(Nonce);
            string response = SendRequest(req,"GET");
            return new CancelAllOrdersResponse(response);
        }
        public OrderStatusResponse GetOrderStatus(int order_id)
        {
            OrderStatusRequest req = new OrderStatusRequest(Nonce, order_id);
            string response = SendRequest(req, "POST");
            return OrderStatusResponse.FromJSON(response);
        }
        public ActiveOrdersResponse GetActiveOrders()
        {
            ActiveOrdersRequest req = new ActiveOrdersRequest(Nonce);
            string response = SendRequest(req, "POST");
            return ActiveOrdersResponse.FromJSON(response);
        }
        public ActivePositionsResponse GetActivePositions()
        {
            ActivePositionsRequest req = new ActivePositionsRequest(Nonce);
            string response = SendRequest(req, "POST");
            return ActivePositionsResponse.FromJSON(response);
        }

        public NewOrderResponse ExecuteBuyOrderBTC(decimal amount, decimal price, OrderExchange exchange, OrderType type)
        {
            return ExecuteOrder(OrderSymbol.BTCUSD, amount, price, exchange, OrderSide.Buy, type);
        }
        public NewOrderResponse ExecuteSellOrderBTC(decimal amount, decimal price, OrderExchange exchange, OrderType type)
        {
            return ExecuteOrder(OrderSymbol.BTCUSD, amount, price, exchange, OrderSide.Sell, type);
        }
        public NewOrderResponse ExecuteOrder(OrderSymbol symbol, decimal amount, decimal price, OrderExchange exchange, OrderSide side, OrderType type)
        {
            NewOrderRequest req = new NewOrderRequest(Nonce, symbol, amount, price, exchange, side, type);
            string response = SendRequest(req,"POST");
            NewOrderResponse resp = NewOrderResponse.FromJSON(response);
            return resp;
        }

        private string SendRequest(GenericRequest request,string httpMethod)
        {
            string json = JsonConvert.SerializeObject(request);
            string json64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            byte[] data = Encoding.UTF8.GetBytes(json64);
            byte[] hash = hashMaker.ComputeHash(data);
            string signature = GetHexString(hash);

            HttpWebRequest wr = WebRequest.Create("https://api.bitfinex.com"+request.request) as HttpWebRequest;
            wr.Headers.Add("X-BFX-APIKEY", Key);
            wr.Headers.Add("X-BFX-PAYLOAD", json64);
            wr.Headers.Add("X-BFX-SIGNATURE", signature);
            wr.Method = httpMethod;
            
            string response = null;
            try
            {
                HttpWebResponse resp = wr.GetResponse() as HttpWebResponse;
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                response = sr.ReadToEnd();
                sr.Close();
            }
            catch (WebException ex)
            {
                StreamReader sr = new StreamReader(ex.Response.GetResponseStream());
                response = sr.ReadToEnd();
                sr.Close();
                throw new BitfinexException(ex, response);
            }
            return response;
        }
    }
}
