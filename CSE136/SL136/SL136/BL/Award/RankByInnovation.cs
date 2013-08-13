using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BL.Award
{
  public class RankByInnovation : IRanking
  {
    public int GetRankScore(string id)
    {
      // retrieve innovation info from database & calculate innovation from 1 to 100.
      // hardcode for now.
      return 90;
    }
  }
}