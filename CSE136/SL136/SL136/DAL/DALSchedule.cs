using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;  // must add this...
using System.Configuration; // must add this... make sure to add "System.Configuration" first
using System.Data.SqlClient; // must add this...
using System.Data; // must add this...
using System.Diagnostics;

namespace DAL
{
  public static class DALSchedule
  {
    static string connection_string = ConfigurationManager.AppSettings["dsn"];

    public static Boolean deleteScheduleDay(string day_id)
    {
        SqlConnection conn = new SqlConnection(connection_string);
        try
        {
            string strSQL = "deleteScheduleDay";
            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters["@schedule_day_id"].Value = day_id;
            DataSet myDS = new DataSet();
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.Fill(myDS);
 
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
            return false;
        }
        conn.Dispose();
        conn = null;
        return true;
    }
    public static Boolean deleteScheduleTime(string time_id)
    {
        SqlConnection conn = new SqlConnection(connection_string);
        try
        {
            string strSQL = "deleteScheduleTime";
            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters["@schedule_time_id"].Value = time_id;
            DataSet myDS = new DataSet();
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.Fill(myDS);

        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
            return false;
        }
        conn.Dispose();
        conn = null;
        return true;
    }

    public static Boolean deleteStudentSchedule(string student_id, string schedule_id)
    {
        SqlConnection conn = new SqlConnection(connection_string);
        try
        {
            string strSQL = "deleteStudentSchedule";
            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters["@student_id"].Value = student_id;
            mySA.SelectCommand.Parameters["@schedule_id"].Value = schedule_id;
            DataSet myDS = new DataSet();            
            mySA.Fill(myDS);

        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
            return false;
        }
        conn.Dispose();
        conn = null;
        return true;
    }


    public static Quota GetQuota(string class_id, ref List<string> errors)
    {
        SqlConnection conn = new SqlConnection(connection_string);
        Quota q = null;

        try
        {
            string strSQL = "spGetQuota";

            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@class_id", SqlDbType.VarChar, 50));
            mySA.SelectCommand.Parameters["@class_id"].Value = class_id;

            DataSet myDS = new DataSet();
            mySA.Fill(myDS);

            if (myDS.Tables[0].Rows.Count == 0)
                return null;

            q = new Quota();
            q.schedule_id = myDS.Tables[0].Rows[0]["schedule_id"].ToString();
            q.course_title = myDS.Tables[0].Rows[0]["course_title"].ToString();
            q.students_enrolled = myDS.Tables[0].Rows[0]["students_enrolled"].ToString();
            q.max_students = myDS.Tables[0].Rows[0]["max_students"].ToString(); 
        }
        catch (Exception e)
        {
            errors.Add("Error: " + e.ToString());
            Debug.WriteLine(e.ToString());
        }
        finally
        {
            conn.Dispose();
            conn = null;
        }
        return q;
    }


    public static Schedule GetSchedule(int schedule_id, ref List<string> errors)
    {
      SqlConnection conn = new SqlConnection(connection_string);     
      Schedule schedule = new Schedule(); 
      try
      {
        string strSQL = "spGetScheduleList";
        SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
        mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
        mySA.SelectCommand.Parameters.Add(new SqlParameter("@identity", SqlDbType.Int));
        mySA.SelectCommand.Parameters["@identity"].Value = schedule_id;
                
        DataSet myDS = new DataSet();
        mySA.Fill(myDS);


        if (myDS.Tables[0].Rows.Count == 0)
            return null;

        schedule.id = Convert.ToInt32(myDS.Tables[0].Rows[0]["schedule_id"].ToString());
        schedule.year = myDS.Tables[0].Rows[0]["year"].ToString();
        schedule.quarter = myDS.Tables[0].Rows[0]["quarter"].ToString();
        schedule.session = myDS.Tables[0].Rows[0]["session"].ToString();
        schedule.quota = myDS.Tables[0].Rows[0]["quota"].ToString();
        schedule.time = myDS.Tables[0].Rows[0]["schedule_time"].ToString();
        schedule.day = myDS.Tables[0].Rows[0]["schedule_day"].ToString();
        schedule.instructor = myDS.Tables[0].Rows[0]["instructor_id"].ToString();
        schedule.type = myDS.Tables[0].Rows[0]["type"].ToString();
        schedule.enrollments = myDS.Tables[0].Rows[0]["enrollments"].ToString(); 
        schedule.course =
          new Course
          {
            id = myDS.Tables[0].Rows[0]["course_id"].ToString(),
            title = myDS.Tables[0].Rows[0]["course_title"].ToString(),
            description = myDS.Tables[0].Rows[0]["course_description"].ToString(),
          };

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

      return schedule;
    }    

    public static void UpdateSchedule(Schedule s, ref List<string> errors)
    {
        SqlConnection conn = new SqlConnection(connection_string);
        try
        {
            string strSQL = "spUpdateCourseSchedule";

            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@session", SqlDbType.VarChar, 50));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@quota", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@type", SqlDbType.VarChar, 2));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 50));

            mySA.SelectCommand.Parameters["@schedule_id"].Value = Convert.ToInt32(s.id);
            mySA.SelectCommand.Parameters["@course_id"].Value = Convert.ToInt32(s.course.id);
            mySA.SelectCommand.Parameters["@year"].Value = Convert.ToInt32(s.year);
            mySA.SelectCommand.Parameters["@session"].Value = s.session;
            mySA.SelectCommand.Parameters["@schedule_day_id"].Value = Convert.ToInt32(s.day);
            mySA.SelectCommand.Parameters["@schedule_time_id"].Value = Convert.ToInt32(s.time);
            mySA.SelectCommand.Parameters["@instructor_id"].Value = Convert.ToInt32(s.instructor);
            mySA.SelectCommand.Parameters["@quota"].Value = Convert.ToInt32(s.quota);
            mySA.SelectCommand.Parameters["@type"].Value = s.type;
            mySA.SelectCommand.Parameters["@quarter"].Value = s.quarter;       

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


    public static int InsertSchedule(Schedule schedule, ref List<string> errors)
    {
        int idVal = -1;
        SqlConnection conn = new SqlConnection(connection_string);        
        try
        {
            string strSQL = "spInsertSchedule";
            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;            
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@session", SqlDbType.VarChar, 50));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));  
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));  
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@quota", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@type", SqlDbType.VarChar, 2));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 50));  

