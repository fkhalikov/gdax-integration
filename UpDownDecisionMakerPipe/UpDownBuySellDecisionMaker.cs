using GDAX.Models;
using GDAX.Models.Logging;
using GDAX.Models.Trader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingPlatform.Common;

namespace UpDownDecisionMakerPipe
{
    public class UpDownBuySellDecisionMaker : IPipe
    {
        int buystep = 3;
        int sellstep = 3;

        PriceMovementTracker buyMovementTracker;
        PriceMovementTracker sellMovementTracker;

        private ITrader _trader = null;
        private ILogger _logger = null;

        public UpDownBuySellDecisionMaker(
              ITrader trader
            , ILogger logger
            , Dictionary<string, int> intParams)
        {
            buystep = intParams["buystep"];
            sellstep = intParams["sellstep"];
            int trackerPrecision = intParams["trackerprecision"];

            buyMovementTracker = new PriceMovementTracker(buystep, trackerPrecision);
            sellMovementTracker = new PriceMovementTracker(sellstep, trackerPrecision);

            _trader = trader;
            _logger = logger;
        }


        TradeSide lastRecommendation = TradeSide.None;

        public ITradeInfo Process(ITradeInfo tInfo)
        {
            Direction direction;

            if (tInfo.Side == TradeSide.Buy)
            {
                direction = buyMovementTracker.Track(tInfo.Price);
            }
            else
            {
                direction = sellMovementTracker.Track(tInfo.Price);
            }

            if (direction != Direction.None) _logger.Log($"Price - {Math.Round(tInfo.Price,2)} -{direction} - {tInfo.Side}");

            decimal price = tInfo.OriginalPrice;
            
            List<IOrder> openOrders = _trader.ListOpenOrders().ToList();
            
            if (buyMovementTracker.AllUP() || sellMovementTracker.AllUP())
            {
                if (lastRecommendation != TradeSide.Buy)
                {
                    if (openOrders.All(x => x.Settled))
                    {
                        _trader.Buy(0.1m, price, TradeType.Limit);
                        lastRecommendation = TradeSide.Buy;

                        _logger.Log($"Buying {0.01} for {price}");
                    }
                    else
                    {
                        _logger.Log("Can not BUY there are still unsettled orders");
                    }
                }
            }
            else if (sellMovementTracker.AllDOWN() || buyMovementTracker.AllDOWN())
            {
                if (lastRecommendation != TradeSide.Sell
                    && lastRecommendation == TradeSide.Buy)
                {
                    if (openOrders.All(x => x.Settled))
                    {
                        _trader.Sell(0.1m, price, TradeType.Limit);

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
