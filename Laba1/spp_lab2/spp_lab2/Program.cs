using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lab2
{
    internal class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            CopyFile.EventCounter = new CountdownEvent(1);//

            Console.WriteLine("Enter path to initial directory");
            CopyFile.targetDirectory = Console.ReadLine();

            Console.WriteLine("Enter path to new directory");
            CopyFile.destDirectory = Console.ReadLine();

            logger.Info("Console app start");
            if (File.Exists(CopyFile.targetDirectory) == false && !File.Exists(CopyFile.destDirectory) == false)
            {
                Console.WriteLine("Directory doesn't exist");
                logger.Error("Directory doesn't exist");
                return;
            }

            const int threadNum = 5;
            ThreadCreate t = new ThreadCreate(threadNum);

            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                CopyFile.ProcessDirectory(CopyFile.targetDirectory, CopyFile.destDirectory);

                CopyFile.EventCounter.Signal();
                t.EndThread(threadNum);
                Console.WriteLine($"Всего скопировано файлов {CopyFile.n}");

                stopwatch.Stop();
                Console.WriteLine($"Время копирования: {stopwatch.Elapsed}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                logger.Error(e.Message);
            }
            logger.Info("End of copying files");
        }
    }
}
