using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace projectX
{
    class Filter
    {
        public void FilterStart(string id)
        {
            Console.WriteLine("Start\n");
            Process myProc = null;
            try
            {
                myProc = Process.GetProcessById(int.Parse(id));
                Console.WriteLine("\n-> PID: {0}\tName: {1}\n", myProc.Id, myProc.ProcessName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName != myProc.ProcessName)
                {
                    Console.WriteLine($"ID: {process.Id}  Name: {process.ProcessName}");
                    Console.WriteLine("\n-> PID: {0}\tName: {1}\n", myProc.Id, myProc.ProcessName);
                }
            }
        }

        public void FilterStop(string id)
        {
            Console.WriteLine("Stop\n");
            
            Process myProc = null;
            try
            {
                int i = int.Parse(id);
                myProc = Process.GetProcessById(i);
                Console.WriteLine("\n-> PID: {0}\tName: {1}\n", myProc.Id, myProc.ProcessName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
