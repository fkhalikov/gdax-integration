using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yahoo.API
{
  public class YahooFinanceService
  {
    public YahooFinanceService()
    {
    }

    public async Task<IEnumerable<IStock>> PullStocksAsync(string[] tickers)
    {
      var tickerBatch = new List<string>();
      var result = new List<IStock>();

      for(int i = 0; i < tickers.Length; i++)
      {
        tickerBatch.Add(tickers[i]);

        if ((i + 1) % 10 == 0 || i == tickers.Length - 1)
        {
          var stocks = await YahooFinanceApi.Yahoo.Symbols(tickers).QueryAsync();
          result.AddRange(stocks.Select(x => new YahooStock(x.Value)));
        }
      }

      return result;
    }

  }
}
