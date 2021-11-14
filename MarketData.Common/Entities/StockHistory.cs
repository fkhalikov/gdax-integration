using Azure;
using Azure.Data.Tables;
using System;

namespace MarketData.Common
{
  public class StockHistory : ITableEntity
  {
    public string Ticker { get; set; }

    public decimal Ask { get; set; }

    public decimal Bid { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public ETag ETag { get; set; }

    
  }
}
