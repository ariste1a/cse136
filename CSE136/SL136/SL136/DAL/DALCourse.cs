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
    public static string InsertCourse(Course course, ref List<string> errors)
    {
        string idVal = "-1";
        SqlConnection conn = new SqlConnection(connection_string);
        try
        {
            string strSQL = "spInsertCourse";

            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_title", SqlDbType.VarChar, 100));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_level", SqlDbType.VarChar, 100));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_description", SqlDbType.VarChar, 50));

            CourseLevel level = course.level;
            int levelInt = (int)level;
            mySA.SelectCommand.Parameters["@course_title"].Value = course.title;  
            mySA.SelectCommand.Parameters["@course_level"].Value = levelInt;
            mySA.SelectCommand.Parameters["@course_description"].Value = course.description;

            DataSet myDS = new DataSet();
            mySA.Fill(myDS);
            idVal = myDS.Tables[0].Rows[0]["identity"].ToString();   
        }
        catch (Exception e)
        {
            errors.Add("Error: " + e.ToString());
        }
        finally
        {
            conn.Dispose();
            conn = null;
        }
        return idVal;
    }
    public static Course GetCourse(string id, ref List<string> errors)
    {
        SqlConnection conn = new SqlConnection(connection_string);
        Course course = null; 
        try
        {
            string strSQL = "spGetCourse";

            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);

            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));

            mySA.SelectCommand.Parameters["@course_id"].Value = Convert.ToInt32(id);
            DataSet myDS = new DataSet();
            mySA.Fill(myDS);

            course = new Course();
            if (myDS.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            course.id = myDS.Tables[0].Rows[0]["course_id"].ToString();
            course.title = myDS.Tables[0].Rows[0]["course_title"].ToString();
            course.level = (CourseLevel) Enum.Parse(typeof(CourseLevel), myDS.Tables[0].Rows[0]["course_level"].ToString());
            course.description = myDS.Tables[0].Rows[0]["course_description"].ToString();            
          
        }
        catch (Exception e)
        {
            errors.Add("Error: " + e.ToString());
        }
        finally
        {
            conn.Dispose();
            conn = null;
        }
        return course;
    }
    public static void DeleteCourse(string id, ref List<string> errors)
    {
        SqlConnection conn = new SqlConnection(connection_string);        
        try
        {
            string strSQL = "spDeleteCourse";

            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);

            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));

            mySA.SelectCommand.Parameters["@course_id"].Value = Convert.ToInt32(id);
            DataSet myDS = new DataSet();
            mySA.Fill(myDS);          
          
        }
        catch (Exception e)
        {
            errors.Add("Error: " + e.ToString());
        }
        finally
        {
            conn.Dispose();
            conn = null;
        }        
    }

    public static void UpdateCourse(Course course, ref List<string> errors)
    {
        SqlConnection conn = new SqlConnection(connection_string);
        try
        {
            string strSQL = "spUpdateCourse";

            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_title", SqlDbType.VarChar, 100));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_level", SqlDbType.VarChar, 100));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_description", SqlDbType.VarChar, 50));


            CourseLevel level = course.level;
            int levelInt = (int)level;
            mySA.SelectCommand.Parameters["@course_id"].Value = course.id;
            mySA.SelectCommand.Parameters["@course_title"].Value = course.title;
            mySA.SelectCommand.Parameters["@course_level"].Value = levelInt;
            mySA.SelectCommand.Parameters["@course_description"].Value = course.description;

            DataSet myDS = new DataSet();
            mySA.Fill(myDS);

        }
        catch (Exception e)
        {
            errors.Add("Error: " + e.ToString());
        }
        finally
        {
            conn.Dispose();
            conn = null;
        }
    }
  }
}
