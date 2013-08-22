using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL;

namespace BL
{
  public class BLAuth
  {
    public static Logon Authenticate(string email, string password, ref List<string> errors)
    {
      if (email == null)
      {
        errors.Add("Email cannot be null");
      }

      if (password == null)
      {
        errors.Add("Password cannot be null");
      }

      if (errors.Count > 0)
      {
        ErrorLog.AsynchLog.LogNow(errors);
        return null;
      }

      return DALAuth.Authenticate(email, password, ref errors);
    }

  }
}
