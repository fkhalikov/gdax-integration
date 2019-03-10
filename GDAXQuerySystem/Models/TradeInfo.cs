using GDAX.Models;
using Newtonsoft.Json;
using System;

namespace GDAXQuerySystem.Models
{

  public class TradeInfo : ITradeInfo
    {
        [JsonIgnore]
        private decimal _originalPrice=0;

        public string time { get; set; }
        public decimal size { get; set; }
        public decimal price { get; set; }
        public int trade_id { get; set; }
        

        /// <summary>
        /// this is market side
        /// </summary>
        public string side { get; set; }

        [JsonIgnore]
        public DateTime Time { get { return Convert.ToDateTime(time); } }
        [JsonIgnore]
        public int TradeId { get { return trade_id; } }
        [JsonIgnore]
        public decimal Price { get { return price; } }
        [JsonIgnore]
        public decimal Size { get { return size; } }
        [JsonIgnore]
        public TradeSide Side { get { return side.Equals("buy", StringComparison.InvariantCultureIgnoreCase) 
                    ? TradeSide.Sell : TradeSide.Buy; } }

        [JsonIgnore]
        public decimal OriginalPrice
        {
            get
            {
                return _originalPrice;
            }
        }
    }
}
