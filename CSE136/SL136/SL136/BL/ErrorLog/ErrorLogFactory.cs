using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // must add this... make sure to add "System.Configuration" first

namespace BL.ErrorLog
{
  public class ErrorLogFactory
  {

    static string logDestination = ConfigurationManager.AppSettings["logDestination"];
    public IErrorLog logInstance = null;

    public IErrorLog GetErrorLogInstance()
    {
      switch (logDestination)
      {
        case "file":
          logInstance = new LogToFile();
          break;
        case "db":
          logInstance = new LogToDB();
          break;
        default:
          break;
      }
      return logInstance;
    }
  }
}
