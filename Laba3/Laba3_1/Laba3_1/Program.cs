using Laba3_1;
using NLog;
using System.Reflection;

namespace Laba3_1 
{
    internal class Program
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Console app start");
            string? assemblyPath = Console.ReadLine();
            AssemblyInfo.CheckPathArg(assemblyPath); 
            try
            {
                string[] types = AssemblyInfo.GetAssemblerTypes(assemblyPath);
                foreach (string type in types)
                {
                    Console.WriteLine(type);
                    logger.Info(type);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
        }
    }
}

