using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; // this is required

namespace POCO
{
    [DataContract]
    public class Enrollment
    {
        [DataMember]
        public string student_id { get; set; }

        [DataMember]
        public string schedule_id { get; set; }
        
        [DataMember]
        public string grade { get; set; }

    }
}
