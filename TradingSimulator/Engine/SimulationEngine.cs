using GDAX.Models;
using GDAX.Models.BatchPipe;
using GDAX.Models.Engine;
using GDAX.Models.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TradingSimulator.Engine
{
    public class SimulationEngine : ITradingEngine
    {
        public event Action Tick;

        Timer timer = null;
        private readonly ITradeReader _tradeReader;
        private readonly IBatchPipe _buyProcessingPipeline;
        private readonly IBatchPipe _sellProcessingPipeline;
        private readonly IPipe _decisionPipeline;

        public SimulationEngine(
              ITradeReader tradeReader
            , IBatchPipe buyProcessingPipeline
            , IBatchPipe sellProcessingPipeline
            , IPipe decisionPipeline)
        {
          
            this._tradeReader = tradeReader;
            this._buyProcessingPipeline = buyProcessingPipeline;
            this._sellProcessingPipeline = sellProcessingPipeline;
            this._decisionPipeline = decisionPipeline;
        }

        public void Start()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Start();
            
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) 
        {
            timer.Enabled = false;

            try
            {
                List<ITradeInfo> trades = this._tradeReader.Read();

                var buyProcessedTrades = _buyProcessingPipeline.Process(trades.Where(x=>x.Side == TradeSide.Buy).ToList());
                var sellProcessedTrades = _sellProcessingPipeline.Process(trades.Where(x => x.Side == TradeSide.Sell).ToList());

                List<ITradeInfo> processedTrades = buyProcessedTrades;
                processedTrades.AddRange(sellProcessedTrades);

                processedTrades = processedTrades.OrderBy(x => x.TradeId).ToList();
                
                foreach(var trade in processedTrades)
                {
                    _decisionPipeline.Process(trade);
                }

                Tick?.Invoke();
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                timer.Enabled = true;
            }
        }
    }
}
