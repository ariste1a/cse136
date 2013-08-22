using System;
using System.Collections.Generic;
using POCO;  // must add this...
using System.Configuration; // must add this... make sure to add "System.Configuration" first
using System.Data.SqlClient; // must add this...
using System.Data; // must add this...
using System.Diagnostics;

namespace DAL
{
    public static class DALDiscussion
    {
        static string connection_string = ConfigurationManager.AppSettings["dsn"];

        public static List<Schedule> GetDiscussions(int class_id, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            List<Schedule> scheduleList = new List<Schedule>();
            try
            {
                string strSQL = "spGetDiscussions";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);

                mySA.SelectCommand.Parameters.Add(new SqlParameter("@class_id", SqlDbType.Int));
                mySA.SelectCommand.Parameters["@class_id"].Value = class_id;
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataSet myDS = new DataSet();
                mySA.Fill(myDS);

                if (myDS.Tables[0].Rows.Count == 0)
                    return null;

                List<Schedule> schedules = new List<Schedule>();
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
                    schedules.Add(schedule);

                }
                return schedules;
            }
            catch (Exception e)
            {
                errors.Add(e.ToString());
                return null;
            }
        } // method

        public static Boolean createDiscussion(int lecture_id, int discussion_id, ref List<String> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                string strSQL = "spInsertDiscussion";
                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@lecture_id", SqlDbType.Int));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@discussion_id", SqlDbType.Int));
                mySA.SelectCommand.Parameters["@lecture_id"].Value = lecture_id;
                mySA.SelectCommand.Parameters["@discussion_id"].Value = discussion_id;
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet myDS = new DataSet();
                mySA.Fill(myDS);
                return true;
            }
            catch (Exception e)
            {
                errors.Add(e.ToString());
                return false;
            }
        }

    public static Boolean removeDiscussion(int lecture_id, int discussion_id, ref List<String> errors) 
    {
        SqlConnection conn = new SqlConnection(connection_string);
        try
        {
            string strSQL = "spDeleteDiscussion";
            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@lecture_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@discussion_id", SqlDbType.Int));
            mySA.SelectCommand.Parameters["@lecture_id"].Value = lecture_id;
            mySA.SelectCommand.Parameters["@discussion_id"].Value = discussion_id;
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet myDS = new DataSet();
            mySA.Fill(myDS);
            return true;
        }
        catch (Exception e)
        {
            errors.Add(e.ToString());
            return false;
        }
    }
    

    } // class
} // namespace
