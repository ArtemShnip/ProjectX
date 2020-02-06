using System;
using System.Management;
using System.Diagnostics;
using projectX;
using System.Collections.Generic;

namespace ProjectX
{
    class Program
    {
        public static string[] arrayProgramm = { "Calculator.exe", "Illustrator.exe", "Photoshop.exe", "notepad.exe", "HxCalendarAppImm.exe", "mspaint.exe", "Telegram.exe" };
        public static Dictionary<string, string> runnedProgramms = new Dictionary<string,string>();
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
                string t = time.ToString("g");
                Console.WriteLine(t);
                string id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                runnedProgramms.Add(id, t);
                Console.WriteLine("Start\n" + name + "  ID: " + id + "  time " + t);
            }
        }

        static void StopProcesses(object programm, EventArrivedEventArgs e)
        {
            string id = e.NewEvent.Properties["ProcessId"].Value.ToString();
            if (runnedProgramms.ContainsKey(id))
            {
                DateTime time = DateTime.Now;
                string t = time.ToString("g");
                string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
                Console.WriteLine("Stop\n" + name + "  ID: " + id + "  time " + t);
            }
        }
    }
}
