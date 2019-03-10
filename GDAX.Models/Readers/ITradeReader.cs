using System;
using System.Collections.Generic;
using System.Text;

namespace GDAX.Models.Readers
{
    public interface ITradeReader
    {
        List<ITradeInfo> Read();
    }
}
