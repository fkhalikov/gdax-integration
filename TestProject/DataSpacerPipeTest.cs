using System;
using System.Collections.Generic;
using DataSpacerPipe;
using GDAX.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class DataSpacerPipeTest
    {
        [TestMethod]
        public void TestDataSpacer_NoStartTime()
        {

            DataTimeSpacerPipe pipe = new DataTimeSpacerPipe(new TimeSpan(0, 15, 0), null);

            List<ITradeInfo> tInfos = new List<ITradeInfo>();

            for(int i = 0; i < 50; i++)
            {
                tInfos.Add(new TestTradeInfo() { Time = DateTime.Now.AddMinutes(5 * (i+1)) });
            }

            var result = pipe.Process(tInfos);

            Assert.AreEqual(17, result.Count);
        }

        [TestMethod]
        public void TestDataSpacer_WithStartTime()
        {

            DataTimeSpacerPipe pipe = new DataTimeSpacerPipe(new TimeSpan(0, 15, 0), DateTime.Now.AddMinutes(-3));

            List<ITradeInfo> tInfos = new List<ITradeInfo>();

            for (int i = 0; i < 50; i++)
            {
                tInfos.Add(new TestTradeInfo() { Time = DateTime.Now.AddMinutes(5 * (i + 1)) });
            }

            var result = pipe.Process(tInfos);

            Assert.AreEqual(16, result.Count);
        }
    }
}
