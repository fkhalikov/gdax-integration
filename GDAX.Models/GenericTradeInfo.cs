using System;
using System.Collections.Generic;
using System.Text;

namespace GDAX.Models
{
    public sealed class GenericTradeInfo : ITradeInfo
    {
        private DateTime _time;
        private int _tradeId;
        private decimal _price;
        private decimal _size;
        private TradeSide _side;
        private decimal _originalPrice;

        public GenericTradeInfo(Decimal originalPrice)
        {
            _originalPrice = originalPrice;
        }

        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }

        public int TradeId
        {
            get
            {
                return _tradeId;
            } set
            {
                _tradeId = value;
            }
        }

        public decimal Price
        {
            get
            {
                return _price;
            } set
            {
                _price = value;
            }
        }

        public decimal Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public TradeSide Side
        {
            get
            {
                return _side;
            }set
            {
                _side = value;
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
