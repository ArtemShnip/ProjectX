using System;
using System.Management;
using System.Diagnostics;

namespace ProjectX
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementEventWatcher startProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"calculator.exe\""));            
            startProgramm.EventArrived += startWatch_class;
            startProgramm.EventArrived += time_class;
            startProgramm.Start();


            ManagementEventWatcher stopProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = \"calculator.exe\""));
            stopProgramm.EventArrived += stopWatch_class;
            stopProgramm.EventArrived += time_class;
            stopProgramm.Start();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
            startProgramm.Stop();
            stopProgramm.Stop();
        }
        static void startWatch_class(object programm, EventArrivedEventArgs e)
        {
            Console.WriteLine("Process started: {0}", e.NewEvent.Properties["ProcessName"].Value);
        }
        static void stopWatch_class(object programm, EventArrivedEventArgs e)
        {
            Console.WriteLine("Process stopped: {0}", e.NewEvent.Properties["ProcessName"].Value);            
        }
        static void time_class(object programm, EventArrivedEventArgs e)
        {
            DateTime time = DateTime.Now;
            Console.WriteLine(time);
        }


    }
}