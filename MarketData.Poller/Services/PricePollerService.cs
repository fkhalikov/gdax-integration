using MarketData.Poller.Entities;
using Storage.Azure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketData.Poller.Services
{
  public class PricePollerService : IPollerService
  {
    private TableManager<StockConfiguration> tableManager;

    public PricePollerService()
    {
      tableManager = new TableManager<StockConfiguration>("StockConfiguration");
    }

    public Task ExecuteAsync()
    {
      var data = tableManager.GetAll();

      foreach(var d in data)
      {
        Console.WriteLine(d);
      }

      return Task.CompletedTask;
    }
  }
}
