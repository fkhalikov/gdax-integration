using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingPlatform.Common
{
    public class FixedSizedQueue<T>
    {
        private readonly object privateLockObject = new object();

        readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

        public int Size { get; private set; }

        public FixedSizedQueue(int size)
        {
            Size = size;
        }

        public void Enqueue(T obj)
        {
            queue.Enqueue(obj);

            lock (privateLockObject)
            {
                while (queue.Count > Size)
                {
                    T outObj;
                    queue.TryDequeue(out outObj);
                }
            }
        }
    }
}
