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
    class DALAdmin
    {
        static string connection_string = ConfigurationManager.AppSettings["dsn"];
        public int InsertAdmin(Admin admin, ref List<string> errors)
        {
          int idVal = -1;
             SqlConnection conn = new SqlConnection(connection_string);
          try
          {
            string strSQL = "spInsertAdmin";

            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 20));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 9));
            mySA.SelectCommand.Parameters.Add("@@IDENTITY", SqlDbType.Int);

            mySA.SelectCommand.Parameters["@email"].Value = admin.email;
            mySA.SelectCommand.Parameters["@password"].Value = admin.password;
            DataSet myDS = new DataSet();
            mySA.Fill(myDS);
            idVal =  Convert.ToInt32(myDS.Tables[1].Rows[1]["identity"].ToString());            
            //how do we get the ID returned value?!
             //should be returned by the procedure, just like a select * does. 
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
        public static Admin GetAdminDetail(string email, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            Student student = null;

            try
            {
                string strSQL = "spGetStudentInfo";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 100));

                mySA.SelectCommand.Parameters["@student_id"].Value = email;

                DataSet myDS = new DataSet();
                mySA.Fill(myDS);

                if (myDS.Tables[0].Rows.Count == 0)
                    return null;

                student = new Student();
                student.id = myDS.Tables[0].Rows[0]["student_id"].ToString();
                student.first_name = myDS.Tables[0].Rows[0]["first_name"].ToString();
                student.last_name = myDS.Tables[0].Rows[0]["last_name"].ToString();
                student.ssn = myDS.Tables[0].Rows[0]["ssn"].ToString();
                student.email = myDS.Tables[0].Rows[0]["email"].ToString();
                student.password = myDS.Tables[0].Rows[0]["password"].ToString();
                student.shoe_size = (float)Convert.ToDouble(myDS.Tables[0].Rows[0]["shoe_size"].ToString());
                student.weight = Convert.ToInt32(myDS.Tables[0].Rows[0]["weight"].ToString());

                if (myDS.Tables[1] != null)
                {
                    student.enrolled = new List<Schedule>();
                    for (int i = 0; i < myDS.Tables[1].Rows.Count; i++)
                    {
                        Schedule schedule = new Schedule();
                        Course course = new Course();
                        course.id = myDS.Tables[1].Rows[i]["course_id"].ToString();
                        course.title = myDS.Tables[1].Rows[i]["course_title"].ToString();
                        course.description = myDS.Tables[1].Rows[i]["course_description"].ToString();
                        schedule.course = course;

                        schedule.quarter = myDS.Tables[1].Rows[i]["quarter"].ToString();
                        schedule.year = myDS.Tables[1].Rows[i]["year"].ToString();
                        schedule.session = myDS.Tables[1].Rows[i]["session"].ToString();
                        schedule.id = Convert.ToInt32(myDS.Tables[1].Rows[i]["schedule_id"].ToString());
                        student.enrolled.Add(schedule);
                    }
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

            return student;
        }
  }    
}
