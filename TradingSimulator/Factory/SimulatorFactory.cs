using DataSpacerPipe;
using GDAX.Models.Pipe;
using SimulationTradeReader;
using System;
using System.Collections.Generic;
using TradingPlatform.Common.Extensions;
using TradingSimulator.Account;
using TradingSimulator.Engine;
using TradingSimulator.Trader;
using UpDownDecisionMakerPipe;

namespace TradingSimulator.Factory
{
  public class SimulatorFactory
  {

    public static Engine.SimulationEngine CreateEMA(decimal currency, Logging.SimulationLogger logger)
    {
      SimulatedTradeReader sm = new SimulatedTradeReader();

      Dictionary<string, int> intParams = new Dictionary<string, int>();
      intParams.Add("buystep", 3);
      intParams.Add("sellstep", 3);
      intParams.Add("trackerprecision", 3);

      Pipeline decisionPipeline = new Pipeline();
      SimulatedAccount account;
      SimulatedTrader trader;

      account = new SimulatedAccount();
      account.CurrencyBalance = currency;

      trader = new SimulatedTrader(account, logger);

      var upDownDecisionMaker = new UpDownBuySellDecisionMaker(trader, logger, intParams);
      decisionPipeline.Add(upDownDecisionMaker);

      BatchPipeline buyProcessingPipeline = new BatchPipeline();
      buyProcessingPipeline.Add(new DataTimeSpacerPipe(new TimeSpan(0, 30, 0), null));
      buyProcessingPipeline.Add(new EmaCalculator.EMACalculatorPipe(10));

      BatchPipeline sellProcessingPipeline = new BatchPipeline();
      sellProcessingPipeline.Add(new DataTimeSpacerPipe(new TimeSpan(0, 30, 0), null));
      sellProcessingPipeline.Add(new EmaCalculator.EMACalculatorPipe(10));

      SimulationEngine smEngine = new SimulationEngine(sm, buyProcessingPipeline, sellProcessingPipeline, decisionPipeline);

      smEngine.Tick += () =>
      {
        LogSimulationResult(new SimulationResult() { Account = account, Trader = trader, IntParams = intParams }
        , logger);
        logger.Flush();
      };

      return smEngine;
    }


    private static void LogSimulationResult(SimulationResult r, Logging.SimulationLogger logger)
    {
      logger.Log("");
      logger.Log("Simulation run parameters");
      logger.Log(r.IntParams.ToMultilineString());
      logger.Log($"Curency {r.Account.CurrencyBalance} asset {r.Account.AssetBalance} Possible account balance {r.Trader.LastBuyPrice * r.Account.AssetBalance + r.Account.CurrencyBalance} Takers fee amounted for {r.Trader.TotalTakersFee} transaction count {r.Trader.TransactionCount} avg takers fee {(r.Trader.TransactionCount != 0 ? r.Trader.TotalTakersFee / r.Trader.TransactionCount : 0m)}");
      logger.Log($"Win trades {r.Trader.WinSales}, loss trades {r.Trader.LossSales}");
      logger.Log("");
    }

  }
}
