using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL;
using System.Text.RegularExpressions;

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

      /*
       * Do regular expressions here*/
    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    Match match = regex.Match(email);
    if (!match.Success)
    {
        errors.Add("Not a valid email address");
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
