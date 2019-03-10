using GDAX.Models;
using GDAX.Models.Trader;
using System.Threading;

namespace TradingSimulator.Trader
{
    public class Order : IOrder
    {
        static int instanceCounter = 0;

        public Order(TradeSide side, decimal size, decimal price, TradeType type)
        {
            Interlocked.Increment(ref instanceCounter);

            this.Side = side;
            this.Size = size;
            this.Price = price;
            this.Type = type;
        }

        public string Identifier { get { return instanceCounter.ToString(); } }
        public bool Settled { get { return true; } }

        public TradeSide Side { get; }

        public decimal Size { get; }

        public decimal Price { get; }

        public TradeType Type { get; }
    }
}
