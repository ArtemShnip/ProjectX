using System;
using System.Management;

namespace ProjectX
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementEventWatcher startWatch = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"calculator.exe\""));
            startWatch.EventArrived += startWatch_class;
            startWatch.EventArrived += time_class;
            startWatch.Start();


            ManagementEventWatcher stopWatch = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = \"calculator.exe\""));
            stopWatch.EventArrived += stopWatch_class;
            stopWatch.EventArrived += time_class;
            stopWatch.Start();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
            startWatch.Stop();
            stopWatch.Stop();
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