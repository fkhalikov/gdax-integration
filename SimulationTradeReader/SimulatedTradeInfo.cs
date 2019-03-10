using GDAX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationTradeReader.Models
{
    public class SimulatedTradeInfo : ITradeInfo
    {
        public DateTime Time => Convert.ToDateTime(_time);

        public int TradeId => Convert.ToInt32(_trade_id);

        public decimal Price => Convert.ToDecimal(_price);

        public decimal Size => Convert.ToDecimal(_size);

        public TradeSide Side => _side.ToString().Equals("buy", StringComparison.InvariantCultureIgnoreCase) ? TradeSide.Buy : TradeSide.Sell;

        public decimal OriginalPrice => Price;

        object _time;
        object _side;
        object _price;
        object _size;
        object _trade_id;
        public SimulatedTradeInfo(object time, object side, object price, object size, object trade_id)
        {
            _time = time;
            _side = side;
            _price = price;
            _size = size;
            _trade_id = trade_id;
        }
        
        
    }
}
