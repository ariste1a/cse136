using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using DAL;

namespace BL
{
  public static class BLStudent
  {
    public static void InsertStudent(Student student, ref List<string> errors)
    {
      if (student == null)
      {
        errors.Add("Student cannot be null");
      }
      else if (student.id.Length < 5)
      {
        errors.Add("Invalid student ID");
      }

      if (errors.Count > 0)
        return;

      DALStudent.InsertStudent(student, ref errors);
    }

    public static void UpdateStudent(Student student, ref List<string> errors)
    {
      if (student == null)
      {
        errors.Add("Student cannot be null");
      }

      if (student.id.Length < 5)
      {
        errors.Add("Invalid student ID");
      }

      if (errors.Count > 0)
        return;

      DALStudent.UpdateStudent(student, ref errors);
    }

    public static Student GetStudent(string id, ref List<string> errors)
    {
      if (id == null)
      {
        errors.Add("Invalid student ID");
      }

      // anything else to validate?

      if (errors.Count > 0)
        return null;

      return (DALStudent.GetStudentDetail(id, ref errors));
    }

    public static void DeleteStudent(string id, ref List<string> errors)
    {
      if (id == null)
      {
        errors.Add("Invalid student ID");
      }

      if (errors.Count > 0)
        return;

      DALStudent.DeleteStudent(id, ref errors);
    }

    public static List<Student> GetStudentList(ref List<string> errors)
    {
      return DALStudent.GetStudentList(ref errors);
    }

    public static int EnrollSchedule(string student_id, int schedule_id, ref List<string> errors)
    {
      if (student_id == null)
      {
        errors.Add("Invalid student ID");
      }

      Schedule schedule = DALSchedule.GetSchedule(schedule_id, ref errors);
      Quota quota = DALSchedule.GetQuota(schedule_id.ToString(), ref errors);
      if (quota != null && quota.students_enrolled >= quota.max_students)
          return -1; 
      // anything else to validate?

      if (errors.Count > 0)
          return -1; 

      DALStudent.EnrollSchedule(student_id, schedule_id, ref errors);
      quota = DALSchedule.GetQuota(schedule_id.ToString(), ref errors);
      return quota.students_enrolled;
    }

    public static int DropEnrolledSchedule(string student_id, int schedule_id, ref List<string> errors)
    {
      Schedule schedule = DALSchedule.GetSchedule(schedule_id, ref errors);
      Student student = DALStudent.GetStudentDetail(student_id, ref errors); 
      if (student_id == null || student ==null)
      {
        errors.Add("Invalid student ID");
      }
      if (schedule_id <= 0 || schedule == null)
      {
          errors.Add("Invalid schedule_id"); 
      }            
      // anything else to validate?
      if (errors.Count > 0)
          return -1; 

      DALStudent.DropEnrolledSchedule(student_id, schedule_id, ref errors);      
      Quota quota = DALSchedule.GetQuota(schedule_id.ToString(), ref errors);
      if (quota != null)
      {
          return quota.students_enrolled;
      }
      return 0;
    }
  }
}
