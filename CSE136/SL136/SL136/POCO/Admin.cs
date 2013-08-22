using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; // this is required

namespace POCO
{   [DataContract]
    public class Admin
    {
    [DataMember]
    public int id { get; set; }

    [DataMember]
    public string email { get; set; }

    [DataMember]
    public string password { get; set; }

    }
}