using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ErrorLog
{
  public interface IErrorLog
  {
    void LogError(List<string> errorList);
  }
}
