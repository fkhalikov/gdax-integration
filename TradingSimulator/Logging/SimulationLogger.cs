using GDAX.Models.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.Logging
{
    public class SimulationLogger : ILogger
    {
        private string _filename = null;

        StringBuilder _strBuilder = new StringBuilder();

        public SimulationLogger(string filename)
        {
            _filename = filename;

            if (File.Exists(_filename))
            {
                File.Create(_filename);
            }
        }

        object lockObject = new object();

        public void Log(string messsage)
        {
            lock (lockObject)
            {
                string message = $"{DateTime.Now} - {messsage} {Environment.NewLine}";

                _strBuilder.Append(message);
                Console.Write(message);

                if (_strBuilder.Length > 1000000)
                {
                    Flush();
                }

            }
        }

        public void Flush()
        {
            if (_strBuilder.Length > 0)
            {
                File.AppendAllText(_filename, _strBuilder.ToString());
                _strBuilder.Clear();
            }

        }
    }
}
