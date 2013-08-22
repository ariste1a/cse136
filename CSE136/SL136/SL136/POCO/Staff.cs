using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; // this is required

namespace POCO
{
    [DataContract]
    public class Staff
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string password { get; set; }

    }
}
