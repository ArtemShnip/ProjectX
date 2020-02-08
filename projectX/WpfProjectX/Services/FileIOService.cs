using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            return null;
        }

        public void SaveDate(BindingList<ProgramModel> _programModelsList)
        {

        }
    }
}
