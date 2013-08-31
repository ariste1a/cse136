using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POCO;
using BL;
namespace SL136
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISLCourse" in both code and config file together.
  [ServiceContract]
  public interface ISLCourse
  {
    [OperationContract]
    List<Course> GetCourseList();

    [OperationContract]
    string InsertCourse(Course course, ref List<string> errors);

    [OperationContract]
    Course GetCourse(string id, ref List<string> errors);

    [OperationContract]
    void DeleteCourse(string id, ref List<string> errors);

    [OperationContract]
    void UpdateCourse(Course course, ref List<string> errors);

  }
  
}
