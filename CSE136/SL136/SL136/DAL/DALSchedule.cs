using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;  // must add this...
using System.Configuration; // must add this... make sure to add "System.Configuration" first
using System.Data.SqlClient; // must add this...
using System.Data; // must add this...

namespace DAL
{
  public static class DALSchedule
  {
    static string connection_string = ConfigurationManager.AppSettings["dsn"];

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
