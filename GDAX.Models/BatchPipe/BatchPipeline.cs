using GDAX.Models.BatchPipe;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDAX.Models.Pipe
{
    public class BatchPipeline : IBatchPipe
    {
        List<IBatchPipe> _pipes = new List<IBatchPipe>();

        public List<ITradeInfo> Process(List<ITradeInfo> tInfo)
        {
            List<ITradeInfo> result = tInfo;

            foreach (var pipe in _pipes)
            {
                result = pipe.Process(result);
            }

            return result;
        }

        public void Add(IBatchPipe pipe)
        {
            _pipes.Add(pipe);
        }
    }
}
