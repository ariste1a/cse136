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

  }
}
