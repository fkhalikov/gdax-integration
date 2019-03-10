using GDAX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAX.Models
{
    public interface IPipe
    {

        ITradeInfo Process(ITradeInfo tInfo);

    }
}
