using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Laba3_1
{
    public static class AssemblyInfo
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static void CheckPathArg(string path)
        {
            if (!File.Exists(path))
            {
                logger.Error("File {0} does not exist.", path);
                throw new FileNotFoundException("File {0} does not exist.", path);
            }
            string ext = Path.GetExtension(path);
            if (!(ext == ".exe" || ext == ".dll"))
            {
                logger.Error("Unknown extension: " + ext + ". Files with .exe and .dll extensions expected.");
                throw new FormatException("Unknown extension: " + ext + ". Files with .exe and .dll extensions expected.");
            }
        }

        public static string[] GetAssemblerTypes(string assemblyPath)
        {
            Assembly assemblyFile = Assembly.LoadFrom(assemblyPath);
            var pubTypes = assemblyFile.GetTypes().Where(type => type.IsPublic).OrderBy(type => type.Namespace + type.Name);
            string[] types = new string[pubTypes.Count()];

            int i = 0;
            foreach (var j in pubTypes)
            {
                types[i] = j.FullName;
                i++;
            }
            return types;
        }
    }

}
