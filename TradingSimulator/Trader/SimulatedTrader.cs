using GDAX.Models;
using GDAX.Models.Account;
using GDAX.Models.Logging;
using GDAX.Models.Trader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.Trader
{
    public class SimulatedTrader : ITrader
    {
        List<IOrder> _orders = new List<IOrder>();
        IAccount _account = null;
        ILogger _logger = null;

        private decimal _totalTakersFee = 0;
        private int _transactioncount = 0;

        public decimal TotalTakersFee
        {
            get
            {
                return _totalTakersFee;
            }
        }

        public int TransactionCount
        {
            get
            {
                return _transactioncount;
            }
        }

        public decimal LastBuyPrice
        {
            get
            {
                return _lastBuyPrice;
            }
        }
        

        public int WinSales { get; set; }

        public int LossSales { get; set; }

        public SimulatedTrader(IAccount account, ILogger logger)
        {
            _account = account;
            _logger = logger;
        }

        private decimal _lastBuyPrice = 0;

        public bool Buy(decimal size, decimal price, TradeType type)
        {
            _orders.Add(new Order(TradeSide.Buy, size, price, type));

            _account.AssetBalance += size;
            _account.CurrencyBalance -= size * price;

            //Takers fee
            _account.CurrencyBalance -= (size * price) * 0.003m;

            _lastBuyPrice = price;

            _transactioncount++;
            _totalTakersFee+= (size * price) * 0.003m;

            _logger.Log($"Takers fee {(size * price) * 0.003m}");
            _logger.Log($"Transaction count {_transactioncount} total takers fee {_totalTakersFee} avg takers fee {_totalTakersFee/_transactioncount}");
            _logger.Log($"Currency {_account.CurrencyBalance} Asset {_account.AssetBalance}");

            return true;
        }

        public IEnumerable<IOrder> ListOpenOrders()
        {
            return this._orders;
        }

        public bool Sell(decimal size, decimal price, TradeType type)
        {
            _orders.Add(new Order(TradeSide.Sell, size, price, type));

            _account.AssetBalance -= size;
            _account.CurrencyBalance += size * price;

            //Takers fee
            _account.CurrencyBalance -= (size * price) * 0.003m;

            _transactioncount++;
            _totalTakersFee += (size * price) * 0.003m;

            decimal lossgain;

            if (IsLoss(_lastBuyPrice,price, 0.003m,size,out lossgain))
            {
                _logger.Log($"Sold at loss. Lost {lossgain}");
                LossSales++;
            } else
            {
                _logger.Log($"Sold at gain. Gained {lossgain}");
                WinSales++;
            }

            _logger.Log($"Takers fee {(size * price) * 0.003m}");
            _logger.Log($"Transaction count {_transactioncount} total takers fee {_totalTakersFee} avg takers fee {_totalTakersFee / _transactioncount}");
            _logger.Log($"Currency {_account.CurrencyBalance} Asset {_account.AssetBalance}");

            return true;
        }

        public bool IsLoss(decimal buy, decimal sell, decimal takersfee,decimal size, out decimal lossgain)
        {
            lossgain = size *( sell - buy * (1 + takersfee) / (1 - takersfee));

            return sell <= buy * (1 + takersfee) / (1 - takersfee);
        }
    }
}
