using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class Processes

    {

        public string PID { get; set; }

        public string Name { get; set; }

        public string Threads { get; set; }

        public string RunningThreads { get; set; }

        public string Priority { get; set; }

        public Processes(string column1, string column2, string column3, string column4, string column5)

        {

            PID = column1;

            Name = column2;

            Threads = column3;

            RunningThreads = column4;

            Priority = column5;

        }

    }
}
