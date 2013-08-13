using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; // this is required

namespace POCO
{
  [DataContract]
  public class Logon
  {
    [DataMember]
    public string id { get; set; }

    [DataMember]
    public string role { get; set; }
  }
}
