using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace ProjectX_V._2
{
    class SaveInfo
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

        public void Stop(EventArrivedEventArgs e)
        {
            DateTime time = DateTime.Now;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            stopwatch.Reset();
            list.Add(new ProgrammInfo()
            {
                Id = e.NewEvent.Properties["ProcessId"].Value.ToString(),
                Name = e.NewEvent.Properties["ProcessName"].Value.ToString(),
                TimeStart = timeStart,
                TimeStop = time.ToLongTimeString(),
                LongTime = ts.ToString()
            });
            Console.WriteLine($"{e.NewEvent.Properties["ProcessName"].Value.ToString()} - data is saved");
        }

        public async void StopAndSave()
        {
            Serialization finish = new Serialization();
            await finish.SerializeAsync(list);
        }
    }
}
