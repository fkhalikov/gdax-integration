using DataSpacerPipe;
using GDAX.Models.Pipe;
using SimulationTradeReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingPlatform.Common.Extensions;
using TradingSimulator.Account;
using TradingSimulator.Engine;
using TradingSimulator.Factory;
using TradingSimulator.Logging;
using TradingSimulator.Trader;
using UpDownDecisionMakerPipe;

namespace TradingSimulator
{
    public class SimulationRunner
    {
        SimulationLogger logger = null;

        public void Run()
        {
            List<SimulationResult> result = new List<SimulationResult>();

            logger = new SimulationLogger(@"C:\gdax\logging\simulation\simulationlog" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".log");

            object objlock = new object();

            Parallel.For(3, 4, (i) =>
            {

              SimulationEngine smEngine = SimulatorFactory.CreateEMA(1000, logger);

                smEngine.Start();

                

                //lock (objlock)
                //{
                //    result.Add(new SimulationResult() { Account = account, Trader = trader, IntParams = intParams });
                //}
            });

            //decimal maxPossibleAccountBalance = 0;
            //SimulationResult bestOption = null;

            //foreach( var res in result)
            //{
            //    LogSimulationResult(res);

            //    decimal currentMax = res.Account.GetPossibleBalance(res.Trader.LastBuyPrice);

            //    if (currentMax>maxPossibleAccountBalance)
            //    {
            //        maxPossibleAccountBalance = currentMax;
            //        bestOption = res;
            //    }
            //}

            //logger.Log("");
            //logger.Log("Winner IS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            //LogSimulationResult(bestOption);
            
            //logger.Flush();

           //return result;
        }

        
        
    }
}
