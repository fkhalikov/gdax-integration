using System;

namespace GDAX.Models
{
  public interface ITradeInfo
  {
    DateTime Time { get; }

    int TradeId { get; }

    decimal Price { get; }

    decimal OriginalPrice { get; }

    decimal Size { get; }

    TradeSide Side { get; }
  }
}
