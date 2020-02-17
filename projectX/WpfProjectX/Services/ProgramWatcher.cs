using System;
using System.Collections.Generic;
using System.Management;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;
using System.IO;

namespace WpfProjectX.Watcher
{
    class ProgramWatcher
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\SaveData\\programDataArray.xml";
        public static string[] arrayProgramm;
        public static Dictionary<string, string> runnedProgramms = new Dictionary<string, string>();
        public delegate void EventWatch(string id);
        public event EventWatch NotifyStart, NotifyStop;

        public async void Wather()
        {
            LoadArray(_path);
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
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            if (Array.Exists(arrayProgramm, element => element == name) && !runnedProgramms.ContainsValue(name))
            {
                string id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                runnedProgramms.Add(id, name);
                NotifyStart?.Invoke(id);
            }
        }

        void StopProcesses(object programm, EventArrivedEventArgs e)
        {
            string id = e.NewEvent.Properties["ProcessId"].Value.ToString();
            if (runnedProgramms.ContainsKey(id))
            {
                runnedProgramms.Remove(id);
                NotifyStop?.Invoke(id);
            }
        }

        public void LoadArray(string path)
        {
            Type type = typeof(string[]);
            string[] retVal;

            XmlSerializer formatter = new XmlSerializer(type);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                retVal = (string[])formatter.Deserialize(stream);
            }
            arrayProgramm = retVal;
        }
    }
}
