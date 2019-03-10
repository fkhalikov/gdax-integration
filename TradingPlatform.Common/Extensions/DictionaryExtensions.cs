using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingPlatform.Common.Extensions
{
    public static class DictionaryExtensions
    {

        public static string ToMultilineString(this Dictionary<string, int> dict)
        {
            StringBuilder strBuilder = new StringBuilder();

            foreach (var keypair in dict)
            {
                strBuilder.AppendLine($"{keypair.Key}:{keypair.Value}");
            }
            return strBuilder.ToString();
        }
    }
}
