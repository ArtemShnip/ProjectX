using System;
using System.Management;


namespace ProjectX_V._2
{
    class Program
    {
        public static Sekundomer sekundomer = new Sekundomer();
        public static ProgrammInfo info = new ProgrammInfo();
        public static string[] arrayProgramm = { "Calculator.exe", "Illustrator.exe", "Photoshop.exe", "notepad.exe", "HxCalendarAppImm.exe", "mspaint.exe","Telegram.exe" };
        public static string id, nameStarted;

        public static void Main(string[] args)
        { 
            ManagementEventWatcher startProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace "));
            startProgramm.EventArrived += StartProcesses;
            startProgramm.Start();
            ManagementEventWatcher stopProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
            stopProgramm.EventArrived += StopProcesses;
            stopProgramm.Start();
            Console.WriteLine("          Press ENTER to exit and save");
            Console.ReadLine();
            sekundomer.StopAndSave();
            startProgramm.Stop();
            stopProgramm.Stop();
        }

        static void StartProcesses(object programm, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (Array.Exists(arrayProgramm, element => element == name) && name != nameStarted)
            {
                id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                DateTime time = DateTime.Now;
                Console.WriteLine("start");
                sekundomer.Start();
                nameStarted = e.NewEvent.Properties["ProcessName"].Value.ToString();
            }
        }

        static void StopProcesses(object programm, EventArrivedEventArgs e)
        {
            if (e.NewEvent.Properties["ProcessId"].Value.ToString().Equals(id))
            {
                nameStarted = "";
                DateTime time = DateTime.Now;
                Console.WriteLine("stop");
                sekundomer.Stop(info, programm , e);
                
            }
        }
    }
}
