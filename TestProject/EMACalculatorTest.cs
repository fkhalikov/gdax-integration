using System;
using System.Collections.Generic;
using EmaCalculator;
using GDAX.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingPlatform.FileUtils;

namespace TestProject
{
    [TestClass]
    public class EMACalculatorTest
    {
        [TestMethod]
        public void CalculateEMA()
        {
            string inputDataFile = @".\TestData\EmaCalculator\inputdata.csv";
            string expectedDataFile = @".\TestData\EmaCalculator\expectedoutput.csv";

            List<ITradeInfo> inputTrades = FileLoader.Load(inputDataFile);
            List<ITradeInfo> expectedTrades = FileLoader.Load(expectedDataFile);

            EMACalculatorPipe emaPipe = new EMACalculatorPipe(10);

            var actual = emaPipe.Process(inputTrades);

            for(int i = 0; i < expectedTrades.Count;i++)
            {
                Assert.AreEqual(actual[i].Time, expectedTrades[i].Time);
                Assert.IsTrue(Math.Abs(actual[i].Price - expectedTrades[i].Price)<0.0000001m);
            }

        }
    }
}
