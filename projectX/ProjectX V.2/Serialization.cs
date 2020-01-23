﻿ using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjectX_V._2
{
    class Serialization
    {
        public async System.Threading.Tasks.Task SerializeAsync(List<ProgrammInfo> list)
        {
            using (FileStream fs = File.Create("save2.json"))   // sreamwriter
            {
                await JsonSerializer.SerializeAsync(fs, list);
            }
        }
    }
}
