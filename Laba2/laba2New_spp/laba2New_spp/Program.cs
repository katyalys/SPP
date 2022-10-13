using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace laba2New_spp
{
    internal class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Console app start");

            try
            {
                Threads threads = new Threads();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
        }
    }

}
