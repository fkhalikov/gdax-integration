using GDAX.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingPlatform.FileUtils
{
    public static class FileLoader
    {

        public static void Save(string filename, List<ITradeInfo> trades)
        {
            StringBuilder data = new StringBuilder();

            foreach(var trade in trades)
            {
                data.AppendLine($"{trade.Time},{trade.Price}");
            }

            File.WriteAllText(filename, data.ToString());
        }

        public static List<ITradeInfo> Load(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            List<ITradeInfo> tInfos = new List<ITradeInfo>();

            foreach (var line in lines)
            {
                var columns = line.Split(',');

                tInfos.Add(new GenericTradeInfo(Convert.ToDecimal(columns[1]))
                {
                    Time = DateTime.FromOADate(Convert.ToDouble(columns[0]))
                    ,
                    Price = Convert.ToDecimal(columns[1])
                });
            }

            return tInfos;
        }

        public static List<Tuple<decimal,string>> LoadPriceMovement(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            List<Tuple<decimal,string>> tInfos = new List<Tuple<decimal, string>>();

            foreach (var line in lines)
            {
                var columns = line.Split(',');

                tInfos.Add(new Tuple<decimal, string>(Convert.ToDecimal(columns[0]), columns[1]));
            }

            return tInfos;
        }
    }
}
