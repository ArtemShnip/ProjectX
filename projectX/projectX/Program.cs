using System;
using System.Management;
using System.Diagnostics;
using projectX;

namespace ProjectX
{
    class Program
    {
        public static string[] arrayProgramm = { "Calculator.exe", "Illustrator.exe", "Photoshop.exe", "notepad.exe", "HxCalendarAppImm.exe", "mspaint.exe", "Telegram.exe" };
        public static string id;
        public static Filter filter = new Filter();


        public static void Main(string[] args)
        {
            ManagementEventWatcher startProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            startProgramm.EventArrived += StartProcesses;
            startProgramm.Start();
            ManagementEventWatcher stopProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
            stopProgramm.EventArrived += StopProcesses;
            stopProgramm.Start();
            Console.WriteLine("          Press ENTER to exit and save");
            Console.ReadLine();
            startProgramm.Stop();
            stopProgramm.Stop();
        }

        static void StartProcesses(object programm, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (Array.Exists(arrayProgramm, element => element == name) )
            {
                DateTime time = DateTime.Now;
                string timeStart = time.ToLongTimeString();
                id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                Console.WriteLine();
                Console.WriteLine("Start\n"+name + "  ID: "+id +"  time "+timeStart);
                //filter.FilterStart(id);
            }
        }

        static void StopProcesses(object programm, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (Array.Exists(arrayProgramm, element => element == name))
            {
                DateTime time = DateTime.Now;
                string timeStart = time.ToLongTimeString();
                id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                Console.WriteLine();
                Console.WriteLine("Stop\n"+name + "  ID: "+id + "  time " + timeStart);
                //filter.FilterStart(id);
            }
            //if (e.NewEvent.Properties["ProcessId"].Value.ToString().Equals(id))
            //{
            //    filter.FilterStop(id);
            //}
        }
    }
}
