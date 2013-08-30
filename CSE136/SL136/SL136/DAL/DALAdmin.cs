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
    public class DALAdmin
    {
        static string connection_string = ConfigurationManager.AppSettings["dsn"];
        //why do we need a static here?
        public static string InsertAdmin(Admin admin, ref List<string> errors)
        {
          string idVal = "-1";
          SqlConnection conn = new SqlConnection(connection_string);
          try
          {
            string strSQL = "spInsertAdmin";

            SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
            mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 20));
            mySA.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 9));            

            mySA.SelectCommand.Parameters["@email"].Value = admin.email;
            mySA.SelectCommand.Parameters["@password"].Value = admin.password;
            DataSet myDS = new DataSet();
            mySA.Fill(myDS);
            int count = myDS.Tables[0].Rows.Count;
            idVal = myDS.Tables[0].Rows[0]["identity"].ToString();            
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
            Admin admin = null;

            try
            {
                string strSQL = "spGetAdmin";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 100));
                mySA.SelectCommand.Parameters["@email"].Value = email;

                DataSet myDS = new DataSet();
                mySA.Fill(myDS);

                if (myDS.Tables[0].Rows.Count == 0)
                    return null;

                admin = new Admin();
                admin.id = myDS.Tables[0].Rows[0]["admin_id"].ToString();
                admin.email = myDS.Tables[0].Rows[0]["email"].ToString();
                admin.password = myDS.Tables[0].Rows[0]["password"].ToString();
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

            return admin;
        }

        public static void DeleteAdmin(string id, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);

            try
            {
                string strSQL = "spDeleteAdmin";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@admin_id", SqlDbType.VarChar, 20));

                mySA.SelectCommand.Parameters["@admin_id"].Value = id;

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

        public static void UpdateAdmin(Admin a, ref List<string> errors)
        {
            SqlConnection conn = new SqlConnection(connection_string);

            try
            {
                string strSQL = "spUpdateAdminInfo";

                SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
                mySA.SelectCommand.CommandType = CommandType.StoredProcedure;

                mySA.SelectCommand.Parameters.Add(new SqlParameter("@admin_id", SqlDbType.VarChar, 20));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 50));
                mySA.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 50));

                mySA.SelectCommand.Parameters["@admin_id"].Value = a.id;
                mySA.SelectCommand.Parameters["@email"].Value = a.email;
                mySA.SelectCommand.Parameters["@password"].Value = a.password;

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




        //might want to return success code or not
  }    
}
