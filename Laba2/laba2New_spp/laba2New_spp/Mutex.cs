using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace laba2New_spp
{
    public class Mutex
    {
        //
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static int location1 = 0;
        private const int value = 1;
        private const int comparand = 0;
        private int threadId = 0;

        public void Lock()
        {
            while (Interlocked.CompareExchange(ref location1, value, comparand) != 0) { }
            threadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void Unlock()
        {
            if (threadId == Thread.CurrentThread.ManagedThreadId)
            {
                Interlocked.Exchange(ref location1, comparand);
            }
        }
    }
}
