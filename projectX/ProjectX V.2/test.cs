using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectX_V._2
{
    static class test
    {
        private static ProgrammInfo ProgrammInfo;

        public static async void Des()
        {
            string v = @"C:\it-academy\ArtemShnip\ProjectX\projectX\ProjectX V.2\bin\Debug\netcoreapp3.0\save2.json";
            string json = File.ReadAllText(@"C:\it-academy\ArtemShnip\ProjectX\projectX\ProjectX V.2\bin\Debug\netcoreapp3.0\save2.json").ToString();
            List<ProgrammInfo> list = new List<ProgrammInfo>();

            ProgrammInfo = JsonSerializer.Deserialize<ProgrammInfo>(json);


        }

        //public static System.Threading.Tasks.ValueTask<TValue> DeserializeAsync<TValue>(System.IO.Stream utf8Json, System.Text.Json.JsonSerializerOptions options = default, System.Threading.CancellationToken cancellationToken = default);
    }
}
