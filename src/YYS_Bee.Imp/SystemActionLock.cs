using System.Collections.Concurrent;
using System.Threading;

namespace YYS_Bee.Imp
{
    public class SystemActionLock
    {
        private readonly ConcurrentQueue<string> _queue;
        public SystemActionLock()
        {
            _queue=new ConcurrentQueue<string>();
        }


        public void InsertQueue(string data)
        {
            _queue.Enqueue(data);
        }


        public void LineUp(string data)
        {
            InsertQueue(data);
            while (true)
            {
                string result;
                _queue.TryPeek(out result);
                if(result == data)
                    break;
                Thread.Sleep(1);
            }

        }

        public void Dequeue()
        {
            string data;
            _queue.TryDequeue(out data);
        }
    }
}
