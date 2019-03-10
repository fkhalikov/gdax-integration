using GDAX.Models;
using GDAX.Models.Logging;
using GDAX.Models.Trader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpDownDecisionMakerPipe
{
    public class UpDownBuyDecisionMaker : IPipe
    {
        int step = 3;

        Queue<Direction> priceMovement = new Queue<Direction>();

        private ITrader _trader = null;
        private ILogger _logger = null;

        public UpDownBuyDecisionMaker(
            ITrader trader
            ,ILogger logger
            , Dictionary<string,int> intParams)
        {
            step = intParams["step"];

            for (int i = 0; i < step; i++)
            {
                priceMovement.Enqueue(Direction.None);
            }
            
            _trader = trader;
            _logger = logger;
        }

        decimal lastPrice = 0;

        
        TradeSide lastRecommendation = TradeSide.None;

        public ITradeInfo Process(ITradeInfo tInfo)
        {
            if (tInfo.Side == TradeSide.Sell) { return tInfo; }

            decimal price = tInfo.Price;

            if (lastPrice == price || lastPrice == 0)
            {
                
            }
            else
            {
                var direction = price > lastPrice ? Direction.UP : price < lastPrice ? Direction.DOWN : Direction.None;

                priceMovement.Dequeue();
                priceMovement.Enqueue(direction);

                _logger.Log($"Price - {price} -{direction}");
            }

            lastPrice = price;

            List<IOrder> openOrders = _trader.ListOpenOrders().ToList();
          

            if (priceMovement.Count(x => x == Direction.UP) == priceMovement.Count)
            {
                if (lastRecommendation != TradeSide.Buy)
                {
                    if (openOrders.All(x => x.Settled))
                    {
                        _trader.Buy(0.01m,price,TradeType.Limit);
                        lastRecommendation = TradeSide.Buy;

                        _logger.Log($"Buying {0.01} for {price}");
                    }
                    else
                    {
                        _logger.Log("Can not BUY there are still unsettled orders");
                    }
                }
            }
            else if ((priceMovement.Count(x => x == Direction.DOWN) == priceMovement.Count))
            {
                if (lastRecommendation != TradeSide.Sell
                    && lastRecommendation == TradeSide.Buy)
                {
                    if (openOrders.All(x => x.Settled))
                    {
                        _trader.Sell(0.01m,price, TradeType.Limit);

                        lastRecommendation = TradeSide.Sell;

                        _logger.Log($"Selling {0.01} for price {price}");
                    }
                    else
                    {
                        _logger.Log("Can not SELL there are still unsettled orders");
                    }
                }
            }
            else
            {
                //Do nothing
            }

            return tInfo;
        }

      
    }
}
