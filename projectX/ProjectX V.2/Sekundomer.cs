using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ProjectX_V._2
{
    class Sekundomer
    {
        Stopwatch stopwatch = new Stopwatch();

        public void Start(ProgrammInfo info)
        {
            stopwatch.Start();
        }

        public void Stop(ProgrammInfo info)
        {
            TimeSpan ts = stopwatch.Elapsed;
            info.LongTime = ts.ToString();
            stopwatch.Stop();
            Console.WriteLine($"{info.ToString()}");
        }
    }
}
