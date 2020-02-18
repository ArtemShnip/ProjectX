using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
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

        public void SaveArrayProgram(string name)
        {
            string pathArray = $"{Environment.CurrentDirectory}\\SaveData\\programDataArray.xml";
            string[] arrayProgram;

            Type type = typeof(string[]);
            string[] retVal;

            XmlSerializer formatter = new XmlSerializer(type);

            using (var stream = new FileStream(pathArray, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                retVal = (string[])formatter.Deserialize(stream);
            }
            arrayProgram = retVal;
            string[] newArrayProgram = new string[arrayProgram.Length + 1];
            for (int i = 0; i < arrayProgram.Length; i++)
            {
                newArrayProgram[i] = arrayProgram[i];
                if (i==arrayProgram.Length-1)
                {
                    newArrayProgram[i + 1] = name;
                }
            }
            newArrayProgram[newArrayProgram.Length-1] = name;
 
            using (var stream = new FileStream(pathArray, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                formatter.Serialize(stream, newArrayProgram);
            }
        }
    }
}
