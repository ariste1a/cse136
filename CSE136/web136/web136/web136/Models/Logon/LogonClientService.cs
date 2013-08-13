using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web136.Models.Logon
{
  public static class LogonClientService
  {
    public static PLLogon Validate(string email, string password)
    {
      SLAuth.ISLAuth client = new SLAuth.SLAuthClient();
      string[] errors = new string[0];

      SLAuth.AuthenticateRequest request = new SLAuth.AuthenticateRequest(email, password, errors);

      SLAuth.AuthenticateResponse response = client.Authenticate(request);
      PLLogon PLLogon = DTO_to_PL(response.AuthenticateResult);
      return PLLogon;
    }

    private static PLLogon DTO_to_PL(SLAuth.Logon logon)
    {
      PLLogon PLLogon = new PLLogon();
      PLLogon.ID = logon.id;
      PLLogon.Role = logon.role;

      return PLLogon;
    }
  }
}