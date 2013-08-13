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
    public int id { get; set; }

    [DataMember]
    public string year { get; set; }

    [DataMember]
    public string quarter { get; set; }

    [DataMember]
    public string session { get; set; }

    [DataMember]
    public Course course { get; set; }

  }
}
