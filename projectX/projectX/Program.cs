using System;
using System.Management;
using System.Diagnostics;

namespace ProjectX
{
    class Program
    {
        public static  Stopwatch sekundomer = new Stopwatch();
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
            string cal = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (cal == "Calculator.exe")
            {
                ID = e.NewEvent.Properties["ProcessId"].ToString();  //????????????????????????????????    HANDLE     ???????????????
                nameStart = e.NewEvent.Properties["ProcessName"].Value.ToString();
                DateTime time = DateTime.Now;
                timeStart = time.ToLongTimeString();
                sekundomer.Start();
            }
        }
        static void stopWatch_class(object programm, EventArrivedEventArgs e)
        {
            DateTime time = DateTime.Now;
            nameStop = e.NewEvent.Properties["ProcessName"].Value.ToString();
            timeStop = time.ToLongTimeString();
            info();
        }
        static void info()
        {
            if (nameStop == "Calculator.exe")
            {
                sekundomer.Stop();
                TimeSpan ts = sekundomer.Elapsed;
                Console.WriteLine($@" {nameStart} started at {timeStart} closed at {timeStop} worked {ts}");
            }
        }
    }
}