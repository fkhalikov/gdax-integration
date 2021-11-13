using System;

namespace Core.Models
{
  public interface IStock
  {

    string Ticker { get; }
    
    string Name { get; }

    Decimal Ask { get; }

    Decimal Bid { get; }

    DateTime Timestamp { get; }

  }
}
