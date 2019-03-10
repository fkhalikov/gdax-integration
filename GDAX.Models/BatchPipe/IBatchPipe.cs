using System;
using System.Collections.Generic;
using System.Text;

namespace GDAX.Models.BatchPipe
{
    public interface IBatchPipe
    {

        List<ITradeInfo> Process(List<ITradeInfo> trades);

    }
}
