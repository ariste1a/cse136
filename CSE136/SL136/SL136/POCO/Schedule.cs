using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; // this is required

namespace POCO
{
    [DataContract]
    public class Schedule
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string year { get; set; }

        [DataMember]
        public string quarter { get; set; }

        [DataMember]
        public string session { get; set; }

        [DataMember]
        public Course course { get; set; }

        [DataMember]
        public string enrollments { get; set; }

        [DataMember]
        public string quota { get; set; }

        [DataMember]
        public string time { get; set; }

        [DataMember]
        public string instructor { get; set; }

        [DataMember]
        public string day { get; set; }

        [DataMember]
        public string type { get; set; }

    }
}