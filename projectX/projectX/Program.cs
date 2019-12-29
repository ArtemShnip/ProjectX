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
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (name == "Calculator.exe")
            {
                bool start = true;
                Calculator_Class(start);
            }
        }
        static void stopWatch_class(object programm, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (name == "Calculator.exe")
            {
                bool start = false;
                Calculator_Class(start);
            }
        }
        static void Calculator_Class(bool start)
        {
            Stopwatch sekundomer = new Stopwatch();     //??????????????????????????
            if (start == true)
            {                
                Console.Write("START  Calculator  ");
                DateTime time = DateTime.Now;
                Console.WriteLine(time);
                sekundomer.Start();
            }
            else
            {
                Console.Write("STOP  Calculator  ");
                DateTime time = DateTime.Now;
                Console.WriteLine(time);
                sekundomer.Stop();
                TimeSpan ts = sekundomer.Elapsed;                
                Console.WriteLine("\nсекундомер -- " + ts);
            }
        }
    }
}