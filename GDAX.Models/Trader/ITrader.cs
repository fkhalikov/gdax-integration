using System;
using System.Collections.Generic;
using System.Text;

namespace GDAX.Models.Trader
{
    public interface ITrader
    {

        bool Buy(decimal size, decimal price, TradeType type);

        bool Sell(decimal size, decimal price, TradeType type);

        IEnumerable<IOrder> ListOpenOrders();

    }
}
