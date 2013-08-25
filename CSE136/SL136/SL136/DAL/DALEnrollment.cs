using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;  // must add this...
using System.Configuration; // must add this... make sure to add "System.Configuration" first
using System.Data.SqlClient; // must add this...
using System.Data;
using System.Diagnostics; // must add this...


namespace DAL
{
    public static class DALEnrollment
    {
        static string connection_string = ConfigurationManager.AppSettings["dsn"];

        public static string InsertEnrollment(Enrollment en, ref List<string> errors)
        {
            string idVal = "-1";
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                string strSQL = "spInsertEnrollment";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 100));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.VarChar, 50));

                mySA.SelectCommand.Parameters["@student_id"].Value = en.student_id;
                mySA.SelectCommand.Parameters["@password"].Value = en.schedule_id;

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
        public static void UpdateEnrollment(Enrollment en, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                string strSQL = "spUpdateEnrollmentGrade";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.Int));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.VarChar, 50));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@grade", SqlDbType.VarChar, 50));

                mySA.SelectCommand.Parameters["@student_id"].Value = en.student_id; 
                mySA.SelectCommand.Parameters["@schedule_id"].Value = en.schedule_id;
                mySA.SelectCommand.Parameters["@grade"].Value = en.grade;

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

        public static void DeleteEnrollmentSchedule(string student_id, string schedule_id, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);

            try
            {
                string strSQL = "spDeleteEnrollmentSchedule";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.VarChar, 20));

                mySA.SelectCommand.Parameters["@student_id"].Value = student_id;
                mySA.SelectCommand.Parameters["@schedule_id"].Value = schedule_id;


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

        public static Staff GetEnrollmentGrade(string student_id, string schedule_id, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            Enrollment en = null;

            try
            {
                string strSQL = "spGetEnrollmentGrade";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 50));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.VarChar, 50));
                mySA.SelectCommand.Parameters["@student_id"].Value = student_id;
                mySA.SelectCommand.Parameters["@schedule_id"].Value = schedule_id;

                DataSet myDS = new DataSet();
                mySA.Fill(myDS);

                if (myDS.Tables[0].Rows.Count == 0)
                    return null;

                en = new Enrollment();
                en.student_id = myDS.Tables[0].Rows[0]["student_id"].ToString();
                en.schedule_id = myDS.Tables[0].Rows[0]["schedule_id"].ToString();
                en.grade = myDS.Tables[0].Rows[0]["grade"].ToString();
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
            return en;
        }
    }
}
