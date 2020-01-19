using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Management;

namespace ProjectX_V._2
{
    class Sekundomer
    {
        Stopwatch stopwatch = new Stopwatch();
        public static List<ProgrammInfo> list = new List<ProgrammInfo>();
        public static string timeStart;

        public void Start()
        {
            stopwatch.Start();
            DateTime time = DateTime.Now;
            timeStart = time.ToLongTimeString();
        }

        public void Stop(ProgrammInfo info, object programm, EventArrivedEventArgs e)
        {
            DateTime time = DateTime.Now;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            stopwatch.Reset();

            list.Add(new ProgrammInfo()
            {
                Id = e.NewEvent.Properties["ProcessId"].Value.ToString(),
                NameStart = e.NewEvent.Properties["ProcessName"].Value.ToString(),
                TimeStart = timeStart,
                TimeStop = time.ToLongTimeString(),
                LongTime = ts.ToString()
            });
            Console.WriteLine($"{e.NewEvent.Properties["ProcessName"].Value.ToString()} - data is saved");
        }

        public void StopAndSave()
        {
            Serialization finish = new Serialization();
            finish.SerializeAsync(list);
        }
    }
}
