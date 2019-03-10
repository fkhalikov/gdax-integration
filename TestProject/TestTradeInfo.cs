using GDAX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestTradeInfo : ITradeInfo
    {
        private DateTime _time;
        private int _tradeId;
        private decimal _price;
        private decimal _size;
        private TradeSide _side;
        private decimal _originalPrice;

        public DateTime Time
        {
            get
            {
                return _time;
            }set
            {
                _time = value;
            }
        }

        public int TradeId
        {
            get
            {
                return _tradeId;
            }
        }

        public decimal Price
        {
            get
            {
                return _price;
            }
        }

        public decimal Size
        {
            get
            {
                return _size;
            }
        }

        public TradeSide Side
        {
            get
            {
                return _side;
            }
        }

        public decimal OriginalPrice
        {
            get
            {
                return _originalPrice;
            }
        }
    }
}
