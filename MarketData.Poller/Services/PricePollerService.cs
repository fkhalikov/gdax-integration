using MarketData.Common;
using Storage.Azure;
using System.Linq;
using System.Threading.Tasks;
using Yahoo.API;

namespace MarketData.Poller.Services
{
  public class PricePollerService : IPollerService
  {
    private TableManager<StockConfiguration> tableManager;

    public YahooFinanceService yahooFinanceService { get; }

    public PricePollerService()
    {
      tableManager = new TableManager<StockConfiguration>("StockConfiguration");

      yahooFinanceService = new YahooFinanceService();
    }

    public async Task ExecuteAsync()
    {
      var data = tableManager.GetAll();

      foreach(var config in data)
      {
        
      }

      var stocks = await yahooFinanceService.PullStocksAsync(data.Select(x => x.Ticker).ToArray()); 
    }
  }
}
