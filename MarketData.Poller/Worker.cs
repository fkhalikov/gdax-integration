using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MarketData.Poller.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MarketData.Poller
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> _logger;
    private readonly IEnumerable<IPollerService> services;

    public Worker(
      ILogger<Worker> logger,
      IEnumerable<IPollerService> services)
    {
      _logger = logger;
      this.services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        foreach (var service in services)
        {
          await service.ExecuteAsync();
        }

        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await Task.Delay(15000, stoppingToken);
      }
    }
  }
}
