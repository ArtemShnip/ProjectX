using System;
using System.Collections.Generic;
using System.Management;
using System.Threading.Tasks;
using System.Threading;

namespace WpfProjectX.Watcher
{
    class ProgramWatcher
    {
        public static string[] arrayProgramm = { "Calculator.exe", "Illustrator.exe", "Photoshop.exe", "notepad.exe", "HxCalendarAppImm.exe", "mspaint.exe","Telegram.exe" };
        public static Dictionary<string, string> runnedProgramms = new Dictionary<string, string>();
        public delegate void EventWatch(string id);
        //public delegate void EventWatch2(string id, string time);
        public event EventWatch NotifyStart, NotifyStop;
        //public event EventWatch2 ;

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
                Thread.Sleep(2147483647);                                                   //partially solved the problem
                startProgramm.Stop();
                stopProgramm.Stop();
            });
       }

        void StartProcesses(object programm, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();                                        // получаю имя запущеного процесса
            if (Array.Exists(arrayProgramm, element => element == name) && !runnedProgramms.ContainsValue(name))        // смотрю есть ли этот процесс в массиве
            {
                string id = e.NewEvent.Properties["ProcessId"].Value.ToString();                                        // получаю Id запущенного процесса
                runnedProgramms.Add(id, name);                                                                          // добавляю в коллекцию запущенных программ Name & Id
                NotifyStart?.Invoke(id);
            }
        }

        void StopProcesses(object programm, EventArrivedEventArgs e)
        {
            string id = e.NewEvent.Properties["ProcessId"].Value.ToString();                                            // смотрю Id остановившегося процесса
            if (runnedProgramms.ContainsKey(id))                                                                        // смотрю есть ли такой Id в колекции запущенных программ
            {
                runnedProgramms.Remove(id);                                                                             // удаляю из коллекции остановившуюся программу
                NotifyStop?.Invoke(id);
            }
        }
    }
}
