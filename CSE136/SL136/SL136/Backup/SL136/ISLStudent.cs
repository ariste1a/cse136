using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POCO; // this is required
using BL; // this is required

namespace SL136
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
  [ServiceContract]
  public interface ISLStudent
  {

    [OperationContract]
    Student GetStudent(string id, ref List<string> errors);

    [OperationContract]
    void InsertStudent(Student student, ref List<string> errors);

    [OperationContract]
    void UpdateStudent(Student student, ref List<string> errors);

    [OperationContract]
    void DeleteStudent(string id, ref List<string> errors);

    [OperationContract]
    List<Student> GetStudentList(ref List<string> errors);

    [OperationContract]
    void EnrollSchedule(string student_id, int schedule_id, ref List<string> errors);

    [OperationContract]
    void DropEnrolledSchedule(string student_id, int schedule_id, ref List<string> errors);
  }
}
