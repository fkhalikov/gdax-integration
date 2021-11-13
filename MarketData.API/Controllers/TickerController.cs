using System.Collections.Generic;
using MarketData.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storage.Azure;

namespace MarketData.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TickerController : ControllerBase
  {
    private readonly ILogger<TickerController> _logger;
    private TableManager<StockConfiguration> tableManager;

    public TickerController(ILogger<TickerController> logger)
    {
      _logger = logger;
      tableManager = new TableManager<StockConfiguration>("StockConfiguration");
    }

    [HttpGet]
    public IEnumerable<StockConfiguration> Get()
    {
      return tableManager.GetAll();
    }
  }
}
