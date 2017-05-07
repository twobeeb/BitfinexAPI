BitfinexAPI
===========

C# Implementation of Bitfinex trade API

Author : Julien Chanaud julien.chanaud[at]supinfo[dot]com

Usage
=====

string key = "your_key";
string secret = "your_secret";

BitfinexApiV1 api = new BitfinexApiV1(key, secret);

BalancesResponse bal = api.GetBalances();

NewOrderResponse buy = api.ExecuteBuyOrderBTC(0.01M, 100.00M, OrderExchange.All, OrderType.MarginLimit);

NewOrderResponse sell = api.ExecuteSellOrderBTC(0.01M, 1000M, OrderExchange.All, OrderType.MarginLimit);

int order_id = int.Parse(buy.order_id);

OrderStatusResponse buyStatus = api.GetOrderStatus(order_id);

CancelOrderResponse cancel = api.CancelOrder(order_id);

ActiveOrdersResponse orders = api.GetActiveOrders();

ActivePositionsResponse positions = api.GetActivePositions();

api.CancelAllOrders();
