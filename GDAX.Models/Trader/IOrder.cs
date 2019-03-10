using System;
using System.Collections.Generic;
using System.Text;

namespace GDAX.Models.Trader
{
    public interface IOrder
    {

        string Identifier { get;}

        bool Settled { get; }

        TradeSide Side { get; }

        decimal Price { get; }

        decimal Size { get; }

        TradeType Type { get; }
    }
}
