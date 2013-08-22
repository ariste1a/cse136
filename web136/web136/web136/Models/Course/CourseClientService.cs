using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web136.Models.Course
{
  public class CourseClientService
  {
    public static List<PLCourse> GetCourseList()
    {
      List<PLCourse> courseList = new List<PLCourse>();

      SLCourse.SLCourseClient client = new SLCourse.SLCourseClient();
      SLCourse.Course[] coursesLoaded = client.GetCourseList();

      if (coursesLoaded != null)
      {
        foreach (SLCourse.Course s in coursesLoaded)
        {
          PLCourse Course = DTO_to_PL(s);
          courseList.Add(Course);
        }
      }
      return courseList;
    }

    private static PLCourse DTO_to_PL(SLCourse.Course s)
    {
      PLCourse myCourse = new PLCourse();

      myCourse.id = s.id;
      myCourse.title = s.title;
      myCourse.description = s.description;

      return myCourse;
    }
  }
}