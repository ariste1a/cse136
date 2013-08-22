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
  public static class DALAuth
  {
    static string connection_string = ConfigurationManager.AppSettings["dsn"];

    public static Logon Authenticate(string email, string password, ref List<string> errors)
    {
      Logon logon = new Logon();
      logon.id = "";
      logon.role = "invalid";

      SqlConnection conn = new SqlConnection(connection_string);
      try
      {
        string strSQL = "spGetLoginInfo";

        SqlDataAdapter mySA = new SqlDataAdapter(strSQL, conn);
        mySA.SelectCommand.CommandType = CommandType.StoredProcedure;
        mySA.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 64));
        mySA.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 64));

        mySA.SelectCommand.Parameters["@email"].Value = email;
        mySA.SelectCommand.Parameters["@password"].Value = password;

        DataSet myDS = new DataSet();
        mySA.Fill(myDS);

        if (myDS.Tables[0].Rows.Count > 0)
        {
          logon.role = myDS.Tables[0].Rows[0]["role"].ToString();
          logon.id = myDS.Tables[0].Rows[0]["id"].ToString();
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

      return logon;
    }
  }
}
