﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ProjectX_V._2
{
    class Program
    {
        public static Sekundomer sekundomer = new Sekundomer();
        public static ProgrammInfo info = new ProgrammInfo();
        public static string[] arrayProgramm = { "Calculator.exe", "Illustrator.exe", "Photoshop.exe", "notepad.exe", "HxCalendarAppImm.exe", "mspaint.exe","Telegram.exe" };
        public static string id;
        public static string nameTest,idTest,timeTest;

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
            if (Array.Exists(arrayProgramm, element => element == name))
            {
                id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                DateTime time = DateTime.Now;
                sekundomer.Start();
                //Console.WriteLine("start");


                nameTest = e.NewEvent.Properties["ProcessName"].Value.ToString();
                idTest = e.NewEvent.Properties["ProcessId"].Value.ToString();
                timeTest = time.ToString();
                Console.WriteLine($"{nameTest}   {idTest}  {timeTest}");
            }
        }

        static void StopProcesses(object programm, EventArrivedEventArgs e)
        {
            if (e.NewEvent.Properties["ProcessId"].Value.ToString().Equals(id))
            {
                DateTime time = DateTime.Now;
                nameTest = e.NewEvent.Properties["ProcessName"].Value.ToString();
                idTest = e.NewEvent.Properties["ProcessId"].Value.ToString();
                timeTest = time.ToString();
                Console.WriteLine($"{nameTest}   {idTest}  {timeTest}");
                sekundomer.Stop(info, programm , e);
                //Console.WriteLine("stop");
            }
        }
    }
}
