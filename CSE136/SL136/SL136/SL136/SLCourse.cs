using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;
using POCO;

namespace SL136
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SLCourse" in both code and config file together.
  public class SLCourse : ISLCourse
  {
    public List<Course> GetCourseList()
    {
      return BLCourse.GetCourseList();
    }

    public static string InsertCourse(Course course, ref List<string> errors)
    {
      return BLCourse.InsertCourse(course, ref errors);
    }

    public static Course GetCourse(string id, ref List<string> errors)
    {
        Course course = BLCourse.GetCourse(id, ref errors);
        return course;
    }

    public static void DeleteCourse(string id, ref List<string> errors)
    {
        BLCourse.DeleteCourse(id, ref errors);
    }

    public static void UpdateCourse(Course course, ref List<string> errors)
    {
        BLCourse.UpdateCourse(course, ref errors);
    }
  }
}
