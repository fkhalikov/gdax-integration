using DataSpacerPipe;
using EmaCalculator;
using GDAX.Models;
using SimulationTradeReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingPlatform.FileUtils;

namespace TradingSimulator
{
    public class EMARunner
    {

        public void Run()
        {

            SimulatedTradeReader smReader = new SimulatedTradeReader();

            List<ITradeInfo> allTrades = new List<ITradeInfo>();

            allTrades = smReader.Read();

            DataTimeSpacerPipe dtsp = new DataTimeSpacerPipe(new TimeSpan(0, 15, 0), null);

           var spacedTrades = dtsp.Process(allTrades);

            EMACalculatorPipe emacalc = new EMACalculatorPipe(10);

            var emaTrades = emacalc.Process(spacedTrades);

            FileLoader.Save(@"c:\temp\emadata.csv", emaTrades);
            FileLoader.Save(@"c:\temp\originaldata.csv", allTrades);
        }

    }
}
