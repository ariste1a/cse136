using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web136.Models.Course
{
  public static class CourseClientService
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

    private static SLCourse.Course PL_to_DTO(PLCourse p)
    {
        SLCourse.Course c = new SLCourse.Course();
        c.description = p.description;
        c.title = p.title;

        return c; 
    } 


    public static void InsertCourse(PLCourse p)
    {
        //SLCourse.SLCourseClient client = new SLCourse.SLCourseClient();
        String[] errors = new string[0];
      
        SLCourse.Course course = PL_to_DTO(p);
        course.title = "asdf";
        course.description = "asdf";
        course.level = SLCourse.CourseLevel.lower;        

        SLCourse.ISLCourse client = new SLCourse.SLCourseClient();        
        SLCourse.InsertCourseRequest request = new SLCourse.InsertCourseRequest(course, errors);
        client.InsertCourse(request);
    }  
  }
}