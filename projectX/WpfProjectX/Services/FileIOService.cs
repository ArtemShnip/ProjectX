using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
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
        public ObservableCollection<ProgramModel> LoadDate()
        {
            var fileExists = File.Exists(_path);
            if (!fileExists)
            {
                File.CreateText(_path).Dispose();
                return new ObservableCollection<ProgramModel>();
            }
            using (var reader = File.OpenText(_path))
            {
                var fileText = reader.ReadToEnd();
                if (fileText != "")
                {
                    return JsonConvert.DeserializeObject<ObservableCollection<ProgramModel>>(fileText);
                }
                else
                {
                    return new ObservableCollection<ProgramModel>();
                }
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
