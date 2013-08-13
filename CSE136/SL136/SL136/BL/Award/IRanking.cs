using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Award
{
  public interface IRanking
  {
    int GetRankScore(string studentId);
  }
}
