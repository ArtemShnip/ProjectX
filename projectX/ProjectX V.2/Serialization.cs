﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjectX_V._2
{
    class Serialization
    {
        public async System.Threading.Tasks.Task SerializeAsync(List<ProgrammInfo> list)
        {
            using (FileStream fs = File.OpenWrite("save2.json"))
            {
                var options = new JsonSerializerOptions {
                    WriteIndented = true
                };
                await JsonSerializer.SerializeAsync(fs, list, options);
            }
        }
    }
}
