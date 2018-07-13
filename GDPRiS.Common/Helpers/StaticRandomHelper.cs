using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GDPRiS.Common.Helpers
{
    public static class StaticRandomHelper
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandomHelper()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }
}
