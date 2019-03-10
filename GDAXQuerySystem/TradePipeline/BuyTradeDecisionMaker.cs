using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boukenken.Gdax;
using GDAX.API.Configuration;
using GDAX.Models;
using GDAXQuerySystem.Models;
using TradingPlatform.Common;

namespace GDAXQuerySystem.TradePipeline
{
    public class BuyTradeDecisionMaker : IPipe
    {
        Queue<Direction> priceMovement = new Queue<Direction>();
        GDAXConfiguration config = new GDAXConfiguration();

        public BuyTradeDecisionMaker()
        {
            priceMovement.Enqueue(Direction.None);
            priceMovement.Enqueue(Direction.None);
            priceMovement.Enqueue(Direction.None);
        }

        decimal lastPrice = 0;

        //TradeActions lastAction = TradeActions.None;
        string recommendation = "";

        public ITradeInfo Process(ITradeInfo tInfo)
        {
            ///It is buy from market side, meaning it is a sell
            if (tInfo.Side == TradeSide.Sell) { return tInfo; }

            decimal price = tInfo.Price;

            if (lastPrice == price || lastPrice == 0)
            {
                Console.WriteLine($"Price {price}");

                
            } else
            {
                var direction = price > lastPrice ? Direction.UP : price < lastPrice ? Direction.DOWN : Direction.None;

                priceMovement.Dequeue();
                priceMovement.Enqueue(direction);

                Console.WriteLine($"Price - {price} -{direction}");
            }

            lastPrice = price;

            
            OrderClient ordClient = new OrderClient(config.Url, new RequestAuthenticator(config.Key, config.Passphrase, config.Secret));

             var result = ordClient.GetOpenOrdersAsync();
            result.Wait();
            var rData = result.Result;
            
            if (priceMovement.Count(x => x == Direction.UP) == priceMovement.Count)
            {
                if (recommendation.Equals("buy",StringComparison.InvariantCultureIgnoreCase)==false)
                {
                    //Console.WriteLine("buy");
                    //Console.WriteLine($"Open order count {rData.Value.Count()}");
                    
                    if (rData.Value.All(x=>x.settled && x.status.Equals("done", StringComparison.InvariantCulture)))
                    {
                        Buy(price);
                        recommendation = "buy";
                        Console.WriteLine("Put bought order");
                    }else
                    {
                        Console.WriteLine("Can not BUY there are still unsettled orders");
                    }
                }
            }
            else if ((priceMovement.Count(x => x == Direction.DOWN) == priceMovement.Count))
            {
                if (recommendation.Equals("sell",StringComparison.InvariantCultureIgnoreCase)==false
                    && recommendation.Equals("buy",StringComparison.InvariantCultureIgnoreCase))
                {
                    if (rData.Value.All(x => x.settled && x.status.Equals("done", StringComparison.InvariantCulture)))
                    {
                        Sell(price);

                        recommendation = "sell";
                        
                        Console.WriteLine("Put sell order");
                    }
                    else
                    {
                        Console.WriteLine("Can not SELL there are still unsettled orders");
                    }
                } 
            }
            else
            {
                //Do nothing
            }

            return tInfo;
        }

        public void Buy(decimal price)
        {
            OrderClient client = new OrderClient(config.Url, new RequestAuthenticator(config.Key, config.Passphrase, config.Secret));

            var ordTsk = client.PlaceOrderAsync("buy", "ETH-EUR", 0.01m, price, "limit");
            ordTsk.Wait();

            Order ord = ordTsk.Result.Value;
        }

        public void Sell(decimal price)
        {
            OrderClient client = new OrderClient(config.Url, new RequestAuthenticator(config.Key, config.Passphrase, config.Secret));

            var ordTsk = client.PlaceOrderAsync("sell", "ETH-EUR", 0.01m, price, "limit");
            ordTsk.Wait();

            Order ord = ordTsk.Result.Value;
        }
    }
}
