using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL;

namespace BL
{
  public class BLCourse
  {    
    public static List<Course> GetCourseList()
    {
      return (DALCourse.GetCourseList());
    }
    public static string InsertCourse(Course course, ref List<string> errors)
    {
        string id = DALCourse.InsertCourse(course, ref errors);
        if (id != null)
        {
            return id; 
        }
        return "-1";
    }
    public static Course GetCourse(string id, ref List<string> errors)
    {
        Course course = DALCourse.GetCourse(id, ref errors);
        return course; 
    }
    public static void DeleteCourse(string id, ref List<string> errors)
    {
        DALCourse.DeleteCourse(id, ref errors); 
    }
    public static void UpdateCourse(Course course, ref List<string> errors)
    {
        DALCourse.UpdateCourse(course, ref errors);
    }    
  }
}
