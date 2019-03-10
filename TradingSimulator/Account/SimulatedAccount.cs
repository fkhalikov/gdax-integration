using GDAX.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.Account
{
    public class SimulatedAccount : IAccount
    {
        private decimal _currencyBalance = 0;

        public decimal CurrencyBalance
        {
            get
            {
                return _currencyBalance;
            }
            set
            {
                _currencyBalance = value;

                Console.WriteLine($"Currency balance {_currencyBalance}");

                if (_currencyBalance < 0)
                {
                    throw new AccountBalanceIsNegativeException();
                }
            }
        }


        decimal _assetBalance = 0;
        public decimal AssetBalance
        {
            get
            { return _assetBalance; }
            set
            {
                _assetBalance = value;

                Console.WriteLine($"Asset balance {_assetBalance}");

                if (_assetBalance < 0)
                {
                    throw new AssetBalanceIsNegativeException();
                }
            }
        }

        public decimal GetPossibleBalance(decimal price)
        {
            return _assetBalance * price + _currencyBalance;
        }
    }
}
