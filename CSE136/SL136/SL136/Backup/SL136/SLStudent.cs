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
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SLStudent" in both code and config file together.
  public class SLStudent : ISLStudent
  {
    public Student GetStudent(string id, ref List<string> errors)
    {
      return BLStudent.GetStudent(id, ref errors);
    }

    public void InsertStudent(Student student, ref List<string> errors)
    {
      BLStudent.InsertStudent(student, ref errors);
    }

    public void UpdateStudent(Student student, ref List<string> errors)
    {
      BLStudent.UpdateStudent(student, ref errors);
    }

    public void DeleteStudent(string id, ref List<string> errors)
    {
      BLStudent.DeleteStudent(id, ref errors);
    }

    public List<Student> GetStudentList(ref List<string> errors)
    {
      return BLStudent.GetStudentList(ref errors);
    }

    public void EnrollSchedule(string student_id, int schedule_id, ref List<string> errors)
    {
      BLStudent.EnrollSchedule(student_id, schedule_id, ref errors);
    }

    public void DropEnrolledSchedule(string student_id, int schedule_id, ref List<string> errors)
    {
      BLStudent.DropEnrolledSchedule(student_id, schedule_id, ref errors);
    }
  }
}
