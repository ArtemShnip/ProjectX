using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ProjectX_V._2
{
    [DataContract]
    class ProgrammInfo
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string TimeStart { get; set; }

        [DataMember]
        public string TimeStop { get; set; }

        [DataMember]
        public string LongTime { get; set; }

        public override string ToString()
        {
            return $"\n ID - {Id}\n name start - {Name}\n  time start - {TimeStart}\n time stop - {TimeStop}\n sekundomer - {LongTime}";
        }
    }
}
