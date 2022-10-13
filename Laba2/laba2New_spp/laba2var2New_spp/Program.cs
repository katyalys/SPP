using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace laba2var2New_spp
{
    internal class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Console app started");
            OSHandle handle = new OSHandle();
            logger.Info("Created object of OSHandle");
            Console.WriteLine(handle.Handle.ToString());
            handle.Dispose();
            logger.Info("OSHandle objected was disposed");

            try
            {
                Console.WriteLine(handle.Handle.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

    }
}
