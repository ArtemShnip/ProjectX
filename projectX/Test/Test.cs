using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace Test
{
    class Test
    {
        static void Main(string[] args)
        {
            foreach (Process process in Process.GetProcesses())
            {
                // выводим id и имя процесса
                Console.WriteLine($"ID: {process.StartTime.ToLongTimeString()}  Name: {process.ProcessName}");
            }
        }
    }
}