            mySA.SelectCommand.Parameters["@course_id"].Value = Convert.ToInt32(schedule.course.id);
            mySA.SelectCommand.Parameters["@year"].Value = Convert.ToInt32(schedule.year);
            mySA.SelectCommand.Parameters["@session"].Value = schedule.session;
            mySA.SelectCommand.Parameters["@schedule_day_id"].Value = Convert.ToInt32(schedule.day);
            mySA.SelectCommand.Parameters["@schedule_time_id"].Value = Convert.ToInt32(schedule.time);
            mySA.SelectCommand.Parameters["@instructor_id"].Value = Convert.ToInt32(schedule.instructor);
            mySA.SelectCommand.Parameters["@quota"].Value = Convert.ToInt32(schedule.quota);
            mySA.SelectCommand.Parameters["@type"].Value = schedule.type;
            mySA.SelectCommand.Parameters["@quarter"].Value = schedule.quarter;            

            DataSet myDS = new DataSet();
            mySA.Fill(myDS);
            idVal = Convert.ToInt32(myDS.Tables[0].Rows[0]["identity"].ToString()); 
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

    public static void DeleteSchedule(int id, ref List<string> errors)
    {
        SqlConnection conn = new SqlConnection(connection_string);        
        try
        {
            string strSQL = "spDeleteCourseSchedule";
            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters["@schedule_id"].Value = Convert.ToInt32(id);      
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

    public static List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
    {
      SqlConnection conn = new SqlConnection(connection_string);
      List<Schedule> scheduleList = new List<Schedule>();

      try
      {
        string strSQL = "spGetScheduleList";

        SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);

        if (year.Length > 0)
        {
          mySA.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
          mySA.SelectCommand.Parameters["@year"].Value = year;
        }

        if (quarter.Length > 0)
        {
          mySA.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 25));
          mySA.SelectCommand.Parameters["@quarter"].Value = quarter;
        }

        mySA.SelectCommand.CommandType = CommandType.StoredProcedure;

        DataSet myDS = new DataSet();
        mySA.Fill(myDS);

        if (myDS.Tables[0].Rows.Count == 0)
          return null;

        for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
        {
          Schedule schedule = new Schedule();
          schedule.id = Convert.ToInt32(myDS.Tables[0].Rows[i]["schedule_id"].ToString());
          schedule.year = myDS.Tables[0].Rows[i]["year"].ToString();
          schedule.quarter = myDS.Tables[0].Rows[i]["quarter"].ToString();
          schedule.session = myDS.Tables[0].Rows[i]["session"].ToString();
          schedule.quota = myDS.Tables[0].Rows[i]["quota"].ToString();
          schedule.time = myDS.Tables[0].Rows[i]["schedule_time"].ToString();
          schedule.day = myDS.Tables[0].Rows[i]["schedule_day"].ToString();
          schedule.type = myDS.Tables[0].Rows[i]["type"].ToString();
          schedule.enrollments = myDS.Tables[0].Rows[i]["enrollments"].ToString(); 
          schedule.course =
            new Course
            {
              id = myDS.Tables[0].Rows[i]["course_id"].ToString(),
              title = myDS.Tables[0].Rows[i]["course_title"].ToString(),
              description = myDS.Tables[0].Rows[i]["course_description"].ToString(),
            };
          scheduleList.Add(schedule);
        }
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

      return scheduleList;
    }
  }
}
