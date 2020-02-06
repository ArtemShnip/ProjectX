using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ProjectX_V._2
{
    class SaveList
    {
        public static List<ProgrammInfo> list = new List<ProgrammInfo>();

        public void Save(string id)
        {
            
            //try
            //{
            //    var proc = Process.GetProcessById(int.Parse(id));
            //}
            //catch (ArgumentException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
           

            var proc = Process.GetProcessById(int.Parse(id));
            list.Add(new ProgrammInfo()
            {
                Id = id,
                Name = proc.ProcessName,
                TimeStart = proc.StartTime
            });
            Console.WriteLine("Save OK");
        }

        public void AddInSave(string time1, string id)
        {
            DateTime time = DateTime.Now;
            int index = list.FindIndex(x =>string.Equals(x.Id, id, StringComparison.CurrentCultureIgnoreCase));
            list[index].TimeStop = time.ToLocalTime();
            list[index].LongTime = time.ToLocalTime().Subtract(list[index].TimeStart);
            Console.WriteLine("This AddedInSave");
            Console.WriteLine(list[index].ToString());
        }

        public async void StopAndSave()
        {
            Serialization finish = new Serialization();
            await finish.SerializeAsync(list);
        }
    }
}
