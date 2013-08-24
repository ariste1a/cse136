using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; // this is required

namespace POCO
{
  [DataContract]
  public class Course
  {
    [DataMember]
    public string id { get; set; }

    [DataMember]
    public string title { get; set; }

    [DataMember]
    public CourseLevel level { get; set; }

    [DataMember]
    public string description { get; set; }

  }
}
