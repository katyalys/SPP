using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lab1
{
    internal class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program is started.");


            Console.WriteLine("Введите количество потоков:");
            try
            {
                int ThreadNum = Convert.ToInt32(Console.ReadLine());
                if (ThreadNum <= 0)
                {
                    logger.Error("Введите положительное число");
                }
                else
                {
                    QueueTask t = new QueueTask(ThreadNum);
                    t.Delegates();
                    t.EndThread(ThreadNum);
                }
            }
            catch (FormatException e)
            {
                logger.Error("Введено не число");
            }

            WriteLogFile.WriteLog("ConsoleLog", String.Format("{0} @ {1}", "Log is Created at", DateTime.Now));
        }
    }

    class WriteLogFile
    {
        public static bool WriteLog(string strFileName, string strMessage)
        {
            try
            {
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", Path.GetTempPath(), strFileName), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                objFilestream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
