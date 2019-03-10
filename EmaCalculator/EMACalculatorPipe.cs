using GDAX.Models;
using GDAX.Models.BatchPipe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmaCalculator
{
  public class EMACalculatorPipe : IBatchPipe
  {
    private readonly int _periodCount;
    private readonly decimal _multiplier;

    ITradeInfo _lastEMAValue = null;

    /// <summary>
    /// Constructor
    /// Also precalculates multiplier value 2/(period+1)
    /// </summary>
    /// <param name="periodCount">Number of records to go back for calculation of the first record</param>
    /// 
    public EMACalculatorPipe(int periodCount)
    {
      this._periodCount = periodCount;

      _multiplier = 2m / (_periodCount + 1m);
    }

    public List<ITradeInfo> Process(List<ITradeInfo> trades)
    {
      int recToSkip = 0;

      List<ITradeInfo> newEmaTrades = new List<ITradeInfo>();

      ///Need to calculate first EMA value
      if (_lastEMAValue == null)
      {
        _lastEMAValue = CalcFirstValue(trades, out recToSkip);

        newEmaTrades.Add(_lastEMAValue);
      }
      
      ///Calculate consecutive EMA values
      foreach (var trade in trades.Skip(recToSkip))
      {
        var prevEma = _lastEMAValue;

        decimal ema = (trade.Price - prevEma.Price) * _multiplier + prevEma.Price;

        _lastEMAValue = new GenericTradeInfo(trade.Price)
        {
          Price = ema
        ,
          Side = trade.Side
        ,
          Size = trade.Size
        ,
          Time = trade.Time
        ,
          TradeId = trade.TradeId
        };

        newEmaTrades.Add(_lastEMAValue);
      }
      
      return newEmaTrades;
    }

    private GenericTradeInfo CalcFirstValue(List<ITradeInfo> trades, out int recordsToSkip)
    {
      if (trades.Count < this._periodCount)
      {
        throw new Exception($"Initial batch of trades needs to have at least {_periodCount} trades");
      }

      var firstNTrades = trades.Take(_periodCount).ToList();

      var recToSkip = _periodCount;

      var lastTrade = firstNTrades.Last();

      recordsToSkip = _periodCount;

      return new GenericTradeInfo(lastTrade.Price)
      {
        Price = firstNTrades.Average(x => x.Price)
          ,
        Side = lastTrade.Side
          ,
        Size = lastTrade.Size
          ,
        Time = lastTrade.Time
          ,
        TradeId = lastTrade.TradeId
      };
    }
  }
}
