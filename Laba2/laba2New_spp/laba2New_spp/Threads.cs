using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace laba2New_spp
{
    public class Threads
    {
        public int num = 5;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Threads()
        {
            Thread myThread = new Thread(Inc);
            myThread.Start();
            Thread myThread1 = new Thread(Dec);
            myThread1.Start();
        }

        public void Inc()
        {
            for (int i = 1; i <= num; i++)
            {
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} waits for mutex");
                logger.Info($"Thread {Environment.CurrentManagedThreadId} waits for mutex");

                SharedRes.mutex.Lock();
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} gets mutex");
                logger.Info($"Thread {Environment.CurrentManagedThreadId} gets mutex");
                //
                SharedRes.Count++;
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId}: value of Count = {SharedRes.Count}");
                logger.Info($"Thread {Environment.CurrentManagedThreadId}: value of Count = {SharedRes.Count}");
                SharedRes.mutex.Unlock();
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} released mutex");
                logger.Info($"Thread {Environment.CurrentManagedThreadId} released mutex");
            }
        }

        public void Dec()
        {
            for (int i = 1; i <= num; i++)
            {
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} waits for mutex");
                logger.Info($"Thread {Environment.CurrentManagedThreadId} waits for mutex");

                SharedRes.mutex.Lock();
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} gets mutex");
                logger.Info($"Thread {Environment.CurrentManagedThreadId} gets mutex");
                //
                SharedRes.Count--;
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId}: value of Count = {SharedRes.Count}");
                logger.Info($"Thread {Environment.CurrentManagedThreadId}: value of Count = {SharedRes.Count}");
                SharedRes.mutex.Unlock();
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} released mutex");
                logger.Info($"Thread {Environment.CurrentManagedThreadId} released mutex");
            }
        }


    }
}
