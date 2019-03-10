using System;
using System.Collections.Generic;
using System.Text;

namespace GDAX.Models.Engine
{
    public interface ITradingEngine
    {

        event Action Tick;

        void Start();
    }
}
