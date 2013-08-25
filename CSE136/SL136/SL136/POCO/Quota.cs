using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; // this is required

namespace POCO
{
    [DataContract]
    public class Quota
    {
        [DataMember]
        public string schedule_id { get; set; }

        [DataMember]
        public string course_title { get; set; }

        [DataMember]
        public string students_enrolled { get; set; }

        [DataMember]
        public string max_students { get; set; }

    }
}