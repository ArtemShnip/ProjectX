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
        public static string id;
        public static string nameTest, idTest, timeTest, parentIdTest, SessionId, CreationDate, Caption;

        public static void Main(string[] args)
        {
            // WHERE ProcessName = \"Illustrator.exe\" 
            ManagementEventWatcher startProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace "));
            startProgramm.EventArrived += startWatch_class;
            startProgramm.Start();
            ManagementEventWatcher stopProgramm = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace  "));
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
            SessionId = e.NewEvent.Properties["SessionId"].Value.ToString();
            parentIdTest = e.NewEvent.Properties["ParentProcessId"].Value.ToString();
            nameTest = e.NewEvent.Properties["ProcessName"].Value.ToString();
            idTest = e.NewEvent.Properties["ProcessId"].Value.ToString();
            DateTime time = DateTime.Now;
            timeTest = time.ToString();

            Console.WriteLine($"START               name    {nameTest}   id    {idTest}   time    {timeTest}   parent   {parentIdTest}   SessionId  {SessionId}   ");
        }

        static void stopWatch_class(object programm, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            SessionId = e.NewEvent.Properties["SessionId"].Value.ToString();
            parentIdTest = e.NewEvent.Properties["ParentProcessId"].Value.ToString();
            nameTest = e.NewEvent.Properties["ProcessName"].Value.ToString();
            idTest = e.NewEvent.Properties["ProcessId"].Value.ToString();
            DateTime time = DateTime.Now;
            timeTest = time.ToString();

            Console.WriteLine($"STOP               name    {nameTest}   id    {idTest}   time    {timeTest}   parent   {parentIdTest}   SessionId  {SessionId} ");
        }

       
    }
}