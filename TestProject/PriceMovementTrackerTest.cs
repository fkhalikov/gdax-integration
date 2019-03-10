using System;
using System.Collections.Generic;
using GDAX.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingPlatform.Common;
using TradingPlatform.FileUtils;

namespace TestProject
{
    [TestClass]
    public class PriceMovementTrackerTest
    {
        [TestMethod]
        public void TestPriceMovementDirection()
        {
            int precision = 2;

            List<Tuple<decimal,string>> expectedData = FileLoader.LoadPriceMovement(".\\TestData\\PriceMovementTracker\\expecteddata.csv");

            PriceMovementTracker tracker = new PriceMovementTracker(3, precision);

            foreach(var t in expectedData)
            {
                var expectedDirection = Enum.Parse(typeof(Direction), t.Item2);

                Assert.AreEqual(expectedDirection, tracker.Track(t.Item1));
            }
        }
    }
}
