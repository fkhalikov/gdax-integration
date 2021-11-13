using Azure;
using Azure.Data.Tables;
using System;

namespace MarketData.Common
{
  public class StockConfiguration : ITableEntity
  {
    public string Ticker { get; set; }

    public bool IsNew { get; set; }

    public DateTimeOffset? Timestamp { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public ETag ETag { get; set; }
  }
}
