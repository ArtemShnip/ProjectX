using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX_V._2
{
    class ProgrammInfo
    {
        public string Id { get; set; }

        public string NameStart { get; set; }

        public string NameStop { get; set; }

        public string TimeStart { get; set; }

        public string TimeStop { get; set; }

        public string LongTime { get; set; }

        public override string ToString()
        {
            return $"\n ID - {Id}\n name start - {NameStart}\n name stop - {NameStop}\n time start - {TimeStart}\n time stop - {TimeStop}\n sekundomer - {LongTime}";
        }
    }
}
