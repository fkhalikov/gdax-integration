using MarketData.Common;
using Storage.Azure;
using System;
using System.Linq;
using System.Threading.Tasks;
using Yahoo.API;

namespace MarketData.Poller.Services
{
  public class PricePollerService : IPollerService
  {
    private TableManager<StockConfiguration> tableManager;
    private readonly TableManager<StockHistory> stockHistoryManager;

    public YahooFinanceService yahooFinanceService { get; }

    public PricePollerService()
    {
      tableManager = new TableManager<StockConfiguration>("StockConfiguration");
      stockHistoryManager = new TableManager<StockHistory>("StockHistory");

      yahooFinanceService = new YahooFinanceService();
    }

    public async Task ExecuteAsync()
    {
      var data = tableManager.GetAll();

      var stocks = await yahooFinanceService.PullStocksAsync(data.Select(x => x.Ticker).ToArray());

      foreach (var stock in stocks)
      {
        try
        {
          stockHistoryManager.Add(new StockHistory()
          {
            Ask = stock.Ask,
            Bid = stock.Bid,
            Ticker = stock.Ticker,
            RowKey = $"{Guid.NewGuid()}",
            Timestamp = stock.Timestamp,
            PartitionKey = string.Empty,
            ETag = new Azure.ETag()
          });
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
        }
      }
    }
  }
}
