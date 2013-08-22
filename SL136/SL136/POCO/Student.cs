using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; // this is required

namespace POCO
{
  [DataContract]
  public class Student
  {
    [DataMember]
    public string id { get; set; }

    [DataMember]
    public string ssn { get; set; }

    [DataMember]
    public string first_name { get; set; }

    [DataMember]
    public string last_name { get; set; }

    [DataMember]
    public string email { get; set; }

    [DataMember]
    public string password { get; set; }

    [DataMember]
    public float shoe_size { get; set; }

    [DataMember]
    public int weight { get; set; }

    [DataMember]
    public List<Schedule> enrolled;

  }
}
