using System;
using TradingPlatform.Common.Extensions;

namespace TradingSimulator
{
    class Program
    {
       

        static void Main(string[] args)
        {
             SimulationRunner sr = new SimulationRunner();

             sr.Run();

            Console.ReadLine();
            //EMARunner emaRunner = new EMARunner();

            //emaRunner.Run();
        }
    }
}
