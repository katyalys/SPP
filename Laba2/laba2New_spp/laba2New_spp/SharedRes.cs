using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2New_spp
{
    public class SharedRes
    {
        public static int Count = 0;
        public static Mutex mutex = new Mutex();
    }
}
