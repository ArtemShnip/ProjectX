using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfProjectX.ProgramModels;

namespace WpfProjectX.Services
{
    class FileIOService
    {
        private readonly string _path;

        public FileIOService(string path)
        {
            _path = path;
        }
        public BindingList<ProgramModel> LoadDate()
        {
            var fileExists = File.Exists(_path);
            if (!fileExists)
            {
                File.CreateText(_path).Dispose();
                return new BindingList<ProgramModel>();
            }
            using (var reader = File.OpenText(_path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ProgramModel>>(fileText);
            }
        }
        public void SaveDate(object programModelsList)
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                string output = JsonConvert.SerializeObject(programModelsList);
                writer.Write(output);
            }
        }
    }
}
