using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfProjectX.Services;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using WpfProjectX.ProgramModels;
using System.Threading;

namespace WpfProjectX.Watcher
{
    class ProgramWatcher
    {
        public static string[] arrayProgramm = { "Calculator.exe", "Illustrator.exe", "Photoshop.exe", "notepad.exe", "HxCalendarAppImm.exe", "mspaint.exe","Telegram.exe" };
        public static Dictionary<string, string> runnedProgramms = new Dictionary<string, string>();
        private static MainWindow _mainWindow;

       public async void Wather()
       {
            await Task.Run(() =>
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
                Task.WaitAll();    //partially solved the problem, not notice "Console.ReadLine();"
                startProgramm.Stop();
                stopProgramm.Stop(); 


            });
       }

        void StartProcesses(object programm, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (Array.Exists(arrayProgramm, element => element == name) && runnedProgramms.ContainsValue(name) == false)
            {
                DateTime time = DateTime.Now;
                string t = time.ToString("g");
                string id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                runnedProgramms.Add(id, name);
                Console.WriteLine("Start\n" + name + "  ID: " + id + "  time " + t);

                try
                {
                    _mainWindow.Save(id); //  +		$exception	{"Ссылка на объект не указывает на экземпляр объекта."}	System.NullReferenceException
                }
                catch (Exception) { }
            }
        }

        void StopProcesses(object programm, EventArrivedEventArgs e)
        {
            string idS = e.NewEvent.Properties["ProcessId"].Value.ToString();
            if (runnedProgramms.ContainsKey(idS))
            {
                DateTime time = DateTime.Now;
                string t = time.ToString("g");
                string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
                Console.WriteLine("Stop\n" + name + "  ID: " + idS + "  time " + t);
                runnedProgramms.Remove(idS);
                _mainWindow.AddInSave(t, idS);
            }
        }
    }
}
