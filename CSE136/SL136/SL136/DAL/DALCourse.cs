using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;  // must add this...
using System.Configuration; // must add this... make sure to add "System.Configuration" first
using System.Data.SqlClient; // must add this...
using System.Data; // must add this...

namespace DAL
{
  public class DALCourse
  {
    static string connection_string = ConfigurationManager.AppSettings["dsn"];
    public static List<Course> GetCourseList()
    { 
      SqlConnection conn = new SqlConnection(connection_string);
      List<Course> courseList = new List<Course>();

      try
      {
        string strSQL = "spGetCourseList";

        SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);

        mySA.SelectCommand.CommandType = CommandType.StoredProcedure;

        DataSet myDS = new DataSet();
        mySA.Fill(myDS);

        if (myDS.Tables[0].Rows.Count == 0)
          return null;

        for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
        {
          Course course = new Course();
          course.id = myDS.Tables[0].Rows[i]["course_id"].ToString();
          course.title = myDS.Tables[0].Rows[i]["course_title"].ToString();
          course.level = (CourseLevel) Enum.Parse(typeof(CourseLevel), myDS.Tables[0].Rows[i]["course_level"].ToString());
          course.description = myDS.Tables[0].Rows[i]["course_description"].ToString();
          courseList.Add(course);
        }
      }
      catch (Exception e)
      {
      }
      finally
      {
        conn.Dispose();
        conn = null;
      }

      return courseList;

    }
  }
}
