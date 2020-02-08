using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WpfProjectX.Services;

namespace WpfProjectX.Watcher
{
    class ProgramWatcher
    {
        public static string[] arrayProgramm = { "Calculator.exe", "Illustrator.exe", "Photoshop.exe", "notepad.exe", "HxCalendarAppImm.exe", "mspaint.exe","Telegram.exe" };
        public static Dictionary<string, string> runnedProgramms = new Dictionary<string, string>();
        private static AddedList _addedList;
        private static MainWindow _mainWindow;

        public static void Wather()
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
            if (Array.Exists(arrayProgramm, element => element == name) && runnedProgramms.ContainsValue(name) == false)
            {
                DateTime time = DateTime.Now;
                string t = time.ToString("g");
                string id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                runnedProgramms.Add(id, name);
                Console.WriteLine("Start\n" + name + "  ID: " + id + "  time " + t);
                _mainWindow.Save(id);
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
                runnedProgramms.Remove(id);
                _mainWindow.AddInSave(t, id);
            }
        }
    }
}
