using System;
using System.Collections.Generic;
using System.Text;

namespace GDAX.Models.Pipe
{
    public class Pipeline : IPipe
    {
        List<IPipe> _pipes = new List<IPipe>();

        public ITradeInfo Process(ITradeInfo tInfo)
        {
            var res = tInfo;

            foreach (var pipe in _pipes)
            {
                res = pipe.Process(res);
            }

            return res;
        }

        public void Add(IPipe pipe)
        {
            _pipes.Add(pipe);
        }
    }
}
