using System;
using System.Management;

namespace ProjectX_V._2
{
    class Program
    {
        public static Sekundomer sekundomer = new Sekundomer();
        public static ProgrammInfo info = new ProgrammInfo();
        public static string[] arrayProgramm = { "Calculator.exe", "Illustrator.exe", "Photoshop.exe" };

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
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (Array.Exists(arrayProgramm, element => element == name))
            {
                info.Id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                info.NameStart = e.NewEvent.Properties["ProcessName"].Value.ToString();
                DateTime time = DateTime.Now;
                info.TimeStart = time.ToLongTimeString();
                sekundomer.Start(info);
            }
        }

        static void stopWatch_class(object programm, EventArrivedEventArgs e)
        {
            if (e.NewEvent.Properties["ProcessId"].Value.ToString().Equals(info.Id))
            {
                info.NameStop = e.NewEvent.Properties["ProcessName"].Value.ToString();
                DateTime time = DateTime.Now;
                info.TimeStop = time.ToLongTimeString();
                sekundomer.Stop(info);
            }
            
        }
    }
}
