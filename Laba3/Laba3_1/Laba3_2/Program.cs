using Laba3_2;
using NLog;
using System.Collections.Concurrent;
using System.Timers;

namespace Laba3_1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("App started");
            LogBuffer logBuf = new LogBuffer();
            for (int i = 0; i < 1000; i++)
            {
                logBuf.Add($"Message - {i}");
                if (i % 100 == 0)
                {
                    Thread.Sleep(200);
                }
            }
            Thread.Sleep(1000);
            logBuf.Close();
            logger.Info("App finished");
        }
    }
}