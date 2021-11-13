using Core.Models;
using System;
using YahooFinanceApi;

namespace Yahoo.API
{
  public class YahooStock : IStock
  {
    private readonly Security security;

    public YahooStock(YahooFinanceApi.Security security)
    {
      Timestamp = DateTime.UtcNow;
      this.security = security;
    }

    public string Ticker => security.Symbol;

    public string Name => security.ShortName;

    public decimal Ask => (decimal)security.Ask;

    public decimal Bid => (decimal)security.Bid;

    public DateTime Timestamp
    {
      get;set;
    }
  }
}
