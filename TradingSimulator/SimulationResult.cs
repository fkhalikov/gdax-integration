using GDAX.Models.Account;
using GDAX.Models.Trader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.Account;
using TradingSimulator.Trader;

namespace TradingSimulator
{
    public class SimulationResult
    {

        public SimulatedAccount Account { get; set; }
        public SimulatedTrader Trader { get; set; }
        public Dictionary<string, int> IntParams { get; set; }

    }
}
