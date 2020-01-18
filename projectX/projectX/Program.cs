using System;
using System.Management;
using System.Diagnostics;

namespace ProjectX
{
    class Program
    {
        public static  Stopwatch sekundomerCal = new Stopwatch();
        public static Stopwatch sekundomerAi = new Stopwatch();
        public static Stopwatch sekundomerPS = new Stopwatch();
        public static string ID, nameStart, nameStop;
        public static string timeStart, timeStop;
       public static void Main(string[] args)
        {
            ManagementEventWatcher startProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            startProgramm.EventArrived += startWatch_class;
            startProgramm.Start();
            ManagementEventWatcher stopProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
            stopProgramm.EventArrived += stopWatch_class;
            stopProgramm.Start();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
            startProgramm.Stop();
            stopProgramm.Stop();
        }
        static void startWatch_class(object programm, EventArrivedEventArgs e)
        {
            //string name123 = e.NewEvent.Properties["ProcessName"].Value.ToString();
            //string id123 = e.NewEvent.Properties["ProcessId"].Value.ToString();
            //Console.WriteLine(" start  " + name123 + "------- " + id123);

            string cal = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (cal == "Calculator.exe")
            {
                ID = e.NewEvent.Properties["ProcessId"].Value.ToString();             //????????????????????????????????    HANDLE     ???????????????  Photoshop.exe
                nameStart = e.NewEvent.Properties["ProcessName"].Value.ToString();
                DateTime time = DateTime.Now;
                timeStart = time.ToLongTimeString();
                sekundomerCal.Start();
            }
            if (cal == "Illustrator.exe")
            {
                ID = e.NewEvent.Properties["ProcessId"].Value.ToString();
                nameStart = e.NewEvent.Properties["ProcessName"].Value.ToString();
                DateTime time = DateTime.Now;
                timeStart = time.ToLongTimeString();
                sekundomerAi.Start();
            }
            if (cal == "Photoshop.exe")
            {
                ID = e.NewEvent.Properties["ProcessId"].Value.ToString();
                nameStart = e.NewEvent.Properties["ProcessName"].Value.ToString();
                DateTime time = DateTime.Now;
                timeStart = time.ToLongTimeString();
                sekundomerPS.Start();
            }
        }
        static void stopWatch_class(object programm, EventArrivedEventArgs e)
        {

            //string name123 = e.NewEvent.Properties["ProcessName"].Value.ToString();
            //string id123 = e.NewEvent.Properties["ProcessId"].Value.ToString();
            //Console.WriteLine(" stop  " + name123 + "------- " + id123);

            DateTime time = DateTime.Now;
            nameStop = e.NewEvent.Properties["ProcessName"].Value.ToString();
            timeStop = time.ToLongTimeString();
            info();
        }
        static void info()
        {
            if (nameStop == "Calculator.exe")
            {
                sekundomerCal.Stop();
                TimeSpan ts = sekundomerCal.Elapsed;
                Console.WriteLine($@" {nameStart} started at {timeStart} closed at {timeStop} worked {ts} --------------- {ID}");
            }
            if (nameStop == "Illustrator.ex")
            {
                sekundomerAi.Stop();
                TimeSpan ts = sekundomerAi.Elapsed;
                Console.WriteLine($@" {nameStart} started at {timeStart} closed at {timeStop} worked {ts} --------------- {ID}");
            }
            if (nameStop == "Photoshop.exe")
            {
                sekundomerPS.Stop();
                TimeSpan ts = sekundomerPS.Elapsed;
                Console.WriteLine($@" {nameStart} started at {timeStart} closed at {timeStop} worked {ts} --------------- {ID}");
            }
        }
    }
}