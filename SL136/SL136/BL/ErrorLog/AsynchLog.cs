using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ErrorLog
{
  public class AsynchLog
  {
    delegate void MethodDelegate(List<string> strError);

    public static void LogNow(List<string> strError)
    {
      IErrorLog log = new ErrorLogFactory().GetErrorLogInstance();

      MethodDelegate callGenerateFileAsync = new MethodDelegate(log.LogError);
      IAsyncResult ar = callGenerateFileAsync.BeginInvoke(strError, null, null);
    }
  }
}
