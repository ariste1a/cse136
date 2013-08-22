using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POCO;

namespace SL136
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISLAuth" in both code and config file together.
  [ServiceContract]
  public interface ISLAuth
  {
    [OperationContract]
    Logon Authenticate(string email, string password, ref List<string> errors);
  }
}
