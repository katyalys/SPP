using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lab2
{
    public class ThreadCreate
    {
        private readonly string targetDirectory = CopyFile.targetDirectory;
        Queue<Thread> ThreadList = new Queue<Thread>();
        private Logger logger = LogManager.GetCurrentClassLogger();

        bool shouldEndThread = false;
        object locker = new object();

        public ThreadCreate(int ThreadNum)
        {

            for (int i = 1; i <= ThreadNum; i++)
            {
                Thread thread1 = new Thread(LockThread);
                thread1.Start();
                ThreadList.Enqueue(thread1);
            }

        }

        public void EndThread(int ThreadNum)
        {
            while (CopyFile.EventCounter.IsSet == false) { }

            if (CopyFile.EventCounter.IsSet == true)
            {
                shouldEndThread = true;
                for (int i = 0; i < ThreadNum; i++)
                {
                    ThreadList.Dequeue();
                }
                logger.Info("Threads finished");
            }

        }

        public void LockThread()
        {
            while (!shouldEndThread)
            {
                if (CopyFile.fileList.Count != 0)
                {
                    Console.WriteLine("\nThread execution {0}", Environment.CurrentManagedThreadId);
                    logger.Info($"Id of thread: {Environment.CurrentManagedThreadId}");
                }

                lock (locker)
                {
                    if (CopyFile.fileList.Count != 0 && CopyFile.destList.Count != 0)
                    {
                        CopyFile.CopyFiles();
                    }
                }
            }
        }
    }
}
