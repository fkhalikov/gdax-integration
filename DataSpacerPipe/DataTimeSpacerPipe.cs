using GDAX.Models;
using GDAX.Models.BatchPipe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataSpacerPipe
{
  public class DataTimeSpacerPipe : IBatchPipe
    {
        private readonly TimeSpan _interval;
        private DateTime? _startTime;

        public DataTimeSpacerPipe(TimeSpan interval, DateTime? startTime)
        {
            this._interval = interval;
            this._startTime = startTime;
        }

        public List<ITradeInfo> Process(List<ITradeInfo> trades)
        {
            List<ITradeInfo> resultTrades = new List<ITradeInfo>();

            if (_startTime.HasValue==false)
            {
                var firstTrade = trades.First();

                _startTime = firstTrade.Time;

                resultTrades.Add(firstTrade);

                Debug.WriteLine($"{firstTrade.Time}");
            }

            foreach(var trade in trades)
            {
                if (trade.Time.Subtract(_startTime.Value).Ticks >= _interval.Ticks)
                {
                    resultTrades.Add(trade);

                    Debug.WriteLine($"{trade.Time} {trade.Time.Subtract(_startTime.Value)}");

                    _startTime = trade.Time;
                }
            }

            return resultTrades;
        }
    }
}
