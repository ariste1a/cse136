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
    public static class DALStaff
    {
        static string connection_string = ConfigurationManager.AppSettings["dsn"];

        public static string InsertStaff(Staff staff, ref List<string> errors)
        {
            string idVal = "-1";
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                string strSQL = "spInsertStaff";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;                
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 100));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 50));
                
                mySA.SelectCommand.Parameters["@email"].Value = staff.email;
                mySA.SelectCommand.Parameters["@password"].Value = staff.password;

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
        public static void UpdateStaff(Staff staff, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                string strSQL = "spUpdateStaffInfo";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure; 
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@staff_id", SqlDbType.Int));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 50));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 50));

                mySA.SelectCommand.Parameters["@staff_id"].Value = staff.id; 
                mySA.SelectCommand.Parameters["@email"].Value = staff.email;
                mySA.SelectCommand.Parameters["@password"].Value = staff.password;

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

        public static void DeleteStaff(string id, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);

            try
            {
                string strSQL = "spDeleteStaff";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@staff_id", SqlDbType.VarChar, 20));

                mySA.SelectCommand.Parameters["@staff_id"].Value = id;

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

        public static Staff GetStaffDetail(string email, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            Staff staff = null;

            try
            {
                string strSQL = "spGetStaff";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 50));
                mySA.SelectCommand.Parameters["@email"].Value = email;

                DataSet myDS = new DataSet();
                mySA.Fill(myDS);

                if (myDS.Tables[0].Rows.Count == 0)
                    return null;

                staff = new Staff();
                staff.id = myDS.Tables[0].Rows[0]["staff_id"].ToString();
                staff.email = myDS.Tables[0].Rows[0]["email"].ToString();
                staff.password = myDS.Tables[0].Rows[0]["password"].ToString();
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
            return staff;
        }
    }
}
